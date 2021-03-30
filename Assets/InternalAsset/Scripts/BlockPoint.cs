using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPoint : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().AddPoint = 1;
            SpawnController.Instance.Spawn(this.gameObject);

            this.gameObject.SetActive(false);
        }
    }
}
