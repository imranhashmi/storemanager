using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskGui : MonoBehaviour {

	public static TaskGui instance;

 	public RectTransform contentRect;
	public GameObject prefab;
	public TaskForm form;

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
		if (AppController.instance.allTasks.Count == 0) {
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
		foreach (Task s in AppController.instance.allTasks) { 
			GameObject go = (GameObject)Instantiate (prefab);
			go.transform.SetParent (contentRect.transform);
			go.name = "Task " + contentRect.childCount.ToString ();
			go.transform.localScale = new Vector3 (1f, 1f, 1f);
			go.SetActive (true);

			if (s.title.IsNullOrEmpty ())
				s.title = go.name;
			
			TaskItem item = go.GetComponent<TaskItem> ();
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
		Task task = new Task (){ date = date, title = title, description = comments };

		GameObject go = (GameObject)Instantiate (prefab);
		go.transform.SetParent (contentRect.transform);
		go.name = "Task " + contentRect.childCount.ToString ();
		go.transform.localScale = new Vector3 (1f, 1f, 1f);
		go.SetActive (true);

		if (task.title.IsNullOrEmpty ())
			task.title = go.name;
		
		TaskItem item = go.GetComponent<TaskItem> ();
		item.Setup (task);

		if ( AppController.instance.allTasks != null )
			AppController.instance.allTasks.Add (task);

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
			List<TaskItem> toRemove = new List<TaskItem>();
			for (int i = 0; i < contentRect.childCount; i++) {
				TaskItem t = contentRect.GetChild (i).GetComponent<TaskItem> ();
				if (t.toggle.isOn)
					toRemove.Add (t);
			} 

			foreach (TaskItem tt in toRemove) {
				AppController.instance.allTasks.Remove (tt.task);
				DestroyImmediate (tt.gameObject);
			}

			Show ();
		}
	} 

	public void ItemSelected()
	{
		int howManySelected = 0;
		for (int i = 0; i < contentRect.childCount; i++) {
			TaskItem t = contentRect.GetChild (i).GetComponent<TaskItem> ();
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
