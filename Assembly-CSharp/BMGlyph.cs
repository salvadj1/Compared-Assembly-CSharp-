using System;
using System.Collections.Generic;

// Token: 0x0200086E RID: 2158
[Serializable]
public class BMGlyph
{
	// Token: 0x06004A2F RID: 18991 RVA: 0x0011DF48 File Offset: 0x0011C148
	public int GetKerning(int previousChar)
	{
		if (this.kerning != null)
		{
			int i = 0;
			int count = this.kerning.Count;
			while (i < count)
			{
				global::BMGlyph.Kerning kerning = this.kerning[i];
				if (kerning.previousChar == previousChar)
				{
					return kerning.amount;
				}
				i++;
			}
		}
		return 0;
	}

	// Token: 0x06004A30 RID: 18992 RVA: 0x0011DFA4 File Offset: 0x0011C1A4
	public void SetKerning(int previousChar, int amount)
	{
		if (this.kerning == null)
		{
			this.kerning = new List<global::BMGlyph.Kerning>();
		}
		for (int i = 0; i < this.kerning.Count; i++)
		{
			if (this.kerning[i].previousChar == previousChar)
			{
				global::BMGlyph.Kerning value = this.kerning[i];
				value.amount = amount;
				this.kerning[i] = value;
				return;
			}
		}
		global::BMGlyph.Kerning item = default(global::BMGlyph.Kerning);
		item.previousChar = previousChar;
		item.amount = amount;
		this.kerning.Add(item);
	}

	// Token: 0x06004A31 RID: 18993 RVA: 0x0011E044 File Offset: 0x0011C244
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		int num = this.x + this.width;
		int num2 = this.y + this.height;
		if (this.x < xMin)
		{
			int num3 = xMin - this.x;
			this.x += num3;
			this.width -= num3;
			this.offsetX += num3;
		}
		if (this.y < yMin)
		{
			int num4 = yMin - this.y;
			this.y += num4;
			this.height -= num4;
			this.offsetY += num4;
		}
		if (num > xMax)
		{
			this.width -= num - xMax;
		}
		if (num2 > yMax)
		{
			this.height -= num2 - yMax;
		}
	}

	// Token: 0x04002870 RID: 10352
	public int index;

	// Token: 0x04002871 RID: 10353
	public int x;

	// Token: 0x04002872 RID: 10354
	public int y;

	// Token: 0x04002873 RID: 10355
	public int width;

	// Token: 0x04002874 RID: 10356
	public int height;

	// Token: 0x04002875 RID: 10357
	public int offsetX;

	// Token: 0x04002876 RID: 10358
	public int offsetY;

	// Token: 0x04002877 RID: 10359
	public int advance;

	// Token: 0x04002878 RID: 10360
	public int channel;

	// Token: 0x04002879 RID: 10361
	public List<global::BMGlyph.Kerning> kerning;

	// Token: 0x0200086F RID: 2159
	public struct Kerning
	{
		// Token: 0x0400287A RID: 10362
		public int previousChar;

		// Token: 0x0400287B RID: 10363
		public int amount;
	}
}
