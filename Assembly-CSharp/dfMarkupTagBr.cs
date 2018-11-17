using System;

// Token: 0x0200072D RID: 1837
[dfMarkupTagInfo("br")]
public class dfMarkupTagBr : dfMarkupTag
{
	// Token: 0x06004302 RID: 17154 RVA: 0x00104834 File Offset: 0x00102A34
	public dfMarkupTagBr() : base("br")
	{
		this.IsClosedTag = true;
	}

	// Token: 0x06004303 RID: 17155 RVA: 0x00104848 File Offset: 0x00102A48
	public dfMarkupTagBr(dfMarkupTag original) : base(original)
	{
		this.IsClosedTag = true;
	}

	// Token: 0x06004304 RID: 17156 RVA: 0x00104858 File Offset: 0x00102A58
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		container.AddLineBreak();
	}
}
