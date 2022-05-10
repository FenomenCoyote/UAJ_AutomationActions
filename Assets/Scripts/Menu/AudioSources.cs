using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSources : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GameManager.instance != null) GameManager.instance.SetAudioSourceMusica(GetComponent<AudioSource>());
        Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
