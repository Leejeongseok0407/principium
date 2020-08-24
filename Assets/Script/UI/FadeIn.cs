using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
	IEnumerator CoFadeIn(float fadeOutTime, System.Action nextEvent = null)
	{
		SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
		Color tempColor = sr.color;
		while (tempColor.a < 1f)
		{
			tempColor.a += Time.deltaTime / fadeOutTime;
			sr.color = tempColor;

			if (tempColor.a >= 1f) tempColor.a = 1f;

			yield return null;
		}

		sr.color = tempColor;
		if (nextEvent != null) nextEvent();
	}
}


