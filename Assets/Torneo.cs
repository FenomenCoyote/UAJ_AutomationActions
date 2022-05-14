using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torneo : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("IniciaTorneo", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void IniciaTorneo()
    {
        GameManager.instance.InicializaTorneo();
    }
}
