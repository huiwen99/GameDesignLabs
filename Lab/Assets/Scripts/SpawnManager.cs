using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    float minSpawnDist = 2.0f;
    private void Awake()
    {
        // spawn two gombaEnemy
        for (int j = 0; j < 2; j++)
            spawnFromPooler(ObjectType.gombaEnemy);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.SpawnEnemy += Spawn;
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void Spawn()
    {
        ObjectType i = (ObjectType) Random.Range(0,2);
        spawnFromPooler(i);
        
    }

    void spawnFromPooler(ObjectType i)
    {
        // static method access
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item != null)
        {
            //set position, and other necessary states
            item.transform.localScale = new Vector3(1, 1, 1);
            GameObject mario = GameObject.Find("Mario");
            float x = Random.Range(-3f, 3f);
            if (Mathf.Abs(x - mario.transform.position.x) < minSpawnDist)
            {
                if (x< mario.transform.position.x)
                {
                    x = mario.transform.position.x + minSpawnDist;
                }
                else
                {
                    x = mario.transform.position.x - minSpawnDist;
                }
            }
            item.transform.position = new Vector3(x, -3.5f, 0);
            item.SetActive(true);
        }
        else
        {
            Debug.Log("not enough items in the pool.");
        }
    }
}
