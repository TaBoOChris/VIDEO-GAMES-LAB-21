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

    float move = 0.0f;      // get controller axis
    float runSpeed = 40.0f;


    public Animator animator;
    public ParticleSystem dustPS;




    void FixedUpdate(){

        if(!canMove || isStunned){

            controller.Move(0, false, false);
            animator.SetFloat("Speed", 0f );
            return;

        }else{
            controller.Move(move * runSpeed * Time.fixedDeltaTime,false,jumped);
            animator.SetFloat("Speed", Mathf.Abs( move) );

            if(Mathf.Abs(move)>0){
                Debug.Log("createDust");
                createDust();
                    
            }

        }
    }   


    
    public void onMove(InputAction.CallbackContext context){

        move = new Vector2(context.ReadValue<Vector2>().x , 0).normalized.x;
        
    }

    public void onJump(InputAction.CallbackContext context){

        jumped = context.action.triggered;
        animator.SetBool("IsJumping", true);
        createDust();
        
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


    public void createDust(){
        if(!dustPS.isPlaying){
          dustPS.Play();
        }   
    }
}
