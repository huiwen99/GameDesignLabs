using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D coinBody;
    AudioSource coinSound;

    // Start is called before the first frame update
    void Start()
    {
        coinBody = GetComponent<Rigidbody2D>();
        coinSound = GetComponent<AudioSource>();
        coinBody.AddForce(Vector2.up, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            coinSound.PlayOneShot(coinSound.clip);
            CentralManager.centralManagerInstance.increaseScore();
            CentralManager.centralManagerInstance.increaseNumSpawn();
            CentralManager.centralManagerInstance.newSpawn();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(finishSound());
        }
        

    }

    IEnumerator finishSound()
    {
        if (coinSound.isPlaying)
        {
            yield return new WaitUntil(() => !coinSound.isPlaying);
        }
        Destroy(gameObject);
    }

}
