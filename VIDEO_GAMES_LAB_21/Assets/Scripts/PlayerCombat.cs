using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public void QuickAttack(InputAction.CallbackContext context){

        if(Time.time >= nextAttackTime){
            // play attack anim
            animator.SetTrigger("QuickAttack");

            nextAttackTime = Time.time + 1f/attackRate;
            // deteck enemies
            // damage them 

        }
    }

}
