using UnityEngine;

public class SceneReset : MonoBehaviour
{
    private void Awake()
    {
        MessageBus.ClearSubscriptions();
    }
}