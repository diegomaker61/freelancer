using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;

public class  CameraFotografica : MonoBehaviour
{
    
    bool Camera1Pessoa;  //verifica se a camera ta em primeira pessoa
    public GameObject PivorCamera; 
    Texture textura;  //textura que pega o screenshot
    public RawImage Imagem;  //RAW image externa
    private RawImage PegaImagem; //Pega o componente Raw Image
    public Canvas UIPADRAO;   //Local onde vai acoplar a foto batida
    public Camera CameraFotografia; 
    public float zoom;         
    public float VelocidadeZoom;    
    public float zoomMaximo;  //Zoom maximo da camera atual 
    public float BateriaMax;  //Quantidade maxima de bateria de cada camera 
    public float BateriaAtual; // Bateria atual da camera atual
    public Image BarraBateria;  //Barra da bateria na HUD 
    MovimentoCamera3Pessoa TempVar;
    public GameObject UltimaFototirada;
    bool Galeria;
    RectTransform PegaTransform;
    public int GaleriaFotoAtual;
    public List <RawImage> FotosGuardadas;
    ControleDeMensagens tempControledeMensagens;
    public LayerMask RaycastInteracaoCamera;
    public GameObject Jogador;
    public bool TirandoFoto;
    public GameObject IconeGaleria;
    public float ZoomFicticio;
    public float PorcentagemBateria;
    public GameObject Obj_Obturador;
    Animator ObturadorAnim;
    public TextMeshProUGUI TextoPorcentagemBateria;
    public TextMeshProUGUI TextoMemoriaCartao;
    public Image FundoBateria;
    Animator AnimFundoBateria;
    public bool ProcessandoFoto;
    public int MemoriaAtual;
    public int MemoriaMaxima;
    public List <Image> EscalaZoom;
    
    //CameraAtual
    public float ResAtual;


    


    
    void Start()
    
    {
    //Pegando componente 
        PegaImagem = Imagem.GetComponent<RawImage>();
        CameraFotografia = gameObject.GetComponent<Camera>();
        TempVar = PivorCamera.gameObject.GetComponent<MovimentoCamera3Pessoa>();
        InvokeRepeating("Bateria",0,1);
        PegaTransform =UltimaFototirada.GetComponent<RectTransform>();
        tempControledeMensagens =Jogador.GetComponent<ControleDeMensagens>();
        ObturadorAnim = Obj_Obturador.GetComponent<Animator>();
        AnimFundoBateria = FundoBateria.GetComponent<Animator>();
        
       
    }

    void Bateria(){
    //Funcionamento da bateria 
    if(Camera1Pessoa && BateriaAtual>0){   
        BateriaAtual -= 0.1f ;
        BateriaAtual = Mathf.Floor(BateriaAtual);
        BarraBateria.rectTransform.sizeDelta = new Vector2((39*BateriaAtual)/BateriaMax,17);
        PorcentagemBateria = (100*BateriaAtual)/BateriaMax;
        PorcentagemBateria = Mathf.Floor(PorcentagemBateria);
        TextoPorcentagemBateria.text = PorcentagemBateria.ToString()+"%";

        //AlertaBateria
        if(PorcentagemBateria<=15){
        BarraBateria.color = new Color32(255,0,0,255);
        AnimFundoBateria.SetBool("BateriaPiscando",true);
        }else{
        BarraBateria.color = new Color32(255,255,255,255);
        AnimFundoBateria.SetBool("BateriaPiscando",false);
        }
        }
    }

