using System;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameObject _startScreen;
    
    public void OnStartButtonClick()
    {
        MainGameLoop.Instance.StartGame();
        _startScreen.SetActive(false);
    }
}