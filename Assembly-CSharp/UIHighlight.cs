using System;

// Token: 0x020008D7 RID: 2263
public struct UIHighlight
{
	// Token: 0x17000EA0 RID: 3744
	// (get) Token: 0x06004D05 RID: 19717 RVA: 0x0012F878 File Offset: 0x0012DA78
	public static global::UIHighlight invalid
	{
		get
		{
			return new global::UIHighlight
			{
				a = new global::UIHighlight.Node
				{
					i = -1
				},
				b = new global::UIHighlight.Node
				{
					i = -1
				}
			};
		}
	}

	// Token: 0x17000EA1 RID: 3745
	// (get) Token: 0x06004D06 RID: 19718 RVA: 0x0012F8C0 File Offset: 0x0012DAC0
	public int lineCount
	{
		get
		{
			return (this.a.i == this.b.i) ? 0 : (this.b.L - this.a.L + 1);
		}
	}

	// Token: 0x17000EA2 RID: 3746
	// (get) Token: 0x06004D07 RID: 19719 RVA: 0x0012F908 File Offset: 0x0012DB08
	public bool empty
	{
		get
		{
			return this.a.i == this.b.i;
		}
	}

	// Token: 0x17000EA3 RID: 3747
	// (get) Token: 0x06004D08 RID: 19720 RVA: 0x0012F924 File Offset: 0x0012DB24
	public bool any
	{
		get
		{
			return this.a.i != this.b.i;
		}
	}

	// Token: 0x17000EA4 RID: 3748
	// (get) Token: 0x06004D09 RID: 19721 RVA: 0x0012F944 File Offset: 0x0012DB44
	public int characterCount
	{
		get
		{
			return this.b.i - this.a.i;
		}
	}

	// Token: 0x17000EA5 RID: 3749
	// (get) Token: 0x06004D0A RID: 19722 RVA: 0x0012F960 File Offset: 0x0012DB60
	public int lineSpan
	{
		get
		{
			return this.b.L - this.a.L;
		}
	}

	// Token: 0x17000EA6 RID: 3750
	// (get) Token: 0x06004D0B RID: 19723 RVA: 0x0012F97C File Offset: 0x0012DB7C
	public global::UIHighlight.Node delta
	{
		get
		{
			global::UIHighlight.Node result;
			result.i = this.b.i - this.a.i;
			result.L = this.b.L - this.a.L;
			result.C = this.b.C - this.a.C;
			return result;
		}
	}

	// Token: 0x04002AF2 RID: 10994
	public global::UIHighlight.Node a;

	// Token: 0x04002AF3 RID: 10995
	public global::UIHighlight.Node b;

	// Token: 0x020008D8 RID: 2264
	public struct Node
	{
		// Token: 0x06004D0C RID: 19724 RVA: 0x0012F9E4 File Offset: 0x0012DBE4
		public override string ToString()
		{
			return string.Format("[{0}({1}:{2})]", this.i, this.L, this.C);
		}

		// Token: 0x04002AF4 RID: 10996
		public int i;

		// Token: 0x04002AF5 RID: 10997
		public int L;

		// Token: 0x04002AF6 RID: 10998
		public int C;
	}
}
