using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    //��� ������ ��� ����������� � �������
    public string NamePlayer;
    //�������� ������������
    [SyncVar]public float Speed;             

    //���. ���������
    private Rigidbody _rb;          
    //���-�� �����
    [SyncVar]private int _countPoint = 0;    
    //������ ������������
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
            /*���������� �������� �� ������� ����� ������*/
            CameraController.Instance.SetTarget = this.transform;
            /*���������� ������� �� �����*/
            SpawnController.Instance.StartGameSpawn();
        }

        if (isClient)
        {
            /*�������� ������ � ������ �� ������*/
            SceneController.Instance.AddPlayer(this);
        }
    }

    void Update()
    {
        Move();
    }


    /*�������� �����*/
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
    /// �������� ���-�� �����
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
