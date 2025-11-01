using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixaDeDialogo : MonoBehaviour
{
    public CaixaDeDialogo ProximoDialogo;
    public Jogador ScriptDoJogador;
    float Timer;

    [Header("Audio")]
    public AudioSource UIAudioSource;
    public AudioClip DialogoAudio;
    public AudioClip Confirmar;
    public AudioClip Selecionar;

    private void OnEnable()
    {
        ScriptDoJogador.enabled = false;
        Time.timeScale = 0;

        UIAudioSource.PlayOneShot(DialogoAudio);
    }

    void Update()
    {
        /*Timer += Time.deltaTime;

        if (Timer > 5)
        {
            this.gameObject.SetActive(false);
            transform.parent.gameObject.SetActive(false);
        }*/

        if (Input.GetKeyDown("joystick button 0")) //APERTAR BOTÃO "A"
        {
            this.gameObject.SetActive(false);
            transform.parent.gameObject.SetActive(false);
            UIAudioSource.PlayOneShot(Selecionar);

            if (ProximoDialogo != null)
            {
                ProximoDialogo.gameObject.SetActive(true);
                ProximoDialogo.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                ScriptDoJogador.enabled = true;
                Time.timeScale = 1;
            }
        }
    }
}
