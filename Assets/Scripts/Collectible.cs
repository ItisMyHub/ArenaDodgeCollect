using UnityEngine;

/// <summary>
/// Collectible object that increases the player's score when picked up.
/// </summary>
[RequireComponent(typeof(Collider))]
public class Collectible : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        // Only react when the Player enters the trigger
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(scoreValue);
            }

            // Remove the collectible after it has been collected
            Destroy(gameObject);
        }
    }
}