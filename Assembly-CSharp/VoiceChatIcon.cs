using System;
using UnityEngine;

// Token: 0x020004AB RID: 1195
public class VoiceChatIcon : MonoBehaviour
{
	// Token: 0x060028DD RID: 10461 RVA: 0x00095898 File Offset: 0x00093A98
	private void OnEnable()
	{
		this.label = base.GetComponent<global::dfLabel>();
	}

	// Token: 0x060028DE RID: 10462 RVA: 0x000958A8 File Offset: 0x00093AA8
	private void Update()
	{
		if (this.label == null)
		{
			return;
		}
		float num = 0f;
		if (global::GameInput.GetButton("Voice").IsDown())
		{
			num = global::USpeaker.CurrentVolume;
		}
		this.label.Opacity = Mathf.Clamp(num * 20f, 0f, 1f);
	}

	// Token: 0x0400139E RID: 5022
	private global::dfLabel label;
}
