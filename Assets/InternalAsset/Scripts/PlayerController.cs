using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Ball3DGame
{
    public class PlayerController : NetworkBehaviour
    {
        #region Переменные

        //Имя игрока для отображения в таблице
        public string NamePlayer;
        //Скорость передвижения
        [SyncVar] public float Speed;

        //Физ. компонент
        private Rigidbody _rb;
        //Кол-во очков
        private int _countPoint = 0;
        //Вектор передвижения
        private Vector3 movementVector = new Vector3();
        #endregion

        /*public override void OnStopClient()
        {
            SceneController.Instance.RemovePlayer(this);
            base.OnStopClient();
        }*/

        /*Срабатывает при подключении клиента*/
        public override void OnStartClient()
        {
            base.OnStartClient();

            /*Добавить игрока в список на сервер*/
            if (isLocalPlayer)
            {
                CmdAddPlayerList();
                _rb = GetComponent<Rigidbody>();
                /*Установить слежение за игроком через камеру*/
                CameraController.Instance.SetTarget = this.transform;

            }
        }

        /// <summary>
        /// Добавить игрока в лист
        /// </summary>
        [Command (requiresAuthority = false)]
        public void CmdAddPlayerList()
        {
            SceneController.Instance.AddPlayer(this);
        }

        void Update()
        {
            Move();
        }


        /// <summary>
        /// Добавить очков игроку и заспавнить новый объект
        /// </summary>
        /// <param name="value"></param>
        [Command]
        public void TakePoint(PlayerController player, int value)
        {
            SpawnController.Instance.Spawn();

            player._countPoint += value;

            /*Обновление очков*/
            HUDController.Instance.TargetUpdatePoint(player.connectionToClient, _countPoint);
            HUDController.Instance.UpdateListPlayers();
        }

        /*public void TakePoint()
        {

        }*/

        /// <summary>
        /// Получить кол-во очков
        /// </summary>
        public int GetPoint
        {
            get
            {
                return _countPoint;
            }
        }

        void Move()
        {
            if (!isLocalPlayer) return;

            movementVector.x = Input.GetAxis("Horizontal");
            movementVector.z = Input.GetAxis("Vertical");

            var camera = CameraController.Instance.transform;
            Vector3 moveXZ = Vector3.zero;

            if (movementVector.x > 0 || movementVector.x < 0)
                moveXZ = new Vector3(camera.right.x * movementVector.x, camera.right.y, camera.right.z * movementVector.x);
            if (movementVector.z > 0 || movementVector.z < 0)
                moveXZ = new Vector3(camera.forward.x * movementVector.z, camera.forward.y, camera.forward.z * movementVector.z);

            _rb.AddForce(moveXZ * Speed);
        }
    }
}
