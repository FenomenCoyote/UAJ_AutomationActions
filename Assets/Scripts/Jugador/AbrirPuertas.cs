using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuertas : MonoBehaviour
{
    public Player jugador; //para diferenciar los jugadores
    bool tieneLlave = false;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Cuando el jugador colisiona con la llave ponemos tieneLlave a true
        //Activamos la imagen de la llave en la UI del jugador correspondiente
        //Y destruimos la llave
        Llave llave = collision.gameObject.GetComponent<Llave>();

        if (llave != null)
        {
            GameManager.instance.EjecutarSonido("Llave", 3);
            tieneLlave = llave.CogeLlave();
            GameManager.instance.ActualizarLlave(jugador, true);
            Destroy(llave.gameObject);
        }
    }

    /// <summary>
    /// Devuelve el valor de la variable tieneLlave
    /// </summary>
    /// <returns></returns>
    public bool TieneLlave()
    {
        return tieneLlave;
    }

    /// <summary>
    /// Pone la variable tieneLlave a false
    /// Desactivamos la imagen de la llave de la UI del jugador correspondiente
    /// </summary>
    public void QuitarLlave()
    {
        tieneLlave = false;
        GameManager.instance.ActualizarLlave(jugador, false);
    }

}
