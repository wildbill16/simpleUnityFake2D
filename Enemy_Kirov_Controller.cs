using UnityEngine;
using System.Collections;
//zf TM
//1.If Airship is at player left, go to player's right 3 uu +random, at Const+random speed
//2.If Airship is at player right, go to player's left 3 uu +random, at Const+random speed
//3.If close enough to player, drop bullets
//
//
//
//
//
//
//
public class Enemy_Kirov_Controller : MonoBehaviour {
    public float spawnYOffset = -0.1f;
    public float bulletdrop_interval = 0.8f;//how fast should enemy drop bullet
    public GameObject bulletSpawned;

    public GameObject whoisplayer;
    public GameObject winJudge;//get connection with the Win condition judgement system
    private Gameplay_Victory colliderWin;//get connectin with Win condition script
    //dynamics
    public float enemyAirShip_Speed = 12.0f;//how fast should enemy move
    private float enemyFinal_Speed = 3.0f;
    public float enemyAirshipMaxOffset = 5.0f;

    private Vector3 playerPosition;
    private Vector3 thisEnemyPosition;
    private float fPlayerEnemyDistance;//only calculate horizontal distance for now

    public float timestart; //when was the last bullet spawned?
                            // Use this for initialization
    private float randomspeeddiff;

    private float distancerandom;
    private bool flagNeedToMoveRight = false; //0 means need to moving left, 1 means need to moving right. Not useful now

    private Rigidbody thisrigidbody;

    //combat
    private Vector3 dropBulletPostion;

    public int magazineSize = 8;//how big is airship's magazine?
    private int currentMagazine=0;//how many bullet is fired for this ammo_rack
    private float lastshottime;

    void Start () {
        thisrigidbody = gameObject.GetComponent<Rigidbody>();

        colliderWin = winJudge.GetComponent<Gameplay_Victory>();//colliderWin can get win trigger variable

        dropBulletPostion= gameObject.transform.position+new Vector3(0.0f,-0.2f,0.0f);
        timestart = Time.time;
        if (whoisplayer.transform.position.x > gameObject.transform.position.x) //ini, player to the right? ship need to move right
        {
            flagNeedToMoveRight = true;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(enemyFinal_Speed, 0, 0);
        }
        else
        {
            flagNeedToMoveRight = false;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-enemyFinal_Speed, 0, 0);
        }
        distancerandom = Random.Range(-1.0f, 1.0f);//the diff for each piston dash
        randomspeeddiff = Random.Range(-0.5f, 0.5f);



    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void FixedUpdate()
    {
        dropBulletPostion = gameObject.transform.position + new Vector3(0.0f, -0.4f, 0.0f);
        Debug.Log(dropBulletPostion); 
        //Debug.Log(fPlayerEnemyDistance);
        enemyFinal_Speed = enemyAirShip_Speed + randomspeeddiff;//enemy's real speed

        playerPosition = whoisplayer.transform.position;
        thisEnemyPosition = gameObject.transform.position;
        fPlayerEnemyDistance = playerPosition.x - thisEnemyPosition.x;//positive means, enemy is at left
        //enemy needs to go positive faster

        

        if (fPlayerEnemyDistance>0) //enemy is left to the player
        {
            if (fPlayerEnemyDistance>enemyAirshipMaxOffset+distancerandom)
            {
                //too much, should change to all the way right, and chaneg random airship speed, change random 
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(enemyFinal_Speed,0.2f*Mathf.Cos(Time.time),0);
                distancerandom = Random.Range(-1.0f, 1.0f);//the diff for each piston dash
                randomspeeddiff = Random.Range(-0.5f, 0.5f);
                flagNeedToMoveRight = true;
            }
            else
            {
                //still needs to go to the right margain
                if (flagNeedToMoveRight)
                {
                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(enemyFinal_Speed, 0.2f * Mathf.Cos(Time.time), 0);
                }
                
                else
                {
                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-enemyFinal_Speed, 0.2f * Mathf.Cos(Time.time), 0);
                }

                trydropbullet();
            }
        }
        else //enemy is right to the player
        {
            if (fPlayerEnemyDistance<-1.0f*(enemyAirshipMaxOffset+distancerandom))
            {
                //too much, enemy should divert all the way to left
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-enemyFinal_Speed, 0.2f * Mathf.Cos(Time.time), 0);
                distancerandom = Random.Range(-1.0f, 1.0f);//the diff for each piston dash
                randomspeeddiff = Random.Range(-0.5f, 0.5f);
                flagNeedToMoveRight = false;
            }
            else
            {
                if (flagNeedToMoveRight)
                {
                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(enemyFinal_Speed, 0.2f * Mathf.Cos(Time.time), 0);
                }

                else
                {
                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-enemyFinal_Speed, 0.2f * Mathf.Cos(Time.time), 0);
                }
                trydropbullet();//still needs to go to the left margain
            }

        }

    }

    private void trydropbullet()
    {
        bool gameStatIsContinue = !colliderWin.isGameWin;
        if (Mathf.Abs(whoisplayer.transform.position.x-gameObject.transform.position.x)<2 && gameStatIsContinue)
        {
            if (currentMagazine <= magazineSize) //which means there are still bullet in the ammo rack
            {
                if ((Time.time-lastshottime>0.08f))//shooting interval
                {
                    GameObject thisnewbullet = GameObject.Instantiate(bulletSpawned, dropBulletPostion, Quaternion.Euler(0, 0, 0)) as GameObject;
                    currentMagazine = currentMagazine + 1;
                    lastshottime = Time.time;
                }

            }
            else
            {
                if ((Time.time-lastshottime)>4.0f)
                {
                    currentMagazine = 0;//reload
                }
                
            }
        }
    }
}
