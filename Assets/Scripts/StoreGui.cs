using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreGui : MonoBehaviour {

	public static StoreGui instance;

 	public RectTransform contentRect;
	public GameObject prefab;
	public StoreForm form;

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
		if (AppController.instance.allStores.Count == 0) {
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
		foreach (Store s in AppController.instance.allStores) { 
			GameObject go = (GameObject)Instantiate (prefab);
			go.transform.SetParent (contentRect.transform);
			go.name = "Store " + contentRect.childCount.ToString ();
			go.transform.localScale = new Vector3 (1f, 1f, 1f);
			go.SetActive (true);

			if (s.title.IsNullOrEmpty ())
				s.title = go.name;
			
			StoreItem item = go.GetComponent<StoreItem> ();
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
		Store store = new Store (){  manager = date, title = title, address = comments };

		GameObject go = (GameObject)Instantiate (prefab);
		go.transform.SetParent (contentRect.transform);
		go.name = "Store " + contentRect.childCount.ToString ();
		go.transform.localScale = new Vector3 (1f, 1f, 1f);
		go.SetActive (true);

		if (store.title.IsNullOrEmpty ())
			store.title = go.name;
		
		StoreItem item = go.GetComponent<StoreItem> ();
		item.Setup (store);

		if ( AppController.instance.allStores != null )
			AppController.instance.allStores.Add (store);

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
			List<StoreItem> toRemove = new List<StoreItem>();
			for (int i = 0; i < contentRect.childCount; i++) {
				StoreItem t = contentRect.GetChild (i).GetComponent<StoreItem> ();
				if (t.toggle.isOn)
					toRemove.Add (t);
			}

			foreach (StoreItem tt in toRemove) {
				AppController.instance.allStores.Remove (tt.store);
				DestroyImmediate (tt.gameObject);
			}

			Show ();
		}
	} 

	public void ItemSelected()
	{
		int howManySelected = 0;
		for (int i = 0; i < contentRect.childCount; i++) {
			StoreItem t = contentRect.GetChild (i).GetComponent<StoreItem> ();
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
