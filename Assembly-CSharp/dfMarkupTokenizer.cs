using System;
using System.Collections.Generic;

// Token: 0x020007B6 RID: 1974
public class dfMarkupTokenizer
{
	// Token: 0x06004324 RID: 17188 RVA: 0x000F7E18 File Offset: 0x000F6018
	public static List<global::dfMarkupToken> Tokenize(string source)
	{
		if (global::dfMarkupTokenizer.singleton == null)
		{
			global::dfMarkupTokenizer.singleton = new global::dfMarkupTokenizer();
		}
		return global::dfMarkupTokenizer.singleton.tokenize(source);
	}

	// Token: 0x06004325 RID: 17189 RVA: 0x000F7E3C File Offset: 0x000F603C
	private List<global::dfMarkupToken> tokenize(string source)
	{
		this.reset();
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

	// Token: 0x06004326 RID: 17190 RVA: 0x000F7EF4 File Offset: 0x000F60F4
	private void reset()
	{
		global::dfMarkupToken.Reset();
		global::dfMarkupTokenAttribute.Reset();
		this.tokens.Clear();
	}

	// Token: 0x06004327 RID: 17191 RVA: 0x000F7F0C File Offset: 0x000F610C
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

	// Token: 0x06004328 RID: 17192 RVA: 0x000F7F78 File Offset: 0x000F6178
	private bool isValidTag(int index, bool endTag)
	{
		for (int i = 0; i < global::dfMarkupTokenizer.validTags.Count; i++)
		{
			string text = global::dfMarkupTokenizer.validTags[i];
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

	// Token: 0x06004329 RID: 17193 RVA: 0x000F804C File Offset: 0x000F624C
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

	// Token: 0x0600432A RID: 17194 RVA: 0x000F80DC File Offset: 0x000F62DC
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

	// Token: 0x0600432B RID: 17195 RVA: 0x000F814C File Offset: 0x000F634C
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

	// Token: 0x0600432C RID: 17196 RVA: 0x000F81E4 File Offset: 0x000F63E4
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

	// Token: 0x0600432D RID: 17197 RVA: 0x000F8254 File Offset: 0x000F6454
	private global::dfMarkupToken parseTag()
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
		global::dfMarkupToken dfMarkupToken = global::dfMarkupToken.Obtain(this.source, global::dfMarkupTokenType.StartTag, startIndex, num);
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
				global::dfMarkupToken dfMarkupToken2 = this.parseQuotedString();
				dfMarkupToken.AddAttribute(dfMarkupToken2, dfMarkupToken2);
			}
			else
			{
				while (this.index < this.source.Length && this.Advance(1) != ']')
				{
					num2++;
				}
				global::dfMarkupToken dfMarkupToken3 = global::dfMarkupToken.Obtain(this.source, global::dfMarkupTokenType.Text, startIndex2, num2);
				dfMarkupToken.AddAttribute(dfMarkupToken3, dfMarkupToken3);
			}
		}
		if (this.Peek(0) == ']')
		{
			this.Advance(1);
		}
		return dfMarkupToken;
	}

	// Token: 0x0600432E RID: 17198 RVA: 0x000F83C8 File Offset: 0x000F65C8
	private global::dfMarkupToken parseAttributeValue()
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
		return global::dfMarkupToken.Obtain(this.source, global::dfMarkupTokenType.Text, startIndex, num);
	}

	// Token: 0x0600432F RID: 17199 RVA: 0x000F8434 File Offset: 0x000F6634
	private global::dfMarkupToken parseEndTag()
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
		return global::dfMarkupToken.Obtain(this.source, global::dfMarkupTokenType.EndTag, startIndex, num);
	}

	// Token: 0x06004330 RID: 17200 RVA: 0x000F84B0 File Offset: 0x000F66B0
	private char Peek(int offset = 0)
	{
		if (this.index + offset > this.source.Length - 1)
		{
			return '\0';
		}
		return this.source[this.index + offset];
	}

	// Token: 0x06004331 RID: 17201 RVA: 0x000F84E4 File Offset: 0x000F66E4
	private char Advance(int amount = 1)
	{
		this.index += amount;
		return this.Peek(0);
	}

	// Token: 0x040023BF RID: 9151
	private static global::dfMarkupTokenizer singleton;

	// Token: 0x040023C0 RID: 9152
	private static List<string> validTags = new List<string>
	{
		"color",
		"sprite"
	};

	// Token: 0x040023C1 RID: 9153
	private List<global::dfMarkupToken> tokens = new List<global::dfMarkupToken>();

	// Token: 0x040023C2 RID: 9154
	private string source;

	// Token: 0x040023C3 RID: 9155
	private int index;
}
