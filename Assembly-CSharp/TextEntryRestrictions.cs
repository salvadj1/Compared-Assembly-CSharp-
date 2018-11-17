using System;
using System.Text;
using UnityEngine;

// Token: 0x020003F9 RID: 1017
public class TextEntryRestrictions : MonoBehaviour
{
	// Token: 0x06002561 RID: 9569 RVA: 0x0008F964 File Offset: 0x0008DB64
	public void OnKeyDown(dfControl control, dfKeyEventArgs keyEvent)
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

	// Token: 0x06002562 RID: 9570 RVA: 0x0008F9A0 File Offset: 0x0008DBA0
	public void OnKeyPress(dfControl control, dfKeyEventArgs keyEvent)
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

	// Token: 0x06002563 RID: 9571 RVA: 0x0008F9DC File Offset: 0x0008DBDC
	public void OnTextChanged(dfTextbox control, string value)
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

	// Token: 0x04001220 RID: 4640
	public string allowedChars = "0123456789";
}
