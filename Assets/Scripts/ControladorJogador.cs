using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJogador : MonoBehaviour
{

    Camera cam ;
    public Camera Camera1Pessoa; 
    public Camera Camera3Pessoa;
    public CharacterController Controlador;
    float RotacaoCamera;
    Vector3 MovimentoFrente;
    Vector3 MovimentoDireita;
    Vector3 MovimentoEsquerda;
    Vector3 MovimentoTras;
    Vector3 MovimentoFrenteDireita;
    Vector3 MovimentoFrenteEsquerda;
    Vector3 MovimentoTrasDireita;
    Vector3 MovimentoTrasEsquerda;   
    Vector3 Pulo;
    public float ForcaPulo;
    public float VelocidadeMovimento;
    public float EscalaGravidade;
    public GameObject ModeloJogador;
    Animator Animacao;
    public bool VendaEntrada ;
    public bool ControleJogador = true ;


    void Start()
    {
        Cursor.visible = false;
        Screen.SetResolution(1280, 720, true);
        Controlador.GetComponent<CharacterController>();
        Animacao = ModeloJogador.GetComponent<Animator>();
    }

     void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "VendaEntrada"){
        VendaEntrada = true;
        }
        
    }
    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "VendaEntrada"){
        VendaEntrada = false;
        }
    }

    void Update(){
        if(VendaEntrada){
        if(Input.GetMouseButtonDown(0)){
            ControleJogador = false;
        }
        }

    }

    void FixedUpdate()
    {
        //Verifica qual camera esta ativa 
        if(Camera3Pessoa.enabled){
            cam = Camera3Pessoa;
        }else{
            cam = Camera1Pessoa;
        }



        RotacaoCamera = Mathf.Floor(cam.transform.eulerAngles.y);       
        
        MovimentoFrente = cam.transform.forward * VelocidadeMovimento;
        MovimentoFrente.y = MovimentoFrente.y +( Physics.gravity.y * EscalaGravidade);
        
        MovimentoDireita = cam.transform.right * VelocidadeMovimento;
        MovimentoDireita.y = MovimentoDireita.y +( Physics.gravity.y * EscalaGravidade);

        MovimentoEsquerda = -cam.transform.right * VelocidadeMovimento;
        MovimentoEsquerda.y = MovimentoEsquerda.y +( Physics.gravity.y * EscalaGravidade);

        MovimentoTras = -cam.transform.forward * VelocidadeMovimento;
        MovimentoTras.y = MovimentoTras.y +( Physics.gravity.y * EscalaGravidade);

        MovimentoFrenteDireita = cam.transform.forward;
        MovimentoFrenteDireita = (Quaternion.Euler(0, +45, 0) * MovimentoFrenteDireita) * VelocidadeMovimento ;
        MovimentoFrenteDireita.y =  MovimentoFrenteDireita.y +( Physics.gravity.y * EscalaGravidade);

        MovimentoFrenteEsquerda = cam.transform.forward;
        MovimentoFrenteEsquerda = (Quaternion.Euler(0,-45,0)* MovimentoFrenteEsquerda) * VelocidadeMovimento;
        MovimentoFrenteEsquerda.y = MovimentoFrenteEsquerda.y + (Physics.gravity.y * EscalaGravidade);

        MovimentoTrasDireita = -cam.transform.forward;
        MovimentoTrasDireita = (Quaternion.Euler(0,-45,0)* MovimentoTrasDireita) * VelocidadeMovimento;
        MovimentoTrasDireita.y = MovimentoTrasDireita.y + (Physics.gravity.y * EscalaGravidade);

        MovimentoTrasEsquerda = -cam.transform.forward;
        MovimentoTrasEsquerda = (Quaternion.Euler(0,+45,0)* MovimentoTrasEsquerda) * VelocidadeMovimento;
        MovimentoTrasEsquerda.y = MovimentoTrasEsquerda.y + (Physics.gravity.y * EscalaGravidade);

        Pulo.y = Pulo.y + (Physics.gravity.y * EscalaGravidade);
        
        if(ControleJogador){
        //Para frente direita
        if(Input.GetKey("w")&& Input.GetKey("d")){
            Controlador.Move(MovimentoFrenteDireita *Time.deltaTime);
            transform.eulerAngles = new Vector3(0,RotacaoCamera,0);
        }//Para frente esquerda
        else if(Input.GetKey("w")&& Input.GetKey("a")){
            Controlador.Move(MovimentoFrenteEsquerda *Time.deltaTime);
            transform.eulerAngles = new Vector3(0,RotacaoCamera,0);
        }//Para tras esquerda
        else if(Input.GetKey("s")&& Input.GetKey("a")){
            Controlador.Move(MovimentoTrasEsquerda *Time.deltaTime);
            transform.eulerAngles = new Vector3(0,RotacaoCamera,0);
        }//Para tras direita
        else if(Input.GetKey("s")&& Input.GetKey("d")){
            Controlador.Move(MovimentoTrasDireita *Time.deltaTime);
            transform.eulerAngles = new Vector3(0,RotacaoCamera,0);
        }//Para frente        
        else if(Input.GetKey("w")){
            Controlador.Move(MovimentoFrente *Time.deltaTime);
            transform.eulerAngles = new Vector3(0,RotacaoCamera,0);
            Animacao.SetInteger("Animacoes",1);
        }//Para esquerda
        else if(Input.GetKey("a")){
            Controlador.Move(MovimentoEsquerda *Time.deltaTime);
            transform.eulerAngles = new Vector3(0,RotacaoCamera,0);
        }//Para Direita
        else if(Input.GetKey("d")){
            Controlador.Move(MovimentoDireita *Time.deltaTime);
            transform.eulerAngles = new Vector3(0,RotacaoCamera,0);
        }//Para Tras
        else if(Input.GetKey("s")){
            transform.eulerAngles = new Vector3(0,RotacaoCamera,0);  
            Controlador.Move(MovimentoTras *Time.deltaTime);
            Animacao.SetInteger("Animacoes",-1);
        }else{
            Animacao.SetInteger("Animacoes",0);
        }
        
        if(Input.GetKeyDown("space")&& Controlador.isGrounded ){
            Pulo.y = ForcaPulo;
        }

        Controlador.Move(Pulo *Time.deltaTime);
        
        //Corrida
        if(Input.GetKey("left shift")){
            VelocidadeMovimento = 10f;
        }else{
            VelocidadeMovimento = 4f;                       
        }
        }
    }   

}
