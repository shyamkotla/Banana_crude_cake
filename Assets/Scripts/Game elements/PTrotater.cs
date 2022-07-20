using UnityEngine;
using DG.Tweening;
namespace Alpha
{
    public class PTrotater : MonoBehaviour
    {
        [SerializeField] private float rotspeed = 2f;
        #region Variables

        #endregion

        #region UnityMethods
        void Start()
        {

        }

        void Update()
        {
            transform.Rotate(new Vector3(0f, 0f, rotspeed * Time.deltaTime));
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