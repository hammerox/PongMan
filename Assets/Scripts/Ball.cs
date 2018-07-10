using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb;
    private GameObject[] instructions;
    private Vector2 velocityBeforeCollision;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        instructions = GameObject.FindGameObjectsWithTag("Instruction");
        NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        bool playerStartedGame = Input.GetKeyDown(KeyCode.Space);
        if (playerStartedGame)
        {
            // Initial random move
            float xInput = Random.Range(-1.0f, 1.0f);
            float yInput = -Mathf.Sqrt(1 - Mathf.Pow(xInput, 2));
            Vector2 initialForce = new Vector2(xInput * speed, yInput * speed);
            rb.AddForce(initialForce);

            ShowInstructions(false);
        }

        velocityBeforeCollision = rb.velocity;
    }

    private void FixedUpdate()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Wall"))
        {
            BounceFrom(collision);

        }
        else if (tag.Equals("PongPad"))
        {
            BounceFrom(collision);

        }
        else if (tag.Equals("Goal"))
        {
            AddScore(collision);
            NewGame();
        }
    }

    private void BounceFrom(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 reflection = Vector2.Reflect(velocityBeforeCollision, contact.normal);
            this.rb.velocity = reflection;
        }
    }

    private void NewGame()
    {
        rb.velocity = new Vector2(0.0f, 0.0f);
        rb.angularVelocity = 0.0f;
        transform.position = new Vector2(0.0f, 0.0f);
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        ShowInstructions(true);
    }

    private void AddScore(Collision2D collision)
    {
        bool isTopGoal = collision.transform.position.y > 0;
        string opponentScore = (!isTopGoal) ? "Score Top" : "Score Bottom";
        GameObject.FindGameObjectWithTag(opponentScore).GetComponent<Score>().AddScore();
    }

    private void ShowInstructions(bool show)
    {
        foreach (GameObject instruction in instructions)
        {
            instruction.SetActive(show);
        }
    }
}
