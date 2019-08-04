using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Venda : MonoBehaviour
{
    
    
    public Camera CameraCompra;
    public GameObject Jogador;
    GameObject Entrada;
    bool PossibilidadeCompra;
    bool Comprando;
    ControladorJogador tempControladorJogador;
    public Camera Camera3Pessoa;
    public GameObject SeletorCompra;
    public int IndiceItem;
    public GameObject FundoCompra;
    public List<GameObject> ItensVenda ;
    ControleUIPadrao tempControleUIPadrao;
    public Canvas UIPadrao;
    public float ValorItemSelecionado;
    Vector3 SeletorLocalInicial;
    Inventario tempInventario;
    public GameObject Inventario; 
    string TipoItem;
    Item PegaItem;
    public GameObject[] InventarioSlot;


    //Componente UI 
    public TextMeshProUGUI NomeItem;
    public TextMeshProUGUI DescricaoItem;
    public TextMeshProUGUI ValorItem;

    Renderer RenderItem;
    Item tempItem ;




    
   

    void Start()
    {
        tempControladorJogador = Jogador.GetComponent<ControladorJogador>();
        tempControleUIPadrao = UIPadrao.GetComponent<ControleUIPadrao>();
        tempInventario = Inventario.GetComponent<Inventario>();
        //Registra posicao inicial do seletor
        SeletorLocalInicial = SeletorCompra.transform.position;
        
        
        
    }
    void OnTriggerEnter(Collider other) {
    if(other.gameObject.name =="Jogador"){
        PossibilidadeCompra = true;
    }
    }

    void OnTriggerExit(Collider other){
    if(other.gameObject.name=="Jogador"){
        PossibilidadeCompra = false;
    }   
    }

    
    void Update()
    {   

        
        if(Input.GetMouseButtonDown(0)&& Comprando){
            //Pega o script do item selecionado
            PegaItem = ItensVenda[IndiceItem].GetComponentInChildren<Item>();
               
                if(!PegaItem.Comprado){

                    if(tempControleUIPadrao.ValorDinheiro>=ValorItemSelecionado){
                        ComprouItem();
                        PegaItem.Comprado=true;
                    }else{
                    Debug.Log("Voce nao tem dinheiro");   
                    }
                }else{
                Debug.Log("Voce ja tem esse item");
                }
        }
              
        
            



        //Saindo da compra 
         if(Input.GetMouseButtonDown(1) && Comprando ){
            //Desativando Compra 
            Comprando =false;
            //desativa camera compra 
            CameraCompra.enabled = false;
            //ativa camera 3 pessoa
            Camera3Pessoa.enabled = true;
            //ativa controle do jogador 
            tempControladorJogador.ControleJogador = true;
            // desativa interface de compra 
            FundoCompra.SetActive(false);
            //Coloca o seletor no seu local inicial
            SeletorCompra.transform.position = SeletorLocalInicial;
        }

        //Possibilidade de compra ativa 
        if(PossibilidadeCompra){
            if(Input.GetMouseButtonDown(0) && !Comprando){
                Comprando = true ;
        }    
        }

        if(Comprando){

            

            //Desativa controle do jogador
            tempControladorJogador.ControleJogador = false;
            //Desativa camera 3 pessoa  
            Camera3Pessoa.enabled = false;
            //Ativa camera de Compra 
            CameraCompra.enabled = true;
            //Olhe para o seletor
            CameraCompra.transform.LookAt(SeletorCompra.transform);
            //Coloque o seletor no local do indice 
            SeletorCompra.transform.position = Vector3.Lerp(SeletorCompra.transform.position,ItensVenda[IndiceItem].transform.position,5*Time.deltaTime);
            //Ligue a interface de compra 
            FundoCompra.SetActive(true);
            
            //Pegando valor do item selecionado 
            //tempItem = ItensVenda[IndiceItem].GetComponent<Item>();
            //RenderItem = ItensVenda[IndiceItem].GetComponent<Renderer>();

            tempItem = ItensVenda[IndiceItem].GetComponentInChildren<Item>();
            RenderItem = ItensVenda[IndiceItem].GetComponentInChildren<Renderer>();


            //Passando valor do item em float 
            ValorItemSelecionado = tempItem.ValorItem;
            
            //Passando para UI
            NomeItem.text = tempItem.NomeItem;
            DescricaoItem.text = tempItem.DescricaoItem;
            ValorItem.text = tempItem.ValorItem.ToString();

            //Item Selecionado
            //ItensVenda[IndiceItem].transform.Rotate(Vector3.up,50*Time.deltaTime);
            RenderItem.material.SetFloat("_Shininess",0.01f);

            Transform MovaChild = ItensVenda[IndiceItem].transform.GetChild(0);
            MovaChild.transform.Rotate(Vector3.up,50*Time.deltaTime);
            

            //Item Nao Selecionado 
            foreach (GameObject ItemAvenda in ItensVenda){
                if(IndiceItem != ItensVenda.IndexOf(ItemAvenda)){
                    Renderer DentroForeach = ItemAvenda.GetComponentInChildren<Renderer>();
                    Transform t = ItemAvenda.transform.GetChild(0);
                    Item tempForeach = ItemAvenda.GetComponentInChildren<Item>();
                    t.transform.eulerAngles = new Vector3(0,tempForeach.RotacaoInicial,0);
                    DentroForeach.material.SetFloat("_Shininess",1);
            }    
            }



            //Transicao entre um item e outro
            if(Input.GetKeyDown("d") && IndiceItem<ItensVenda.Count-1){
                IndiceItem+=1;    
            }else if(Input.GetKeyDown("a") && IndiceItem >0){
                IndiceItem-=1;    
            }




        }  
         
    }
     void ComprouItem(){
        //Subtrai o dinheiro do jogador
        tempControleUIPadrao.ValorDinheiro -= ValorItemSelecionado;
        //Informa ao jogador oq ele comprou
        Debug.Log(" Voce Comprou " + ItensVenda[IndiceItem].name);
        //Crie o mesmo objeto no centro da cena
        GameObject NovoItem = Instantiate(ItensVenda[IndiceItem],new Vector3(0,0,0),Quaternion.identity) as GameObject;
        //Pegando Script do novo item
        Item tempItem = NovoItem.GetComponentInChildren<Item>();
        tempItem.Avenda = false;
        tempItem.DentroInventario = true;
        Transform t = NovoItem.transform.GetChild(0);
            if(tempItem.Tipo == "Flash"){
                tempInventario.Flashs.Add(NovoItem);
                NovoItem.transform.position = InventarioSlot[tempItem.SlotFinal].transform.position;
                t.transform.localEulerAngles = new Vector3(0,0,0);

            }
            if(tempItem.Tipo == "Bateria"){
                tempInventario.Baterias.Add(NovoItem);
            }
            if(tempItem.Tipo == "CameraFotografica"){
                tempInventario.CamerasFotograficas.Add(NovoItem);
                NovoItem.transform.position = InventarioSlot[tempItem.SlotFinal].transform.position;
                t.transform.localPosition=new Vector3(0,0,0);
                t.transform.localEulerAngles = new Vector3(0,0,0);

                    
            }
            if(tempItem.Tipo =="Lente"){
                tempInventario.Lentes.Add(NovoItem);
                NovoItem.transform.position = InventarioSlot[tempItem.SlotFinal].transform.position;
                t.transform.localEulerAngles = new Vector3(0,0,0);
                    


            }

            
        }
}
