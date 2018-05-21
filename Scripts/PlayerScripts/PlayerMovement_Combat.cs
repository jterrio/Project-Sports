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
    private float lastPositionTime = 1f;
    private Vector3 lastPosition = Vector3.zero;
    private Vector3 forceVector;
    private PlayerMovement playerMovement;

	// Use this for initialization
	void Awake () {
        inCombat = false;
        combatStarted = false;
        playerMovement = GetComponent<PlayerMovement>();
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
            if(charge > 50) {
                charge = 50;
            }
            forceVector = targetPosition - transform.position;
            forceVector.Normalize();
            forceVector *= charge;
            forceVector.y = 0f;
            rb.AddForce(forceVector, ForceMode.VelocityChange);
            charge = 0;
        }
    }

    void InitCombat() {

        if (!combatStarted && CheckPositionTimer()) {
            rb.drag = 2;
            rb.angularDrag = 1;
        } else {
            combatStarted = true;
            rb.drag = 0f;        }
    }

    public void CheckCombat() {
        if (Input.GetKeyDown(KeyCode.V)) {
            inCombat = !inCombat;
            MouseLock.MouseLocked = false;
        }
    }

    bool CheckPositionTimer() {
        if(lastPosition == Vector3.zero) {
            lastPosition = transform.position;
            lastPositionTime = Time.time + 1f;
            return true;
        }
        if(Time.time >= lastPositionTime) {
            if(transform.position == lastPosition) {
                lastPosition = Vector3.zero;
                return false;
            } else {
                lastPositionTime = Time.time + 1f;
                lastPosition = transform.position;
                return true;
            }
        }return true;
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
