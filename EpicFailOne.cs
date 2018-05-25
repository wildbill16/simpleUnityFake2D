using UnityEngine;
using System.Collections;

public class EpicFailOne : MonoBehaviour {

    private void OnTriggerEnter(Collider whocollidewithme)
    {
        if (whocollidewithme.tag=="Player")
        {
            Application.LoadLevel("FakeScene");
        }
        
    }
}
