using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneraTroncos : MonoBehaviour {

    //periodo de tiempo entre generacion y generacion
    public float periodo;
    //referencia al prefab
    public GameObject tronco;

    private Transform tran;
   
    void Start ()
    {
        tran = transform;
        //Genera 1 tronco cada periodo segundos
        InvokeRepeating("GeneraTronco", 1, periodo);        
	}

	/// <summary>
    /// hace un Instantiate del tronco en la posicion del objeto generador
    /// </summary>
    void GeneraTronco()
    {
        Instantiate(tronco, tran);
    }
}
