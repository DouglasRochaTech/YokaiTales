using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivadorDeObjetos : MonoBehaviour
{
    public GameObject[] ObjetosParaAtivar;
    public GameObject[] ObjetosParaDesativar;

    private void OnTriggerEnter(Collider other)
    {
        if (!ObjetosParaAtivar[0].activeSelf)
        {
            if (other.gameObject.tag == "Player")
            {
                foreach (GameObject ObjetoParaAtivar in ObjetosParaAtivar)
                {
                    ObjetoParaAtivar.SetActive(true);
                }

                foreach (GameObject ObjetoParaDesativar in ObjetosParaDesativar)
                {
                    ObjetoParaDesativar.SetActive(true);
                }
            }
        }
    }
}
