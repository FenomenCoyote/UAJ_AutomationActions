using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltoParedes : MonoBehaviour
{
    public float fuerzaSalto, x, y, velocidadBajarsePared;                  //Fuerza con la que saltará.X,y: datos para el vector de dirección. Velocidad para salir de la pared
    
    Rigidbody2D rb;
    Vector2 direccion;
    Muros pared;
    KeyCode teclaSaltoParedes, teclaIzqdaParedes, teclaDchaParedes;             //Teclas de movimiento en salto en paredes

    float gravedadPorDefecto;                                                  //Variable para almacenar la gravedad que gestiona ControladorJugador
    bool puedeSaltarParedes = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("SetGravedadPorDefecto", 0.2f);        //Invocamos a los 0.2 segundos ya que se tarda un poco en calcular la gravedad por defecto del jugador
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(teclaSaltoParedes) && puedeSaltarParedes)        //Si salta y está autorizado el saltar paredes activamos el salto
        {
            puedeSaltarParedes = false;
            SaltoPared();
        }

        if (Input.GetKeyDown(teclaDchaParedes) && puedeSaltarParedes && pared == Muros.izquierda)      //Si el jugador se mueve a la derecha en la parez izquierda gestionamos la bajada
            rb.velocity = new Vector2(velocidadBajarsePared, 0);                              //Aplicamos una velocidad en X para separar al jugador de la pared

        if (Input.GetKeyDown(teclaIzqdaParedes) && puedeSaltarParedes && pared == Muros.derecha)      //Si el jugador se mueve a la izquierda en la parez derecha gestionamos la bajada
            rb.velocity = new Vector2(-velocidadBajarsePared, 0);                             //Aplicamos una velocidad en X para separar al jugador de la pared
    }

    /// <summary>
    /// Método para autorizar y configurar el salto en paredes. Se activa cuando el jugador salta hacia una pared que permite el salto en paredes
    /// </summary>
    /// <param name="puede">Autoriza o no el salto</param>
    /// <param name="lado">Configura el lado de la pared en la que ha saltado el jugador</param>
    public void SetSalto(bool puede, Muros lado)
    {
        if (!GetComponent<ControladorJugador>().GetVolar())
        {
            if (puede) rb.gravityScale = 0.1f;                                                    //Si se autoriza el salto es que está apoyado en la pared, configuramos gravedad a 0.1 para efecto de resbalar
            else rb.gravityScale = gravedadPorDefecto;                                            //En caso de negar la autorización devolvemos la gravedad a su estado

            pared = lado;                                                                         //Guardamos el lado en el que está para saber en qué dirección empujar en caso de activar salto
            puedeSaltarParedes = puede;
        }                                                              //Autorizamos o no el salto en paredes en función de los parámetros de entrada
    }

    //Estos metodos establecen las teclas respecto a los controles del salto en paredes
    public void SetTeclaSaltoParedes(KeyCode nuevaTecla)
    {
        teclaSaltoParedes = nuevaTecla;
    }
    public void SetTeclaIzqdaParedes(KeyCode nuevaTecla)
    {
        teclaIzqdaParedes = nuevaTecla;
    }
    public void SetTeclaDchaParedes(KeyCode nuevaTecla)
    {
        teclaDchaParedes = nuevaTecla;
    }

    /// <summary>
    /// Método para realizar el salto en paredes del jugador.
    /// </summary>
    void SaltoPared()
    {
        if (pared == Muros.izquierda) direccion = new Vector2(x, y);                          //Si la pared es la izquierda, la dirección X del salto será positiva
        else direccion = new Vector2(-x, y);                                                  //Si la pared es la derecha, la dirección X del salto será negativa
        rb.AddForce(direccion * fuerzaSalto, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Método para almacenar el valor por defecto de la gravedad del jugador calculada por ControladorJugador
    /// </summary>
    void SetGravedadPorDefecto()
    {
        gravedadPorDefecto = rb.gravityScale;
    }
}
