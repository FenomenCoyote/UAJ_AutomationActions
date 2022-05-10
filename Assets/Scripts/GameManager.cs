using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject mapPrefab, mapPrefab2, mapPrefab3;    

    UIManager ui;
    AudioManager audioManager;
    AudioSource musicaManagerJ1, musicaManagerJ2;
    GameObject mundoJ1, mundoJ2, mapaJ1, mapaJ2;
    Transform[] coordPoderesMapa;
    Transform transformJ1, transformJ2, puntoInicialJ1, puntoInicialJ2;
    KeyCode teclaMenuJ1, teclaMenuJ2;

    struct Graficos
    {
        public bool pantallaCompleta;
        public int indiceGraficos, indiceResolucion;
        public Resolution resolucion;
    }
    Graficos configuracionGraficos;
    
    int victoriasJ1, victoriasJ2, indiceMapaActual = 1;
    float volumenSonidos, volumenMusica;   

    bool enMenu = false, primeraCarga = true, primerAudioSource=true;

    //Asegurarse de que solo hay una instancia
    public static GameManager instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        volumenSonidos = 1;
        volumenMusica = 0.09f;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(teclaMenuJ1) && !enMenu) || (Input.GetKeyDown(teclaMenuJ2) && !enMenu))
        {
            if (Input.GetKeyDown(teclaMenuJ1)) ui.AbreMenuIngame(Player.jugador1);
            else ui.AbreMenuIngame(Player.jugador2);

            PausaJuego();
        }
        else if((Input.GetKeyDown(teclaMenuJ1) && enMenu) || (Input.GetKeyDown(teclaMenuJ2) && enMenu))
        {
            ui.CierraMenuIngame();
            QuitaPausaJuego();
        }
    }

    /// <summary>
    /// Inicializa el torneo
    /// </summary>
    public void InicializaTorneo()
    {
        victoriasJ1 = 0;
        victoriasJ2 = 0;
        indiceMapaActual = 1;

        Controles.instance.SetEnMenuPrincipal(false);

        if(!CheatsManager.instance.GetEstadoCheats())
        {
            PantallaDeCarga(3f);
            CargaMapaEnMundos();
        }
        else
        {
            ui.CierraPantallaDeCarga();
            ui.AbrePantallaCheats();
        }
    }

    /// <summary>
    /// Cambia el Keycode que da acceso al menu
    /// </summary>
    /// <param name="nuevaTecla"></param>
    /// <param name="jugador"></param>
    public void SetTeclaMenu(KeyCode nuevaTecla, Player jugador)
    {
        if (jugador == Player.jugador1) teclaMenuJ1 = nuevaTecla;
        else teclaMenuJ2 = nuevaTecla;
    }

    /// <summary>
    /// este método se llama cuando uno de los jugadores alcanza la llegada (arrival)
    /// </summary>
    /// <param name="jugadorEnMeta"></param>
    public void FinalizarRonda(Player jugadorEnMeta)
    {
        if (jugadorEnMeta == Player.jugador1) victoriasJ1++;
        else victoriasJ2++;

        if (transformJ1.gameObject.GetComponent<ControladorJugador>() != null)
            transformJ1.gameObject.GetComponent<ControladorJugador>().SetEstadoControlador(false);
        if (transformJ2.gameObject.GetComponent<ControladorJugador>() != null)
            transformJ2.gameObject.GetComponent<ControladorJugador>().SetEstadoControlador(false);

        if (transformJ1.gameObject.GetComponent<SaltoParedes>() != null)
            transformJ1.gameObject.GetComponent<SaltoParedes>().SetSalto(false, Muros.derecha);

        if (transformJ2.gameObject.GetComponent<SaltoParedes>() != null)
            transformJ2.gameObject.GetComponent<SaltoParedes>().SetSalto(false, Muros.derecha);

        if (transformJ1.gameObject.GetComponentInChildren<CubitoHielo>() != null)
            Destroy(transformJ1.gameObject.GetComponentInChildren<CubitoHielo>().gameObject);
        if (transformJ2.gameObject.GetComponentInChildren<CubitoHielo>() != null)
            Destroy(transformJ2.gameObject.GetComponentInChildren<CubitoHielo>().gameObject);

        if (transformJ1.gameObject.GetComponent<FeedbackVisual>() != null)
            transformJ1.gameObject.GetComponent<FeedbackVisual>().DesactivaTodos();
        if (transformJ2.gameObject.GetComponent<FeedbackVisual>() != null)
            transformJ2.gameObject.GetComponent<FeedbackVisual>().DesactivaTodos();

        transformJ1.gameObject.SetActive(false);
        transformJ2.gameObject.SetActive(false);

        if (indiceMapaActual < 3)
        {
            indiceMapaActual++;

            ui.CargaResultados(victoriasJ1, victoriasJ2, indiceMapaActual, 4f);
            CargaMapaEnMundos();
        }
        else
        {
            if (victoriasJ1 == 3 || victoriasJ2 == 1 && victoriasJ1 ==2) ui.AbrePantallaGanador(Player.jugador1);
            else ui.AbrePantallaGanador(Player.jugador2);

            Invoke("CambiaEscena", 6f);
        }
    }

    /// <summary>
    /// Le dice a la UI que actualize las gemas
    /// </summary>
    /// <param name="gema"></param>
    /// <param name="jugador"></param>
    /// <param name="poder"></param>
    public void ActualizaGemas(int gema, Player jugador, Poderes poder)
    {
        ui.ActualizaGema(gema, jugador, poder);
    }   

    /// <summary>
    /// Llama al metodo ActualizarLlave del UIManager
    /// </summary>
    /// <param name="jugador"></param>
    /// <param name="activado"></param>
    public void ActualizarLlave(Player jugador, bool activado)
    {
        ui.ActualizarLlave(jugador, activado);
    }

    /// <summary>
    /// Recogemos UI
    /// </summary>
    /// <param name="UIM"></param>
    public void SetUI(UIManager UIM)
    {
        ui = UIM;
    }

    /// <summary>
    /// Método para almacenar el volúmen de los sonidos.  
    /// </summary>
    /// <param name="vol"></param>
    public void SetVolumenSonidos(float vol)
    {
        volumenSonidos = vol;
    }

    /// <summary>
    /// Devuelve volumenSonidos
    /// </summary>
    /// <returns></returns>
    public float GetVolumenSonidos()
    {
        return volumenSonidos;
    }

    /// <summary>
    /// Método para almacenar el volúmen de la musica.  
    /// </summary>
    /// <param name="vol"></param>
    public void SetVolumenMusica(float vol)
    {
        volumenMusica = vol;
        ConfiguraVolumenMusica();
    }

    /// <summary>
    /// Devuelve volumenMusica
    /// </summary>
    /// <returns></returns>
    public float GetVolumenMusica()
    {
        return volumenMusica;
    }

    /// <summary>
    /// Cambia la musica
    /// </summary>
    /// <param name="musicaManager"></param>
    public void SetAudioSourceMusica(AudioSource musicaManager)
    {
        if (primerAudioSource)
        {
            musicaManagerJ1 = musicaManager;
            primerAudioSource = false;
        }
        else musicaManagerJ2 = musicaManager;
    }
   
    /// <summary>
    /// Llamado para que la seleccion de graficos desde el menu siga en vigor
    /// </summary>
    /// <param name="pantallaCompleta"></param>
    /// <param name="indiceGraficos"></param>
    /// <param name="indiceResolucion"></param>
    /// <param name="resolucionActual"></param>
    public void GuardaConfiguracionGraficos(bool pantallaCompleta, int indiceGraficos, int indiceResolucion, Resolution resolucionActual)
    {
        configuracionGraficos.pantallaCompleta = pantallaCompleta;
        configuracionGraficos.indiceGraficos = indiceGraficos;
        configuracionGraficos.indiceResolucion = indiceResolucion;
        configuracionGraficos.resolucion = resolucionActual;
    }

    /// <summary>
    /// Recogemos el audio manager
    /// </summary>
    /// <param name="AM"></param>
    public void SetAudioManager(AudioManager AM)
    {
        audioManager = AM;
    }

    /// <summary>
    /// Llama a audioManager para que ejecute un audio
    /// </summary>
    /// <param name="audioSource"></param>
    /// <param name="nombreSonido"></param>
    public void EjecutarSonido(AudioSource audioSource, string nombreSonido)
    {
        audioManager.EjecutarSonido(audioSource, nombreSonido, volumenSonidos);
    }

    /// <summary>
    /// Llama a audioManager para que ejecute un audio
    /// </summary>
    /// <param name="nombreSonido"></param>
    /// <param name="eleccion"></param>
    public void EjecutarSonido(string nombreSonido, int eleccion)
    {
        audioManager.EjecutarSonido(nombreSonido, eleccion, volumenSonidos);
    }

    /// <summary>
    /// Método para informar al GameManager de las coordenadas que se usarán para la gestión de los poderes.
    /// </summary>
    /// <param name="coords"></param>
    public void SetCoordenadasPoderes(Transform[] coords)
    {
        coordPoderesMapa = coords;
        if(transformJ1.gameObject.GetComponent<PoderesManager>()!=null)
            transformJ1.gameObject.GetComponent<PoderesManager>().ConfiguraCoordenadasPoderes(coords);
        if (transformJ2.gameObject.GetComponent<PoderesManager>() != null)
            transformJ2.gameObject.GetComponent<PoderesManager>().ConfiguraCoordenadasPoderes(coords);
    }

    /// <summary>
    /// Método para informar a los componentes que lo necesiten de las coordenadas que se usarán para los poderes.
    /// </summary>
    /// <returns></returns>
    public Transform[] GetCoordenadasPoderes()
    {
        return coordPoderesMapa;
    }

    /// <summary>
    /// Setter para que el controlador PuntoInicial pueda informar del punto inicial de cada mapa para cada jugador
    /// </summary>
    /// <param name="tr"></param>
    /// <param name="jug"></param>
    public void SetPuntoInicial(Transform tr, Mundos mundo)
    {
        if (mundo == Mundos.mundoJ1) puntoInicialJ1 = tr;
        else puntoInicialJ2 = tr;
    }

    /// <summary>
    /// Método para tomar control en GameManager de cada mundo y su jugador.
    /// </summary>
    /// <param name="mundo">Tipo de Mundo(enum) para identificar la referencia de cada mundo y jugador</param>
    /// <param name="goMundo">Referencia al GO del mundo en cuestión</param>
    /// <param name="tr">Transform del jugador correspondiente a ese mundo</param>
    public void SetMundoYJugador(Mundos mundo, GameObject goMundo, Transform tr)
    {
        //Cambiar a bool y adaptar ControlMundos cuando implemente una forma de comprobar que realmente se ha cargado todo -> pensar detenidamente
        if (mundo == Mundos.mundoJ1)
        {
            mundoJ1 = goMundo;
            transformJ1 = tr;
        }
        else
        {
            mundoJ2 = goMundo;
            transformJ2 = tr;
        }
    }

    /// <summary>
    /// Método para instanciar mapa "reseteado", colocarlo, y asignarlo a su mundo correspondiente.
    /// </summary>
    public void CargaMapaEnMundos()
    {
        Debug.Log("Antes de borrar mapa");
        if (!primeraCarga)
        {
            Destroy(mapaJ1.gameObject);
            Destroy(mapaJ2.gameObject);            
        }

        Debug.Log("Despues de borrar mapa");

        switch (indiceMapaActual)
        {
            case 1:
                if (mapPrefab != null)
                {
                    var mapa1 = Instantiate(mapPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    mapa1.transform.parent = mundoJ1.transform;
                    mapaJ1 = mapa1;

                    var mapa2 = Instantiate(mapPrefab, new Vector3(0, -100, 0), Quaternion.identity) as GameObject;
                    mapa2.transform.parent = mundoJ2.transform;
                    mapaJ2 = mapa2;
                }break;
            case 2:
                if (mapPrefab2 != null)
                {
                    var mapa1 = Instantiate(mapPrefab2, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    mapa1.transform.parent = mundoJ1.transform;
                    mapaJ1 = mapa1;

                    var mapa2 = Instantiate(mapPrefab2, new Vector3(0, -100, 0), Quaternion.identity) as GameObject;
                    mapa2.transform.parent = mundoJ2.transform;
                    mapaJ2 = mapa2;
                }
                break;
            case 3:
                if (mapPrefab3 != null){
                    var mapa1 = Instantiate(mapPrefab3, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    mapa1.transform.parent = mundoJ1.transform;
                    mapaJ1 = mapa1;

                    var mapa2 = Instantiate(mapPrefab3, new Vector3(0, -100, 0), Quaternion.identity) as GameObject;
                    mapa2.transform.parent = mundoJ2.transform;
                    mapaJ2 = mapa2;
                }
                break;
        }
        Debug.Log("Despues de cargar el nuevo mapa");

        if(CheatsManager.instance.GetEstadoCheats())
            Invoke("ColocaJugadores", 0.3f);

        primeraCarga = false;
    }

    /// <summary>
    /// Coloca los jugadores en sus puntos iniciales correspondientes en cada mapa, los hace visible para los jugadores y activa sus controles.
    /// </summary>
    public void ColocaJugadores()
    {
        transformJ1.position = puntoInicialJ1.position;
        transformJ2.position = puntoInicialJ2.position;

        transformJ1.gameObject.SetActive(true);
        transformJ2.gameObject.SetActive(true);

        //Si el jugador tiene una llave se le quita
        if (transformJ1.gameObject.GetComponent<AbrirPuertas>() != null)
            transformJ1.gameObject.GetComponent<AbrirPuertas>().QuitarLlave();
        if (transformJ2.gameObject.GetComponent<AbrirPuertas>() != null)
            transformJ2.gameObject.GetComponent<AbrirPuertas>().QuitarLlave();

        
        if (transformJ1.gameObject.GetComponent<EstadoFantasma>() != null)
            transformJ1.gameObject.GetComponent<EstadoFantasma>().DesactivaEstadoFantasma();
        if (transformJ2.gameObject.GetComponent<EstadoFantasma>() != null)
            transformJ2.gameObject.GetComponent<EstadoFantasma>().DesactivaEstadoFantasma();


        //Si el jugador tiene el cubo de hielo se le quita
        if (transformJ1.gameObject.GetComponentInChildren<CubitoHielo>() != null)
            Destroy(transformJ1.gameObject.GetComponentInChildren<CubitoHielo>().gameObject);
        if (transformJ2.gameObject.GetComponentInChildren<CubitoHielo>() != null)
            Destroy(transformJ2.gameObject.GetComponentInChildren<CubitoHielo>().gameObject);

        
        if (transformJ1.gameObject.GetComponent<PerdidasControl>()!=null)
            transformJ1.gameObject.GetComponent<PerdidasControl>().SetEstado(PerdidaControles.sinCc);
        if (transformJ2.gameObject.GetComponent<PerdidasControl>() != null)
            transformJ2.gameObject.GetComponent<PerdidasControl>().SetEstado(PerdidaControles.sinCc);


        if (transformJ1.gameObject.GetComponent<ControladorJugador>() != null)
            transformJ1.gameObject.GetComponent<ControladorJugador>().SetEstadoControlador(true);
        if (transformJ2.gameObject.GetComponent<ControladorJugador>() != null)
            transformJ2.gameObject.GetComponent<ControladorJugador>().SetEstadoControlador(true);
    }    

    /// <summary>
    /// Restablece el juego de su pausa y cierra el menú.
    /// </summary>
    public void QuitaPausaJuego()
    {
        enMenu = false;
        Time.timeScale = 1;

        InterruptorMundos(true);
    }

    /// <summary>
    /// Cambia el mapa a cargar
    /// </summary>
    /// <param name="indice"></param>
    public void SetIndiceMapa(int indice)
    {
        indiceMapaActual = indice;
    }

    /// <summary>
    /// Cambia la escena a menu
    /// </summary>
    void CambiaEscena()
    {
        Controles.instance.SetEnMenuPrincipal(true);
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Actualiza el volumen a la que se escucha la musica
    /// </summary>
    void ConfiguraVolumenMusica()
    {
        if (musicaManagerJ1 != null && musicaManagerJ2 != null)
        {
            musicaManagerJ1.volume = volumenMusica;
            musicaManagerJ2.volume = volumenMusica;
        }
    }

    /// <summary>
    /// Configura el juego para establecer una pausa y muestra la pantalla de menu inGame, se activa en caso de que algún jugador abre dicho menú.
    /// </summary>
    void PausaJuego()
    {
        enMenu = true;
        Time.timeScale = 0;

        InterruptorMundos(false);
    }

    /// <summary>
    /// Método para controlar la visualización y activación de mundo/jugador. 
    /// Hace de "interruptor" para cada mundo y su jugador.
    /// </summary>
    /// <param name="mundo">Mundo</param>
    /// <param name="cont">Controles del jugador en dicho mundo</param>
    /// <param name="est">Valor del interruptor</param>
    void InterruptorMundos(bool est)
    {
        if (transformJ1.gameObject.GetComponent<ControladorJugador>() != null)
            transformJ1.gameObject.GetComponent<ControladorJugador>().SetEstadoControlador(est);
        if (transformJ2.gameObject.GetComponent<ControladorJugador>() != null)
            transformJ2.gameObject.GetComponent<ControladorJugador>().SetEstadoControlador(est);

        mundoJ1.SetActive(est);
        mundoJ2.SetActive(est);
    }
    
    /// <summary>
    /// Activa la pantalla de carga y la desactiva al rato
    /// </summary>
    /// <param name="tiempo">tiempo que tarda en desactivarse</param>
    void PantallaDeCarga(float tiempo)
    {
        ui.AbrePantallaDeCarga(tiempo);
    }
}
