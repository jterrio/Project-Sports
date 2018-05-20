using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public SphericalVector spherical_Vector_Data = new SphericalVector(0, 0, 1);
    protected virtual void Update() {
        transform.position = spherical_Vector_Data.Position;
    }
}