using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float camLerpSpeed = 2f;
    private Vector3 offset;
    public Transform target;
    private bool ghost;
    private void Start()
    {
        offset = target.position - transform.position;
        //transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
    private void FixedUpdate()
    {
        if(!ghost)
        {
            transform.position = new Vector3(target.position.x-offset.x, target.position.y-offset.y, this.transform.position.z);
        }
        
    }
    private void LateUpdate()
    {
        if(ghost)
        {
            var A = new Vector3(transform.position.x, transform.position.y, -10f);
            var B = target.position - offset;
            B.z = -10f;
            transform.position = Vector3.Lerp(A, B, camLerpSpeed * Time.deltaTime);
        }
        //transform.position = Vector3.Lerp(transform.position, target.position - offset, camLerpSpeed * Time.deltaTime);
        //transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
       

    }
    public void SetTarget(Transform target,float lerpspeed,bool state)
    {
        this.target = target;
        this.camLerpSpeed = lerpspeed;
        this.ghost = state;
    }
}
