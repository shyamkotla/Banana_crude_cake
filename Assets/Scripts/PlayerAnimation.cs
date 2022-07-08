using UnityEngine;
using Baracuda.Monitoring;

public class PlayerAnimation : MonitoredBehaviour
{
    #region Variables
    [SerializeField] public Animator animator;
    PlayerInput playerinput;
    CollisionCheck collisonCheck;
    Rigidbody2D rb;
    [Monitor]
    private Vector2 velocity => rb.velocity;

    #endregion

    #region UnityMethods

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        playerinput = GetComponent<PlayerInput>();
        collisonCheck = GetComponent<CollisionCheck>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerinput.playerState == PlayerInput.PlayerState.LAUNCHED)
        {
            CheckVelocity();
        }

        if (playerinput.playerState == PlayerInput.PlayerState.FIRSTBOUNCE)
        {
            animator.SetTrigger("ground");
        }
    }

    private void CheckVelocity()
    {
        if (rb.velocity.y > 0f)
        {
            animator.ResetTrigger("aim");
            animator.SetBool("vel",true);
            
        }
        else if (rb.velocity.y < 0f)
        {
            animator.SetBool("vel",false);

        }
    }
    #endregion

    #region PublicMethods
    public void SetAimTrigger()
    {
        animator.ResetTrigger("ground");
        animator.SetTrigger("aim");

    }
    #endregion

    #region PrivateMethods

    #endregion

    #region GameEventListeners

    #endregion
}

