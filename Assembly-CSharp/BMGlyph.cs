using System;
using System.Collections.Generic;

// Token: 0x02000789 RID: 1929
[Serializable]
public class BMGlyph
{
	// Token: 0x060045C2 RID: 17858 RVA: 0x001145C8 File Offset: 0x001127C8
	public int GetKerning(int previousChar)
	{
		if (this.kerning != null)
		{
			int i = 0;
			int count = this.kerning.Count;
			while (i < count)
			{
				BMGlyph.Kerning kerning = this.kerning[i];
				if (kerning.previousChar == previousChar)
				{
					return kerning.amount;
				}
				i++;
			}
		}
		return 0;
	}

	// Token: 0x060045C3 RID: 17859 RVA: 0x00114624 File Offset: 0x00112824
	public void SetKerning(int previousChar, int amount)
	{
		if (this.kerning == null)
		{
			this.kerning = new List<BMGlyph.Kerning>();
		}
		for (int i = 0; i < this.kerning.Count; i++)
		{
			if (this.kerning[i].previousChar == previousChar)
			{
				BMGlyph.Kerning value = this.kerning[i];
				value.amount = amount;
				this.kerning[i] = value;
				return;
			}
		}
		BMGlyph.Kerning item = default(BMGlyph.Kerning);
		item.previousChar = previousChar;
		item.amount = amount;
		this.kerning.Add(item);
	}

	// Token: 0x060045C4 RID: 17860 RVA: 0x001146C4 File Offset: 0x001128C4
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

	// Token: 0x04002639 RID: 9785
	public int index;

	// Token: 0x0400263A RID: 9786
	public int x;

	// Token: 0x0400263B RID: 9787
	public int y;

	// Token: 0x0400263C RID: 9788
	public int width;

	// Token: 0x0400263D RID: 9789
	public int height;

	// Token: 0x0400263E RID: 9790
	public int offsetX;

	// Token: 0x0400263F RID: 9791
	public int offsetY;

	// Token: 0x04002640 RID: 9792
	public int advance;

	// Token: 0x04002641 RID: 9793
	public int channel;

	// Token: 0x04002642 RID: 9794
	public List<BMGlyph.Kerning> kerning;

	// Token: 0x0200078A RID: 1930
	public struct Kerning
	{
		// Token: 0x04002643 RID: 9795
		public int previousChar;

		// Token: 0x04002644 RID: 9796
		public int amount;
	}
}
