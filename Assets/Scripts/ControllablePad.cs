using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllablePad : MonoBehaviour {

    public float speedMultiplier;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal") * speedMultiplier;
        transform.Translate(new Vector2(xInput * speedMultiplier, 0.0f));
    }
}
