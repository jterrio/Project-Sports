using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {


    public float zoom_Sensitivity = 15f;
    public float zoom_Speed = 30f;
    public float zoom_Min = 5f;
    public float zoom_Max = 20f;

    public float z;
    public Camera mainCamera;
    private CameraFollow cameraFollow;

    // Use this for initialization
    void Start() {
        cameraFollow = mainCamera.GetComponent<CameraFollow>();
        z = cameraFollow.camera_Lenght;
    }

    // Update is called once per frame
    void Update() {
        z -= Input.GetAxis("Mouse ScrollWheel") * zoom_Sensitivity;
        z = Mathf.Clamp(z, zoom_Min, zoom_Max);
    }

    void LateUpdate() {
        cameraFollow.camera_Lenght = Mathf.Lerp(cameraFollow.camera_Lenght, z, Time.deltaTime * zoom_Speed);
    }
}
