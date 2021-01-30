using UnityEngine;

public enum GameState
{
    TitleScreen,
    Running,
    GameOver
}

public class MainGameLoop : Singleton<MainGameLoop>
{
    [SerializeField] private int _timeOutInSeconds = 5;
    [SerializeField] private IngredientList _ingredientList;

    private GameState _gameState = GameState.TitleScreen;

    public int TimeOutInSeconds => _timeOutInSeconds;

    public Recipe CurrentRecipe { get; private set; }

    public void StartGame()
    {
        Timer.Instance.StartTimer();
        CurrentRecipe = GetRandomRecipe();
        MessageBus.Instance.Publish(new NewRecipeEvent{Recipe = CurrentRecipe});
    }
    
    private void Start()
    {
        MessageBus.Instance.Subscribe<TimeElapsedEvent>(OnTimeElapsed);
        _gameState = GameState.Running;
    }

    private void OnTimeElapsed(TimeElapsedEvent evt)
    {
        if (evt.ElapsedTime.Seconds >= _timeOutInSeconds)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        Timer.Instance.StopTimer();
        _gameState = GameState.GameOver;
    }

    private Recipe GetRandomRecipe()
    {
        return new Recipe
        {
            ColorIngredient = _ingredientList.ColorIngredients.SelectRandom(),
            SymbolIngredient = _ingredientList.SymbolIngredients.SelectRandom()
        };
    }
}