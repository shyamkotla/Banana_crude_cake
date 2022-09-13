using UnityEngine;

public class Enemy : MonoBehaviour,Idamagable
{
    [Header("Enemy ")]
    [SerializeField] float health;
    [SerializeField] float deathDelay = 0.3f;
    [SerializeField] GameObject deathFX;
    [SerializeField] public float damage;


    #region Variables

    #endregion

    #region UnityMethods
    void Start()
    {
        
    }

    void Update()
    {

    }
    #endregion

    #region PublicMethods
    public void Die()
    {
        Destroy(this.gameObject, deathDelay);
        Instantiate(deathFX, this.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
    }
    #endregion

    #region PublicMethods
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
            Die();
    }
    #endregion

    #region PrivateMethods

    #endregion

    #region GameEventListeners

    #endregion
}