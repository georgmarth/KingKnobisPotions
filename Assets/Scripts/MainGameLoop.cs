using System.Collections;
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

    private GameState _gameState;

    public int TimeOutInSeconds => _timeOutInSeconds;

    public Recipe CurrentRecipe { get; private set; }

    public void StartGame()
    {
        Timer.Instance.StartTimer();
        CreateNewRecipe();
        SetGameState(GameState.Running);
    }

    private void CreateNewRecipe()
    {
        CurrentRecipe = GetRandomRecipe();
        MessageBus.Instance.Publish(new NewRecipeEvent {Recipe = CurrentRecipe});
    }

    private IEnumerator CreateDelayedRecipe()
    {
        yield return new WaitForSeconds(1.5f);
        CreateNewRecipe();
    }

    private void Start()
    {
        MessageBus.Instance.Subscribe<TimeElapsedEvent>(OnTimeElapsed);
        MessageBus.Instance.Subscribe<PotionCorrectEvent>(OnPotionCorrect);
        MessageBus.Instance.Subscribe<WrongIngredientEvent>(OnWrongIngredient);
        SetGameState(GameState.TitleScreen);
    }

    private void OnTimeElapsed(TimeElapsedEvent evt)
    {
        if (evt.ElapsedTime.Seconds >= _timeOutInSeconds)
        {
            GameOver();
        }
    }

    private void OnPotionCorrect(PotionCorrectEvent evt)
    {
        StartCoroutine(CreateDelayedRecipe());
    }

    private void OnWrongIngredient(WrongIngredientEvent evt)
    {
        StartCoroutine(CreateDelayedRecipe());
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        Timer.Instance.StopTimer();
        SetGameState(GameState.GameOver);
    }

    private void SetGameState(GameState gameState)
    {
        _gameState = gameState;
        MessageBus.Instance.Publish(_gameState);
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