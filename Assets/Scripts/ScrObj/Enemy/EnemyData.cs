using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public bool IsRandom;
    public bool IsUnique;
    public List<EnemyParameters> Enemies = new();
}