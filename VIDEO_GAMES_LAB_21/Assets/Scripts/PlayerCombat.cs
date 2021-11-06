using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    
    public bool isAttacking = false;
    
    private void Awake(){
        animator = gameObject.GetComponent<Animator>();
    }

    public void QuickAttack(InputAction.CallbackContext context){
        
        if(context.performed && !isAttacking) { 
               
            isAttacking = true; 
        }
    }

}
