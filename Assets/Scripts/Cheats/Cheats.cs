using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {

    public GameObject jugador;
    public Player tipoJugador;


    Transform[] coordsRespawns;
    float gravedadPorDefecto = 0;
    bool volar = false, invencibilidad = false, gravedadRecogida = false;

    /// <summary>
    /// Activa y desactiva la función incenvibilidad en el modo Cheats.
    /// </summary>
    public void Invencibilidad()
    {
        invencibilidad = !invencibilidad;
        if(CheatsManager.instance!=null)
            CheatsManager.instance.SetEstadoInvencibilidad(invencibilidad);

        if(invencibilidad && jugador.GetComponent<EstadoFantasma>()!=null)
            jugador.GetComponent<EstadoFantasma>().ActivaEstadoFantasma(-1);
        else if(jugador.GetComponent<EstadoFantasma>() != null)
            jugador.GetComponent<EstadoFantasma>().ActivaEstadoFantasma(0.01f);
    }

    /// <summary>
    /// Devuelve el estado de la invencibilidad.
    /// </summary>
    /// <returns></returns>
    public bool Getinvencibilidad()
    {
        return invencibilidad;
    }

    /// <summary>
    /// Activa y desactiva la función volar en el modo Cheats.
    /// </summary>
    public void Volar()
    {
        if (!gravedadRecogida && jugador.GetComponent<Rigidbody2D>()!=null)
        {
            gravedadPorDefecto = jugador.GetComponent<Rigidbody2D>().gravityScale;
            gravedadRecogida = true;
        }            
        volar = !volar;

        if(jugador.GetComponent<ControladorJugador>()!=null)
            jugador.GetComponent<ControladorJugador>().SetVolar(volar);

        if (volar && jugador.GetComponent<Rigidbody2D>()!=null)
            jugador.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        else if(jugador.GetComponent<Rigidbody2D>() != null)
            jugador.GetComponent<Rigidbody2D>().gravityScale = gravedadPorDefecto;
    }

    /// <summary>
    /// Método para gestionar el desplazamiento entre respawns para el modo Cheats.
    /// </summary>
    public void SiguienteRespawn()
    {
        if(GameManager.instance!=null)
            coordsRespawns = GameManager.instance.GetCoordenadasPoderes();

        int indice = 1;
        while (coordsRespawns!=null && indice + 2 < coordsRespawns.Length && jugador.transform.position.x >= coordsRespawns[indice].position.x) indice++;
        if(indice+2 < coordsRespawns.Length)
        {
            Vector3 posicion;
            if (tipoJugador == Player.jugador1)
                posicion = new Vector3(coordsRespawns[indice].localPosition.x, coordsRespawns[indice].localPosition.y + coordsRespawns[indice].transform.localScale.y * 2, 0);
            else posicion = new Vector3(coordsRespawns[indice].position.x, coordsRespawns[indice].position.y + coordsRespawns[indice].transform.localScale.y * 2, 0);
            jugador.transform.position = posicion;
        }            
    }

    /// <summary>
    /// Método para gestionar el desplazamiento hacia el último respawn en el modo Cheats.
    /// </summary>
    public void UltimoRespawn()
    {
        if (GameManager.instance != null)
            coordsRespawns = GameManager.instance.GetCoordenadasPoderes();

        if (coordsRespawns != null)
        {
            Vector3 posicion;
            int indice = coordsRespawns.Length - 2;
            if (tipoJugador == Player.jugador1)
                posicion = new Vector3(coordsRespawns[indice].localPosition.x, coordsRespawns[indice].localPosition.y + coordsRespawns[indice].transform.localScale.y * 2, 0);
            else posicion = new Vector3(coordsRespawns[indice].position.x, coordsRespawns[indice].position.y + coordsRespawns[indice].transform.localScale.y * 2, 0);
            jugador.transform.position = posicion;
        }        
    }
}
