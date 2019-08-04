using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ControleDeMensagens : MonoBehaviour
{
    public GameObject Mensagem;
    bool MensagemAberta;
    public  TextMeshProUGUI TextoCorpoDaMensagem;
    public TextMeshProUGUI TextoNomeDoRemetente;
    public TextMeshProUGUI TextoAssuntoRemetente;  
    public int IDMensagem;        
    public bool DentroDoServiço;               //Verifica se o jogador esta dentro de um serviço
    Mensagem tempMSG;   
    public GameObject ControladorMSG;    
    bool ObjetivoAtual;
   
    
   
    
    
   
    
    

    void Start()
    {
    
        
    }

    void OnTriggerEnter(Collider other) {
      if(other.gameObject.tag == "Mensagem"){
        tempMSG =  other.gameObject.GetComponent<Mensagem>();
        TextoCorpoDaMensagem.text = tempMSG.MensagemRemetente; 
        TextoNomeDoRemetente.text = tempMSG.NomeRemetente ;
        TextoAssuntoRemetente.text = tempMSG.AssuntoRemetente;
        IDMensagem = tempMSG.IDMensagem;
        Mensagem.SetActive(true);
        MensagemAberta = true;
      
      } 
    }

    void OnTriggerExit(Collider other){
       if(other.gameObject.tag == "Mensagem"){
        Mensagem.SetActive(false);
        MensagemAberta = false;
       } 
    }
    void Update()
    {
        if (MensagemAberta){ //Aceitando servico
            if (Input.GetMouseButton(0)){
                //Passando conteudo da mensagem para as listas 
                DentroDoServiço = true;     
                //Mensagem.SetActive(false);
                MensagemAberta = false;   
                

        }   
        }

        if(!DentroDoServiço){  
            ControladorMSG.SetActive(true);            
        }else {
            ControladorMSG.SetActive(false);
        }        
        

        if (DentroDoServiço){
        if(Input.GetKeyDown(KeyCode.Tab)){
            ObjetivoAtual =!ObjetivoAtual;
        }

        if(ObjetivoAtual){
            Mensagem.SetActive(true); 
        }else{
            Mensagem.SetActive(false);
        }
        }
        

    }
    
}
