using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	public void changeScene()
	{
		StartCoroutine(LoadYourAsyncScene("MarioLevel2"));
	}


	IEnumerator LoadYourAsyncScene(string sceneName)
	{
		// The Application loads the Scene in the background as the current Scene runs.
		// This is particularly good for creating loading screens.
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
}