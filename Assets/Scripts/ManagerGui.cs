using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerGui : MonoBehaviour {

	public static ManagerGui instance;

 	public RectTransform contentRect;
	public GameObject prefab;
	public ManagerForm form;

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
		if (AppController.instance.allManagers.Count == 0) {
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
		foreach (Manager s in AppController.instance.allManagers) { 
			GameObject go = (GameObject)Instantiate (prefab);
			go.transform.SetParent (contentRect.transform);
			go.name = "Manager " + contentRect.childCount.ToString ();
			go.transform.localScale = new Vector3 (1f, 1f, 1f);
			go.SetActive (true);

			if (s.title.IsNullOrEmpty ())
				s.title = go.name;
			
			ManagerItem item = go.GetComponent<ManagerItem> ();
			item.Setup (s); 

			updateContentRect ();

			yield return new WaitForEndOfFrame ();
		}
	}

	void updateContentRect()
	{
		RectTransform t = contentRect.GetComponent<RectTransform>();
		float contentSizeY = contentRect.childCount * prefab.GetComponent<RectTransform> ().sizeDelta.y;
		t.sizeDelta = new Vector2(t.sizeDelta.x, contentSizeY);
	}

	public void ShowForm()
	{
		form.gameObject.SetActive (true);
		addButton.gameObject.SetActive (false);
		removeButton.gameObject.SetActive (true);
	}

	public void AddItem(string date, string title, string comments)
	{
		Manager manager = new Manager (){ phone = date, title = title, description = comments };

		GameObject go = (GameObject)Instantiate (prefab);
		go.transform.SetParent (contentRect.transform);
		go.name = "Manager " + contentRect.childCount.ToString ();
		go.transform.localScale = new Vector3 (1f, 1f, 1f);
		go.SetActive (true);

		if (manager.title.IsNullOrEmpty ())
			manager.title = go.name;

		ManagerItem item = go.GetComponent<ManagerItem> ();
		item.Setup (manager);

		if ( AppController.instance.allManagers != null )
			AppController.instance.allManagers.Add (manager);

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
			List<ManagerItem> toRemove = new List<ManagerItem>();
			for (int i = 0; i < contentRect.childCount; i++) {
				ManagerItem t = contentRect.GetChild (i).GetComponent<ManagerItem> ();
				if (t.toggle.isOn)
					toRemove.Add (t);
			}

			foreach (ManagerItem tt in toRemove) {
				AppController.instance.allManagers.Remove (tt.manager);
				DestroyImmediate (tt.gameObject);
			}

			Show ();
		}
	} 

	public void ItemSelected()
	{
		int howManySelected = 0;
		for (int i = 0; i < contentRect.childCount; i++) {
			ManagerItem t = contentRect.GetChild (i).GetComponent<ManagerItem> ();
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
