using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatsManager : MonoBehaviour {

    public static CheatsManager instance = null;

    Text cheatText;
    bool estadoCheats, estadoInvencibilidad = false;
    
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
        estadoCheats = false;
	}

    /// <summary>
    /// Método para recuperar referencias del Text hijo del InputField para configurarlo tras las cargas de escenas.
    /// </summary>
    /// <param name="text"></param>
    public void SetGameObjectText(Text text)
    {
        cheatText = text;
    }

    /// <summary>
    /// Acción de recoger el texto introducido en el inputfield y su posterior análisis
    /// </summary>
    public void RecogeTextoInputFieldCheats()
    {
        
        string codigo = cheatText.text;
        Debug.Log("Codigo introducido: "+codigo);

        AnalizaCodigoIntroducido(codigo);
    }

    /// <summary>
    /// Método para destruir los cheats. Una vez ejecutado SERÁN IRRECUPERABLES hasta cerrar y abrir el juego de nuevo.
    /// </summary>
    public void DestroyCheats()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Devuelve el estado de los cheats.
    /// </summary>
    /// <returns></returns>
    public bool GetEstadoCheats()
    {
        return estadoCheats;
    }

    /// <summary>
    /// Configura el estado de la invencibilidad
    /// </summary>
    /// <param name="est"></param>
    public void SetEstadoInvencibilidad(bool est)
    {
        estadoInvencibilidad = est;
    }

    /// <summary>
    /// Devuelve el estado de la invencibilidad para gestionar la interacción con los objetos del mapa.
    /// </summary>
    /// <returns></returns>
    public bool GetEstadoInvencibilidad()
    {
        return estadoInvencibilidad;
    }

    /// <summary>
    /// Analiza el código introducido, se podrá activar, desactivar o destruir los cheats a través de dichos códigos.
    /// </summary>
    /// <param name="codigo">Codigo cheat</param>
    void AnalizaCodigoIntroducido(string codigo)
    {
        //if (codigo == "*-=G@DM0D3_0N:UCM_StYLe=-*")
        if (codigo == "12345")
        {
            Debug.Log("Congratz! YOU ARE THE F. MASTER NOW! ENJOY THE POWER OF THE CIBERTRIAL'S GODS! ");
            SetCheats(true);
        }
        //else if (codigo == "*-=G@DM0D3_0FF:UCM_StYLe=-*")
        else if (codigo == "54321")
        {
            Debug.Log("Loosing Cibertrial's Powers! MAY THE FORCE BE WITH YOU!");
            SetCheats(false);
        }
        else if (codigo == "12345destroy")
        {
            Debug.Log("Destroying Cibertrial's Powers! SORRY FOR YOU!");
            DestroyCheats();
        }
        else
        {
            Debug.Log("Are you keeding me? Nothing to do... :D ");
            cheatText.text = "Nothing to do..";
        }
    }

    /// <summary>
    /// Método para controlar el estado de los cheats
    /// </summary>
    /// <param name="estado"></param>
    void SetCheats(bool estado)
    {
        estadoCheats = estado;
    }
}
