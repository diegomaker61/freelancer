using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaVenda : MonoBehaviour
{
    public Camera Camera1Pessoa;
    public Camera Camera3Pessoa;
    public Camera CameraEntrada;
    bool JogadorNaEntrada;
    public bool JogadorEntrando;
    public GameObject Jogador;
    ControladorJogador tempControleJogador;
    CharacterController Controlador;
    float velocidade ;
    Animator AnimacaoJogador;
    public GameObject ModeloJogador;
    public GameObject SaidaVenda;
    SaidaVenda tempSaidaVenda;
    Vector3 PontoFinalEntrada;
    public GameObject ImagemFade;
    Animator AnimacaoFade;
    public GameObject Porta;
    Animator AnimacaoPorta;
    
    
    void Start()
    {
        tempControleJogador = Jogador.GetComponent<ControladorJogador>();
        Controlador = Jogador.GetComponent<CharacterController>();
        AnimacaoJogador = ModeloJogador.GetComponent<Animator>();
        tempSaidaVenda = SaidaVenda.GetComponent<SaidaVenda>();
        PontoFinalEntrada = transform.position;
        PontoFinalEntrada.x -= 3f;
        AnimacaoFade = ImagemFade.GetComponent<Animator>();
        AnimacaoPorta = Porta.GetComponent<Animator>();
    

    }
    
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.name =="Jogador"){
        JogadorNaEntrada = true;
    }
    }
    void OnTriggerExit(Collider other) {
        if(other.gameObject.name =="Jogador"){
        JogadorNaEntrada = false;
    }
        
    }
    void Update()

    {  
       
        
        if(JogadorNaEntrada){ 
        if(Input.GetMouseButtonDown(0) && !tempSaidaVenda.JogadorSaindo){
        Camera1Pessoa.enabled = false;
        Camera3Pessoa.enabled = false;  
        CameraEntrada.enabled = true;
        Jogador.transform.position = this.transform.position;
        JogadorEntrando = true;
        
        }
        }

        if(JogadorEntrando){
            if(Jogador.transform.position.x > PontoFinalEntrada.x){
                AnimacaoJogador.SetInteger("Animacoes",1);
                Jogador.transform.eulerAngles = transform.eulerAngles;
                Controlador.Move(this.transform.forward *(4*Time.deltaTime));

            }
            if(Jogador.transform.position.x < PontoFinalEntrada.x){
                AnimacaoFade.SetBool("Fade",true);
                AnimacaoPorta.SetBool("PortaAbrindo",false);
                AnimacaoJogador.SetInteger("Animacoes",0);
                InvokeRepeating("Esperar",0.5f,0);
                JogadorEntrando = false;

            }   
            
        }
  
    }

    void Esperar(){
        CameraEntrada.enabled =false;
        Camera3Pessoa.enabled =true;
        tempControleJogador.ControleJogador = true;
        AnimacaoFade.SetBool("Fade",false);
    }
  
}
