using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Baracuda.Monitoring;


public class PlayerInput : MonitoredBehaviour
{
    private Vector2 startPos;
    private Vector2 dragPos;
    private Vector2 endPos;
    private Vector2 dir;
    private Vector2 forcedir;

    //[Monitor]
    //Vector2 forceDisplay => forcedir.normalized;

    [Header("DragandShoot")]
    [SerializeField] public float maxForce;
    [SerializeField] public float poundForce = 10f;
    [SerializeField] int linepoints;
    [SerializeField] float fallMultiplayer = 2.5f;
    [SerializeField] Transform aimer;
    [SerializeField] Transform dragger;
    [SerializeField] Transform starterPos;
    [SerializeField] LineRenderer lr;
    [SerializeField] Transform aimArrow;
    [SerializeField] Projection projectionCreator;
    [SerializeField] TMP_Dropdown optionDropdown;
    
    private Rigidbody2D rb;
    private Camera camRef;
    private CollisionCheck collisonCheck;
    private PlayerAnimation playerAnimation;
    public bool poundActive;
    public bool optionMenuActive;
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

    protected override void Awake()
    {
        base.Awake();
    }
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

    private void OnEnable()
    {
        if(aimdebug)
        {
            OptionMenu.optionMenuOpened.AddListener(SetOptionActive);
            OptionMenu.optionMenuClosed.AddListener(SetOptionInactive);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if(!optionMenuActive && !GameManager.instance.gamePaused)
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

    }
    private void FixedUpdate()
    {
        //faster falling
        if (rb.velocity.y <= 0.1f && playerState == PlayerState.LAUNCHED)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplayer - 1) * Time.deltaTime;
        }
    }
    private void MouseInput()
    {
        //FixedForceAiming();
        switch (optionDropdown.value)
        {
            case 0:
                FixedForceAiming();
                break;
            case 1:
                VariableForceAiming();
                break;
            default:
                break;
        }

    }

    void VariableForceAiming()
    {
        if (Input.GetMouseButtonDown(0))
        {


            playerState = PlayerState.AIMING;

            playerAnimation.SetAimTrigger();
            playerAnimation.SetAimReticle(true);

            projectionCreator.ToggelLineRenderer(true);
            //lr.enabled = true;
            if (aimdebug)
            {
                starterPos.position = startPos;
                aimer.gameObject.SetActive(true);
                dragger.gameObject.SetActive(true);
                starterPos.gameObject.SetActive(true);
            }

            startPos = camRef.ScreenToWorldPoint(Input.mousePosition); // A vector

        }
        if (Input.GetMouseButton(0))
        {
            dragPos = camRef.ScreenToWorldPoint(Input.mousePosition);
            if (aimdebug)
            {
                dragger.position = dragPos;
            }
            dir = dragPos - startPos;

            aimer.position = (Vector2)transform.position - dir;

            forcedir = aimer.position - transform.position;
            //forcedir = forcedir.normalized;
            //forcedir = new Vector2(Mathf.Clamp(forcedir.x, -maxForce, maxForce), Mathf.Clamp(forcedir.y, -maxForce, maxForce));

            //flip sprite while aiming
            collisonCheck.FlipSprite(forcedir);

            //draw trajectory via simulation
            projectionCreator.SimulateTrajectory(transform.position, forcedir,rb.velocity, maxForce);

            //lr.SetPosition(0, transform.position);
            //lr.SetPosition(1, aimer.position);

        }
        if (Input.GetMouseButtonUp(0))
        {
            playerState = PlayerState.LAUNCHED;

            SoundManager.instance.PlayLaunchSFx();

            playerAnimation.SetAimReticle(false);

            if (aimdebug)
            {
                aimer.gameObject.SetActive(false);
                dragger.gameObject.SetActive(false);
                starterPos.gameObject.SetActive(false);
            }

             projectionCreator.ToggelLineRenderer(false);
            //lr.enabled = false;


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
    void FixedForceAiming()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerState = PlayerState.AIMING;

            playerAnimation.SetAimTrigger();
            playerAnimation.SetAimReticle(true);
            aimArrow.gameObject.SetActive(true);
            //lr.enabled = true;
            if (aimdebug)
            {
                starterPos.position = startPos;
                aimer.gameObject.SetActive(true);
                dragger.gameObject.SetActive(true);
                starterPos.gameObject.SetActive(true);
            }

            startPos = camRef.ScreenToWorldPoint(Input.mousePosition); // A vector

        }
        if (Input.GetMouseButton(0))
        {
            dragPos = camRef.ScreenToWorldPoint(Input.mousePosition);
            if (aimdebug)
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


            //lr.enabled = false;


            dragPos = camRef.ScreenToWorldPoint(Input.mousePosition);
            dir = dragPos - startPos;

            aimer.position = (Vector2)transform.position - dir;

            forcedir = aimer.position - transform.position;
            forcedir = new Vector2(Mathf.Clamp(forcedir.x, -maxForce, maxForce), Mathf.Clamp(forcedir.y, -maxForce, maxForce));

            rb.velocity = (forcedir.normalized) * maxForce;

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

    void SetOptionActive()
    {
        StartCoroutine(delay(true,0f));
    }

    void SetOptionInactive()
    {
        StartCoroutine(delay(false,1f));
    }

    IEnumerator delay(bool state,float time)
    {
        yield return new WaitForSeconds(time);
        optionMenuActive = state;
    }
    
    
}
