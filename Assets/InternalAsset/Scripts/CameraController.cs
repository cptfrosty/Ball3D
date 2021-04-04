using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ball3DGame
{

    public class CameraController : MonoBehaviour
    {
        #region Singletone
        public static CameraController _instance;

        public static CameraController Instance
        {
            get { return _instance; }
        }

        private void Awake()
        {
            _instance = this;
        }
        #endregion

        /*������ �� ������� ���������� �������*/
        [SerializeField] private Transform _target;
        public Transform SetTarget
        {
            set
            {
                _target = value;
            }
        }


        public Vector3 offset;
        public float sensitivity = 3; // ���������������� �����
        public float limit = 80; // ����������� �������� �� Y
        public float zoom = 0.25f; // ���������������� ��� ����������, ��������� �����
        public float zoomMax = 10; // ����. ����������
        public float zoomMin = 3; // ���. ����������
        private float X, Y;

        private void Start()
        {
            limit = Mathf.Abs(limit);
            if (limit > 90) limit = 90;
            offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax) / 2);
            //transform.position = _target.position + offset;
        }

        private void Update()
        {
            if (_target == null) return;

            if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoom;
            else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom;
            offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

            X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
            Y += Input.GetAxis("Mouse Y") * sensitivity;
            Y = Mathf.Clamp(Y, -limit, limit);
            transform.localEulerAngles = new Vector3(-Y, X, 0);
            transform.position = transform.localRotation * offset + _target.position;
        }
    }
}
