using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DiaNoite : MonoBehaviour
{   

    public float TempoDiaMinutos;
    float VelocidadeDia;
    public float TempoTotal ;
    public int Hora;
    public float Minuto;
    Transform t;
    Light Luz;
    public TextMeshProUGUI Horario;
    string Horastring;
    public bool TempoPassando;


    void Start()
    {
    t = this.GetComponent<Transform>();
    Luz = this.GetComponent<Light>();
    TempoDiaMinutos = TempoDiaMinutos*60; //Converte min 
    transform.eulerAngles = new Vector3(0,0,0); 

    }

    
    void Update()
    {
    
    if(TempoPassando){
    t.Rotate(360/TempoDiaMinutos*Time.deltaTime,0,0);
    VelocidadeDia = 24/TempoDiaMinutos*Time.deltaTime;
    TempoTotal+= VelocidadeDia;
    
    //Clamp tempo total
    if(TempoTotal>24){
        TempoTotal = 0;
    }

    Hora = Mathf.FloorToInt(TempoTotal);
    Minuto = Mathf.FloorToInt((TempoTotal - Hora)*60);
    }

    //Ajuste de intensidade de acordo com o horario
    if(Hora>16){
        Luz.intensity -=VelocidadeDia;
    }

    if(Hora>4 && Hora< 7 && Luz.intensity<1){
        Luz.intensity += VelocidadeDia;
    }
    if(Hora == 6){
        Luz.intensity = 1;
    }

    //Valor do horario na tela 
    
    if( Minuto<10){
    Horario.text = Hora.ToString() +":0"+ Minuto.ToString();    
    }else{
    Horario.text = Hora.ToString() +":"+ Minuto.ToString();
    }
    





    
    

    

     


    

        

        

    }
   
}
