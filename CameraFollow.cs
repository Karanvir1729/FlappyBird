using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float xOffset = 0f;

    private Vector3 initialOffset;

    void Start()
    {
        if (target != null)
        {
            initialOffset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x + initialOffset.x + xOffset, transform.position.y, initialOffset.z);
            transform.position = targetPosition;
        }
    }
}
