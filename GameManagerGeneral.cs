using UnityEngine;
using System.Collections;
//
public class GameManagerGeneral : MonoBehaviour {

    private float timeStart;
    public int lengthUnitRight;
    public int lengthUnityLeft;
    public GameObject backgroundTile;
    public float desiredBackGroundDepth;
	// Use this for initialization
	void Start () {
     Instantiate(backgroundTile, new Vector3(0, 0, desiredBackGroundDepth), Quaternion.Euler(-90, 0, 0));
        for (int ibg1 = 1;ibg1<= lengthUnitRight; ibg1++)
        {
            Instantiate(backgroundTile, new Vector3(ibg1* 10.0f, 0, desiredBackGroundDepth), Quaternion.Euler(-90,0,0));
            
        }
        for (int ibg2 = 1;ibg2<=lengthUnityLeft;ibg2++)
        {
            Instantiate(backgroundTile, new Vector3(ibg2 * 10.0f * -1.0f, 0, desiredBackGroundDepth), Quaternion.Euler(-90, 0, 0));
        }
    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
