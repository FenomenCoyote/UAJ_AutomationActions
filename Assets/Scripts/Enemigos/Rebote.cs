using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebote : MonoBehaviour {   

    private Rigidbody2D rb2d;
    private ControladorJugador controlador;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        controlador = GetComponent<ControladorJugador>();
    }
  
    /// <summary>
    /// Desactiva los controles del jugador(ControladorJugador),le aplica una fuerza y los vuelve a activar pasado un tiempo
    /// </summary>
    /// <param name="vector">vector que almacena el punto de contacto entre el jugador y el enemigo</param>
    /// <param name="fuerzaRebote">fuerza con la que rebota el jugador</param>
    /// <param name="tiempo">tiempo que estan los controles desactivados</param>
    public void AplicarRebote(Vector2 vector,float fuerzaRebote,float tiempo)
    {
        controlador.enabled = false; //desactiva los controles
        rb2d.AddForce(vector * fuerzaRebote,ForceMode2D.Impulse); //aplica el impulso
        Invoke("ActivarMov", tiempo); //reactiva los controles pasado "tiempo" tiempo
    }

    /// <summary>
    /// Activa los controles del jugador(ControladorJugador)
    /// </summary>
    void ActivarMov()
    {
        controlador.enabled = true;
    }

}
