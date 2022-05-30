using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private float _speed = 3.5f;
    [SerializeField]    
    private float _Jumpspeed = 1f;
    public float horizontalInput;
    public float verticalInput;
    public float jumpInput;

    public Rigidbody2D rb;


    void Start()
    {
        //transform.position = new Vector3(0, 0, 0);
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        float jumpInput = Input.GetAxis("Jump");

        //new Vector3(1,0,0) * 0 * 3.5f * Real time
        transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);

        //new Vector3(0, 0, 1) * 0 * 3.5f * Real time
        //transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * _Jumpspeed, ForceMode2D.Impulse);
        }
    }
}
