using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    //private variable that has a refernece to the camera
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        //use GetComponent<camera> to get a reference to the camera
        cam = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        // players clicks the left mouse button for this code to work
        if (Input.GetMouseButtonDown(0))
        {
            // stores location in middle of screen
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);
            // creates a ray
            Ray ray = cam.ScreenPointToRay(point);

            //create raycasthit 
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit: " + hit.point);
            }
        }


    }
}
