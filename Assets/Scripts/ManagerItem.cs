using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DataModels;

public class ManagerItem : BasicItem<Manager> {
	
	public override void Setup(Manager t )
	{
		base.Setup(t);

		date.text = t.phone;
		title.text = t.title;
		comments.text = t.description;
	}

	public override void ConfirmSelected()
	{
		ManagerGui.Instance.ItemSelected ();	
	}
	 
}
