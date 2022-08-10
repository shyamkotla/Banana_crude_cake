using System.Collections;
using UnityEngine;
using DG.Tweening;
namespace Alpha
{
    public class BridgeDrop : MonoBehaviour
    {
        #region Variables

        #endregion
        [SerializeField] DG.Tweening.Ease dropstyle;
        [SerializeField] float duration = 0.5f;
        private bool dropped;

        #region UnityMethods
        void Start()
        {
            
        }

        void Update()
        {

        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(!dropped)
            {
                transform.DORotate(new Vector3(0f, 0f, 0f), duration).SetEase(dropstyle);
                dropped = true;
            }
            
        }
        
        #endregion
        
        #region PublicMethods

        #endregion
        
        #region PrivateMethods
        IEnumerator DropBridge()
        {
            while(true)
            {
                if(transform.eulerAngles.z != 0)
                {
                    transform.eulerAngles -= new Vector3(0f, 0f, -1f*Time.deltaTime);
                }
                else
                {
                    yield return null;
                }
            }
               
        } 
        #endregion

        #region GameEventListeners

        #endregion
    }
}