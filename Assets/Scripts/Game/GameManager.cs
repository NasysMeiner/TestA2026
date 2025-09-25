using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _maxLevelPlayer = 3;

    private Factory _factory;

    private List<TypeLocation> _locations;
    private Queue<TypeLocation> _queueLocations = new();

    private DisplayController _displayController;
    private Session _currentSession;
    private LocationManager _locationManager;
    private GameLooper _gameLooper;

    private bool _isEndLoocation = false;

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
        _currentSession = new(_factory.GenerateEntity(typeClass));

        CreateLevel();
    }

    public void CreateLevel()
    {
        if (_locationManager != null)
            DeleteLevel();

        //Location
        CreateLocation(_queueLocations.Dequeue());

        //Enemy
        CreateEnemy(_locationManager.TypeLocation);

        _gameLooper.Init(_currentSession.Player.Entity, _currentSession.Enemy.Entity);
        _locationManager.Init(_currentSession, _displayController.ActionView, _displayController.HealthViewController);

        _displayController.SetStats(_currentSession.Player.Entity, _currentSession.Enemy.Entity, _currentSession.Enemy.EntityUi);

        if (_locationManager.TypeLocation == TypeLocation.Shop)
            _displayController.EnableLevelUpWindow(_currentSession.Player.Entity.LevelData.CurrentLevel >= _maxLevelPlayer);

        if (!_isEndLoocation)
            _locationManager.StartLocation();
    }

    public void LevelUpPlayer(TypeClass typeClass)
    {
        _factory.LevelUpEntity(_currentSession.Player.Entity, typeClass);
        _isEndLoocation = true;
    }

    public void EndLocation()
    {
        _locationManager.EndLocation();
        _isEndLoocation = true;
    }

    public void SwapWeapon()
    {
        _currentSession.Player.Entity.SetWeapon(_factory.CreateWeapon(_currentSession.Enemy.Entity.Weapon.TypeWeapon));
    }

    private void CreateLocation(TypeLocation typeLocation)
    {
        GeneratedLocation location = _factory.GenerateLocation(typeLocation);
        _locationManager = location.LocationManager;
        _gameLooper = location.GameLooper;

        _locationManager.EndAnimation += OnEndAnimation;
        _gameLooper.EndOneLoop += OnEndOneLoop;
        _gameLooper.Death += OnDeath;
    }

    private void CreateEnemy(TypeLocation typeLocation)
    {
        GeneratedData enemy;

        if (_locationManager.TypeLocation != TypeLocation.Shop && _locationManager.TypeLocation != TypeLocation.LocationEnd)
            enemy = _factory.GenerateEntity();
        else
            enemy = new();

        _currentSession.SetDataEnemy(enemy);
    }

    private void OnEndAnimation()
    {
        if (_locationManager.TypeLocation == TypeLocation.LocationEnd)
        {
            _displayController.EnableEndWindow();

            return;
        }

        if (!_isEndLoocation && _locationManager.TypeLocation != TypeLocation.Shop)
            _gameLooper.MakeOneLoop();
        else if (_isEndLoocation)
            CreateLevel();
    }

    private void OnEndOneLoop(DamageData damageData)
    {
        _displayController.SetStats(_currentSession.Player.Entity, _currentSession.Enemy.Entity, _currentSession.Enemy.EntityUi);

        _locationManager.StartAnimationAttack(damageData);
    }

    private void OnDeath(Entity deathEntity)
    {
        if (deathEntity.TypeEntity == TypeEntity.Player)
        {
            _displayController.ActivateDeathPlayerWindow();
        }
        else
        {
            _isEndLoocation = true;
            _currentSession.Enemy.Entity.SetWeapon(_factory.CreateWeapon(_currentSession.Enemy.Entity.Weapon.TypeWeapon));
            _displayController.ActivateNextLocationWindow(_currentSession.Player.Entity.Weapon, _currentSession.Enemy.Entity.Weapon);
        }
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
        _queueLocations.Clear();
        DeleteLevel();
        Destroy(_currentSession.Player.EntityUi.gameObject);
    }

    private void DeleteLevel()
    {
        _isEndLoocation = false;
        UnsubscribeAllEvent();
        Destroy(_locationManager.gameObject);
        Destroy(_gameLooper.gameObject);
    }
}
