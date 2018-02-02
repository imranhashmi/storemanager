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

	void updateContentRect()
	{
		RectTransform t = contentRect.GetComponent<RectTransform>();
		float contentSizeY = (contentRect.childCount - 1) * prefab.GetComponent<RectTransform> ().sizeDelta.y;
		t.sizeDelta = new Vector2(t.sizeDelta.x, contentSizeY);
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
			foreach (StoreItem t in AppController.instance.allStores) {
				t.toggle.isOn = false;
			}
		}
	}

	public void ShowForm()
	{
		form.gameObject.SetActive (true);
		addButton.gameObject.SetActive (false);
		removeButton.gameObject.SetActive (true);
	}

	public void AddItem(string date, string title, string comments)
	{

		Store store = new Store (){ title = title, address = comments };

		GameObject go = (GameObject)Instantiate (prefab);
		go.transform.SetParent (prefab.transform.parent);
		go.SetActive (true);
		StoreItem item = go.GetComponent<StoreItem> ();
		item.Setup (store);

		if ( AppController.instance.allStores != null )
			AppController.instance.allStores.Add (item);

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
			foreach (StoreItem t in AppController.instance.allStores) {
				if (t.toggle.isOn)
					toRemove.Add (t);
			}

			foreach (StoreItem tt in toRemove) {
				AppController.instance.allStores.Remove (tt);
				DestroyImmediate (tt.gameObject);
			}

			ItemSelected ();
		}
	} 

	public void ItemSelected()
	{
		int howManySelected = 0;
		foreach (StoreItem t in AppController.instance.allStores) {

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
