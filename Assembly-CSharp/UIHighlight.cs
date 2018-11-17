using System;

// Token: 0x020007E6 RID: 2022
public struct UIHighlight
{
	// Token: 0x17000E06 RID: 3590
	// (get) Token: 0x0600485A RID: 18522 RVA: 0x00125914 File Offset: 0x00123B14
	public static UIHighlight invalid
	{
		get
		{
			return new UIHighlight
			{
				a = new UIHighlight.Node
				{
					i = -1
				},
				b = new UIHighlight.Node
				{
					i = -1
				}
			};
		}
	}

	// Token: 0x17000E07 RID: 3591
	// (get) Token: 0x0600485B RID: 18523 RVA: 0x0012595C File Offset: 0x00123B5C
	public int lineCount
	{
		get
		{
			return (this.a.i == this.b.i) ? 0 : (this.b.L - this.a.L + 1);
		}
	}

	// Token: 0x17000E08 RID: 3592
	// (get) Token: 0x0600485C RID: 18524 RVA: 0x001259A4 File Offset: 0x00123BA4
	public bool empty
	{
		get
		{
			return this.a.i == this.b.i;
		}
	}

	// Token: 0x17000E09 RID: 3593
	// (get) Token: 0x0600485D RID: 18525 RVA: 0x001259C0 File Offset: 0x00123BC0
	public bool any
	{
		get
		{
			return this.a.i != this.b.i;
		}
	}

	// Token: 0x17000E0A RID: 3594
	// (get) Token: 0x0600485E RID: 18526 RVA: 0x001259E0 File Offset: 0x00123BE0
	public int characterCount
	{
		get
		{
			return this.b.i - this.a.i;
		}
	}

	// Token: 0x17000E0B RID: 3595
	// (get) Token: 0x0600485F RID: 18527 RVA: 0x001259FC File Offset: 0x00123BFC
	public int lineSpan
	{
		get
		{
			return this.b.L - this.a.L;
		}
	}

	// Token: 0x17000E0C RID: 3596
	// (get) Token: 0x06004860 RID: 18528 RVA: 0x00125A18 File Offset: 0x00123C18
	public UIHighlight.Node delta
	{
		get
		{
			UIHighlight.Node result;
			result.i = this.b.i - this.a.i;
			result.L = this.b.L - this.a.L;
			result.C = this.b.C - this.a.C;
			return result;
		}
	}

	// Token: 0x040028A4 RID: 10404
	public UIHighlight.Node a;

	// Token: 0x040028A5 RID: 10405
	public UIHighlight.Node b;

	// Token: 0x020007E7 RID: 2023
	public struct Node
	{
		// Token: 0x06004861 RID: 18529 RVA: 0x00125A80 File Offset: 0x00123C80
		public override string ToString()
		{
			return string.Format("[{0}({1}:{2})]", this.i, this.L, this.C);
		}

		// Token: 0x040028A6 RID: 10406
		public int i;

		// Token: 0x040028A7 RID: 10407
		public int L;

		// Token: 0x040028A8 RID: 10408
		public int C;
	}
}
