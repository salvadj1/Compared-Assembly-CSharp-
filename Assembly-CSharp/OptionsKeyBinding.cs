using System;
using UnityEngine;

// Token: 0x02000401 RID: 1025
public class OptionsKeyBinding : MonoBehaviour
{
	// Token: 0x0600259E RID: 9630 RVA: 0x00090934 File Offset: 0x0008EB34
	public void Setup(GameInput.GameButton button)
	{
		this.labelName.Text = button.Name;
		this.keyOne.Text = button.bindingOne.ToString();
		this.keyTwo.Text = button.bindingTwo.ToString();
		if (this.keyOne.Text == "None")
		{
			this.keyOne.Text = " ";
		}
		if (this.keyTwo.Text == "None")
		{
			this.keyTwo.Text = " ";
		}
	}

	// Token: 0x0600259F RID: 9631 RVA: 0x000909DC File Offset: 0x0008EBDC
	public void OnClickOne()
	{
		this.StartKeyListen(this.keyOne);
	}

	// Token: 0x060025A0 RID: 9632 RVA: 0x000909EC File Offset: 0x0008EBEC
	public void OnClickTwo()
	{
		this.StartKeyListen(this.keyTwo);
	}

	// Token: 0x060025A1 RID: 9633 RVA: 0x000909FC File Offset: 0x0008EBFC
	private void StartKeyListen(dfRichTextLabel key)
	{
		if (OptionsKeyBinding.doingKeyListen != null)
		{
			return;
		}
		OptionsKeyBinding.strPreviousValue = key.Text;
		key.Text = "...";
		OptionsKeyBinding.doingKeyListen = key;
	}

	// Token: 0x060025A2 RID: 9634 RVA: 0x00090A2C File Offset: 0x0008EC2C
	private void Update()
	{
		if (OptionsKeyBinding.doingKeyListen != this.keyOne && OptionsKeyBinding.doingKeyListen != this.keyTwo)
		{
			return;
		}
		if (!Input.anyKeyDown)
		{
			return;
		}
		KeyCode keyCode = this.FetchKey();
		if (keyCode == null)
		{
			return;
		}
		if (keyCode == 27)
		{
			OptionsKeyBinding.doingKeyListen.Text = " ";
		}
		else
		{
			OptionsKeyBinding.doingKeyListen.Text = keyCode.ToString();
		}
		OptionsKeyBinding.doingKeyListen = null;
	}

	// Token: 0x060025A3 RID: 9635 RVA: 0x00090AB4 File Offset: 0x0008ECB4
	private KeyCode FetchKey()
	{
		for (int i = 0; i < 429; i++)
		{
			if (Input.GetKey(i))
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x060025A4 RID: 9636 RVA: 0x00090AE8 File Offset: 0x0008ECE8
	public void UpdateConVars()
	{
		string text = "None";
		string text2 = "None";
		if (this.keyOne.Text.Length > 0 && this.keyOne.Text != " ")
		{
			text = this.keyOne.Text;
		}
		if (this.keyTwo.Text.Length > 0 && this.keyTwo.Text != " ")
		{
			text2 = this.keyTwo.Text;
		}
		ConsoleSystem.Run(string.Concat(new string[]
		{
			"input.bind ",
			this.labelName.Text,
			" ",
			text,
			" ",
			text2,
			string.Empty
		}), false);
	}

	// Token: 0x0400123B RID: 4667
	public dfRichTextLabel keyOne;

	// Token: 0x0400123C RID: 4668
	public dfRichTextLabel keyTwo;

	// Token: 0x0400123D RID: 4669
	public dfRichTextLabel labelName;

	// Token: 0x0400123E RID: 4670
	protected static dfRichTextLabel doingKeyListen;

	// Token: 0x0400123F RID: 4671
	protected static string strPreviousValue = string.Empty;
}
