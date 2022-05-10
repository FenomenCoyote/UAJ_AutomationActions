using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlesManager : MonoBehaviour {

    public Player tipoJugador;

    Dictionary<string, KeyCode> controles = new Dictionary<string, KeyCode>();
    bool cargaInicial = true;

    // Use this for initialization
    void Start () {
        Invoke("AsignaControlesInicialesJugador", 0.1f);
	}

    /// <summary>
    /// Cambia un control del jugador por otro, usando SetControlXXXXX
    /// </summary>
    /// <param name="nombreControl"></param>
    /// <param name="nuevaTecla"></param>
    public void CambiaControlJugador(string nombreControl, KeyCode nuevaTecla)
    {
        switch (nombreControl)
        {
            case "Saltar": SetControlSaltar(nuevaTecla); break;
            case "Rodar": SetControlRodar(nuevaTecla); break;
            case "Poder": SetControlPoder(nuevaTecla); break;
            case "RomperParedes": SetControlRomperParedes(nuevaTecla); break;
            case "IzqdaParedes": SetControlIzqdaParedes(nuevaTecla); break;
            case "DchaParedes": SetControlDchaParedes(nuevaTecla); break;
            case "Menu": SetControlMenu(nuevaTecla); break;
        }
        InformaControles();
    }

    /// <summary>
    /// Inicializa los controles del jugador
    /// </summary>
    void AsignaControlesInicialesJugador()
    {
        controles = Controles.instance.GetControlesJugador(tipoJugador);

        SetControlSaltar(controles["Saltar"]);
        SetControlRodar(controles["Rodar"]);

        SetControlPoder(controles["Poder"]);

        SetControlRomperParedes(controles["RomperParedes"]);

        SetControlIzqdaParedes(controles["IzqdaParedes"]);
        SetControlDchaParedes(controles["DchaParedes"]);

        SetControlMenu(controles["Menu"]);

        cargaInicial = false;
    }

    /// <summary>
    /// Cambia un KeyCode por otro
    /// </summary>
    /// <param name="nuevaTecla"></param>
    void SetControlSaltar(KeyCode nuevaTecla)
    {
        if (GetComponent<ControladorJugador>() != null)
            GetComponent<ControladorJugador>().SetTeclaSaltar(nuevaTecla);

        if(!cargaInicial) controles["Saltar"] = nuevaTecla;

        SetControlSaltoParedes(nuevaTecla);
    }

    /// <summary>
    /// Cambia un KeyCode por otro
    /// </summary>
    /// <param name="nuevaTecla"></param>
    void SetControlRodar(KeyCode nuevaTecla)
    {
        if (GetComponent<ControladorJugador>() != null)
            GetComponent<ControladorJugador>().SetTeclaRodar(nuevaTecla);

        if (!cargaInicial) controles["Rodar"] = nuevaTecla;
    }

    /// <summary>
    /// Cambia un KeyCode por otro
    /// </summary>
    /// <param name="nuevaTecla"></param>
    void SetControlPoder(KeyCode nuevaTecla)
    {
        if (GetComponent<PoderesManager>() != null)
            GetComponent<PoderesManager>().SetTeclaPoder(nuevaTecla);

        if (!cargaInicial) controles["Poder"] = nuevaTecla;
    }

    /// <summary>
    /// Cambia un KeyCode por otro
    /// </summary>
    /// <param name="nuevaTecla"></param>
    void SetControlRomperParedes(KeyCode nuevaTecla)
    {
        if (GetComponent<RomperParedes>() != null)
            GetComponent<RomperParedes>().SetTeclaRomperParedes(nuevaTecla);

        if (!cargaInicial) controles["RomperParedes"] = nuevaTecla;
    }

    /// <summary>
    /// Cambia un KeyCode por otro
    /// </summary>
    /// <param name="nuevaTecla"></param>
    void SetControlSaltoParedes(KeyCode nuevaTecla)
    {
        if (GetComponent<SaltoParedes>() != null)
            GetComponent<SaltoParedes>().SetTeclaSaltoParedes(nuevaTecla);
    }

    /// <summary>
    /// Cambia un KeyCode por otro
    /// </summary>
    /// <param name="nuevaTecla"></param>
    void SetControlIzqdaParedes(KeyCode nuevaTecla)
    {
        if (GetComponent<SaltoParedes>() != null)
            GetComponent<SaltoParedes>().SetTeclaIzqdaParedes(nuevaTecla);

        if (!cargaInicial) controles["IzqdaParedes"] = nuevaTecla;
    }

    /// <summary>
    /// Cambia un KeyCode por otro
    /// </summary>
    /// <param name="nuevaTecla"></param>
    void SetControlDchaParedes(KeyCode nuevaTecla)
    {
        if (GetComponent<SaltoParedes>() != null)
            GetComponent<SaltoParedes>().SetTeclaDchaParedes(nuevaTecla);

        if (!cargaInicial) controles["DchaParedes"] = nuevaTecla;
    }

    /// <summary>
    /// Cambia un KeyCode por otro
    /// </summary>
    /// <param name="nuevaTecla"></param>
    void SetControlMenu(KeyCode nuevaTecla)
    {
        GameManager.instance.SetTeclaMenu(nuevaTecla, tipoJugador);

        if (!cargaInicial) controles["Menu"] = nuevaTecla;
    }

    /// <summary>
    /// Informa a Controles.cs que cambie los controles a tipoJugador
    /// </summary>
    void InformaControles()
    {
        Controles.instance.SetControlesJugador(controles, tipoJugador);
    }   
}
