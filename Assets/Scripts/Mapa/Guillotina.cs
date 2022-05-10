using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guillotina : MonoBehaviour {

    public Transform puntoArriba, puntoAbajo; //acceso a los puntos entre los que se mueve

    public float vBajada, vSubida, tEspera; //valores para configuarar el movimiento

    Vector3 movimiento;

	// Use this for initialization
	void Start ()
    {
        //el vector se inicializa a cero y despues se llama a caer que lo actualiza
        movimiento = new Vector3( 0, 0, 0); 
        Caer();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movimiento);//movimiento siguiendo el vector
        if (transform.position.y >= puntoArriba.position.y) //si llega a arriba
        {
            Parar();//frena la cuchilla
            Invoke("Caer", tEspera); //llama a caer tras esperar "tEspera" segundos
        }
        else if (transform.position.y <= puntoAbajo.position.y) //si llega a abajo
        {
            CancelInvoke(); //para evitar que a la segunda se pase de largo
            Subir(); //hace que suba
        }
    }

    /// <summary>
    /// Hace que el movimiento sea hacia arriba y con vSubida
    /// </summary>
    void Subir()
    {
        movimiento.y = -vSubida;
    }

    /// <summary>
    /// Hace que el movimiento sea hacia abajo y con vBajada
    /// </summary>
    void Caer()
    {
        movimiento.y = vBajada;
    }

    /// <summary>
    /// Pone el movimiento a cero hasta que se invoque bajar de nuevo
    /// </summary>
    void Parar()
    {
        movimiento.y = 0f;
    }

}
