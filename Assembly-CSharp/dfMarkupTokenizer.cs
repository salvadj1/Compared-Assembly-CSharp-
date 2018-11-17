using System;
using System.Collections.Generic;

// Token: 0x020006E4 RID: 1764
public class dfMarkupTokenizer
{
	// Token: 0x06003F08 RID: 16136 RVA: 0x000EF214 File Offset: 0x000ED414
	public static List<dfMarkupToken> Tokenize(string source)
	{
		if (dfMarkupTokenizer.singleton == null)
		{
			dfMarkupTokenizer.singleton = new dfMarkupTokenizer();
		}
		return dfMarkupTokenizer.singleton.tokenize(source);
	}

	// Token: 0x06003F09 RID: 16137 RVA: 0x000EF238 File Offset: 0x000ED438
	private List<dfMarkupToken> tokenize(string source)
	{
		this.reset();
		this.source = source;
		this.index = 0;
		while (this.index < source.Length)
		{
			char c = this.Peek(0);
			if (this.AtTagPosition())
			{
				dfMarkupToken dfMarkupToken = this.parseTag();
				if (dfMarkupToken != null)
				{
					this.tokens.Add(dfMarkupToken);
				}
			}
			else
			{
				dfMarkupToken dfMarkupToken2 = null;
				if (char.IsWhiteSpace(c))
				{
					if (c != '\r')
					{
						dfMarkupToken2 = this.parseWhitespace();
					}
				}
				else
				{
					dfMarkupToken2 = this.parseNonWhitespace();
				}
				if (dfMarkupToken2 == null)
				{
					this.Advance(1);
				}
				else
				{
					this.tokens.Add(dfMarkupToken2);
				}
			}
		}
		return this.tokens;
	}

	// Token: 0x06003F0A RID: 16138 RVA: 0x000EF2F0 File Offset: 0x000ED4F0
	private void reset()
	{
		dfMarkupToken.Reset();
		dfMarkupTokenAttribute.Reset();
		this.tokens.Clear();
	}

	// Token: 0x06003F0B RID: 16139 RVA: 0x000EF308 File Offset: 0x000ED508
	private bool AtTagPosition()
	{
		if (this.Peek(0) != '[')
		{
			return false;
		}
		char c = this.Peek(1);
		if (c == '/')
		{
			return char.IsLetter(this.Peek(2)) && this.isValidTag(this.index + 2, true);
		}
		return char.IsLetter(c) && this.isValidTag(this.index + 1, false);
	}

