using System;
using System.Collections.Generic;

// Token: 0x020007B3 RID: 1971
public class dfMarkupToken
{
	// Token: 0x060042F7 RID: 17143 RVA: 0x000F7480 File Offset: 0x000F5680
	protected dfMarkupToken()
	{
	}

	// Token: 0x060042F9 RID: 17145 RVA: 0x000F749C File Offset: 0x000F569C
	public static void Reset()
	{
		global::dfMarkupToken.poolIndex = 0;
	}

	// Token: 0x060042FA RID: 17146 RVA: 0x000F74A4 File Offset: 0x000F56A4
	public static global::dfMarkupToken Obtain(string source, global::dfMarkupTokenType type, int startIndex, int endIndex)
	{
		if (global::dfMarkupToken.poolIndex >= global::dfMarkupToken.pool.Count - 1)
		{
			global::dfMarkupToken.pool.Add(new global::dfMarkupToken());
		}
		global::dfMarkupToken dfMarkupToken = global::dfMarkupToken.pool[global::dfMarkupToken.poolIndex++];
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

	// Token: 0x17000CD5 RID: 3285
	// (get) Token: 0x060042FB RID: 17147 RVA: 0x000F7530 File Offset: 0x000F5730
	// (set) Token: 0x060042FC RID: 17148 RVA: 0x000F7538 File Offset: 0x000F5738
	public int AttributeCount { get; private set; }

	// Token: 0x17000CD6 RID: 3286
	// (get) Token: 0x060042FD RID: 17149 RVA: 0x000F7544 File Offset: 0x000F5744
	// (set) Token: 0x060042FE RID: 17150 RVA: 0x000F754C File Offset: 0x000F574C
	public global::dfMarkupTokenType TokenType { get; private set; }

	// Token: 0x17000CD7 RID: 3287
	// (get) Token: 0x060042FF RID: 17151 RVA: 0x000F7558 File Offset: 0x000F5758
	// (set) Token: 0x06004300 RID: 17152 RVA: 0x000F7560 File Offset: 0x000F5760
	public string Source { get; private set; }

	// Token: 0x17000CD8 RID: 3288
	// (get) Token: 0x06004301 RID: 17153 RVA: 0x000F756C File Offset: 0x000F576C
	// (set) Token: 0x06004302 RID: 17154 RVA: 0x000F7574 File Offset: 0x000F5774
	public int StartOffset { get; private set; }

	// Token: 0x17000CD9 RID: 3289
	// (get) Token: 0x06004303 RID: 17155 RVA: 0x000F7580 File Offset: 0x000F5780
	// (set) Token: 0x06004304 RID: 17156 RVA: 0x000F7588 File Offset: 0x000F5788
	public int EndOffset { get; private set; }

	// Token: 0x17000CDA RID: 3290
	// (get) Token: 0x06004305 RID: 17157 RVA: 0x000F7594 File Offset: 0x000F5794
	// (set) Token: 0x06004306 RID: 17158 RVA: 0x000F759C File Offset: 0x000F579C
	public int Width { get; internal set; }

	// Token: 0x17000CDB RID: 3291
	// (get) Token: 0x06004307 RID: 17159 RVA: 0x000F75A8 File Offset: 0x000F57A8
	// (set) Token: 0x06004308 RID: 17160 RVA: 0x000F75B0 File Offset: 0x000F57B0
	public int Height { get; set; }

	// Token: 0x17000CDC RID: 3292
	// (get) Token: 0x06004309 RID: 17161 RVA: 0x000F75BC File Offset: 0x000F57BC
	public int Length
	{
		get
		{
			return this.EndOffset - this.StartOffset + 1;
		}
	}

	// Token: 0x17000CDD RID: 3293
	// (get) Token: 0x0600430A RID: 17162 RVA: 0x000F75D0 File Offset: 0x000F57D0
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

	// Token: 0x17000CDE RID: 3294
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

	// Token: 0x0600430C RID: 17164 RVA: 0x000F7674 File Offset: 0x000F5874
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

	// Token: 0x0600430D RID: 17165 RVA: 0x000F76D0 File Offset: 0x000F58D0
	internal void AddAttribute(global::dfMarkupToken key, global::dfMarkupToken value)
	{
		global::dfMarkupTokenAttribute dfMarkupTokenAttribute = global::dfMarkupTokenAttribute.Obtain(key, value);
		if (this.AttributeCount == 0)
		{
			this.startAttributeIndex = dfMarkupTokenAttribute.Index;
		}
		this.AttributeCount++;
	}

	// Token: 0x0600430E RID: 17166 RVA: 0x000F770C File Offset: 0x000F590C
	public global::dfMarkupTokenAttribute GetAttribute(int index)
	{
		if (index < this.AttributeCount)
		{
			return global::dfMarkupTokenAttribute.GetAttribute(this.startAttributeIndex + index);
		}
		return null;
	}

	// Token: 0x0600430F RID: 17167 RVA: 0x000F772C File Offset: 0x000F592C
	public override string ToString()
	{
		return base.ToString();
	}

	// Token: 0x040023AB RID: 9131
	private static List<global::dfMarkupToken> pool = new List<global::dfMarkupToken>();

	// Token: 0x040023AC RID: 9132
	private static int poolIndex = 0;

	// Token: 0x040023AD RID: 9133
	private string value;

	// Token: 0x040023AE RID: 9134
	private int startAttributeIndex;
}
