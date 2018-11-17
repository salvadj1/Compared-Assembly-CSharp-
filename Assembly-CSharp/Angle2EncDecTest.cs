using System;
using UnityEngine;

// Token: 0x020004F9 RID: 1273
public class Angle2EncDecTest : MonoBehaviour
{
	// Token: 0x06002B93 RID: 11155 RVA: 0x000A2028 File Offset: 0x000A0228
	private void Update()
	{
		float num = Time.deltaTime * this.rate;
		if (num != 0f)
		{
			this.a.x = this.a.x + num;
			while (this.a.x > 360f)
			{
				this.a.x = this.a.x - 360f;
			}
			while (this.a.x < 0f)
			{
				this.a.x = this.a.x + 360f;
			}
			this.dec = null;
		}
	}

	// Token: 0x06002B94 RID: 11156 RVA: 0x000A20D4 File Offset: 0x000A02D4
	private void OnGUI()
	{
		if (this.dec == null)
		{
			this.dec = new global::Angle2?(this.a.decoded);
			this.contents[this.contentIndex++].text = string.Concat(new object[]
			{
				"Enc:\t",
				this.a.x,
				"\tDec:\t",
				this.dec.Value.x,
				"\tRED:\t",
				this.dec.Value.decoded.x
			});
			this.contentIndex %= this.contents.Length;
		}
		foreach (GUIContent guicontent in this.contents)
		{
			GUILayout.Label(guicontent, new GUILayoutOption[0]);
		}
	}

	// Token: 0x0400156A RID: 5482
	public float rate = 360f;

	// Token: 0x0400156B RID: 5483
	public GUIContent[] contents;

	// Token: 0x0400156C RID: 5484
	private int contentIndex;

	// Token: 0x0400156D RID: 5485
	private global::Angle2 a;

	// Token: 0x0400156E RID: 5486
	private global::Angle2? dec;
}
