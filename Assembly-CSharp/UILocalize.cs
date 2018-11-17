using System;
using UnityEngine;

// Token: 0x020007F6 RID: 2038
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/UI/Localize")]
public class UILocalize : MonoBehaviour
{
	// Token: 0x0600491B RID: 18715 RVA: 0x0012BE28 File Offset: 0x0012A028
	private void OnLocalize(Localization loc)
	{
		if (this.mLanguage != loc.currentLanguage)
		{
			UIWidget component = base.GetComponent<UIWidget>();
			UILabel uilabel = component as UILabel;
			UISprite uisprite = component as UISprite;
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

	// Token: 0x0600491C RID: 18716 RVA: 0x0012BF04 File Offset: 0x0012A104
	private void OnEnable()
	{
		if (this.mStarted && Localization.instance != null)
		{
			this.OnLocalize(Localization.instance);
		}
	}

	// Token: 0x0600491D RID: 18717 RVA: 0x0012BF38 File Offset: 0x0012A138
	private void Start()
	{
		this.mStarted = true;
		if (Localization.instance != null)
		{
			this.OnLocalize(Localization.instance);
		}
	}

	// Token: 0x0400294B RID: 10571
	public string key;

	// Token: 0x0400294C RID: 10572
	private string mLanguage;

	// Token: 0x0400294D RID: 10573
	private bool mStarted;
}
