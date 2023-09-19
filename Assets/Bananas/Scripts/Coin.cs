using UnityEngine;

public class Coin : MonoBehaviour
{
    #region Variables

    #endregion

    #region UnityMethods
    void Start()
    {

    }

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CollisionCheck>())
        {
            SoundManager.instance.PlayCollectibleSFx();
            GameManager.instance.CoinCollected();
            Destroy(this.gameObject);
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