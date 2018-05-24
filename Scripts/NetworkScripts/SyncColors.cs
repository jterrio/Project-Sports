using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyncColors : NetworkBehaviour {


    private Color testColor = new Color(0f, 0f, 0f);
    private Color testColorTwo = new Color(1f, 1f, 1f);



    public void Sync(GameObject[] playerList) {
        foreach (GameObject p in playerList) {
            Color temp = p.GetComponent<PlayerColor>().c;
            if (p.GetComponent<PlayerColor>().c != testColor) {
                p.GetComponent<PlayerColor>().c = testColor;
            } else {
                p.GetComponent<PlayerColor>().c = testColorTwo;
            }
            p.GetComponent<PlayerColor>().c = temp;
        }
    }
}
 
