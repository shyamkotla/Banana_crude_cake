using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShoot : MonoBehaviour
{
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 dragPos;
    [SerializeField] private Vector2 endPos;
    [SerializeField] private float force;
    [SerializeField] Transform aimer;
    [SerializeField] Transform dragger;
    [SerializeField] LineRenderer lr;
    Rigidbody2D rb;
    Vector2 dir;
    Camera camRef;
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        camRef = Camera.main;

        
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    private void OnMouseDown()
    {
        
        aimer.gameObject.SetActive(true);
        dragger.gameObject.SetActive(true);
        Debug.Log("started");
        lr.SetPosition(0, transform.position);

    }
    private void OnMouseDrag()
    {
        lr.enabled = true;
        dragPos = camRef.ScreenToWorldPoint(Input.mousePosition);
        dragger.position = dragPos; // B vector
        startPos = transform.position; // A vector

        lr.SetPosition(1, aimer.position);// linerenderer

        dir = dragPos - startPos; // dir vector

        aimer.position = startPos - dir;// mirror of dir vector

        angle = Vector2.Angle(aimer.position.normalized, Vector2.right);
        Debug.Log(angle);
    }
    private void OnMouseUp()
    {
        aimer.gameObject.SetActive(false);
        dragger.gameObject.SetActive(false);

        startPos = transform.position;

        endPos = camRef.ScreenToWorldPoint(Input.mousePosition);
        dir = endPos - startPos;
        var newdirection = startPos - dir;
        aimer.position = newdirection;
        newdirection.Normalize();


        var forcedir = aimer.position - transform.position;
       
        

       rb.AddForce(forcedir/**force*/, ForceMode2D.Impulse);
        
        lr.enabled = false;
        //Debug.Log(aimer.position);

    }
}
