using UnityEngine;

public class CompositeRootStart : CompositeRoot
{
    [Header("GameManager")]
    [SerializeField] private GameManager _gameManager;
    [Header("Location Factory")]
    [SerializeField] private Factory _factory;
    [SerializeField] private LocationFactory _locationFactory;
    [SerializeField] private LocationData _locationData;
    [Header("Player Factory")]
    [SerializeField] private PlayerFactory _playerFactory;
    [SerializeField] private PlayerData _playerData;

    public override void Compose()
    {
        _locationFactory.Init(_locationData);
        _playerFactory.Init(_playerData);
        _factory.Init(_locationFactory, _playerFactory);

        _gameManager.Init(_factory);
    }
}
