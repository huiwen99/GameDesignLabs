using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    private bool broken = false;
    public GameObject prefab;
    private AudioSource breakAudioSource;
    public GameConstants gameConstants;
    public GameObject spawnedCoin; // the spawned coin prefab


    // Start is called before the first frame update
    void Start()
    {
        breakAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !broken)
        {
            broken = true;
            breakAudioSource.PlayOneShot(breakAudioSource.clip);

            Instantiate(spawnedCoin, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z), Quaternion.identity);

            for (int x = 0; x < gameConstants.spawnNumberOfDebris; x++)
            {
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<EdgeCollider2D>().enabled = false;

            StartCoroutine(finishSound());
        }
    }

    IEnumerator finishSound()
    {
        if (breakAudioSource.isPlaying)
        {
            yield return new WaitUntil(() => !breakAudioSource.isPlaying);
        }
        Destroy(gameObject.transform.parent.gameObject);
    }


}
