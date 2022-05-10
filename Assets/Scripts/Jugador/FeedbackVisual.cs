using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackVisual : MonoBehaviour {
    public GameObject stun, ralentizar, powerupPico, powerupFantasma, resucitar, powerUpVel, inversion, despacio;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Activación/desactivación de Feedbacks visuales.
    /// </summary>
    /// <param name="caso"></param>
    /// <param name="actdes"></param>
    public void ActivarDesactivarFeedBack(int caso, bool actdes)
    {
        switch (caso)
        {
            case 0://stun
                stun.SetActive(actdes);
                break;
            case 1://ralentizar
                ralentizar.SetActive(actdes);
                break;
            case 2://ppPico
                powerupPico.SetActive(actdes);
                break;
            case 3://ppFan
                powerupFantasma.SetActive(actdes);
                break;
            case 4://resucitar
                resucitar.SetActive(actdes);
                break;
            case 5://ppVel
                powerUpVel.SetActive(actdes);
                break;
            case 6://inversion de controles
                inversion.SetActive(actdes);
                break;
            case 7://inversion de controles
                despacio.SetActive(actdes);
                break;
        }
    }

    /// <summary>
    /// Método para desactivar todos los feedbacks. Se utiliza como "limpieza" en casos de cambios de escenas y situaciones similares.
    /// </summary>
    public void DesactivaTodos()
    {
        stun.SetActive(false);
        ralentizar.SetActive(false);
        powerupPico.SetActive(false);
        powerupFantasma.SetActive(false);
        resucitar.SetActive(false);
        powerUpVel.SetActive(false);
        inversion.SetActive(false);
        despacio.SetActive(false);
    }
}
