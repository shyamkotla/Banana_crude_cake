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
    Rigidbody2D rb;
    Vector2 dir;
    Camera camRef;

    // Start is called before the first frame update
    void Start()
    {
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
        
    }
    private void OnMouseDrag()
    {
        dragPos = camRef.ScreenToWorldPoint(Input.mousePosition);
        dragger.position = dragPos; // B vector
        startPos = transform.position; // A vector

        dir = dragPos - startPos; // dir vector

        aimer.position = startPos - dir;// mirror of dir vector
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
        rb.AddForce(newdirection*force, ForceMode2D.Impulse);
        

    }
}
