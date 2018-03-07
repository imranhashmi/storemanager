using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : BasicItem<Task> { 

	public override void Setup(Task t )
	{
		base.Setup(t);

 		date.text = t.date;
		title.text = t.title;
		comments.text = t.description;
	}

	public override void ConfirmSelected()
	{
		TaskGui.Instance.ItemSelected ();	
	} 

}
