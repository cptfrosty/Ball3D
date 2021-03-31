using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

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
    //Кол-во объектов в пуле
    public int CountPool;
    //Кол-во объектов на сцене
    public int CountOnScene;
    public List<GameObject> ObjectPool = new List<GameObject>();
    
    /*Зона спавна*/
    [SerializeField]private Transform ZoneSpawn;

    /*Позиции для спавна*/
    [SyncVar] private float posX;
    [SyncVar] private float posZ;

    private void Awake()
    {
        /*Singleton*/
        _instance = this;
    }

    [ServerCallback]
    /*Спавн объектов в самом начале игры*/
    public void StartGameSpawn()
    {
        #region Спавн объектов в начале игры
        if (CountPool < CountOnScene)
        {
            Debug.LogError("Кол-во на сцене не может превышать пул-объектов");
        }
        else
        {
            SpawnInPool();
        }
        #endregion

        for (int i = 0; i < CountOnScene; i++)
        {
            Spawn(null);
        }
    }

    [ServerCallback]
    /*Спавн объектов*/
    public void Spawn(GameObject obj)
    {
        /*Генерация позиции для спавна*/
        posX = Random.Range(-(int)ZoneSpawn.localScale.x, (int)ZoneSpawn.localScale.z) / 2;
        posZ = Random.Range(-(int)ZoneSpawn.localScale.x, (int)ZoneSpawn.localScale.z) / 2;

        /*Чтобы не возникло путаницы с кол-вом объектом, решено в зависимости от ситуации выбирать способ спавна.
         Если использовать просто SpawnObjFromPool, при условии CountPool == CountOnScene, то объект не успеет исчезнуть и в пуле он не найдётся*/
        if (ObjectPool.Count > CountOnScene)
        {
            SpawnObjFromPool(posX, posZ);
        }
        else
        {
            SpawnObj(posX, posZ, obj);
        }
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

    /*Спавн конкретного объекта*/
    [Server]
    void SpawnObj(float posX, float posZ, GameObject go)
    {
        go.transform.position = new Vector3(posX, 20, posZ);
        go.SetActive(true);
    }
}
