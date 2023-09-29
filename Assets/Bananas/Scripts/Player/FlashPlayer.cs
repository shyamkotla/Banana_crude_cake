using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashPlayer : MonoBehaviour
{
    [SerializeField] float flashDuration = 0.5f;
    [SerializeField] float onHitUpForce = 3f;
    [SerializeField] float camShakeStrength = 6f;
    [SerializeField] float camShakeDuration = 0.4f;
    [SerializeField] Material flashMaterial;
    [SerializeField] Rigidbody2D myRigidbody;
    Material originalMaterial;
    SpriteRenderer mySpriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        //myRigidbody= GetComponent<Rigidbody2D>();
        mySpriteRenderer= GetComponent<SpriteRenderer>();
        originalMaterial = mySpriteRenderer.material;
    }

    public void FlashOnHit(Vector2 position)
    {
        //this.transform.position = position; 
        mySpriteRenderer.material = flashMaterial;
        //stop the rb on hit
        myRigidbody.velocity = new Vector2(0f,myRigidbody.velocity.y);
        
        //camera shake on hit

        Camera.main.DOShakePosition(camShakeDuration, camShakeStrength, 10, 50, false).SetUpdate(true);
        //trigger respawn effect after some delay
        UnityTimer.Timer.Register(flashDuration, () =>
        {
            mySpriteRenderer.material = originalMaterial;
            RespawnPlayer.instance.MoveToCheckpoint(transform.position);
        });
        //add upward force
        myRigidbody.AddForce(Vector2.up * onHitUpForce,ForceMode2D.Impulse);
    }
}
