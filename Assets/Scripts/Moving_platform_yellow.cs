using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_platform_yellow : MonoBehaviour
{
   
    [SerializeField]
    private float _PlatSpeed = 2;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(Vector3.up * _PlatSpeed * Time.deltaTime);
        
        if(transform.position.y >= 13)
        {
            transform.Translate(Vector3.down * _PlatSpeed * Time.deltaTime);
        }

        if (transform.position.y < -13)
        {
            transform.Translate(Vector3.up * _PlatSpeed * Time.deltaTime);
        }
    }
    
}




//transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);