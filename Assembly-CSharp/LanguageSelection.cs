using System;
using UnityEngine;

// Token: 0x0200075A RID: 1882
[RequireComponent(typeof(UIPopupList))]
[AddComponentMenu("NGUI/Interaction/Language Selection")]
public class LanguageSelection : MonoBehaviour
{
	// Token: 0x060044AD RID: 17581 RVA: 0x0010CFE4 File Offset: 0x0010B1E4
	private void Start()
	{
		this.mList = base.GetComponent<UIPopupList>();
		this.UpdateList();
		this.mList.eventReceiver = base.gameObject;
		this.mList.functionName = "OnLanguageSelection";
	}

	// Token: 0x060044AE RID: 17582 RVA: 0x0010D01C File Offset: 0x0010B21C
	private void UpdateList()
	{
		if (Localization.instance != null && Localization.instance.languages != null)
		{
			this.mList.items.Clear();
			int i = 0;
			int num = Localization.instance.languages.Length;
			while (i < num)
			{
				TextAsset textAsset = Localization.instance.languages[i];
				if (textAsset != null)
				{
					this.mList.items.Add(textAsset.name);
				}
				i++;
			}
			this.mList.selection = Localization.instance.currentLanguage;
		}
	}

	// Token: 0x060044AF RID: 17583 RVA: 0x0010D0BC File Offset: 0x0010B2BC
	private void OnLanguageSelection(string language)
	{
		if (Localization.instance != null)
		{
			Localization.instance.currentLanguage = language;
		}
	}

	// Token: 0x040024E6 RID: 9446
	private UIPopupList mList;
}
