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
            resultText.text = $"You lost!\nTime survived: {time:F1} seconds";
        }
        else
        {
            resultText.text = "You lost!";
        }
    }
}