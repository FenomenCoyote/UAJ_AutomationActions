using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCollider : MonoBehaviour {

    private SpriteRenderer spriteEnemigo;
    private Collider2D colliderVision;

    private float colliderVisionPosX;
    private float colliderVisionNegX;

    // Use this for initialization
    void Start ()
    {
        colliderVision = GetComponent<Collider2D>();
        spriteEnemigo = GetComponent<SpriteRenderer>();

        colliderVisionPosX = colliderVision.offset.x; //valores del offset para ajustar el collider cuando se produzca el flip del enemigo
        colliderVisionNegX = -colliderVision.offset.x;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(spriteEnemigo.flipX)//si esta en flip
        {
            colliderVision.offset = new Vector2(colliderVisionNegX, colliderVision.offset.y);//se le asigna el offset negativo
        }
        else//si no esta en flip
        {
            colliderVision.offset = new Vector2( colliderVisionPosX, colliderVision.offset.y);//se le asigna el offset positivo
        }
	}
}
