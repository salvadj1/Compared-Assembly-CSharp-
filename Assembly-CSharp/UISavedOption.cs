using System;
using UnityEngine;

// Token: 0x0200077A RID: 1914
[AddComponentMenu("NGUI/Interaction/Saved Option")]
public class UISavedOption : MonoBehaviour
{
	// Token: 0x17000D7D RID: 3453
	// (get) Token: 0x0600456C RID: 17772 RVA: 0x0011213C File Offset: 0x0011033C
	private string key
	{
		get
		{
			return (!string.IsNullOrEmpty(this.keyName)) ? this.keyName : ("NGUI State: " + base.name);
		}
	}

	// Token: 0x0600456D RID: 17773 RVA: 0x0011216C File Offset: 0x0011036C
	private void OnEnable()
	{
		string @string = PlayerPrefs.GetString(this.key);
		if (!string.IsNullOrEmpty(@string))
		{
			UICheckbox component = base.GetComponent<UICheckbox>();
			if (component != null)
			{
				component.isChecked = (@string == "true");
			}
			else
			{
				UICheckbox[] componentsInChildren = base.GetComponentsInChildren<UICheckbox>();
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					UICheckbox uicheckbox = componentsInChildren[i];
					UIEventListener uieventListener = UIEventListener.Get(uicheckbox.gameObject);
					uieventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Remove(uieventListener.onClick, new UIEventListener.VoidDelegate(this.Save));
					uicheckbox.isChecked = (uicheckbox.name == @string);
					Debug.Log(@string);
					UIEventListener uieventListener2 = UIEventListener.Get(uicheckbox.gameObject);
					uieventListener2.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uieventListener2.onClick, new UIEventListener.VoidDelegate(this.Save));
					i++;
				}
			}
		}
	}

	// Token: 0x0600456E RID: 17774 RVA: 0x00112254 File Offset: 0x00110454
	private void OnDisable()
	{
		this.Save(null);
	}

	// Token: 0x0600456F RID: 17775 RVA: 0x00112260 File Offset: 0x00110460
	private void Save(GameObject go)
	{
		UICheckbox component = base.GetComponent<UICheckbox>();
		if (component != null)
		{
			PlayerPrefs.SetString(this.key, (!component.isChecked) ? "false" : "true");
		}
		else
		{
			UICheckbox[] componentsInChildren = base.GetComponentsInChildren<UICheckbox>();
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				UICheckbox uicheckbox = componentsInChildren[i];
				if (uicheckbox.isChecked)
				{
					PlayerPrefs.SetString(this.key, uicheckbox.name);
					break;
				}
				i++;
			}
		}
	}

	// Token: 0x040025D7 RID: 9687
	public string keyName;
}
