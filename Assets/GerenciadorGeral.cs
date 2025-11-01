using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GerenciadorGeral : MonoBehaviour
{
    public Jogador ScriptDoJogador;
    public Slider SliderVida;
    public GameObject TextoFimDeJogo;

    [Header("Debug")]
    public bool PAUSADO;
    public int PausaSelecao;
    public float HitStopTimer = -1;
    bool DPadCima;
    bool DPadBaixo;
    bool DPadEsquerda;
    bool DPadDireita;
    bool DPadPressionado;

    [Header("Pausa")]
    public GameObject MenuPausa;
    public GameObject Selecao;
    public GameObject Retomar;
    public GameObject Opcoes;
    public GameObject SalvarSair;

    [Header("Audio")]
    public AudioSource UIAudioSource;
    public AudioClip Confirmar;
    public AudioClip Selecionar;

    void Update()
    {
        InputsDPAD();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown("joystick button 6"))
        {
            SceneManager.LoadScene(0);
        }

        if (ScriptDoJogador)
        {
            SliderVida.value = ScriptDoJogador.Vida;
        }
        else
        {
            if (!TextoFimDeJogo.activeSelf)
            {
                TextoFimDeJogo.SetActive(true);
            }

            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(0);
            }
        }

        //PAUSA___________________________________________________
        if (Input.GetKeyDown("joystick button 7"))
        {
            PAUSADO = !PAUSADO;

            if (PAUSADO)
            {
                Time.timeScale = 0;
                ScriptDoJogador.enabled = false;
                MenuPausa.SetActive(true);
                SelecionarOpcaoPausa(); //Pra atualizar a posição das florezinhas
            }
            else
            {
                Time.timeScale = 1;
                ScriptDoJogador.enabled = true;
                MenuPausa.SetActive(false);            
            }
        }

        if (PAUSADO)
        {
            if (Input.GetKeyDown("joystick button 0"))
            {
                ConfirmarOpcaoPausa();
            }

            if (!DPadPressionado)
            {
                if (DPadCima)  
                { 
                    PausaSelecao++;
                    SelecionarOpcaoPausa(); 
                    DPadPressionado = true;
                    UIAudioSource.PlayOneShot(Selecionar);
                    
                }
                if (DPadBaixo) 
                { 
                    PausaSelecao--;
                    SelecionarOpcaoPausa(); 
                    DPadPressionado = true;
                    UIAudioSource.PlayOneShot(Selecionar);
                }
            }
        }

        /*if (HitStopTimer != -1)
        {
            Time.timeScale = 0;
            HitStopTimer += Time.unscaledDeltaTime;

            if (HitStopTimer > 0.2f)
            {
                Time.timeScale = 1;
                HitStopTimer = -1;
            }
        }*/
    }

    void InputsDPAD()
    {
        DPadEsquerda = false;
        DPadDireita  = false;
        DPadBaixo    = false;
        DPadCima     = false;

        if ((Input.GetAxis("DPadHorizontal")) < 0) { DPadEsquerda = true; }
        if ((Input.GetAxis("DPadHorizontal")) > 0) { DPadDireita = true; }
        if ((Input.GetAxis("DPadVertical")) > 0)   { DPadBaixo = true; }
        if ((Input.GetAxis("DPadVertical")) < 0)   { DPadCima = true; }

        if (!DPadEsquerda && !DPadDireita && !DPadBaixo && !DPadCima)
        {
            DPadPressionado = false;
        }
    }

    void SelecionarOpcaoPausa()
    {
        if (PausaSelecao < 0) { PausaSelecao = 2; }
        if (PausaSelecao > 2) { PausaSelecao = 0; }

        switch (PausaSelecao)
        {
            case 0: //RETORNAR
                Selecao.transform.position = Retomar.transform.position;
                break;

            case 1: //OPÇÕES
                Selecao.transform.position = Opcoes.transform.position;
                break;

            case 2: //SALVAR E SAIR
                Selecao.transform.position = SalvarSair.transform.position;
                break;
        }
    }

    void ConfirmarOpcaoPausa()
    {
        switch (PausaSelecao)
        {
            case 0: //RETORNAR
                Time.timeScale = 1;
                ScriptDoJogador.enabled = true;
                MenuPausa.SetActive(false);
                PAUSADO = false;
                break;

            case 1: //OPÇÕES

                break;

            case 2: //SALVAR E SAIR
                Application.Quit();
                break;
        }

        UIAudioSource.PlayOneShot(Confirmar);
    }
}
