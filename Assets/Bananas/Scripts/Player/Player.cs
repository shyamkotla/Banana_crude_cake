using UnityEngine;

public class Player : MonoBehaviour,Idamagable
{
    [SerializeField] float health;
    [SerializeField] public float damage;
    [SerializeField] float playerHitJump;
    Rigidbody2D rb;

    #region Variables

    #endregion

    #region UnityMethods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }
    #endregion

    #region PublicMethods
    public void Die()
    {
        //respawn or scene reset
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        rb.AddForce(Vector2.up*playerHitJump, ForceMode2D.Impulse);
        if (health <= 0f)
            Die();
    }
    #endregion

    #region PrivateMethods

    #endregion

    #region GameEventListeners

    #endregion
}