using System;
using UnityEngine;

// Token: 0x020008E5 RID: 2277
[AddComponentMenu("NGUI/UI/Input (Saved)")]
public class UIInputSaved : global::UIInput
{
	// Token: 0x06004D82 RID: 19842 RVA: 0x00134504 File Offset: 0x00132704
	private void Start()
	{
		base.Init();
		if (!string.IsNullOrEmpty(this.playerPrefsField) && PlayerPrefs.HasKey(this.playerPrefsField))
		{
			base.text = PlayerPrefs.GetString(this.playerPrefsField);
		}
	}

	// Token: 0x06004D83 RID: 19843 RVA: 0x00134540 File Offset: 0x00132740
	private void OnApplicationQuit()
	{
		if (!string.IsNullOrEmpty(this.playerPrefsField))
		{
			PlayerPrefs.SetString(this.playerPrefsField, base.text);
		}
	}

	// Token: 0x04002B71 RID: 11121
	public string playerPrefsField;
}
