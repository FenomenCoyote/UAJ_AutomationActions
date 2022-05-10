using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMov : MonoBehaviour {

    Transform mapa;
    // Use this for initialization

    private void OnTriggerEnter2D(Collider2D other)
    {
        mapa = other.gameObject.transform.parent;
    }
    private void OnTriggerStay2D(Collider2D other)
    {     
        other.gameObject.transform.parent = this.gameObject.transform; //el objeto que se encuentra sobre la plataforma se vuelve hijo de esta
                                                                       //actualmente solo el jugador (collision matrix) 
    }

    private void OnTriggerExit2D(Collider2D other)
    {  
        other.gameObject.transform.parent = mapa; // al saltar el objeto deja de estar encima y deja de ser hijo de la plataforma
    }
}
