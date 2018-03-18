using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DataModels;

public class ManagerGui : BasicListGui<Manager> { 

	public override void AddItem(string date, string title, string comments)
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

	public override void DeleteItem()
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
				AppController.instance.allManagers.Remove (tt.item);
				DestroyImmediate (tt.gameObject);
			}
			Show (AppController.instance.allManagers);
		}
	} 

	public override void ItemSelected()
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
