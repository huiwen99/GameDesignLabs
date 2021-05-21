using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Transform restartButton;

    void Awake()
    {
        Time.timeScale = 0.0f;
    }
    public void StartButtonClicked()
    {
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name != "Score")
            {
                Debug.Log("Child found. Name: " + eachChild.name);
                // disable them
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        restartButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
