using UnityEngine;

public class AnimationSprite : MonoBehaviour
{
    [SerializeField] private EntityUi _entityUi;

    public void OnFinishAttack()
    {
        _entityUi.OnFinishAttack();
    }

    public void OnActiveFireBreath()
    {
        _entityUi.OnActiveFireBreath();
    }
}
