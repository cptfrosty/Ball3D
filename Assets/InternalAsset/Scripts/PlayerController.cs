using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Ball3DGame
{
    public class PlayerController : NetworkBehaviour
    {
        #region ����������

        //��� ������ ��� ����������� � �������
        public string NamePlayer;
        //�������� ������������
        [SyncVar] public float Speed;

        //���. ���������
        private Rigidbody _rb;
        //���-�� �����
        private int _countPoint = 0;
        //������ ������������
        private Vector3 movementVector = new Vector3();
        #endregion

        /*public override void OnStopClient()
        {
            SceneController.Instance.RemovePlayer(this);
            base.OnStopClient();
        }*/

        /*����������� ��� ����������� �������*/
        public override void OnStartClient()
        {
            base.OnStartClient();

            /*�������� ������ � ������ �� ������*/
            if (isLocalPlayer)
            {
                CmdAddPlayerList();
                _rb = GetComponent<Rigidbody>();
                /*���������� �������� �� ������� ����� ������*/
                CameraController.Instance.SetTarget = this.transform;

            }
        }

        /// <summary>
        /// �������� ������ � ����
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
        /// �������� ����� ������ � ���������� ����� ������
        /// </summary>
        /// <param name="value"></param>
        [Command]
        public void TakePoint(PlayerController player, int value)
        {
            SpawnController.Instance.Spawn();

            player._countPoint += value;

            /*���������� �����*/
            HUDController.Instance.TargetUpdatePoint(player.connectionToClient, _countPoint);
            HUDController.Instance.UpdateListPlayers();
        }

        /*public void TakePoint()
        {

        }*/

        /// <summary>
        /// �������� ���-�� �����
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
