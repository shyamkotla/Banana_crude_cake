using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShoot : MonoBehaviour
{
     private Vector2 startPos;
     private Vector2 dragPos;
     private Vector2 endPos;
    [SerializeField] public float maxForce;
    [SerializeField] private float currentForce;
    [SerializeField] int linepoints;
    [SerializeField] Transform aimer;
    [SerializeField] Transform dragger;
    [SerializeField] Transform starterPos;
    [SerializeField] Projection projection;
    //[SerializeField] LineRenderer lr;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] PhysicsMaterial2D playerPhyMaterial;
    private Vector2 dir;
    private Camera camRef;
    public Vector2 forcedir;
    [Header("Visual")]
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spr;
    [SerializeField] Transform visualRef;
    bool inputdone;
    bool readyToBounce;
    
    public enum PlayerState
    {
        IDLE,
        AIMING,
        LAUNCHED,
        FIRSTBOUNCE,
        SECONDBOUNCE
    }
    public PlayerState playerState;
    [SerializeField] Color ActiveColor;
    [SerializeField] Color NotActiveColor;

    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.IDLE;
        rb = GetComponent<Rigidbody2D>();
        camRef = Camera.main;

        
    }

    // Update is called once per frame
    void Update()
    {

        
        if (readyToBounce == true)
        {
            anim.SetTrigger("ground");

            spr.color = ActiveColor;
            //Cursor.lockState = CursorLockMode.Locked;
            MousInput();
        }
        else
        {
            spr.color = NotActiveColor;

        }
        if (rb.velocity.y > 2f)
        {
            anim.SetTrigger("up");
        }
        else if (rb.velocity.y <-2f)
        {
            anim.SetTrigger("down");
            
        }
        

    }

    private void MousInput()
    {
        
        if (Input.GetMouseButtonDown(0))
        {


            playerState = PlayerState.AIMING;

            anim.SetTrigger("aim");
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

            //flip sprite while aiming
            if (Vector2.Dot(Vector2.right, forcedir) < 0)
            {
                spr.flipX = true;
            }
            else
            {
                spr.flipX = false;

            }

            //Debug.Log(Vector2.Dot(Vector2.right, forcedir));
            projection.SimulateTrajectory(transform.position, forcedir, maxForce);
        }
        if (Input.GetMouseButtonUp(0))
        {
            playerState = PlayerState.LAUNCHED;
            aimer.gameObject.SetActive(false);
            dragger.gameObject.SetActive(false);
            starterPos.gameObject.SetActive(false);

            projection.lr.enabled = false;


            dragPos = camRef.ScreenToWorldPoint(Input.mousePosition);
            dir = dragPos - startPos;

            aimer.position = (Vector2)transform.position - dir;
            
            forcedir = aimer.position - transform.position;
            forcedir = new Vector2(Mathf.Clamp(forcedir.x, -maxForce, maxForce), Mathf.Clamp(forcedir.y, -maxForce, maxForce));

            rb.velocity = forcedir * maxForce;

            readyToBounce = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {

        readyToBounce = true;
        if(playerState == PlayerState.LAUNCHED)
        {

            playerState = PlayerState.FIRSTBOUNCE;
        }
        else if(playerState == PlayerState.FIRSTBOUNCE)
        {
            rb.velocity = Vector2.zero;
            //var dum = visualRef.transform.rotation.eulerAngles;
            //Debug.Log(visualRef.Rotate(Vector3.zero,Space.World);
            visualRef.Rotate(Vector3.zero, Space.World);
            playerState = PlayerState.SECONDBOUNCE;
            
        }
        

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (playerState == PlayerState.SECONDBOUNCE)
        {
            rb.velocity = Vector2.zero;

        }
    }
}
