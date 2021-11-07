using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public bool canMove = true;
    public bool isStunned = false;

    float move = 0.0f;
    float runSpeed = 40.0f;

    private bool jumped = false;
    private bool groundedPlayer;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){

        if(!canMove || isStunned){

            controller.Move(0, false, false);
            animator.SetFloat("Speed", 0f );
            return;

        }else{

            controller.Move(move * runSpeed * Time.fixedDeltaTime,false,jumped);
            animator.SetFloat("Speed", Mathf.Abs( move) );

        }
    }   


    
    public void onMove(InputAction.CallbackContext context){

        move = new Vector2(context.ReadValue<Vector2>().x , 0).normalized.x;
        
    }

    public void onJump(InputAction.CallbackContext context){

        jumped = context.action.triggered;
        animator.SetBool("IsJumping", true);
        
    }


    public void OnLand(){
        animator.SetBool("IsJumping", false);
    }


    public void Stun(){
        isStunned = true;

    }

    public void StopStun(){
        isStunned = false;
    }
}
