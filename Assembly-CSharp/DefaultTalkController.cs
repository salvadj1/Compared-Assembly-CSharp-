using System;
using MoPhoGames.USpeak.Interface;
using UnityEngine;

// Token: 0x020000D5 RID: 213
[AddComponentMenu("USpeak/Default Talk Controller")]
public class DefaultTalkController : MonoBehaviour, MoPhoGames.USpeak.Interface.IUSpeakTalkController
{
	// Token: 0x06000494 RID: 1172 RVA: 0x00016E8C File Offset: 0x0001508C
	public void OnInspectorGUI()
	{
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x00016E90 File Offset: 0x00015090
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

	// Token: 0x04000411 RID: 1041
	[SerializeField]
	[HideInInspector]
	public KeyCode TriggerKey;

	// Token: 0x04000412 RID: 1042
	[SerializeField]
	[HideInInspector]
	public int ToggleMode;

	// Token: 0x04000413 RID: 1043
	private bool val;
}
