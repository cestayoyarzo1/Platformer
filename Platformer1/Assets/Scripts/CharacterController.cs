using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float speed, movement, deltaMove, force, rotationSpeed;

    enum CharacterState
    {
        Idle,
        RunningForward,
        RunningBack,
        Jumping
    }
    CharacterState mainstate = CharacterState.Idle;
    Animator animatorController;
    Rigidbody rBody;
    void Start()
    {
        animatorController = GetComponentInChildren<Animator>();
        rBody = GetComponentInChildren<Rigidbody>();
        EventManager.Instance.onBoardInteraction.AddListener(CommandReceived);
    }

    private void CommandReceived(GameObject sender, CustomEventArgs args)
    {
        if (args.BoardCommand.Equals("@SU1"))
        {
            print("UP pressed");
        }
        
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

            case CharacterState.RunningForward:
                transform.localRotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * speed * movement * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                break;

            case CharacterState.RunningBack:
                transform.localRotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -90, 0), rotationSpeed * Time.deltaTime);
                transform.Translate(-Vector3.forward * speed * movement * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
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
        if(movement > 0)
        {
            if (mainstate != CharacterState.RunningForward)
            {
                mainstate = CharacterState.RunningForward;
                animatorController.SetBool("Run", true);
                animatorController.SetBool("Stop", false);
            }
        }
        else if ((movement < 0))
        {
            if (mainstate != CharacterState.RunningBack)
            {
                mainstate = CharacterState.RunningBack;
                animatorController.SetBool("Run", true);
                animatorController.SetBool("Stop", false);
            }
        }


    }
}
