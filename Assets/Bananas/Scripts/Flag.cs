using UnityEngine;

namespace Alpha
{
    public class Flag : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject checkPointEffect;
        [SerializeField] private Transform effectPoint;
        bool unlockCheckPoint;
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
            if(other.GetComponent<CollisionCheck>() && !unlockCheckPoint)
            {
                GetComponent<Animator>().SetTrigger("flagReached");
                Instantiate(checkPointEffect,effectPoint.position,Quaternion.Euler(-90f,0f,0f));
                RespawnPlayer.instance.SetLastCheckPoint(this.transform);
                SoundManager.instance.PlayFlagCheckPointSFx();
                unlockCheckPoint = true;
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