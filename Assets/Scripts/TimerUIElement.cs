using System;
using TMPro;
using UnityEngine;

public class TimerUIElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    
    private void Start()
    {
        MessageBus.Instance.Subscribe<TimeElapsedEvent>(OnTimeElapsed);
    }

    private void OnTimeElapsed(TimeElapsedEvent evt)
    {
        _text.text = $"Time elapsed: {TimeLeft(evt.ElapsedTime):mm\\:ss\\:FFF}";
    }

    private TimeSpan TimeLeft(TimeSpan timeElapsed)
    {
        return TimeSpan.FromSeconds(MainGameLoop.Instance.TimeOutInSeconds) - timeElapsed;
    }
}