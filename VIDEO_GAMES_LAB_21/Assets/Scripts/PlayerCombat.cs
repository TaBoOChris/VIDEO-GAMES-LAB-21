using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    
    public bool isAttacking = false;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask stickLayers;
    
    private void Awake(){
        animator = gameObject.GetComponent<Animator>();

    }

    public void QuickAttack(InputAction.CallbackContext context){
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,stickLayers);
        Debug.Log(hitEnemies.Length);
     
        foreach ( Collider2D enemy in hitEnemies){
            Debug.Log("hit " + enemy.name);
        } 

        if(context.performed && !isAttacking) { 
               
            isAttacking = true; 

            
        }
    }


    void OnDrawGizmosSelected(){
        if(attackPoint != null){
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

}
