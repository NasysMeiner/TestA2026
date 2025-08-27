using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    private PlayerData _playerData;

    public void Init(PlayerData playerData)
    {
        _playerData = playerData;
    }

    public PlayerUi CreatePlayer(TypeClass typeClass)
    {
        //Random;
        Player player = _playerData.Players[0];
        return Instantiate(player.PrefabPlayer);
    }
}
