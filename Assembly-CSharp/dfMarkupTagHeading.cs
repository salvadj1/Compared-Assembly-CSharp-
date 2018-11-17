using System;

// Token: 0x0200072A RID: 1834
[dfMarkupTagInfo("h3")]
[dfMarkupTagInfo("h4")]
[dfMarkupTagInfo("h5")]
[dfMarkupTagInfo("h6")]
[dfMarkupTagInfo("h2")]
[dfMarkupTagInfo("h1")]
public class dfMarkupTagHeading : dfMarkupTag
{
	// Token: 0x060042F8 RID: 17144 RVA: 0x001044B4 File Offset: 0x001026B4
	public dfMarkupTagHeading() : base("h1")
	{
	}

	// Token: 0x060042F9 RID: 17145 RVA: 0x001044C4 File Offset: 0x001026C4
	public dfMarkupTagHeading(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x060042FA RID: 17146 RVA: 0x001044D0 File Offset: 0x001026D0
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		dfMarkupBorders margins = default(dfMarkupBorders);
		dfMarkupStyle style2 = this.applyDefaultStyles(style, ref margins);
		style2 = base.applyTextStyleAttributes(style2);
		dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
		{
			"margin"
		});
		if (dfMarkupAttribute != null)
		{
			margins = dfMarkupBorders.Parse(dfMarkupAttribute.Value);
		}
		dfMarkupBox dfMarkupBox = new dfMarkupBox(this, dfMarkupDisplayType.block, style2);
		dfMarkupBox.Margins = margins;
		container.AddChild(dfMarkupBox);
		base._PerformLayoutImpl(dfMarkupBox, style2);
		dfMarkupBox.FitToContents(false);
	}

	// Token: 0x060042FB RID: 17147 RVA: 0x00104544 File Offset: 0x00102744
	private dfMarkupStyle applyDefaultStyles(dfMarkupStyle style, ref dfMarkupBorders margins)
	{
		float num = 1f;
		float num2 = 1f;
		string tagName = base.TagName;
		switch (tagName)
		{
		case "h1":
			num2 = 2f;
			num = 0.65f;
			break;
		case "h2":
			num2 = 1.5f;
			num = 0.75f;
			break;
		case "h3":
			num2 = 1.35f;
			num = 0.85f;
			break;
		case "h4":
			num2 = 1.15f;
			num = 0f;
			break;
		case "h5":
			num2 = 0.85f;
			num = 1.5f;
			break;
		case "h6":
			num2 = 0.75f;
			num = 1.75f;
			break;
		}
		style.FontSize = (int)((float)style.FontSize * num2);
		style.FontStyle = 1;
		style.Align = dfMarkupTextAlign.Left;
		num *= (float)style.FontSize;
		int top = margins.bottom = (int)num;
		margins.top = top;
		return style;
	}
}
