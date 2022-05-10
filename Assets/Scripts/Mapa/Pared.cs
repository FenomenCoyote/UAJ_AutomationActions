using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pared : MonoBehaviour {

    public Sprite[] sprites;
    public int salud = 2;
    SpriteRenderer spriteRend;


	void Start ()
    {
        spriteRend = GetComponent<SpriteRenderer>();
	}

	/// <summary>
    /// Decrementa la vida del muro y si es menor de 0 se destuye
    /// A medida que va bajando la vida cambia el sprite del muro
    /// </summary>
    /// <param name="damage">daño que causamos al muro</param>
    public void DañarPared(int daño)
    {
        salud -= daño;
        if (salud < 0)
        {
            salud = 0;
            GameManager.instance.EjecutarSonido("DestruirMuro", 3);
            GameObject.Destroy(this.gameObject);
        }
        //Cuando tengamos sprite
       spriteRend.sprite = sprites[salud];
    }
}
