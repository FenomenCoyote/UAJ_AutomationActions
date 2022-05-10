using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonGO : MonoBehaviour {

    /// <summary>
    /// Método para gestionar la recogida de texto del inputfield desde el botón GO.
    /// </summary>
    public void RecogerTextoInputField()
    {
        if (CheatsManager.instance != null) CheatsManager.instance.RecogeTextoInputFieldCheats();
    }
}
