using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskForm : BasicForm {

	public override void Submit()
	{
		base.Submit();

		if (TaskGui.Instance != null)
			TaskGui.Instance.AddItem (date.text, title.text, comments.text);
 	}

}
