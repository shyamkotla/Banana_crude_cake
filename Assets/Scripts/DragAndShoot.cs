using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShoot : MonoBehaviour
{
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 dragPos;

    [SerializeField] private Vector2 endPos;
    [SerializeField] public float maxForce;
    [SerializeField] private float currentForce;
    [SerializeField] int linepoints;
    [SerializeField] Transform aimer;
    [SerializeField] Transform dragger;
    [SerializeField] Transform starterPos;
    [SerializeField] Projection projection;
    [SerializeField] Animator anim;
    //[SerializeField] LineRenderer lr;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 dir;
    private Camera camRef;
    public Vector2 forcedir;
    SpriteRenderer spr;
    bool inputdone;
    bool firstbouncedone;
    
    [SerializeField] Color ActiveColor;
    [SerializeField] Color NotActiveColor;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        camRef = Camera.main;
        spr = GetComponent<SpriteRenderer>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(firstbouncedone == true)
        {
            spr.color = ActiveColor;
            //Cursor.lockState = CursorLockMode.Locked;
            MousInput();
        }
        else
        {
            Debug.Log("input not allowed");
            spr.color = NotActiveColor;

        }
        
        

    }

    private void MousInput()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            projection.lr.enabled = true;
            aimer.gameObject.SetActive(true);
            dragger.gameObject.SetActive(true);
            starterPos.gameObject.SetActive(true);

            startPos = camRef.ScreenToWorldPoint(Input.mousePosition); // A vector
            starterPos.position = startPos;
        }
        if (Input.GetMouseButton(0))
        {
            dragPos = camRef.ScreenToWorldPoint(Input.mousePosition);
            dragger.position = dragPos;
            dir = dragPos - startPos;

            aimer.position = (Vector2)transform.position - dir;

            forcedir = aimer.position - transform.position;
            forcedir = new Vector2(Mathf.Clamp(forcedir.x, -maxForce, maxForce), Mathf.Clamp(forcedir.y, -maxForce, maxForce));
            projection.SimulateTrajectory(transform.position, forcedir, maxForce);
        }
        if (Input.GetMouseButtonUp(0))
        {
            aimer.gameObject.SetActive(false);
            dragger.gameObject.SetActive(false);
            starterPos.gameObject.SetActive(false);

            projection.lr.enabled = false;


            dragPos = camRef.ScreenToWorldPoint(Input.mousePosition);
            dir = dragPos - startPos;

            aimer.position = (Vector2)transform.position - dir;
            
            forcedir = aimer.position - transform.position;
            forcedir = new Vector2(Mathf.Clamp(forcedir.x, -maxForce, maxForce), Mathf.Clamp(forcedir.y, -maxForce, maxForce));

            anim.SetTrigger("shoot");
            rb.velocity = forcedir * maxForce;

            firstbouncedone = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        firstbouncedone = true;
    }
    //private void OnCollisionExit2D(Collision2D other)
    //{
    //    firstbouncedone = false;
    //}
}
