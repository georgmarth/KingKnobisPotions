using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _addedScoreText;
    [SerializeField] private Animator _addedScoreAnimator;

    private void Start()
    {
        _scoreText.text = ScoreSystem.Instance.Score.ToString();
        MessageBus.Instance.Subscribe<ScoreUpdatedEvent>(UpdateScoreDisplay);
    }

    private void UpdateScoreDisplay(ScoreUpdatedEvent obj)
    {
        _scoreText.text = obj.Score.ToString();
        _addedScoreText.text = obj.NewPoints.ToString();
        
        if (obj.NewPoints > 0)
            _addedScoreAnimator.SetTrigger("Added");
        else if (obj.NewPoints < 0)
            _addedScoreAnimator.SetTrigger("Removed");
    }
}