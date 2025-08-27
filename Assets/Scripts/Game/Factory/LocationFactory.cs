using System.Collections.Generic;
using UnityEngine;

public class LocationFactory : MonoBehaviour
{
    private LocationData _locationData;

    private Dictionary<TypeLocation, int> _lastLocations;
    private int _currentLoc = 0;

    public void Init(LocationData locationData)
    {
        _locationData = locationData;
    }

    public LocationManager CreateLocation(TypeLocation typeLocation)
    {
        Location location;

        if (_locationData.IsRandom)
        {
            if (_locationData.IsUnique && _lastLocations.Count == _locationData.Locations.Count)
                _lastLocations.Clear();

            while (true)
            {
                _currentLoc = Random.Range(0, _locationData.Locations.Count);

                if (_locationData.IsUnique)
                {
                    if (_lastLocations.ContainsKey(_locationData.Locations[_currentLoc].TypeLocation))
                        continue;
                    else
                        _lastLocations.Add(_locationData.Locations[_currentLoc].TypeLocation, 1);
                }

                break;
            }

            location = _locationData.Locations[_currentLoc];
        }
        else
        {
            location = _locationData.Locations[_currentLoc++];
        }

        LocationManager locationManager = Instantiate(location.PrefabLocation);
        locationManager.transform.position = Vector3.zero;
        return locationManager;
    }
}
