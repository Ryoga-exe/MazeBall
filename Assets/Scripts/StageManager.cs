using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    public float rSpeed = 0.5f;
    public int maxAbsAngle = 20;

    float currentRotateX = 0.0f;
    float currentRotateZ = 0.0f;

    void Start() {

    }


    void FixedUpdate() {

        bool isMovingX = false;
        bool isMovingZ = false;

        if (Input.GetKey(KeyCode.RightArrow)) {
            currentRotateX += rSpeed;
            isMovingX = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            currentRotateX -= rSpeed;
            isMovingX = true;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            currentRotateZ += rSpeed;
            isMovingZ = true;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            currentRotateZ -= rSpeed;
            isMovingZ = true;
        }
        if (!isMovingX) currentRotateX = Mathf.Lerp(currentRotateX, 0, .4f);
        if (!isMovingZ) currentRotateZ = Mathf.Lerp(currentRotateZ, 0, .4f);

        currentRotateX = Mathf.Clamp(currentRotateX, -maxAbsAngle, maxAbsAngle);
        currentRotateZ = Mathf.Clamp(currentRotateZ, -maxAbsAngle, maxAbsAngle);

        Quaternion rotation = Quaternion.Euler(currentRotateX, 0, currentRotateZ);

        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rotation, .25f);

    }
}
