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
    public float VolumeAudio = 1;

    private void OnEnable()
    {
        ScriptDoJogador.enabled = false;
        ScriptDoJogador.AudioSourceCorridinha.volume = 0;
        Time.timeScale = 0;

        UIAudioSource.PlayOneShot(DialogoAudio, VolumeAudio);
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
                ScriptDoJogador.AudioSourceCorridinha.volume = 1;
                Time.timeScale = 1;
            }
        }
    }
}
