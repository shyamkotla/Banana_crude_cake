using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    #region Variables
    [SerializeField] public Animator animator;
    PlayerInput playerinput;
    CollisionCheck collisonCheck;
    Rigidbody2D rb;
    #endregion

    #region UnityMethods
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
            animator.ResetTrigger("down");
            animator.SetTrigger("ground");
        }
    }

    private void CheckVelocity()
    {
        if (rb.velocity.y > 0f)
        {
            animator.ResetTrigger("aim");
            animator.SetTrigger("up");
            
        }
        else if (rb.velocity.y < 0f)
        {
            animator.ResetTrigger("up");
            animator.SetTrigger("down");

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

