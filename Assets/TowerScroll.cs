using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScroll : MonoBehaviour
{
    private GameController myGameController;

    // Transforms to act as start and end markers for the journey.
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;

    // Movement speed in units per second.
    public float speed = 5.0f;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    [SerializeField] private float screenHeight;

    [SerializeField] private float scrolledHeight;

    bool isLerping;

    bool isScrollingBack;

    private Vector3 initPos;

    [SerializeField] private float maxHeight = 45.0f;
    [SerializeField] private float minHeight = 5.0f;

    void Start()
    {
        myGameController = GameObject.FindWithTag("System").transform.Find("GameController").GetComponent<GameController>();
        isLerping = false;
        isScrollingBack = false;
        scrolledHeight = 0f;
        initPos = transform.position;
    }

    

    // Move to the target end position.
    void Update()
    {
        if (isLerping)
        {
            towerLerp();
        }

        if (!isScrollingBack)
        {
            screenHeight = transform.GetComponent<Tower>().getCurHeight() - scrolledHeight;
            if (screenHeight > maxHeight)
            {
                startScroll();
                myGameController.gameSuspended = true;
                scrolledHeight += (maxHeight - minHeight);
            }
        }

    }

    private void towerLerp()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);

        if (transform.position == endPos)
        {
            isLerping = false;
            myGameController.gameSuspended = false;
        }
        else if (isScrollingBack && transform.position.y >= initPos.y)
        {
            isLerping = false;
            myGameController.gameSuspended = false;
            scrolledHeight = startPos.y - transform.position.y;
            if (scrolledHeight <= 0f) { scrolledHeight = 0f; }
            isScrollingBack = false;
            screenHeight = 0f;
            transform.GetComponent<Tower>().setCurHeight(0f);
        }
    }

    private void startScroll()
    {
        initialize();
        isLerping = true;
    }

    public void scrollBack(float height)
    {
        isScrollingBack = true;

        startTime = Time.time;

        startPos = transform.position;
        endPos = startPos + new Vector3(0f, height, 0f);
        // Keep a note of the time the movement started.

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startPos, endPos);
        isLerping = true;

        myGameController.gameSuspended = true;
    }

    private void initialize()
    {
        startTime = Time.time;

        startPos = transform.position;
        endPos = startPos + new Vector3(0f, -(maxHeight - minHeight), 0f);
        // Keep a note of the time the movement started.

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startPos, endPos);
    }

    public float getScrolledHeight()
    {
        return scrolledHeight;
    }
}
