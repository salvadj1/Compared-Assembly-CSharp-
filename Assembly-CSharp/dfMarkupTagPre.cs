using System;

// Token: 0x02000808 RID: 2056
[global::dfMarkupTagInfo("pre")]
public class dfMarkupTagPre : global::dfMarkupTag
{
	// Token: 0x06004743 RID: 18243 RVA: 0x0010DA20 File Offset: 0x0010BC20
	public dfMarkupTagPre() : base("pre")
	{
	}

	// Token: 0x06004744 RID: 18244 RVA: 0x0010DA30 File Offset: 0x0010BC30
	public dfMarkupTagPre(global::dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x06004745 RID: 18245 RVA: 0x0010DA3C File Offset: 0x0010BC3C
	protected override void _PerformLayoutImpl(global::dfMarkupBox container, global::dfMarkupStyle style)
	{
		style = base.applyTextStyleAttributes(style);
		style.PreserveWhitespace = true;
		style.Preformatted = true;
		if (style.Align == global::dfMarkupTextAlign.Justify)
		{
			style.Align = global::dfMarkupTextAlign.Left;
		}
		global::dfMarkupBox dfMarkupBox;
		if (style.BackgroundColor.a > 0.1f)
		{
			global::dfMarkupBoxSprite dfMarkupBoxSprite = new global::dfMarkupBoxSprite(this, global::dfMarkupDisplayType.block, style);
			dfMarkupBoxSprite.LoadImage(base.Owner.Atlas, base.Owner.BlankTextureSprite);
			dfMarkupBoxSprite.Style.Color = style.BackgroundColor;
			dfMarkupBox = dfMarkupBoxSprite;
		}
		else
		{
			dfMarkupBox = new global::dfMarkupBox(this, global::dfMarkupDisplayType.block, style);
		}
		global::dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
		{
			"margin"
		});
		if (dfMarkupAttribute != null)
		{
			dfMarkupBox.Margins = global::dfMarkupBorders.Parse(dfMarkupAttribute.Value);
		}
		global::dfMarkupAttribute dfMarkupAttribute2 = base.findAttribute(new string[]
		{
			"padding"
		});
		if (dfMarkupAttribute2 != null)
		{
			dfMarkupBox.Padding = global::dfMarkupBorders.Parse(dfMarkupAttribute2.Value);
		}
		container.AddChild(dfMarkupBox);
		base._PerformLayoutImpl(dfMarkupBox, style);
		dfMarkupBox.FitToContents(false);
	}
}
