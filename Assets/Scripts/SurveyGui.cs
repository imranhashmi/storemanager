using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveyGui : BasicListGui<Survey> { 
	
	public override void AddItem( string date, string title, string comments )
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

	public override void DeleteItem()
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
				AppController.instance.allSurveys.Remove (tt.item);
				DestroyImmediate (tt.gameObject);
			}
			Show (AppController.instance.allSurveys);
		}
	} 

	public override void ItemSelected()
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
