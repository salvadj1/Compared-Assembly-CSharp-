using System;
using System.Collections.Generic;

// Token: 0x020007B5 RID: 1973
public class dfRichTextTokenizer
{
	// Token: 0x06004316 RID: 17174 RVA: 0x000F77E4 File Offset: 0x000F59E4
	public static List<global::dfMarkupToken> Tokenize(string source)
	{
		if (global::dfRichTextTokenizer.singleton == null)
		{
			global::dfRichTextTokenizer.singleton = new global::dfRichTextTokenizer();
		}
		return global::dfRichTextTokenizer.singleton.tokenize(source);
	}

	// Token: 0x06004317 RID: 17175 RVA: 0x000F7808 File Offset: 0x000F5A08
	private List<global::dfMarkupToken> tokenize(string source)
	{
		global::dfMarkupToken.Reset();
		global::dfMarkupTokenAttribute.Reset();
		this.tokens.Clear();
		this.source = source;
		this.index = 0;
		while (this.index < source.Length)
		{
			char c = this.Peek(0);
			if (this.AtTagPosition())
			{
				global::dfMarkupToken dfMarkupToken = this.parseTag();
				if (dfMarkupToken != null)
				{
					this.tokens.Add(dfMarkupToken);
				}
			}
			else
			{
				global::dfMarkupToken dfMarkupToken2 = null;
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

	// Token: 0x06004318 RID: 17176 RVA: 0x000F78CC File Offset: 0x000F5ACC
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

	// Token: 0x06004319 RID: 17177 RVA: 0x000F791C File Offset: 0x000F5B1C
	private global::dfMarkupToken parseQuotedString()
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
		return global::dfMarkupToken.Obtain(this.source, global::dfMarkupTokenType.Text, startIndex, num);
	}

	// Token: 0x0600431A RID: 17178 RVA: 0x000F79AC File Offset: 0x000F5BAC
	private global::dfMarkupToken parseNonWhitespace()
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
		return global::dfMarkupToken.Obtain(this.source, global::dfMarkupTokenType.Text, startIndex, num);
	}

	// Token: 0x0600431B RID: 17179 RVA: 0x000F7A1C File Offset: 0x000F5C1C
	private global::dfMarkupToken parseWhitespace()
	{
		int num = this.index;
		int num2 = this.index;
		if (this.Peek(0) == '\n')
		{
			this.Advance(1);
			return global::dfMarkupToken.Obtain(this.source, global::dfMarkupTokenType.Newline, num, num);
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
		return global::dfMarkupToken.Obtain(this.source, global::dfMarkupTokenType.Whitespace, num, num2);
	}

	// Token: 0x0600431C RID: 17180 RVA: 0x000F7AB4 File Offset: 0x000F5CB4
	private global::dfMarkupToken parseWord()
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
		return global::dfMarkupToken.Obtain(this.source, global::dfMarkupTokenType.Text, startIndex, num);
	}

	// Token: 0x0600431D RID: 17181 RVA: 0x000F7B24 File Offset: 0x000F5D24
	private global::dfMarkupToken parseTag()
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
		global::dfMarkupToken dfMarkupToken = global::dfMarkupToken.Obtain(this.source, global::dfMarkupTokenType.StartTag, startIndex, num);
		while (this.index < this.source.Length && this.Peek(0) != '>')
		{
			c = this.Peek(0);
			if (char.IsWhiteSpace(c))
			{
				this.parseWhitespace();
			}
			else
			{
				global::dfMarkupToken dfMarkupToken2 = this.parseWord();
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
						global::dfMarkupToken dfMarkupToken3;
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

	// Token: 0x0600431E RID: 17182 RVA: 0x000F7CA0 File Offset: 0x000F5EA0
	private global::dfMarkupToken parseAttributeValue()
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
		return global::dfMarkupToken.Obtain(this.source, global::dfMarkupTokenType.Text, startIndex, num);
	}

	// Token: 0x0600431F RID: 17183 RVA: 0x000F7D0C File Offset: 0x000F5F0C
	private global::dfMarkupToken parseEndTag()
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
		return global::dfMarkupToken.Obtain(this.source, global::dfMarkupTokenType.EndTag, startIndex, num);
	}

	// Token: 0x06004320 RID: 17184 RVA: 0x000F7D88 File Offset: 0x000F5F88
	private char Peek(int offset = 0)
	{
		if (this.index + offset > this.source.Length - 1)
		{
			return '\0';
		}
		return this.source[this.index + offset];
	}

	// Token: 0x06004321 RID: 17185 RVA: 0x000F7DBC File Offset: 0x000F5FBC
	private char Advance(int amount = 1)
	{
		this.index += amount;
		return this.Peek(0);
	}

	// Token: 0x040023BB RID: 9147
	private static global::dfRichTextTokenizer singleton;

	// Token: 0x040023BC RID: 9148
	private List<global::dfMarkupToken> tokens = new List<global::dfMarkupToken>();

	// Token: 0x040023BD RID: 9149
	private string source;

	// Token: 0x040023BE RID: 9150
	private int index;
}
