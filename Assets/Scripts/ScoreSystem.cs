using UnityEngine;

public class ScoreSystem : Singleton<ScoreSystem>
{
    [SerializeField] private ScoreSettings _scoreSettings;

    public int Score { get; private set; }

    private void Start()
    {
        MessageBus.Instance.Subscribe<PotionCorrectEvent>(OnPotionCorrect);
        MessageBus.Instance.Subscribe<WrongIngredientEvent>(OnPotionInCorrect);
        MessageBus.Instance.Subscribe<GameState>(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.Running)
            SetScore(0);
    }

    private void OnPotionInCorrect(WrongIngredientEvent evt)
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

    private void SetScore(int points)
    {
        Score = points;
        MessageBus.Instance.Publish(new ScoreUpdatedEvent{Score = Score});
    }
}