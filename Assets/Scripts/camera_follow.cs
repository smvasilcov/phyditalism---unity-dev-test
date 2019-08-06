using UnityEngine;

public class camera_follow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.925f;
    public Vector3 offset;
    public float rotationSpeed = 5.0f;

    void FixedUpdate()
    {
        ChooseActiveBall();
        
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * 10 * Time.deltaTime);
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(0))        // You can rotate camera by pressing mouse button and moving mouse
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            offset = camTurnAngle * offset;
            Quaternion camTurnAngleRight = Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * rotationSpeed, Vector3.right);
            offset = camTurnAngleRight * offset;
        }
    }

    void ChooseActiveBall()
    {
        foreach(GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            if (ball.GetComponent<ball_controller>().isActive)
                target = ball.transform;
        }
    }
}
