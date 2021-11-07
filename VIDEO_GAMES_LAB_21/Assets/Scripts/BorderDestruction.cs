using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderDestruction : MonoBehaviour
{
    private Explodable explodable; 

    void Start()
    {
        explodable = GetComponent<Explodable>();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            PlayerCombat pc = collision.gameObject.GetComponent<PlayerCombat>();

            if(pc.isHurt){
                StartExplosion();
            }
        }
    }

    void StartExplosion(){
        explodable.explode();
        
    }
}
