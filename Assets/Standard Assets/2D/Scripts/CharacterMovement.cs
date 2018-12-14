using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    private Vector2 Direction;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
    }

    private void Move()
    {
        transform.Translate(Direction * Speed * Time.deltaTime);
        //transform.Translate(Direction);
    }

    private void GetInput()
    {
           
       Direction = Vector2.zero;
      Direction.y = Input.GetAxis("Vertical");
        Direction.x = Input.GetAxis("Horizontal");

        //print("Something?" + Direction);
        /*
        if (Input.GetKeyDown(KeyCode.W))
        {
            print("W");
            Direction += Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            print("S");
            Direction += Vector2.right;
        }

        if (Input.GetKey(KeyCode.A))
        {
            print("A");
            Direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            print("D");
            Direction += Vector2.up;
        }
        */
    }
}
