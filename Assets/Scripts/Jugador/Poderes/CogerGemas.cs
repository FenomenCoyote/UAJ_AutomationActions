using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerGemas : MonoBehaviour {

    /// <summary>
    /// Detecta la colision con el jugador, suma una gema al contador y destruye la gema
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        PoderesManager pm = other.GetComponent<PoderesManager>();
        if (pm != null)
        {
            if (pm.AñadirGemas())
            {
                GameManager.instance.EjecutarSonido("Gema", 2);
                Destroy(this.gameObject);
            }  
        }
    }
}
