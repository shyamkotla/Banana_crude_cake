using UnityEngine;

public class ShooterEnemy : Enemy
{
    #region Variables
    [SerializeField] float throwForce;
    Transform player;
    bool playerInRange;
    #endregion

    #region UnityMethods
    void Start()
    {

    }

    void Update()
    {

    }
    #endregion
    void AimPlayer()
    {
        if (playerInRange)
        {
            var dir = player.position - transform.position;

        }
    }
    #region PublicMethods
    public void PlayerInRange(bool state,Transform player)
    {
        this.player = player;
        playerInRange = state;
    }
    #endregion

    #region PrivateMethods

    #endregion

    #region GameEventListeners

    #endregion
}