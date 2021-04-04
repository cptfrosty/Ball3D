using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace Ball3DGame {
    public class LogInController : MonoBehaviour
    {
        #region Singleton
        public static LogInController _instance;

        public static LogInController Instance
        {
            get { return _instance; }
        }
        #endregion

        [Header ("Parent")]
        public GameObject UI_MAIN;
        [Header ("UI")]
        [Tooltip("Поле с вводом имени игрока")]
        public InputField PlayerNameField;
        [Tooltip("Кнопка создания хоста")]
        public Button StartHost;
        [Tooltip("Кнопка подключения к серверу")]
        public Button Connect;
        [Tooltip("Поле ссылки адреса сервера")]
        public InputField ServerAdress;
        [Tooltip("Кнопка создания сервера")]
        public Button CreateServer;
        [Tooltip("Кнопка выхода из игры")]
        public Button ExitGame;

        public string namePlayer;

        private void Awake()
        {
            //Singleton
            _instance = this;
        }

        private void Start()
        {
            PlayerNameField.onEndEdit.AddListener(SelectNamePlayer);

            StartHost.onClick.AddListener(ConnectedToServerHost);
            Connect.onClick.AddListener(ConnectToServer);
            CreateServer.onClick.AddListener(CreateSelfServer);
            ExitGame.onClick.AddListener(ExitTheGame);
        }

        private void SelectNamePlayer(string arg0)
        {
            namePlayer = arg0;
        }

        public void ConnectToServer()
        {
            if (string.IsNullOrEmpty(ServerAdress.text)) return;

            NetworkManager.singleton.networkAddress = ServerAdress.text;
            NetworkManager.singleton.StartClient();
            
        }

        public void ConnectedToServerHost()
        {
            NetworkManager.singleton.StartHost();
        }

        public void CreateSelfServer()
        {
            NetworkManager.singleton.StartServer();
        }

        public void HidePanel()
        {
            UI_MAIN.SetActive(false);
        }

        public void ShowPanel()
        {
            UI_MAIN.SetActive(true);
        }

        /// <summary>
        /// Выйти их игры
        /// </summary>
        public void ExitTheGame()
        {
            Application.Quit();
        }
    }
}
