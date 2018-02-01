using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGui : MonoBehaviour {

	public static ManagerGui instance;

	public GameObject scrollView;
	public RectTransform contentRect;
	public GameObject prefab;
	public TaskForm form;

	void Awake()
	{
		instance = this;	
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void updateTaskContentRect()
	{
		RectTransform t = contentRect.GetComponent<RectTransform>();
		float contentSizeY = (contentRect.childCount - 1) * prefab.GetComponent<RectTransform> ().sizeDelta.y;
		t.sizeDelta = new Vector2(t.sizeDelta.x, contentSizeY);
	}

	public void ShowTaskForm()
	{
		form.gameObject.SetActive (true);
		//newtaskButton.SetActive (false);
		//removetaskButton.SetActive (true);
	}

	public void AddTask( string date, int id, Manager manager, Store store )
	{

		Survey survey = new Survey (){ date = date, id = id, manager = manager, store = store };

		GameObject go = (GameObject)Instantiate (prefab);
		go.transform.SetParent (prefab.transform.parent);
		go.SetActive (true);
		SurveyItem item = go.GetComponent<SurveyItem> ();
		item.Setup (survey);

		if ( StoreManager.instance.allSurveys != null )
			StoreManager.instance.allSurveys.Add (item);

		updateTaskContentRect ();

		form.gameObject.SetActive (false);
		//newtaskButton.SetActive (true);
		//removetaskButton.SetActive (false);
	}

	public void DeleteTask()
	{
		if (form.gameObject.activeSelf) {
			form.gameObject.SetActive (false);
			//newtaskButton.SetActive (true);
			//removetaskButton.SetActive (false);
		}
		else {
			// Delete any seleted task from list.
			List<SurveyItem> toRemove = new List<SurveyItem>();
			foreach (SurveyItem t in StoreManager.instance.allSurveys) {
				if (t.toggle.isOn)
					toRemove.Add (t);
			}

			foreach (SurveyItem tt in toRemove) {
				StoreManager.instance.allSurveys.Remove (tt);
				DestroyImmediate (tt.gameObject);
			}

			TaskSelected ();
		}
	} 

	public void TaskSelected()
	{
		int howManySelected = 0;
		foreach (SurveyItem t in StoreManager.instance.allSurveys) {

			if (t.toggle.isOn)
				howManySelected++;

		}	

		if (howManySelected > 0) {
			//newtaskButton.SetActive (false);
			//removetaskButton.SetActive (true);
		} else {

			//newtaskButton.SetActive (true);
			//removetaskButton.SetActive (false);

		}
	}
}
