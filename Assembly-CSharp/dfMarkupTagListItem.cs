using System;
using UnityEngine;

// Token: 0x02000727 RID: 1831
[dfMarkupTagInfo("li")]
public class dfMarkupTagListItem : dfMarkupTag
{
	// Token: 0x060042EF RID: 17135 RVA: 0x00104108 File Offset: 0x00102308
	public dfMarkupTagListItem() : base("li")
	{
	}

	// Token: 0x060042F0 RID: 17136 RVA: 0x00104118 File Offset: 0x00102318
	public dfMarkupTagListItem(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x060042F1 RID: 17137 RVA: 0x00104124 File Offset: 0x00102324
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		if (base.ChildNodes.Count == 0)
		{
			return;
		}
		float x = container.Size.x;
		dfMarkupBox dfMarkupBox = new dfMarkupBox(this, dfMarkupDisplayType.listItem, style);
		dfMarkupBox.Margins.top = 10;
		container.AddChild(dfMarkupBox);
		dfMarkupTagList dfMarkupTagList = base.Parent as dfMarkupTagList;
		if (dfMarkupTagList == null)
		{
			base._PerformLayoutImpl(container, style);
			return;
		}
		style.VerticalAlign = dfMarkupVerticalAlign.Baseline;
		string text = "•";
		if (dfMarkupTagList.TagName == "ol")
		{
			text = container.Children.Count + ".";
		}
		dfMarkupStyle style2 = style;
		style2.VerticalAlign = dfMarkupVerticalAlign.Baseline;
		style2.Align = dfMarkupTextAlign.Right;
		dfMarkupBoxText dfMarkupBoxText = dfMarkupBoxText.Obtain(this, dfMarkupDisplayType.inlineBlock, style2);
		dfMarkupBoxText.SetText(text);
		dfMarkupBoxText.Width = dfMarkupTagList.BulletWidth;
		dfMarkupBoxText.Margins.left = style.FontSize * 2;
		dfMarkupBox.AddChild(dfMarkupBoxText);
		dfMarkupBox dfMarkupBox2 = new dfMarkupBox(this, dfMarkupDisplayType.inlineBlock, style);
		int fontSize = style.FontSize;
		float num = x - dfMarkupBoxText.Size.x - (float)dfMarkupBoxText.Margins.left - (float)fontSize;
		dfMarkupBox2.Size = new Vector2(num, (float)fontSize);
		dfMarkupBox2.Margins.left = (int)((float)style.FontSize * 0.5f);
		dfMarkupBox.AddChild(dfMarkupBox2);
		for (int i = 0; i < base.ChildNodes.Count; i++)
		{
			base.ChildNodes[i].PerformLayout(dfMarkupBox2, style);
		}
		dfMarkupBox2.FitToContents(false);
		dfMarkupBox2.Parent.FitToContents(false);
		dfMarkupBox.FitToContents(false);
	}
}
