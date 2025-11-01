using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public Jogador JogadorScript;
    public bool NoChao;
    public bool NaGrama;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Chao" || other.gameObject.tag == "Grama")
        {
            NoChao = true;
            JogadorScript.FoxAnimator.SetBool("Pulando", false);
        }

        if (other.gameObject.tag == "Grama")
        {
            NaGrama = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Chao" || other.gameObject.tag == "Grama")
        {
            NoChao = false;
            NaGrama = false;
        }
    }
}
