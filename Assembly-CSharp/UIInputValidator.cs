using System;
using UnityEngine;

// Token: 0x02000859 RID: 2137
[RequireComponent(typeof(global::UIInput))]
[AddComponentMenu("NGUI/Interaction/Input Validator")]
public class UIInputValidator : MonoBehaviour
{
	// Token: 0x060049BB RID: 18875 RVA: 0x0011AB28 File Offset: 0x00118D28
	private void Start()
	{
		base.GetComponent<global::UIInput>().validator = new global::UIInput.Validator(this.Validate);
	}

	// Token: 0x060049BC RID: 18876 RVA: 0x0011AB44 File Offset: 0x00118D44
	private char Validate(string text, char ch)
	{
		if (this.logic == global::UIInputValidator.Validation.None || !base.enabled)
		{
			return ch;
		}
		if (this.logic == global::UIInputValidator.Validation.Integer || this.logic == global::UIInputValidator.Validation.IntegerPositive)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (this.logic != global::UIInputValidator.Validation.IntegerPositive && ch == '-' && text.Length == 0)
			{
				return ch;
			}
		}
		else if (this.logic == global::UIInputValidator.Validation.Float)
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
		else if (this.logic == global::UIInputValidator.Validation.Alphanumeric)
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
		else if (this.logic == global::UIInputValidator.Validation.Username)
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
		else if (this.logic == global::UIInputValidator.Validation.Name)
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

	// Token: 0x040027E8 RID: 10216
	public global::UIInputValidator.Validation logic;

	// Token: 0x0200085A RID: 2138
	public enum Validation
	{
		// Token: 0x040027EA RID: 10218
		None,
		// Token: 0x040027EB RID: 10219
		Integer,
		// Token: 0x040027EC RID: 10220
		Float,
		// Token: 0x040027ED RID: 10221
		Alphanumeric,
		// Token: 0x040027EE RID: 10222
		Username,
		// Token: 0x040027EF RID: 10223
		Name,
		// Token: 0x040027F0 RID: 10224
		IntegerPositive
	}
}
