using UnityEngine;

    public class CameraFollow : MonoBehaviour
    {
        public float camLerpSpeed = 2f;
        private Vector3 offset;
        public Transform target;

        private void Start()
        {
            offset = target.position - transform.position;
        }
       
        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, target.position - offset, camLerpSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
            
        }
    }
