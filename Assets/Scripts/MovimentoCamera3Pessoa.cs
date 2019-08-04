using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoCamera3Pessoa : MonoBehaviour
{
    public GameObject Jogador;              //Referencia ao jogador
    public GameObject Camera;               //Referencia a camera main
    public float VelocidadeAcompanhamento;  //Velocidade que a camera acompanha o jogador 
    public GameObject LocalDaCamera;        //Local da camera onde ela sempre volta
    float rotacaoX3Pessoa;                  //Rotacao de camera 
    float rotacaoY3Pessoa;                  //Rotacao de camera
    public float velocidadeRotacao3Pessoa; //Velocidade de rotacao da camera 
    RaycastHit hit;                        //HIT para colisao de camera 
    public LayerMask Layer;                //Mascara para colisao de camera
    public Camera Camera3Pessoa;           //Objecto para ligar /Desligar
    public Camera Camera1Pessoa;           //Objecto para ligar desligar
    public bool CameraFotografia;           // switch da camera fotografica 
    public GameObject Mensagem;             //Saber se tem alguma mensagem na tela
    CameraFotografica tempCameraFotografica;    //Pega variavel da camera fotografica
    public GameObject PivorCamera1Pessoa;    
    ControladorJogador tempControladorJogador;   
    public float VelocidadeCameraColisao;
    public GameObject ReferenciaCamera3Pessoa;

    
    
    
    
    void Start() {
        //Pegando o componente das camera 
        Camera1Pessoa.GetComponent<Camera>();
        Camera3Pessoa.GetComponent<Camera>();
        tempCameraFotografica = Camera1Pessoa.GetComponent<CameraFotografica>();
        tempControladorJogador = Jogador.GetComponent<ControladorJogador>();
        
    }


    void Update() {
        //Se pressionar o btn direito , a camera estiver com bateria e nao tiver dentro de uma missao
        if(Input.GetMouseButtonDown(1) && !Mensagem.activeSelf && tempCameraFotografica.BateriaAtual>0 && tempControladorJogador.ControleJogador){
            CameraFotografia =!CameraFotografia;
        }
        
        //Se camera fotografica estiver ativa e com bateria 
        if(!tempControladorJogador.VendaEntrada){
        if(CameraFotografia && tempCameraFotografica.BateriaAtual>0){
            Camera3Pessoa.enabled = false;
            Camera1Pessoa.enabled = true;
        }else{
            Camera3Pessoa.enabled = true;
            Camera1Pessoa.enabled = false;
            CameraFotografia =false;

        }
        }
    }

    void FixedUpdate()

    {  
        //ORBITA CAMERA 3 Pessoa
        
        if (Camera3Pessoa.enabled==true){        
        rotacaoX3Pessoa += Input.GetAxis("Mouse X")*velocidadeRotacao3Pessoa;        
        rotacaoY3Pessoa += Input.GetAxis("Mouse Y")*-velocidadeRotacao3Pessoa;       
        rotacaoY3Pessoa = Mathf.Clamp(rotacaoY3Pessoa,-45,38);
        transform.eulerAngles = new Vector3(rotacaoY3Pessoa,rotacaoX3Pessoa,0); 
        }
        
        //CAMERA 3 PESSOA SEGUINDO JOGADOR 
        transform.position = Vector3.Lerp(transform.position ,ReferenciaCamera3Pessoa.transform.position,Time.deltaTime *VelocidadeAcompanhamento);
        //transform.position =Jogador.transform.position;
       

        //COLISAO CAMERA 3° PESSOA

        if(!Physics.Linecast(ReferenciaCamera3Pessoa.transform.position,LocalDaCamera.transform.position,out hit,Layer)){
             Camera.transform.position = Vector3.Lerp(Camera.transform.position,LocalDaCamera.transform.position,1*Time.deltaTime);
            
        }else if(Physics.Linecast(ReferenciaCamera3Pessoa.transform.position,LocalDaCamera.transform.position,out hit,Layer)){
            //Camera.transform.position = hit.point + Camera.transform.forward * 0.3f;     
            Camera.transform.position = Vector3.Lerp(LocalDaCamera.transform.position,hit.point+Camera.transform.forward*0.3f,VelocidadeCameraColisao);      
        }
        
    }
}
