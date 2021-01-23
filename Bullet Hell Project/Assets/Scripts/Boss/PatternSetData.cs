using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPatternSetData", menuName = "Data/Pattern Set Data")]
public class PatternSetData : ScriptableObject
{
    #region Variables
    [Header("Pattern Set")]
    [SerializeField] List<PatternData> patternSets;

    [Header("Settings")]
    [SerializeField] float patternSetDuration = 3f;
    [SerializeField] float timeUntilNextPattern = 1f;
    #endregion

    #region Getters and Setters
    public List<PatternData> PatternSets { get => patternSets; set => patternSets = value; }
    public float PatternSetDuration { get => patternSetDuration; set => patternSetDuration = value; }
    public float TimeUntilNextPattern { get => timeUntilNextPattern; set => timeUntilNextPattern = value; }
    #endregion
}
