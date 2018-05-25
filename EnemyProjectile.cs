using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {
    public AudioClip hitplayer;
    AudioSource EPSFX_Source;
    private float timeStart;//used for gc
	// Use this for initialization

    public float projectileLifetime=2.0f;
	void Start () {
        EPSFX_Source = GetComponent<AudioSource>();
        timeStart = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	    if ((Time.time-timeStart)> projectileLifetime)
        {
            Object.Destroy(gameObject);
        }
	}
    
    private void OnCollisionEnter(Collision whocollidewithme)
    {
        MeshRenderer thisbulletmesh = gameObject.GetComponent<MeshRenderer>();
        Collider thisshellcollider = gameObject.GetComponent<SphereCollider>();
        if (whocollidewithme.gameObject.tag == "Player") //collision is a event
        {
            Debug.Log("Soundhere");
            EPSFX_Source.PlayOneShot(hitplayer);
            thisbulletmesh.enabled = false;
            thisshellcollider.enabled = false;
            //Object.Destroy(gameObject);
            StartCoroutine(mysleep());
            
            
            
        }
    }

    IEnumerator mysleep()
    {
        
        yield return new WaitForSeconds(0.5f);
        Object.Destroy(gameObject);
    }
}
