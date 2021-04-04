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

        public GameObject MainHUD; //�������� ���� ������ ��������
        public UnityEngine.UI.Text TextPoint;
        public UnityEngine.UI.Text StatsPlayers;

        private void Awake()
        {
            /*Singleton*/
            _instance = this;

            HideHUD(); //������ ����������� HUD
        }

        /// <summary>
        /// �������� ���� ������
        /// </summary>
        /// <param name="point">���-�� �����</param>
        [TargetRpc]
        public void TargetUpdatePoint(NetworkConnection target,int point)
        {
            TextPoint.text = "����: " + point;
        }

        [Server]
        public void UpdateListPlayers()
        {
            /*������ �� �������, ��� ����, ����� ������� ���� ������ ���*/
            ref List<PlayerController> players = ref SceneController.Instance.Players;

            /*��������� ������ ������������� � ���-�� �� ��������� �����*/
            string newListPlayers;
            newListPlayers = "";
            newListPlayers = "������: \n";
            for (int i = 0; i < players.Count; i++)
            {
                newListPlayers += $"{i + 1}. {players[i].NamePlayer} - {players[i].GetPoint} �����\n";
            }

            /*���������� �������� �������������� ������*/
            UpdateListClients(newListPlayers);
        }

        /// <summary>
        /// �������� ������ ������������� � ��������
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
