using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelFadeEffect : MonoBehaviour {
	
	public static float Duration = 0.5f;
	public new bool enabled ;
	public bool Enabled {
		get { return enabled;}
		set {
			if (enabled == value && gameObject.activeSelf == value) return;
			enabled = value;
			if (enabled)
				canvasGroup.alpha = 0;
			gameObject.SetActive(true);
		}
	}

	void OnEnable()
	{
		enabled = true;
	}

	public float duration {
		get { return enabled?Duration:-Duration;}
	}

	CanvasGroup _canvasGroup ;
	public CanvasGroup canvasGroup {
		get { if (_canvasGroup == null) {
				_canvasGroup = GetComponent<CanvasGroup>();
				if (_canvasGroup == null)
					_canvasGroup = gameObject.AddComponent<CanvasGroup>();
				} 
				return _canvasGroup;
		}
	}

	void Update() {
		canvasGroup.alpha += Time.deltaTime / duration;
		if (!enabled && canvasGroup.alpha <= 0)
			gameObject.SetActive(false);
	}
}
