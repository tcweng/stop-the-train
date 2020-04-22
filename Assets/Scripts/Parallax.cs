using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // length is the total length of the background
    // startPos is the starting position of the background, which is also same as Camera starting position.
    private float length;
    private float startPos;

    public GameObject cam;
    // parallaxEffect is the movement speed correspond to camera movement.
    // If ParallaxEffect is 0.1, whenever camera move 1 unit, background move 0.1 unit.
    public float parallaxEffect;

    private void Start()
    {
        startPos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;

    }

    private void Update()
    {
        // if Camera Position is equal to y =1
        // temp will be 1 * (1-0.1) = 0.9.
        float temp = (cam.transform.position.y * (1 - parallaxEffect));

        // If Camera Position is equal to y = 1.
        // dist will be 1 * 0.1 = 0.1.
        float dist = (cam.transform.position.y * parallaxEffect);

        // Transform.position (Background Position) will be 1 * 0.1 = 0.1.
        transform.position = new Vector3(transform.position.x, startPos + dist, transform.position.z);

        // If Start Position = 0
        // And Length = 10
        // Temp more than 10 unit, background starting position will move 10 (length size) unit higher.
        if (temp > startPos + length)
        {
            startPos += Mathf.Round(length);
        }
        // Temp less than 10 unit, background starting position will move 10 (length size) unit lower.
        // This is the case if camera move backward (reverse)
        // But not applicable to this game as this game only move forward.
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
