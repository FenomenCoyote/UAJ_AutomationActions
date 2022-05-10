using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMundos : MonoBehaviour
{
    public GameObject mundo;
    public Transform jugador;
    public Mundos tipoMundo;

    // Use this for initialization
    void Start()
    {
        InformaGameManager();
    }
    /// <summary>
    /// Método para informar a GameManager de la relación mundo y jugador y que este tenga control sobre ellos.
    /// </summary>
    void InformaGameManager()
    {
        if (jugador != null && mundo != null) GameManager.instance.SetMundoYJugador(tipoMundo, mundo, jugador);
        //Destruimos todas las referencias públicas tras el informe para evitar que estén en memoria innecesariamente. 
        //GameManager es el único que necesita conocerlos.
        Destroy(this);     
    }
}