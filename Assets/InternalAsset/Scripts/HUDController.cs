using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HUDController : NetworkBehaviour
{
    #region Singleton
    public static HUDController _instance;

    public static HUDController Instance
    {
        get { return _instance; }
    }
    #endregion

    public UnityEngine.UI.Text TextPoint;
    public UnityEngine.UI.Text StatsPlayers;

    private void Awake()
    {
        /*Singleton*/
        _instance = this;
    }

    /// <summary>
    /// Обновить очки игрока
    /// </summary>
    /// <param name="point">Кол-во очков</param>
    [TargetRpc]
    public void UpdatePoint(NetworkConnection target, int point)
    {
        if (isLocalPlayer) return;
        TextPoint.text = "Очки: " + point;
    }

    public void UpdateListPlayer()
    {
        UpdateListPlayers(SceneController.Instance.Players);
    }

    public void UpdateListPlayers(List<PlayerController> players)
    {
        StatsPlayers.text = "";
        StatsPlayers.text = "ИГРОКИ: \n";
        for(int i = 0; i < players.Count; i++)
        {
            StatsPlayers.text += $"{i + 1}. {players[i].NamePlayer} - {players[i].GetPoint} очков\n";
        }
    }
}
