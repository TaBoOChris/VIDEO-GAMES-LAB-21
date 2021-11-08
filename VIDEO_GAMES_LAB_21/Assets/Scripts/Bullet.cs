using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float speed = 2f;
    public Rigidbody2D rb;

    public GameObject parent;

    void Start(){
        rb.velocity = transform.right*speed;
    }
    
   
   void OnTriggerEnter2D(Collider2D hitInfo){
        if(hitInfo.gameObject != parent){

            if(hitInfo.GetComponent<PlayerCombat>()){

                float damageImpact = 300.0f;

                hitInfo.GetComponent<PlayerCombat>().TakeDamage(damageImpact);
                hitInfo.GetComponent<PlayerCombat>().Stun(0.5f);

                hitInfo.GetComponent<PlayerCombat>().Propulse(gameObject.transform.position);
            }

            Destroy(gameObject);
        }
   }
}
