using System;

// Token: 0x020007E5 RID: 2021
public struct UITextPosition
{
	// Token: 0x06004854 RID: 18516 RVA: 0x001257F8 File Offset: 0x001239F8
	public UITextPosition(UITextRegion beforeOrPre)
	{
		this.line = 0;
		this.column = 0;
		this.position = 0;
		this.deformed = 0;
		this.region = beforeOrPre;
		this.uniformRegion = beforeOrPre;
	}

	// Token: 0x06004855 RID: 18517 RVA: 0x00125830 File Offset: 0x00123A30
	public UITextPosition(int line, int column, int position, UITextRegion region)
	{
		this.line = line;
		this.column = column;
		this.position = position;
		this.deformed = 0;
		this.region = region;
		this.uniformRegion = region;
	}

	// Token: 0x17000E04 RID: 3588
	// (get) Token: 0x06004856 RID: 18518 RVA: 0x0012586C File Offset: 0x00123A6C
	// (set) Token: 0x06004857 RID: 18519 RVA: 0x0012587C File Offset: 0x00123A7C
	public int uniformPosition
	{
		get
		{
			return this.position + (int)this.deformed;
		}
		set
		{
			this.deformed = (short)(value - this.position);
		}
	}

	// Token: 0x17000E05 RID: 3589
	// (get) Token: 0x06004858 RID: 18520 RVA: 0x00125890 File Offset: 0x00123A90
	public bool valid
	{
		get
		{
			return this.region != UITextRegion.Invalid;
		}
	}

	// Token: 0x06004859 RID: 18521 RVA: 0x001258A0 File Offset: 0x00123AA0
	public override string ToString()
	{
		return string.Format("[{0} pos={1}{{{2}:{3}}} uniform={{{4}-{5}}}]", new object[]
		{
			this.region,
			this.position,
			this.line,
			this.column,
			this.uniformPosition,
			this.uniformRegion
		});
	}

	// Token: 0x0400289E RID: 10398
	public int line;

	// Token: 0x0400289F RID: 10399
	public int column;

	// Token: 0x040028A0 RID: 10400
	public int position;

	// Token: 0x040028A1 RID: 10401
	public short deformed;

	// Token: 0x040028A2 RID: 10402
	public UITextRegion region;

	// Token: 0x040028A3 RID: 10403
	public UITextRegion uniformRegion;
}
