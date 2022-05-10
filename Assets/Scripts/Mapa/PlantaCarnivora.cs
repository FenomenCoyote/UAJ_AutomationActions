using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaCarnivora : MonoBehaviour {

    public float retardo, reapertura;

    private GameObject CapulloA;
    private GameObject CapulloB;

    void Start()
    {
        CapulloA = this.gameObject.transform.GetChild(0).gameObject; ;       //Hijo1 de la base con sprite de flor abierta
        CapulloB = this.gameObject.transform.GetChild(1).gameObject; ;      //Hijo2 de la base con sprite de flor cerrado

        CapulloB.gameObject.SetActive(false); ;        //Como para poder localizarlo tiene que estar activo al encontrarlo lo desactivamos
    }

    /// <summary>
    /// Al entrar en colision con la base de la plata esta ejecuta dos metodos, uno que hará que la planta se cierre y otro que hará
    /// que despues se habra de nuevo. Ambos tiempos son configurables desde el editor
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contacts[0].normal == Vector2.down)  //Si el golpe es hacia abajo si que permite la ejecución
        {
            Invoke("AtaquePlanta", retardo);
            Invoke("DeshacerAtaquePlanta", retardo + reapertura);
            //Como ambos metodos se ejecutan a la misma vez hay sumarle al retardo del segundo el del primero para que se mantenga la diferencias
        }

    }

    //Pone uno de los sprites desactivado y activa el otro
    void AtaquePlanta()
    {
        CapulloA.gameObject.SetActive(false); ;
        CapulloB.gameObject.SetActive(true); ;
    }

    //Deshace el metodo de ataque planta, vuele a abrir la flor
    void DeshacerAtaquePlanta()
    {
        CapulloA.gameObject.SetActive(true); ;
        CapulloB.gameObject.SetActive(false); ;
    }
}