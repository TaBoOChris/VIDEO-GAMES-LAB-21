using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public float percentage = 0.0f;
    
    public bool isAttacking = false;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask stickLayers;
    
    private void Awake(){
        animator = gameObject.GetComponent<Animator>();

    }

    public void QuickAttack(InputAction.CallbackContext context){
        
        if(context.performed != true){
            return;
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,stickLayers);
     
        foreach ( Collider2D enemy in hitEnemies){
            if(enemy.gameObject != gameObject){

                Debug.Log("hit " + enemy.name);

                float damageImpact = 20.0f;

                enemy.GetComponent<PlayerCombat>().TakeDamage(damageImpact , attackPoint.position);
               
            }
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

    public void TakeDamage(float percent , Vector3 DamageImpactPosition){


        Vector2 forceDirection = this.gameObject.transform.position - DamageImpactPosition;
        forceDirection = forceDirection.normalized;
        gameObject.GetComponent<Rigidbody2D>().AddForce(forceDirection * percent + forceDirection*percentage);


        percentage += percent;

        Debug.Log("current percent : " + percentage);
        // play hurt anim
    }

}
