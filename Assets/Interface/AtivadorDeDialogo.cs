using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivadorDeDialogo : MonoBehaviour
{
    public GameObject DialogoParaAtivar;
    public float TempoDeDelay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (TempoDeDelay == 0)
            {
                AtivarDialogo();
            }
            else
            {
                this.enabled = true;
            }
        }
    }

    void AtivarDialogo()
    {
        DialogoParaAtivar.gameObject.SetActive(true);
        DialogoParaAtivar.transform.parent.gameObject.SetActive(true);

        this.gameObject.SetActive(false);
    }

    void Start()
    {
        this.enabled = false;
    }

    private void Update()
    {
        TempoDeDelay -= Time.deltaTime;

        if (TempoDeDelay <= 0)
        {
            AtivarDialogo();
        }
    }
}
