using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
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
    
    [SerializeField]private Transform ZoneSpawn;

    private void Awake()
    {
        /*Singleton*/
        _instance = this;

        #region Спавн объектов в начале игры
        if (CountPool < CountOnScene)
        {
            Debug.LogError("Кол-во на сцене не может превышать пул-объектов");
        }
        else
        {
            SpawnInPool();

            for (int i = 0; i < CountOnScene; i++)
            {
                Spawn(null);
            }
        }
        #endregion
    }

    public void Spawn(GameObject obj)
    {
        /*Генерация позиции для спавна*/
        float posX = Random.Range(-(int)ZoneSpawn.localScale.x, (int)ZoneSpawn.localScale.z) / 2;
        float posZ = Random.Range(-(int)ZoneSpawn.localScale.x, (int)ZoneSpawn.localScale.z) / 2;

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
    void SpawnInPool()
    {
        for (int i = 0; i < CountPool; i++)
        {
            var poolObj = Instantiate(Prefab);
            ObjectPool.Add(poolObj);
            poolObj.SetActive(false);
        }
    }

    /// <summary>
    /// Спавн объекта
    /// </summary>
    /// <param name="go"></param>
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

    void SpawnObj(float posX, float posZ, GameObject go)
    {
        go.transform.position = new Vector3(posX, 20, posZ);
        go.SetActive(true);
    }
}
