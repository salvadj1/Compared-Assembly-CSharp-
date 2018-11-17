using System;
using System.Text.RegularExpressions;

// Token: 0x020007FA RID: 2042
public struct dfMarkupBorders
{
	// Token: 0x06004700 RID: 18176 RVA: 0x0010C434 File Offset: 0x0010A634
	public dfMarkupBorders(int left, int right, int top, int bottom)
	{
		this.left = left;
		this.top = top;
		this.right = right;
		this.bottom = bottom;
	}

	// Token: 0x17000DA4 RID: 3492
	// (get) Token: 0x06004701 RID: 18177 RVA: 0x0010C454 File Offset: 0x0010A654
	public int horizontal
	{
		get
		{
			return this.left + this.right;
		}
	}

	// Token: 0x17000DA5 RID: 3493
	// (get) Token: 0x06004702 RID: 18178 RVA: 0x0010C464 File Offset: 0x0010A664
	public int vertical
	{
		get
		{
			return this.top + this.bottom;
		}
	}

	// Token: 0x06004703 RID: 18179 RVA: 0x0010C474 File Offset: 0x0010A674
	public static global::dfMarkupBorders Parse(string value)
	{
		global::dfMarkupBorders result = default(global::dfMarkupBorders);
		value = Regex.Replace(value, "\\s+", " ");
		string[] array = value.Split(new char[]
		{
			' '
		});
		if (array.Length == 1)
		{
			int num = global::dfMarkupStyle.ParseSize(value, 0);
			result.left = (result.right = num);
			result.top = (result.bottom = num);
		}
		else if (array.Length == 2)
		{
			int num2 = global::dfMarkupStyle.ParseSize(array[0], 0);
			result.top = (result.bottom = num2);
			int num3 = global::dfMarkupStyle.ParseSize(array[1], 0);
			result.left = (result.right = num3);
		}
		else if (array.Length == 3)
		{
			int num4 = global::dfMarkupStyle.ParseSize(array[0], 0);
			result.top = num4;
			int num5 = global::dfMarkupStyle.ParseSize(array[1], 0);
			result.left = (result.right = num5);
			int num6 = global::dfMarkupStyle.ParseSize(array[2], 0);
			result.bottom = num6;
		}
		else if (array.Length == 4)
		{
			int num7 = global::dfMarkupStyle.ParseSize(array[0], 0);
			result.top = num7;
			int num8 = global::dfMarkupStyle.ParseSize(array[1], 0);
			result.right = num8;
			int num9 = global::dfMarkupStyle.ParseSize(array[2], 0);
			result.bottom = num9;
			int num10 = global::dfMarkupStyle.ParseSize(array[3], 0);
			result.left = num10;
		}
		return result;
	}

	// Token: 0x06004704 RID: 18180 RVA: 0x0010C5E8 File Offset: 0x0010A7E8
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

	// Token: 0x04002549 RID: 9545
	public int left;

	// Token: 0x0400254A RID: 9546
	public int top;

	// Token: 0x0400254B RID: 9547
	public int right;

	// Token: 0x0400254C RID: 9548
	public int bottom;
}
