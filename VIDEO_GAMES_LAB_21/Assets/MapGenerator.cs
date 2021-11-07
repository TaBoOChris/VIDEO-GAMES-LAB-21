using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform prefab;

    void Start()
    {
        Transform newObj = Instantiate(prefab, new Vector3(-3.0f, -1, 0), Quaternion.identity);
        //newObj.

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
