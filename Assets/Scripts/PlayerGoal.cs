using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoal : MonoBehaviour {

    public GameObject canvas;
    // Start is called before the first frame update
    void Start() {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Goal") {
            canvas.SetActive(true);
        }
    }
}
