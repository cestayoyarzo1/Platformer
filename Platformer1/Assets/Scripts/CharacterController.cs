using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float speed, movement, deltaMove, force;

    enum CharacterState
    {
        Idle,
        Running,
        Jumping
    }
    CharacterState mainstate = CharacterState.Idle;
    Animator animatorController;
    Rigidbody rBody;
    void Start()
    {
        animatorController = GetComponentInChildren<Animator>();
        rBody = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rBody.AddForce(force * Vector3.up);
        }
        if(Mathf.Abs(movement) > deltaMove)
        {
            Run();
        }
        else
        {
            Stop();
        }
        

        switch (mainstate)
        {
            case CharacterState.Idle:
                break;

            case CharacterState.Running:
                transform.Translate(Vector3.forward * speed * movement * Time.deltaTime);
                break;

            case CharacterState.Jumping:
                break;

            default:
                break;
        }


    }

    private void Stop()
    {
        if (mainstate != CharacterState.Idle)
        {
            mainstate = CharacterState.Idle;
            animatorController.SetBool("Run", false);
            animatorController.SetBool("Stop", true);
        }
    }

    void Run()
    {
        if(mainstate != CharacterState.Running)
        {
            mainstate = CharacterState.Running;
            animatorController.SetBool("Run", true);
            animatorController.SetBool("Stop", false);
        }

    }
}
