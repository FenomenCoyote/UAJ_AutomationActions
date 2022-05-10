using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarPanel : MonoBehaviour {

    public GameObject panelMenu,panelConfig,panelCreditos, panelGraficosSonido, panelControles;

    Animator panel;
    string nombrePanel;

    // Use this for initialization
    void Start ()
    {
        if(GetComponent<Animator>() != null)
            panel = GetComponent<Animator>();
    }
    
    /// <summary>
    /// Metodo para que desparezca el panel
    /// </summary>
    public void Desvanecerse()
    {
        panel.SetTrigger("fadeOut");
    }

    /// <summary>
    /// Activa el trigger de la transicion dependiendo de el boton pulsado
    /// </summary>
    public void ActivaTrigger()
    {
        panel.SetTrigger(nombrePanel);

        switch (nombrePanel)
        {
            case "menu":
                panelMenu.gameObject.SetActive(true);
                break;
            case "configuracion":
                panelConfig.gameObject.SetActive(true);
                break;
            case "creditos":
                panelCreditos.gameObject.SetActive(true);
                break;
            case "controles":
                panelControles.gameObject.SetActive(true);
                break;
            case "graficosysonido":
                panelGraficosSonido.gameObject.SetActive(true);
                break;
        }
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Segun el boton que se pulse el trigger será distinto
    /// </summary>
    /// <param name="sigPanel"></param>
    public void EstablecePanel(string sigPanel)
    {
        nombrePanel = sigPanel;
    }
}
