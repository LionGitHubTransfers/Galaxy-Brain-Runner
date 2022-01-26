using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateFinal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerController>().isDragging = true;
            this.GetComponent<Animator>().enabled = true;
        }
    }
}
