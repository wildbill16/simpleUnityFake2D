using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gameplay_Victory : MonoBehaviour {
    public GameObject WLResult;
    public bool isGameWin=false;
	// Use this for initialization
	void Start () {
        WLResult = GameObject.FindGameObjectWithTag("Canvas");

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            Debug.Log("Should win");
            Text thistext= WLResult.GetComponentInChildren<UnityEngine.UI.Text>();
            thistext.text = "Youwin!";
            isGameWin = true;
            
        }
    }
}
