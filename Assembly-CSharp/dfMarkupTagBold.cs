using System;

// Token: 0x02000729 RID: 1833
[dfMarkupTagInfo("strong")]
[dfMarkupTagInfo("b")]
public class dfMarkupTagBold : dfMarkupTag
{
	// Token: 0x060042F5 RID: 17141 RVA: 0x0010444C File Offset: 0x0010264C
	public dfMarkupTagBold() : base("b")
	{
	}

	// Token: 0x060042F6 RID: 17142 RVA: 0x0010445C File Offset: 0x0010265C
	public dfMarkupTagBold(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x060042F7 RID: 17143 RVA: 0x00104468 File Offset: 0x00102668
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		style = base.applyTextStyleAttributes(style);
		if (style.FontStyle == null)
		{
			style.FontStyle = 1;
		}
		else if (style.FontStyle == 2)
		{
			style.FontStyle = 3;
		}
		base._PerformLayoutImpl(container, style);
	}
}
