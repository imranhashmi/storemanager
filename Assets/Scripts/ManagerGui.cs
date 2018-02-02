﻿using System.Collections;
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

	void updateContentRect()
	{
		RectTransform t = contentRect.GetComponent<RectTransform>();
		float contentSizeY = (contentRect.childCount - 1) * prefab.GetComponent<RectTransform> ().sizeDelta.y;
		t.sizeDelta = new Vector2(t.sizeDelta.x, contentSizeY);
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
			foreach (ManagerItem t in AppController.instance.allManagers) {
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

		Manager manager = new Manager (){ title = title, discription = comments };

		GameObject go = (GameObject)Instantiate (prefab);
		go.transform.SetParent (prefab.transform.parent);
		go.SetActive (true);
		ManagerItem item = go.GetComponent<ManagerItem> ();
		item.Setup (manager);

		if ( AppController.instance.allManagers != null )
			AppController.instance.allManagers.Add (item);

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
			foreach (ManagerItem t in AppController.instance.allManagers) {
				if (t.toggle.isOn)
					toRemove.Add (t);
			}

			foreach (ManagerItem tt in toRemove) {
				AppController.instance.allManagers.Remove (tt);
				DestroyImmediate (tt.gameObject);
			}

			ItemSelected ();
		}
	} 

	public void ItemSelected()
	{
		int howManySelected = 0;
		foreach (ManagerItem t in AppController.instance.allManagers) {

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
