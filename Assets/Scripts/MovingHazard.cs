using UnityEngine;

/// <summary>
/// Moves a hazard back and forth along a direction using Mathf.PingPong.
/// </summary>
public class MovingHazard : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection = new Vector3(1f, 0f, 0f);
    [SerializeField] private float moveDistance = 5f;
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        // t will smoothly go from -1 to 1 over time
        float t = Mathf.PingPong(Time.time * moveSpeed, 2f) - 1f;

        // Move around the start position along moveDirection within ±moveDistance
        transform.position = _startPosition + moveDirection.normalized * t * moveDistance;
    }
}