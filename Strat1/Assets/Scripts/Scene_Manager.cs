using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
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
        if(Input.GetKeyDown(KeyCode.K))
            GoToNextScene();
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene("1");
    }

    public void GoToNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
