using System;
using System.Text.RegularExpressions;

// Token: 0x0200071E RID: 1822
public struct dfMarkupBorders
{
	// Token: 0x060042BC RID: 17084 RVA: 0x00103124 File Offset: 0x00101324
	public dfMarkupBorders(int left, int right, int top, int bottom)
	{
		this.left = left;
		this.top = top;
		this.right = right;
		this.bottom = bottom;
	}

	// Token: 0x17000D1A RID: 3354
	// (get) Token: 0x060042BD RID: 17085 RVA: 0x00103144 File Offset: 0x00101344
	public int horizontal
	{
		get
		{
			return this.left + this.right;
		}
	}

	// Token: 0x17000D1B RID: 3355
	// (get) Token: 0x060042BE RID: 17086 RVA: 0x00103154 File Offset: 0x00101354
	public int vertical
	{
		get
		{
			return this.top + this.bottom;
		}
	}

	// Token: 0x060042BF RID: 17087 RVA: 0x00103164 File Offset: 0x00101364
	public static dfMarkupBorders Parse(string value)
	{
		dfMarkupBorders result = default(dfMarkupBorders);
		value = Regex.Replace(value, "\\s+", " ");
		string[] array = value.Split(new char[]
		{
			' '
		});
		if (array.Length == 1)
		{
			int num = dfMarkupStyle.ParseSize(value, 0);
			result.left = (result.right = num);
			result.top = (result.bottom = num);
		}
		else if (array.Length == 2)
		{
			int num2 = dfMarkupStyle.ParseSize(array[0], 0);
			result.top = (result.bottom = num2);
			int num3 = dfMarkupStyle.ParseSize(array[1], 0);
			result.left = (result.right = num3);
		}
		else if (array.Length == 3)
		{
			int num4 = dfMarkupStyle.ParseSize(array[0], 0);
			result.top = num4;
			int num5 = dfMarkupStyle.ParseSize(array[1], 0);
			result.left = (result.right = num5);
			int num6 = dfMarkupStyle.ParseSize(array[2], 0);
			result.bottom = num6;
		}
		else if (array.Length == 4)
		{
			int num7 = dfMarkupStyle.ParseSize(array[0], 0);
			result.top = num7;
			int num8 = dfMarkupStyle.ParseSize(array[1], 0);
			result.right = num8;
			int num9 = dfMarkupStyle.ParseSize(array[2], 0);
			result.bottom = num9;
			int num10 = dfMarkupStyle.ParseSize(array[3], 0);
			result.left = num10;
		}
		return result;
	}

	// Token: 0x060042C0 RID: 17088 RVA: 0x001032D8 File Offset: 0x001014D8
	public override string ToString()
	{
		return string.Format("[T:{0},R:{1},L:{2},B:{3}]", new object[]
		{
			this.top,
			this.right,
			this.left,
			this.bottom
		});
	}

	// Token: 0x04002326 RID: 8998
	public int left;

	// Token: 0x04002327 RID: 8999
	public int top;

	// Token: 0x04002328 RID: 9000
	public int right;

	// Token: 0x04002329 RID: 9001
	public int bottom;
}
