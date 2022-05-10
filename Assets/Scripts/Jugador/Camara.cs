using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour {

    public Transform transformJugador;
    public float distanciaX, distanciaY; //para justar el espacio que se ve alrededor del jugador

    private void LateUpdate()
    {
        if (transformJugador != null)//si el jugador esta en escena
        {
            //la camara sigue al jugador usando su posicion mas los offsets
            transform.position = new Vector3(transformJugador.position.x + distanciaX, transformJugador.position.y + distanciaY, transformJugador.position.z-10);
        }
    }
}