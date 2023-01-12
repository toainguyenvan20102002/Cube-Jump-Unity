
using UnityEngine;

public class Raiser : MonoBehaviour
{
    private float raiseHorizontal;
    private float raiseVertical;

    private float oldPositionX;
    private float oldPositionY;

    private float direc;
    private float ratio = 0.6f;

    private float padding = 1f;

    private float minSpeed = 3f;
    private float maxSpeed = 10f;

    private float speed;

    private void Start()
    {
        oldPositionX = transform.position.x;
        oldPositionY = transform.position.y;

        if (Random.Range(-1, 1) >= 0) direc = 1;
        else direc = -1;

        speed = Random.Range(minSpeed, maxSpeed);
        raiseHorizontal = Random.Range(0, 2);
        raiseVertical = 1 - raiseHorizontal;
    }
    private void Update()
    {
        float moveDistance = (Mathf.Sin(Time.time * speed) * direc) + padding;
        Vector3 nextPosition = new Vector3(oldPositionX + moveDistance*raiseHorizontal,
                                            oldPositionY + moveDistance*raiseVertical,
                                            transform.position.z);
        transform.position = Vector3.Lerp(transform.position, nextPosition, ratio);
    }
}
