using System;
using UnityEngine;

// Token: 0x02000776 RID: 1910
[RequireComponent(typeof(UIInput))]
[AddComponentMenu("NGUI/Interaction/Input Validator")]
public class UIInputValidator : MonoBehaviour
{
	// Token: 0x06004556 RID: 17750 RVA: 0x001111A8 File Offset: 0x0010F3A8
	private void Start()
	{
		base.GetComponent<UIInput>().validator = new UIInput.Validator(this.Validate);
	}

	// Token: 0x06004557 RID: 17751 RVA: 0x001111C4 File Offset: 0x0010F3C4
	private char Validate(string text, char ch)
	{
		if (this.logic == UIInputValidator.Validation.None || !base.enabled)
		{
			return ch;
		}
		if (this.logic == UIInputValidator.Validation.Integer || this.logic == UIInputValidator.Validation.IntegerPositive)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (this.logic != UIInputValidator.Validation.IntegerPositive && ch == '-' && text.Length == 0)
			{
				return ch;
			}
		}
		else if (this.logic == UIInputValidator.Validation.Float)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && text.Length == 0)
			{
				return ch;
			}
			if (ch == '.' && !text.Contains("."))
			{
				return ch;
			}
		}
		else if (this.logic == UIInputValidator.Validation.Alphanumeric)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch;
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.logic == UIInputValidator.Validation.Username)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch - 'A' + 'a';
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.logic == UIInputValidator.Validation.Name)
		{
			char c = (text.Length <= 0) ? ' ' : text[text.Length - 1];
			if (ch >= 'a' && ch <= 'z')
			{
				if (c == ' ')
				{
					return ch - 'a' + 'A';
				}
				return ch;
			}
			else if (ch >= 'A' && ch <= 'Z')
			{
				if (c != ' ' && c != '\'')
				{
					return ch - 'A' + 'a';
				}
				return ch;
			}
			else if (ch == '\'')
			{
				if (c != ' ' && c != '\'' && !text.Contains("'"))
				{
					return ch;
				}
			}
			else if (ch == ' ' && c != ' ' && c != '\'')
			{
				return ch;
			}
		}
		return '\0';
	}

	// Token: 0x040025B1 RID: 9649
	public UIInputValidator.Validation logic;

	// Token: 0x02000777 RID: 1911
	public enum Validation
	{
		// Token: 0x040025B3 RID: 9651
		None,
		// Token: 0x040025B4 RID: 9652
		Integer,
		// Token: 0x040025B5 RID: 9653
		Float,
		// Token: 0x040025B6 RID: 9654
		Alphanumeric,
		// Token: 0x040025B7 RID: 9655
		Username,
		// Token: 0x040025B8 RID: 9656
		Name,
		// Token: 0x040025B9 RID: 9657
		IntegerPositive
	}
}
