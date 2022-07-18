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
    [SerializeField] Projection projection;
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
    

    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.IDLE;
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        collisonCheck = GetComponent<CollisionCheck>();
        camRef = Camera.main;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (collisonCheck.readyToBounce)
            MouseInput();
        if(playerState == PlayerState.FIRSTBOUNCE && Input.GetMouseButtonDown(1))
        {
            poundActive = true;
            //rb.AddForce(Vector2.down * poundForce, ForceMode2D.Impulse);
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
            collisonCheck.FlipSprite(forcedir);

            projection.SimulateTrajectory(transform.position, forcedir, maxForce);
        }
        if (Input.GetMouseButtonUp(0))
        {
            playerState = PlayerState.LAUNCHED;

            playerAnimation.SetAimReticle(false);
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

            collisonCheck.readyToBounce = false;
            collisonCheck.SetSpriteColor(false);
        }
    }
    
    
}
