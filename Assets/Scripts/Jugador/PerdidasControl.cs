using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerdidasControl : MonoBehaviour {

 
    //segundos que dura la modificacion de velocidad
    public float segundosModificaVelocidad;

    PerdidaControles estadoActual;
    ControladorJugador controles;
    int casoFeedBack;
    
    private void Start()
    {
        //acceso al controlador jugador para llamar al switch
        if(this.gameObject.GetComponent<ControladorJugador>()!=null)
            controles = this.gameObject.GetComponent<ControladorJugador>();        
    }

    /// <summary>
    /// cambia el estado de los controles a true
    /// </summary>
    public void ActivaControles()
    {
        if (estadoActual != PerdidaControles.enCubo)
            controles.SetEstadoControlador(true);    
        GetComponent<FeedbackVisual>().ActivarDesactivarFeedBack(casoFeedBack, false);
    }

    /// <summary>
    /// cambia el estado de los controles a false
    /// </summary>
    /// <param name="segundos"></param>
    public void DesactivaControles(float segundos, int caso)
    {
        //desactiva los controles
        controles.ReseteaStats();
        controles.SetEstadoControlador(false);        
        //los vuelve a activar pasados "segundos" segundos. Utilizamos el valor (-1) para indicar que no los vuelva a activar porque se hará sin invoke cuando corresponda.
        if(segundos!=-1) Invoke("ActivaControles", segundos);

        GetComponent<FeedbackVisual>().ActivarDesactivarFeedBack(caso, true);

        //puede sobreescribirse si ocurren dos cc's a la vez
        casoFeedBack = caso;
    }

    /// <summary>
    /// modifica la velocidad
    /// </summary>
    /// <param name="velocidadModificada"></param>
    public void ActivaModificaVelocidad(float velocidadModificada, int caso)
    {
        //modifica la velocidad multiplicandola por "velocidadModificada"
        controles.ModificaVelocidad(velocidadModificada);
        if (caso != 1)
        {
            //la devuelve a su valor normal pasados "segundosModificaVelocidad" segundos en caso de que no sea la Neblina
            Invoke("DesactivaModificaVelocidad", segundosModificaVelocidad);
            GetComponent<FeedbackVisual>().ActivarDesactivarFeedBack(7, true);
        }

        else GetComponent<FeedbackVisual>().ActivarDesactivarFeedBack(1, true);
    }

    /// <summary>
    /// restaura la velocidad del jugador
    /// </summary>
    public void DesactivaModificaVelocidad()
    {
        controles.RestauraVelocidad();
        GetComponent<FeedbackVisual>().ActivarDesactivarFeedBack(7, false);
    }
    /// <summary>
    /// Desactiva el feedback correspondiente a la neblina
    /// </summary>
    public void DesactivaModificaVelocidadNeblina()
    {
        controles.RestauraVelocidad();
        GetComponent<FeedbackVisual>().ActivarDesactivarFeedBack(1, false);
    }

    public void SetEstado(PerdidaControles estado)
    {
        estadoActual = estado;
    }

    public PerdidaControles GetEstadoActual()
    {
        return estadoActual;
    }
}