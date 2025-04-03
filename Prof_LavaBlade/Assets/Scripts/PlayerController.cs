using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks
{
    //Connected Components
    Rigidbody myBod;
    Collider myCol;
    Transform myHealthBar;
    Text myNameTxt;
    Text myScoreTxt;

    public int health;

    // Start is called before the first frame update
    void Start()
    {
        myBod = GetComponent<Rigidbody>();
        myCol = GetComponent<Collider>();
        myHealthBar = transform.Find("Canvas/GreenHealth").GetComponent<Transform>();
        myNameTxt = transform.Find("Canvas/NamePlate").GetComponent<Text>();
        myScoreTxt = transform.Find("Canvas/ScorePlate").GetComponent<Text>();

        myNameTxt.text = photonView.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) {
            //dead
            myBod.velocity = Vector3.zero;
            myCol.enabled = false;
            myNameTxt.color = Color.gray;
            myScoreTxt.color = Color.gray;
        } else if(photonView.IsMine) {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            myBod.AddForce(500 * (new Vector3(h, v, 0)) * Time.deltaTime);
            // tell the world where I (the owner) sent my cube.
            // send both position and velocity
            // this is done automatically by photon rigid body oberserver component
        }
        else {
            // recieve position from photon rigid body oberserver
        }
        
    }

    void OnTriggerStay(Collider other) {
        health--;
        myHealthBar.localScale = new Vector3(health / 100f, 1, 1);
    }
}
