using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScroll : MonoBehaviour
{
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

    [SerializeField] private float maxHeight = 45.0f;
    [SerializeField] private float minHeight = 5.0f;

    void Start()
    {
        isLerping = false;
        scrolledHeight = 0f;
    }

    

    // Move to the target end position.
    void Update()
    {
        if (isLerping)
        {
            towerLerp();
        }

        screenHeight = transform.GetComponent<Tower>().getCurHeight() - scrolledHeight;
        if (screenHeight > maxHeight)
        {
            startScroll();
            scrolledHeight += (maxHeight - minHeight);
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
        }
    }

    private void startScroll()
    {
        initialize();
        isLerping = true;
    }

    private void initialize()
    {
        startTime = Time.time;

        startPos = transform.position;
        endPos = startPos + new Vector3(0f, -10f, 0f);
        // Keep a note of the time the movement started.

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startPos, endPos);
    }
}
