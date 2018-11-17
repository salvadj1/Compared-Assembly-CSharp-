using System;
using UnityEngine;

// Token: 0x020004B0 RID: 1200
public class ConvarBinding : MonoBehaviour
{
	// Token: 0x0600290E RID: 10510 RVA: 0x00096510 File Offset: 0x00094710
	private void Start()
	{
		this.UpdateFromConVar();
	}

	// Token: 0x0600290F RID: 10511 RVA: 0x00096518 File Offset: 0x00094718
	public void UpdateFromConVar()
	{
		global::dfSlider component = base.GetComponent<global::dfSlider>();
		if (component != null)
		{
			component.Value = global::ConVar.GetFloat(this.convarName, component.Value);
		}
		global::dfDropdown component2 = base.GetComponent<global::dfDropdown>();
		if (component2 != null)
		{
			if (this.useValuesNotNumbers)
			{
				string @string = global::ConVar.GetString(this.convarName, string.Empty);
				if (!string.IsNullOrEmpty(@string))
				{
					int selectedIndex = component2.SelectedIndex;
					component2.SelectedValue = @string;
					if (component2.SelectedIndex == -1)
					{
						component2.SelectedIndex = selectedIndex;
					}
				}
			}
			else
			{
				int @int = global::ConVar.GetInt(this.convarName, -1f);
				if (@int != -1)
				{
					component2.SelectedIndex = @int;
				}
			}
		}
		global::dfCheckbox component3 = base.GetComponent<global::dfCheckbox>();
		if (component3 != null)
		{
			component3.IsChecked = global::ConVar.GetBool(this.convarName, component3.IsChecked);
		}
	}

	// Token: 0x06002910 RID: 10512 RVA: 0x00096604 File Offset: 0x00094804
	public void UpdateConVars()
	{
		string str;
		if (!this.GetStringValueFromControl(out str))
		{
			return;
		}
		global::ConsoleSystem.Run(this.convarName + " \"" + str + "\"", false);
	}

	// Token: 0x06002911 RID: 10513 RVA: 0x0009663C File Offset: 0x0009483C
	public bool GetStringValueFromControl(out string value)
	{
		global::dfSlider component = base.GetComponent<global::dfSlider>();
		if (component != null)
		{
			value = component.Value.ToString();
			return true;
		}
		global::dfDropdown component2 = base.GetComponent<global::dfDropdown>();
		if (component2)
		{
			int selectedIndex = component2.SelectedIndex;
			if (selectedIndex == -1)
			{
				value = string.Empty;
				return false;
			}
			if (this.useValuesNotNumbers)
			{
				value = component2.SelectedValue;
			}
			else
			{
				value = selectedIndex.ToString();
			}
			return true;
		}
		else
		{
			global::dfCheckbox component3 = base.GetComponent<global::dfCheckbox>();
			if (component3)
			{
				value = ((!component3.IsChecked) ? bool.FalseString : bool.TrueString);
				return true;
			}
			value = string.Empty;
			return false;
		}
	}

	// Token: 0x040013B5 RID: 5045
	public string convarName;

	// Token: 0x040013B6 RID: 5046
	public bool useValuesNotNumbers;
}
