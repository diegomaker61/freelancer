using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimentoCamera1Pessoa : MonoBehaviour
{
    float rotacaoX1Pessoa;
    float rotacaoY1Pessoa;
    public GameObject Jogador;
    public float velocidadeRotacao1Pessoa;
    public float VelocidadeAcompanhamento;
    public GameObject PivorCamera3Pessoa;
    public float MovimentoCameraX;
    public float MovimentoCameraY;
    Animator ControleAnimacao;
    public float respiracao;
    MovimentoCamera3Pessoa tempMovimentoCamera3Pessoa;
    


    void Start()
    {
        tempMovimentoCamera3Pessoa = PivorCamera3Pessoa.GetComponent<MovimentoCamera3Pessoa>();
        ControleAnimacao = GetComponent<Animator>();
        InvokeRepeating("Respiracao",0,1);
        
    }

    void Respiracao(){
        if(Input.GetKey("z") && tempMovimentoCamera3Pessoa.Camera1Pessoa.enabled ==true ){
        respiracao-= 1;
        }
        
    }
    
    void Update()
    {
        
        //MOVIMENTCAO CAMERA FOTOGRAFICA
        Vector3 JogadorSemY = new Vector3(Jogador.transform.position.x,Jogador.transform.position.y+ 1.268f,Jogador.transform.position.z);
        this.transform.position = Vector3.Lerp(this.transform.position ,JogadorSemY,Time.deltaTime *VelocidadeAcompanhamento);

        //ORBITA CAMERA FOTOGRAFICA

        
        //Quando a camera estiver ligada 
        if(tempMovimentoCamera3Pessoa.Camera1Pessoa.enabled ==true){
            MovimentoCameraX = Input.GetAxis("Mouse X");
            MovimentoCameraY = Input.GetAxis("Mouse Y");

            if(MovimentoCameraX !=0 || MovimentoCameraY != 0){
            //Se voce estiver movimentando a camera
            ControleAnimacao.SetBool("CameraParada",false);

            rotacaoX1Pessoa += Input.GetAxis("Mouse X")*velocidadeRotacao1Pessoa;
            rotacaoY1Pessoa += Input.GetAxis("Mouse Y")*-velocidadeRotacao1Pessoa;
            rotacaoY1Pessoa = Mathf.Clamp(rotacaoY1Pessoa,-80,33);
            transform.eulerAngles = new Vector3(rotacaoY1Pessoa, rotacaoX1Pessoa,0);

            }else if( MovimentoCameraX == 0 && MovimentoCameraY ==0 && !Input.GetKey("z")){
            //Se voce nao estiver movimentando a camera o jogador respira 
            ControleAnimacao.SetBool("CameraParada",true);
            }else if( Input.GetKey("z")){
            ControleAnimacao.SetBool("CameraParada",false);
            }

            

        }else{ //Sempre que desativar a camera fotografica 
                
            transform.eulerAngles = new Vector3(Jogador.transform.eulerAngles.x,Jogador.transform.eulerAngles.y,0);
            rotacaoX1Pessoa = Jogador.transform.eulerAngles.y;
            rotacaoY1Pessoa = Jogador.transform.eulerAngles.x;
        }


    }

}
