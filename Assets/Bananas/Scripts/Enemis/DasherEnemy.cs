using UnityEngine;
using System.Collections;
public class DasherEnemy : Enemy
{
    #region Variables
    [Header("Dasher Enemy ")]
    [SerializeField] float dashValue;
    [SerializeField] float time_bw_attacks;
    DasherRange range;
    Player player;
    Alpha.patrol patrol;
    Rigidbody2D rb;
    Animator animator;
    bool inrange;
    bool attacking;
    WaitForSeconds waitForSeconds;
    #endregion

    #region UnityMethods
    void Start()
    {
        waitForSeconds = new WaitForSeconds(time_bw_attacks);
        patrol = GetComponent<Alpha.patrol>();
        rb = GetComponent<Rigidbody2D>();
        range = GetComponentInChildren<DasherRange>();
        animator = GetComponent<Animator>();
        player = null;
    }

    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            AttackIfOnRight(other,player);
            
        }
    }

    private void AttackIfOnRight(Collision2D other, Player player)
    {
        if (transform.localScale.x == 1)
        {
            //local scale 1
            //if (transform.position.x > other.transform.position.x)
            //{
            //    Debug.Log("left");
            //}
            if (transform.position.x < other.transform.position.x)
            {
                this.TakeDamage(player.damage);
            }
        }
        else if(transform.localScale.x == -1)
        {
            //local scale -1

            //if (transform.position.x < other.transform.position.x)
            //{
            //    Debug.Log("left");
            //}
            if (transform.position.x > other.transform.position.x)
            {
                this.TakeDamage(player.damage);
            }
        }
        
    }
    #endregion

    #region PublicMethods

    public void PlayerInRange(bool state,Player player)
    {
        this.inrange = state;
        if(inrange)
        {
            patrol.enabled = false;
            if(!attacking)
            {
                StartCoroutine(AttackPlayer(player));
            }
        }
        else
        {
            patrol.enabled = true;
            animator.SetBool("attack", false);
        }
    }
    #endregion

    #region PrivateMethods
    IEnumerator AttackPlayer(Player player)
    {
        while(inrange)
        {
            attacking = true;
            // dash its visual transform
            DashLocal();
            //play animation
            animator.SetBool("attack", true);
            //take player health
            player.TakeDamage(this.damage);
            yield return waitForSeconds;
            attacking = false;
        }

    }

    private void DashLocal()
    {
        if (transform.localScale.x == 1)
        {
            //patrol.transform.Translate(new Vector3(-dashValue, 0f, 0f));
            rb.AddForce(new Vector2(-dashValue, 0), ForceMode2D.Impulse);
        }
        else
        {
            //patrol.transform.Translate(new Vector3(dashValue, 0f, 0f));
            rb.AddForce(new Vector2(dashValue, 0), ForceMode2D.Impulse);

        }
    }
    #endregion

    #region GameEventListeners

    #endregion
}