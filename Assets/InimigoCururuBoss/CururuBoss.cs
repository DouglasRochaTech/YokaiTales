using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CururuBoss : MonoBehaviour
{
    public float Vida = 1000;

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
    public Animator Animador;
    public GameObject EfeitoExplosao;

    [Header("Audio")]
    public AudioSource AudioSourceCururu;
    public AudioClip Mordida;
    public AudioClip Rosnada;
    public AudioClip GemidoDor;
    bool Rosnou;

    void Start()
    {
        Animador.Play("CairRugir");
        Animador.SetBool("Rugindo", true);
    }
}
