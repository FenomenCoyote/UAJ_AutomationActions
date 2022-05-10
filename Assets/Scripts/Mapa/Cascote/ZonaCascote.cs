using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaCascote : MonoBehaviour {

   private Piedra piedra;

	void Start ()
    {
        if(GetComponentInChildren<Piedra>() != null)
        {
            piedra = GetComponentInChildren<Piedra>();
        }
    }

	//cuando el jugador entra en la zona
    private void OnTriggerEnter2D(Collider2D other)
    {
        piedra.IniciaMov();
    }
}
