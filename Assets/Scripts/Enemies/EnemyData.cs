using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public enum EnemyType
    {
        smallCube,
        bigCube,
        smallBall,
        bigBall
    }

    [Header("Settings")]
    [SerializeField] private EnemyType type;
    private float startHp = 100f;
    [Range(0.1f, 2f)][SerializeField] float hpMultiplayer = 1f;
    [Range(1f, 10f)][SerializeField] private float basicSpeed = 5f;
    [Range(0.01f, 5f)][SerializeField] float speedMultiplayer = 1f;
    [Range(1, 30)][SerializeField] int xpGain;
    [Range(1, 10)][SerializeField] int scoreGain;

    public EnemyType Type => type;
    public float StartHp => startHp;
    public float HpMultiplayer => hpMultiplayer;
    public float BasicSpeed => basicSpeed;
    public float SpeedMultiplayer => speedMultiplayer;
    public int XpGain => xpGain;
    public int ScoreGain => scoreGain;

}
