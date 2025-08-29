using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Location/LocationData")]
public class LocationData : ScriptableObject
{
    public List<Location> Locations;
    public GameLooper PrefabGameLooper;
}

[Serializable]
public struct Location
{
    public TypeLocation TypeLocation;
    public LocationManager PrefabLocation;
}
