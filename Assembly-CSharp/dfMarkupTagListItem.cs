using System;
using UnityEngine;

// Token: 0x02000803 RID: 2051
[global::dfMarkupTagInfo("li")]
public class dfMarkupTagListItem : global::dfMarkupTag
{
	// Token: 0x06004733 RID: 18227 RVA: 0x0010D418 File Offset: 0x0010B618
	public dfMarkupTagListItem() : base("li")
	{
	}

	// Token: 0x06004734 RID: 18228 RVA: 0x0010D428 File Offset: 0x0010B628
	public dfMarkupTagListItem(global::dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x06004735 RID: 18229 RVA: 0x0010D434 File Offset: 0x0010B634
	protected override void _PerformLayoutImpl(global::dfMarkupBox container, global::dfMarkupStyle style)
	{
		if (base.ChildNodes.Count == 0)
		{
			return;
		}
		float x = container.Size.x;
		global::dfMarkupBox dfMarkupBox = new global::dfMarkupBox(this, global::dfMarkupDisplayType.listItem, style);
		dfMarkupBox.Margins.top = 10;
		container.AddChild(dfMarkupBox);
		global::dfMarkupTagList dfMarkupTagList = base.Parent as global::dfMarkupTagList;
		if (dfMarkupTagList == null)
		{
			base._PerformLayoutImpl(container, style);
			return;
		}
		style.VerticalAlign = global::dfMarkupVerticalAlign.Baseline;
		string text = "•";
		if (dfMarkupTagList.TagName == "ol")
		{
			text = container.Children.Count + ".";
		}
		global::dfMarkupStyle style2 = style;
		style2.VerticalAlign = global::dfMarkupVerticalAlign.Baseline;
		style2.Align = global::dfMarkupTextAlign.Right;
		global::dfMarkupBoxText dfMarkupBoxText = global::dfMarkupBoxText.Obtain(this, global::dfMarkupDisplayType.inlineBlock, style2);
		dfMarkupBoxText.SetText(text);
		dfMarkupBoxText.Width = dfMarkupTagList.BulletWidth;
		dfMarkupBoxText.Margins.left = style.FontSize * 2;
		dfMarkupBox.AddChild(dfMarkupBoxText);
		global::dfMarkupBox dfMarkupBox2 = new global::dfMarkupBox(this, global::dfMarkupDisplayType.inlineBlock, style);
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
