using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviourPunCallbacks
{
    public Color[] colors;
    private int currentColor;
    Renderer myRend;

    // Start is called before the first frame update
    void Start()
    {
        myRend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        myRend.material.color = colors[currentColor];
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherGO = collision.gameObject;
        if(otherGO.tag == "Player" && PhotonNetwork.IsMasterClient) {
            //give player 10 points
            //PlayerController pc = otherGO.GetComponent<PlayerController>();
            //pc.score += 10;

            int points = 10;
            if(currentColor == 1) {
                points = -10;
            }
            //send to every clone of this player an RPC msg
            PhotonView pv = otherGO.GetComponent<PhotonView>();
            pv.RPC("increaseScore", RpcTarget.AllBuffered, points);

            //update currentColor on all clones of the ball
            photonView.RPC("setCurrentColor", RpcTarget.All, currentColor + 1);
        }
    }

    [PunRPC]
    public void setCurrentColor(int c) {
        this.currentColor = c % colors.Length; //(Or c % 2)
    }
}
