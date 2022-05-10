using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MejoraPico : MonoBehaviour {

    public float segundos = 5f; //tiempo que dura el powerUp
    public int dañoModificado; //daño que aumentamos

    /// <summary>
    /// Cuando colisiona con el power up incrementa el daño de picado
    /// durante unos segundos, realizando un feedback visual
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
         RomperParedes romper = collision.GetComponent<RomperParedes>();

         if (romper != null)
         {
            GameManager.instance.EjecutarSonido("PowerUp", 1);
            romper.IncreaseDamage(dañoModificado, segundos);
            Destroy(this.gameObject);
         }
     }
}
