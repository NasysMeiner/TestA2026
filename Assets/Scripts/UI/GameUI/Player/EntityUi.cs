using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class EntityUi : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _indent = 0.5f;

    [SerializeField] private Animator _animator;

    private bool _isFinishAttack;
    private string _nameEntity;

    public string NameEntity => _nameEntity;

    public event UnityAction AtPoint;

    public void SetNameEntity(string name)
    {
        _nameEntity = name;
    }

    public void SetPoint(Vector3 endPoint)
    {
        StopAllCoroutines();
        StartCoroutine(Move(endPoint));
    }

    public void AnimationAttack(Vector3 targetPoint)
    {
        
    }

    public IEnumerator Attack(Vector3 targetPoint, TypeWeapon playerWeapon = TypeWeapon.Empty)
    {
        yield return Move(targetPoint, true);

        _isFinishAttack = false;

        string weaponAttack = "Attack";

        if (playerWeapon != TypeWeapon.Empty)
            weaponAttack = playerWeapon.ToString() + weaponAttack;

        _animator.SetTrigger(weaponAttack);

        while (!_isFinishAttack)
            yield return null;
    }

    public IEnumerator TakeDamageAnimation(bool isDead)
    {
        if(!isDead)
            _animator.SetTrigger("Hit");
        else
            _animator.SetTrigger("Dead");

        return null;
    }

    public IEnumerator Move(Vector3 endPoint, bool isIndent = false)
    {
        if (isIndent)
        {
            if (endPoint.x > transform.position.x)
                endPoint.x -= _indent;
            else
                endPoint.x += _indent;
        }

        _animator.SetTrigger("Walk");

        while (transform.position != endPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, _speed * Time.deltaTime);

            yield return null;
        }

        _animator.SetTrigger("Idle");

        AtPoint?.Invoke();
    }

    public void OnFinishAttack()
    {
        _isFinishAttack = true;
    }
}
