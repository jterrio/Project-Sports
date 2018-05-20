using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Combat : MonoBehaviour {


    [HideInInspector]
    public Rigidbody rb;

    private static bool inCombat;
    public bool combatStarted;
    public bool isTurn;
    private float targetPositionDis;
    private Vector3 targetPosition;
    private bool isMoving;
    public bool powerCharging;
    public float charge = 0;

	// Use this for initialization
	void Awake () {
        inCombat = false;
        combatStarted = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (inCombat) {
            InitCombat();
            if (combatStarted) {
                CombatStages();
            }
        }
	}

    void CombatStages() {
        MoveThePlayer();
    }

    void MoveThePlayer() {
        //if mouse is down
        if (Input.GetMouseButton(1) && isTurn) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && !powerCharging) {
                powerCharging = true;
                if (hit.collider.name == "Terrain") {
                    targetPositionDis = Vector3.Distance(transform.position, hit.point);
                    if (targetPositionDis >= 1.0f) {
                        targetPosition = hit.point;
                        isMoving = true;
                    }
                }
            }else if (powerCharging) {
                charge += 1;
            }
        }else if (powerCharging) {
            powerCharging = false;
            if(charge > 10) {
                charge = 10;
            }
            rb.AddForce(targetPosition * charge);
            charge = 0;
        }
    }

    void InitCombat() {
        if (!combatStarted && rb.velocity.magnitude >= 1f) {
            rb.drag = 1f;
        } else {
            combatStarted = true;
            rb.drag = 0.0f;
        }
    }

    public void CheckCombat() {
        if (Input.GetKeyDown(KeyCode.V)) {
            inCombat = !inCombat;
            MouseLock.MouseLocked = false;
        }
    }


    public static bool Combat{
        get{
            return inCombat;
        }
        set {
            inCombat = value;
        }
    }

}
