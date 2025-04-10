using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherGO = collision.gameObject;
        if(otherGO.tag == "Player" && PhotonNetwork.IsMasterClient) {
            //give player 10 points
            //PlayerController pc = otherGO.GetComponent<PlayerController>();
            //pc.score += 10;

            //send to every clone of this player an RPC msg
            PhotonView pv = otherGO.GetComponent<PhotonView>();
            pv.RPC("increaseScore", RpcTarget.AllBuffered, 10);
        }
    }
}
