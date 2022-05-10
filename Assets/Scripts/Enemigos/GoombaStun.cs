using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaStun : MonoBehaviour {

    public float segundos;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
/// <summary>
    /// Este metodo se llama si el enemigo goomba colisiona por trigger con algun otro objeto
    /// </summary>
    /// /// <param name="other"></param> El que ha colisionado con el enemigo
    void OnTriggerEnter2D(Collider2D other)
    {
        // Se usa en el if para ver si el jugador tiene el estado fantasma o no
        EstadoFantasma est = other.GetComponent<EstadoFantasma>();

        PerdidasControl pc = other.GetComponent<PerdidasControl>();
        if (pc != null && est != null && !est.CogerEstadoFantasma())
        {
            GameManager.instance.EjecutarSonido(audioSource, "Goomba");
            pc.DesactivaControles(segundos, 0);
        }
    }
}
