using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float forwardSpeed = 5f; // Speed of forward movement

    void Update()
    {
        // Move the camera straight forward
        transform.position += Vector3.right * forwardSpeed * Time.deltaTime;
    }
}