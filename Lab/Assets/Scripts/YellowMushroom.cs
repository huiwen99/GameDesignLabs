using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowMushroom : MonoBehaviour, ConsumableInterface
{
	public Texture t;
	private Rigidbody2D mushroomBody;
	public float upwardFactor = 15.0f;
	public float speed = 5.0f;
	private Vector2 currentDirection;
	private Vector3 scaler;
	// Start is called before the first frame update
	void Start()
	{
		mushroomBody = GetComponent<Rigidbody2D>();

		currentDirection = new Vector2(1.0f, 0.0f);

		mushroomBody.AddForce(Vector2.up * upwardFactor, ForceMode2D.Impulse);

		scaler = transform.localScale / (float)5;

	}
	private void FixedUpdate()
	{
		mushroomBody.velocity = currentDirection * speed + new Vector2(0, mushroomBody.velocity.y);

	}
	public void consumedBy(GameObject player)
	{
		// give player jump boost
		player.GetComponent<PlayerController>().upSpeed += 10;
		StartCoroutine(removeEffect(player));
	}

	IEnumerator removeEffect(GameObject player)
	{
		yield return new WaitForSeconds(5.0f);
		player.GetComponent<PlayerController>().upSpeed -= 10;
	}
	


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
			// update UI
			GetComponent<Collider2D>().enabled = false;
			CentralManager.centralManagerInstance.addPowerup(t, 1, this);
			
			StartCoroutine("Scale");
		}
		if (col.gameObject.CompareTag("Pipe"))
		{
			//change direction
			currentDirection *= -1.0f;
		}

	}

	IEnumerator Scale()
	{
		// render for 0.5 second
		for (int step = 0; step < 5; step++)
		{
			this.transform.localScale = this.transform.localScale - scaler;
			// wait for next frame
			yield return null;
		}


	}

}
