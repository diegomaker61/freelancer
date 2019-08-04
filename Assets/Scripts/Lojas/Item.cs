using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    //Informacao do item
    public string NomeItem;
    public float ValorItem;
    public string DescricaoItem;
    public string Tipo;
    
    

    //Atributos de item 
    
    //flash
    public float IluminacaoMaxima;
    //Lente
    public float ZoomMaximo;
    //Cartao de memoria
    public float MemoriaMaxima;
    //Bateria
    public float BateriaMaxima;
    //Camera 
    public float ResolucaoMaxima;



    //Variaveis de compra 
    
    public float RotacaoInicial;
    public bool Avenda;
    public bool Comprado;

    //Variaveis de Inventario 
    public bool DentroInventario;
    public int SlotFinal;
    

    void Start()
    {
    RotacaoInicial = this.transform.eulerAngles.y;
    }
}

    
    
    
    
