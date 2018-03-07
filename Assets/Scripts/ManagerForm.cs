using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerForm : BasicForm {

	public override void Submit()
	{
		base.Submit();

		if (ManagerGui.Instance != null)
			ManagerGui.Instance.AddItem (date.text, title.text, comments.text);
 	}
	 
}
