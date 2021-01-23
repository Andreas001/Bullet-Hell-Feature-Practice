using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBossData", menuName = "Data/Boss Data")]
public class BossData : ScriptableObject
{
    #region Variables
    [Header("Stats")]
    [SerializeField] float health = 10000f;
    [SerializeField] int startingPhase = 0;
    [SerializeField] float startFiringAfterSeconds = 3f;

    [Header("Pattern Sets")]
    [SerializeField] List<PhaseData> phases;
    #endregion

    #region Getters and Setters
    public float Health { get => health; set => health = value; }
    public int StartingPhase { get => startingPhase; set => startingPhase = value; }
    public float StartFiringAfterSeconds { get => startFiringAfterSeconds; set => startFiringAfterSeconds = value; }
    public List<PhaseData> Phases { get => phases; set => phases = value; }
    #endregion
}
