using System;
using UnityEngine;

// Token: 0x020008E8 RID: 2280
[AddComponentMenu("NGUI/UI/Localize")]
[RequireComponent(typeof(global::UIWidget))]
public class UILocalize : MonoBehaviour
{
	// Token: 0x06004DCA RID: 19914 RVA: 0x00135D8C File Offset: 0x00133F8C
	private void OnLocalize(global::Localization loc)
	{
		if (this.mLanguage != loc.currentLanguage)
		{
			global::UIWidget component = base.GetComponent<global::UIWidget>();
			global::UILabel uilabel = component as global::UILabel;
			global::UISprite uisprite = component as global::UISprite;
			if (string.IsNullOrEmpty(this.mLanguage) && string.IsNullOrEmpty(this.key) && uilabel != null)
			{
				this.key = uilabel.text;
			}
			string text = (!string.IsNullOrEmpty(this.key)) ? loc.Get(this.key) : loc.Get(component.name);
			if (uilabel != null)
			{
				uilabel.text = text;
			}
			else if (uisprite != null)
			{
				uisprite.spriteName = text;
				uisprite.MakePixelPerfect();
			}
			this.mLanguage = loc.currentLanguage;
		}
	}

	// Token: 0x06004DCB RID: 19915 RVA: 0x00135E68 File Offset: 0x00134068
	private void OnEnable()
	{
		if (this.mStarted && global::Localization.instance != null)
		{
			this.OnLocalize(global::Localization.instance);
		}
	}

	// Token: 0x06004DCC RID: 19916 RVA: 0x00135E9C File Offset: 0x0013409C
	private void Start()
	{
		this.mStarted = true;
		if (global::Localization.instance != null)
		{
			this.OnLocalize(global::Localization.instance);
		}
	}

	// Token: 0x04002B99 RID: 11161
	public string key;

	// Token: 0x04002B9A RID: 11162
	private string mLanguage;

	// Token: 0x04002B9B RID: 11163
	private bool mStarted;
}
