using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJugador : MonoBehaviour
{    
    public int alturaSalto, distanciaRecorrida;
    public float VelocidadRodar, velocidadX; //cantidad de velocidad reducida en % (de 0 a 1)
    public string axisHorizontal, axisVertical;
    public float minDrag = 0, maxDrag = 8; //variables para controlar el LinearDrag del rigidbody

    BoxCollider2D colliderCorre;
    CircleCollider2D colliderRueda;
    Rigidbody2D rb;
    AudioSource audioSource;
    Animator anim;
    KeyCode teclaRodar, teclaSaltar;
    float deltaX, deltaY, g, velocidadY, velocidadEstandar;       //velocidadEstandar = variable auxiliar donde guardamos la velocidad original
    bool salto, estadoControles = true, rodando, puedeSaltar,
         enTubería = false, enPared = false, movHorizontal = false,enSuelo, volar = false;

    // Use this for initialization
    void Start()
    {
        g = (-2 * alturaSalto * velocidadX * velocidadX) / ((distanciaRecorrida / 2) * (distanciaRecorrida / 2));   //gravedad calculada
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = g / Physics2D.gravity.y;                                                                  //gravedad del jugador
        velocidadY = (2 * alturaSalto * velocidadX) / (distanciaRecorrida / 2);                                             //velocidad del salto

        colliderCorre = GetComponent<BoxCollider2D>();
        colliderRueda = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        velocidadEstandar = velocidadX;

        salto = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (estadoControles)
        {
            //RODAR
            if (colliderCorre != null && colliderRueda != null)
            {
                //si pulsamos tecla
                if (Input.GetKeyDown(teclaRodar))
                {
                    rodando = true;
                    colliderCorre.enabled = false;
                    colliderRueda.enabled = true;
                    ModificaVelocidad(VelocidadRodar);
                }
                else if (Input.GetKeyUp(teclaRodar) && !enTubería)
                {
                    rodando = false;
                    colliderCorre.enabled = true;
                    colliderRueda.enabled = false;
                    velocidadX = velocidadEstandar;                    
                }

                // AnimacionRodar 
                anim.SetBool("Rodando", rodando);
            }

            //flip del personaje
            if (Input.GetAxisRaw(axisHorizontal) > 0)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else if(Input.GetAxisRaw(axisHorizontal) < 0)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
           
            //animacion de moverse
            bool parado = (Input.GetAxisRaw(axisHorizontal) == 0);
            anim.SetBool("Moviendose", !parado);
            
            //mov horizontal
            if (Input.GetAxis(axisHorizontal) != 0 && !enPared)
            {
                deltaX = Input.GetAxisRaw(axisHorizontal);
                movHorizontal = true;
                rb.drag = minDrag; //si se mueve el LinearDrag lo ponemos al minimo

                //sonido de moverse
                if (!audioSource.isPlaying && puedeSaltar)
                {
                    audioSource.loop = true;
                    GameManager.instance.EjecutarSonido(audioSource, "Moverse");
                }
            }
            else
            {
                audioSource.loop = false; //se pone el loop a false para que el sonido no se ejecute tras pararse
                movHorizontal = false;
                rb.drag = maxDrag; //cuando se para el LinearDrag lo ponemos al maximo para que no se deslice
            }

            //salto
            if (Input.GetKey(teclaSaltar) && puedeSaltar && !volar)
            {
                rb.drag = minDrag; // al saltar poemos el LinearDrag al minimo
                salto = true;
                puedeSaltar = false;
                enSuelo = false;
                audioSource.loop = false;
                GameManager.instance.EjecutarSonido(audioSource, "Salto");
            }

            if (Input.GetKey(teclaSaltar) && volar == true)
            {                
                deltaY = 0.3f;
            }
            else if (Input.GetKey(teclaRodar) && volar == true)
            {
                deltaY = -0.3f;
            }
            else
            {
                deltaY = 0;
            } 

            //si esta parado y salta ponemos el LinearDrag al minimo
            if ((!puedeSaltar && !movHorizontal) || !enSuelo) rb.drag = minDrag;

            //animacion de salto
            anim.SetBool("Saltando", salto); 
        }
    }
    
    public void SetVolar(bool est)
    {
        volar = est;
    }

    public bool GetVolar()
    {
        return volar;
    }

    public void SetTeclaRodar(KeyCode nuevaTecla)
    {
        teclaRodar = nuevaTecla;
    }

    public void SetTeclaSaltar(KeyCode nuevaTecla)
    {
        teclaSaltar = nuevaTecla;
    }

    /// <summary>
    /// Setter para gestionar el estado funcional del controlador.
    /// </summary>
    public void SetEstadoControlador(bool est)
    {
        estadoControles = est;
    }

    /// <summary>
    /// Metodo que se llama desde checksalto cuando el jugador toca el suelo
    /// </summary>
    public void ActivaPuedeSaltar()
    {
        puedeSaltar = true;
        enSuelo = true;
    }

    /// <summary>
    /// Metodo que se llama desde checksalto cuando el jugador deja de tocar el suelo
    /// </summary>
    public void DesactivaEnSuelo()
    {
        enSuelo = false;
    }

    /// <summary>
    /// Método que se utiliza para comunicar al controlador si está en pared o no
    /// </summary>
    /// <param name="estado"></param>
    public void EstaEnPared(bool estado)
    {
        enPared = estado;
    }

    /// <summary>
    /// Cambia el estado de ver si esta o no entre paredes
    /// </summary>
    /// <param name="check"></param>
    public void CheckDejarRodar(bool check)
    {
        enTubería = check;
    }

    /// <summary>
    /// Pone al jugador de pie trás pasar una tubería si este no esta presionando
    /// la tecla de rodar
    /// </summary>
    public void PonerDePie()
    {
        if (!Input.GetKey(teclaRodar)) //si no presiona la tecla rodar
        {
            //Restauramos al jugador como si corriera
            rodando = false;
            colliderCorre.enabled = true;
            colliderRueda.enabled = false;
            RestauraVelocidad();
        }
    }

    /// <summary>
    /// Gestión de la velocidad del avatar
    /// </summary>
    /// <param name="cambioVelocidad">multiplicador de velocidad</param>
    public void ModificaVelocidad(float cambioVelocidad)
    {
        velocidadX = cambioVelocidad * velocidadEstandar;
    }

    public void ModificaVelocidadEstandar(float cambioVelocidad)
    {
        velocidadEstandar *= cambioVelocidad;
    }

    /// <summary>
    /// Aumenta la velocidadX durante un tiempo e invoca a RestauraVelocidad tras el tiempo "duracion".
    /// </summary>
    /// <param name="cantidad">cantidad de velocidad que aumentamos a velocidadX</param>
    /// <param name="duracion">tiempo que dura el aumento</param>
    public void AumentaVelocidad(float cantidad, float duracion)
    {
        velocidadX += cantidad;
        Invoke("RestauraVelocidad", duracion);
        GetComponent<FeedbackVisual>().ActivarDesactivarFeedBack(5, true);
    }

    /// <summary>
    /// Velocidad en X vuelve a su estado original
    /// </summary>
    public void RestauraVelocidad()
    {
        velocidadX = velocidadEstandar;
        GetComponent<FeedbackVisual>().ActivarDesactivarFeedBack(5, false);
    }

    /// <summary>
    /// Método para resetear valores de movimiento y pone el loop del AS a false para
    /// que no se ejecute el sonido de caminar tras morir.
    /// Tambien hacemos que la animaciones de moverse,rodar y saltar paren y ejecutamos la de idle
    /// para que tras ser estuneado o "morir" solo se ejecute la de idle
    /// </summary>
    public void ReseteaStats()
    {
        deltaX = 0;
        //deltaY = 0;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        audioSource.loop = false;
        salto = false;
        anim.SetBool("Saltando", salto);
        anim.SetBool("Moviendose", false);
        anim.Play("Parado");
        rodando = false;
        anim.SetBool("Rodando", rodando);
    }

    /// <summary>
    /// Método para cambiar el input de las teclas
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    public void SwapTeclas()
    {
        KeyCode aux;
        aux = teclaRodar;
        teclaRodar = teclaSaltar;
        teclaSaltar = aux;
    }


    private void FixedUpdate()
    {
        //salto
        if (salto && !volar)
        {
            rb.velocity = new Vector2(rb.velocity.x, velocidadY);
            salto = false;
        }

        if (volar && CheatsManager.instance.GetEstadoCheats())
        {
            rb.velocity = new Vector2(rb.velocity.x, velocidadY * deltaY);
        }/*
        else if(!volar && CheatsManager.instance.GetEstadoCheats())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }*/

        //mov horizontal
        if (movHorizontal && !enPared)
            rb.velocity = new Vector2(deltaX * velocidadX, rb.velocity.y);
    }
}
