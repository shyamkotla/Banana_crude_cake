using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputcontroller : MonoBehaviour
{
    Player player;
    private void Awake()
    {
        Player player = new Player();
    }
    private void OnEnable()
    {
        player.Enable();
    }
    private void OnDisable()
    {
        player.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        player.Land.movement.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
