using UnityEngine;

public class CameraContol : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 Offset;


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + Offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
