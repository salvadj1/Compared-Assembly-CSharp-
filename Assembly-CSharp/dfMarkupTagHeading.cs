﻿using System;

// Token: 0x02000806 RID: 2054
[global::dfMarkupTagInfo("h2")]
[global::dfMarkupTagInfo("h1")]
[global::dfMarkupTagInfo("h5")]
[global::dfMarkupTagInfo("h4")]
[global::dfMarkupTagInfo("h3")]
[global::dfMarkupTagInfo("h6")]
public class dfMarkupTagHeading : global::dfMarkupTag
{
	// Token: 0x0600473C RID: 18236 RVA: 0x0010D7C4 File Offset: 0x0010B9C4
	public dfMarkupTagHeading() : base("h1")
	{
	}

	// Token: 0x0600473D RID: 18237 RVA: 0x0010D7D4 File Offset: 0x0010B9D4
	public dfMarkupTagHeading(global::dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x0600473E RID: 18238 RVA: 0x0010D7E0 File Offset: 0x0010B9E0
	protected override void _PerformLayoutImpl(global::dfMarkupBox container, global::dfMarkupStyle style)
	{
		global::dfMarkupBorders margins = default(global::dfMarkupBorders);
		global::dfMarkupStyle style2 = this.applyDefaultStyles(style, ref margins);
		style2 = base.applyTextStyleAttributes(style2);
		global::dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
		{
			"margin"
		});
		if (dfMarkupAttribute != null)
		{
			margins = global::dfMarkupBorders.Parse(dfMarkupAttribute.Value);
		}
		global::dfMarkupBox dfMarkupBox = new global::dfMarkupBox(this, global::dfMarkupDisplayType.block, style2);
		dfMarkupBox.Margins = margins;
		container.AddChild(dfMarkupBox);
		base._PerformLayoutImpl(dfMarkupBox, style2);
		dfMarkupBox.FitToContents(false);
	}

	// Token: 0x0600473F RID: 18239 RVA: 0x0010D854 File Offset: 0x0010BA54
	private global::dfMarkupStyle applyDefaultStyles(global::dfMarkupStyle style, ref global::dfMarkupBorders margins)
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
		style.Align = global::dfMarkupTextAlign.Left;
		num *= (float)style.FontSize;
		int top = margins.bottom = (int)num;
		margins.top = top;
		return style;
	}
}
