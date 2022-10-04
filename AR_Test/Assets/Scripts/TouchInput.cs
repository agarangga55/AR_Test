using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    private Touch touch;

    public Quaternion rotateY;

    public Vector2 touchPos;
    public Vector2 firstTouch;
    public Vector2 secondTouch;

    public float rotateSpeed = 0.2f;
    public float stationaryRotateSpd;
    public float currentDistance;
    public float previousDistance;

    public bool isNotMultiTouch = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, stationaryRotateSpd * 10 * Time.deltaTime, 0);
        SwipeRotate();
        PinchScale();
    }

    // Rotate Function
    public void SwipeRotate()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                rotateY = Quaternion.Euler(0f, -touch.deltaPosition.x * rotateSpeed, 0f);
                transform.rotation = rotateY * transform.rotation;
            }
        }
    }

    // Scale / Zoom Function
    public void PinchScale()
    {
        if (Input.touchCount > 1)
        {
            firstTouch = Input.GetTouch(0).position;
            secondTouch = Input.GetTouch(1).position;
            currentDistance = secondTouch.magnitude - firstTouch.magnitude;
            if (isNotMultiTouch)
            {
                previousDistance = currentDistance;
                isNotMultiTouch = false;
            }
            if (currentDistance != previousDistance)
            {
                Vector3 scaleDistance = transform.localScale * (currentDistance / previousDistance);
                transform.localScale = scaleDistance;
                previousDistance = currentDistance;
            }
        }
        else
        {
            isNotMultiTouch = true;
        }
    }
}
