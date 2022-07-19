using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 dragPos;
    private Vector2 endPos;
    private Vector2 dir;
    private Vector2 forcedir;
    [Header("DragandShoot")]
    [SerializeField] public float maxForce;
    [SerializeField] public float poundForce = 10f;
    [SerializeField] int linepoints;
    [SerializeField] Transform aimer;
    [SerializeField] Transform dragger;
    [SerializeField] Transform starterPos;
    [SerializeField] LineRenderer lr;
    [SerializeField] Transform aimArrow;
    
    private Rigidbody2D rb;
    private Camera camRef;
    private CollisionCheck collisonCheck;
    private PlayerAnimation playerAnimation;
    public bool poundActive;
    public enum PlayerState
    {
        IDLE,
        AIMING,
        LAUNCHED,
        FIRSTBOUNCE,
        POUNDED,
        SECONDBOUNCE
    }
    public PlayerState playerState;
    [SerializeField] bool aimdebug;


    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.IDLE;
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        collisonCheck = GetComponent<CollisionCheck>();
        camRef = Camera.main;
        lr.positionCount = 2;
    }
    
    // Update is called once per frame
    void Update()
    {

        if (collisonCheck.readyToBounce)
            MouseInput();
        if (playerState == PlayerState.FIRSTBOUNCE && Input.GetMouseButtonDown(1))
        {
            collisonCheck.SetTrailColor(true);
            poundActive = true;
            rb.velocity = Vector2.down * poundForce;
        }

    }

    private void MouseInput()
    {
        
        if (Input.GetMouseButtonDown(0))
        {


            playerState = PlayerState.AIMING;

            playerAnimation.SetAimTrigger();
            playerAnimation.SetAimReticle(true);
            aimArrow.gameObject.SetActive(true);
            lr.enabled = true;
            if(aimdebug)
            {
                aimer.gameObject.SetActive(true);
                dragger.gameObject.SetActive(true);
                starterPos.gameObject.SetActive(true);
            }

            startPos = camRef.ScreenToWorldPoint(Input.mousePosition); // A vector
            if(aimdebug)
            {
                starterPos.position = startPos;
            }
        }
        if (Input.GetMouseButton(0))
        {
            dragPos = camRef.ScreenToWorldPoint(Input.mousePosition);
            if(aimdebug)
            {
                dragger.position = dragPos;
            }
            dir = dragPos - startPos;

            aimer.position = (Vector2)transform.position - dir;

            forcedir = aimer.position - transform.position;
            forcedir = new Vector2(Mathf.Clamp(forcedir.x, -maxForce, maxForce), Mathf.Clamp(forcedir.y, -maxForce, maxForce));
            
            //flip sprite while aiming
            collisonCheck.FlipSprite(forcedir);

            AimArrow(aimer.position);


            //lr.SetPosition(0, transform.position);
            //lr.SetPosition(1, aimer.position);

        }
        if (Input.GetMouseButtonUp(0))
        {
            playerState = PlayerState.LAUNCHED;

            playerAnimation.SetAimReticle(false);
            aimArrow.gameObject.SetActive(false);

            if (aimdebug)
            {
                aimer.gameObject.SetActive(false);
                dragger.gameObject.SetActive(false);
                starterPos.gameObject.SetActive(false);
            }
            

            lr.enabled = false;


            dragPos = camRef.ScreenToWorldPoint(Input.mousePosition);
            dir = dragPos - startPos;

            aimer.position = (Vector2)transform.position - dir;
            
            forcedir = aimer.position - transform.position;
            forcedir = new Vector2(Mathf.Clamp(forcedir.x, -maxForce, maxForce), Mathf.Clamp(forcedir.y, -maxForce, maxForce));

            rb.velocity = forcedir * maxForce;

            collisonCheck.readyToBounce = false;
            collisonCheck.SetSpriteColor(false);
        }
    }
    
    void AimArrow(Vector3 target)
    {
        var direction = target - aimArrow.position;
        direction.Normalize();
        float rotation_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        aimArrow.rotation = Quaternion.Euler(0f, 0f, rotation_z);
    }
   
}
