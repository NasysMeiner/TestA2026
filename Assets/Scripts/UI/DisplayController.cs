using UnityEngine;

public class DisplayController : MonoBehaviour
{
    [SerializeField] private GameObject _deathPlayerWindow;
    [SerializeField] private GameObject _nextLocationWindow;
    [SerializeField] private GameObject _endWindow;
    [SerializeField] private SelectorClass _shopWindow;
    [SerializeField] private SelectorClass _createSessionWindow;

    [SerializeField] private StatsView _statsEndGame;
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

    public void EnableStartWindow()
    {
        _createSessionWindow.gameObject.SetActive(true);
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
        EndLocation();
    }

    public void EnableLevelUpWindow(bool isMaxLevel)
    {
        if (!isMaxLevel)
            _shopWindow.gameObject.SetActive(true);
        else
            EndLocation();
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

    public void EnableEndWindow()
    {
        _endWindow.SetActive(true);
    }

    public void SetStats(Entity player, Entity enemy, EntityUi entituUi)
    {
        if (player != null)
            _statsEndGame.SetStats(player, player.TypeClass.ToString());
        else
            _statsViewPlayer.SetStats();

        if (player != null)
            _statsViewPlayer.SetStats(player, player.TypeClass.ToString());
        else
            _statsViewPlayer.SetStats();

        if (enemy != null)
            _statsViewEnemy.SetStats(enemy, entituUi.NameEntity);
        else
            _statsViewEnemy.SetStats();
    }
}
