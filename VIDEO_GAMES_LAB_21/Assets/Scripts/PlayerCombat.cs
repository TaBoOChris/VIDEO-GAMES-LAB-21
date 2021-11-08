using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public float percentage = 0.0f;
    public bool isAttacking = false;
    public bool isHeavyAttacking = false;
    public bool isFiring = false;
    
    public Transform attackPoint;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float attackRange = 0.5f;
    public LayerMask stickLayers;

    public FightEffectManager fightEffectManager;

    public AudioManager audioManager;

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


    public void HeavyAttackInput(InputAction.CallbackContext context){
        
        if(context.performed != true || !canAttack){
            return;
        }

        if(context.performed && !isHeavyAttacking) { 
               
            isHeavyAttacking = true; 

            
        }
    }


    public void FireAttackInput(InputAction.CallbackContext context){
        
        if(context.performed != true || !canAttack){
            return;
        }

        if(context.performed && !isFiring) { 
               
            isFiring = true;
            Shoot();        
        }
    }


    void OnDrawGizmosSelected(){
        if(attackPoint != null){
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    public void TakeDamage(float percent){

        percentage += percent;
        
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

                audioManager.Play("punch");
                
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


    public void HeavyAttack(int i){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,stickLayers);
        foreach ( Collider2D enemy in hitEnemies){
            if(enemy.gameObject != gameObject){

                audioManager.Play("punch");
                
                float damageImpact = 300.0f;

                enemy.GetComponent<PlayerCombat>().TakeDamage(damageImpact * i);
                enemy.GetComponent<PlayerCombat>().Stun(0.5f);
                fightEffectManager.AddImpactEffect();
                enemy.GetComponent<PlayerCombat>().Propulse(attackPoint.position);              
            }
        } 
    }



    public void Shoot(){
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponent<Bullet>();
        bullet.parent = gameObject;
    }



    // ------------ STUN ------------------------ 
    public void Stun(float duration){


        canAttack = false;          // avoid attack
        isHurt = true;              // run the animation
        animator.Play("Player_Hurt");
        fightEffectManager.AddHurtEffect(gameObject);
        GetComponent<PlayerMovement>().Stun();  // disable movement

    }

    public void StopStun(){
        canAttack = true;
        isHurt = false;   
        fightEffectManager.RemoveHurtEffect(gameObject);
        GetComponent<PlayerMovement>().StopStun();
    }



}
