using UnityEngine;

namespace Alpha
{
    public class Flag : MonoBehaviour
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
            if(other.GetComponent<CollisionCheck>())
            {
                GetComponent<Animator>().SetTrigger("flagReached");
                RespawnPlayer.instance.SetLastCheckPoint(this.transform);
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