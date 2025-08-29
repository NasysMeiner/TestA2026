using UnityEngine;
using UnityEngine.Events;

public class GameLooper : MonoBehaviour
{
    private Entity _player;
    private Entity _enemy;

    private Entity _attacker = null;
    private Entity _target = null;

    public event UnityAction<DamageData> EndOneLoop;
    public event UnityAction<Entity> Death;

    public void Init(Entity player, Entity enemy)
    {
        _player = player;
        _enemy = enemy;

        _player.RegenerateHealthPoints();
    }

    internal void MakeOneLoop()
    {
        if (_player.HealPoint <= 0 || _enemy.HealPoint <= 0)
        {
            Death?.Invoke(_player.HealPoint <= 0 ? _player : _enemy);
            Debug.Log("Death");
            return;
        }

        if (_attacker == null)
            SelectAttacker();
        else
            (_attacker, _target) = (_target, _attacker);

        DamageData damageData = new();

        _attacker.CalculateDamage(damageData, _target);
        _target.TakeDamage(damageData);

        Debug.Log("endloop...");
        EndOneLoop?.Invoke(damageData);
    }

    private void SelectAttacker()
    {
        if (_player.Dexterity >= _enemy.Dexterity)
        {
            _attacker = _player;
            _target = _enemy;
        }
        else
        {
            _attacker = _enemy;
            _target = _player;
        }
    }
}
