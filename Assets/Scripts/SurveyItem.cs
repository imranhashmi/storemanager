using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DataModels;

public class SurveyItem : BasicItem<Survey> {

	public override void Setup(Survey t )
	{
		base.Setup(t);

		date.text = t.date;
		title.text = t.title;
		comments.text = t.comments;
	} 

	public override void ConfirmSelected()
	{
		base.ConfirmSelected();

		SurveyGui.Instance.ItemSelected ();	
	}

	public override void Edit()
	{
		base.Edit();

		AppController.instance.ShowTasks (item.allTasks);
	} 
	 
}
