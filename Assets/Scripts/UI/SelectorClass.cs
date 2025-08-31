using System.Collections.Generic;
using UnityEngine;

public class SelectorClass : MonoBehaviour
{
    [SerializeField] private DisplayController _displayController;
    [SerializeField] private List<SelectorButtonClass> _buttons = new();

    private SelectorButtonClass _currentButtonClass = null;

    private bool _isCreate = false;

    public void SetTypeClass(SelectorButtonClass buttonClass)
    {
        if (_currentButtonClass != null)
            _currentButtonClass.EnableButton();

        _currentButtonClass = buttonClass;
        _currentButtonClass.DisableButton();
    }

    public void ActiveWindow()
    {
        _isCreate = false;
    }

    public void GenerateGame()
    {
        if (!_isCreate && _currentButtonClass != null)
        {
            _isCreate = true;
            _displayController.CreateSession(_currentButtonClass.TypeClass);
            gameObject.SetActive(false);
            _currentButtonClass = null;
        }
    }

    public void LevelUp(SelectorButtonClass buttonClass)
    {
        if(!_isCreate && _currentButtonClass != null)
        {
            _isCreate = true;
            //NextLevel
            gameObject.SetActive(false);
        }
    }
}
