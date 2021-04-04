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
        /// Добавить игрока
        /// </summary>
        /// <param name="player">Ссылка на игрока</param>
        public void AddPlayer(PlayerController player)
        {
            Players.Add(player);
            HUDController.Instance.UpdateListPlayers(); //Обновить лист игроков
        }

        [Server]
        /// <summary>
        /// Удалить игрока
        /// </summary>
        /// <param name="player">Ссылка на игрока</param>
        public void CmdRemovePlayer(uint netId)
        {
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].netId == netId)
                {
                    Players.RemoveAt(i);
                }
            }

            HUDController.Instance.UpdateListPlayers(); //Обновить лист игроков
        }
    }
}
