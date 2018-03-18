using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DataModels;

public class TaskGui : BasicListGui<Task> {

	public override void AddItem( string date, string title, string comments )
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

	public override void DeleteItem()
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
				AppController.instance.allTasks.Remove (tt.item);
				DestroyImmediate (tt.gameObject);
			}
			Show (AppController.instance.allTasks);
		}
	} 

	public override void ItemSelected()
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
