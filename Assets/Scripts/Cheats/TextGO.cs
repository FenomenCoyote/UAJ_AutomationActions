using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGO : MonoBehaviour {

    /// <summary>
    /// Llamada para refrescar la referencia del Text que se utiliza para introducir los cheats.
    /// De esta forma podremos interactuar con ellos indiferentemente de las cargas de escenas.
    /// Destruye el objeto tras el informe ya que no necesitamos el componente para nada más.
    /// </summary>
	// Use this for initialization
	void Start () {
        if (CheatsManager.instance != null) CheatsManager.instance.SetGameObjectText(this.gameObject.GetComponent<Text>());
        Destroy(this);
	}
}
