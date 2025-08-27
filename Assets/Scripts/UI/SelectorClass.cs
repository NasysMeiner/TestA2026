using System.Collections.Generic;
using UnityEngine;

public class SelectorClass : MonoBehaviour
{
    private List<SelectorButtonClass> _buttons = new();

    private SelectorButtonClass _currentButtonClass = null;

    public void SetTypeClass(SelectorButtonClass buttonClass)
    {
        if (_currentButtonClass != null)
            _currentButtonClass.EnableButton();

        _currentButtonClass = buttonClass;
        _currentButtonClass.DisableButton();
    }

    public void GenerateGame()
    {
        //if(_currentButtonClass != null)
        //GameManager.Generate(_currentButtonClass.TypeClass);
    }
}
