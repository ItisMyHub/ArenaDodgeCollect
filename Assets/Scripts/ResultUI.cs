using UnityEngine;
using TMPro;  // If you're using TextMeshPro. If using legacy Text, adjust below.

/// <summary>
/// Displays the final result information (time survived and score) on the Result screen.
/// </summary>
public class ResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    // If using legacy UI Text instead:
    // [SerializeField] private UnityEngine.UI.Text resultText;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            float time = GameManager.Instance.ElapsedTime;
            int score = GameManager.Instance.Score;

            resultText.text = $"You lost!\nTime survived: {time:F1} seconds\nScore: {score}";
        }
        else
        {
            resultText.text = "You lost!";
        }
    }
}