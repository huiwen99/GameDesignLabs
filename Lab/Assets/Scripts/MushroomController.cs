using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    private Rigidbody2D mushroomBody;
    public float upwardFactor = 15.0f;
    public float speed = 5.0f;
    private Vector2 currentDirection;

    // Start is called before the first frame update
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();

        currentDirection = new Vector2(1.0f, 0.0f);

        mushroomBody.AddForce(Vector2.up * upwardFactor, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        mushroomBody.velocity = currentDirection * speed + new Vector2(0, mushroomBody.velocity.y);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col);

        if (col.gameObject.CompareTag("Pipe"))
        {
            //change direction
            currentDirection *= -1.0f;
        }

        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }


}
