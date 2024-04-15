using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject startButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton = GameObject.Find("StartButton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene("1");
    }
}
