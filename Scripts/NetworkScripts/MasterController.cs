using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MasterController : NetworkBehaviour {

    
    public GameObject[] playerList;
    public int numberOfPlayers;
    public GameObject gm;
    public GameObject minimap;

    // Use this for initialization
    void Awake () {
        gm = GameObject.FindGameObjectWithTag("Game Manager");
        minimap = GameObject.FindGameObjectWithTag("Minimap");
    }

	void Update () {
        if (gm == null || minimap == null) {
            Awake();
            return;
        }
        playerList = GameObject.FindGameObjectsWithTag("Player");
        numberOfPlayers = playerList.Length;
        gm.GetComponent<GameManagerScript>().PlayerCount = numberOfPlayers;
        GetComponent<SyncColors>().Sync(playerList);
    }
}
