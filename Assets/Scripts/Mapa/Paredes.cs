using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paredes : MonoBehaviour{
    public Transform paredContraria;   //Transform de la pared contraria del salto en paredes

    Muros ladoPared;

    // Use this for initialization
    void Start()
    {
        if (transform.position.x < paredContraria.position.x)  //Reconocemos si su posición X es menor que la pared contraria y asignamos su lado
            ladoPared = Muros.izquierda;
        else ladoPared = Muros.derecha;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Método para detectar que el jugador ha saltado a una pared que permite el salto en paredes.
    /// </summary>
    /// <param name="other">Jugador</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Rigidbody2D>() != null)
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;   //Reiniciamos su velocidad a 0 para que haga efecto el resto de configuraciones

        if (other.gameObject.GetComponent<ControladorJugador>() != null)
            other.gameObject.GetComponent<ControladorJugador>().EstaEnPared(true);  //Informamos al controlador de que está en una pared

        if (other.gameObject.GetComponent<SaltoParedes>() != null)
            other.gameObject.GetComponent<SaltoParedes>().SetSalto(true, ladoPared);//Configuramos el salto
    }

    /// <summary>
    /// Método para informar a SaltoParedes de que no puede saltar porque ya no está en la pared. 
    /// No informamos al controlador aún de que ya no está en pared hasta que no caiga al suelo. 
    /// Lo hacemos en CheckSalto para evitar bugs.
    /// </summary>
    /// <param name="other">Jugador</param>
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<SaltoParedes>() != null)
            other.gameObject.GetComponent<SaltoParedes>().SetSalto(false, ladoPared); //Informamos a SaltoParedes de que se ha ido de esta pared
    }
}
