using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    //when you connect to server
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("I connected to server");
        
    }

    public override void OnServerAddPlayer (NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        Color playerColor = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f));

        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();
        player.SetDisplayName($"Player{numPlayers}");
        player.SetDisplayColour(playerColor);
  
        //Debug.Log("Player Was Added");
        //Debug.Log($"PlayerCount: {numPlayers}");
    }
}
