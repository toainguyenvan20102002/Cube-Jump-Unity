using UnityEngine;

public class WallMove : MonoBehaviour
{
    [SerializeField] private Transform startPosition;

    [SerializeField] private Transform endPosition;

    [SerializeField] private float speedMove = 10f;

    private void Update()
    {
        RestartPosition();
        MoveX();
    }

    void RestartPosition()
    {
        if(transform.position.z <= endPosition.position.z)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y,startPosition.position.z);
        }
    }

    private void MoveX()
    {
        transform.Translate(Vector3.forward * -1 * speedMove * Time.deltaTime);
    }
}
