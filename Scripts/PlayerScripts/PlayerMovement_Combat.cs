using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement_Combat : NetworkBehaviour {


    public float MAX_CHARGE = 50;
    [HideInInspector]
    public Rigidbody rb;


    private static bool inCombat;
    private bool combatStarted;
    public bool isTurn;

    private float targetPositionDis;
    private Vector3 targetPosition;
    public Camera mainCamera;

    private bool isMoving;
    private bool powerCharging;
    private bool gainingCharge;
    private bool startedCharging;
    private float charge = 0;
    private float timer = 0.01f;

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
        if (!isLocalPlayer) {
            return;
        }
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

    bool Timer() {
        if(Time.time >= timer) {
            timer = Time.time + 0.01f;
            return true;
        } else {
            return false;
        }
    }

    void MoveThePlayer() {

        if (Input.GetMouseButtonDown(1) && !startedCharging && isTurn) {
            startedCharging = true;
        }

        if(powerCharging && Input.GetMouseButton(1) && Input.GetMouseButton(0)) {
            powerCharging = false;
            startedCharging = false;
            charge = 0;
        }

        //if mouse is down
        if (Input.GetMouseButton(1) && !powerCharging && startedCharging) {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                powerCharging = true;
                if (hit.collider.name == "Terrain") {
                    targetPositionDis = Vector3.Distance(transform.position, hit.point);
                    if (targetPositionDis >= 1.0f) {
                        targetPosition = hit.point;
                        isMoving = true;
                    }
                }
            }
        } else if (Input.GetMouseButton(1) && powerCharging && Timer()) {
            if (charge > MAX_CHARGE) {
                charge = MAX_CHARGE;
                gainingCharge = false;
            } else if (charge < 0) {
                charge = 0;
                gainingCharge = true;
            }
            if (gainingCharge) {
                charge += 1;
            } else {
                charge -= 1;
            }
        } else if (powerCharging && !Input.GetMouseButton(1)) {
            startedCharging = false;
            powerCharging = false;
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
        if (Input.GetKeyDown(KeyCode.B)) {
            isTurn = !isTurn;
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

    public float Charge {
        get {
            return charge;
        }
        set {
            charge = value;
        }
    }

    public bool Turn {
        get {
            return isTurn;
        }
        set {
            isTurn = value;
        }
    }

    public float MaxCharge {
        get {
            return MAX_CHARGE;
        }
        set {
            MAX_CHARGE = value;
        }
    }
}
