using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level : MonoBehaviour
{
    AudioSource audioSource;
    bool collisionDisabled = false;

    [SerializeField] AudioClip death;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem successParticles;

    [SerializeField] float levelLoadDelay = 3.0f;

    public enum State { Alive, Dying, Transcending }
    public static State state = State.Alive;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
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
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || collisionDisabled) return;
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }
    private void StartDeathSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        state = State.Dying;
        Invoke("LoadFirstLevel", levelLoadDelay);
    }

    private void StartSuccessSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        state = State.Transcending;
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void LoadFirstLevel()
    {
        state = State.Alive;
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        state = State.Alive;
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
