using System;
using System.Collections.Generic;

// Token: 0x020006E1 RID: 1761
public class dfMarkupToken
{
	// Token: 0x06003EDB RID: 16091 RVA: 0x000EE87C File Offset: 0x000ECA7C
	protected dfMarkupToken()
	{
	}

	// Token: 0x06003EDD RID: 16093 RVA: 0x000EE898 File Offset: 0x000ECA98
	public static void Reset()
	{
		dfMarkupToken.poolIndex = 0;
	}

	// Token: 0x06003EDE RID: 16094 RVA: 0x000EE8A0 File Offset: 0x000ECAA0
	public static dfMarkupToken Obtain(string source, dfMarkupTokenType type, int startIndex, int endIndex)
	{
		if (dfMarkupToken.poolIndex >= dfMarkupToken.pool.Count - 1)
		{
			dfMarkupToken.pool.Add(new dfMarkupToken());
		}
		dfMarkupToken dfMarkupToken = dfMarkupToken.pool[dfMarkupToken.poolIndex++];
		dfMarkupToken.Source = source;
		dfMarkupToken.TokenType = type;
		dfMarkupToken.value = null;
		dfMarkupToken.StartOffset = startIndex;
		dfMarkupToken.EndOffset = endIndex;
		dfMarkupToken.AttributeCount = 0;
		dfMarkupToken.startAttributeIndex = 0;
		dfMarkupToken.Width = 0;
		dfMarkupToken.Height = 0;
		return dfMarkupToken;
	}

	// Token: 0x17000C51 RID: 3153
	// (get) Token: 0x06003EDF RID: 16095 RVA: 0x000EE92C File Offset: 0x000ECB2C
	// (set) Token: 0x06003EE0 RID: 16096 RVA: 0x000EE934 File Offset: 0x000ECB34
	public int AttributeCount { get; private set; }

	// Token: 0x17000C52 RID: 3154
	// (get) Token: 0x06003EE1 RID: 16097 RVA: 0x000EE940 File Offset: 0x000ECB40
	// (set) Token: 0x06003EE2 RID: 16098 RVA: 0x000EE948 File Offset: 0x000ECB48
	public dfMarkupTokenType TokenType { get; private set; }

	// Token: 0x17000C53 RID: 3155
	// (get) Token: 0x06003EE3 RID: 16099 RVA: 0x000EE954 File Offset: 0x000ECB54
	// (set) Token: 0x06003EE4 RID: 16100 RVA: 0x000EE95C File Offset: 0x000ECB5C
	public string Source { get; private set; }

	// Token: 0x17000C54 RID: 3156
	// (get) Token: 0x06003EE5 RID: 16101 RVA: 0x000EE968 File Offset: 0x000ECB68
	// (set) Token: 0x06003EE6 RID: 16102 RVA: 0x000EE970 File Offset: 0x000ECB70
	public int StartOffset { get; private set; }

	// Token: 0x17000C55 RID: 3157
	// (get) Token: 0x06003EE7 RID: 16103 RVA: 0x000EE97C File Offset: 0x000ECB7C
	// (set) Token: 0x06003EE8 RID: 16104 RVA: 0x000EE984 File Offset: 0x000ECB84
	public int EndOffset { get; private set; }

	// Token: 0x17000C56 RID: 3158
	// (get) Token: 0x06003EE9 RID: 16105 RVA: 0x000EE990 File Offset: 0x000ECB90
	// (set) Token: 0x06003EEA RID: 16106 RVA: 0x000EE998 File Offset: 0x000ECB98
	public int Width { get; internal set; }

	// Token: 0x17000C57 RID: 3159
	// (get) Token: 0x06003EEB RID: 16107 RVA: 0x000EE9A4 File Offset: 0x000ECBA4
	// (set) Token: 0x06003EEC RID: 16108 RVA: 0x000EE9AC File Offset: 0x000ECBAC
	public int Height { get; set; }

	// Token: 0x17000C58 RID: 3160
	// (get) Token: 0x06003EED RID: 16109 RVA: 0x000EE9B8 File Offset: 0x000ECBB8
	public int Length
	{
		get
		{
			return this.EndOffset - this.StartOffset + 1;
		}
	}

	// Token: 0x17000C59 RID: 3161
	// (get) Token: 0x06003EEE RID: 16110 RVA: 0x000EE9CC File Offset: 0x000ECBCC
	public string Value
	{
		get
		{
			if (this.value == null)
			{
				int length = Math.Min(this.EndOffset - this.StartOffset + 1, this.Source.Length - this.StartOffset);
				this.value = this.Source.Substring(this.StartOffset, length);
			}
			return this.value;
		}
	}

	// Token: 0x17000C5A RID: 3162
	public char this[int index]
	{
		get
		{
			if (index < 0 || this.StartOffset + index > this.Source.Length - 1)
			{
				return '\0';
			}
			return this.Source[this.StartOffset + index];
		}
	}

	// Token: 0x06003EF0 RID: 16112 RVA: 0x000EEA70 File Offset: 0x000ECC70
	public bool Matches(string text)
	{
		if (this.Length != text.Length)
		{
			return false;
		}
		int length = text.Length;
		for (int i = 0; i < length; i++)
		{
			if (char.ToLowerInvariant(text[i]) != char.ToLowerInvariant(this[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06003EF1 RID: 16113 RVA: 0x000EEACC File Offset: 0x000ECCCC
	internal void AddAttribute(dfMarkupToken key, dfMarkupToken value)
	{
		dfMarkupTokenAttribute dfMarkupTokenAttribute = dfMarkupTokenAttribute.Obtain(key, value);
		if (this.AttributeCount == 0)
		{
			this.startAttributeIndex = dfMarkupTokenAttribute.Index;
		}
		this.AttributeCount++;
	}

	// Token: 0x06003EF2 RID: 16114 RVA: 0x000EEB08 File Offset: 0x000ECD08
	public dfMarkupTokenAttribute GetAttribute(int index)
	{
		if (index < this.AttributeCount)
		{
			return dfMarkupTokenAttribute.GetAttribute(this.startAttributeIndex + index);
		}
		return null;
	}

	// Token: 0x06003EF3 RID: 16115 RVA: 0x000EEB28 File Offset: 0x000ECD28
	public override string ToString()
	{
		return base.ToString();
	}

	// Token: 0x040021A2 RID: 8610
	private static List<dfMarkupToken> pool = new List<dfMarkupToken>();

	// Token: 0x040021A3 RID: 8611
	private static int poolIndex = 0;

	// Token: 0x040021A4 RID: 8612
	private string value;

	// Token: 0x040021A5 RID: 8613
	private int startAttributeIndex;
}
