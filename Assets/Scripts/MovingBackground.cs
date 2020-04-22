using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    public float scrollSpeed;
    public GameObject background;

    // Update is called once per frame
    void Update()
    {
        background.transform.position += new Vector3(0, -scrollSpeed * Time.deltaTime,0);
    }
}
