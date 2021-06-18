using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameConstants gameConstants;
    public float speed = 70;
    public float maxSpeed = 10;
    public float upSpeed = 25;

    //public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
    //private bool countScoreState = false;
    public Transform restartButton;
    public Transform panel;

    private bool onGroundState = true;
    private bool dead = false;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    private Rigidbody2D marioBody;

    private Animator marioAnimator;
    private AudioSource marioAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudioSource = GetComponent<AudioSource>();

        // subscribe to player event
        GameManager.OnPlayerDeath += PlayerDiesSequence;
    }

    void PlayerDiesSequence()
    {
        // Mario dies
        Debug.Log("Mario dies");
        marioAudioSource.clip = gameConstants.dieClip;
        marioAudioSource.PlayOneShot(marioAudioSource.clip);
        dead = true;
        speed = 0;
        marioBody.velocity = new Vector2(0, 0);

    }


    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            marioAnimator.SetBool("dead", dead);

        }
        else
        {
            if (Input.GetKeyDown("z"))
            {
                CentralManager.centralManagerInstance.consumePowerup(KeyCode.Z, this.gameObject);
            }

            if (Input.GetKeyDown("x"))
            {
                CentralManager.centralManagerInstance.consumePowerup(KeyCode.X, this.gameObject);
            }

            // toggle state
            if ((Input.GetKeyDown("a") || Input.GetKeyDown("left")) && faceRightState)
            {
                faceRightState = false;
                marioSprite.flipX = true;
                if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                    marioAnimator.SetTrigger("onSkid");
            }

            if ((Input.GetKeyDown("d") || Input.GetKeyDown("right")) && !faceRightState)
            {
                faceRightState = true;
                marioSprite.flipX = false;
                if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                    marioAnimator.SetTrigger("onSkid");
            }

            marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));

        }


        //if (!onGroundState && countScoreState)
        //{
        //    if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
        //    {
        //        countScoreState = false;
        //        score++;
        //        Debug.Log(score);
        //    }
        //}

    }

    void FixedUpdate()
    {
        if (dead&& onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            marioBody.GetComponent<Collider2D>().enabled = false;
        }
        else
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
                marioAnimator.SetBool("onGround", onGroundState);

                //countScoreState = true;

            }
        }
        

    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGroundState = true; // back on ground
            marioAnimator.SetBool("onGround", onGroundState);
            //countScoreState = false; // reset score state
            scoreText.text = "Score: " + score.ToString();
        };
        if ((col.gameObject.CompareTag("Obstacles")||col.gameObject.CompareTag("Pipe")) && Mathf.Abs(marioBody.velocity.y) < 0.01f)
        {
            onGroundState = true; // back on ground
            marioAnimator.SetBool("onGround", onGroundState);

        }

    }
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Enemy"))
    //    {
    //        Time.timeScale = 0.0f;
    //        Debug.Log("Game Over!");
    //        panel.gameObject.SetActive(true);
    //        restartButton.gameObject.SetActive(true);
    //    }
    //}

    void PlayJumpSound()
    {
        marioAudioSource.PlayOneShot(marioAudioSource.clip);
    }

}
