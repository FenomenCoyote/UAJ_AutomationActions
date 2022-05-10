using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearCoordenadasMapa : MonoBehaviour {

    Transform[] spawns;

	// Use this for initialization
	void Start () {
        spawns = GetComponentsInChildren<Transform>();
        OrdenaCoordenadasSpawn();
    }
    /// <summary>
    /// Método para ordenar el vector de coordenadas de menor a mayor respecto al eje de las X antes de entregárselo al GM
    /// </summary>
    void OrdenaCoordenadasSpawn()
    {
        for(int x = 0; x+1 < spawns.Length; x++)
        {
            for(int j=x+1; j<spawns.Length;j++)
                if(spawns[x].position.x > spawns[j].position.x) 
                {
                    Transform trAux = spawns[x];
                    spawns[x] = spawns[j];
                    spawns[j] = trAux;
                }
        }
        //Mandamos las coordenadas al GameManager
        GameManager.instance.SetCoordenadasPoderes(spawns);
        Destroy(this);
    }
}
