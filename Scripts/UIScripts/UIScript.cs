using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UIScript : MonoBehaviour {

    public GameObject gm;
    public Canvas outOfCombatUI;
    public RectTransform bookPanel, facePanel, barPanel;

    void Awake() {
        gm = GameObject.FindGameObjectWithTag("Game Manager");
    }
    /*
    void Update() {
        gm = GameObject.FindGameObjectWithTag("Network Manager");
    }
    */

}
