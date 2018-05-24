using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManagerScript : NetworkBehaviour {


    public GameObject player;
    public GameObject minimap;
    public GameObject[] playerList;
    public Camera playerCamera;
    [SyncVar(hook = "AssignCharacters")] private int playerCount = 0;

    void AssignCharacters(int count) {
        playerList = GameObject.FindGameObjectsWithTag("Player");
        minimap = GameObject.FindGameObjectWithTag("Minimap");
        foreach (GameObject p in playerList) {
            if (p.GetComponent<NetworkBehaviour>().isLocalPlayer) {
                player = p;
                playerCamera = p.transform.Find("Player Camera").GetComponent<Camera>();
            }
        }
        minimap.GetComponent<Minimap>().AssignMinimapColor();
    }

    public int PlayerCount {
        get {
            return playerCount;
        }
        set {
            playerCount = value;
        }
    }





}
