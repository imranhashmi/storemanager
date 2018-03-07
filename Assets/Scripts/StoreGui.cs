using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreGui : BasicListGui<Store> {
	
	public override void AddItem(string date, string title, string comments)
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

	public override void DeleteItem()
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
				AppController.instance.allStores.Remove (tt.item);
				DestroyImmediate (tt.gameObject);
			}
			Show (AppController.instance.allStores);
		}
	} 

	public override void ItemSelected()
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
