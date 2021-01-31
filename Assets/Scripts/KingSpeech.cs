using UnityEngine;
using UnityEngine.Audio;

public class KingSpeech : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private AudioSource _kingSpeech;

    private void Start()
    {
        MessageBus.Instance.Subscribe<GameState>(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.TitleScreen)
        {
            _mixer.SetFloat("musicVol", -25f);
            _kingSpeech.Play();
        }
        else
        {
            _mixer.SetFloat("musicVol", -10f);
            _kingSpeech.Stop();
        }
    }
}