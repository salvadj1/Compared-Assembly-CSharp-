using System;
using System.Text;
using UnityEngine;

// Token: 0x020004AA RID: 1194
public class TextEntryRestrictions : MonoBehaviour
{
	// Token: 0x060028D9 RID: 10457 RVA: 0x0009579C File Offset: 0x0009399C
	public void OnKeyDown(global::dfControl control, global::dfKeyEventArgs keyEvent)
	{
		if (char.IsControl(keyEvent.Character))
		{
			return;
		}
		if (this.allowedChars.IndexOf(keyEvent.Character) == -1)
		{
			keyEvent.Use();
		}
	}

	// Token: 0x060028DA RID: 10458 RVA: 0x000957D8 File Offset: 0x000939D8
	public void OnKeyPress(global::dfControl control, global::dfKeyEventArgs keyEvent)
	{
		if (char.IsControl(keyEvent.Character))
		{
			return;
		}
		if (this.allowedChars.IndexOf(keyEvent.Character) == -1)
		{
			keyEvent.Use();
		}
	}

	// Token: 0x060028DB RID: 10459 RVA: 0x00095814 File Offset: 0x00093A14
	public void OnTextChanged(global::dfTextbox control, string value)
	{
		int num = control.CursorIndex;
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < value.Length; i++)
		{
			if (this.allowedChars.IndexOf(value[i]) != -1)
			{
				stringBuilder.Append(value[i]);
			}
			else
			{
				num = Mathf.Max(0, num + 1);
			}
		}
		control.Text = stringBuilder.ToString();
		control.CursorIndex = num;
	}

	// Token: 0x0400139D RID: 5021
	public string allowedChars = "0123456789";
}
