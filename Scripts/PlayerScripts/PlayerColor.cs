using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerColor : NetworkBehaviour {

    [SyncVar(hook = "changeColor")] public Color c;
    private GameObject gm;

    void changeColor(Color color) {
        GetComponent<Renderer>().material.color = color;
    }

    void Awake() {
        gm = GameObject.FindGameObjectWithTag("Game Manager");
    }
 

    // Use this for initialization
    void Start () {
        if (!isServer) {
            return;
        }
        c = GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
	}
}
