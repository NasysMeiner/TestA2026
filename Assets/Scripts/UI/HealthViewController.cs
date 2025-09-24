using UnityEngine;

public class HealthViewController : MonoBehaviour
{
    [SerializeField] private HealthView _playerHealth;
    [SerializeField] private HealthView _enemyHealth;

    public void StartLocation(Session currentSession)
    {
        if(currentSession.Player.Entity != null)
        {
            _playerHealth.SetName(currentSession.Player.EntityUi.NameEntity);
            _playerHealth.SetMaxHealth(currentSession.Player.Entity.MaxHealPoint);
            _playerHealth.SetHealth(currentSession.Player.Entity.HealPoint);
            _playerHealth.gameObject.SetActive(true);
        }
        else
        {
            _playerHealth.gameObject.SetActive(false);
        }

        if(currentSession.Enemy.Entity != null)
        {
            _enemyHealth.SetName(currentSession.Enemy.EntityUi.NameEntity);
            _enemyHealth.SetMaxHealth(currentSession.Enemy.Entity.MaxHealPoint);
            _enemyHealth.SetHealth(currentSession.Enemy.Entity.HealPoint);
            _enemyHealth.gameObject.SetActive(true);
        }
        else
        {
            _enemyHealth.gameObject.SetActive(false);
        }
    }

    public void UpdateHealth(float currentHealthPlayer, float currentHealthEnemy)
    {
        _playerHealth.SetHealth(currentHealthPlayer);
        _enemyHealth.SetHealth(currentHealthEnemy);
    }
}
