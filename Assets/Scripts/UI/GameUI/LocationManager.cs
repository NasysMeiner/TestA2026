using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LocationManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerStartPoint;
    [SerializeField] private GameObject _playerPoint;
    [SerializeField] private GameObject _enemyStartPoint;
    [SerializeField] private GameObject _enemyPoint;

    private EntityUi _playerUi;
    private EntityUi _enemyUi;

    private int _countReady = 3;
    private int _isReady = 0;

    private TypeLocation _typeLocation;

    public event UnityAction EndAnimation;

    public TypeLocation TypeLocation => _typeLocation;

    private void OnDestroy()
    {
        UnsubscribeAllEvent();
    }

    public void SetTypeLocation(TypeLocation typeLocation)
    {
        _typeLocation = typeLocation;
    }

    public void Init(EntityUi playerUi, EntityUi enemyUi)
    {
        if(playerUi != null)
        {
            _playerUi = playerUi;
            _playerUi.transform.position = _playerStartPoint.transform.position;
            _playerUi.AtPoint += OnAtPoint;
        }
        
        if(enemyUi != null)
        {
            _enemyUi = enemyUi;
            _enemyUi.transform.position = _enemyStartPoint.transform.position;
            _enemyUi.transform.SetParent(gameObject.transform);
            _enemyUi.AtPoint += OnAtPoint;
        }
    }

    public void StartLocation()
    {
        if (_playerUi != null)
            _playerUi.SetPoint(_playerPoint.transform.position);

        if (_enemyUi != null)
            _enemyUi.SetPoint(_enemyPoint.transform.position);

        OnAtPoint();
        Debug.Log("StartLocation anim...");
    }

    public void OnAtPoint()
    {
        _isReady++;

        if (_isReady == _countReady)
        {
            Debug.Log("end anim...");
            EndAnimation?.Invoke();
            _isReady = 0;
        }
    }

    internal void StartAnimationAttack(DamageData damageData)
    {
        EntityUi attacker = _playerUi;
        EntityUi target = _enemyUi;

        if (damageData.Attacker.TypeEntity != TypeEntity.Player)
            (attacker, target) = (target, attacker);

        //SetSkillAttribute;

        StartCoroutine(AnimationAttack(attacker, target, damageData.IsDead));
    }

    private IEnumerator AnimationAttack(EntityUi attacker, EntityUi target, bool isDead)
    {
        Vector3 startPos = attacker.transform.position;

        yield return attacker.Move(target.transform.position, true);

        yield return attacker.Attack();
        yield return target.TakeDamageAnimation(isDead);

        yield return new WaitForSeconds(0.5f);

        yield return attacker.Move(startPos);

        OnAtPoint();
    }

    private void UnsubscribeAllEvent()
    {
        if (_playerUi != null)
        {
            _playerUi.AtPoint -= OnAtPoint;
        }

        if (_enemyUi != null)
        {
            _enemyUi.AtPoint -= OnAtPoint;
            Destroy(_enemyUi.gameObject);
        }
    }
}
