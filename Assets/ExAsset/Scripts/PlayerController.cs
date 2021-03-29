using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Скорость передвижения
    public float Speed;             

    //Физ. компонент
    private Rigidbody _rb;          
    //Кол-во очков
    private int _countPoint = 0;    
    //Вектор передвижения
    private Vector3 movementVector = new Vector3(); 

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        /*Установить слежение за игроком через камеру*/
        CameraController.Instance.SetTarget = this.transform;
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
            HUDController.Instance.UpdatePoint(_countPoint);
        }
    }

    void Move()
    {
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