    void Update() {
       

    //Pega a variavel de outro script e iguala a desse 
    Camera1Pessoa = TempVar.CameraFotografia;

    //Caso a camera esteja ligada
    if(Camera1Pessoa){
        
        //Zoom
        zoom += Input.GetAxis("Mouse ScrollWheel")*-VelocidadeZoom;
        zoom = Mathf.Clamp(zoom,zoomMaximo,60);
        CameraFotografia.fieldOfView = zoom;

        //Zoom Ficticio
        ZoomFicticio += Input.GetAxis("Mouse ScrollWheel")*10;
        ZoomFicticio  = Mathf.Clamp(ZoomFicticio,0,10);
        
        //EscalaZoom
        //Deixando todos os blocos <= ao valor que zoom ficticio em verde
        foreach (Image BlocoZoom in EscalaZoom){
        if(EscalaZoom.IndexOf(BlocoZoom)<= ZoomFicticio){
        BlocoZoom.color = new Color32(255,195,0,128); 
        }    
        }
        //Deixa todos os blocos > zoom fic. para branco 
        foreach (Image BlocoZoom in EscalaZoom){
        if(EscalaZoom.IndexOf(BlocoZoom)> ZoomFicticio){
        BlocoZoom.color = new Color32(255,255,255,128); 
        }    
        }

        //Cartao de Memoria 
        TextoMemoriaCartao.text = MemoriaAtual.ToString()+"/"+ MemoriaMaxima.ToString();
        
        
        //Ativando thumb na ultima foto tirada 
        UltimaFototirada.SetActive(true);
        
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position,this.transform.forward, out hit,RaycastInteracaoCamera,100,QueryTriggerInteraction.Collide)){
            Debug.DrawRay(this.transform.position,this.transform.forward*100,Color.red);
                if(hit.collider.name == "NpcTranseunte"){
                    Debug.Log("Apontando Camera para transeunte");
        }
        }
        
        //Acesso a galeria
        if(Input.GetKeyDown("g")&& FotosGuardadas.Count>0){
        Galeria =!Galeria;
        }

        if(Galeria){
        //Mostrando Foto
        PegaTransform.anchoredPosition = new Vector2(0,0);
        UltimaFototirada.transform.localScale = new Vector3(5f,4.88f,0);
        //PassandoFoto
        if(Input.GetKeyDown("a") && GaleriaFotoAtual >0){
        GaleriaFotoAtual -=1;
        }else if (Input.GetKeyDown("d") && GaleriaFotoAtual < FotosGuardadas.Count-1){
        GaleriaFotoAtual +=1;
        }
        //Coloca a foto selecionada a frente das outras
        FotosGuardadas[GaleriaFotoAtual].rectTransform.SetAsLastSibling();
        
        }else{
        //Galeria desativada
        PegaTransform.anchoredPosition = new Vector2(494,-159);
        UltimaFototirada.transform.localScale = new Vector3(1,1,0); 
        }
        
        if(TirandoFoto){
        IconeGaleria.SetActive(false);    
        }else{
        IconeGaleria.SetActive(true);
        }

        }else{
        UltimaFototirada.SetActive(false);
        
    }
    
    

}  
    

  
    
    
    void LateUpdate() {

        //Verifica se a camera ta ativa
        Camera1Pessoa = TempVar.CameraFotografia;

        if(Camera1Pessoa){    
            if(Input.GetMouseButtonDown(0)&& MemoriaMaxima>MemoriaAtual && !ProcessandoFoto ){ 
                //Chama funcao de desligar icones 
                DesligarIcones();
                ProcessandoFoto = true;

            }
        }
    }

    void DesligarIcones(){
        TirandoFoto = true;
               
        //Depois de desligar todos os icones visiveis da camera chame a foto
        InvokeRepeating("TirarFoto",0.1f,0);
    }
    void TirarFoto(){
            
                    //Coloca o screensht na textura 
                    textura = ScreenCapture.CaptureScreenshotAsTexture(1);
                    //Instancia um novo objecto do tipo raw image
                    RawImage NovaImagem = Instantiate(Imagem,new Vector2(0,0),Quaternion.identity) as RawImage;
                    //Aplica textura no novo obj
                    NovaImagem.texture = textura;
                    //Parentea a UI
                    NovaImagem.transform.SetParent(UltimaFototirada.transform);
                    //Manda a foto para o local de amostra
                    NovaImagem.transform.position = UltimaFototirada.transform.position;
                    //Escala da imagem
                    NovaImagem.transform.localScale = new Vector3(1,1,1);
                    //Adiciona foto dentro da lista de fotos 
                    FotosGuardadas.Add(NovaImagem);
                    //Depois da foto reestabeleça todos os icones ao normal 
                    MemoriaAtual+=1;
                    LigarIcones();
                    Obturador();
                    

    }
    void LigarIcones(){
        TirandoFoto = false;

    }

    void Obturador(){
        ObturadorAnim.SetTrigger("TirouFoto");
        InvokeRepeating("ProcessamentoFotoTerminado",0.5f,0);

    }

    void ProcessamentoFotoTerminado(){
        ProcessandoFoto = false;
    }
}
