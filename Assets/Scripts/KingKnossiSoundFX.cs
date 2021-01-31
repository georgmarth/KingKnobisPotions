using UnityEngine;

public class KingKnossiSoundFX : MonoBehaviour
{
    [SerializeField] private AudioClip[] _correctIngredientSounds;
    [SerializeField] private AudioClip[] _wrongIngredientSounds;
    [SerializeField] private AudioClip[] _correctPotionSounds;

    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        MessageBus.Instance.Subscribe<PotionCorrectEvent>(PlayCorrectPotionSound);
        MessageBus.Instance.Subscribe<WrongIngredientEvent>(PlayWrongIngredientSound);
        MessageBus.Instance.Subscribe<CorrectIngredientEvent>(PlayCorrectIngredientSound);
    }

    private void PlayCorrectIngredientSound(CorrectIngredientEvent evt)
    {
        _audioSource.clip = _correctIngredientSounds.SelectRandom();
        _audioSource.Play();
    }

    private void PlayWrongIngredientSound(WrongIngredientEvent evt)
    {
        _audioSource.clip = _wrongIngredientSounds.SelectRandom();
        _audioSource.Play();
    }

    private void PlayCorrectPotionSound(PotionCorrectEvent evt)
    {
        _audioSource.clip = _correctPotionSounds.SelectRandom();
        _audioSource.Play();
    }
}