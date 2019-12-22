using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
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

        rigidBody.freezeRotation = false;
    }

    private void HandleRotate()
    {
        HandleLeftRotation(); //not used, and held by rigidbody constraints
        HandleForwardRotation();
    }

    private void HandleForwardRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //print("Rotate right");
            transform.Rotate(Vector3.forward);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
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
            //print("Trusting");
            rigidBody.AddRelativeForce(Vector3.up);
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
}
