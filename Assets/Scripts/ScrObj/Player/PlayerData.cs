using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int MinValue = 1;
    public int MaxValue = 3;
    public List<PlayerParameters> Players = new();
}