using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveyGui : MonoBehaviour {

	public static SurveyGui instance;

 	public RectTransform contentRect;
	public GameObject prefab;
	public SurveyForm form;

	public Button addButton;
	public Button removeButton;

	void Awake()
	{
		instance = this;
		addButton.gameObject.SetActive (false);
		removeButton.gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void Show()
	{ 
		if (AppController.instance.allSurveys.Count == 0) {
			ShowForm (); 
		}
		else {
			form.gameObject.SetActive (false);
			addButton.gameObject.SetActive (true);
			removeButton.gameObject.SetActive (false); 
			foreach (SurveyItem t in AppController.instance.allSurveys) {
				t.toggle.isOn = false;
			}
		}
	}

	public void Hide()
	{
		
	}

	void updateContentRect()
	{
		RectTransform t = contentRect.GetComponent<RectTransform>();
		float contentSizeY = (contentRect.childCount - 1) * prefab.GetComponent<RectTransform> ().sizeDelta.y;
		t.sizeDelta = new Vector2(t.sizeDelta.x, contentSizeY);
	}

	public void ShowForm()
	{
		form.gameObject.SetActive (true);
		addButton.gameObject.SetActive (false);
		removeButton.gameObject.SetActive (true); 
 	}

	public void AddItem( string date, string title, string comments )
	{
		Survey survey = new Survey (){ date = date, title = title, manager = new Manager(), store = new Store() };

		GameObject go = (GameObject)Instantiate (prefab);
		go.transform.SetParent (prefab.transform.parent);
		go.SetActive (true);
		SurveyItem item = go.GetComponent<SurveyItem> ();
		item.Setup (survey);

		if ( AppController.instance.allSurveys != null )
			AppController.instance.allSurveys.Add (item);

		updateContentRect ();

 		form.gameObject.SetActive (false);
		addButton.gameObject.SetActive (true);
		removeButton.gameObject.SetActive (false);
	}

	public void DeleteItem()
	{
		if (form.gameObject.activeSelf) {
			form.gameObject.SetActive (false);
			addButton.gameObject.SetActive (true);
			removeButton.gameObject.SetActive (false);
		}
		else {
			// Delete any seleted item from list.
			List<SurveyItem> toRemove = new List<SurveyItem>();
			foreach (SurveyItem t in AppController.instance.allSurveys) {
				if (t.toggle.isOn)
					toRemove.Add (t);
			}

			foreach (SurveyItem tt in toRemove) {
				AppController.instance.allSurveys.Remove (tt);
				DestroyImmediate (tt.gameObject);
			}

			ItemSelected ();
		}
	} 

	public void ItemSelected()
	{
		int howManySelected = 0;
		foreach (SurveyItem t in AppController.instance.allSurveys) {

			if (t.toggle.isOn)
				howManySelected++;

		}	

		if (howManySelected > 0) {
			addButton.gameObject.SetActive (false);
			removeButton.gameObject.SetActive (true);
		} else {

			addButton.gameObject.SetActive (true);
			removeButton.gameObject.SetActive (false);

		}
	}
}
