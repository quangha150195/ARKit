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

    // Use this for initialization
    void Start () {
       
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
            iTween.MoveTo(m_ScrollViewObject, iTween.Hash("x", m_ScrollViewObject.transform.position.x + 250, "time", 1.0f));
            iTween.RotateTo(m_BtnHiddemScrollView.GetComponentInChildren<Image>().gameObject, iTween.Hash("y", 180, "time", 2.0f));
        }
        else
        {
            iTween.MoveTo(m_ScrollViewObject, iTween.Hash("x", 1222.5f, "time", 1.0f));
            iTween.RotateTo(m_BtnHiddemScrollView.GetComponentInChildren<Image>().gameObject, iTween.Hash("y", 0, "time", 2.0f));           
        }
    }


}
