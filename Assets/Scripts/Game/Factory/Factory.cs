using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    private LocationFactory _locationFactory;
    private PlayerFactory _playerFactory;

    public void Init(LocationFactory locationFactory, PlayerFactory playerFactory)
    {
        _locationFactory = locationFactory;
        _playerFactory = playerFactory;
    }

    public GeneratedLocation GenerateLocation(TypeClass typeClass, TypeLocation typeLocation)
    {
        GeneratedLocation generatedLocation = new();
        generatedLocation.LocationManager = _locationFactory.CreateLocation(typeLocation);

        return generatedLocation;
    }
}
