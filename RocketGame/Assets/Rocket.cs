using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100.0f;
    [SerializeField] float mainThrust = 1.0f;

    enum State { Alive, Dying, Transcending}
    State state = State.Alive;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO *turn off thrusting sound if not alive*
        if (state != State.Alive) return;
        Thrusting();
        Rotate();
    }


    private void Rotate()
    {
        rigidBody.freezeRotation = true;
        float rotateSize = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotateSize);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotateSize);
        }
        rigidBody.freezeRotation = false;
    }

    private void Thrusting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
            if (!audioSource.isPlaying) // checks if audio is playing
            {
                audioSource.Play();
            }
        }
        else // if audio is alread playing, it stops.
        {
            audioSource.Stop();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) return;
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Friendly");
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextScene", 3f);
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstLevel", 3f);
                break;
        }
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
        state = State.Alive;
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
        state = State.Alive;
    }
}
