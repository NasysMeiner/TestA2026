using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class PlayerUi : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Animator _animator;

    public event UnityAction AtPoint;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetPoint(Vector3 endPoint)
    {
        StopAllCoroutines();
        StartCoroutine(Move(endPoint));
    }

    private IEnumerator Move(Vector3 endPoint)
    {
        _animator.SetTrigger("WalkTrigger");

        while (transform.position != endPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, _speed * Time.deltaTime);

            yield return null;
        }

        _animator.SetTrigger("IdleTrigger");

        AtPoint?.Invoke();
    }
}
