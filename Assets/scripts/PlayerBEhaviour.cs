using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerBEhaviour : MonoBehaviour {

	public int jumpHeight;
	public float moveSpeed;
	public int maxJump;
	public bool isAlive;
    public AudioClip coinClip;

    AudioSource collectablesAudio;

    private int numJumps;
	private Rigidbody2D body2D;
    private Animation anim;


    void Awake(){
		isAlive = true;
	}


	void Start () {

		numJumps = 0;
		body2D = GetComponent<Rigidbody2D> ();
        GetComponent<Animator>().SetBool("isJumping", false);
        GetComponent<Animator>().SetBool("isRunning", false);
    }
	

	void Update () {
		if (isAlive == true) {

		

			var val = Input.GetAxis ("Horizontal");
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                GetComponent<Animator>().SetBool("isRunning", true);
                GetComponent<Animator>().SetBool("isJumping", false);
            }
            body2D.velocity = new Vector2 (moveSpeed * val, body2D.velocity.y);
			if (Input.GetKeyDown (KeyCode.Space) && CanJump ()) {
				float x = GetComponent<Rigidbody2D> ().velocity.x;
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (x, jumpHeight);
                GetComponent<Animator>().SetBool("isJumping", true);
                GetComponent<Animator>().SetBool("isRunning", false);
                ++numJumps;
			}	
		}	else {
			Debug.Log("Died");
	}


    }
	void OnCollisionEnter2D (Collision2D coll){
		if (coll.gameObject.CompareTag ("Ground")) {
			numJumps = 0;
		}
		if (coll.gameObject.tag == "Enemies") {
			if (isAlive) {
				isAlive = false;
			}
		
		}

	}

	void OnTriggerEnter2D(Collider2D collider2d){
		if(Coins.instance != null){
			Coins.instance.SetScore();
            Destroy(collider2d.gameObject);
            collectablesAudio = GetComponent<AudioSource>();
            collectablesAudio.clip = coinClip;
            collectablesAudio.Play();

        }
	}


	bool CanJump(){
		return numJumps < maxJump;
	}

}
