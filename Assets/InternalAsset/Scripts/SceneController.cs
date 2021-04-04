using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Ball3DGame
{

    public class SceneController : NetworkBehaviour
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

        [Server]
        /// <summary>
        /// �������� ������
        /// </summary>
        /// <param name="player">������ �� ������</param>
        public void AddPlayer(PlayerController player)
        {
            Players.Add(player);
            HUDController.Instance.UpdateListPlayers(); //�������� ���� �������
        }

        [Server]
        /// <summary>
        /// ������� ������
        /// </summary>
        /// <param name="player">������ �� ������</param>
        public void RemovePlayer(PlayerController player)
        {
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].netId == player.netId)
                {
                    Players.RemoveAt(i);
                }
            }
        }
    }
}