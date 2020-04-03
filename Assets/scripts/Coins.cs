﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Coins : MonoBehaviour {

	 public static Coins instance;
     public AudioClip coinClip;

    AudioSource collectablesAudio;

    Text myscore;
	int coin = 0;

	void Awake () {
		myscore = GameObject.Find("Scoretext").GetComponent<Text> ();

		if (instance == null)
			instance = this;
	}


	void OnTriggerEnter2D(Collider2D collider2d){
			if (collider2d.tag == "coins") {
			this.SetScore ();
            collectablesAudio = GetComponent<AudioSource>();
            collectablesAudio.clip = coinClip;
            collectablesAudio.Play();
            Destroy (collider2d.gameObject);

        }
	}

	
	public void SetScore(){
		this.coin++;
		myscore.text = "" + coin;
		
	}

	}

