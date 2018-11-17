using System;
using UnityEngine;

// Token: 0x020003FF RID: 1023
public class ConvarBinding : MonoBehaviour
{
	// Token: 0x06002596 RID: 9622 RVA: 0x000906D8 File Offset: 0x0008E8D8
	private void Start()
	{
		this.UpdateFromConVar();
	}

	// Token: 0x06002597 RID: 9623 RVA: 0x000906E0 File Offset: 0x0008E8E0
	public void UpdateFromConVar()
	{
		dfSlider component = base.GetComponent<dfSlider>();
		if (component != null)
		{
			component.Value = ConVar.GetFloat(this.convarName, component.Value);
		}
		dfDropdown component2 = base.GetComponent<dfDropdown>();
		if (component2 != null)
		{
			if (this.useValuesNotNumbers)
			{
				string @string = ConVar.GetString(this.convarName, string.Empty);
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
				int @int = ConVar.GetInt(this.convarName, -1f);
				if (@int != -1)
				{
					component2.SelectedIndex = @int;
				}
			}
		}
		dfCheckbox component3 = base.GetComponent<dfCheckbox>();
		if (component3 != null)
		{
			component3.IsChecked = ConVar.GetBool(this.convarName, component3.IsChecked);
		}
	}

	// Token: 0x06002598 RID: 9624 RVA: 0x000907CC File Offset: 0x0008E9CC
	public void UpdateConVars()
	{
		string str;
		if (!this.GetStringValueFromControl(out str))
		{
			return;
		}
		ConsoleSystem.Run(this.convarName + " \"" + str + "\"", false);
	}

	// Token: 0x06002599 RID: 9625 RVA: 0x00090804 File Offset: 0x0008EA04
	public bool GetStringValueFromControl(out string value)
	{
		dfSlider component = base.GetComponent<dfSlider>();
		if (component != null)
		{
			value = component.Value.ToString();
			return true;
		}
		dfDropdown component2 = base.GetComponent<dfDropdown>();
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
			dfCheckbox component3 = base.GetComponent<dfCheckbox>();
			if (component3)
			{
				value = ((!component3.IsChecked) ? bool.FalseString : bool.TrueString);
				return true;
			}
			value = string.Empty;
			return false;
		}
	}

	// Token: 0x04001238 RID: 4664
	public string convarName;

	// Token: 0x04001239 RID: 4665
	public bool useValuesNotNumbers;
}
