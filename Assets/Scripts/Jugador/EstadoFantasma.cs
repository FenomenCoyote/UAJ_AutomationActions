using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoFantasma : MonoBehaviour {

    bool estadoFantasma;

    private void Start()
    {
        
    }

    /// <summary>
    /// metodo que se llama cuando se activa el cubo de hielo
    /// </summary>
    /// <param name="segundos"></param>
    public void ActivaEstadoFantasmaHielo()
    {
        estadoFantasma = true;
    }

    /// <summary>
    /// metodo que se llama desde el objeto con el power up
    /// </summary>
    /// <param name="segundos"></param>
    public void ActivaEstadoFantasma(float segundos)
    {
        estadoFantasma = true;
        // a los 'segundos' se desactiva la variable, usar -1 para no desactivarse automáticamente.
        if(segundos != -1) Invoke("DesactivaEstadoFantasma", segundos);

        GetComponent<FeedbackVisual>().ActivarDesactivarFeedBack(3, true);
    }

    /// <summary>
    /// Quita el estado fantasma del jugador y desactiva el feedback visual
    /// </summary>
    public void DesactivaEstadoFantasma()
    {
        estadoFantasma = false;
        GetComponent<FeedbackVisual>().ActivarDesactivarFeedBack(3, false);
    }

    /// <summary>
    /// Sirve para que los enemigos accedan a la variable estadoFantasma
    /// </summary>
    /// <returns></returns>
    public bool CogerEstadoFantasma()
    {
        return estadoFantasma;
    }
}
