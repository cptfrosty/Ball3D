using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Ball3DGame
{
    public class HUDController : NetworkBehaviour
    {
        #region Singleton
        public static HUDController _instance;

        public static HUDController Instance
        {
            get { return _instance; }
        }
        #endregion

        public GameObject MainHUD; //Родитель двух нижних объектов
        public UnityEngine.UI.Text TextPoint;
        public UnityEngine.UI.Text StatsPlayers;

        private void Awake()
        {
            /*Singleton*/
            _instance = this;

            HideHUD(); //Скрыть отображение HUD
        }

        /// <summary>
        /// Обновить очки игрока
        /// </summary>
        /// <param name="point">Кол-во очков</param>
        [TargetRpc]
        public void TargetUpdatePoint(NetworkConnection target,int point)
        {
            TextPoint.text = "Очки: " + point;
        }

        [Server]
        public void UpdateListPlayers()
        {
            /*Ссылка на игроков, для того, чтобы удобней было читать код*/
            ref List<PlayerController> players = ref SceneController.Instance.Players;

            /*Формирует список пользователей и кол-во их набранных очков*/
            string newListPlayers;
            newListPlayers = "";
            newListPlayers = "ИГРОКИ: \n";
            for (int i = 0; i < players.Count; i++)
            {
                newListPlayers += $"{i + 1}. {players[i].NamePlayer} - {players[i].GetPoint} очков\n";
            }

            /*Отправляет клиентам сформированный список*/
            UpdateListClients(newListPlayers);
        }

        /// <summary>
        /// Обновить список пользователей у клиентов
        /// </summary>
        [ClientRpc]
        public void UpdateListClients(string listClients)
        {
            StatsPlayers.text = listClients;
        }

        public void ShowHUD()
        {
            MainHUD.SetActive(true);
        }

        public void HideHUD()
        {
            MainHUD.SetActive(false);
        }
    }
}
