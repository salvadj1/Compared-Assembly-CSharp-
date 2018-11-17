using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020007FF RID: 2047
public class dfMarkupTag : global::dfMarkupElement
{
	// Token: 0x06004710 RID: 18192 RVA: 0x0010CB04 File Offset: 0x0010AD04
	public dfMarkupTag(string tagName)
	{
		this.Attributes = new List<global::dfMarkupAttribute>();
		this.TagName = tagName;
		this.id = tagName + global::dfMarkupTag.ELEMENTID++.ToString("X");
	}

	// Token: 0x06004711 RID: 18193 RVA: 0x0010CB50 File Offset: 0x0010AD50
	public dfMarkupTag(global::dfMarkupTag original)
	{
		this.TagName = original.TagName;
		this.Attributes = original.Attributes;
		this.IsEndTag = original.IsEndTag;
		this.IsClosedTag = original.IsClosedTag;
		this.IsInline = original.IsInline;
		this.id = original.id;
		List<global::dfMarkupElement> childNodes = original.ChildNodes;
		for (int i = 0; i < childNodes.Count; i++)
		{
			global::dfMarkupElement node = childNodes[i];
			base.AddChildNode(node);
		}
	}

	// Token: 0x17000DA7 RID: 3495
	// (get) Token: 0x06004713 RID: 18195 RVA: 0x0010CBDC File Offset: 0x0010ADDC
	// (set) Token: 0x06004714 RID: 18196 RVA: 0x0010CBE4 File Offset: 0x0010ADE4
	public string TagName { get; set; }

	// Token: 0x17000DA8 RID: 3496
	// (get) Token: 0x06004715 RID: 18197 RVA: 0x0010CBF0 File Offset: 0x0010ADF0
	public string ID
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x17000DA9 RID: 3497
	// (get) Token: 0x06004716 RID: 18198 RVA: 0x0010CBF8 File Offset: 0x0010ADF8
	// (set) Token: 0x06004717 RID: 18199 RVA: 0x0010CC00 File Offset: 0x0010AE00
	public virtual bool IsEndTag { get; set; }

	// Token: 0x17000DAA RID: 3498
	// (get) Token: 0x06004718 RID: 18200 RVA: 0x0010CC0C File Offset: 0x0010AE0C
	// (set) Token: 0x06004719 RID: 18201 RVA: 0x0010CC14 File Offset: 0x0010AE14
	public virtual bool IsClosedTag { get; set; }

	// Token: 0x17000DAB RID: 3499
	// (get) Token: 0x0600471A RID: 18202 RVA: 0x0010CC20 File Offset: 0x0010AE20
	// (set) Token: 0x0600471B RID: 18203 RVA: 0x0010CC28 File Offset: 0x0010AE28
	public virtual bool IsInline { get; set; }

	// Token: 0x17000DAC RID: 3500
	// (get) Token: 0x0600471C RID: 18204 RVA: 0x0010CC34 File Offset: 0x0010AE34
	// (set) Token: 0x0600471D RID: 18205 RVA: 0x0010CC3C File Offset: 0x0010AE3C
	public global::dfRichTextLabel Owner
	{
		get
		{
			return this.owner;
		}
		set
		{
			this.owner = value;
			for (int i = 0; i < base.ChildNodes.Count; i++)
			{
				global::dfMarkupTag dfMarkupTag = base.ChildNodes[i] as global::dfMarkupTag;
				if (dfMarkupTag != null)
				{
					dfMarkupTag.Owner = value;
				}
			}
		}
	}

	// Token: 0x0600471E RID: 18206 RVA: 0x0010CC8C File Offset: 0x0010AE8C
	internal override void Release()
	{
		base.Release();
	}

	// Token: 0x0600471F RID: 18207 RVA: 0x0010CC94 File Offset: 0x0010AE94
	protected override void _PerformLayoutImpl(global::dfMarkupBox container, global::dfMarkupStyle style)
	{
		if (this.IsEndTag)
		{
			return;
		}
		for (int i = 0; i < base.ChildNodes.Count; i++)
		{
			base.ChildNodes[i].PerformLayout(container, style);
		}
	}

