using System;
using System.Text;
using UnityEngine;

// Token: 0x020004A1 RID: 1185
public class DigitEntryRestrictions : MonoBehaviour
{
	// Token: 0x060028AF RID: 10415 RVA: 0x00094B84 File Offset: 0x00092D84
	public void OnKeyDown(global::dfControl control, global::dfKeyEventArgs keyEvent)
	{
		if (char.IsControl(keyEvent.Character))
		{
			return;
		}
		if (!char.IsDigit(keyEvent.Character))
		{
			keyEvent.Use();
		}
	}

	// Token: 0x060028B0 RID: 10416 RVA: 0x00094BB0 File Offset: 0x00092DB0
	public void OnKeyPress(global::dfControl control, global::dfKeyEventArgs keyEvent)
	{
		if (char.IsControl(keyEvent.Character))
		{
			return;
		}
		if (!char.IsDigit(keyEvent.Character))
		{
			keyEvent.Use();
		}
	}

	// Token: 0x060028B1 RID: 10417 RVA: 0x00094BDC File Offset: 0x00092DDC
	public void OnTextChanged(global::dfTextbox control, string value)
	{
		int cursorIndex = control.CursorIndex;
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < value.Length; i++)
		{
			if (char.IsDigit(value[i]))
			{
				stringBuilder.Append(value[i]);
			}
		}
		control.Text = stringBuilder.ToString();
	}
}
