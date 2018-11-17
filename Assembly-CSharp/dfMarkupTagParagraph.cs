using System;

// Token: 0x02000804 RID: 2052
[global::dfMarkupTagInfo("p")]
public class dfMarkupTagParagraph : global::dfMarkupTag
{
	// Token: 0x06004736 RID: 18230 RVA: 0x0010D5E0 File Offset: 0x0010B7E0
	public dfMarkupTagParagraph() : base("p")
	{
	}

	// Token: 0x06004737 RID: 18231 RVA: 0x0010D5F0 File Offset: 0x0010B7F0
	public dfMarkupTagParagraph(global::dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x06004738 RID: 18232 RVA: 0x0010D5FC File Offset: 0x0010B7FC
	protected override void _PerformLayoutImpl(global::dfMarkupBox container, global::dfMarkupStyle style)
	{
		if (base.ChildNodes.Count == 0)
		{
			return;
		}
		style = base.applyTextStyleAttributes(style);
		int top = (container.Children.Count != 0) ? style.LineHeight : 0;
		global::dfMarkupBox dfMarkupBox;
		if (style.BackgroundColor.a > 0.005f)
		{
			global::dfMarkupBoxSprite dfMarkupBoxSprite = new global::dfMarkupBoxSprite(this, global::dfMarkupDisplayType.block, style);
			dfMarkupBoxSprite.Atlas = base.Owner.Atlas;
			dfMarkupBoxSprite.Source = base.Owner.BlankTextureSprite;
			dfMarkupBoxSprite.Style.Color = style.BackgroundColor;
			dfMarkupBox = dfMarkupBoxSprite;
		}
		else
		{
			dfMarkupBox = new global::dfMarkupBox(this, global::dfMarkupDisplayType.block, style);
		}
		dfMarkupBox.Margins = new global::dfMarkupBorders(0, 0, top, style.LineHeight);
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
		if (dfMarkupBox.Children.Count > 0)
		{
			dfMarkupBox.Children[dfMarkupBox.Children.Count - 1].IsNewline = true;
		}
		dfMarkupBox.FitToContents(true);
	}
}
