using UnityEngine;


public class CollisionCheck : MonoBehaviour
{
    #region Variables
    PlayerInput playerInput;
    Rigidbody2D rb;
    [SerializeField] SpriteRenderer spr;
    [SerializeField] Color ActiveColor;
    [SerializeField] Color NotActiveColor;
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

    public void SetSpriteColor(bool state)
    {
        spr.color = state ? ActiveColor : NotActiveColor;

    }
    public void FlipSprite(bool state)
    {
        spr.flipX = state;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {

        readyToBounce = true;
        SetSpriteColor(true);
        if (playerInput.playerState == PlayerInput.PlayerState.LAUNCHED || playerInput.playerState == PlayerInput.PlayerState.IDLE)
        {
            playerInput.playerState = PlayerInput.PlayerState.FIRSTBOUNCE;
        }
        else if (playerInput.playerState == PlayerInput.PlayerState.FIRSTBOUNCE)
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
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (dragAndShoot.playerState == PlayerInput.PlayerState.SECONDBOUNCE)
    //    {
    //        rb.velocity = Vector2.zero;

    //    }
    //}

    #endregion

    #region PublicMethods

    #endregion

    #region PrivateMethods


    #endregion

    #region GameEventListeners

    #endregion
}
