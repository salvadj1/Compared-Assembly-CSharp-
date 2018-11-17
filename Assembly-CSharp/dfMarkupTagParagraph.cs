using System;

// Token: 0x02000728 RID: 1832
[dfMarkupTagInfo("p")]
public class dfMarkupTagParagraph : dfMarkupTag
{
	// Token: 0x060042F2 RID: 17138 RVA: 0x001042D0 File Offset: 0x001024D0
	public dfMarkupTagParagraph() : base("p")
	{
	}

	// Token: 0x060042F3 RID: 17139 RVA: 0x001042E0 File Offset: 0x001024E0
	public dfMarkupTagParagraph(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x060042F4 RID: 17140 RVA: 0x001042EC File Offset: 0x001024EC
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		if (base.ChildNodes.Count == 0)
		{
			return;
		}
		style = base.applyTextStyleAttributes(style);
		int top = (container.Children.Count != 0) ? style.LineHeight : 0;
		dfMarkupBox dfMarkupBox;
		if (style.BackgroundColor.a > 0.005f)
		{
			dfMarkupBoxSprite dfMarkupBoxSprite = new dfMarkupBoxSprite(this, dfMarkupDisplayType.block, style);
			dfMarkupBoxSprite.Atlas = base.Owner.Atlas;
			dfMarkupBoxSprite.Source = base.Owner.BlankTextureSprite;
			dfMarkupBoxSprite.Style.Color = style.BackgroundColor;
			dfMarkupBox = dfMarkupBoxSprite;
		}
		else
		{
			dfMarkupBox = new dfMarkupBox(this, dfMarkupDisplayType.block, style);
		}
		dfMarkupBox.Margins = new dfMarkupBorders(0, 0, top, style.LineHeight);
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
		if (dfMarkupBox.Children.Count > 0)
		{
			dfMarkupBox.Children[dfMarkupBox.Children.Count - 1].IsNewline = true;
		}
		dfMarkupBox.FitToContents(true);
	}
}
