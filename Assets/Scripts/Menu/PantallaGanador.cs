using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PantallaGanador : MonoBehaviour {

    public Image j1, j2;

    public Sprite imagenGanador1, imagenGanador2;

    public Image textoGanador;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetGanador(Player jugador)
    {
        if(jugador == Player.jugador1)
        {
            j1.gameObject.SetActive(true);
            textoGanador.sprite = imagenGanador1;

        }
        else
        {
            j2.gameObject.SetActive(true);
            textoGanador.sprite = imagenGanador2;
        }  
    }
}
