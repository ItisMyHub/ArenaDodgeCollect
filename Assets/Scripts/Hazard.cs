using UnityEngine;

/// <summary>
/// Hazard that ends the run when the player collides with it.
/// </summary>
[RequireComponent(typeof(Collider))]
public class Hazard : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Only react when colliding with the Player
        if (collision.collider.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}