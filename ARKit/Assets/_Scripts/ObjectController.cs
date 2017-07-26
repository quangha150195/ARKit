using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class ObjectController : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;

    public Transform m_HitTransform;

//    bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes)
//    {
//        List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
//        if (hitResults.Count > 0) {
//            foreach (var hitResult in hitResults) {
//                Debug.Log ("Got hit!");
//                m_HitTransform.position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
//                m_HitTransform.rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);            
//                return true;
//            }
//        }
//        return false;
//    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

//        ARPoint point = new ARPoint {
//            x = offset.x,
//            y = offset.y
//        };
//        // prioritize reults types
//        ARHitTestResultType[] resultTypes = {
//            ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
//            // if you want to use infinite planes use this:
//            //ARHitTestResultType.ARHitTestResultTypeExistingPlane,
//            ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, a
//            ARHitTestResultType.ARHitTestResultTypeFeaturePoint
//        }; 
//
//        foreach (ARHitTestResultType resultType in resultTypes)
//        {
//            if (HitTestWithResultType (point, resultType))
//            {
//                return;
//            }
//        }
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

        ARPoint point = new ARPoint {
            
            x = curPosition.x,
            y = curPosition.y
        };

        List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, 
            ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent);
        if (hitResults.Count > 0) {
            foreach (var hitResult in hitResults) {
                Vector3 position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
                break;
            }
        }

//        // prioritize reults types
//        ARHitTestResultType[] resultTypes = {
//            ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
//            // if you want to use infinite planes use this:
//            //ARHitTestResultType.ARHitTestResultTypeExistingPlane,
//            ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
//            ARHitTestResultType.ARHitTestResultTypeFeaturePoint
//        }; 
//
//        foreach (ARHitTestResultType resultType in resultTypes)
//        {
//            if (HitTestWithResultType (point, resultType))
//            {
//                return;
//            }
//        }
//
    }
}
