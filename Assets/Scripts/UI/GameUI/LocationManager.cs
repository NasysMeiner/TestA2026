using UnityEngine;

public class LocationManager : MonoBehaviour
{
    [SerializeField] private TypeLocation _typeLocation;
    [SerializeField] private GameObject _playerStartPoint;
    [SerializeField] private GameObject _playerPoint;
    [SerializeField] private GameObject _enemyStartPoint;
    [SerializeField] private GameObject _enemyPoint;

    private PlayerUi _playerUi;

    private void OnDisable()
    {
        _playerUi.AtPoint -= OnAtPoint;
    }

    public void Init(PlayerUi playerUi)
    {
        _playerUi = playerUi;
        _playerUi.transform.position = _playerStartPoint.transform.position;
        _playerUi.AtPoint += OnAtPoint;
    }

    public void StartLocation()
    {
        _playerUi.SetPoint(_playerPoint.transform.position);
    }

    public void OnAtPoint()
    {

    }
}
