using UnityEngine;

public class GameManager : MonoBehaviour
{
    private LocationFactory _locationFactory;
    private Factory _factory;

    private int _countFight = 3;

    private LocationManager _locationManager;
    private GameLooper _gameLooper;

    private TypeLocation _lastLocation = TypeLocation.Empty;

    public void Init(Factory factory)
    {
        _factory = factory;
    }

    public void CreateLevel()
    {
        GeneratedLocation location = _factory.GenerateLocation(TypeClass.Warrior, _lastLocation == TypeLocation.Empty ? TypeLocation.Empty : TypeLocation.Shop);
        _locationManager = location.LocationManager;
        _gameLooper = location.GameLooper;

        //Entity player = _fabrick.CreatePlayer(typeClass);
        //Entity[] enemy1..2..3 = _fabrick.CreateEnemy(Random); x3
        _locationManager.StartLocation();
    }
}
