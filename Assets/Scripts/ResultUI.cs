using UnityEngine;
using TMPro; 

public class ResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText; 

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