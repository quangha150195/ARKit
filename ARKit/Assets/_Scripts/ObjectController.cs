using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class ObjectController : MonoBehaviour {
    [SerializeField]
    private Transform m_HitTransform;
    [SerializeField]
    private GameObject m_BtnMenu;


    private Vector3 screenPoint;
    private Vector3 offset;

    private bool m_IsRotating= false;
    private bool m_IsScaling = false;
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
                    m_IsRotating = true;
                }
                else if(hitInfo.collider.tag == "delete")
                    {
                        Destroy(this.gameObject);
                    }
                    else if(hitInfo.collider.tag == "ok")
                        {
                            m_BtnMenu.SetActive(false);
                        }
                        else if(hitInfo.collider.tag == "scale")
                            {
                                m_IsScaling = true;
                            }
                            else if(hitInfo.collider.tag == "Object")
                                {
                                    m_BtnMenu.SetActive(true);
                                }

            }
        }
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        if (m_IsRotating)
        {
            m_MouseOffset = (Input.mousePosition - m_MouseReference);
            m_Rotation.y = -(m_MouseOffset.x + m_MouseOffset.z) * m_Sensitivity;
           // m_Rotation.z = -(m_MouseOffset.x + m_MouseOffset.y) * m_Sensitivity;
            gameObject.transform.Rotate(m_Rotation);

            m_MouseReference = Input.mousePosition;

        }
        else
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

    public void scale()
    {
        Vector3 newScale = gameObject.transform.localScale;
    }
}
