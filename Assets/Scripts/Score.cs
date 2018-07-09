using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    public int value = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddScore()
    {
        value++;
        this.GetComponent<UnityEngine.UI.Text>().text = value.ToString();
    }
}
