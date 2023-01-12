using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int numOrder = 0;

    float timeToJump = 0;

    bool isJumping = false;

    float vZero = 0;

    //float a = -5.0f / 3;
    //float b = 3;
    float a = -20.0f / 9;
    float b = 4f;

    float countTime = 0;
    int direction = 0;

    float checkGroundRadius = 0.75f;
    bool isLose = false;


    private Rigidbody rb;
    [Header("Line Position")]
    [SerializeField] private Transform[] arrLinePosition;

    [Header("Check Game")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform checkGroundPosition;
    void Start()
    {
        CalTimeToJump();
        CheckNumOrder();
        SetVZero();

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void CalTimeToJump()
    {
        timeToJump = 1.0f / LaneManager.GetSpeed();
    }

    void CheckNumOrder()
    {
        if (transform.position.x == arrLinePosition[0].position.x)
            numOrder = 0;
        else if (transform.position.x == arrLinePosition[1].position.x)
            numOrder = 1;
        else numOrder = 2;
    }

    void SetVZero()
    {
        vZero = 1.8f / (timeToJump);
    }

    // Su dung phuong trinh 
    void Jump(int direction)
    {
        if (!isJumping || (numOrder + direction < 0) || (numOrder + direction) > 2 || isLose) return;
        countTime += Time.deltaTime;

        float deltax = vZero * direction * countTime;

        float x = arrLinePosition[numOrder].position.x + deltax;
        float y = arrLinePosition[numOrder].position.y + a * Mathf.Pow(deltax, 2) + b * Mathf.Abs(deltax);

        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, transform.position.z), 0.6f);
        if (countTime >= timeToJump)
        {
            isJumping = false;
            countTime = 0;
            numOrder = numOrder + direction;
            transform.position = arrLinePosition[numOrder].position;
            if (!CheckGround())
            {
                rb.useGravity = true;
                isLose = true;
                rb.velocity = new Vector3(0, -8, 0);
                GameManager.instance.EndGame();
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && !isJumping)
        {
            if (numOrder <= 0) return;
            isJumping = true;
            direction = -1;
        }
        else if(Input.GetKeyDown(KeyCode.D) && !isJumping)
        {
            if (numOrder >= 2) return;
            isJumping = true;
            direction = 1;
        }
        Jump(direction);

        if (!CheckGround() && !isJumping)
        {
            rb.useGravity = true;
            isLose = true;
            rb.velocity = new Vector3(0, -8, 0);
            GameManager.instance.EndGame();
        }

    }

    bool CheckGround()
    {
        if (Physics.OverlapSphere(checkGroundPosition.position,checkGroundRadius,whatIsGround).Length == 0)
        {
            return false;
        }
        return true;
    }
}
