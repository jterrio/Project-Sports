using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CharacterInfo : MonoBehaviour {

    public GameObject localPlayer;
    public GameObject minimapBlip;
    


	// Use this for initialization
	void Awake () {
	}

    void Update() {
    }





    public string Name { get; set; }
    public int Health { get; set; }


}
