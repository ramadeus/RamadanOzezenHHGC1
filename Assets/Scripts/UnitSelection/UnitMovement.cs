using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitMovement: MonoBehaviour {
    // ıı
    Camera myCam;
    public LayerMask ground;
    private void Start()
    {
        myCam = Camera.main;

    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {

            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {

                //go go go

            }
        }
    }
}
