using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaidaVenda : MonoBehaviour
{
    public bool JogadorSaida;
    public Camera CameraSaida;
    public Camera Camera3Pessoa;
    ControladorJogador tempControladorJogador;
    public GameObject Jogador;
    EntradaVenda tempEntradaVenda;
    public GameObject EntradaVenda;
    public bool JogadorSaindo;
    Animator AnimacaoJogador;
    CharacterController Controlador;
    public GameObject ModeloJogador;
    Vector3 FinalSaida;
    bool SaidaFinal;
    public GameObject FadeImage;
    Animator AnimacaoFade;
    public GameObject Porta;
    Animator AnimacaoPorta;



    void Start()
    {
        tempControladorJogador = Jogador.GetComponent<ControladorJogador>();
        tempEntradaVenda = EntradaVenda.GetComponent<EntradaVenda>();
        Controlador = Jogador.GetComponent<CharacterController>();
        AnimacaoJogador = ModeloJogador.GetComponent<Animator>();
        FinalSaida = this.transform.position;
        FinalSaida.x += 3f;
        AnimacaoFade = FadeImage.GetComponent<Animator>();
        AnimacaoPorta = Porta.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other) {
    if(other.gameObject.name =="Jogador"){
        JogadorSaida = true;
    }    
    }

    void OnTriggerExit(Collider other) {
    if(other.gameObject.name =="Jogador"){
        JogadorSaida = false;    
    }
    }
    
    void Update()
    {
        if(JogadorSaida && !tempEntradaVenda.JogadorEntrando){
            if(Input.GetMouseButtonDown(0)){
                tempControladorJogador.ControleJogador = false;
                Camera3Pessoa.enabled =false;
                CameraSaida.enabled = true;
                Jogador.transform.position =this.transform.position;
                JogadorSaindo=true;
        }    
        }

        if(JogadorSaindo){
            if(Jogador.transform.position.x < FinalSaida.x){
                AnimacaoJogador.SetInteger("Animacoes",1);
                Jogador.transform.eulerAngles = transform.eulerAngles;
                Controlador.Move(this.transform.forward *(4*Time.deltaTime));
            }

            if(Jogador.transform.position.x > FinalSaida.x ){
                AnimacaoFade.SetBool("Fade",true);
                AnimacaoPorta.SetBool("PortaAbrindoParaFora",false);
                AnimacaoJogador.SetInteger("Animacoes",0);
                InvokeRepeating("Esperar",0.5f,0);
                JogadorSaindo = false;
            }
        }
    }
    void Esperar(){
        CameraSaida.enabled =false;
        Camera3Pessoa.enabled =true;
        tempControladorJogador.ControleJogador = true;
        AnimacaoFade.SetBool("Fade",false);
        

    }
}
