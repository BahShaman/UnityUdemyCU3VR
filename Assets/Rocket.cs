using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    Rigidbody rigidBody;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.freezeRotation = true;

        HandleThrust();
        HandleRotate();
        CleanYRotation();

        rigidBody.freezeRotation = false;
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
        HandleForwardRotation();
        HandleLeftRotation(); //not used, and held by rigidbody constraints
    }

    private void HandleForwardRotation()
    {

        float rotationThisFrame = Time.deltaTime * rcsThrust;
        if (Input.GetKey(KeyCode.A))
        {
            //print("Rotate right");
            transform.Rotate(Vector3.forward * rotationThisFrame);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
            //print("Rotate left");
        }
    }

    private void HandleLeftRotation()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //print("Rotate forward");
            transform.Rotate(Vector3.left);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(-Vector3.left);
            //print("Rotate back");
        }
    }

    private void HandleThrust()
    {
        if (Input.GetKey(KeyCode.Space)) //can thrust while rotating
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

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("collided Friendly");
                break;
            case "Fuel":
                print("Refueled");
                break;
            default:
                HandleExplode();
                break;
        }
    }

    private void HandleExplode()
    {
        print("Exploding");
        //nada
    }
}
