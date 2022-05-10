using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuberia : MonoBehaviour
{
    private Vector3 posicionIni;

    /// <summary>
    /// Estas plataformas es posible colocarlas en modo espejo poniendo la escala del padre a su valor pero en negativo
    /// Es posible que se pueda quitar la comprobacion por tags ya que puede que no se pongan enemigos en este tipo de plataformas
    /// </summary>

    public float tiempoCaida, tiempoRotura, tiempoReparacion, velCaida;  //Tiempos de casting

    private bool caida = false;

    void Start()
    {
        posicionIni = transform.position;   //Guardamos la posicioninicial de la tuberia
    }

    void Update()
    {
        if (caida == true)   //El tubo se desprende y comienza la caida libre rotando un poco
        {
            transform.Translate(Vector2.down * Time.deltaTime * velCaida);
        }
    }

    /// <summary>
    /// Este metodo lo que hace es ver como se ha producido la colision (debe de ser desde arriba) y quien ha colisionado (si no es uno de los jugadores no hace nada)
    /// Tras esto pone caida a true para que se empiece a ejecutar e invoca a dos métodos: uno que desactiva la tuberia y otro que la reactivará en la posicion que tenia originalmente
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contacts[0].normal == Vector2.down && other.gameObject.CompareTag("jugador1") || other.gameObject.CompareTag("jugador2"))
        {
            Invoke("RomperTuberia", tiempoRotura);
            Invoke("DestruirObjetoTubo", tiempoCaida + tiempoRotura);             //Tiempo hasta que se ejecuta el metodo RomperTubo
            Invoke("RepararTubo", tiempoReparacion + tiempoCaida + tiempoRotura);             //El tiempo de casting será la suma de los 2 ya que se activan a la vez y así la diferencia es el tiempoReparacion original
        }
    }

    /// <summary>
    /// //Falsea caida y desactiva la tuberia
    /// </summary>
    void DestruirObjetoTubo()       
    {
        caida = false;
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Pone caida a true para que el update se empieze a ejecutar
    /// </summary>
    void RomperTuberia()
    {
        caida = true;
    }

    /// <summary>
    /// Recoloca , orienta la tuberia y la reactiva
    /// </summary>
    void RepararTubo()
    {
        transform.position = posicionIni;               //La posicion pasa a ser la inicial
        transform.rotation = Quaternion.identity;       //Ponemos la rotacion a (0,0,0)
        this.gameObject.SetActive(true);                //Se reactiva el gameObject
    }
}
