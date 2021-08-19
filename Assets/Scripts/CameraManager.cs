using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public GameObject player;
    public Vector3 additionalPosition;

    void Start() {
        
    }

    void Update() {
        this.transform.position = player.transform.position + additionalPosition;
    }
}
