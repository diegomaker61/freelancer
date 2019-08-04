using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventario : MonoBehaviour
{
    bool EntradaInventario;
    public bool DentroInventaro;
    public Camera CameraInventario;
    public Camera Camera3Pessoa;
    public GameObject Jogador;
    ControladorJogador tempControleJogador;
    public GameObject Seletor;
    public int IndiceHorizontal0,IndiceHorizontal1,IndiceHorizontal2,IndiceHorizontal3 ;
    public int IndiceVertical;

    //Listas 
    public List<GameObject> CamerasFotograficas;
    public List<GameObject> Flashs;
    public List<GameObject> Baterias;
    public List<GameObject> Lentes;
    public List<GameObject> Memoria;
    public List<GameObject> Slots;


    public GameObject FundoInventario;
    public GameObject Visualizacao;
    Item tempItem;
    public GameObject EncaixeLente;
    public GameObject EncaixeFlash;

    //UI
    public TextMeshProUGUI NomeCamera;
    public TextMeshProUGUI NomeLentes;
    public TextMeshProUGUI NomeBateria;
    public TextMeshProUGUI NomeFlash;
    public TextMeshProUGUI NomeMemoria;

   
    //ATB Itens
    public GameObject CameraFotografica;
    CameraFotografica tempCameraFotografica;
    public float ResolucaoAtual;
    public float Iluminacao;   
    public float ZoomAtual;  
    public float MemoriaAtual;
    public float BateriaAtual;
    

    



    void Start()
    {
        tempCameraFotografica = CameraFotografica.GetComponent<CameraFotografica>();
        tempControleJogador = Jogador.GetComponent<ControladorJogador>();
        IndiceHorizontal0 = 0;
        IndiceHorizontal1 = 0;

    }

    void OnTriggerEnter(Collider other) {
    if(other.gameObject.name =="Jogador"){
        EntradaInventario = true;
    }
        
    }

    void OnTriggerExit(Collider other) {
    if(other.gameObject.name=="Jogador"){
        EntradaInventario = false;
    }
        
    }
    void Update()
    {

        if(EntradaInventario){
            if(Input.GetMouseButtonDown(0)&& CamerasFotograficas.Count>0){
                tempControleJogador.ControleJogador = false;
                Camera3Pessoa.enabled = false;
                CameraInventario.enabled = true;
                DentroInventaro = true;
            }    
        }

        
        if(DentroInventaro){
        // Ativa Interface de inventario
           FundoInventario.SetActive(true); 

                //Rotacao do obj independe da opcao atual
                CamerasFotograficas[IndiceHorizontal0].transform.eulerAngles = Visualizacao.transform.eulerAngles;
                //Recebendo Atributos da camera
                Item tempCameraAtual = CamerasFotograficas[IndiceHorizontal0].GetComponentInChildren<Item>();
                ResolucaoAtual = tempCameraAtual.ResolucaoMaxima;
                

                
                if(IndiceVertical == 0){

                //Pintando texto 
                NomeCamera.color = Color.red;
                //Coloca a camera selecionada na visualizacao e Rotacionar ela de acordo
                CamerasFotograficas[IndiceHorizontal0].transform.position = Visualizacao.transform.position;
                
                
                //Coloca as cameras nao selecionadas nos seus slots
                foreach (GameObject Objeto in CamerasFotograficas){
                    Item tempObj = Objeto.GetComponentInChildren<Item>();
                    if(CamerasFotograficas.IndexOf(Objeto)!=IndiceHorizontal0){
                        foreach (GameObject SlotFinal in Slots){
                            if(tempObj.SlotFinal == Slots.IndexOf(SlotFinal)){
                                Objeto.transform.position = Slots[tempObj.SlotFinal].transform.position;
                                Objeto.transform.eulerAngles = new Vector3(0,0,0);

                            }   
                        } 
                    }      
                }

                if(Input.GetKeyDown("a") && IndiceHorizontal0 > 0){
                    IndiceHorizontal0-=1;
                }       
                if (Input.GetKeyDown("d") && IndiceHorizontal0<CamerasFotograficas.Count-1){
                    IndiceHorizontal0+=1;
                }   

                }else{
                //Sem selecao
                NomeCamera.color = Color.white;   
                }

                /*************************************************************************************/
                /*************************************************************************************/

                //Rotacao do obj independe da opcao atual
               
                if(Lentes.Count > 0){
                Lentes[IndiceHorizontal1].transform.position = EncaixeLente.transform.position;
                Lentes[IndiceHorizontal1].transform.eulerAngles = Visualizacao.transform.eulerAngles;
                }
                

                if(IndiceVertical == 1){

                //Pintando texto 
                NomeLentes.color = Color.red;

                foreach (GameObject Lente in Lentes){
                    Item tempItem = Lente.GetComponentInChildren<Item>();
                    if(Lentes.IndexOf(Lente)!=IndiceHorizontal1){
                        foreach(GameObject SlotFinal in Slots){
                            if(tempItem.SlotFinal == Slots.IndexOf(SlotFinal)){
                            Lente.transform.position = Slots[tempItem.SlotFinal].transform.position;
                            //Depois colocar rotacao inicial pre-definida
                            Lente.transform.eulerAngles = new Vector3(0,0,0);     
                            }    
                        }    
                    }
                }

                if(Input.GetKeyDown("a")&& IndiceHorizontal1 > 0){
                    IndiceHorizontal1-=1;
                }
                if(Input.GetKeyDown("d") && IndiceHorizontal1< Lentes.Count-1){
                    IndiceHorizontal1+=1;
                }

                }else{
                NomeLentes.color = Color.white;   
                }

                /*************************************************************************************** */
                /*************************************************************************************** */


                if(IndiceVertical == 2){
                NomeBateria.color =Color.red;





                }else{
                NomeBateria.color = Color.white;
                }

                /*************************************************************************************** */
                /*************************************************************************************** */
                
                if(Flashs.Count>0){
                Flashs[IndiceHorizontal3].transform.position = EncaixeFlash.transform.position;
                Flashs[IndiceHorizontal3].transform.eulerAngles = Visualizacao.transform.eulerAngles; 
                }
                
                

                if(IndiceVertical ==3){
                NomeFlash.color=Color.red;  
                
                foreach (GameObject Flash in Flashs){
                    Item tempItem = Flash.GetComponentInChildren<Item>();
                    if(Flashs.IndexOf(Flash)!= IndiceHorizontal3){
                        foreach(GameObject SlotFinal in Slots){
                            if(tempItem.SlotFinal == Slots.IndexOf(SlotFinal)){
                                Flash.transform.position = Slots[tempItem.SlotFinal].transform.position;
                                Flash.transform.eulerAngles = new Vector3(0,0,0);

                            }    
                        }   
                    }

                }
 

                
                if(Input.GetKeyDown("a")&& IndiceHorizontal3 > 0){
                    IndiceHorizontal3-=1;
                }
                if(Input.GetKeyDown("d") && IndiceHorizontal3< Flashs.Count-1){
                    IndiceHorizontal3+=1;
                }
                
                
                
                
                
                
                }else{
                NomeFlash.color =Color.white;    
                }


                  



            //Rotacionando Visualizacao
            if(Input.GetMouseButton(0)){
                Visualizacao.transform.eulerAngles+= new Vector3(0,-Input.GetAxis("Mouse X")*15,0);
            }
            

            //Alterna verticalmente 
            if(Input.GetKeyDown("w")){
                IndiceVertical-=1;
            }else if(Input.GetKeyDown("s")){
                IndiceVertical+=1;
            }

            //Saindo do Inventario 
            if(Input.GetMouseButtonDown(1)){
                SairInventario();   
            }

            if(Input.GetKeyDown("p")){
                PegarCamera();
            }

        }else{
            //Desativa interface do inventario
            FundoInventario.SetActive(false);    
        }
    }

    void SairInventario(){
        tempControleJogador.ControleJogador = true;
        Camera3Pessoa.enabled = true;
        CameraInventario.enabled = false;
        DentroInventaro = false;

    }

    void PegarCamera(){
        tempCameraFotografica.ResAtual = ResolucaoAtual;
    }
}
