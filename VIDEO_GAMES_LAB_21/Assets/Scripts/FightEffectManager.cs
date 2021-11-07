using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEffectManager : MonoBehaviour
{
    // CAMERA SHAKE
    public CameraShake cameraShake;
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.05f;


    public float flashTime = 0.1f;
    public GameObject player1;
    public GameObject player2;
    public GameObject background;

    private Material SticksMaterial1;
    private Material SticksMaterial2;

    private Material BackGroundMaterial;

    // Start is called before the first frame update
    void Start()
    {
        SticksMaterial1 = player1.GetComponent<SpriteRenderer>().material;
        SticksMaterial2 = player2.GetComponent<SpriteRenderer>().material;
        BackGroundMaterial = background.GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddHurtEffect(GameObject playerHurt){
        Material Material = playerHurt.GetComponent<SpriteRenderer>().material;
        Material.SetInt("_isHurtBool",1);

        
        StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
    }
    public void RemoveHurtEffect(GameObject playerHurt){
        Material Material = playerHurt.GetComponent<SpriteRenderer>().material;
        Material.SetInt("_isHurtBool",0);
    }


    
    public void AddImpactEffect(){
        StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
        
        SticksMaterial1.SetInt("_ImpactBool", 1);
        SticksMaterial2.SetInt("_ImpactBool", 1);
        BackGroundMaterial.SetInt("_ImpactBool", 1);

        Invoke("RemoveImpactEffect",flashTime);
    }

    public void RemoveImpactEffect(){
        SticksMaterial1.SetInt("_ImpactBool", 0);
        SticksMaterial2.SetInt("_ImpactBool", 0);
        BackGroundMaterial.SetInt("_ImpactBool", 0);
    }
}
