using DG.Tweening;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float camLerpSpeed = 2f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private LevelStart levelStart;
    public Transform target;
    private bool ghost;
    private bool launched;

    private void Start()
    {
        launched = false;
        offset = new Vector3(-9.150002f, -1.910292f, 10f);
        levelStart.LaunchComplete.AddListener(() => { 
            transform.DOMove(target.position - offset, 1f).OnComplete(() =>
            {
                launched = true;
            });
        });
        //offset = target.position - transform.position;
    }
    private void FixedUpdate()
    {
        // since main player is rb controlled ..
        if (!ghost && launched)
        {
            //Debug.Log("cam update");
           LerpToTarget();
        }
        
    }
    private void LateUpdate()
    {
        // player ghost updates
        if(ghost && launched)
        {
            LerpToTarget();
        }
    }

    private void LerpToTarget()
    {
        var A = new Vector3(transform.position.x, transform.position.y, -10f);
        var B = target.position - offset;
        B.z = -10f;
        if(ghost)
        {
            transform.position = Vector3.Lerp(A, B, camLerpSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(A, B, camLerpSpeed * Time.fixedDeltaTime);
        }
        
    }
    public void SetTarget(Transform target,float lerpspeed,bool state)
    {
        this.target = target;
        this.camLerpSpeed = lerpspeed;
        this.ghost = state;
    }
}
