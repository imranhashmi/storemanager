using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelAlphaEffect : MonoBehaviour {
	
	public float Duration = 1;
	public Graphic[] graphics;
	public new bool enabled ;
	public bool Enabled {
		get { return enabled;}
		set {
			if (enabled == value) return;
			enabled = value;
			if (enabled) 
			{
				if (graphics == null || graphics.Length == 0)
					Start();
				Alpha = 0;
				gameObject.SetActive(true);
			}
		}
	}
	
	
	void Start() {
		List<Graphic> allgraphics = new List<Graphic>( GetComponentsInChildren<Graphic>());
		allgraphics.AddRange(GetComponents<Graphic>());
		graphics = allgraphics.ToArray();
	}
	
	float alpha = 1;
	public float Alpha{
		get { return alpha;}
		set {
			value = Mathf.Clamp( value, 0f, 1f);
			if (alpha == value) return;
			alpha = value;
			foreach( var graph in graphics)
			{
				graph.canvasRenderer.SetAlpha( alpha);
			}
		}
	}
	
	void LateUpdate() {
		foreach( var graph in graphics)
		{
				graph.canvasRenderer.SetAlpha( alpha);
		}
	}
	/*void LateUpdate() {
		if (Alpha < 1 && enabled)
		{
			Alpha += Time.deltaTime / Duration;
		}
		else
		if (Alpha > 0 && !enabled)
		{
			Alpha -= Time.deltaTime / Duration;
		}
	}*/
}
