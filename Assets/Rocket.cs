using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    int currentLevel = 0;

    Rigidbody rigidBody;
    AudioSource audioSource;
    
    enum  State { Alive, Dying, Transcending};
    State state = State.Alive;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Alive)
        {
            HandleReset();
            HandleThrust();
            HandleRotate();
            CleanYRotation();
        }
    }

    private void HandleReset()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            LoadFirstLevel();
        }
    }

    private void CleanYRotation()
    {
        Quaternion currentQuat = transform.rotation;
            
        //print(currentQuat);
        currentQuat.y = 0f;
        transform.rotation = currentQuat;
        //transform.rotation = Quaternion.AngleAxis(0f, Vector3.up);   
    }

    private void HandleRotate()
    {
        HandleLevelInput();
        //HandleAllAxisRotation();
        HandleAxisRotation();
        HandleAxisRotationLeft();
        HandleForwardRotation();
        HandleLeftRotation(); //not used, and held by rigidbody constraints
    }

    private void HandleLevelInput()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(2);
        }
    }

    private void HandleAllAxisRotation()
    {
        rigidBody.freezeRotation = true;
        transform.localRotation = UnityEngine.XR.InputTracking.GetLocalRotation(UnityEngine.XR.XRNode.GameController);
        rigidBody.freezeRotation = false;
    }


    private void HandleAxisRotation()
    {
        float rotation = Input.GetAxis("Horizontal");
        float rotationThisFrame = Time.deltaTime * rotation;
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame);
        rigidBody.freezeRotation = false;
    }

    private void HandleAxisRotationLeft()
    {
        float rotation = Input.GetAxis("Horizontal");
        float rotationThisFrame = Time.deltaTime * rotation;
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.left * rotationThisFrame);
        rigidBody.freezeRotation = false;
    }

    private void HandleForwardRotation()
    {

        float rotationThisFrame = Time.deltaTime * rcsThrust;
        if (Input.GetKey(KeyCode.A))
        {
            //print("Rotate right");
            rigidBody.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotationThisFrame);
            rigidBody.freezeRotation = false;

        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidBody.freezeRotation = true;
            transform.Rotate(-Vector3.forward * rotationThisFrame);
            rigidBody.freezeRotation = false;
            //print("Rotate left");
        }
    }

    private void HandleLeftRotation()
    {
        float rotationThisFrame = Time.deltaTime * rcsThrust;
        if (Input.GetKey(KeyCode.W))
        {
            //print("Rotate forward");
            rigidBody.freezeRotation = true;
            transform.Rotate(Vector3.left * rotationThisFrame);
            rigidBody.freezeRotation = false;

        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigidBody.freezeRotation = true;
            transform.Rotate(-Vector3.left * rotationThisFrame);
            rigidBody.freezeRotation = false;
            //print("Rotate back");
        }
    }

    private void HandleThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button15)) //can thrust while rotating
        {
            float thrustThisFrame = Time.deltaTime * mainThrust;
            //print("Thrusting");
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void LoadNextScene()
    {
        if (currentLevel == 1)
        {
            currentLevel = 2;
        }
        else
        {
            currentLevel++;
        }
        SceneManager.LoadScene(currentLevel);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("collided Friendly");
                break;
            case "Fuel":
                print("Refueled");
                break;
            case "Finish":
                print("Finish");
                state = State.Transcending;
                Invoke("LoadNextScene", 1f);
                break;
            default:
                //state = State.Dying;
                //HandleExplode();
                //Invoke("LoadFirstLevel", 1f);
                break;
        }
    }

    private void HandleExplode()
    {
        print("Exploding");
        //nada
    }
}
