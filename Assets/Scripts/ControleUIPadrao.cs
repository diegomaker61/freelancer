using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControleUIPadrao : MonoBehaviour
{
    public float ValorDinheiro;
    public TextMeshProUGUI TextoDinheiro;
    public GameObject FundoBanco;
    public GameObject PivorCamera;
    public GameObject FundoBarraBateria;
    public bool Camera1Pessoa;
    public GameObject CameraFotografica;
    MovimentoCamera3Pessoa tempMovimentoCamera;
    CameraFotografica tempCameraFotografica;

    bool pause ;

    void Start()
    {   
        tempMovimentoCamera = PivorCamera.GetComponent<MovimentoCamera3Pessoa>();
        tempCameraFotografica = CameraFotografica.GetComponent<CameraFotografica>();
    }

   
    void Update()
    {   
        

        //Pega a variavel de Movimento Camera 
        Camera1Pessoa = tempMovimentoCamera.CameraFotografia;
        //Mostrando dinheiro do banco 
        TextoDinheiro.text = ValorDinheiro.ToString();

        // Caso a camera de fotografia seja ligada 
        if(Camera1Pessoa && tempCameraFotografica.BateriaAtual >0 ){
            FundoBanco.SetActive(false);
            
            //Se tiver batendo foto
            if(tempCameraFotografica.TirandoFoto){
                FundoBarraBateria.SetActive(false);
            }else{
                FundoBarraBateria.SetActive(true);
            }
            
        }else{
            FundoBanco.SetActive(true);
            FundoBarraBateria.SetActive(false);            
        }


        if(Input.GetKeyDown("p")){
            pause =!pause;
            JogoPausado();
        }
    
     
    }
    void JogoPausado(){
        if(pause){
            Time.timeScale = 0;
        }else{
            Time.timeScale = 1;
        }
    }


}
