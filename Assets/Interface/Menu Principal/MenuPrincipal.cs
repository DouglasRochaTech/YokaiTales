using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject Selecao;
    public GameObject NovoJogo;
    public GameObject Opcoes;
    public GameObject SalvarSair;

    [Header("Debug")]
    public int MenuSelecao;
    bool DPadCima;
    bool DPadBaixo;
    bool DPadEsquerda;
    bool DPadDireita;
    bool DPadPressionado;

    [Header("Audio")]
    public AudioSource UIAudioSource;
    public AudioClip Confirmar;
    public AudioClip Selecionar;

    void Update()
    {
        InputsDPAD();

        if (!DPadPressionado)
        {
            if (DPadCima)
            {
                MenuSelecao++;
                SelecionarOpcao();
                DPadPressionado = true;
                UIAudioSource.PlayOneShot(Selecionar);

            }
            if (DPadBaixo)
            {
                MenuSelecao--;
                SelecionarOpcao();
                DPadPressionado = true;
                UIAudioSource.PlayOneShot(Selecionar);
            }
        }

        if (Input.GetKeyDown("joystick button 0"))
        {
            ConfirmarOpcao();
        }
    }

    void InputsDPAD()
    {
        DPadEsquerda = false;
        DPadDireita = false;
        DPadBaixo = false;
        DPadCima = false;

        if ((Input.GetAxis("DPadHorizontal")) < 0) { DPadEsquerda = true; }
        if ((Input.GetAxis("DPadHorizontal")) > 0) { DPadDireita = true; }
        if ((Input.GetAxis("DPadVertical")) > 0) { DPadBaixo = true; }
        if ((Input.GetAxis("DPadVertical")) < 0) { DPadCima = true; }

        if (!DPadEsquerda && !DPadDireita && !DPadBaixo && !DPadCima)
        {
            DPadPressionado = false;
        }
    }

    void SelecionarOpcao()
    {
        if (MenuSelecao < 0) { MenuSelecao = 2; }
        if (MenuSelecao > 2) { MenuSelecao = 0; }

        switch (MenuSelecao)
        {
            case 0: //NOVO JOGO
                Selecao.transform.position = new Vector3(Selecao.transform.position.x, NovoJogo.transform.position.y, Selecao.transform.position.z);
                break;

            case 1: //OPÇÕES
                Selecao.transform.position = new Vector3(Selecao.transform.position.x, Opcoes.transform.position.y, Selecao.transform.position.z);
                break;

            case 2: //SALVAR E SAIR
                Selecao.transform.position = new Vector3(Selecao.transform.position.x, SalvarSair.transform.position.y, Selecao.transform.position.z);
                break;
        }
    }

    void ConfirmarOpcao()
    {
        switch (MenuSelecao)
        {
            case 0: //NOVO JOGO
                SceneManager.LoadScene(1);
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
