using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piedra : MonoBehaviour {

    public float vCaida;
    public float tDestruccion;

    Vector2 mov;
    bool destruido=false;    

	// Use this for initialization
	void Start ()
    {
        mov = new Vector2(0, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(mov * Time.deltaTime);
	}

    /// <summary>
    /// Inicia el movimiento de caída de las piedras y destruye el go transcurrido tDestruccion segundos.
    /// </summary>
    public void IniciaMov()
    {
        mov = new Vector2(0, -vCaida);

        if(!destruido) Destroy(GetComponentInParent<Transform>().gameObject, tDestruccion);
        destruido = true;
    }
}
