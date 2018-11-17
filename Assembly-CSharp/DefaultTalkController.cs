using System;
using MoPhoGames.USpeak.Interface;
using UnityEngine;

// Token: 0x020000C1 RID: 193
[AddComponentMenu("USpeak/Default Talk Controller")]
public class DefaultTalkController : MonoBehaviour, IUSpeakTalkController
{
	// Token: 0x06000416 RID: 1046 RVA: 0x000154C4 File Offset: 0x000136C4
	public void OnInspectorGUI()
	{
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x000154C8 File Offset: 0x000136C8
	public bool ShouldSend()
	{
		if (this.ToggleMode == 0)
		{
			this.val = Input.GetKey(this.TriggerKey);
		}
		else if (Input.GetKeyDown(this.TriggerKey))
		{
			this.val = !this.val;
		}
		return this.val;
	}

	// Token: 0x040003A2 RID: 930
	[HideInInspector]
	[SerializeField]
	public KeyCode TriggerKey;

	// Token: 0x040003A3 RID: 931
	[HideInInspector]
	[SerializeField]
	public int ToggleMode;

	// Token: 0x040003A4 RID: 932
	private bool val;
}
