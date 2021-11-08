using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public bool canMove = true;
    public bool isStunned = false;
    private bool jumpedForMove = false;
    private bool groundedPlayer;
    private bool falling = false;

    float move = 0.0f;      // get controller axis
    float runSpeed = 40.0f;


    public Animator animator;
    public ParticleSystem dustPS;




    void FixedUpdate(){

        if(!canMove || isStunned){

            controller.Move(0, false, false);
            animator.SetFloat("Speed", 0f );
            

        }else{
            controller.Move(move * runSpeed * Time.fixedDeltaTime,false,jumpedForMove);
            

            
                
            // speed for animation
            animator.SetFloat("Speed", Mathf.Abs( move) );


            // Dust Effeck
            if(Mathf.Abs(move)>0){
                createDust();
            }

        }
        jumpedForMove = false;

        // IsFalling
        Rigidbody2D rb = GetComponent<Rigidbody2D>();        
        if( rb.velocity.y <= -0.0001f ){
            falling = true;
        } 

    }   


    // receive input for move
    public void onMove(InputAction.CallbackContext context){

        Vector2 input = context.ReadValue<Vector2>();

        if(Mathf.Abs(input.x) > 0.6f){

            move = input.normalized.x;

        }else{
            
            move = 0;
        }
        
        
    }

    // receive input for jump
    public void onJump(InputAction.CallbackContext context){

        if(context.action.triggered && canMove){

            jumpedForMove = true;
            animator.SetBool("IsJumping", true);
            createDust();

        }
        
    }

    // when player touch the floor
    public void OnLand(){
        if(falling){

            animator.SetBool("IsJumping", false);
            falling = false;
        }
        
    }


    public void Stun(){
        isStunned = true;

    }

    public void StopStun(){
        isStunned = false;
    }


    public void createDust(){
        if(!dustPS.isPlaying){
          dustPS.Play();
        }   
    }
}
