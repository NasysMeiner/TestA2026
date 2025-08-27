using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SelectorButtonClass : MonoBehaviour
{
    [SerializeField] private TypeClass _typeClass;

    public TypeClass TypeClass => _typeClass;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void DisableButton()
    {
        _button.interactable = false;
    }

    public void EnableButton()
    {
        _button.interactable = true;
    }
}
