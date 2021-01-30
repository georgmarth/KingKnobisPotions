using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Start()
    {
        _scoreText.text = ScoreSystem.Instance.Score.ToString();
        MessageBus.Instance.Subscribe<ScoreUpdatedEvent>(UpdateScoreDisplay);
    }

    private void UpdateScoreDisplay(ScoreUpdatedEvent obj)
    {
        _scoreText.text = obj.Score.ToString();
    }
}