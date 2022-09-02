using UnityEngine;

public class Enemy : MonoBehaviour,Idamagable
{
    public float health { get ; set ; }
    public float deathDelay { get ; set ; }

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

    #endregion

    #region PrivateMethods
    public void Die()
    {
        Destroy(gameObject,deathDelay);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health<=0)
        {
            Die();
        }
    }
    #endregion

    #region GameEventListeners

    #endregion
}