using UnityEngine;
using System.Collections;
//Modified Based on Micheal McCoy's 2D Cam Control Script
public class CameraController : MonoBehaviour {
    public GameObject objectToFollow;                       //Variable that points object to follow

    public bool alwaysFollowOnX = true;                     //Always lock to objects X position
    public bool alwaysFollowOnY = true;                     //Always lock to objects Y position
    public int followLag = 20;                              //How long it takes to zero to players location Larger numbers are slower

    //X and Y camera offsets
    public float xOffset = 0.0f;
    public float yOffset = 1.0f;

    public float cameraJiggle = 1.0f; //multiplier for camera jiggle

    private GameObject savedObjectToFollow;
    // Use this for initialization
    void Start () {
        //Set the lower limit on follow lag amount
        if (followLag < 1) followLag = 1;

        //Save the objectToFollow
        savedObjectToFollow = objectToFollow;
    }
	
	// Update is called once per frame
	void Update () {
        //Calculate the distance on the x and y between camera and player + offsets
        float xDistance = this.transform.position.x - (objectToFollow.transform.position.x + xOffset);
        float yDistance = this.transform.position.y - (objectToFollow.transform.position.y + yOffset);

        //Move the camera to the players x,y coordinates
        if (alwaysFollowOnX && alwaysFollowOnY)
            this.transform.position = new Vector3(this.transform.position.x - xDistance / followLag+0.001f*Mathf.Cos(cameraJiggle*Time.time),
                                                   this.transform.position.y - yDistance / followLag+ 0.002f * Mathf.Sin(cameraJiggle * Time.time),
                                                   this.transform.position.z);
        else if (alwaysFollowOnX && !alwaysFollowOnY)
            //Move the camera toward the players x coordinates
            this.transform.position = new Vector3(this.transform.position.x - xDistance / followLag,
                                                   this.transform.position.y,
                                                   this.transform.position.z);
        else if (!alwaysFollowOnX && alwaysFollowOnY)
            //Move the camera toward the players y coordinates
            this.transform.position = new Vector3(this.transform.position.x,
                                                   this.transform.position.y - yDistance / followLag,
                                                   this.transform.position.z);
    }

    public void StopFollowingPlayer()
    {
        objectToFollow = null;
    }

    public void StartFollowingPlayer()
    {
        objectToFollow = savedObjectToFollow;
    }
}
