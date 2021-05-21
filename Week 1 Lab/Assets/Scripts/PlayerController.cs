using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 70;
    public float maxSpeed = 10;
    public float upSpeed = 25;

    public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
    private bool countScoreState = false;
    public Transform restartButton;
    public Transform panel;

    private bool onGroundState = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    private Rigidbody2D marioBody;
    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // toggle state
        if ((Input.GetKeyDown("a")||Input.GetKeyDown("left")) && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if ((Input.GetKeyDown("d")||Input.GetKeyDown("right")) && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
        }

        if (!onGroundState && countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score++;
                Debug.Log(score);
            }
        }

    }

    void FixedUpdate()
    {
        
        // dynamic rigidbody
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
                marioBody.AddForce(movement * speed);
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            // stop
            marioBody.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            countScoreState = true;
        }

    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGroundState = true; // back on ground
            countScoreState = false; // reset score state
            scoreText.text = "Score: " + score.ToString();
        };
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Time.timeScale = 0.0f;
            Debug.Log("Game Over!");
            panel.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }

}
