using UnityEngine;

public class ScreenDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _display;
    [SerializeField] private GameState _displayInGameState;

    public void Start()
    {
        MessageBus.Instance.Subscribe<GameState>(state => _display.SetActive(state == _displayInGameState));
    }
}