using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Unit: MonoBehaviour {
    // ıı
    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;
    public Vector3 target;
    public float speed = 20;
    public float turnSpeed = 3;
    public float turnDst = 5;
    public float stoppingDst = 10;
    Animator anim;
    [SerializeField] LayerMask collectibleLayerMask;
    Path path;
    //Vector3[] path;
    //int targetIndex;
    Coroutine followPathCR;
    Coroutine updatePathCR;
    bool isThereCollectible = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void GoToTarget(Vector3 _target,bool _isThereCollectible)
    {
        isThereCollectible = _isThereCollectible;
        target = _target;
        if(updatePathCR != null)
        {
            StopCoroutine(updatePathCR);
        }
        updatePathCR = StartCoroutine(UpdatePath());

    } 
    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if(pathSuccessful)
        {
            path = new Path(waypoints, transform.position, turnDst, stoppingDst);
            if(followPathCR != null)
            {

                StopCoroutine(followPathCR);
            }

            followPathCR = StartCoroutine(FollowPath());
        }
    }

    IEnumerator UpdatePath()
    {

        PathRequestManager.RequestPath(new PathRequest(transform.position, target, OnPathFound));
        anim.SetBool("Running", true);
        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target;
        while(true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            if((target - targetPosOld).sqrMagnitude > sqrMoveThreshold)
            {
                PathRequestManager.RequestPath(new PathRequest(transform.position, target, OnPathFound));
                targetPosOld = target;
            }

        }
    } 
    IEnumerator FollowPath()
    {
        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);
        float speedPercent = 1;

        while(followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while(path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if(pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
        anim.SetBool("Running", false);
                    if(isThereCollectible)
                    {
                        CollectCollectible();
                    }


                    break;
                } else
                {
                    pathIndex++;
                }
            }
            if(followingPath)
            {
                if(pathIndex >= path.slowDownIndex && stoppingDst > 0)
                {
                    speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);
                    if(speedPercent < 0.01f)
                    {
                        followingPath = false;
                    }
                }
                Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
            }

            yield return null;
        }
    } 
    private void CollectCollectible()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1, collectibleLayerMask);
        for(int i = 0; i < colliders.Length; i++)
        {
            //if(colliders[i].TryGetComponent(out ICollectible collectible))
            //{
            colliders[i].GetComponent<PhotonView>().RPC("Collect", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber);
            //}
        }
    } 
    public void OnDrawGizmos()
    {
        if(path != null)
        {
            path.DrawWithGizmos();
        }
    }
}
