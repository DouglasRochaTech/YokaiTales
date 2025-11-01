using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    public float Vida = 100; //"float" é a variável de número REAL. Quando a variável é "pública" ela é visível no editor!
    public float Velocidade = 5;

    [Header("Debug")]
    public float InputHor;
    public float InputVer;
    float AlturaPulo = 4;         // Altura do pulo
    float Gravidade = -40;        // Força da gravidade
    float VelocidadeVertical;
    float EspadadaTimer = -1;
    bool EspadadaSwitch; //"bool" é a variável BOOLEANA! O q quer dizer q é somente VERDADEIRA ou FALSA!
    int RotacaoColisor; //"int" é a variável de número INTEIRO

    [Header("ItensEssenciais")] //Os itens abaixo não são variáveis, mas referências a componentes e outras coisas do Unity!!!
    public Detector DetectorDeColisoes;
    public Transform PontoDeInstanciacao; //Os componentes "Transform" armazenam os dados de posição, rotação e tamanho dos objetos!!! 
    public Transform DirecaoDebug;
    public CharacterController Controlador; //Os componentes "CharacterController" facilitam na programação da física dos personagens
    public GameObject ColisorDeDano;
    GameObject ColisorDeDanoInstanciado;
    public Animator FoxAnimator;
    public ParticleSystem SistemaDeParticulas;

    [Header("Audio")]
    public AudioSource AudioSourceJogador;
    public AudioSource AudioSourceCorridinha;
    public AudioClip EspadadaFraca;
    public AudioClip[] FoxAtaquesFracos;
    public AudioClip[] FoxDores;
    public AudioClip Pulo;
    public AudioClip[] Passos;
    public AudioClip PassoNaGrama;
    float TimerGemido = -1;
    float TimerPassos;

    //EXPLICAÇÃO!!!!
    //Primeiro de tudo, esse código aqui serve como um componente. É uma espécie de "item" que coloquei no objeto do jogador.
    //Se você clicar no objeto do personagem, verá que esse código aqui está nele! E as variáveis que coloquei como "public" estarão visíveis no editor!

    void Update()
    {
        InputHor = Input.GetAxis("Horizontal");
        InputVer = Input.GetAxis("Vertical");

        DirecaoDebug.localPosition = new Vector3(InputHor * 10, 0, -InputVer * 10);

        if (EspadadaTimer == -1)
        {
            if (Mathf.Abs(InputHor) > 0.1f || Mathf.Abs(InputVer) > 0.1f) 
            {                                                             
                DirecaoDebug.transform.position = new Vector3(DirecaoDebug.transform.position.x, transform.position.y, DirecaoDebug.transform.position.z);
                Controlador.transform.LookAt(DirecaoDebug); 

                if (Input.GetKey("joystick button 4")) //CORRIDINHA - APERTANDO "LB"
                {
                    if (DetectorDeColisoes.NoChao)
                    {
                        FoxAnimator.SetBool("Correr", false);
                        FoxAnimator.SetBool("Correr4Patas", true);
                        Controlador.Move(transform.forward * (Velocidade * 1.4f) * Time.deltaTime);

                        if (!AudioSourceCorridinha.enabled) { AudioSourceCorridinha.enabled = true; }
                    }
                    else 
                    {
                        Controlador.Move(transform.forward * Velocidade * Time.deltaTime);

                        if (AudioSourceCorridinha.enabled) { AudioSourceCorridinha.enabled = false; }
                    }
                }
                else //CORRER NORMALMENTE
                {
                    FoxAnimator.SetBool("Correr", true);
                    FoxAnimator.SetBool("Correr4Patas", false);
                    Controlador.Move(transform.forward * Velocidade * Time.deltaTime);

                    if (AudioSourceCorridinha.enabled) { AudioSourceCorridinha.enabled = false; }
                    AudioPassos();
                }

                if (!SistemaDeParticulas.isPlaying)
                {
                    SistemaDeParticulas.Play();
                }
            }
            else
            {
                FoxAnimator.SetBool("Correr", false);
                FoxAnimator.SetBool("Correr4Patas", false);

                if (SistemaDeParticulas.isEmitting)
                {
                    SistemaDeParticulas.Stop();
                }
            }
            if (DetectorDeColisoes.NoChao)
            {
                if (!Input.GetKey("joystick button 4"))
                {
                    if (Input.GetKeyDown("joystick button 2")) //APERTANDO "X"
                    {
                        if (EspadadaSwitch) { FoxAnimator.SetBool("Espadada1", true); FoxAnimator.SetBool("Espadada2", false); RotacaoColisor = 180; }
                        if (!EspadadaSwitch) { FoxAnimator.SetBool("Espadada2", true); FoxAnimator.SetBool("Espadada1", false); RotacaoColisor = 0; }
                        EspadadaSwitch = !EspadadaSwitch;

                        ColisorDeDanoInstanciado = Instantiate(ColisorDeDano, transform.position, transform.rotation);
                        Destroy(ColisorDeDanoInstanciado, 0.16f);
                        ColisorDeDanoInstanciado.transform.Rotate(0, 0, RotacaoColisor);

                        AudioSourceJogador.PlayOneShot(EspadadaFraca);
                        AudioSourceJogador.PlayOneShot(FoxAtaquesFracos[Random.Range(0, 2)]);

                        EspadadaTimer = 0;
                    }
                }
            }
        }
        else
        {
            EspadadaTimer += Time.deltaTime; 

            if (EspadadaTimer >= 0.2f)
            {
                EspadadaTimer = -1;
                FoxAnimator.SetBool("Espadada1", false);
                FoxAnimator.SetBool("Espadada2", false);
            }
        }

        if (!DetectorDeColisoes.NoChao)
        {
            Controlador.Move(-transform.up * 2 * Time.deltaTime); //Pra cair um pouco mais rápido...
            VelocidadeVertical += Gravidade * Time.deltaTime;

            if (SistemaDeParticulas.isEmitting)
            {
                SistemaDeParticulas.Stop();
            }
        }
        else
        {
            if (Input.GetKeyDown("joystick button 0")) //APERTANDO "A"
            {
                FoxAnimator.Play("Pular");
                FoxAnimator.SetBool("Pulando", true);
                AudioSourceJogador.PlayOneShot(Pulo);
                VelocidadeVertical = Mathf.Sqrt(AlturaPulo * -3f * Gravidade);
            }
        }

        if (TimerGemido != -1)
        {
            TimerGemido += Time.deltaTime;

            if (TimerGemido > 0.5f) { TimerGemido = -1; }
        }

        //APLICA GRAVIDADE
        Controlador.Move(transform.up * VelocidadeVertical * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) //Tudo aqui dentro é executado cada vez que o "hitbox" do jogador (o componente box collider) detecta outros colliders
    {
        if (other.gameObject.tag == "DanoInimigo") //Se a tag do objeto do colisor detectado se chamar "DanoInimigo", então...
        {
            ChecarDano(15); //Ao escrever isso, o código dentro de ChecarDano(float Dano) é executado, com 15 como o valor da variável Dano!!!
        }
    }

    void ChecarDano(float Dano)
    {
        Vida -= Dano;
        FoxAnimator.Play("Dano");
        EspadadaTimer = 0;

        if (TimerGemido == -1)
        {
            AudioSourceJogador.PlayOneShot(FoxDores[Random.Range(0, 2)]);
            TimerGemido = 0;
        }

        if (Vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    void AudioPassos()
    {
        if (!DetectorDeColisoes.NoChao) { return; }
        TimerPassos += Time.deltaTime;

        if (TimerPassos > 0.185f)
        {
            TimerPassos = 0;
            if (!DetectorDeColisoes.NaGrama)
            {
                AudioSourceJogador.PlayOneShot(Passos[Random.Range(0, 3)], 0.3f);
            }
            else
            {
                AudioSourceJogador.PlayOneShot(Passos[Random.Range(0, 3)], 0.1f);
                AudioSourceJogador.PlayOneShot(PassoNaGrama, 0.03f);
            }
        }
    }
}
