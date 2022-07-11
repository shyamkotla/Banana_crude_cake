using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 dragPos;
    private Vector2 endPos;
    private Vector2 dir;
    public Transform starter;
    public Transform aimer;
    public Transform dragger;
    private Vector2 forcedir;
    [Header("DragandShoot")]
    [SerializeField] public float maxForce;
    [SerializeField] int linepoints;
    [SerializeField] LineRenderer lr;
    private Rigidbody2D rb;
    private Camera camRef;
    private CollisionCheck collisonCheck;
    private PlayerAnimation playerAnimation;
    
    public enum PlayerState
    {
        IDLE,
        AIMING,
        LAUNCHED,
        FIRSTBOUNCE,
        SECONDBOUNCE
    }
    public PlayerState playerState;
    

    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.IDLE;
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        collisonCheck = GetComponent<CollisionCheck>();
        camRef = Camera.main;
        lr.useWorldSpace = false;
        //lr.setc
    }

    //Update is called once per frame
    void Update()
    {
        if (collisonCheck.readyToBounce)
            MouseInput();
    }
    //void FixedUpdate()
    //{
    //    if (collisonCheck.readyToBounce)
    //        MouseInput();
    //}

    private void MouseInput()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            playerState = PlayerState.AIMING;

            lr.enabled = true;
            playerAnimation.SetAimTrigger();

            startPos = camRef.ScreenToWorldPoint(Input.mousePosition); // A vector
            starter.position = startPos;
            lr.SetPosition(0, (Vector2)transform.position);
        }
        if (Input.GetMouseButton(0))
        {
            //calculate mirror aim direction
            CalculateShootDirection();

           
            // draw raycast based trajectory
            DrawTrajectory(forcedir);

            //flip sprite while aiming
            collisonCheck.FlipSprite(forcedir);

        }
        if (Input.GetMouseButtonUp(0))
        {
            playerState = PlayerState.LAUNCHED;
            lr.enabled = false;
            CalculateShootDirection();
            rb.velocity = forcedir * maxForce;
            collisonCheck.readyToBounce = false;
            collisonCheck.SetSpriteColor(false);
        }
    }

    private void CalculateShootDirection()
    {
        dragPos = camRef.ScreenToWorldPoint(Input.mousePosition);
        dragger.position = dragPos;
        dir = dragPos - startPos;
        aimer.position = (Vector2)transform.position - dir;
        
        forcedir = aimer.position - transform.position;
        forcedir = new Vector2(Mathf.Clamp(forcedir.x, -maxForce, maxForce), Mathf.Clamp(forcedir.y, -maxForce, maxForce));
    }

    private void DrawTrajectory(Vector2 forcedirection)
    {
        Debug.DrawRay(transform.position, forcedir);
        Debug.Log(forcedirection);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, forcedirection);
        if (hit)
        {
            Debug.Log(hit.collider.name);
            var dis = Vector2.Distance(transform.position, hit.point);
            if(dis>forcedirection.magnitude)
            {
                lr.SetPosition(1, forcedirection);

            }
            else
            {
                lr.SetPosition(1,hit.point) ;
            }
        }
        else
        {
            lr.SetPosition(1, forcedirection);
        }

    }

    private void OnDrawGizmos()
    {
        
    }
}
