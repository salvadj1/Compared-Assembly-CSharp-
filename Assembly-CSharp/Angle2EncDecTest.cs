using System;
using UnityEngine;

// Token: 0x02000443 RID: 1091
public class Angle2EncDecTest : MonoBehaviour
{
	// Token: 0x06002803 RID: 10243 RVA: 0x0009C0A8 File Offset: 0x0009A2A8
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

	// Token: 0x06002804 RID: 10244 RVA: 0x0009C154 File Offset: 0x0009A354
	private void OnGUI()
	{
		if (this.dec == null)
		{
			this.dec = new Angle2?(this.a.decoded);
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

	// Token: 0x040013E7 RID: 5095
	public float rate = 360f;

	// Token: 0x040013E8 RID: 5096
	public GUIContent[] contents;

	// Token: 0x040013E9 RID: 5097
	private int contentIndex;

	// Token: 0x040013EA RID: 5098
	private Angle2 a;

	// Token: 0x040013EB RID: 5099
	private Angle2? dec;
}
