using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPhaseData", menuName = "Data/Phase Data")]
public class PhaseData : ScriptableObject
{
    #region Variables
    [SerializeField] List<PatternSetData> patternSetData;

    [SerializeField] int phase = 0;
    [SerializeField] float changePhaseAtThisHealth = 7000f;
    #endregion

    #region Getters and Setters
    public List<PatternSetData> PatternSetData { get => patternSetData; set => patternSetData = value; }
    public int Phase { get => phase; set => phase = value; }
    public float ChangePhaseAtThisHealth { get => changePhaseAtThisHealth; set => changePhaseAtThisHealth = value; }
    #endregion
}
