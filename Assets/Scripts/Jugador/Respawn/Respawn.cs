using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    private Transform spawn;

    /// <summary>
    /// Recoloca al jugador en el ultimo punto guardado
    /// </summary>
    /// <param name="jugador"></param>
    public void RespawnJugador(Transform other)
    {
        Vector3 posicion = new Vector3(spawn.position.x, spawn.position.y + other.transform.localScale.y*2, 0);
        other.position = posicion;   
    }

    /// <summary>
    /// Actualiza el spawnpoint de los jugadores
    /// </summary>
    /// <param name="posicionNueva"></param> nueva posicion
    /// <param name="jugador"></param>
    public void CambiaSpawn(Transform posicionNueva)
    {
        spawn = posicionNueva;
    }
}
