using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public bool canMove = true;
    public bool isStunned = false;
    private bool jumped = false;
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
            controller.Move(move * runSpeed * Time.fixedDeltaTime,false,jumped);
            

            
                
            // speed for animation
            animator.SetFloat("Speed", Mathf.Abs( move) );


            // Dust Effeck
            if(Mathf.Abs(move)>0){
                Debug.Log("createDust");
                createDust();
                    
            }

        }

        // IsFalling
        jumped = false;
        if( GetComponent<Rigidbody2D>().velocity.y <= -0.01f){
            falling = true;
        } 
    }   


    // receive input for move
    public void onMove(InputAction.CallbackContext context){

        move = new Vector2(context.ReadValue<Vector2>().x , 0).normalized.x;
        
    }

    // receive input for jump
    public void onJump(InputAction.CallbackContext context){

        if(context.action.triggered && canMove){
            jumped = true;
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
