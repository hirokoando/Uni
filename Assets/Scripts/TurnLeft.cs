using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLeft : MonoBehaviour {

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
        if(other.tag == "Ground")
        {
            unicon.turn = false;

            if(unicon.unistate == UniCon.UniState.Clim && unicon.climL == false)
            {
                unicon.climR = true;
            }
            
        }
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (unicon.unistate == UniCon.UniState.Clim && unicon.climR == true)
        {
            if (other.tag == "Ground")
            {
                unicon.turn = true;
                unicon.climFin = true;
            }
        }

    }

}
