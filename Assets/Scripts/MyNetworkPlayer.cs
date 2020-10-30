using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text displayNameText = null;
    [SerializeField] private Renderer displayColorRenderer = null;

    [SyncVar(hook = nameof(HandleDisplayNameUpdated))]
    [SerializeField]
    string displayName = "Missing Name";
    
    [SyncVar(hook =nameof(HandleDisplayColourUpdated))]
    [SerializeField] 
    private Color displayColor = Color.black;


    #region Server
    //[Server will only let the server change the variable]
    public void SetDisplayName(string _displayName)
    {
        displayName = _displayName;
    }

    [Server]//[Server will only let the server change the variable]
    public void SetDisplayColour(Color _displayColor)
    {
        displayColor = _displayColor;
    }

    [Command] 
    private void CmdSetDisplayName(string _newDisplayName)//Client to server funciton
    {
        if (_newDisplayName.Length < 3) { return; }   //server validation would go here
        
        RpcLogNewName(_newDisplayName);//server calling a client function
        SetDisplayName(_newDisplayName);
        
    }

    #endregion

    #region Client
    public void HandleDisplayNameUpdated(string _oldName, string _newName)//it needs the old to take two vars to update
    {
        displayNameText.text = _newName;
    }

    public void HandleDisplayColourUpdated(Color _oldColour, Color _newColor)//it needs the old to take two vars to update
    {
        displayColorRenderer.material.SetColor("_Color", _newColor);
    }

    //get name from inspector 
    [ContextMenu ("Set My Name")]
    private void SetMyname()
    {
        CmdSetDisplayName("My");
    }

    [ClientRpc]
    private void RpcLogNewName(string _newDisplayName)//server calling a client function
    {
        Debug.Log(_newDisplayName);
    }
    #endregion
}
