using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000617 RID: 1559
public class LightStyleTest : MonoBehaviour
{
	// Token: 0x06003774 RID: 14196 RVA: 0x000C9654 File Offset: 0x000C7854
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

	// Token: 0x06003775 RID: 14197 RVA: 0x000C9754 File Offset: 0x000C7954
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

	// Token: 0x06003776 RID: 14198 RVA: 0x000C9890 File Offset: 0x000C7A90
	private void Reset()
	{
		this.tests = new string[]
		{
			"pulsate"
		};
	}

	// Token: 0x04001B97 RID: 7063
	public LightStylist stylist;

	// Token: 0x04001B98 RID: 7064
	public string[] tests;

	// Token: 0x04001B99 RID: 7065
	public float spacebarTargetWeight = 1f;

	// Token: 0x04001B9A RID: 7066
	public float spacebarFadeLength = 1.3f;

	// Token: 0x04001B9B RID: 7067
	public float enterCrossfadeLength = 0.3f;

	// Token: 0x04001B9C RID: 7068
	private int index;

	// Token: 0x04001B9D RID: 7069
	private List<float> weights;
}
