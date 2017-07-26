using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityEngine.XR.iOS
{
public class MainScene : MonoBehaviour {
    [SerializeField]
    private GameObject m_ScrollViewObject;
    [SerializeField]
    private Toggle m_BtnHiddemScrollView;

    private GameObject m_btnHidden;
    private float m_PosXScroll;
    private float m_PosXBtnHidden;
    // Use this for initialization
    void Start () {
        m_btnHidden = m_BtnHiddemScrollView.GetComponentInChildren<Image>().gameObject;
        m_PosXScroll = m_ScrollViewObject.transform.position.x;
        m_PosXBtnHidden = m_btnHidden.transform.position.x;
    }

        bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes, Transform trans)
        {
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
            if (hitResults.Count > 0) {
                foreach (var hitResult in hitResults) {
                    Debug.Log ("Got hit!");
                    trans.position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
                    trans.rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);            
                    return true;
                }
            }
            return false;
        }
                

	// Update is called once per framea
	void Update () {
            
	}

        public void addObject(string _name)
    {
            Vector3 screenPosition = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width/2,Screen.height/2,1));
            ARPoint point = new ARPoint {
                x = screenPosition.x,
                y = screenPosition.y
            }; 

            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, 
                ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent);
            if (hitResults.Count > 0) {
                foreach (var hitResult in hitResults) {
                    Vector3 position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
                    GameObject gameObj = Instantiate(Resources.Load("_Model/" + _name), position, Quaternion.identity) as GameObject;
                    iTween.ScaleFrom(gameObj, Vector3.zero, 1);
                    break;
                }
            }
    }

    public void showScrollView()
    {
        if(m_BtnHiddemScrollView.isOn)
        {
            iTween.MoveTo(m_ScrollViewObject, iTween.Hash("x", m_PosXScroll + 250, "time", 1.0f));
            iTween.MoveTo(m_btnHidden, iTween.Hash("x", m_PosXBtnHidden + 160, "time", 1.0f));
            iTween.RotateTo(m_btnHidden , iTween.Hash("y", 180, "time", .5f));
        }
        else
        {
            iTween.MoveTo(m_ScrollViewObject, iTween.Hash("x", m_PosXScroll, "time", 1.0f));
            iTween.MoveTo(m_btnHidden, iTween.Hash("x", m_PosXBtnHidden, "time", 1.0f));
            iTween.RotateTo(m_btnHidden, iTween.Hash("y", 0, "time", .5f));           
        }
    }

    }
}
