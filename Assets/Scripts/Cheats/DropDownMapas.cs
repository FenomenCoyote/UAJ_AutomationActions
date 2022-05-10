using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownMapas : MonoBehaviour {

    public Dropdown dropDownCheats;
    public Button botonCargarMapa;

	// Use this for initialization
	void Start () {
        ConfiguraDropDownCheats();
	}

    /// <summary>
    /// Configura el índice del mapa a cargar según que mapa se ha elegido en el drop down.
    /// </summary>
    /// <param name="indice"></param>
    public void SetIndiceMapa(int indice)
    {
        botonCargarMapa.enabled = true;
        if (GameManager.instance != null)
            GameManager.instance.SetIndiceMapa(indice + 1);        
    }

    /// <summary>
    /// Inicializa la carga de mapa. Se desactivará el botón hasta que se cambie el índice de nuevo
    /// para evitar posibles spams involuntarios en el botón de cargar que puedieran crear malas interacciones.
    /// </summary>
    public void CargarMapa()
    {
        botonCargarMapa.enabled = false;
        if(GameManager.instance!=null)
            GameManager.instance.CargaMapaEnMundos();
    }

    /// <summary>
    /// Configura el Drop Down de mapas para los cheats.
    /// </summary>
    void ConfiguraDropDownCheats()
    {
        if (dropDownCheats != null)
        {
            dropDownCheats.ClearOptions();
            List<string> opciones = new List<string>();
            for (int x = 1; x <= 3; x++) opciones.Add("Mapa " + x);

            dropDownCheats.AddOptions(opciones);
            dropDownCheats.value = 0;
            dropDownCheats.RefreshShownValue();
        }
    }
}
