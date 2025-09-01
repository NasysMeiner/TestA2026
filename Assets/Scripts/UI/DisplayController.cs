using UnityEngine;

public class DisplayController : MonoBehaviour
{
    [SerializeField] private GameObject _deathPlayerWindow;
    [SerializeField] private GameObject _nextLocationWindow;
    [SerializeField] private SelectorClass _shopWindow;
    [SerializeField] private SelectorClass _createSessionWindow;

    [SerializeField] private StatsView _statsViewPlayer;
    [SerializeField] private StatsView _statsViewEnemy;

    private GameManager _gameManager;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void EndLocation()
    {
        _gameManager.EndLocation();
    }

    public void CreateSession(TypeClass typeClass)
    {
        _gameManager.GenerateSession(typeClass);
    }

    public void CreateNextLevel()
    {
        _gameManager.CreateLevel();
    }

    public void LevelUpPlayer(TypeClass typeClass)
    {
        _gameManager.LevelUpPlayer(typeClass);
        CreateNextLevel();
    }

    public void EnableLevelUpWindow()
    {
        _shopWindow.gameObject.SetActive(true);
    }

    public void ActivateDeathPlayerWindow()
    {
        _deathPlayerWindow.SetActive(true);
    }

    public void ActivateNextLocationWindow()
    {
        _nextLocationWindow.SetActive(true);
    }

    public void ActivateCreateSessionWindow()
    {
        _deathPlayerWindow.SetActive(false);
        _createSessionWindow.gameObject.SetActive(true);
    }

    public void SetStats(Entity player, Entity enemy)
    {
        if (player != null)
            _statsViewPlayer.SetStats(player);
        else
            _statsViewPlayer.SetStats();

        if (enemy != null)
            _statsViewEnemy.SetStats(enemy);
        else
            _statsViewEnemy.SetStats();
    }
}
