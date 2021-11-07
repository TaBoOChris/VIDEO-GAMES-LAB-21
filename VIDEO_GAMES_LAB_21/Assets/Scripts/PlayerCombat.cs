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

    public FightEffectManager fightEffectManager;

    public bool canAttack = true;
    public bool isHurt = false;

    //-------------------------------------------------------
    
    private void Awake(){
        animator = gameObject.GetComponent<Animator>();

        // get MAterial


    }

    public void QuickAttackInput(InputAction.CallbackContext context){
        
        if(context.performed != true || !canAttack){
            return;
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

    public void TakeDamage(float percent){

        percentage += percent;
        // play hurt anim
    }

    public void Propulse( Vector3 DamageImpactPosition){

        Vector2 forceDirection = this.gameObject.transform.position - DamageImpactPosition;
        forceDirection = forceDirection.normalized;
        gameObject.GetComponent<Rigidbody2D>().AddForce(forceDirection * percentage);

    }



    public void QuickAttack(int i){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,stickLayers);
     
        foreach ( Collider2D enemy in hitEnemies){
            if(enemy.gameObject != gameObject){


                float damageImpact = 100.0f;

                enemy.GetComponent<PlayerCombat>().TakeDamage(damageImpact * i);
                enemy.GetComponent<PlayerCombat>().Stun(0.5f);
                
                if(i >= 3 ){

                    fightEffectManager.AddImpactEffect();
                    enemy.GetComponent<PlayerCombat>().Propulse(attackPoint.position);
                }

               
            }
        } 
    }


    public void Stun(float duration){
        Debug.Log("stun");
        canAttack = false;
        isHurt = true;
        GetComponent<PlayerMovement>().Stun();

        CancelInvoke();
        Invoke("StopStun", duration);
    }

    public void StopStun(){
        canAttack = true;
        GetComponent<PlayerMovement>().StopStun();
    }



}
