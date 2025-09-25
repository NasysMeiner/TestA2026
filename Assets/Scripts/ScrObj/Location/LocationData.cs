using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Location/LocationData")]
public class LocationData : ScriptableObject
{
    public List<LocationParameters> Locations;
    public GameLooper PrefabGameLooper;
}