using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubitoHielo : MonoBehaviour {

    //golpes necesarios para romper el bloque de hielo
    public int GolpesParaRomper;

    //variables de acceso a otros componentes
    RomperParedes picar;
    PoderesManager pm;

    AudioSource audioSource;

    //numero de golpes que ya ha recibido el cubo de hielo
    int GolpesRecibidos;

	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();

        GolpesRecibidos = 0; // al principio aun no ha recibido ningun golpe

        //acceso a Romper Paredes para usar la misma tecla
        if (this.gameObject.GetComponentInParent<RomperParedes>() != null)
        {
            picar = this.gameObject.GetComponentInParent<RomperParedes>();
        }

        //acceso a PerdidasControl para llamar a activar/desactivar el cubo de hielo
        if (this.gameObject.GetComponentInParent<PoderesManager>() != null)
        {
            pm = this.gameObject.GetComponentInParent<PoderesManager>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //si se pulsa la tecla de picar el cubo recibe un golpe
        if (Input.GetKeyDown(picar.GetTeclaRomperParedes()))
        {
            GameManager.instance.EjecutarSonido(audioSource,"Picar");
            GolpesRecibidos++;
        }

        //si se alcanza el numero de bloques necesarios se desactiva el cubo y se destruye
        if (GolpesRecibidos >= GolpesParaRomper)
        {
            GameManager.instance.EjecutarSonido("DestruirHielo", 3);
            pm.DesactivarCuboDeHielo();
            Destroy(this.gameObject);
        }
	}
}
