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
			Reset ();
			StartCoroutine ("fillList");
		}
	}

	public void Reset()
	{
		StopCoroutine ("fillList");
		contentRect.transform.ClearChildren ();
	}

	IEnumerator fillList()
	{		
		foreach (Survey s in AppController.instance.allSurveys) { 
			GameObject go = (GameObject)Instantiate (prefab);
			go.transform.SetParent (contentRect.transform);
			go.name = "Survey " + contentRect.childCount.ToString ();
			go.transform.localScale = new Vector3 (1f, 1f, 1f);
			go.SetActive (true);

			if (s.title.IsNullOrEmpty ())
				s.title = go.name;
			
			SurveyItem item = go.GetComponent<SurveyItem> ();
			item.Setup (s); 

			updateContentRect ();

			yield return new WaitForEndOfFrame ();
		}
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
		Survey survey = new Survey (){ date = date, title = title, comments = comments, manager = new Manager(), store = new Store() };

		GameObject go = (GameObject)Instantiate (prefab);
		go.transform.SetParent (contentRect.transform);
		go.name = "Survey " + contentRect.childCount.ToString ();
		go.transform.localScale = new Vector3 (1f, 1f, 1f);
		go.SetActive (true);

		if (survey.title.IsNullOrEmpty ())
			survey.title = go.name;
		
		SurveyItem item = go.GetComponent<SurveyItem> ();
		item.Setup (survey);

		if ( AppController.instance.allSurveys != null )
			AppController.instance.allSurveys.Add (survey);

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
			for (int i = 0; i < contentRect.childCount; i++) {
				SurveyItem t = contentRect.GetChild (i).GetComponent<SurveyItem> ();
				if (t.toggle.isOn)
					toRemove.Add (t);
			} 

			foreach (SurveyItem tt in toRemove) {
				AppController.instance.allSurveys.Remove (tt.survey);
				DestroyImmediate (tt.gameObject);
			}

			Show ();
		}
	} 

	public void ItemSelected()
	{
		int howManySelected = 0;
		for (int i = 0; i < contentRect.childCount; i++) {
			SurveyItem t = contentRect.GetChild (i).GetComponent<SurveyItem> ();
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
