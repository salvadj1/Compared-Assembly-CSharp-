using System;
using System.Collections.Generic;

// Token: 0x020006E3 RID: 1763
public class dfRichTextTokenizer
{
	// Token: 0x06003EFA RID: 16122 RVA: 0x000EEBE0 File Offset: 0x000ECDE0
	public static List<dfMarkupToken> Tokenize(string source)
	{
		if (dfRichTextTokenizer.singleton == null)
		{
			dfRichTextTokenizer.singleton = new dfRichTextTokenizer();
		}
		return dfRichTextTokenizer.singleton.tokenize(source);
	}

	// Token: 0x06003EFB RID: 16123 RVA: 0x000EEC04 File Offset: 0x000ECE04
	private List<dfMarkupToken> tokenize(string source)
	{
		dfMarkupToken.Reset();
		dfMarkupTokenAttribute.Reset();
		this.tokens.Clear();
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

	// Token: 0x06003EFC RID: 16124 RVA: 0x000EECC8 File Offset: 0x000ECEC8
	private bool AtTagPosition()
	{
		if (this.Peek(0) != '<')
		{
			return false;
		}
		char c = this.Peek(1);
		if (c == '/')
		{
			return char.IsLetter(this.Peek(2));
		}
		return char.IsLetter(c);
	}

	// Token: 0x06003EFD RID: 16125 RVA: 0x000EED18 File Offset: 0x000ECF18
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

	// Token: 0x06003EFE RID: 16126 RVA: 0x000EEDA8 File Offset: 0x000ECFA8
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

	// Token: 0x06003EFF RID: 16127 RVA: 0x000EEE18 File Offset: 0x000ED018
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

	// Token: 0x06003F00 RID: 16128 RVA: 0x000EEEB0 File Offset: 0x000ED0B0
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

	// Token: 0x06003F01 RID: 16129 RVA: 0x000EEF20 File Offset: 0x000ED120
	private dfMarkupToken parseTag()
	{
		if (this.Peek(0) != '<')
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
		while (this.index < this.source.Length && this.Peek(0) != '>')
		{
			c = this.Peek(0);
			if (char.IsWhiteSpace(c))
			{
				this.parseWhitespace();
			}
			else
			{
				dfMarkupToken dfMarkupToken2 = this.parseWord();
				if (dfMarkupToken2 == null)
				{
					this.Advance(1);
				}
				else
				{
					c = this.Peek(0);
					if (c != '=')
					{
						dfMarkupToken.AddAttribute(dfMarkupToken2, dfMarkupToken2);
					}
					else
					{
						c = this.Advance(1);
						dfMarkupToken dfMarkupToken3;
						if (c == '"' || c == '\'')
						{
							dfMarkupToken3 = this.parseQuotedString();
						}
						else
						{
							dfMarkupToken3 = this.parseAttributeValue();
						}
						dfMarkupToken.AddAttribute(dfMarkupToken2, dfMarkupToken3 ?? dfMarkupToken2);
					}
				}
			}
		}
		if (this.Peek(0) == '>')
		{
			this.Advance(1);
		}
		return dfMarkupToken;
	}

	// Token: 0x06003F02 RID: 16130 RVA: 0x000EF09C File Offset: 0x000ED29C
	private dfMarkupToken parseAttributeValue()
	{
		int startIndex = this.index;
		int num = this.index;
		while (this.index < this.source.Length)
		{
			char c = this.Advance(1);
			if (c == '>' || char.IsWhiteSpace(c))
			{
				break;
			}
			num++;
		}
		return dfMarkupToken.Obtain(this.source, dfMarkupTokenType.Text, startIndex, num);
	}

	// Token: 0x06003F03 RID: 16131 RVA: 0x000EF108 File Offset: 0x000ED308
	private dfMarkupToken parseEndTag()
	{
		this.Advance(2);
		int startIndex = this.index;
		int num = this.index;
		while (this.index < this.source.Length && char.IsLetterOrDigit(this.Advance(1)))
		{
			num++;
		}
		if (this.Peek(0) == '>')
		{
			this.Advance(1);
		}
		return dfMarkupToken.Obtain(this.source, dfMarkupTokenType.EndTag, startIndex, num);
	}

	// Token: 0x06003F04 RID: 16132 RVA: 0x000EF184 File Offset: 0x000ED384
	private char Peek(int offset = 0)
	{
		if (this.index + offset > this.source.Length - 1)
		{
			return '\0';
		}
		return this.source[this.index + offset];
	}

	// Token: 0x06003F05 RID: 16133 RVA: 0x000EF1B8 File Offset: 0x000ED3B8
	private char Advance(int amount = 1)
	{
		this.index += amount;
		return this.Peek(0);
	}

	// Token: 0x040021B2 RID: 8626
	private static dfRichTextTokenizer singleton;

	// Token: 0x040021B3 RID: 8627
	private List<dfMarkupToken> tokens = new List<dfMarkupToken>();

	// Token: 0x040021B4 RID: 8628
	private string source;

	// Token: 0x040021B5 RID: 8629
	private int index;
}
