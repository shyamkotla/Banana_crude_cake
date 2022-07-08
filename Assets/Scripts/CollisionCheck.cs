using UnityEngine;


public class CollisionCheck : MonoBehaviour
{
    #region Variables
    PlayerInput dragAndShoot;
    Rigidbody2D rb;
    [SerializeField] SpriteRenderer spr;
    [SerializeField] Color ActiveColor;
    [SerializeField] Color NotActiveColor;
    public bool readyToBounce;
    
    #endregion

    #region UnityMethods
    void Start()
    {
        dragAndShoot = GetComponent<PlayerInput>();
        rb = rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {

    }

    public void SetSpriteColor(bool state)
    {
        spr.color = state ? ActiveColor : NotActiveColor;

    }
    public void FlipSprite(Vector2 aimDirection)
    {
        spr.flipX = Vector2.Dot(Vector2.right, aimDirection) < 0 ? true : false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {

        readyToBounce = true;
        SetSpriteColor(true);
        if (dragAndShoot.playerState == PlayerInput.PlayerState.LAUNCHED)
        {
            dragAndShoot.playerState = PlayerInput.PlayerState.FIRSTBOUNCE;
        }
        else if (dragAndShoot.playerState == PlayerInput.PlayerState.FIRSTBOUNCE)
        {
            //reset velocity
            rb.velocity = Vector2.zero;

            //reset rotation
            rb.MoveRotation(Quaternion.identity);
            transform.rotation = Quaternion.Euler(Vector3.zero);

            //change state
            dragAndShoot.playerState = PlayerInput.PlayerState.SECONDBOUNCE;

        }


    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (dragAndShoot.playerState == PlayerInput.PlayerState.SECONDBOUNCE)
        {
            rb.velocity = Vector2.zero;

        }
    }

    #endregion

    #region PublicMethods

    #endregion

    #region PrivateMethods


    #endregion

    #region GameEventListeners

    #endregion
}
