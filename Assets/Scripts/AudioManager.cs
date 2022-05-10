using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sonido
{
   public string nombre;
   public AudioClip sonido;
}

public class AudioManager : MonoBehaviour {

    public AudioSource powerUps;
    public AudioSource gemas;
    public AudioSource otros;

    [SerializeField]
    public Sonido[] sonidos;

    void Start ()
    {
        GameManager.instance.SetAudioManager(this);
	}
			
    /// <summary>
    /// Ejecuta un sonido.Sirve para objetos que no se destruyen(Jugador,enemigos,amenazas...)
    /// </summary>
    /// <param name="audioSource">el audioSource del objeto</param>
    /// <param name="nombreSonido">identificador del sonido a ejecutar</param>
    public void EjecutarSonido(AudioSource audioSource,string nombreSonido, float volumen)
    {
        audioSource.volume = volumen;
        audioSource.clip = BuscarSonido(nombreSonido);
        audioSource.Play();               
    }
  
    /// <summary>
    /// Ejecuta un sonido.Sirve para los objetos que se destruyen(PowerUps,Gemas...)
    /// </summary>
    /// <param name="nombreSonido">identificador del sonido a ejecutar</param>
    /// <param name="eleccion">para elegir el audioSource en el que se va ejecutar el sonido
    /// (Mirar hijos del AudioManager)</param>
    public void EjecutarSonido(string nombreSonido,int eleccion, float vol)
    {
        powerUps.volume = vol;
        gemas.volume = vol;
        otros.volume = vol;
        if (eleccion == 1)
        {
            powerUps.clip = BuscarSonido(nombreSonido);
            powerUps.Play();
        }
        else if (eleccion == 2)
        {
            gemas.clip = BuscarSonido(nombreSonido);
            gemas.Play();
        }
        else if(eleccion == 3)
        {
            otros.clip = BuscarSonido(nombreSonido);
            otros.Play();
        }      
    }
    
    /// <summary>
    /// Busca el sonido que queramos ejecutar
    /// </summary>
    /// <param name="identificador">nombre del sonido para encontrarlo</param>
    /// <returns></returns>
    AudioClip BuscarSonido(string identificador)
    {
        AudioClip sonido = null;

        int i = 0;
        bool encontrado = false;
        while (i < sonidos.Length && !encontrado)
        {
            if(sonidos[i].nombre == identificador)
            {
                sonido = sonidos[i].sonido;
                encontrado = true;
            }
            i++;
        }
        return sonido;
    }
}
