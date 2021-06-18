using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this has methods callable by players
public class CentralManager : MonoBehaviour
{
	public GameObject gameManagerObject;
	private GameManager gameManager;
	public static CentralManager centralManagerInstance;
	// add reference to PowerupManager
	public GameObject powerupManagerObject;
	private PowerupManager powerUpManager;
	public GameObject enemyPoolObject;
	private ObjectPooler objectPooler;

	void Awake()
	{
		centralManagerInstance = this;
	}
	// Start is called before the first frame update
	void Start()
	{
		gameManager = gameManagerObject.GetComponent<GameManager>();
		powerUpManager = powerupManagerObject.GetComponent<PowerupManager>();
		objectPooler = enemyPoolObject.GetComponent<ObjectPooler>();

	}

	public void consumePowerup(KeyCode k, GameObject g)
	{
		powerUpManager.consumePowerup(k, g);
	}

	public void addPowerup(Texture t, int i, ConsumableInterface c)
	{
		powerUpManager.addPowerup(t, i, c);
	}

	public void increaseScore()
	{
		Debug.Log("Increase score");
		gameManager.increaseScore();
	}

	public void damagePlayer()
	{
		gameManager.damagePlayer();
	}

	public void newSpawn()
    {
		gameManager.newSpawn();
    }

	public void increaseNumSpawn()
    {
		objectPooler.increaseNumSpawn();
    }
}