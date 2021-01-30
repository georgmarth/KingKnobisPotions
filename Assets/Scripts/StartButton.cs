using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        MainGameLoop.Instance.StartGame();
    }
}