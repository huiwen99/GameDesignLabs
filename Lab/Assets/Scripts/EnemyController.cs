using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameConstants gameConstants;
    private float originalX;
    private int moveRight;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;
    private SpriteRenderer enemySprite;
    private AudioSource stompSound;

    // Start is called before the first frame update
    void Start()
    {
        // to move enemy
        enemyBody = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        stompSound = GetComponent<AudioSource>();
        
        // get the starting position
        originalX = transform.position.x;

        moveRight = Random.Range(0, 2) == 0 ? -1 : 1;

        ComputeVelocity();

        // subscribe to player event
        GameManager.OnPlayerDeath += EnemyRejoice;

    }
    // animation when player is dead
    void EnemyRejoice()
    {
        Debug.Log("Enemy killed Mario");
        
        StartCoroutine(rejoiceDance());
        
    }

    IEnumerator rejoiceDance()
    {
        moveRight = 0;
        velocity = new Vector2(0, 0);
        for (int i = 0; i < 5; i++)
        {
            enemySprite.flipX = true;
            yield return new WaitForSeconds(0.2f);
            enemySprite.flipX = false;
            yield return new WaitForSeconds(0.2f);
        }
        
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * gameConstants.maxOffset / gameConstants.enemyPatroltime, 0);
        
    }
    void MoveEnemy()
    {
       enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < gameConstants.maxOffset)
        {
            // move enemy
            MoveEnemy();
        }
        else
        {
            if (moveRight != 0)
            {
                if (enemyBody.position.x - originalX > gameConstants.maxOffset)
                {
                    moveRight = -1;
                }
                else
                {
                    moveRight = 1;
                }

                ComputeVelocity();
                MoveEnemy();
            }
            
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // check if it collides with Mario
        if (other.gameObject.tag == "Player")
        {
            // check if collides on top
            float yoffset = (other.transform.position.y - this.transform.position.y);
            if (yoffset > 0.75f)
            {
                KillSelf();
                Debug.Log("Killed");
                CentralManager.centralManagerInstance.newSpawn();
                
            }
            else
            {
                // hurt player
                CentralManager.centralManagerInstance.damagePlayer();
            }
        }
    }
    void KillSelf()
    {
        // enemy dies
        stompSound.PlayOneShot(stompSound.clip);
        CentralManager.centralManagerInstance.increaseScore();
        StartCoroutine(flatten());
        Debug.Log("Kill sequence ends");
    }
    IEnumerator flatten()
    {
        Debug.Log("Flatten starts");
        int steps = 5;
        float stepper = 1.0f / (float)steps;

        for (int i = 0; i < steps; i++)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - stepper, this.transform.localScale.z);

            // make sure enemy is still above ground
            this.transform.position = new Vector3(this.transform.position.x, gameConstants.groundSurface + GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
            yield return null;
        }
        Debug.Log("Flatten ends");
        this.gameObject.SetActive(false);
        Debug.Log("Enemy returned to pool");
        yield break;
    }

    private void OnDestroy()
    {
        GameManager.OnPlayerDeath -= EnemyRejoice;
    }
}
