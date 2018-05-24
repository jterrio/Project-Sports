using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {

    

    [HideInInspector]
    public Rigidbody rb;

    public float MAX_SPEED = 25;
    public float MIN_SPEED = 12;
    public float AIR_SPEED = 10;
    public float speed;
    public float oldSpeed;
    public PlayerMovement_Combat playerMovementC;
    public Camera mainCamera;
    [HideInInspector]
    public Vector3 movement;

    private bool isGrounded;
    private bool inAir;
    private float moveHorizontal;
    private float moveVertical;
    private RaycastHit hit;
    private float regSpeedUpdate = 1f;
    private float angleSpeedUpdate = 1f;
    private float timeOnAngle = 1f;

    void Awake() {
        // Init oldSpeed
        oldSpeed = speed - 1;
        playerMovementC = GetComponent<PlayerMovement_Combat>();
    }

    void Update() {
        if (!isLocalPlayer) {
            return;
        }
        if (!mainCamera.gameObject.activeInHierarchy) {
            mainCamera.gameObject.SetActive(true);
        }
        playerMovementC.CheckCombat();
    }

    
    void FixedUpdate () {
        if (!PlayerMovement_Combat.Combat) {
            // Check if the player is grounded and calculate speed


            if (!isGrounded) {
                speed = AIR_SPEED;
                inAir = true;
            } else {
                CheckSpeed();
            }
            // Set some local float variables equal to the value of our Horizontal and Vertical Inputs
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");

            // Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
            movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            movement = mainCamera.transform.TransformDirection(movement);


            // Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
            // multiplying it by 'speed' - our public player speed that appears in the inspector
            rb.AddForce(movement * speed);
            //rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
        }
    }

    // Check collision with ground
    void OnCollisionEnter(Collision theCollision) {
        
        if(theCollision.gameObject.name == "Terrain") {
            isGrounded = true;
        }
    }

    //Timer for angle's speed
    bool Timer() {
        if (Time.time >= angleSpeedUpdate) {
            angleSpeedUpdate = Mathf.Floor(Time.time) + 1f;
            return true;
        } else {
            return false;
        }
    }

    // Reduce speed if on an angle
    void ReduceSpeedAngle() {
        if (CheckAngle() > 80f) {
            if (Timer()) {
                speed -= 20f;
                timeOnAngle += 1;
            }
        } else if (CheckAngle() > 70f) {
            if (Timer()) {
                speed -= 7f + timeOnAngle;
                timeOnAngle += 1;
            }
        } else if (CheckAngle() > 60f) {
            if (Timer()) {
                speed -= 5f + timeOnAngle;
                timeOnAngle += 1;
            }
        } else if (CheckAngle() > 50f) {
            if (Timer()) {
                speed -= 4f + timeOnAngle;
                timeOnAngle += 1;
            }
        } else if (CheckAngle() > 40f) {
            if (Timer()) {
                speed -= 3f + timeOnAngle;
                timeOnAngle = 1;
            }
        } else if (CheckAngle() > 30f) {
            if (Timer()) {
                speed -= 2f;
                timeOnAngle -= 1;
            }
        } else if (CheckAngle() > 15f) {
            if (Timer()) {
                speed -= 1f;
                timeOnAngle -= 2;
            }
        } else {
            timeOnAngle = 1;
        }
        if (timeOnAngle < 1) {
            timeOnAngle = 1;
        }
    }


    // Check if we are past min or max speed
    void CheckSpeedBounds() {
        if (oldSpeed > MAX_SPEED) {
            oldSpeed = MAX_SPEED;
        } else if (oldSpeed < MIN_SPEED) {
            oldSpeed = MIN_SPEED;
        }
        if (speed > MAX_SPEED) {
            speed = MAX_SPEED;
        } else if (speed < MIN_SPEED) {
            speed = MIN_SPEED;
        }
    }

    // Check if we are on an angle and calculate speed accordingly
    void CheckSpeed() {
        if (inAir) {
            inAir = false;
            speed = oldSpeed;
        } else {
            if (Time.time >= regSpeedUpdate) {
                speed += 3.5f;
                regSpeedUpdate = Mathf.Floor(Time.time) + 1f;
            } else {
                ReduceSpeedAngle();
            }
        }
        oldSpeed = speed;
        CheckSpeedBounds();
    }

    //consider when character is jumping .. it will exit collision.
    void OnCollisionExit(Collision theCollision) {
        if (theCollision.gameObject.name == "Terrain") {
            isGrounded = false;
        }
    }

    // Returns angle of slope we are currently on (if on one)
    float CheckAngle() {
        Ray ray = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(ray, out hit)){
            return 180 - Vector3.Angle(hit.normal, Vector3.down);
        } else {
            return 0;
        }
    }

}
