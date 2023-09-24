using UnityEngine;

public class Coin : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject collectionVFX;
    [SerializeField] private float effectScale = 3f;
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
            var effect = Instantiate(collectionVFX, transform.position,Quaternion.identity);
            effect.transform.localScale = Vector3.one * effectScale;
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