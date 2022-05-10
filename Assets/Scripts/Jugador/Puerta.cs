using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Cuando la puerta colisiona con el jugador,si tiene una llave ponemos la variable tieneLlave a false 
        //y destruimos la puerta
        AbrirPuertas abrirPuertas = collision.gameObject.GetComponent<AbrirPuertas>();

        if (abrirPuertas != null && abrirPuertas.TieneLlave())
        {
            GameManager.instance.EjecutarSonido("Puerta",3);
            abrirPuertas.QuitarLlave();
            GameObject.Destroy(this.gameObject);
        }
    }
}
