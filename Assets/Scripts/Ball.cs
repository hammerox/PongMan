using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float xSpeed = 0.0f;
    public float ySpeed = 0.0f;
    public float speedMultiplier;
    public float rotationMultiplier;

    private Rigidbody2D rb;
    private bool waitingForInput = true;
    private GameObject[] instructions;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        instructions = GameObject.FindGameObjectsWithTag("Instruction");
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void FixedUpdate()
    {
        bool triggered = Input.GetKeyDown(KeyCode.Space);
        if (waitingForInput && triggered)
        {
            float xInput = Random.Range(-1.0f, 1.0f) * speedMultiplier;
            float yInput = -Mathf.Sqrt(1 - Mathf.Pow(xInput, 2)) * speedMultiplier;

            xSpeed = xSpeed + xInput;
            ySpeed = ySpeed + yInput;
            waitingForInput = false;
            InstructionsVisibility(false);
        }
        Vector2 speedVector = new Vector2(xSpeed, ySpeed);
        this.transform.position = new Vector2(transform.position.x + xSpeed, transform.position.y + ySpeed);
        this.transform.Rotate(new Vector3(0.0f, 0.0f, speedVector.magnitude * rotationMultiplier));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Wall"))
        {
            xSpeed = -xSpeed;

        }
        else if (tag.Equals("Goal"))
        {
            AddScore(collision);
            EndGame();

        }
        else if (tag.Equals("PongPad"))
        {
            ySpeed = -ySpeed;
            //var force = collision.contacts[0].normal;
            //rb.AddForce(force * 2);
        }
    }

    private void EndGame()
    {
        xSpeed = 0.0f;
        ySpeed = 0.0f;
        transform.position = new Vector2(0.0f, 0.0f);
        waitingForInput = true;
        InstructionsVisibility(true);
    }

    private void AddScore(Collision2D collision)
    {
        bool isTopGoal = collision.transform.position.y > 0;
        string opponentScore = (!isTopGoal) ? "Score Top" : "Score Bottom";
        GameObject.FindGameObjectWithTag(opponentScore).GetComponent<Score>().AddScore();
    }

    private void InstructionsVisibility(bool show)
    {
        foreach (GameObject instruction in instructions)
        {
            instruction.SetActive(show);
            //instruction.GetComponent<UnityEngine.UI.Text>().enabled = show;
        }
    }
}
