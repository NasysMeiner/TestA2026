using UnityEngine;

public class DisplayController : MonoBehaviour
{
    [SerializeField] private GameObject _deathPlayerWindow;
    [SerializeField] private SelectorClass _createSessionWindow;

    private GameManager _gameManager;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void CreateSession(TypeClass typeClass)
    {
        _gameManager.GenerateSession(typeClass);
    }

    public void ActivateDeathPlayerWindow()
    {
        _deathPlayerWindow.SetActive(true);
    }

    public void ActivateCreateSessionWindow()
    {
        _deathPlayerWindow.SetActive(false);
        _createSessionWindow.gameObject.SetActive(true);
        _createSessionWindow.ActiveWindow();
    }
}
