using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetMan : MonoBehaviourPunCallbacks
{
    Spinner lavaBladeSpinner;
    // Start is called before the first frame update
    void Start()
    {
        lavaBladeSpinner = GameObject.Find("LavaBlade").GetComponent<Spinner>();
    }

    //Runs on each laptop when they join the game
    public override void OnJoinedRoom() {
        lavaBladeSpinner.transform.eulerAngles = 
            lavaBladeSpinner.spinVec * (float) PhotonNetwork.Time;
    }
}
