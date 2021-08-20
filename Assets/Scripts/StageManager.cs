using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Net;

public class StageManager : MonoBehaviour {

    public float rSpeed = 0.5f;
    public int maxAbsAngle = 20;

    bool useKeyboard = true;
    float currentRotateX = 0.0f;
    float currentRotateZ = 0.0f;

    WebSocket ws;
    string dx = "0", dy = "0", dz = "0";
    string host = "ws://localhost:8080/"; // please change here to your environment

    void Start() {
        if (!useKeyboard) {
            ws = new WebSocket(host);
            ws.OnOpen += (sender, e) => {
                Debug.Log("WebSocket Open");
            };

            ws.OnMessage += (sender, e) => {
                string data = e.Data;
                dx = data.Substring(1, data.IndexOf("y") - 1);
                dy = data.Substring(data.IndexOf("y") + 1);
                dy = dy.Substring(0, dy.IndexOf("z") - 1);
                dz = data.Substring(data.IndexOf("z") + 1);

            };

            ws.OnError += (sender, e) => {
                Debug.Log("WebSocket Error Message: " + e.Message);
            };

            ws.OnClose += (sender, e) => {
                Debug.Log("WebSocket Close");
            };

            ws.Connect();
        }
    }

    void FixedUpdate() {
        
        if (useKeyboard) {

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
        else {
            float x = -float.Parse(double.Parse(dz).ToString("F4"));
            float y = -float.Parse((double.Parse(dx) - 90).ToString("F4"));
            float z = float.Parse(double.Parse(dy).ToString("F4")) - 30;

            Debug.Log(x);
            Debug.Log(y);
            Debug.Log(z);

            Quaternion rotation = Quaternion.Euler(x, y, z);
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rotation, .25f);
        }
    }
    
    void OnDestroy() {
        if (!useKeyboard) {
            ws.Close();
            ws = null;
        }
    }
}
