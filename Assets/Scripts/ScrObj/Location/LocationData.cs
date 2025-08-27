using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Location/LocationData")]
public class LocationData : ScriptableObject
{
    public bool IsRandom;
    public bool IsUnique;
    public List<Location> Locations;
}

[Serializable]
public struct Location
{
    public TypeLocation TypeLocation;
    public LocationManager PrefabLocation;
}