	// Token: 0x06003F0C RID: 16140 RVA: 0x000EF374 File Offset: 0x000ED574
	private bool isValidTag(int index, bool endTag)
	{
		for (int i = 0; i < dfMarkupTokenizer.validTags.Count; i++)
		{
			string text = dfMarkupTokenizer.validTags[i];
			bool flag = true;
			int num = 0;
			while (num < text.Length - 1 && num + index < this.source.Length - 1)
			{
				if (!endTag && this.source[num + index] == ' ')
				{
					break;
				}
				if (this.source[num + index] == ']')
				{
					break;
				}
				if (char.ToLowerInvariant(text[num]) != char.ToLowerInvariant(this.source[num + index]))
				{
					flag = false;
					break;
				}
				num++;
			}
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003F0D RID: 16141 RVA: 0x000EF448 File Offset: 0x000ED648
	private dfMarkupToken parseQuotedString()
	{
		char c = this.Peek(0);
		if (c != '"' && c != '\'')
		{
			return null;
		}
		this.Advance(1);
		int startIndex = this.index;
		int num = this.index;
		while (this.index < this.source.Length && this.Advance(1) != c)
		{
			num++;
		}
		if (this.Peek(0) == c)
		{
			this.Advance(1);
		}
		return dfMarkupToken.Obtain(this.source, dfMarkupTokenType.Text, startIndex, num);
	}

	// Token: 0x06003F0E RID: 16142 RVA: 0x000EF4D8 File Offset: 0x000ED6D8
	private dfMarkupToken parseNonWhitespace()
	{
		int startIndex = this.index;
		int num = this.index;
		while (this.index < this.source.Length)
		{
			char c = this.Advance(1);
			if (char.IsWhiteSpace(c) || this.AtTagPosition())
			{
				break;
			}
			num++;
		}
		return dfMarkupToken.Obtain(this.source, dfMarkupTokenType.Text, startIndex, num);
	}

	// Token: 0x06003F0F RID: 16143 RVA: 0x000EF548 File Offset: 0x000ED748
	private dfMarkupToken parseWhitespace()
	{
		int num = this.index;
		int num2 = this.index;
		if (this.Peek(0) == '\n')
		{
			this.Advance(1);
			return dfMarkupToken.Obtain(this.source, dfMarkupTokenType.Newline, num, num);
		}
		while (this.index < this.source.Length)
		{
			char c = this.Advance(1);
			if (c == '\n' || c == '\r' || !char.IsWhiteSpace(c))
			{
				break;
			}
			num2++;
		}
		return dfMarkupToken.Obtain(this.source, dfMarkupTokenType.Whitespace, num, num2);
	}

	// Token: 0x06003F10 RID: 16144 RVA: 0x000EF5E0 File Offset: 0x000ED7E0
	private dfMarkupToken parseWord()
	{
		if (!char.IsLetter(this.Peek(0)))
		{
			return null;
		}
		int startIndex = this.index;
		int num = this.index;
		while (this.index < this.source.Length && char.IsLetter(this.Advance(1)))
		{
			num++;
		}
		return dfMarkupToken.Obtain(this.source, dfMarkupTokenType.Text, startIndex, num);
	}

	// Token: 0x06003F11 RID: 16145 RVA: 0x000EF650 File Offset: 0x000ED850
	private dfMarkupToken parseTag()
	{
		if (this.Peek(0) != '[')
		{
			return null;
		}
		char c = this.Peek(1);
		if (c == '/')
		{
			return this.parseEndTag();
		}
		this.Advance(1);
		c = this.Peek(0);
		if (!char.IsLetterOrDigit(c))
		{
			return null;
		}
		int startIndex = this.index;
		int num = this.index;
		while (this.index < this.source.Length && char.IsLetterOrDigit(this.Advance(1)))
		{
			num++;
		}
		dfMarkupToken dfMarkupToken = dfMarkupToken.Obtain(this.source, dfMarkupTokenType.StartTag, startIndex, num);
		if (this.index < this.source.Length && this.Peek(0) != ']')
		{
			c = this.Peek(0);
			if (char.IsWhiteSpace(c))
			{
				this.parseWhitespace();
			}
			int startIndex2 = this.index;
			int num2 = this.index;
			if (this.Peek(0) == '"')
			{
				dfMarkupToken dfMarkupToken2 = this.parseQuotedString();
				dfMarkupToken.AddAttribute(dfMarkupToken2, dfMarkupToken2);
			}
			else
			{
				while (this.index < this.source.Length && this.Advance(1) != ']')
				{
					num2++;
				}
				dfMarkupToken dfMarkupToken3 = dfMarkupToken.Obtain(this.source, dfMarkupTokenType.Text, startIndex2, num2);
				dfMarkupToken.AddAttribute(dfMarkupToken3, dfMarkupToken3);
			}
		}
		if (this.Peek(0) == ']')
		{
			this.Advance(1);
		}
		return dfMarkupToken;
	}

	// Token: 0x06003F12 RID: 16146 RVA: 0x000EF7C4 File Offset: 0x000ED9C4
	private dfMarkupToken parseAttributeValue()
	{
		int startIndex = this.index;
		int num = this.index;
		while (this.index < this.source.Length)
		{
			char c = this.Advance(1);
			if (c == ']' || char.IsWhiteSpace(c))
			{
				break;
			}
			num++;
		}
		return dfMarkupToken.Obtain(this.source, dfMarkupTokenType.Text, startIndex, num);
	}

	// Token: 0x06003F13 RID: 16147 RVA: 0x000EF830 File Offset: 0x000EDA30
	private dfMarkupToken parseEndTag()
	{
		this.Advance(2);
		int startIndex = this.index;
		int num = this.index;
		while (this.index < this.source.Length && char.IsLetterOrDigit(this.Advance(1)))
		{
			num++;
		}
		if (this.Peek(0) == ']')
		{
			this.Advance(1);
		}
		return dfMarkupToken.Obtain(this.source, dfMarkupTokenType.EndTag, startIndex, num);
	}

	// Token: 0x06003F14 RID: 16148 RVA: 0x000EF8AC File Offset: 0x000EDAAC
	private char Peek(int offset = 0)
	{
		if (this.index + offset > this.source.Length - 1)
		{
			return '\0';
		}
		return this.source[this.index + offset];
	}

	// Token: 0x06003F15 RID: 16149 RVA: 0x000EF8E0 File Offset: 0x000EDAE0
	private char Advance(int amount = 1)
	{
		this.index += amount;
		return this.Peek(0);
	}

	// Token: 0x040021B6 RID: 8630
	private static dfMarkupTokenizer singleton;

	// Token: 0x040021B7 RID: 8631
	private static List<string> validTags = new List<string>
	{
		"color",
		"sprite"
	};

	// Token: 0x040021B8 RID: 8632
	private List<dfMarkupToken> tokens = new List<dfMarkupToken>();

	// Token: 0x040021B9 RID: 8633
	private string source;

	// Token: 0x040021BA RID: 8634
	private int index;
}
