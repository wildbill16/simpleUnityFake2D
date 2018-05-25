using System;
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(dasPlatformCharacter))]
public class dasPlayerControl : MonoBehaviour {
    public bool allowInput = true;
    private dasPlatformCharacter player;
    private bool jumping=false;
    // Use this for initialization
    void Start () {
	
	}

    private void Awake()
    {
        player = GetComponent<dasPlatformCharacter>();
    }

    // Update is called once per frame
    private void Update () {
        
            if (!jumping)
            {
                // Read the jump input in Update so button presses aren't missed.
                jumping = Input.GetButtonDown("Jump");
            }
    }

    private void FixedUpdate()
    {
        // Read the inputs.
        if (allowInput)     //Other functions can disallow input for a certain amount of time
        {
            float h = Input.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            player.Move(h, jumping);
            jumping = false;
        }
    }
}
