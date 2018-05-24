using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ChargeScript : UIScript {

    public RectTransform chargePanel;
    public Image charge;
    private PlayerMovement_Combat playerMovementCombat;
    private Vector3 localPosition;
    private Vector3 hiddenPosition;

    // Use this for initialization
    void CallAfter () {
        //playerMovementCombat = Player.GetComponent<PlayerMovement_Combat>();
        localPosition = chargePanel.transform.localPosition;
        hiddenPosition = new Vector3(localPosition.x, localPosition.y + 200, localPosition.z);
    }
	
	// Update is called once per frame
	void Update () {
        if(true) {
            return;
        }
        if (playerMovementCombat == null) {
            CallAfter();
        }
        if (playerMovementCombat.Turn) {
            chargePanel.transform.localPosition = localPosition;
            charge.fillAmount = playerMovementCombat.Charge / playerMovementCombat.MaxCharge;
        } else {
            chargePanel.transform.localPosition = hiddenPosition;
        }
	}
}
