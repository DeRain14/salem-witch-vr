using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public static CameraControl instance;
    private Camera cam;
    private const int offset = 14;
    private float lowHeight;
    private float defaultHeight;
    private float highHeight;
    private float targetHeight;
    public enum direction
    {
        down = -1,
        none,
        up
    };
    private direction dir;
    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this;
        cam = GetComponent<Camera>();
        defaultHeight = cam.transform.position.y;
        lowHeight = defaultHeight - offset;
        highHeight = defaultHeight + offset;
        targetHeight = defaultHeight;
        dir = direction.none;
    }

    // Update is called once per frame
    void Update()
    {
        if (dir == direction.up)
        {
            cam.transform.Translate(0, offset * Time.deltaTime, 0);
            if (cam.transform.position.y >= targetHeight)
            {
                cam.transform.position = new Vector3(cam.transform.position.x, targetHeight, cam.transform.position.z);
                dir = direction.none;
            }
        }
        else if (dir == direction.down)
        {
            cam.transform.Translate(0, -1 * offset * Time.deltaTime, 0);
            if (cam.transform.position.y <= targetHeight)
            {
                cam.transform.position = new Vector3(cam.transform.position.x, targetHeight, cam.transform.position.z);
                dir = direction.none;
            }
        }
    }

    /**
    * Check if Camera has reached low, medium, or high height
    * @param level -1 for low, 0 for medium, 1 for high
    * return true if camera has reached height of interest
    */
    public bool atHeight()
    {
        return dir == direction.none;
    }

    public void adjustHeight(int level)
    {
        if (level > 0)
            targetHeight = highHeight;
        else if (level < 0)
            targetHeight = lowHeight;
        else
            targetHeight = defaultHeight;

        if (transform.position.y < targetHeight)
            dir = direction.up;
        else if (transform.position.y > targetHeight)
            dir = direction.down;
        else
            dir = direction.none;

    }

    //Get the vector pointing where the player is looking
    public Vector3 getDirection()
    {
        return cam.transform.forward;
    }
}
