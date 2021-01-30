using System;
using UnityEngine;

public class Timer : Singleton<Timer>
{
    public TimeSpan ElapsedTime { get; private set; }
    private bool _timerRunning; 
    
    public void StartTimer()
    {
        ResetTimer();
        ResumeTimer();
    }

    public void ResumeTimer()
    {
        _timerRunning = true;
    }

    public void ResetTimer()
    {
        ElapsedTime = new TimeSpan(0);
    }

    public void PauseTimer()
    {
        _timerRunning = false;
    }

    public void StopTimer()
    {
        _timerRunning = false;
        ResetTimer();
    }

    private void Update()
    {
        if (_timerRunning)
        {
            ElapsedTime += TimeSpan.FromSeconds(Time.deltaTime);
            MessageBus.Instance.Publish(new TimeElapsedEvent{ElapsedTime = ElapsedTime}, false);
        }
    }
}