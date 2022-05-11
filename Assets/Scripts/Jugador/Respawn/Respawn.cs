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
        Debug.Log("Cambiamos posicion del jugador a " + posicion);
        other.position = posicion;   
    }

    /// <summary>
    /// Actualiza el spawnpoint de los jugadores
    /// </summary>
    /// <param name="posicionNueva"></param> nueva posicion
    /// <param name="jugador"></param>
    public void CambiaSpawn(Transform posicionNueva)
    {
        Debug.Log("Cambiamos posicion del spawn a " + posicionNueva);
        spawn = posicionNueva;
    }

    public Vector3 GetSpawnPosition()
    {
        return this.spawn.position;
    }
}
