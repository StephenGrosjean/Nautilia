using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _movementHorizontal;
    private float _movementVertical;
    private Rigidbody2D _playerRb;
    
    public float playerSpeed = 10;
     
     
    private void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movementHorizontal = Input.GetAxisRaw("Horizontal");
        _movementVertical = Input.GetAxisRaw("Vertical");
    } 

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(_movementHorizontal,_movementVertical);

        _playerRb.velocity = movement * playerSpeed;
    }
}
