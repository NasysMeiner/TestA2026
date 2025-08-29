using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Factory _factory;

    private List<TypeLocation> _locations;
    private Queue<TypeLocation> _queueLocations = new();

    private DisplayController _displayController;
    private Session _currentSession;
    private LocationManager _locationManager;
    private GameLooper _gameLooper;

    private void OnDestroy()
    {
        UnsubscribeAllEvent();
    }

    public void Init(Factory factory, List<TypeLocation> locations, DisplayController displayController)
    {
        _factory = factory;
        _locations = locations;
        _displayController = displayController;
    }

    public void GenerateSession(TypeClass typeClass)
    {
        if (_currentSession != null)
            DeleteLastSession();

        foreach (TypeLocation typeLocation in _locations)
            _queueLocations.Enqueue(typeLocation);

        //Player
        _currentSession = new(_factory.GenerateEntity(TypeClass.Warrior));

        CreateLevel();
    }

    public void CreateLevel()
    {
        //Location
        CreateLocation(_queueLocations.Dequeue());

        //Enemy
        CreateEnemy(_locationManager.TypeLocation);

        _locationManager.Init(_currentSession.Player.EntityUi, _currentSession.Enemy.EntityUi);
        _gameLooper.Init(_currentSession.Player.Entity, _currentSession.Enemy.Entity);

        _locationManager.StartLocation();
    }

    private void CreateLocation(TypeLocation typeLocation)
    {
        GeneratedLocation location = _factory.GenerateLocation(typeLocation);
        _locationManager = location.LocationManager;
        _gameLooper = location.GameLooper;

        _locationManager.EndAnimation += OnEndAnimation;
        _gameLooper.EndOneLoop += OnEndOneLoop;
        _gameLooper.Death += OnDeath;

        Debug.Log("TypeLocation: " + _locationManager.TypeLocation);
    }

    private void CreateEnemy(TypeLocation typeLocation)
    {
        GeneratedData enemy;

        if (_locationManager.TypeLocation != TypeLocation.Shop)
            enemy = _factory.GenerateEntity();
        else
            enemy = new();

        _currentSession.SetDataEnemy(enemy);
    }

    private void OnEndAnimation()
    {
        Debug.Log("startloop...");
        _gameLooper.MakeOneLoop();
    }

    private void OnEndOneLoop(DamageData damageData)
    {
        Debug.Log("start anim...");
        _locationManager.StartAnimationAttack(damageData);
    }

    private void OnDeath(Entity deathEntity)
    {
        _displayController.ActivateDeathPlayerWindow();
    }

    private void UnsubscribeAllEvent()
    {
        if (_locationManager != null)
            _locationManager.EndAnimation -= OnEndAnimation;

        if (_gameLooper != null)
        {
            _gameLooper.EndOneLoop -= OnEndOneLoop;
            _gameLooper.Death -= OnDeath;
        }
    }

    private void DeleteLastSession()
    {
        UnsubscribeAllEvent();
        _queueLocations.Clear();
        Destroy(_locationManager.gameObject);
        Destroy(_gameLooper.gameObject);
    }
}
