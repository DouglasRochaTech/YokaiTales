using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorSomPlataformasHorizontais : MonoBehaviour
{
    public AudioSource MinhaAudioSource;
    public PlataformaQueMoveHorizontalmente[] Plataformas;
    int PlataformasAtivas;

    void Update()
    {
        PlataformasAtivas = 0;

        foreach (PlataformaQueMoveHorizontalmente Plataforma in Plataformas)
        {
            if (Plataforma.Animacao <= 1) { PlataformasAtivas++; }
        }

        if (PlataformasAtivas == 0)
        {
            if (MinhaAudioSource.enabled)
            {
                MinhaAudioSource.enabled = false;
            }
            //this.enabled = false;
        }
        else
        {
            if (!MinhaAudioSource.enabled)
            {
                MinhaAudioSource.enabled = true;
            }
        }
    }
}
