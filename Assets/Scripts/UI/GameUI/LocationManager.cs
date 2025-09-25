using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LocationManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerStartPoint;
    [SerializeField] private GameObject _playerPoint;
    [SerializeField] private GameObject _enemyStartPoint;
    [SerializeField] private GameObject _enemyPoint;

    private ActionView _actionView;
    private HealthViewController _healthViewController;

    private EntityUi _playerUi;
    private EntityUi _enemyUi;

    private int _countReady;
    private int _countReadyAnimEndLoc = 1;
    private int _countReadyAnimAttack = 3;
    private int _isReady;

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

    public void Init(Session currentSession, ActionView actionView, HealthViewController healthViewController)
    {
        if(currentSession.Player.EntityUi != null)
        {
            _playerUi = currentSession.Player.EntityUi;
            _playerUi.transform.position = _playerStartPoint.transform.position;
            _playerUi.AtPoint += OnAtPoint;
        }
        
        if(currentSession.Enemy.EntityUi != null)
        {
            _enemyUi = currentSession.Enemy.EntityUi;
            _enemyUi.transform.position = _enemyStartPoint.transform.position;
            _enemyUi.transform.SetParent(gameObject.transform);
            _enemyUi.AtPoint += OnAtPoint;
        }

        _actionView = actionView;
        _healthViewController = healthViewController;
        _healthViewController.StartLocation(currentSession);
    }

    public void StartLocation()
    {
        _countReady = 0;
        _isReady = 0;

        if (_playerUi != null)
        {
            _playerUi.SetPoint(_playerPoint.transform.position);
            _countReady++;
        }

        if (_enemyUi != null)
        {
            _enemyUi.SetPoint(_enemyPoint.transform.position);
            _countReady++;
        }

        Debug.Log("StartLocation anim...");
    }

    public void EndLocation()
    {
        _countReady = _countReadyAnimEndLoc;

        if (_playerUi != null)
        {
            Debug.Log("Ready");
            _playerUi.SetPoint(_enemyStartPoint.transform.position);
        }
    }

    public void OnAtPoint()
    {
        _isReady++;

        if (_isReady == _countReady)
        {
            _isReady = 0;
            EndAnimation?.Invoke();
        }
    }

    internal void StartAnimationAttack(DamageData damageData)
    {
        EntityUi attacker = _playerUi;
        EntityUi target = _enemyUi;

        if (damageData.Attacker.TypeEntity != TypeEntity.Player)
            (attacker, target) = (target, attacker);

        if(damageData.Attacker.TypeEntity == TypeEntity.Enemy)
            StartCoroutine(AnimationAttack(attacker, target, damageData));
        else
            StartCoroutine(AnimationAttack(attacker, target, damageData, damageData.Attacker.Weapon.TypeWeapon));
    }

    private IEnumerator AnimationAttack(EntityUi attacker, EntityUi target, DamageData damageData, TypeWeapon playerWeapon = TypeWeapon.Empty)
    {
        Vector3 startPos = attacker.transform.position;

        _countReady = _countReadyAnimAttack;

        yield return attacker.Attack(target.transform.position, playerWeapon, damageData.UseMainSkill);

        if(damageData.IsMiss)
        {
            _actionView.ViewTextMiss(target.transform.position);
        }
        else
        {
            Debug.Log(damageData.FinalDamage);
            yield return target.TakeDamageAnimation(damageData.IsDead);
            _actionView.ViewDamageType(target.transform.position, damageData);
        }

        UpdateHealthView(damageData);

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

    private void UpdateHealthView(DamageData damageData)
    {
        Entity player = damageData.Attacker.TypeEntity == TypeEntity.Player ? damageData.Attacker : damageData.Target;
        Entity enemy = damageData.Attacker.TypeEntity != TypeEntity.Player ? damageData.Attacker : damageData.Target;

        _healthViewController.UpdateHealth(player.HealPoint, enemy.HealPoint);
    }
}
