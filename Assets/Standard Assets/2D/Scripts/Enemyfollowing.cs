using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyfollowing : MonoBehaviour {

    public float speed;
    public float stoppingDistance;
    private float moveTimer = 3;
    private float startMove;

    private Transform target;

	// Use this for initialization
	void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        startMove = Time.time + moveTimer;
	}
	
	// Update is called once per frame
	void Update () {
        //if (Vector2.Distance(transform.position, target.position) > 3)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        //}
        if (Time.time >= startMove)
            
        {
            print("Don't drop the soap!");
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}

