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

    //������ ��� ������
    public GameObject Prefab;
    //���-�� �������� � ����
    public int CountPool;
    //���-�� �������� �� �����
    public int CountOnScene;
    public List<GameObject> ObjectPool = new List<GameObject>();
    
    [SerializeField]private Transform ZoneSpawn;

    private void Awake()
    {
        /*Singleton*/
        _instance = this;

        #region ����� �������� � ������ ����
        if (CountPool < CountOnScene)
        {
            Debug.LogError("���-�� �� ����� �� ����� ��������� ���-��������");
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
        /*��������� ������� ��� ������*/
        float posX = Random.Range(-(int)ZoneSpawn.localScale.x, (int)ZoneSpawn.localScale.z) / 2;
        float posZ = Random.Range(-(int)ZoneSpawn.localScale.x, (int)ZoneSpawn.localScale.z) / 2;

        /*����� �� �������� �������� � ���-��� ��������, ������ � ����������� �� �������� �������� ������ ������.
         ���� ������������ ������ SpawnObjFromPool, ��� ������� CountPool == CountOnScene, �� ������ �� ������ ��������� � � ���� �� �� �������*/
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
    /// ����� � ��� ��������
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
    /// ����� �������
    /// </summary>
    /// <param name="go"></param>
    void SpawnObjFromPool(float posX, float posZ)
    {
        GameObject go = new GameObject(); //������ ��� ������

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
