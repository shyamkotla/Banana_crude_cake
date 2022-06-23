using UnityEngine;

    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] Transform target;
        Vector3 offset;
        [SerializeField] private float camLerpSpeed = 2f;

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
