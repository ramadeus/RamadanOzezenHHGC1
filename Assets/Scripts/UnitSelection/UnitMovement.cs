using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitMovement: MonoBehaviour {
    // ıı
    Camera myCam;
    public LayerMask ground;
    Unit unit;
    private void Start()
    {
        myCam = Camera.main;
        unit = GetComponent<Unit>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        { 
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            { 
                bool isThereCollectible = hit.collider.GetComponent<ICollectible>() != null; 
                    unit.GoToTarget(hit.point, isThereCollectible); 
                //go go go

            }
        }
    }
}
