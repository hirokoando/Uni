using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCollCon : MonoBehaviour {
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

    private void OnTriggerEnter2D(Collider2D othercoll)
    {

        if (unicon.unistate == UniCon.UniState.Bomb)
        {
            string textc = LayerMask.LayerToName(othercoll.gameObject.layer);//名前文字列

            if (  textc == "Ground")
            {
                //unicon.destroy = othercoll.gameObject;
            }

        }
    }

}
