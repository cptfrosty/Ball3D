using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Ball3DGame
{
    public class BlockPoint : NetworkBehaviour
    {
        /* ������ �������� �������, ������� ��������� ����� � �������� �� ��� ����. 
         * �������������� ���������� ������ ������������ ���� �������� �� ��������� 
         * PlayerController (��� ������ ���� ��� Ball) � BlockPoint*/

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Ball")
            {
                collision.gameObject.GetComponent<PlayerController>().TakePoint(collision.gameObject.GetComponent<PlayerController>(),1);
                NetworkServer.Destroy(this.gameObject);
            }
        }
    }
}
