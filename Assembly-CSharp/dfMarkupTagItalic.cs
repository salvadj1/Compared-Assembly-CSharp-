using System;

// Token: 0x0200072B RID: 1835
[dfMarkupTagInfo("i")]
[dfMarkupTagInfo("em")]
public class dfMarkupTagItalic : dfMarkupTag
{
	// Token: 0x060042FC RID: 17148 RVA: 0x001046A8 File Offset: 0x001028A8
	public dfMarkupTagItalic() : base("i")
	{
	}

	// Token: 0x060042FD RID: 17149 RVA: 0x001046B8 File Offset: 0x001028B8
	public dfMarkupTagItalic(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x060042FE RID: 17150 RVA: 0x001046C4 File Offset: 0x001028C4
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		style = base.applyTextStyleAttributes(style);
		if (style.FontStyle == null)
		{
			style.FontStyle = 2;
		}
		else if (style.FontStyle == 1)
		{
			style.FontStyle = 3;
		}
		base._PerformLayoutImpl(container, style);
	}
}
