using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceScratch : MonoBehaviour {

    private Camera cam;

    private void Start()
    {
        // Make sure that the main camera has this tag
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update () {

        // PC
        
        // Mouse is clicked, so the player is trying to scratch, so we show the coin
        if (Input.GetMouseButton(0))
        {

            // Make coin visible
            gameObject.GetComponent<SpriteRenderer>().enabled = true;

            // Get x,y coordinates of mouse
            Vector2 mousePos = cam.ScreenPointToRay(Input.mousePosition).GetPoint(0f);


            // We just don't ever change z coordinate from it's starting value
            float z = GetComponent<Transform>().position.z;

            // Set new position
            GetComponent<Transform>().position = new Vector3(mousePos.x, mousePos.y, z);
        }
        else
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
    

        // Mobile
        /**
        if (Input.touchCount > 0)
        {

            // Make coin visible
            gameObject.GetComponent<SpriteRenderer>().enabled = true;

            // Get x,y coordinates of mouse
            Vector2 mousePos = cam.ScreenPointToRay(Input.GetTouch(0).position).GetPoint(0f);


            // We just don't ever change z coordinate from it's starting value
            float z = GetComponent<Transform>().position.z;

            // Set new position
            GetComponent<Transform>().position = new Vector3(mousePos.x, mousePos.y, z);
        }
        else
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
    */

    }
}
