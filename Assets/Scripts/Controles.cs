using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controles : MonoBehaviour {

    public static Controles instance = null;
    
    public KeyCode teclaSaltarJ1, teclaRodarJ1, teclaPoderJ1, teclaRomperParedesJ1, teclaIzqdaParedesJ1, teclaDchaParedesJ1, teclaMenuJ1;//Variables que corresponden a las teclas de accion de j1

    public KeyCode teclaSaltarJ2, teclaRodarJ2, teclaPoderJ2, teclaRomperParedesJ2, teclaIzqdaParedesJ2, teclaDchaParedesJ2, teclaMenuJ2;//Variables que corresponden a las teclas de accion de j2

    Dictionary<string, KeyCode> controlesJ1 = new Dictionary<string, KeyCode>();
    Dictionary<string, KeyCode> controlesJ2 = new Dictionary<string, KeyCode>();

    bool cargaInicial=true, enMenuPrincipal = true;

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
    void Start () {
        if (cargaInicial)
        {
            CreaDiccionarioControles();//LLamada al metodo para crear el diccionario
            cargaInicial = false;
        }        
    }
	
    void CreaDiccionarioControles()//Mete en el diccionario espacio para todas las teclas que se usan en el juego
    {
        controlesJ1.Add("Saltar", teclaSaltarJ1);
        controlesJ1.Add("Rodar", teclaRodarJ1);
        controlesJ1.Add("Poder", teclaPoderJ1);
        controlesJ1.Add("RomperParedes", teclaRomperParedesJ1);
        controlesJ1.Add("IzqdaParedes", teclaIzqdaParedesJ1);
        controlesJ1.Add("DchaParedes", teclaDchaParedesJ1);
        controlesJ1.Add("Menu", teclaMenuJ1);

        controlesJ2.Add("Saltar", teclaSaltarJ2);
        controlesJ2.Add("Rodar", teclaRodarJ2);
        controlesJ2.Add("Poder", teclaPoderJ2);
        controlesJ2.Add("RomperParedes", teclaRomperParedesJ2);
        controlesJ2.Add("IzqdaParedes", teclaIzqdaParedesJ2);
        controlesJ2.Add("DchaParedes", teclaDchaParedesJ2);
        controlesJ2.Add("Menu", teclaMenuJ2);
    }

    public Dictionary<string, KeyCode> GetControlesJugador(Player jugador)
    {
        if (jugador == Player.jugador1) return controlesJ1;
        else return controlesJ2;
    }

    public void SetControlesJugador(Dictionary<string, KeyCode> controles, Player jugador)//Las teclas introducidas se asignan a uno de los jugadores
    {
        if (jugador == Player.jugador1) controlesJ1 = controles;
        else controlesJ2 = controles;
    }

    public bool GetEnMenuPrincipal()
    {
        return enMenuPrincipal;
    }

    public void SetEnMenuPrincipal(bool est)
    {
        enMenuPrincipal = est;
    }
}
