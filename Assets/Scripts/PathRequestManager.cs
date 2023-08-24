using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{
    // ıı
    Queue<PathResult> results = new Queue<PathResult>();
    //Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    //PathRequest currentPathRequest;
    Pathfinding pathfinding;
    //bool isProcessingPath;
      static PathRequestManager instance;
    private void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }
    private void Update()
    {
        if(results.Count>0)
        {
            int itemsInQueue = results.Count;
            lock(results)
            {
                for(int i = 0; i < itemsInQueue; i++)
                {
                    PathResult result = results.Dequeue();
                    result.callback(result.path, result.success);
                }
            }
        }
    }
    public static void RequestPath(PathRequest request)
    {
        ThreadStart threadStart = delegate
        {
            instance.pathfinding.FindPath(request,instance. FinishedProcessingPath);
        };
        threadStart.Invoke();
    }
    public void FinishedProcessingPath(PathResult result)
    {
        lock(results)
        {
            results.Enqueue(result);
        }
    }
   
    //public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    //{
    //    PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
    //    instance.pathRequestQueue.Enqueue(newRequest);
    //    instance.TryProcessNext();
    //}
    //void TryProcessNext()
    //{
    //    if(!isProcessingPath && pathRequestQueue.Count >0)
    //    {
    //        currentPathRequest = pathRequestQueue.Dequeue();
    //        isProcessingPath = true;
    //        pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
    //    }
    //}
    //public void FinishedProcessingPath(Vector3[] path, bool success)
    //{
    //    currentPathRequest.callback(path, success);
    //    isProcessingPath = false;
    //    TryProcessNext();
    //}

}
public struct PathResult {
    public Vector3[] path;
    public bool success;
    public Action<Vector3[], bool> callback;
    public PathResult(Vector3[] _path, bool _success, Action<Vector3[], bool> _callback)
    {
        path = _path;
        success = _success;
        callback = _callback;
    }

}
public struct PathRequest {
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public Action<Vector3[], bool> callback;

    public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callack)
    {
        pathStart = _start;
        pathEnd = _end;
        callback = _callack;
    }
}
