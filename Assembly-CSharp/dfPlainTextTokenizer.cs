using System;
using System.Collections.Generic;

// Token: 0x020006E5 RID: 1765
public class dfPlainTextTokenizer
{
	// Token: 0x06003F17 RID: 16151 RVA: 0x000EF90C File Offset: 0x000EDB0C
	public static List<dfMarkupToken> Tokenize(string source)
	{
		if (dfPlainTextTokenizer.singleton == null)
		{
			dfPlainTextTokenizer.singleton = new dfPlainTextTokenizer();
		}
		return dfPlainTextTokenizer.singleton.tokenize(source);
	}

	// Token: 0x06003F18 RID: 16152 RVA: 0x000EF930 File Offset: 0x000EDB30
	private List<dfMarkupToken> tokenize(string source)
	{
		dfMarkupToken.Reset();
		dfMarkupTokenAttribute.Reset();
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
					this.tokens.Add(dfMarkupToken.Obtain(source, dfMarkupTokenType.Text, num, i - 1));
					num = i;
				}
				if (i < length && source[i] == '\n')
				{
					this.tokens.Add(dfMarkupToken.Obtain(source, dfMarkupTokenType.Newline, i, i));
					i++;
					num = i;
				}
				while (i < length && source[i] != '\n' && source[i] != '\r' && char.IsWhiteSpace(source[i]))
				{
					i++;
				}
				if (i > num)
				{
					this.tokens.Add(dfMarkupToken.Obtain(source, dfMarkupTokenType.Whitespace, num, i - 1));
					num = i;
				}
			}
		}
		return this.tokens;
	}

	// Token: 0x040021BB RID: 8635
	private static dfPlainTextTokenizer singleton;

	// Token: 0x040021BC RID: 8636
	private List<dfMarkupToken> tokens = new List<dfMarkupToken>();
}
