using UnityEngine;
using System.Collections;

public class HazardWarning : MonoBehaviour {
    public GameObject thePlayer;
    public Sprite normal;
    public Sprite shock;
    public Sprite thesprite;
	// Use this for initialization
	void Start () {
        //thesprite = thePlayer.GetComponentInChildren<SpriteRenderer>().sprite;


    }
	
	// Update is called once per frame
	void Update () {
	
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            thePlayer.GetComponentInChildren<SpriteRenderer>().sprite = shock;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            thePlayer.GetComponentInChildren<SpriteRenderer>().sprite = normal;
        }
    }
}
