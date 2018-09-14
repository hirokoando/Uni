using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepBCon : MonoBehaviour {

    BoxCollider2D step;
    BoxCollider2D real;

	// Use this for initialization
	void Start () {
        step = this.GetComponent<BoxCollider2D>();
        real = this.transform.Find("collider").gameObject.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Physics2D.IgnoreCollision(real, other.GetComponent<CircleCollider2D>(),true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Physics2D.IgnoreCollision(real, other.GetComponent<CircleCollider2D>(), false);
        }
    }
}
