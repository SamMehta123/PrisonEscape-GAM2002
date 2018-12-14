using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAndGo : MonoBehaviour {
    [SerializeField]
    float moveSpeed = 1f;

    Rigidbody2D rb;

    Touch touch;
    Vector3 touchPosition, whereToMove;
    Vector2 MoveTo;
    Vector2 MoveVector;
    bool isMoving = false;
    float previousDistanceToTouchPos, currentDistanceToTouchPos;


	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();	
	}
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 TouchPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            MoveTo = new Vector2(TouchPoint.x,TouchPoint.y );
            //MoveVector = new Vector2(TouchPoint.x - transform.position.x, TouchPoint.y - transform.position.y);
            
            isMoving = true;
        }
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), MoveTo, moveSpeed * Time.deltaTime);
           // transform.Translate(MoveTo * moveSpeed * Time.deltaTime);
            
            if (transform.position.x == MoveTo.x && transform.position.y == MoveTo.y)
            {
                isMoving = false;
            }

        }

        //if ()
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (isMoving)
    //        currentDistanceToTouchPos = (touchPosition - transform.position).magnitude;
    //    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    //    {
    //        Vector2 TouchPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
    //        print(TouchPoint);
    //        touchPosition = new Vector3(touchPosition.x, TouchPoint.y, touchPosition.z);

    //        previousDistanceToTouchPos = 0;
    //        currentDistanceToTouchPos = 0;
    //        isMoving = true;
    //        touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
    //        touchPosition.z = 0;
    //        whereToMove = (touchPosition - transform.position).normalized;
    //        rb.velocity = new Vector2(whereToMove.x * moveSpeed, whereToMove.y * moveSpeed);
    //    }
    //    if (currentDistanceToTouchPos > previousDistanceToTouchPos)
    //    {
    //        isMoving = false;
    //        rb.velocity = Vector2.zero;
    //    }
    //    if (isMoving)
    //        previousDistanceToTouchPos = (touchPosition = transform.position).magnitude;
    //}
}
