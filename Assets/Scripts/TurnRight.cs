using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRight : MonoBehaviour {

    GameObject uni;
    UniCon unicon;

    // Use this for initialization
    void Start () {

        GameObject uni = transform.parent.gameObject;
        this.unicon = uni.GetComponent<UniCon>();
    }
	
	// Update is called once per frame
	void Update () {

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            unicon.turn = true;

            if (unicon.unistate == UniCon.UniState.Clim && unicon.climR == false)
            {
                unicon.climL = true;
            }
           
        }
           

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            unicon.turn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (unicon.unistate == UniCon.UniState.Clim && unicon.climL == true)
        {
            if (other.tag == "Ground")
            {
                unicon.turn = false;
                unicon.climFin = true;
               
            }
        }
    }

}
