using System;
using System.Collections.Generic;

// Token: 0x020007B7 RID: 1975
public class dfPlainTextTokenizer
{
	// Token: 0x06004333 RID: 17203 RVA: 0x000F8510 File Offset: 0x000F6710
	public static List<global::dfMarkupToken> Tokenize(string source)
	{
		if (global::dfPlainTextTokenizer.singleton == null)
		{
			global::dfPlainTextTokenizer.singleton = new global::dfPlainTextTokenizer();
		}
		return global::dfPlainTextTokenizer.singleton.tokenize(source);
	}

	// Token: 0x06004334 RID: 17204 RVA: 0x000F8534 File Offset: 0x000F6734
	private List<global::dfMarkupToken> tokenize(string source)
	{
		global::dfMarkupToken.Reset();
		global::dfMarkupTokenAttribute.Reset();
		this.tokens.Clear();
		int i = 0;
		int num = 0;
		int length = source.Length;
		while (i < length)
		{
			if (source[i] == '\r')
			{
				i++;
				num = i;
			}
			else
			{
				while (i < length && !char.IsWhiteSpace(source[i]))
				{
					i++;
				}
				if (i > num)
				{
					this.tokens.Add(global::dfMarkupToken.Obtain(source, global::dfMarkupTokenType.Text, num, i - 1));
					num = i;
				}
				if (i < length && source[i] == '\n')
				{
					this.tokens.Add(global::dfMarkupToken.Obtain(source, global::dfMarkupTokenType.Newline, i, i));
					i++;
					num = i;
				}
				while (i < length && source[i] != '\n' && source[i] != '\r' && char.IsWhiteSpace(source[i]))
				{
					i++;
				}
				if (i > num)
				{
					this.tokens.Add(global::dfMarkupToken.Obtain(source, global::dfMarkupTokenType.Whitespace, num, i - 1));
					num = i;
				}
			}
		}
		return this.tokens;
	}

	// Token: 0x040023C4 RID: 9156
	private static global::dfPlainTextTokenizer singleton;

	// Token: 0x040023C5 RID: 9157
	private List<global::dfMarkupToken> tokens = new List<global::dfMarkupToken>();
}
