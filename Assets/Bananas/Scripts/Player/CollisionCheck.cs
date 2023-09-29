using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEditor.UIElements;

public class CollisionCheck : MonoBehaviour
{
    #region Variables
    PlayerInput playerInput;
    Rigidbody2D rb;
    [SerializeField] float shakeStrength;
    [SerializeField] float shakeDuration;
    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] TrailRenderer trailRend;
    [SerializeField] FlashPlayer flashPlayer;
    [SerializeField] AnimationCurve poundTrailWidthCurve;
    //[SerializeField] GameObject notActiveIcon;
    public Sprite[] splashSprites;
    [SerializeField] GameObject poundSplatterFxPrefab;
    Gradient normalGrad;
    [SerializeField] Gradient poundGrad;
    //public bool readyToSloMoAim;
    Color tempgradient;
    float tempwidth;
    AnimationCurve defCurve;
    #endregion

    #region UnityMethods
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = rb = GetComponent<Rigidbody2D>();
        trailRend = GetComponent<TrailRenderer>();
        normalGrad = trailRend.colorGradient;
        tempwidth = trailRend.startWidth;
        defCurve = trailRend.widthCurve;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (playerInput.playerState == PlayerInput.PlayerState.POUND)
        {
            //check if other body is breakable
            if(other.collider.TryGetComponent<BreakablePT>(out BreakablePT breakable))
            {
                breakable.OnCollisionCheckPounded();
            }
            else
            {
                //SFX
                SoundManager.instance.PlayPoundSFx();
            }
            //Pounded after launch
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
            // play hit audio
            SoundManager.instance.PlayOnHitSFx();

            playerInput.playerState = PlayerInput.PlayerState.HIT;
            //Debug.Log("flash hit "+other.gameObject.name);
            flashPlayer.FlashOnHit(this.transform.position);
            //rb.velocity = Vector2.zero;
        }
    }
    #endregion

    #region PublicMethods
    public void SetTrailColor(bool state)
    {
        trailRend.colorGradient = state ? poundGrad : normalGrad;
        trailRend.startWidth = state ? 2f : tempwidth;
        trailRend.widthCurve = state ? poundTrailWidthCurve : defCurve;
    }
    
    public void FlipSprite(Vector2 aimDirection)
    {
        spriteRend.flipX = Vector2.Dot(Vector2.right, aimDirection) < 0 ? true : false;
    }


    #endregion

    #region PrivateMethods
    private void PoundEffects()
    {
        //cameraShake
        Camera.main.DOShakePosition(shakeDuration, shakeStrength, 10, 90, false);
        //splatter effect
        var rotRange = Random.Range(165f, 185f);
        var posRange = Random.Range(-0.5f, -1.5f);
        var poundFx = Instantiate(poundSplatterFxPrefab, transform.position + new Vector3(0f, posRange, 0f), Quaternion.Euler(0f, 0f, rotRange));
        poundFx.GetComponent<SpriteRenderer>().sprite = splashSprites[Random.Range(0, splashSprites.Length)];

        UnityTimer.Timer.Register(0.5f, () =>
        {
            SetTrailColor(false);
        });
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
