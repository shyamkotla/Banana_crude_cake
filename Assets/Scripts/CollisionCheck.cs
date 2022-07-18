using UnityEngine;


public class CollisionCheck : MonoBehaviour
{
    #region Variables
    PlayerInput playerInput;
    Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRend;
   // [SerializeField] TrailRenderer trailRend;
    [SerializeField] Color ActiveColor;
    [SerializeField] Color NotActiveColor;
    [SerializeField] Color poundTrailColor;
    public bool readyToBounce;
    
    #endregion

    #region UnityMethods
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = rb = GetComponent<Rigidbody2D>();

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
            playerInput.playerState = PlayerInput.PlayerState.FIRSTBOUNCE;
        }
        else if (playerInput.playerState == PlayerInput.PlayerState.FIRSTBOUNCE && playerInput.poundActive)
        {
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
       


    }
   

    #endregion

    #region PublicMethods
    public void SetSpriteColor(bool state)
    {
        spriteRend.color = state ? ActiveColor : NotActiveColor;

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
