using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Ball3DGame
{
    public class BlockPoint : NetworkBehaviour
    {
        public ParticleSystem particleTake;
        public AudioSource audioTake;

        /* ������ �������� �������, ������� ��������� ����� � �������� �� ��� ����. 
         * �������������� ���������� ������ ������������ ���� �������� �� ��������� 
         * PlayerController (��� ������ ���� ��� Ball) � BlockPoint*/

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Ball")
            {
                collision.gameObject.GetComponent<PlayerController>().TakePoint(collision.gameObject.GetComponent<PlayerController>(),1);
                
                this.GetComponent<BoxCollider>().enabled = false;
                this.GetComponent<MeshRenderer>().enabled = false;

                StartCoroutine(DestroyCube());
            }
        }

        /// <summary>
        /// �������� ������� ��������� � ����������� �������
        /// </summary>
        /// <returns></returns>
        public IEnumerator DestroyCube()
        {
            particleTake.Play();
            audioTake.Play();
            yield return new WaitForSeconds(1f);
            NetworkServer.Destroy(this.gameObject);
        }
    }
}
