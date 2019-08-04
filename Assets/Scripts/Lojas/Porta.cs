using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    Animator AnimacaoPorta;
    EntradaVenda tempEntradaVenda;
    SaidaVenda tempSaidaVenda;
    public GameObject EntradaVenda;
    public GameObject SaidaVenda;
    public bool JogadorNaPorta;
    
    void Start()
    {
        AnimacaoPorta = gameObject.GetComponent<Animator>();
        tempEntradaVenda = EntradaVenda.GetComponent<EntradaVenda>();
        tempSaidaVenda = SaidaVenda.GetComponent<SaidaVenda>();
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Jogador"){
        JogadorNaPorta =true;
           
     }   
    }
    void OnTriggerExit(Collider other){
        if(other.gameObject.name == "Jogador"){
            JogadorNaPorta =false;
        }
    }
    
    void Update()
    {
        if(JogadorNaPorta && tempEntradaVenda.JogadorEntrando){
            AnimacaoPorta.SetBool("PortaAbrindo",true); 
        }
        if( JogadorNaPorta && tempSaidaVenda.JogadorSaindo){
            AnimacaoPorta.SetBool("PortaAbrindoParaFora",true); 
        }
    }
}
