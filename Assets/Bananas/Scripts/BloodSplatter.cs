using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class BloodSplatter : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject splatterPrefab;
    
    Transform splatHolder;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    ParticleSystem particles;
    #endregion

    #region UnityMethods
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    void Update()
    {

    }
    #endregion
    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particles, other, collisionEvents);
        int count = collisionEvents.Count;
        for (int i = 0; i < count; i++)
        {
            Instantiate(splatterPrefab, collisionEvents[i].intersection, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        }
    }
    #region PublicMethods

    #endregion

    #region PrivateMethods

    #endregion

    #region GameEventListeners

    #endregion
}