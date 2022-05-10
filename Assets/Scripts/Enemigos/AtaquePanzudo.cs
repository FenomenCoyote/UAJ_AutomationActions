using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaquePanzudo : MonoBehaviour {

    public float fuerzaRebote = 100f;
    public float tiempo = 1f;

    private AudioSource audioSource;

	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rebote rebote = collision.gameObject.GetComponent<Rebote>();

        // Se usa en el if para ver si el jugador tiene el estado fantasma o no
        EstadoFantasma est = collision.gameObject.GetComponent<EstadoFantasma>();

        ContactPoint2D p = collision.GetContact(0);        
        Vector2 normal = p.normal;
        
       
        if (rebote != null && est != null && !est.CogerEstadoFantasma())
        {
            GameManager.instance.EjecutarSonido(audioSource, "Panzudo");
            //lo multiplicamos por -1 porque queremos que la fuerza se aplique en la direccion contraria al vector de la normal            
            rebote.AplicarRebote(normal * -1,fuerzaRebote,tiempo);                                     
        }        
    }
}
