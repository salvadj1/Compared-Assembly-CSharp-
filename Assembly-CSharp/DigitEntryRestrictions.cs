using System;
using System.Text;
using UnityEngine;

// Token: 0x020003F1 RID: 1009
public class DigitEntryRestrictions : MonoBehaviour
{
	// Token: 0x0600253D RID: 9533 RVA: 0x0008F198 File Offset: 0x0008D398
	public void OnKeyDown(dfControl control, dfKeyEventArgs keyEvent)
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

	// Token: 0x0600253E RID: 9534 RVA: 0x0008F1C4 File Offset: 0x0008D3C4
	public void OnKeyPress(dfControl control, dfKeyEventArgs keyEvent)
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

	// Token: 0x0600253F RID: 9535 RVA: 0x0008F1F0 File Offset: 0x0008D3F0
	public void OnTextChanged(dfTextbox control, string value)
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
