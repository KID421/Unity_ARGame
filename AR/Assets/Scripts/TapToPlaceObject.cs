using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

// 第一次套用此腳本時添加指定的元件 ARRaycastManager
[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlaceObject : MonoBehaviour
{
    [Header("想放置的物件")]
    public GameObject tapObject;

    /// <summary>
    /// AR 射線碰撞管理器
    /// </summary>
    private ARRaycastManager arRaycast;

    /// <summary>
    /// AR 射線碰撞到的物件 (集合 清單)
    /// </summary>
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    /// <summary>
    /// 點擊座標
    /// </summary>
    private Vector2 mousePos;

    private void Start()
    {
        // 取得 AR 射線管理器元件
        arRaycast = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        TapObject();
    }

    /// <summary>
    /// 點擊放置物件
    /// </summary>
    private void TapObject()
    {
        // 判斷是否點擊
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // 儲存點擊座標
            mousePos = Input.mousePosition;
            // AR 射線碰撞
            if (arRaycast.Raycast(mousePos, hits, TrackableType.PlaneWithinPolygon))
            {
                // 生成物件在點擊座標
                Pose pose = hits[0].pose;

                GameObject temp = Instantiate(tapObject, pose.position, pose.rotation);
                Vector3 look = transform.position;
                look.y = temp.transform.position.y;
                temp.transform.LookAt(look);
            }
        }
    }
}
