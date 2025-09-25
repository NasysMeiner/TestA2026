using UnityEngine;

public class LocationFactory : MonoBehaviour
{
    private LocationData _locationData;

    public void Init(LocationData locationData)
    {
        _locationData = locationData;
    }

    public GeneratedLocation CreateLocation(TypeLocation typeLocation)
    {
        GeneratedLocation generated = new();
        LocationParameters location = SearchLocation(typeLocation);

        LocationManager locationManager = Instantiate(location.PrefabLocation);
        locationManager.SetTypeLocation(location.TypeLocation);
        locationManager.transform.position = Vector3.zero;
        generated.LocationManager = locationManager;
        generated.GameLooper = Instantiate(_locationData.PrefabGameLooper);
        return generated;
    }

    private LocationParameters SearchLocation(TypeLocation typeLocation)
    {
        foreach (LocationParameters loc in _locationData.Locations)
            if (loc.TypeLocation == typeLocation)
                return loc;

        return new();
    }
}
