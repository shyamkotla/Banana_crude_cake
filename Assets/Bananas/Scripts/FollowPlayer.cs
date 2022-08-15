using UnityEngine;

namespace Alpha
{
    public class FollowPlayer : MonoBehaviour
    {
        public float followSpeed = 2f;
        private Vector3 offset;
        public Transform target;

        private void Start()
        {
            offset = target.position - transform.position;
        }
        private void Update()
        {
            transform.position = target.position - offset;
        }
        //private void LateUpdate()
        //{
        //    transform.position = Vector3.Lerp(transform.position, target.position - offset, followSpeed * Time.deltaTime);
        //    //transform.position = new Vector3(transform.position.x, transform.position.y, -10f);

        //}
    }
}