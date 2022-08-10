using UnityEngine;

namespace Alpha
{
    public class Checkpoint : MonoBehaviour
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
        #endregion
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.GetComponent<CollisionCheck>())
            {
                //GameManager.instance.SetLastCheckPoint(this.transform);
            }
        }
        #region PublicMethods

        #endregion

        #region PrivateMethods

        #endregion

        #region GameEventListeners

        #endregion
    }
}