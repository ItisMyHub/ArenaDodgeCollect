using UnityEngine;

public class MovingHazard : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection = new Vector3(0f, 0f, 1f);
    [SerializeField] private float moveDistance = 5f;
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        // Ping-pong value between -1 and 1
        float t = Mathf.PingPong(Time.time * moveSpeed, 2f) - 1f;

        // Move around the start position along moveDirection
        transform.position = _startPosition + moveDirection.normalized * t * moveDistance;
    }
}