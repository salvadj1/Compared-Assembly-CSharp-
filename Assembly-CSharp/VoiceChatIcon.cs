using System;
using UnityEngine;

// Token: 0x020003FA RID: 1018
public class VoiceChatIcon : MonoBehaviour
{
	// Token: 0x06002565 RID: 9573 RVA: 0x0008FA60 File Offset: 0x0008DC60
	private void OnEnable()
	{
		this.label = base.GetComponent<dfLabel>();
	}

	// Token: 0x06002566 RID: 9574 RVA: 0x0008FA70 File Offset: 0x0008DC70
	private void Update()
	{
		if (this.label == null)
		{
			return;
		}
		float num = 0f;
		if (GameInput.GetButton("Voice").IsDown())
		{
			num = USpeaker.CurrentVolume;
		}
		this.label.Opacity = Mathf.Clamp(num * 20f, 0f, 1f);
	}

	// Token: 0x04001221 RID: 4641
	private dfLabel label;
}
