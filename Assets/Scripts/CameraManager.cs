using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour {

    public GameObject player;
    public Vector3 additionalPosition;
    public GameObject canvas;

    void Start() {
        
    }

    void Update() {
        this.transform.position = player.transform.position + additionalPosition;
        canvas.transform.position = this.transform.position + this.transform.forward * 3.0f;
        Quaternion CameraRot = this.transform.rotation;
        CameraRot.x = 0f;   // Canvas���΂߂ɂȂ�Ȃ��悤�ɒ���
        CameraRot.z = 0f;   // Canvas���΂߂ɂȂ�Ȃ��悤�ɒ���
        canvas.transform.rotation = CameraRot;
    }
}
