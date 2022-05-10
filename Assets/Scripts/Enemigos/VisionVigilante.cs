using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionVigilante : MonoBehaviour {
    public float reduccionVelVigilante;
    public float maxAngulo, minAngulo, varAngulo;

    Vector3 vectorRotacion;

    // Use this for initialization
    void Start ()
    {
        vectorRotacion  = new Vector3 (0f, 0f, varAngulo);
    }
	
	// Update is called once per frame
	void Update ()
    {        
        transform.Rotate(vectorRotacion); //el collider esta rotando continuamente       

        if(transform.rotation.z >= maxAngulo || transform.rotation.z <= minAngulo) //si alcanza los valores maximos de amplitud
        {
            vectorRotacion *= (-1);//se invierte la rotacion
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        EstadoFantasma est = other.GetComponent<EstadoFantasma>();

        // Llamamos al ralentizar
        PerdidasControl pc = other.GetComponent<PerdidasControl>();
        if (pc != null && est != null && !est.CogerEstadoFantasma()) // Si no esta en modo fantasma
        {
            pc.ActivaModificaVelocidad(reduccionVelVigilante, 7);
        }
        
        if (this.gameObject.GetComponentInParent<SeguirObjetivo>() != null && est != null)  //Si este enemigo dispone de la capacidad de seguir al jugador
        {
            SeguirObjetivo so = this.gameObject.GetComponentInParent<SeguirObjetivo>();                                     
            if (!est.CogerEstadoFantasma() && so.PuedeIniciarSeguimiento())
                so.SigueAlJugador(other.transform);        //Si no está en modo fantasma y el enemigo puede iniciar un seguimiento
        }                                                  //Autorizamos el seguimiento al jugador y pasamos su transform para gestionar su posición
    }   
}
