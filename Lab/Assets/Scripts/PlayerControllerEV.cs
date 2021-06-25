using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControllerEV : MonoBehaviour
{
    private float force;
    public IntVariable marioUpSpeed;
    public IntVariable marioMaxSpeed;
    public GameConstants gameConstants;

    private bool faceRightState = true;
    private bool isDead = false;
    private bool isADKey = false;
    private bool isSpacebar = false;
    private Rigidbody2D marioBody;
    private bool onGroundState = false;
    private Animator marioAnimator;
    private AudioSource marioAudioSource;
    private SpriteRenderer marioSprite;

    public UnityEvent castOrange;
    public UnityEvent castYellow;

    // Start is called before the first frame update
    void Start()
    {
        marioUpSpeed.SetValue(gameConstants.playerMaxJumpSpeed);
        marioMaxSpeed.SetValue(gameConstants.playerMaxSpeed);
        force = gameConstants.playerDefaultForce;

        marioBody = GetComponent<Rigidbody2D>();
        marioAnimator = GetComponent<Animator>();
        marioAudioSource = GetComponent<AudioSource>();
        marioSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown("a") || Input.GetKeyDown("left")))
        {
            if (faceRightState)
            {
                faceRightState = false;
                marioSprite.flipX = true;
            }
            
            isADKey = true;
            
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                marioAnimator.SetTrigger("onSkid");
        }
        else if((Input.GetKeyDown("d") || Input.GetKeyDown("right")))
        {
            if (!faceRightState)
            {
                faceRightState = true;
                marioSprite.flipX = false;
            }
            
            isADKey = true;
            
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                marioAnimator.SetTrigger("onSkid");
        }
        else if((Input.GetKeyUp("a") || Input.GetKeyUp("left")) && !faceRightState)
        {
            isADKey = false;
        }
        else if ((Input.GetKeyUp("d") || Input.GetKeyUp("right")) && faceRightState)
        {
            isADKey = false;
        }

        if (Input.GetKeyDown("space"))
        {
            isSpacebar = true;
        }
        else if (Input.GetKeyUp("space"))
        {
            isSpacebar = false;
        }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));

        if (Input.GetKeyDown("z"))
        {
            castOrange.Invoke();
        }
        if (Input.GetKeyDown("x"))
        {
            castYellow.Invoke();
        }
    }
    void FixedUpdate()
    {
        if (!isDead)
        {
            if (isSpacebar && onGroundState)
            {
                marioBody.AddForce(Vector2.up * marioUpSpeed.Value, ForceMode2D.Impulse);
                onGroundState = false;
                // part 2
                marioAnimator.SetBool("onGround", onGroundState);
                //countScoreState = true; //check if Gomba is underneath
            }
            //check if a or d is pressed currently
            if (isADKey)
            {
                float direction = faceRightState ? 1.0f : -1.0f;
                Vector2 movement = new Vector2(force * direction, 0);
                if (marioBody.velocity.magnitude < marioMaxSpeed.Value)
                    marioBody.AddForce(movement);
            }

            
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGroundState = true; // back on ground
            marioAnimator.SetBool("onGround", onGroundState);
        };
        if ((col.gameObject.CompareTag("Obstacles") || col.gameObject.CompareTag("Pipe")) && Mathf.Abs(marioBody.velocity.y) < 0.01f)
        {
            onGroundState = true; // back on ground
            marioAnimator.SetBool("onGround", onGroundState);

        }
    }

    public void PlayerDiesSequence()
    {
        isDead = true;
        marioAudioSource.clip = gameConstants.dieClip;
        marioAudioSource.PlayOneShot(marioAudioSource.clip);
        marioBody.velocity = new Vector2(0, 0);
        marioAnimator.SetBool("dead", true);
        GetComponent<Collider2D>().enabled = false;
        marioBody.AddForce(Vector3.up * 30, ForceMode2D.Impulse);
        marioBody.gravityScale = 9;
        StartCoroutine(dead());
    }

    IEnumerator dead()
    {
        yield return new WaitForSeconds(1.0f);
        marioBody.bodyType = RigidbodyType2D.Static;
    }

    void PlayJumpSound()
    {
        marioAudioSource.PlayOneShot(marioAudioSource.clip);
    }
}
