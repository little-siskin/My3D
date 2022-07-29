using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour {

    private List<Button> tabBtns;
    public Button tab1;
    public Button tab2;
	public Button tab3;

    private List<GameObject> viewObjects;
	public GameObject ob1;
    public GameObject ob2 ;
	public GameObject ob3;
    private void Awake()
    {
        tabBtns = new List<Button>();
        viewObjects = new List<GameObject>();

        AddTabBtns(tab1);
        AddTabBtns(tab2);
		AddTabBtns(tab3);
 
        AddViewObjects(ob1);
        AddViewObjects(ob2);
		AddViewObjects(ob3);
        Debug.Log(ob1.name + (viewObjects.Count()).ToString());
        ob1.SetActive(false);
        ob2.SetActive(false);
		ob3.SetActive(false);
    }
    private void Start()
    {
        foreach (Button tab in tabBtns)
        {
            AddTabBtnListener(tab); 
        }
        if (tabBtns.Count > 0)
        {
            viewObjects[0].SetActive(true);
        }
        else
        {
            Debug.LogError("No exist tabBtns");
        }
    }
    public void AddTabBtns(Button tabBtn)
    {
        tabBtns.Add(tabBtn);
    }
 
    public void AddViewObjects(GameObject viewObject)
    {
        viewObjects.Add(viewObject);
    }
 
    private void AddTabBtnListener(Button tabBtn)
    {
        tabBtn.onClick.AddListener(() => SelectTabBtn(tabBtn));
    }
 
    private void SelectTabBtn(Button tabBtn)
    {
        
        for (int i = 0; i < tabBtns.Count(); i++)
        {
            bool isSelect = (tabBtns[i] == tabBtn);
            tabBtns[i].interactable = !isSelect;
            if(isSelect)
            {
                viewObjects[i].SetActive(true);
            }
            for (int j = 0; j < viewObjects.Count(); j++)
            {
                if ((j!= i)&&isSelect)
                {
                    viewObjects[j].SetActive(false);
                }
            }
        }
    }
}
