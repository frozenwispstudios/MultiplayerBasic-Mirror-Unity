using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    private Camera mainCamera;

    #region Server

    [Command]
    private void CmdMove(Vector3 _position)
    {
        if (!NavMesh.SamplePosition(_position, out NavMeshHit Hit,1f, NavMesh.AllAreas)) { return; }//checks if you can do there before going there
        agent.SetDestination(Hit.position);
    }
    #endregion

    #region Client

    public override void OnStartAuthority()
    {
        //base.OnStartAuthority();
        mainCamera = Camera.main;
    }

    [ClientCallback]//stop from running on server (Client only update)
    private void Update()
    {
        if (!hasAuthority){ return; } //Only runs on this client
        if (!Input.GetMouseButtonDown(1)){ return; } //if not right click stop

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)){ return; }//if you have clean on a place to move 

        CmdMove(hit.point);//move logic

    }
    #endregion
}
