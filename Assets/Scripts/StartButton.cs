using System;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameObject _startScreen;
    
    public void OnStartButtonClick()
    {
        Timer.Instance.StartTimer();
        _startScreen.SetActive(false);
    }
}