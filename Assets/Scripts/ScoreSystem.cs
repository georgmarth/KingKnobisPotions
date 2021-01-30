using UnityEngine;

public class ScoreSystem : Singleton<ScoreSystem>
{
    [SerializeField] private ScoreSettings _scoreSettings;

    public int Score { get; private set; }

    private void Start()
    {
        Score = 0;
        MessageBus.Instance.Subscribe<PotionCorrectEvent>(OnPotionCorrect);
        MessageBus.Instance.Subscribe<WrongIngredientEvent>(OnPotionInCorrect);
    }

    private void OnPotionInCorrect(WrongIngredientEvent obj)
    {
        UpdateScore(_scoreSettings.FailPoints);
    }

    private void OnPotionCorrect(PotionCorrectEvent evt)
    {
        UpdateScore(_scoreSettings.SuccessPoints);
    }

    private void UpdateScore(int points)
    {
        Score += points;
        Score = Mathf.Max(Score, 0);
        MessageBus.Instance.Publish(new ScoreUpdatedEvent{Score = Score, NewPoints = points});
    }
}