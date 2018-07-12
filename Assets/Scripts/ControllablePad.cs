using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllablePad : MonoBehaviour {

    public float speedMultiplier;

    private float minPosition;
    private float maxPosition;
    private Collider2D collider;

    // Use this for initialization
    void Start () {
        collider = GetComponent<Collider2D>();
        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;
        minPosition = -horzExtent + collider.bounds.extents.x;
        maxPosition = horzExtent - collider.bounds.extents.x;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal") * speedMultiplier;
        float nextPosition = xInput + transform.position.x;
        print(nextPosition);
        bool isLeftOfScreen = nextPosition < minPosition;
        bool isRightOfScreen = nextPosition > maxPosition;

        if (isLeftOfScreen)
        {
            // Hit left wall
            transform.position = new Vector2(minPosition, transform.position.y);

        } else if (isRightOfScreen)
        {
            // Hit right wall
            transform.position = new Vector2(maxPosition, transform.position.y);

        } else
        {
            // Move normally
            transform.Translate(new Vector2(xInput, 0.0f));
        }
    }
}
