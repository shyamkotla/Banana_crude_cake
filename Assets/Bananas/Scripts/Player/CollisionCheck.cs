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
    //[SerializeField] GameObject notActiveIcon;
    public Sprite[] splashSprites;
    [SerializeField] GameObject poundSplatterFxPrefab;
    //public bool readyToSloMoAim;
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
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (playerInput.playerState == PlayerInput.PlayerState.POUND)
        {
            //Pounded after first bounce
            PoundEffects();
            SetToIdle();
        }
        else if (playerInput.playerState == PlayerInput.PlayerState.LAUNCHED )
        {
            //first bounce
            SoundManager.instance.PlayBounceSFx();
            playerInput.playerState = PlayerInput.PlayerState.FIRSTBOUNCE;
        }
        else if (playerInput.playerState == PlayerInput.PlayerState.FIRSTBOUNCE && other.collider.CompareTag("Platform")) 
        {
            SetToIdle();
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
    //public void SetSpriteColor(bool state)
    //{
    //    //spriteRend.color = state ? ActiveColor : NotActiveColor;
    //    notActiveIcon.gameObject.SetActive(!state);

    //}
    public void FlipSprite(Vector2 aimDirection)
    {
        spriteRend.flipX = Vector2.Dot(Vector2.right, aimDirection) < 0 ? true : false;
    }


    #endregion

    #region PrivateMethods
    private void PoundEffects()
    {
        //SFX
        SoundManager.instance.PlayPoundSFx();
        //cameraShake
        Camera.main.DOShakePosition(shakeDuration, shakeStrength, 10, 90, false);
        //splatter effect
        var rotRange = Random.Range(165f, 185f);
        var posRange = Random.Range(-0.5f, -1.5f);
        var poundFx = Instantiate(poundSplatterFxPrefab, transform.position + new Vector3(0f, posRange, 0f), Quaternion.Euler(0f, 0f, rotRange));
        poundFx.GetComponent<SpriteRenderer>().sprite = splashSprites[Random.Range(0, splashSprites.Length)];
        SetTrailColor(false);
    }

    private void SetToIdle()
    {
        //reset velocity
        rb.velocity = Vector2.zero;

        //reset rotation
        rb.MoveRotation(Quaternion.identity);
        transform.rotation = Quaternion.Euler(Vector3.zero);

        //change state
        playerInput.playerState = PlayerInput.PlayerState.IDLE;
    }
    #endregion

    #region GameEventListeners

    #endregion
}
