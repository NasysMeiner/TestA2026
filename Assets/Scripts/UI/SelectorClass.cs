using UnityEngine;

public class SelectorClass : MonoBehaviour
{
    [SerializeField] private DisplayController _displayController;

    private bool _isCreate = false;

    private void OnEnable()
    {
        _isCreate = false;
    }

    public void CreateNextLevel()
    {
        if(!_isCreate)
        {
            _isCreate = true;
            _displayController.CreateNextLevel();
            gameObject.SetActive(false);
        }
    }

    public void GenerateGame(SelectorButtonClass selectorButton)
    {
        if (!_isCreate)
        {
            _isCreate = true;
            _displayController.CreateSession(selectorButton.TypeClass);
            gameObject.SetActive(false);
        }
    }

    public void LevelUp(SelectorButtonClass selectorButton)
    {
        if (!_isCreate)
        {
            _isCreate = true;
            _displayController.LevelUpPlayer(selectorButton.TypeClass);
            gameObject.SetActive(false);
        }
    }
}
