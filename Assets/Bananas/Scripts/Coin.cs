using UnityEngine;

namespace Alpha
{
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
                Destroy(this.gameObject,0.2f);
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
}