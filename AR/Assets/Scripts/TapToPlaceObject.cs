using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlaceObject : MonoBehaviour
{
    public GameObject dragon;

    private GameObject spawnDragon;
    private ARRaycastManager arRaycastManager;
    private Vector2 touchPosition;

    public List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    private void PlaceObject()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            touchPosition = Input.GetTouch(0).position;

            if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                spawnDragon = Instantiate(dragon, hitPose.position, Quaternion.identity);
                Vector3 look = transform.position;
                look.y = spawnDragon.transform.position.y;
                spawnDragon.transform.LookAt(look);
            }
        }
    }

    private void Update()
    {
        PlaceObject();
    }
}
