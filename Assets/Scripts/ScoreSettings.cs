using UnityEngine;

[CreateAssetMenu(menuName = "Create ScoreSettings", fileName = "ScoreSettings", order = 0)]
public class ScoreSettings : ScriptableObject
{
    [SerializeField] private int _successPoints;
    [SerializeField] private int _failPoints;

    public int SuccessPoints => _successPoints;
    public int FailPoints => _failPoints;
}