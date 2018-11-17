using System;
using UnityEngine;

// Token: 0x02000726 RID: 1830
[dfMarkupTagInfo("ol")]
[dfMarkupTagInfo("ul")]
public class dfMarkupTagList : dfMarkupTag
{
	// Token: 0x060042E9 RID: 17129 RVA: 0x00103F48 File Offset: 0x00102148
	public dfMarkupTagList() : base("ul")
	{
	}

	// Token: 0x060042EA RID: 17130 RVA: 0x00103F58 File Offset: 0x00102158
	public dfMarkupTagList(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x17000D24 RID: 3364
	// (get) Token: 0x060042EB RID: 17131 RVA: 0x00103F64 File Offset: 0x00102164
	// (set) Token: 0x060042EC RID: 17132 RVA: 0x00103F6C File Offset: 0x0010216C
	internal int BulletWidth { get; private set; }

	// Token: 0x060042ED RID: 17133 RVA: 0x00103F78 File Offset: 0x00102178
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		if (base.ChildNodes.Count == 0)
		{
			return;
		}
		style.Align = dfMarkupTextAlign.Left;
		dfMarkupBox dfMarkupBox = new dfMarkupBox(this, dfMarkupDisplayType.block, style);
		container.AddChild(dfMarkupBox);
		this.calculateBulletWidth(style);
		for (int i = 0; i < base.ChildNodes.Count; i++)
		{
			dfMarkupTag dfMarkupTag = base.ChildNodes[i] as dfMarkupTag;
			if (dfMarkupTag != null && !(dfMarkupTag.TagName != "li"))
			{
				dfMarkupTag.PerformLayout(dfMarkupBox, style);
			}
		}
		dfMarkupBox.FitToContents(false);
	}

	// Token: 0x060042EE RID: 17134 RVA: 0x00104014 File Offset: 0x00102214
	private void calculateBulletWidth(dfMarkupStyle style)
	{
		if (base.TagName == "ul")
		{
			this.BulletWidth = Mathf.CeilToInt(style.Font.MeasureText("•", style.FontSize, style.FontStyle).x);
			return;
		}
		int num = 0;
		for (int i = 0; i < base.ChildNodes.Count; i++)
		{
			dfMarkupTag dfMarkupTag = base.ChildNodes[i] as dfMarkupTag;
			if (dfMarkupTag != null && dfMarkupTag.TagName == "li")
			{
				num++;
			}
		}
		string text = new string('X', num.ToString().Length) + ".";
		this.BulletWidth = Mathf.CeilToInt(style.Font.MeasureText(text, style.FontSize, style.FontStyle).x);
	}
}
