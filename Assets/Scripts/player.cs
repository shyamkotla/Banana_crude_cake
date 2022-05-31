using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private float _speed = 3.5f;
    [SerializeField]    
    private float _Jumpspeed = 1f;
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] Transform grounpos;
    [SerializeField] bool grounded;
    [SerializeField] float raylength =0.1f;
    public Rigidbody2D rb;

    [SerializeField] LayerMask groundlayer;


    void Start()
    {
        grounded = false;
        //transform.position = new Vector3(0, 0, 0);
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if(groundcheck())
        {
            
            jump();
        }
        movement();

    }

    private bool groundcheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(grounpos.position, Vector2.down,raylength);
        if (hit)
        {
            grounded = true;
            Debug.Log(Physics2D.Raycast(grounpos.position, Vector2.down * raylength, groundlayer).collider.name);
            return true;
        }
        else
        {
            grounded = false;
            return false;
        }
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(grounpos.position, Vector2.down * raylength);
    }
    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * _Jumpspeed, ForceMode2D.Impulse);
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Debug.DrawRay(transform.position, new Vector3(0f, raylength, 0f));
    //}

    private void movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");

        //new Vector3(1,0,0) * 0 * 3.5f * Real time
        transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);

        //new Vector3(0, 0, 1) * 0 * 3.5f * Real time
        //transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
    }
}
