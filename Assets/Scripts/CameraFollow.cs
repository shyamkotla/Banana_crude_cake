using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] private float camLerpSpeed = 2f;
    [SerializeField] private float aimSwitchSpeed = 0.5f;
    [SerializeField] private float aimSwitchThreshold = 0.5f;
    Vector3 offset;
    [SerializeField] Vector3 leftAimoffset ;
    [SerializeField] Vector3 rightAimoffset;
    [SerializeField] bool aimSwitchCam;

    private void Start()
    {
        rightAimoffset = new Vector3(-5.0f, -0.5f, 10f);
        leftAimoffset =  new Vector3(6.0f, -0.5f, 10f);

        offset = target.position - transform.position;
        //Debug.Log(offset);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position - offset, camLerpSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);

    }

    public void SetCamOffset(float dotVal)
    {
        if (aimSwitchCam)
        {
            if (dotVal < -aimSwitchThreshold)
            {
                offset = leftAimoffset;
            }
            else if (dotVal > aimSwitchThreshold)
            {
                offset = rightAimoffset;
            }
            else
            {
                ResetCamOffset();
            }
        }
    }

    public void ResetCamOffset()
    {
        offset = Vector2.zero;

    }
}
