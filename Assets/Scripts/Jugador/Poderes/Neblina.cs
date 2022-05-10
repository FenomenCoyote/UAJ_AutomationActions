using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neblina : MonoBehaviour {

    public float cantidadRalentizar;

    /// <summary>
    /// Método para detectar que se ha entrado en el campo de la Neblina
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PerdidasControl>() != null)
            other.GetComponent<PerdidasControl>().ActivaModificaVelocidad(cantidadRalentizar, 1);   //En caso de entrar en la Neblina activa la modificación de velocidad con la cantidad a ralentizar
    }

    /// <summary>
    /// Método para detectar que se abandona el campo de la Neblina
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PerdidasControl>() != null)
            other.GetComponent<PerdidasControl>().DesactivaModificaVelocidadNeblina();  //En caso de salir de la Neblina desactiva la reducción de velocidad.
    }
}
