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

        //Объект для спавна
        public GameObject Prefab;
        //Кол-во объектов на сцене
        public int CountOnScene;

        /*Зона спавна*/
        [SerializeField] private Transform ZoneSpawn;

        /*Позиции для спавна*/
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
            StartGameSpawn(); //Запустить спавн объектов
        }

        [Server]
        /*Спавн объектов в самом начале игры*/
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
            //Генерация позиции для спавна
            posX = UnityEngine.Random.Range(-(int)ZoneSpawn.localScale.x, (int)ZoneSpawn.localScale.z) / 2;
            posZ = UnityEngine.Random.Range(-(int)ZoneSpawn.localScale.x, (int)ZoneSpawn.localScale.z) / 2;

            GameObject obj = Instantiate(Prefab);

            obj.transform.position = new Vector3(posX, 20, posZ);
            obj.SetActive(true);

            NetworkServer.Spawn(obj);
        }

        /*
        [Server]
        [Obsolete]
        /// <summary>
        /// Пулинг объектов коробок.
        /// </summary>
        /// <param name="obj">Объект коробка, которая отправляется в пулинг</param>
        public void Spawn(GameObject obj)
        {
            if(obj != null)
                obj.SetActive(false); //Скрыть объект на сцене

            //Генерация позиции для спавна
            posX = UnityEngine.Random.Range(-(int)ZoneSpawn.localScale.x, (int)ZoneSpawn.localScale.z) / 2;
            posZ = UnityEngine.Random.Range(-(int)ZoneSpawn.localScale.x, (int)ZoneSpawn.localScale.z) / 2;

            //Чтобы не возникло путаницы с кол-вом объектом, решено в зависимости от ситуации выбирать способ спавна.
             //Если использовать просто SpawnObjFromPool, при условии CountPool == CountOnScene, то объект не успеет исчезнуть и в пуле он не найдётся
            if (ObjectPool.Count > CountOnScene)
            {
                SpawnObjFromPool(posX, posZ);
            }
            else
            {
                SpawnObj(posX, posZ, obj);
            }
        }

        public void Spawn()
        {
            //Генерация позиции для спавна
            posX = UnityEngine.Random.Range(-(int)ZoneSpawn.localScale.x, (int)ZoneSpawn.localScale.z) / 2;
            posZ = UnityEngine.Random.Range(-(int)ZoneSpawn.localScale.x, (int)ZoneSpawn.localScale.z) / 2;

            SpawnObjFromPool(posX, posZ);
        }

        /// <summary>
        /// Спавн в пул объектов
        /// </summary>
        [Server]
        void SpawnInPool()
        {
            for (int i = 0; i < CountPool; i++)
            {
                var poolObj = Instantiate(Prefab);
                ObjectPool.Add(poolObj);
                NetworkServer.Spawn(poolObj);
                poolObj.SetActive(false);
            }
        }

        /// <summary>
        /// Спавн свободного объекта из пула
        /// </summary>
        /// <param name="go"></param>
        [Server]
        void SpawnObjFromPool(float posX, float posZ)
        {
            GameObject go = new GameObject(); //Объект для спавна

            for(int i = 0; i < ObjectPool.Count; i++)
            {
                if (!ObjectPool[i].activeSelf)
                {
                    go = ObjectPool[i];
                }
            }

            go.transform.position = new Vector3(posX, 20,posZ);
            go.SetActive(true);
        }

        //Спавн конкретного объекта
        [Server]
        void SpawnObj(float posX, float posZ, GameObject go)
        {
            go.transform.position = new Vector3(posX, 20, posZ);
            go.SetActive(true);
        }*/
    }
}
