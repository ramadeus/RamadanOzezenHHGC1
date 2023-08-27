using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick: MonoBehaviour {
    // ıı
    Camera myCam;

    public LayerMask clickable;
    public LayerMask ground;
    public GameObject groundMarker;
    private void Start()
    {
        myCam = Camera.main;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if(!hit.collider.GetComponent<PhotonView>().IsMine)
                {
                    return;
                }
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelections.Instance.ShiftClickSelect(hit.collider.GetComponent<ClickableUnit>());
                } else
                {
                    UnitSelections.Instance.ClickSelect(hit.collider.GetComponent<ClickableUnit>());
                }
            } else
            {
                if(!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelections.Instance.DeselectAll();
                }
            }
        }
        if(Input.GetMouseButtonDown(1))
        {

            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                if(!hit.collider.TryGetComponent(out ICollectible collectible))
                {
                    groundMarker.transform.position = hit.point;
                    groundMarker.SetActive(false);
                    groundMarker.SetActive(true);
                }

            }
        }
    }
}
