using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject deathFX;
    #endregion

    #region UnityMethods

    void Start()
    {

    }

    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.GetComponent<CollisionCheck>())
        {
            Destroy(this.gameObject);
            var temp = Instantiate(deathFX,this.transform.position,Quaternion.identity);
            Destroy(temp, 0.5f);
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