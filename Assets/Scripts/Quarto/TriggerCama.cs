using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCama : MonoBehaviour
{
    DiaNoite tempDiaNoite;
    public GameObject Sol;
    float HoraIncialSono;
    float HoraFinalSono;
    public bool PossibilidadeSono;
    Animator FadeAnimacao;
    public GameObject FadeImage;
    MeshRenderer MeshObj;
    BoxCollider BoxObj;
    void Start()
    {
    tempDiaNoite = Sol.GetComponent<DiaNoite>();
    FadeAnimacao = FadeImage.GetComponent<Animator>();
    MeshObj = this.GetComponent<MeshRenderer>();
    BoxObj = this.GetComponent<BoxCollider>();
    }

    void OnTriggerEnter (Collider other){
    if(other.gameObject.name == "Jogador"){
        PossibilidadeSono = true;
    }
    
    }
    void OnTriggerExit(Collider other){
    if(other.gameObject.name == "Jogador"){
        PossibilidadeSono = false;    
    }
    }
    
    void Update()
    {

    //Habilita e desabilita o obj de acordo com horario 
    if(tempDiaNoite.Hora>=20 && tempDiaNoite.Hora<=24){
    BoxObj.enabled = true;
    MeshObj.enabled = true;
    }else {
    BoxObj.enabled = false;
    MeshObj.enabled = false;    
    }
    
    if(PossibilidadeSono){
    if(Input.GetMouseButtonDown(0)){
        FadeAnimacao.SetBool("Fade",true);
        tempDiaNoite.TempoPassando = false;
            InvokeRepeating("FadeCama",1,0);


    }
    }
    }
    void FadeCama(){
    tempDiaNoite.TempoTotal = 6;
    tempDiaNoite.Hora= 6;
    tempDiaNoite.Minuto = 0;
    Sol.transform.eulerAngles = new Vector3(0,0,0);
    FadeAnimacao.SetBool("Fade",false);
    tempDiaNoite.TempoPassando = true;
    }
}
