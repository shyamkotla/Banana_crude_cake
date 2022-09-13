using UnityEngine;

namespace Alpha
{
    public class patrol : MonoBehaviour
    {
        [SerializeField] private float _PlatSpeed;
        [SerializeField] Transform ptA;
        [SerializeField] Transform ptB;
        SpriteRenderer spr;
        Vector3 target;
        Vector3 direction;
        #region Variables

        #endregion

        #region UnityMethods
        void Start()
        {
            spr = GetComponent<SpriteRenderer>();   
            direction = Vector2.up;
            target = ptA.position;
        }

        void Update()
        {
            
            if (Vector2.Distance(transform.position,target)>0.5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target, _PlatSpeed * Time.deltaTime);
            }
            else
            {
                target = GetTarget();
            }
           
        }
        #endregion

        #region PublicMethods

        #endregion

        #region PrivateMethods
        private Vector3 GetTarget()
        {
            var dis1 = Vector2.Distance(transform.position, ptA.position);
            var dis2 = Vector2.Distance(transform.position, ptB.position);
            if(dis1>1f)
            {
                if(spr!=null)
                {
                    //spr.flipX = false;
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }
                return ptA.position;
            }
            else if(dis2>1f)
            {
                if (spr != null)
                {
                    //spr.flipX = true;
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                return ptB.position;
            }
            return Vector3.zero ;

        }
        #endregion

        #region GameEventListeners

        #endregion
    }
}