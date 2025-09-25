using UnityEngine;
using UnityEngine.Events;

public class GameLooper : MonoBehaviour
{
    private Entity _player;
    private Entity _enemy;

    private Entity _attacker = null;
    private Entity _target = null;

    private int _countMoves;

    public event UnityAction<DamageData> EndOneLoop;
    public event UnityAction<Entity> Death;

    public void Init(Entity player, Entity enemy)
    {
        _player = player;
        _enemy = enemy;

        _countMoves = 0;
        _player.ResetEntity();
    }

    internal void MakeOneLoop()
    {
        if (_player.HealPoint <= 0 || _enemy.HealPoint <= 0)
        {
            Death?.Invoke(_player.HealPoint <= 0 ? _player : _enemy);
            return;
        }

        SelectAttacker();

        DamageData damageData = new() { CountMoves = _countMoves, Target = _target };

        damageData.IsMiss = CalculateMiss(_attacker, _target);

        _attacker.CalculateDamage(damageData, _target);
        _target.TakeDamage(damageData);

        EndOneLoop?.Invoke(damageData);

        _countMoves++;
    }

    private bool CalculateMiss(Entity attacker, Entity target)
    {
        int randomNumber = Random.Range(1, attacker.Dexterity + target.Dexterity + 1);

        if (randomNumber <= target.Dexterity)
            return true;
        else
            return false;
    }

    private void SelectAttacker()
    {
        if (_attacker != null)
        {
            (_attacker, _target) = (_target, _attacker);
            return;
        }

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
