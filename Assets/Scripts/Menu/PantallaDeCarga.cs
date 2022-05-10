using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaDeCarga : MonoBehaviour {

    public Sprite[] piePantallaCarga;
    public GameObject imagenCargando, jugador1, jugador2;
    public Text textoConsejo;

    private int mapa;
	
	void Start ()
    {
        textoConsejo.text = ConsejoDinamico();
	}
		
    /// <summary>
    /// Pantalla de carga Dinámica.
    /// Sección para mostrar los resultados de los jugadores.
    /// </summary>
    /// <param name="rondasJ1">Rondas ganadas J1</param>
    /// <param name="rondasJ2">Rondas ganadas J2</param>
    /// <param name="mapa">indice mapa</param>
    /// <param name="tiempoMostrarResultados">tiempo que se mostrarán los resultados</param>
    public void MostrarResultados(int rondasJ1, int rondasJ2, int mapa, float tiempoMostrarResultados)
    { 
        textoConsejo.enabled = false;
        imagenCargando.SetActive(false);

        jugador1.SetActive(true);
        jugador2.SetActive(true);

        jugador1.GetComponentInChildren<Text>().text = "" + rondasJ1;
        jugador2.GetComponentInChildren<Text>().text = "" + rondasJ2;

        this.mapa = mapa;

        Invoke("CambiaPieDeCarga", tiempoMostrarResultados/2);
    }

    /// <summary>
    /// Consejos dinámicos.
    /// Devuelve un consejo aleatorio guardado en el array para mostrar en la pantalla de carga.
    /// </summary>
    /// <returns></returns>
    string ConsejoDinamico()
    {
        int indice = Random.Range(0, 4);
        string[] consejos = new string[4];
        consejos[0] = "Consejo: No podrás salir hasta que consigas la llave...";
        consejos[1] = "Consejo: Usa tus poderes sabiamente, pero úsalos!";
        consejos[2] = "Consejo: Cuidado, hay pelotas traicioneras y mocos con mala leche..";
        consejos[3] = "Consejo: Corre, corre,corre! Gana el que antes llega a la meta!!";
        return consejos[indice];
    }

    /// <summary>
    /// Pie de carga dinámico.
    /// Cambia el pie de carga para mostrar los consejos y el mapa que se está cargando durante el tiempo de carga.
    /// </summary>
    void CambiaPieDeCarga()
    {
        textoConsejo.enabled = true;
        imagenCargando.SetActive(true);

        imagenCargando.GetComponent<Image>().sprite = piePantallaCarga[mapa - 1];
        textoConsejo.text = ConsejoDinamico();

        jugador1.SetActive(false);
        jugador2.SetActive(false);
    }
}
