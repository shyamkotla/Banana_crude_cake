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
    }
    private void FixedUpdate()
    {
        // since main player is rb controlled ..
        if(!ghost)
        {
           LerpToTarget();

        }
        
    }
    private void LateUpdate()
    {
        // player ghost updates
        if(ghost)
        {
            LerpToTarget();
        }
    }

    private void LerpToTarget()
    {
        var A = new Vector3(transform.position.x, transform.position.y, -10f);
        var B = target.position - offset;
        B.z = -10f;
        transform.position = Vector3.Lerp(A, B, camLerpSpeed * Time.deltaTime);
    }
    public void SetTarget(Transform target,float lerpspeed,bool state)
    {
        this.target = target;
        this.camLerpSpeed = lerpspeed;
        this.ghost = state;
    }
}
