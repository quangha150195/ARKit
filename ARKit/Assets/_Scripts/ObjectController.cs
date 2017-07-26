using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class ObjectController : MonoBehaviour {

    public Transform m_HitTransform;

    private Vector3 screenPoint;
    private Vector3 offset;

    private bool m_IsRotating;

    private float m_Sensitivity = 0.4f;
    private Vector3 m_MouseReference;
    private Vector3 m_MouseOffset;
    private Vector3 m_Rotation = Vector3.zero;

    // Use this for initialization
    void Start () {

		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                if(hitInfo.collider.tag == "rotate")
                {
                    rotation();
                }
            }
        }
    }

    void OnMouseDown()
    {
        m_IsRotating = true;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {      
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;

            ARPoint point = new ARPoint
            {

                x = curPosition.x,
                y = curPosition.y
            };

            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface().HitTest(point,
                ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent);
            if (hitResults.Count > 0)
            {
                foreach (var hitResult in hitResults)
                {
                    Vector3 position = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
                    break;
                }
            }
        }
    }

    public void OnMouseUp()
    {
        m_IsRotating = false;
    }

    public void rotation()
    {
        //if (m_IsRotating)
        {
            m_MouseOffset = (Input.mousePosition - m_MouseReference);
            m_Rotation.y = -(m_MouseOffset.x + m_MouseOffset.z) * m_Sensitivity;

            gameObject.transform.Rotate(m_Rotation);

            m_MouseReference = Input.mousePosition;

        }
    }

    public void scale()
    {
        Vector3 newScale = gameObject.transform.localScale;

    }
}
