using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Servicos : MonoBehaviour
{
    ControleDeMensagens tempControleMensagem;
    public GameObject ObjectoControleMensagem;
    bool DentroServico;
    int IDMensagem;
    public Camera CameraFotografica;
    
    
    
    //Serviço 1 
    GameObject NPCTranseunte ;
    int QuantidadeFotos;
    public LayerMask LayerRaycast;
    GameObject UIPadrao;
    ControleUIPadrao tempControleUIPadrao ;
    GameObject Mensagem1 ;

    

    void Awake() {
    
    NPCTranseunte = GameObject.Find("NpcTranseunte"); 
    UIPadrao = GameObject.Find("UIPadrao");
    Mensagem1 = GameObject.Find("Mensagem1");

    }
    void Start()
    {
        tempControleMensagem = ObjectoControleMensagem.GetComponent<ControleDeMensagens>();

        //Servico 1
        tempControleUIPadrao = UIPadrao.GetComponent<ControleUIPadrao>();
        
    }

    
    void Update()
    {
        DentroServico = tempControleMensagem.DentroDoServiço;
        IDMensagem =tempControleMensagem.IDMensagem;
        
        if(DentroServico){
            if(IDMensagem == 1){
                //variaveis do servico    
               
               RaycastHit finalraio;

               if(Physics.Raycast(CameraFotografica.transform.position,CameraFotografica.transform.forward, out finalraio,100,LayerRaycast,QueryTriggerInteraction.Collide)){
                Debug.DrawRay(CameraFotografica.transform.position,CameraFotografica.transform.forward *100,Color.red);
                    if(finalraio.collider.name == "NpcTranseunte"){       
                        if(Input.GetMouseButtonDown(0)){
                            QuantidadeFotos +=1;
                            Debug.Log("FotoTirada");
                            }
                    }

               }
            //Caso complete o serviço    
            if(QuantidadeFotos >= 3){
                Debug.Log("Servico Completo");
                tempControleUIPadrao.ValorDinheiro += 300;
                tempControleMensagem.DentroDoServiço = false;
                tempControleMensagem.IDMensagem = 0;
                Destroy(Mensagem1);


            }
            //Caso perca o serviço
            
            float  Distancia = Vector3.Distance(NPCTranseunte.transform.position,this.transform.position);
            
            if(Distancia < 15){
                    Debug.Log("Servico Perdido");
                    tempControleMensagem.DentroDoServiço = false;
                    tempControleMensagem.IDMensagem = 0;
            }



            }

            
        }
    }
}
