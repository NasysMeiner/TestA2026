using UnityEngine;

public class DisplayController : MonoBehaviour
{
    [SerializeField] private GameObject _deathPlayerWindow;
    [SerializeField] private GameObject _nextLocationWindow;
    [SerializeField] private SelectorClass _createSessionWindow;

    private GameManager _gameManager;

    private bool _isCreated = false;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void CreateSession(TypeClass typeClass)
    {
        _gameManager.GenerateSession(typeClass);
    }

    public void CreateNextLevel()
    {
        if(!_isCreated)
        {
            _isCreated = true;
            _gameManager.CreateLevel();
            _nextLocationWindow.SetActive(false);
        }
    }

    public void ActivateDeathPlayerWindow()
    {
        _deathPlayerWindow.SetActive(true);
    }

    public void ActivateNextLocationWindow()
    {
        _isCreated = false;
        _nextLocationWindow.SetActive(true);
    }

    public void ActivateCreateSessionWindow()
    {
        _deathPlayerWindow.SetActive(false);
        _createSessionWindow.gameObject.SetActive(true);
        _createSessionWindow.ActiveWindow();
    }
}
