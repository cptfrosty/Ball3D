using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    //Имя игрока для отображения в таблице
    public string NamePlayer;
    //Скорость передвижения
    [SyncVar]public float Speed;             

    //Физ. компонент
    private Rigidbody _rb;          
    //Кол-во очков
    [SyncVar]private int _countPoint = 0;    
    //Вектор передвижения
    private Vector3 movementVector = new Vector3();


    public override void OnStopClient()
    {
        SceneController.Instance.RemovePlayer(this);
        base.OnStopClient();
    }

    void Start()
    {
        if (isLocalPlayer)
        {
            _rb = GetComponent<Rigidbody>();
            /*Установить слежение за игроком через камеру*/
            CameraController.Instance.SetTarget = this.transform;
            /*Заспавнить объекты на карте*/
            SpawnController.Instance.StartGameSpawn();
        }

        if (isClient)
        {
            /*Добавить игрока в список на сервер*/
            SceneController.Instance.AddPlayer(this);
        }
    }

    void Update()
    {
        Move();
    }


    /*Добавить очков*/
    public int AddPoint
    {
        set
        {
            _countPoint += value;
            HUDController.Instance.UpdatePoint(this.connectionToClient,_countPoint);
            HUDController.Instance.UpdateListPlayer();
        }
    }

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

    [Client]
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
