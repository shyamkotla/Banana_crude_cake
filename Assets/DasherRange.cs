using UnityEngine;

public class DasherRange : MonoBehaviour
{
    #region Variables
    DasherEnemy myparent;
    #endregion

    #region UnityMethods
    void Start()
    {
        myparent = GetComponentInParent<DasherEnemy>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            myparent.PlayerInRange(true,player);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<CollisionCheck>())
        {
            myparent.PlayerInRange(false,null);
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