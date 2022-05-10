using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarSpawn : MonoBehaviour {

    /// <summary>
    /// Al colisionar con el jugador, actualiza el spawnpoint de respawn
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Respawn>()!=null)
            other.gameObject.GetComponent<Respawn>().CambiaSpawn(this.transform);
     }
}
