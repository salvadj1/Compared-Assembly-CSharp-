using System;
using UnityEngine;

// Token: 0x020004B2 RID: 1202
public class OptionsKeyBinding : MonoBehaviour
{
	// Token: 0x06002916 RID: 10518 RVA: 0x0009676C File Offset: 0x0009496C
	public void Setup(global::GameInput.GameButton button)
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

	// Token: 0x06002917 RID: 10519 RVA: 0x00096814 File Offset: 0x00094A14
	public void OnClickOne()
	{
		this.StartKeyListen(this.keyOne);
	}

	// Token: 0x06002918 RID: 10520 RVA: 0x00096824 File Offset: 0x00094A24
	public void OnClickTwo()
	{
		this.StartKeyListen(this.keyTwo);
	}

	// Token: 0x06002919 RID: 10521 RVA: 0x00096834 File Offset: 0x00094A34
	private void StartKeyListen(global::dfRichTextLabel key)
	{
		if (global::OptionsKeyBinding.doingKeyListen != null)
		{
			return;
		}
		global::OptionsKeyBinding.strPreviousValue = key.Text;
		key.Text = "...";
		global::OptionsKeyBinding.doingKeyListen = key;
	}

	// Token: 0x0600291A RID: 10522 RVA: 0x00096864 File Offset: 0x00094A64
	private void Update()
	{
		if (global::OptionsKeyBinding.doingKeyListen != this.keyOne && global::OptionsKeyBinding.doingKeyListen != this.keyTwo)
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
			global::OptionsKeyBinding.doingKeyListen.Text = " ";
		}
		else
		{
			global::OptionsKeyBinding.doingKeyListen.Text = keyCode.ToString();
		}
		global::OptionsKeyBinding.doingKeyListen = null;
	}

	// Token: 0x0600291B RID: 10523 RVA: 0x000968EC File Offset: 0x00094AEC
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

	// Token: 0x0600291C RID: 10524 RVA: 0x00096920 File Offset: 0x00094B20
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
		global::ConsoleSystem.Run(string.Concat(new string[]
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

	// Token: 0x040013B8 RID: 5048
	public global::dfRichTextLabel keyOne;

	// Token: 0x040013B9 RID: 5049
	public global::dfRichTextLabel keyTwo;

	// Token: 0x040013BA RID: 5050
	public global::dfRichTextLabel labelName;

	// Token: 0x040013BB RID: 5051
	protected static global::dfRichTextLabel doingKeyListen;

	// Token: 0x040013BC RID: 5052
	protected static string strPreviousValue = string.Empty;
}
