using UnityEngine;
using UnityEngine.InputSystem;

public class RespawnPlayer : MonoBehaviour
{
    #region Variables
    private Vector2 A,B,C,D;
    private Vector2 AB,BC,CD;
    private Vector2 ABC,BCD;
    private Vector2 ABCD;
    //private bool respawning;
    //[SerializeField] Transform offsetB;
    //[SerializeField] Transform offsetC;
    public static RespawnPlayer instance;
    [SerializeField] Transform playerRef;
    [SerializeField] Transform playerDummy;
    [SerializeField] Transform lastCheckPoint;
    [SerializeField] GameObject ghostParticles;
    
    float lerpAmount = 1f;
    [SerializeField] float ghostCamLerpSpeed = 8f;
    [SerializeField] float divisor = 2f;
    [SerializeField] private float ghostTravelSpeed;
    CameraFollow camFollow;
    bool respawning = false;
    bool reached = false;
    float orginalLerpSpeed;
    #endregion

    #region UnityMethods
    private void Awake()
    {
        instance = this;
        //if (instance != this && instance != null)
        //{
        //    Destroy(this.gameObject);
        //}
        //else
        //{
        //    instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
    }
    void Start()
    {
        ghostParticles.gameObject.SetActive(false);
        camFollow = Camera.main.GetComponent<CameraFollow>();
        orginalLerpSpeed = camFollow.camLerpSpeed;

    }

    void Update()
    {
        
        if(respawning)
        {
            if (lerpAmount < 1f)
            {
                //lerpAmount = lerpAmount + (float)((Time.deltaTime) / divisor);
                lerpAmount += ghostTravelSpeed * Time.deltaTime;
                //Debug.Log(lerpAmount);
                BezierCurveLerping();
            }
            else
            {
                reached = true;
            }

            if (reached)
            {
                // disable rotating player visual
                playerDummy.gameObject.SetActive(false);
                // spawn player at last checkpoint
                playerRef.gameObject.SetActive(true);
                playerRef.position = lastCheckPoint.position;
                playerRef.transform.rotation = Quaternion.Euler(Vector3.zero);
                // reset camera target to main player 
                camFollow.SetTarget(playerRef, orginalLerpSpeed, false);
                //disable particles
                ghostParticles.gameObject.SetActive(false);

                reached = false;
                respawning = false;

                playerRef.GetComponent<PlayerInput>().playerState = PlayerInput.PlayerState.IDLE;
                SoundManager.instance.PlayGhostRespawnSFx(false);

            }
            //offsetB.position = B;
            //offsetC.position = C;
        }
        
    }
    #endregion

    #region PublicMethods
    public void SetLastCheckPoint(Transform checkpoint)
    {
        if (lastCheckPoint != checkpoint)
        {
            lastCheckPoint = checkpoint;
            //flagPrefab.position = lastCheckPoint.position;
        }
    }

    public void MoveToCheckpoint(Vector2 currentPos)
    {
        A = currentPos;
        D = lastCheckPoint.position;
        // disable player/player collision
        playerRef.gameObject.SetActive(false);
        // spawn rotating player visual
        playerDummy.gameObject.SetActive(true);
        playerDummy.position = A;
        // set camera target to new visual
        camFollow.SetTarget(playerDummy, ghostCamLerpSpeed, true);
        //enable ghost particles
        ghostParticles.gameObject.SetActive(true);
        // move player visual to lastcheckpoint via curves
        lerpAmount = 0.01f;
        respawning = true;
        //start playing ghost audio
        SoundManager.instance.PlayGhostRespawnSFx(true);
        SetRandomValues();
        
    }
    #endregion

    #region PrivateMethods
   
    void SetRandomValues()
    {
        float offset = 5f;
        Vector2 mid = (A + D) / 2;
        Vector2 Amid = (A + mid) / 2;
        Vector2 Dmid = (D + mid) / 2;
        
        //Vector2 direction = D - A;
        var dir1 = D - Amid;
        var dir2 = D - Dmid;

        B = Amid+ Vector2.Perpendicular(dir1.normalized)*offset;
        C = Dmid+ (Vector2.Perpendicular(dir2.normalized) *-1f * offset);
        
    }
    
    void BezierCurveLerping()
    {
        //quadratic bezier curve b, c as control points

        AB = Vector2.Lerp(A, B, lerpAmount);
        BC = Vector2.Lerp(B, C, lerpAmount);
        CD = Vector2.Lerp(C, D, lerpAmount);
        ABC = Vector2.Lerp(AB, BC, lerpAmount);
        BCD = Vector2.Lerp(BC, CD, lerpAmount);

        ABCD = Vector2.Lerp(ABC, BCD, lerpAmount);

        playerDummy.transform.position = ABCD;
    }
    #endregion
    
}