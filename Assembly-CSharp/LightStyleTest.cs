using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006D7 RID: 1751
public class LightStyleTest : MonoBehaviour
{
	// Token: 0x06003B4C RID: 15180 RVA: 0x000D1B84 File Offset: 0x000CFD84
	private void Update()
	{
		if (Input.GetKeyDown(274))
		{
			this.index = (this.index + 1) % this.tests.Length;
		}
		if (Input.GetKeyDown(273))
		{
			this.index = (this.index + (this.tests.Length - 1)) % this.tests.Length;
		}
		if (Input.GetKey(32))
		{
			this.stylist.Blend(this.tests[this.index], this.spacebarTargetWeight, this.spacebarFadeLength);
		}
		if (Input.GetKeyDown(13))
		{
			this.stylist.CrossFade(this.tests[this.index], this.enterCrossfadeLength);
		}
		if (Input.GetKeyDown(306) | Input.GetKeyDown(305))
		{
			this.stylist.Play(this.tests[this.index]);
		}
	}

	// Token: 0x06003B4D RID: 15181 RVA: 0x000D1C84 File Offset: 0x000CFE84
	private void OnGUI()
	{
		for (int i = 0; i < this.tests.Length; i++)
		{
			if (this.index == i)
			{
				GUILayout.Box(this.tests[i], new GUILayoutOption[0]);
			}
			else
			{
				GUILayout.Label(this.tests[i], new GUILayoutOption[0]);
			}
		}
		if (Event.current.type == 7)
		{
			if (this.weights == null)
			{
				this.weights = new List<float>();
			}
			else
			{
				this.weights.Clear();
			}
			this.weights.AddRange(this.stylist.Weights);
			int count = this.weights.Count;
			for (int j = 0; j < count; j++)
			{
				GUI.Box(new Rect((float)(Screen.width / count * j), (float)(Screen.height - 120), (float)(Screen.width / count), 120f * this.weights[j]), this.weights[j].ToString());
			}
			GUI.Label(new Rect((float)(Screen.width - 400), 0f, 400f, 100f), "\nPress up and down to change light style.\nHold space to apply it through LightStylist.Blend\nPress enter to apply it through LightStylist.CrossFade\nPress ctrl to apply it through LightStylist.Play");
		}
	}

	// Token: 0x06003B4E RID: 15182 RVA: 0x000D1DC0 File Offset: 0x000CFFC0
	private void Reset()
	{
		this.tests = new string[]
		{
			"pulsate"
		};
	}

	// Token: 0x04001D7D RID: 7549
	public global::LightStylist stylist;

	// Token: 0x04001D7E RID: 7550
	public string[] tests;

	// Token: 0x04001D7F RID: 7551
	public float spacebarTargetWeight = 1f;

	// Token: 0x04001D80 RID: 7552
	public float spacebarFadeLength = 1.3f;

	// Token: 0x04001D81 RID: 7553
	public float enterCrossfadeLength = 0.3f;

	// Token: 0x04001D82 RID: 7554
	private int index;

	// Token: 0x04001D83 RID: 7555
	private List<float> weights;
}
