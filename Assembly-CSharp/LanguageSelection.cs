using System;
using UnityEngine;

// Token: 0x0200083C RID: 2108
[AddComponentMenu("NGUI/Interaction/Language Selection")]
[RequireComponent(typeof(global::UIPopupList))]
public class LanguageSelection : MonoBehaviour
{
	// Token: 0x0600490E RID: 18702 RVA: 0x00116964 File Offset: 0x00114B64
	private void Start()
	{
		this.mList = base.GetComponent<global::UIPopupList>();
		this.UpdateList();
		this.mList.eventReceiver = base.gameObject;
		this.mList.functionName = "OnLanguageSelection";
	}

	// Token: 0x0600490F RID: 18703 RVA: 0x0011699C File Offset: 0x00114B9C
	private void UpdateList()
	{
		if (global::Localization.instance != null && global::Localization.instance.languages != null)
		{
			this.mList.items.Clear();
			int i = 0;
			int num = global::Localization.instance.languages.Length;
			while (i < num)
			{
				TextAsset textAsset = global::Localization.instance.languages[i];
				if (textAsset != null)
				{
					this.mList.items.Add(textAsset.name);
				}
				i++;
			}
			this.mList.selection = global::Localization.instance.currentLanguage;
		}
	}

	// Token: 0x06004910 RID: 18704 RVA: 0x00116A3C File Offset: 0x00114C3C
	private void OnLanguageSelection(string language)
	{
		if (global::Localization.instance != null)
		{
			global::Localization.instance.currentLanguage = language;
		}
	}

	// Token: 0x0400271D RID: 10013
	private global::UIPopupList mList;
}
