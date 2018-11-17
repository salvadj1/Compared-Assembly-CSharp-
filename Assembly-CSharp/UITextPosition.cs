using System;

// Token: 0x020008D6 RID: 2262
public struct UITextPosition
{
	// Token: 0x06004CFF RID: 19711 RVA: 0x0012F75C File Offset: 0x0012D95C
	public UITextPosition(global::UITextRegion beforeOrPre)
	{
		this.line = 0;
		this.column = 0;
		this.position = 0;
		this.deformed = 0;
		this.region = beforeOrPre;
		this.uniformRegion = beforeOrPre;
	}

	// Token: 0x06004D00 RID: 19712 RVA: 0x0012F794 File Offset: 0x0012D994
	public UITextPosition(int line, int column, int position, global::UITextRegion region)
	{
		this.line = line;
		this.column = column;
		this.position = position;
		this.deformed = 0;
		this.region = region;
		this.uniformRegion = region;
	}

	// Token: 0x17000E9E RID: 3742
	// (get) Token: 0x06004D01 RID: 19713 RVA: 0x0012F7D0 File Offset: 0x0012D9D0
	// (set) Token: 0x06004D02 RID: 19714 RVA: 0x0012F7E0 File Offset: 0x0012D9E0
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

	// Token: 0x17000E9F RID: 3743
	// (get) Token: 0x06004D03 RID: 19715 RVA: 0x0012F7F4 File Offset: 0x0012D9F4
	public bool valid
	{
		get
		{
			return this.region != global::UITextRegion.Invalid;
		}
	}

	// Token: 0x06004D04 RID: 19716 RVA: 0x0012F804 File Offset: 0x0012DA04
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

	// Token: 0x04002AEC RID: 10988
	public int line;

	// Token: 0x04002AED RID: 10989
	public int column;

	// Token: 0x04002AEE RID: 10990
	public int position;

	// Token: 0x04002AEF RID: 10991
	public short deformed;

	// Token: 0x04002AF0 RID: 10992
	public global::UITextRegion region;

	// Token: 0x04002AF1 RID: 10993
	public global::UITextRegion uniformRegion;
}