	// Token: 0x06004720 RID: 18208 RVA: 0x0010CCDC File Offset: 0x0010AEDC
	protected global::dfMarkupStyle applyTextStyleAttributes(global::dfMarkupStyle style)
	{
		global::dfMarkupAttribute dfMarkupAttribute = this.findAttribute(new string[]
		{
			"font",
			"font-family"
		});
		if (dfMarkupAttribute != null)
		{
			style.Font = global::dfDynamicFont.FindByName(dfMarkupAttribute.Value);
		}
		global::dfMarkupAttribute dfMarkupAttribute2 = this.findAttribute(new string[]
		{
			"style",
			"font-style"
		});
		if (dfMarkupAttribute2 != null)
		{
			style.FontStyle = global::dfMarkupStyle.ParseFontStyle(dfMarkupAttribute2.Value, style.FontStyle);
		}
		global::dfMarkupAttribute dfMarkupAttribute3 = this.findAttribute(new string[]
		{
			"size",
			"font-size"
		});
		if (dfMarkupAttribute3 != null)
		{
			style.FontSize = global::dfMarkupStyle.ParseSize(dfMarkupAttribute3.Value, style.FontSize);
		}
		global::dfMarkupAttribute dfMarkupAttribute4 = this.findAttribute(new string[]
		{
			"color"
		});
		if (dfMarkupAttribute4 != null)
		{
			Color color = global::dfMarkupStyle.ParseColor(dfMarkupAttribute4.Value, style.Color);
			color.a = style.Opacity;
			style.Color = color;
		}
		global::dfMarkupAttribute dfMarkupAttribute5 = this.findAttribute(new string[]
		{
			"align",
			"text-align"
		});
		if (dfMarkupAttribute5 != null)
		{
			style.Align = global::dfMarkupStyle.ParseTextAlignment(dfMarkupAttribute5.Value);
		}
		global::dfMarkupAttribute dfMarkupAttribute6 = this.findAttribute(new string[]
		{
			"valign",
			"vertical-align"
		});
		if (dfMarkupAttribute6 != null)
		{
			style.VerticalAlign = global::dfMarkupStyle.ParseVerticalAlignment(dfMarkupAttribute6.Value);
		}
		global::dfMarkupAttribute dfMarkupAttribute7 = this.findAttribute(new string[]
		{
			"line-height"
		});
		if (dfMarkupAttribute7 != null)
		{
			style.LineHeight = global::dfMarkupStyle.ParseSize(dfMarkupAttribute7.Value, style.LineHeight);
		}
		global::dfMarkupAttribute dfMarkupAttribute8 = this.findAttribute(new string[]
		{
			"text-decoration"
		});
		if (dfMarkupAttribute8 != null)
		{
			style.TextDecoration = global::dfMarkupStyle.ParseTextDecoration(dfMarkupAttribute8.Value);
		}
		global::dfMarkupAttribute dfMarkupAttribute9 = this.findAttribute(new string[]
		{
			"background",
			"background-color"
		});
		if (dfMarkupAttribute9 != null)
		{
			style.BackgroundColor = global::dfMarkupStyle.ParseColor(dfMarkupAttribute9.Value, Color.clear);
			style.BackgroundColor.a = style.Opacity;
		}
		return style;
	}

	// Token: 0x06004721 RID: 18209 RVA: 0x0010CF04 File Offset: 0x0010B104
	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("[");
		if (this.IsEndTag)
		{
			stringBuilder.Append("/");
		}
		stringBuilder.Append(this.TagName);
		for (int i = 0; i < this.Attributes.Count; i++)
		{
			stringBuilder.Append(" ");
			stringBuilder.Append(this.Attributes[i].ToString());
		}
		if (this.IsClosedTag)
		{
			stringBuilder.Append("/");
		}
		stringBuilder.Append("]");
		if (!this.IsClosedTag)
		{
			for (int j = 0; j < base.ChildNodes.Count; j++)
			{
				stringBuilder.Append(base.ChildNodes[j].ToString());
			}
			stringBuilder.Append("[/");
			stringBuilder.Append(this.TagName);
			stringBuilder.Append("]");
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06004722 RID: 18210 RVA: 0x0010D014 File Offset: 0x0010B214
	protected global::dfMarkupAttribute findAttribute(params string[] names)
	{
		for (int i = 0; i < this.Attributes.Count; i++)
		{
			for (int j = 0; j < names.Length; j++)
			{
				if (this.Attributes[i].Name == names[j])
				{
					return this.Attributes[i];
				}
			}
		}
		return null;
	}

	// Token: 0x0400256E RID: 9582
	private static int ELEMENTID;

	// Token: 0x0400256F RID: 9583
	public List<global::dfMarkupAttribute> Attributes;

	// Token: 0x04002570 RID: 9584
	private global::dfRichTextLabel owner;

	// Token: 0x04002571 RID: 9585
	private string id;
}
