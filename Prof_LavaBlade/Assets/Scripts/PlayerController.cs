using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    //Connected Components
    Rigidbody myBod;
    Collider myCol;
    Transform myHealthBar;
    Text myNameTxt;
    Text myScoreTxt;

    public int health;

    void Awake() {
        myBod = GetComponent<Rigidbody>();
        myCol = GetComponent<Collider>();
        myHealthBar = transform.Find("Canvas/GreenHealth").GetComponent<Transform>();
        myNameTxt = transform.Find("Canvas/NamePlate").GetComponent<Text>();
        myScoreTxt = transform.Find("Canvas/ScorePlate").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        myNameTxt.text = photonView.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        //I AM THE OWNER OF THIS CUBE
        if(photonView.IsMine) {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            myBod.AddForce(500 * (new Vector3(h, v, 0)) * Time.deltaTime);
            // tell the world where I (the owner) sent my cube.
            // send both position and velocity
            // this is done automatically by photonRigidbodyView component
        }
        //THIS IS SOMEONE ELSE'S CUBE
        else {
            //recieve position and velocity
            //update cube
            // this is done automatically by photonRigidbodyView component
        }

        //ALL CUBES
        myHealthBar.localScale = new Vector3(health / 100f, 1, 1);
        if(health <= 0) {
            //dead
            health = 0;
            myBod.velocity = Vector3.zero;
            myBod.constraints = RigidbodyConstraints.FreezeAll;
            myCol.enabled = false;
            myNameTxt.color = Color.gray;
            myScoreTxt.color = Color.gray;
        }         
    }

    void OnTriggerStay(Collider other) {
        //I AM THE OWNER OF THIS CUBE
        if(photonView.IsMine) {
            health--;
            //send health over network to all clones
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if(photonView.IsMine) {
            stream.SendNext(health);
        }
        else {
            health = (int) stream.ReceiveNext();
        }
    }
}
