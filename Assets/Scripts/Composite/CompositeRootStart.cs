using System.Collections.Generic;
using UnityEngine;

public class CompositeRootStart : CompositeRoot
{
    [Header("GameManager")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private List<TypeLocation> _locQue;
    [Header("Location Factory")]
    [SerializeField] private Factory _factory;
    [SerializeField] private LocationFactory _locationFactory;
    [SerializeField] private LocationData _locationData;
    [Header("Player Factory")]
    [SerializeField] private EntityFactory _playerFactory;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private SkillData _skillData;
    [SerializeField] private AttributeData _attributeData;
    [Header("UI")]
    [SerializeField] private DisplayController _prefabCanvas;
    [SerializeField] private UiWeaponData UiWeaponData;

    private DisplayController _displayController;

    public override void Compose()
    {
        _displayController = Instantiate(_prefabCanvas);

        _locationFactory.Init(_locationData);
        _playerFactory.Init(_playerData, _enemyData, _weaponData, _skillData, _attributeData);
        _factory.Init(_locationFactory, _playerFactory);

        _gameManager.Init(_factory, _locQue, _displayController);

        _displayController.Init(_gameManager, UiWeaponData);
    }
}
