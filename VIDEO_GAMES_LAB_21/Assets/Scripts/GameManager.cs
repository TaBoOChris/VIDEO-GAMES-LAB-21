using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( player1.transform.position.y < -10 || player1.transform.position.y > 3){
            player1.transform.position = new Vector2(-10.0f,-10.0f);
            Invoke("BackMenu", 1.0f);
        }

        if(player2.transform.position.y < -10  || player2.transform.position.y > 3 ){
            player2.transform.position = new Vector2(-10.0f,-10.0f);
            Invoke("BackMenu", 1.0f);
        }
    }


    public void BackMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1); 

    }
}
