using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Ball3DGame
{
    public class BlockPoint : NetworkBehaviour
    {
        /* Скрипт игрового объекта, который поднимает игрок и получает за это очки. 
         * Взаимодействие происходит засчет столкновение двух объектов со скриптами 
         * PlayerController (ещё должен быть тэг Ball) и BlockPoint*/

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
