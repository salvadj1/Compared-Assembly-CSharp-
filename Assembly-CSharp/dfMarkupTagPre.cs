using System;

// Token: 0x0200072C RID: 1836
[dfMarkupTagInfo("pre")]
public class dfMarkupTagPre : dfMarkupTag
{
	// Token: 0x060042FF RID: 17151 RVA: 0x00104710 File Offset: 0x00102910
	public dfMarkupTagPre() : base("pre")
	{
	}

	// Token: 0x06004300 RID: 17152 RVA: 0x00104720 File Offset: 0x00102920
	public dfMarkupTagPre(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x06004301 RID: 17153 RVA: 0x0010472C File Offset: 0x0010292C
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		style = base.applyTextStyleAttributes(style);
		style.PreserveWhitespace = true;
		style.Preformatted = true;
		if (style.Align == dfMarkupTextAlign.Justify)
		{
			style.Align = dfMarkupTextAlign.Left;
		}
		dfMarkupBox dfMarkupBox;
		if (style.BackgroundColor.a > 0.1f)
		{
			dfMarkupBoxSprite dfMarkupBoxSprite = new dfMarkupBoxSprite(this, dfMarkupDisplayType.block, style);
			dfMarkupBoxSprite.LoadImage(base.Owner.Atlas, base.Owner.BlankTextureSprite);
			dfMarkupBoxSprite.Style.Color = style.BackgroundColor;
			dfMarkupBox = dfMarkupBoxSprite;
		}
		else
		{
			dfMarkupBox = new dfMarkupBox(this, dfMarkupDisplayType.block, style);
		}
		dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
		{
			"margin"
		});
		if (dfMarkupAttribute != null)
		{
			dfMarkupBox.Margins = dfMarkupBorders.Parse(dfMarkupAttribute.Value);
		}
		dfMarkupAttribute dfMarkupAttribute2 = base.findAttribute(new string[]
		{
			"padding"
		});
		if (dfMarkupAttribute2 != null)
		{
			dfMarkupBox.Padding = dfMarkupBorders.Parse(dfMarkupAttribute2.Value);
		}
		container.AddChild(dfMarkupBox);
		base._PerformLayoutImpl(dfMarkupBox, style);
		dfMarkupBox.FitToContents(false);
	}
}
