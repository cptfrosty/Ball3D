using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

namespace Ball3DGame
{
    public class SpawnController : NetworkBehaviour
    {
        #region Singleton
        public static SpawnController _instance;

        public static SpawnController Instance
        {
            get { return _instance; }
        }
        #endregion

        //������ ��� ������
        public GameObject Prefab;
        //���-�� �������� �� �����
        public int CountOnScene;

        /*���� ������*/
        [SerializeField] private Transform ZoneSpawn;

        /*������� ��� ������*/
        [SyncVar] private float posX;
        [SyncVar] private float posZ;

        private void Awake()
        {
            /*Singleton*/
            _instance = this;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            StartGameSpawn(); //��������� ����� ��������
        }

        [Server]
        /*����� �������� � ����� ������ ����*/
        public void StartGameSpawn()
        {
            for (int i = 0; i < CountOnScene; i++)
            {
                Spawn();
            }
        }

        [Server]
        public void Spawn()
        {
            //��������� ������� ��� ������
            posX = UnityEngine.Random.Range(-(int)ZoneSpawn.localScale.x, (int)ZoneSpawn.localScale.z) / 2;
            posZ = UnityEngine.Random.Range(-(int)ZoneSpawn.localScale.x, (int)ZoneSpawn.localScale.z) / 2;

            GameObject obj = Instantiate(Prefab);

            obj.transform.position = new Vector3(posX, 20, posZ);
            obj.SetActive(true);

            NetworkServer.Spawn(obj);
        }
    }
}
