using System;
using UnityEngine;

// Token: 0x0200085D RID: 2141
[AddComponentMenu("NGUI/Interaction/Saved Option")]
public class UISavedOption : MonoBehaviour
{
	// Token: 0x17000E0D RID: 3597
	// (get) Token: 0x060049D1 RID: 18897 RVA: 0x0011BABC File Offset: 0x00119CBC
	private string key
	{
		get
		{
			return (!string.IsNullOrEmpty(this.keyName)) ? this.keyName : ("NGUI State: " + base.name);
		}
	}

	// Token: 0x060049D2 RID: 18898 RVA: 0x0011BAEC File Offset: 0x00119CEC
	private void OnEnable()
	{
		string @string = PlayerPrefs.GetString(this.key);
		if (!string.IsNullOrEmpty(@string))
		{
			global::UICheckbox component = base.GetComponent<global::UICheckbox>();
			if (component != null)
			{
				component.isChecked = (@string == "true");
			}
			else
			{
				global::UICheckbox[] componentsInChildren = base.GetComponentsInChildren<global::UICheckbox>();
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					global::UICheckbox uicheckbox = componentsInChildren[i];
					global::UIEventListener uieventListener = global::UIEventListener.Get(uicheckbox.gameObject);
					uieventListener.onClick = (global::UIEventListener.VoidDelegate)Delegate.Remove(uieventListener.onClick, new global::UIEventListener.VoidDelegate(this.Save));
					uicheckbox.isChecked = (uicheckbox.name == @string);
					Debug.Log(@string);
					global::UIEventListener uieventListener2 = global::UIEventListener.Get(uicheckbox.gameObject);
					uieventListener2.onClick = (global::UIEventListener.VoidDelegate)Delegate.Combine(uieventListener2.onClick, new global::UIEventListener.VoidDelegate(this.Save));
					i++;
				}
			}
		}
	}

	// Token: 0x060049D3 RID: 18899 RVA: 0x0011BBD4 File Offset: 0x00119DD4
	private void OnDisable()
	{
		this.Save(null);
	}

	// Token: 0x060049D4 RID: 18900 RVA: 0x0011BBE0 File Offset: 0x00119DE0
	private void Save(GameObject go)
	{
		global::UICheckbox component = base.GetComponent<global::UICheckbox>();
		if (component != null)
		{
			PlayerPrefs.SetString(this.key, (!component.isChecked) ? "false" : "true");
		}
		else
		{
			global::UICheckbox[] componentsInChildren = base.GetComponentsInChildren<global::UICheckbox>();
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				global::UICheckbox uicheckbox = componentsInChildren[i];
				if (uicheckbox.isChecked)
				{
					PlayerPrefs.SetString(this.key, uicheckbox.name);
					break;
				}
				i++;
			}
		}
	}

	// Token: 0x0400280E RID: 10254
	public string keyName;
}
