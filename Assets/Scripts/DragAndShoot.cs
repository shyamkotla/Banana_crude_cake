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


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        camRef = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        MousInput();

    }

    private void MousInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            projection.lr.enabled = true;
        }
        if (Input.GetMouseButton(0))
        {
            dragPos = camRef.ScreenToWorldPoint(Input.mousePosition);
            dragger.position = dragPos; // B vector
            startPos = transform.position; // A vector


            dir = dragPos - startPos; // dir vector

            aimer.position = startPos - dir;// mirror of dir vector

            forcedir = aimer.position - transform.position;// force dir from player local pos

            projection.SimulateTrajectory(transform.position, forcedir, maxForce);
        }
        if (Input.GetMouseButtonUp(0))
        {
            projection.lr.enabled = false;

            startPos = transform.position;

            endPos = camRef.ScreenToWorldPoint(Input.mousePosition);
            dir = endPos - startPos;
            aimer.position = startPos - dir;

            forcedir = aimer.position - transform.position;

            rb.velocity = forcedir * maxForce;
           
        }
    }
}
