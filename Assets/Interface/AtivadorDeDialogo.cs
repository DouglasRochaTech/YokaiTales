using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivadorDeDialogo : MonoBehaviour
{
    public GameObject DialogoParaAtivar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DialogoParaAtivar.gameObject.SetActive(true);
            DialogoParaAtivar.transform.parent.gameObject.SetActive(true);

            this.gameObject.SetActive(false);
        }
    }
}
