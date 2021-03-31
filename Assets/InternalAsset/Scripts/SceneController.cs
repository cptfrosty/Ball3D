using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SceneController : MonoBehaviour
{
    #region Singleton
    public static SceneController _instance;

    public static SceneController Instance
    {
        get { return _instance; }
    }
    #endregion
    private void Awake()
    {
        /*Singleton*/
        _instance = this;
    }
   
    public List<PlayerController> Players = new List<PlayerController>();

    [Client]
    /// <summary>
    /// �������� ������
    /// </summary>
    /// <param name="player">������ �� ������</param>
    public void AddPlayer(PlayerController player)
    {
        Players.Add(player);
        HUDController.Instance.UpdateListPlayer(); //�������� ���� �������
    }

    [Server]
    /// <summary>
    /// ������� ������
    /// </summary>
    /// <param name="player">������ �� ������</param>
    public void RemovePlayer(PlayerController player)
    {
        for(int i = 0; i < Players.Count; i++)
        {
            if(Players[i].netId == player.netId)
            {
                Players.RemoveAt(i);
            }
        }
    }
}
