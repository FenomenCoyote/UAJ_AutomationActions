using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Oscilacion : MonoBehaviour {

    Vector3 vectorRotacion;

    public float velRotacion;       //Como de rápido oscila el punto de pivote
    public float maxAngulo;         //Mitad del angulo total que describe    

    void Start ()
    {
        vectorRotacion = new Vector3(0f, 0f, velRotacion);      //Generamos vector de rotacion en eje z
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(vectorRotacion * Time.deltaTime);      //En cada frame vamos rotando el objeto    

        if (transform.rotation.z >= maxAngulo || transform.rotation.z <= -maxAngulo)        //si alcanza los valores maximos del ángulo
        {
            vectorRotacion *= (-1);     //se invierte la rotacion
        }
    }    
}
