using System;
using UnityEngine;

// Token: 0x02000802 RID: 2050
[global::dfMarkupTagInfo("ul")]
[global::dfMarkupTagInfo("ol")]
public class dfMarkupTagList : global::dfMarkupTag
{
	// Token: 0x0600472D RID: 18221 RVA: 0x0010D258 File Offset: 0x0010B458
	public dfMarkupTagList() : base("ul")
	{
	}

	// Token: 0x0600472E RID: 18222 RVA: 0x0010D268 File Offset: 0x0010B468
	public dfMarkupTagList(global::dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x17000DAE RID: 3502
	// (get) Token: 0x0600472F RID: 18223 RVA: 0x0010D274 File Offset: 0x0010B474
	// (set) Token: 0x06004730 RID: 18224 RVA: 0x0010D27C File Offset: 0x0010B47C
	internal int BulletWidth { get; private set; }

	// Token: 0x06004731 RID: 18225 RVA: 0x0010D288 File Offset: 0x0010B488
	protected override void _PerformLayoutImpl(global::dfMarkupBox container, global::dfMarkupStyle style)
	{
		if (base.ChildNodes.Count == 0)
		{
			return;
		}
		style.Align = global::dfMarkupTextAlign.Left;
		global::dfMarkupBox dfMarkupBox = new global::dfMarkupBox(this, global::dfMarkupDisplayType.block, style);
		container.AddChild(dfMarkupBox);
		this.calculateBulletWidth(style);
		for (int i = 0; i < base.ChildNodes.Count; i++)
		{
			global::dfMarkupTag dfMarkupTag = base.ChildNodes[i] as global::dfMarkupTag;
			if (dfMarkupTag != null && !(dfMarkupTag.TagName != "li"))
			{
				dfMarkupTag.PerformLayout(dfMarkupBox, style);
			}
		}
		dfMarkupBox.FitToContents(false);
	}

	// Token: 0x06004732 RID: 18226 RVA: 0x0010D324 File Offset: 0x0010B524
	private void calculateBulletWidth(global::dfMarkupStyle style)
	{
		if (base.TagName == "ul")
		{
			this.BulletWidth = Mathf.CeilToInt(style.Font.MeasureText("•", style.FontSize, style.FontStyle).x);
			return;
		}
		int num = 0;
		for (int i = 0; i < base.ChildNodes.Count; i++)
		{
			global::dfMarkupTag dfMarkupTag = base.ChildNodes[i] as global::dfMarkupTag;
			if (dfMarkupTag != null && dfMarkupTag.TagName == "li")
			{
				num++;
			}
		}
		string text = new string('X', num.ToString().Length) + ".";
		this.BulletWidth = Mathf.CeilToInt(style.Font.MeasureText(text, style.FontSize, style.FontStyle).x);
	}
}
