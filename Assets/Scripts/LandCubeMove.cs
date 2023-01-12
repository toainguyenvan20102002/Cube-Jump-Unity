using UnityEngine;

public class LandCubeMove : MonoBehaviour
{
    [SerializeField] private Transform endPosition;
    [SerializeField] private float speedMove;

    [Header("Line Position")]
    [SerializeField] private Transform xLineLeft, xLineMid, xLineRight;

    private int numOrder;
    private bool isRotated;

    #region SET_GET

    public int NumOrder { get => numOrder; set => numOrder = value; }
    public bool IsRotated { get => isRotated; set => isRotated = value; }
    public float SpeedMove { get => speedMove; set => speedMove = value; }

    #endregion

    private void Start()
    {
        if (transform.position.x == xLineLeft.position.x) numOrder = 0;
        else if (transform.position.x == xLineMid.position.x) numOrder = 1;
        else numOrder = 2;

        isRotated = false;
    }

    private void Update()
    {
        ResetPosition();
        MoveX();
    }

    private void ResetPosition()
    {
        if(this.transform.position.z <= endPosition.position.z && isRotated == false)
        {
            isRotated = true;
            LaneManager.instance.RotateLandCube();
        }
    }

    private void MoveX()
    {
        transform.Translate(Vector3.forward * -1 * SpeedMove * Time.deltaTime);
    }
}
