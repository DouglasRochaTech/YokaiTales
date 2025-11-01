using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public float Vida = 100;

    [Header("Debug")]
    public float Distancia;
    float TimerAtaque = -1;
    float TimerDano = -1;
    bool Atacou;
    public SkinnedMeshRenderer Renderizador;
    public Material MaterialNormal;
    public Material MaterialDano;

    [Header("ItensEssenciais")]
    public GerenciadorGeral GG;
    public CharacterController Controlador;
    public Transform JogadorT;
    public Transform PontoDeInstanciacao;
    public GameObject ColisorDeDano;
    GameObject ColisorDeDanoInstanciado;
    public Animator InimigoAnimator;
    public GameObject EfeitoExplosao;

    [Header("Audio")]
    public AudioSource AudioSourceCururu;
    public AudioClip Mordida;
    public AudioClip Rosnada;
    public AudioClip GemidoDor;
    bool Rosnou;


    void Update() //Tudo aqui dentro atualiza a cada frame!!!
    {
        transform.LookAt(JogadorT);

        if (JogadorT != null)
        {
            if (TimerDano != -1)
            {
                TimerDano += Time.unscaledDeltaTime;

                if (TimerDano > 0.2f)
                {
                    Renderizador.material = MaterialNormal;
                    TimerDano = -1;
                    InimigoAnimator.SetBool("Dano", false);
                }
            }
            else
            {
                if (TimerAtaque == -1)
                {
                    if (JogadorT)
                    {
                        Distancia = Vector3.Distance(transform.position, JogadorT.position);

                        if (Distancia < 10)
                        {
                            if (!Rosnou) { AudioSourceCururu.PlayOneShot(Rosnada, 0.2f); Rosnou = true; }

                            if (Distancia > 1.5f)
                            {
                                Controlador.Move(transform.forward * 3 * Time.deltaTime);
                                InimigoAnimator.SetBool("Andar", true);
                            }
                            else
                            {
                                //INICIAR ATAQUE!!!
                                TimerAtaque = 0;
                                InimigoAnimator.SetBool("Andar", false);
                                InimigoAnimator.SetBool("Atacar", true);
                            }
                        }
                        else
                        {
                            InimigoAnimator.SetBool("Andar", false);
                        }
                    }
                    else
                    {
                        InimigoAnimator.SetBool("Andar", false);
                    }
                }
                else
                {
                    TimerAtaque += Time.deltaTime;

                    if (!Atacou)
                    {
                        if (TimerAtaque > 0.15f)
                        {
                            ColisorDeDanoInstanciado = Instantiate(ColisorDeDano, PontoDeInstanciacao.position, transform.rotation);
                            Destroy(ColisorDeDanoInstanciado, 0.05f);
                            Atacou = true;
                            AudioSourceCururu.PlayOneShot(Mordida, 0.4f);
                        }
                    }

                    if (TimerAtaque > 0.6f) //TERMINAR ATAQUE!!!
                    {
                        TimerAtaque = -1;
                        Atacou = false;
                        InimigoAnimator.SetBool("Atacar", false);
                    }
                }
            }
        }
        else
        {
            InimigoAnimator.SetBool("Andar", false);
            InimigoAnimator.SetBool("Atacar", false);
            InimigoAnimator.SetBool("Dano", false);
        }

        Controlador.Move(-transform.up * 2f * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DanoJogador")
        {
            ChecarDano(40);
        }
    }

    void ChecarDano(float Dano)
    {
        TimerDano = 0;
        Renderizador.material = MaterialDano;
        Controlador.Move(transform.forward * -0.3f);
        InimigoAnimator.SetBool("Dano", true);
        //GG.HitStopTimer = 0;
        Vida -= 40;
        AudioSourceCururu.PlayOneShot(GemidoDor, 0.5f);

        if (Vida <= 0)
        {
            InimigoAnimator.SetBool("Morrer", true);
            Renderizador.material = MaterialNormal;
            EfeitoExplosao.SetActive(true);

            Destroy(this);
            Destroy(Controlador);
        }
    }
}
