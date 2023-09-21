using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTesting : MonoBehaviour
{
    [SerializeField] Transform start, last;
    [SerializeField] Transform Btrans, Ctrans;
    #region Variables
    private Vector2 A, B, C, D;
    private Vector2 AB, BC, CD;
    private Vector2 ABC, BCD;
    private Vector2 ABCD;
    //private bool respawning;
    [SerializeField] Transform playerDummy;

    float lerpAmount = 1f;
    [SerializeField] float divisor = 2f;
    [SerializeField] private float ghostTravelSpeed;
    bool respawning = false;
    bool reached = false;
    float orginalLerpSpeed;
    #endregion

    #region UnityMethods
   
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            D = last.position;
            A = start.position;
            playerDummy.position = A;

            MoveToCheckpoint(A);
        }

        if (respawning)
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
                reached = false;
                respawning = false;
               

            }
        }

    }
    #endregion

    #region PublicMethods
    
    public void MoveToCheckpoint(Vector2 currentPos)
    {

        lerpAmount = 0.01f;
        respawning = true;
        
        SetRandomValues();

    }
    #endregion

    #region PrivateMethods
    void SetRandomValues()
    {
        var distance = Vector2.Distance(A, D);
        float offset = distance/2;
        B = Vec2RandomOffset(offset);
        C = Vec2RandomOffset(offset);
        Btrans.position = B;
        Ctrans.position = C;
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
}
