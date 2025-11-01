using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorSomPlataformas : MonoBehaviour
{
    public AudioSource MinhaAudioSource;
    public PlataformaQueMove[] Plataformas;
    int PlataformasAtivas;

    void Update()
    {
        PlataformasAtivas = 0;

        foreach (PlataformaQueMove Plataforma in Plataformas)
        {
            if (Plataforma.enabled) { PlataformasAtivas++; }
        }

        if (PlataformasAtivas == 0)
        {
            MinhaAudioSource.enabled = false;
            this.enabled = false;
        }
    }
}
