using UnityEngine;

public class MainGameLoop : Singleton<MainGameLoop>
{
    [SerializeField] private int _timeOutInSeconds = 5;

    public int TimeOutInSeconds
    {
        get { return _timeOutInSeconds; }
    }

    private void Start()
    {
        MessageBus.Instance.Subscribe<TimeElapsedEvent>(OnTimeElapsed);
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
    }
}