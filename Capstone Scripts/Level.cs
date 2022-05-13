using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerInventory playerInventory = GetComponent<PlayerInventory>();

        if(playerInventory.gemCount == 10)
        {
            StartSuccessSequence();
        }



        if (Debug.isDebugBuild)
        {
            RespondToDebugBuild();
        }
    }

    private void RespondToDebugBuild()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }
    }

    private void StartSuccessSequence()
    {
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        print("Scene Count " + currentSceneIndex);
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        print("Next Scene Index " + nextSceneIndex);
        SceneManager.LoadScene(nextSceneIndex);
    }
}
