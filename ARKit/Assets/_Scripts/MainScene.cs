using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addObject(string _name)
    {
        GameObject gameObj = Instantiate(Resources.Load("_Model/" + _name),new Vector3(0,0,100), Quaternion.identity) as GameObject;
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
