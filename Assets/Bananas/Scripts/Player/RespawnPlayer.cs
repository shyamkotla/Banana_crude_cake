using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    #region Variables
    private Vector2 A,B,C,D;
    private Vector2 AB,BC,CD;
    private Vector2 ABC,BCD;
    private Vector2 ABCD;
    //private bool respawning;
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
        if (instance != this && instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
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
                // reset camera target to main player 
                camFollow.SetTarget(playerRef, orginalLerpSpeed, false);
                //disable particles
                ghostParticles.gameObject.SetActive(false);

                reached = false;
                respawning = false;
                SoundManager.instance.PlayGhostRespawnSFx(false);

            }
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
        B = Vec2RandomOffset(offset);
        C = Vec2RandomOffset(offset);
        divisor = Random.Range(2.5f, 3f);
    }

    Vector2 Vec2RandomOffset(float off)
    {
        return new Vector2(Random.Range(A.x - off, D.x + off),
                            Random.Range(A.y - off, D.y + off));
    }
    void BezierCurveLerping()
    {


        AB = Vector2.Lerp(A, B, lerpAmount);
        BC = Vector2.Lerp(B, C, lerpAmount);
        CD = Vector2.Lerp(C, D, lerpAmount);
        ABC = Vector2.Lerp(AB, BC, lerpAmount);
        BCD = Vector2.Lerp(BC, CD, lerpAmount);

        ABCD = Vector2.Lerp(ABC, BCD, lerpAmount);

        playerDummy.transform.position = ABCD;


    }
    #endregion

    #region GameEventListeners

    #endregion
}