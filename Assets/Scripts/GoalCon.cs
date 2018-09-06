using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCon : MonoBehaviour {

    //rimortCon取得
    RemortCon remocon;

    // Use this for initialization
    void Start () {
        //リモート取得
        this.remocon = GameObject.Find("Remort").GetComponent<RemortCon>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            remocon.goalunis += 1;
            Destroy(other.gameObject);
        }

    }
}
