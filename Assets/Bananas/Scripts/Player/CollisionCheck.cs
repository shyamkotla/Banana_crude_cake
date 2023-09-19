using UnityEngine;
using DG.Tweening;

public class CollisionCheck : MonoBehaviour
{
    #region Variables
    PlayerInput playerInput;
    Rigidbody2D rb;
    [SerializeField] float shakeStrength;
    [SerializeField] float shakeDuration;
    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] TrailRenderer trailRend;
    [SerializeField] Color ActiveColor;
    [SerializeField] Color NotActiveColor;
    [SerializeField] Color poundTrailColor;
    [SerializeField] GameObject notActiveIcon;
    public bool readyToBounce;
    Color tempgradient;
    float tempwidth;
    #endregion

    #region UnityMethods
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = rb = GetComponent<Rigidbody2D>();
        trailRend = GetComponent<TrailRenderer>();
        tempgradient = trailRend.material.color;
        tempwidth = trailRend.startWidth;
    }

    void Update()
    {

    }

    
    
    private void OnCollisionEnter2D(Collision2D other)
    {

        readyToBounce = true;
        SetSpriteColor(true);
        if (playerInput.playerState == PlayerInput.PlayerState.LAUNCHED)
        {
            SoundManager.instance.PlayFirstBounceSFx();
            playerInput.playerState = PlayerInput.PlayerState.FIRSTBOUNCE;
        }
        else if (playerInput.playerState == PlayerInput.PlayerState.FIRSTBOUNCE && playerInput.poundActive)
        {
            //SFX
            SoundManager.instance.PlayPoundSFx();
            //cameraShake
            
            Camera.main.DOShakePosition(shakeDuration, shakeStrength,10,90, false);
            SetTrailColor(false);
            playerInput.playerState = PlayerInput.PlayerState.POUNDED;
            playerInput.poundActive = false;
        }
        else if ((playerInput.playerState == PlayerInput.PlayerState.FIRSTBOUNCE && other.collider.CompareTag("Platform")) 
            || playerInput.playerState == PlayerInput.PlayerState.POUNDED)
        {
            //reset velocity
            rb.velocity = Vector2.zero;

            //reset rotation
            rb.MoveRotation(Quaternion.identity);
            transform.rotation = Quaternion.Euler(Vector3.zero);

            //change state
            playerInput.playerState = PlayerInput.PlayerState.SECONDBOUNCE;

        }
        if(other.transform.CompareTag("Spike"))
        {
            rb.velocity = Vector2.zero;
            RespawnPlayer.instance.MoveToCheckpoint(transform.position);
            
        }


    }


    #endregion

    #region PublicMethods
    public void SetTrailColor(bool state)
    {
        trailRend.material.color = state ? poundTrailColor : tempgradient;
        trailRend.startWidth = state ? 1f : tempwidth;
    }
    public void SetSpriteColor(bool state)
    {
        //spriteRend.color = state ? ActiveColor : NotActiveColor;
        notActiveIcon.gameObject.SetActive(!state);

    }
    public void FlipSprite(Vector2 aimDirection)
    {
        spriteRend.flipX = Vector2.Dot(Vector2.right, aimDirection) < 0 ? true : false;
    }


    #endregion

    #region PrivateMethods


    #endregion

    #region GameEventListeners

    #endregion
}
