using System;
using UnityEngine;

// Token: 0x020007F3 RID: 2035
[AddComponentMenu("NGUI/UI/Input (Saved)")]
public class UIInputSaved : UIInput
{
	// Token: 0x060048D3 RID: 18643 RVA: 0x0012A5A0 File Offset: 0x001287A0
	private void Start()
	{
		base.Init();
		if (!string.IsNullOrEmpty(this.playerPrefsField) && PlayerPrefs.HasKey(this.playerPrefsField))
		{
			base.text = PlayerPrefs.GetString(this.playerPrefsField);
		}
	}

	// Token: 0x060048D4 RID: 18644 RVA: 0x0012A5DC File Offset: 0x001287DC
	private void OnApplicationQuit()
	{
		if (!string.IsNullOrEmpty(this.playerPrefsField))
		{
			PlayerPrefs.SetString(this.playerPrefsField, base.text);
		}
	}

	// Token: 0x04002923 RID: 10531
	public string playerPrefsField;
}
