using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Minimap : NetworkBehaviour {

    public float offset = 10;
    public GameObject gm;

    private Camera mainCamera;


    public Material[] hostPlayer;
    public Material[] clientPlayer;
    public Material[] NeutralNPC;
    public Material[] HostileNPC;
    public Material[] FriendlyNPC;
    public Material[] ImportantNPC;


    void Awake() {
        gm = GameObject.FindGameObjectWithTag("Game Manager");
    }

    public void AssignMinimapColor() {
        foreach(GameObject p in gm.GetComponent<GameManagerScript>().playerList) {
            if (p.GetComponent<NetworkBehaviour>().isLocalPlayer) {
                p.GetComponent<CharacterInfo>().minimapBlip.GetComponent<Renderer>().materials = hostPlayer;
            } else {
                p.GetComponent<CharacterInfo>().minimapBlip.GetComponent<Renderer>().materials = clientPlayer;
            }
        }
    }

    void Update() {
        if(gm == null) {
            Awake();
        }
    }

    void LateUpdate() {
        AssignMinimapPosition();
    }

	
	public void AssignMinimapPosition() {
        GameObject p = gm.GetComponent<GameManagerScript>().player;
        if (p != null) {
            Vector3 newPosition = p.transform.position;
            newPosition.y += offset;
            transform.position = newPosition;
            transform.eulerAngles = new Vector3(90f, gm.GetComponent<GameManagerScript>().playerCamera.transform.eulerAngles.y, 0f);
        }
    }
}
