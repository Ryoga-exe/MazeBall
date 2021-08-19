using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Space)) {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 6, 0), ForceMode.Impulse);
        }
        
    }
}
