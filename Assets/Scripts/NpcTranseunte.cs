using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NpcTranseunte : MonoBehaviour
{
    NavMeshAgent NavegadorTranseunte;
    public string NomeTag;
    public int PontoAtual ;
    public GameObject [] PontosAndaveis;
    Vector3 ProximoPonto;
    public string EstadoTranseunte;
    
    void Start()
    {  
    //pega o navmesh do transeunte 
    NavegadorTranseunte = this.GetComponent<NavMeshAgent>(); 
    }

    //Caso o transeunte colida com waypoint 
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == NomeTag && EstadoTranseunte == "Patrulha"){
            if(PontoAtual < PontosAndaveis.Length){
                PontoAtual += 1 ;
                Irparadestino();
            }
        } 
        if(other.gameObject.name =="Jogador"){
            EstadoTranseunte = "BloqueadoPeloJogador";
            
        }
        
    }
    void Update()
    {
        if(PontoAtual == PontosAndaveis.Length && EstadoTranseunte == "Patrulha"){
          PontoAtual = 0;
          Irparadestino();
        }
    }

   

    void Irparadestino(){
        if(PontoAtual<PontosAndaveis.Length ){
            ProximoPonto = new Vector3(PontosAndaveis[PontoAtual].transform.position.x,PontosAndaveis[PontoAtual].transform.position.y,PontosAndaveis[PontoAtual].transform.position.z);
            NavegadorTranseunte.SetDestination(ProximoPonto);
            transform.LookAt(PontosAndaveis[PontoAtual].transform.position);
        }
    }
}
