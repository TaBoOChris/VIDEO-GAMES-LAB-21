using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;

    public void QuickAttack(InputAction.CallbackContext context){
        // play attack anim
        animator.SetTrigger("QuickAttack");
        // deteck enemies
        // damage them 
    }

}
