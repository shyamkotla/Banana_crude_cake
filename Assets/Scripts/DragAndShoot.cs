using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShoot : MonoBehaviour
{
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 dragPos;
    [SerializeField] private Vector2 endPos;
    [SerializeField] private float maxForce;
    [SerializeField] private float currentForce;
    [SerializeField] int linepoints;
    [SerializeField] Transform aimer;
    [SerializeField] Transform dragger;
    [SerializeField] Projection projection;
    //[SerializeField] LineRenderer lr;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 dir;
    private Camera camRef;
    private Vector2 forcedir;
    private bool mouseClicked;
    public bool isGhost;


    // Start is called before the first frame update
    void Start()
    {
        //lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        camRef = Camera.main;
        //lr.positionCount = linepoints;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnMouseDown()
    {
        
        projection.lr.enabled = true;
        //aimer.gameObject.SetActive(true);
        //dragger.gameObject.SetActive(true);


    }
    private void OnMouseDrag()
    {
       
        dragPos = camRef.ScreenToWorldPoint(Input.mousePosition);
        dragger.position = dragPos; // B vector
        startPos = transform.position; // A vector


        dir = dragPos - startPos; // dir vector

        aimer.position = startPos - dir;// mirror of dir vector

        forcedir = aimer.position - transform.position;// force dir from player local pos

        //var newdir = forcedir.normalized;
        //Debug.Log(newdir);
        projection.SimulateTrajectory(transform.position,forcedir,maxForce);
    }
    private void OnMouseUp()
    {
        projection.lr.enabled = false;
        //aimer.gameObject.SetActive(false);
        //dragger.gameObject.SetActive(false);

        startPos = transform.position;

        endPos = camRef.ScreenToWorldPoint(Input.mousePosition);
        dir = endPos - startPos;
        aimer.position = startPos - dir;
        
        forcedir = aimer.position - transform.position;
        
        rb.velocity =  forcedir* maxForce;
        //rb.AddForce(forcedir.normalized * maxForce, ForceMode2D.Impulse);
       
        
        //lr.enabled = false;
        mouseClicked = false;

        //Debug.Log(aimer.position);

    }

    //void DrawProjectile()
    //{
    //    for(int i=0;i<lr.positionCount;i++)
    //    {
    //        var eachpos = CurrentPos(i * 0.1f);
    //        lr.SetPosition(i,new Vector3(eachpos.x,eachpos.y,0));
    //    }
    //}

    //Vector2 CurrentPos(float t)
    //{
    //    // s = ut+1/2at2
    //    // s(displacement) => p1(current)-p0(starting)
    //    // p1 = p0 + ut+1/2at2
    //    var currentpos = (Vector2)transform.position + (forcedir.normalized * maxForce * t) + (0.5f * Physics2D.gravity * t * t);
    //    return currentpos;
    //}
}
