using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MejoraFantasma : MonoBehaviour {

    public float duracionFantasma;

    /// <summary>
    /// Cuando el jugador colisione con el objeto PowerUp, le aplica el power up de fantasma durante duracionFantasma segundos
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EstadoFantasma estadoFantasma = collision.GetComponent<EstadoFantasma>();

        if (estadoFantasma != null)
        {
            GameManager.instance.EjecutarSonido("PowerUp", 1);
            estadoFantasma.ActivaEstadoFantasma(duracionFantasma);
            Destroy(this.gameObject);  
        }
    }
}
