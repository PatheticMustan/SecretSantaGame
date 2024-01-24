using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public PlayerController pc;
    public float movementDistanceMultiplier;
    public float cameraSpeedMultiplier;

    void Start() {

    }

    void Update() {
        transform.position = Vector3.Lerp(transform.position, pc.transform.position + new Vector3(pc.GetControls().x*movementDistanceMultiplier, 0, -10), cameraSpeedMultiplier * Time.deltaTime);
    }
}