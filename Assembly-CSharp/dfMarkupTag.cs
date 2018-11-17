using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000723 RID: 1827
public class dfMarkupTag : dfMarkupElement
{
	// Token: 0x060042CC RID: 17100 RVA: 0x001037F4 File Offset: 0x001019F4
	public dfMarkupTag(string tagName)
	{
		this.Attributes = new List<dfMarkupAttribute>();
		this.TagName = tagName;
		this.id = tagName + dfMarkupTag.ELEMENTID++.ToString("X");
	}

	// Token: 0x060042CD RID: 17101 RVA: 0x00103840 File Offset: 0x00101A40
	public dfMarkupTag(dfMarkupTag original)
	{
		this.TagName = original.TagName;
		this.Attributes = original.Attributes;
		this.IsEndTag = original.IsEndTag;
		this.IsClosedTag = original.IsClosedTag;
		this.IsInline = original.IsInline;
		this.id = original.id;
		List<dfMarkupElement> childNodes = original.ChildNodes;
		for (int i = 0; i < childNodes.Count; i++)
		{
			dfMarkupElement node = childNodes[i];
			base.AddChildNode(node);
		}
	}

	// Token: 0x17000D1D RID: 3357
	// (get) Token: 0x060042CF RID: 17103 RVA: 0x001038CC File Offset: 0x00101ACC
	// (set) Token: 0x060042D0 RID: 17104 RVA: 0x001038D4 File Offset: 0x00101AD4
	public string TagName { get; set; }

	// Token: 0x17000D1E RID: 3358
	// (get) Token: 0x060042D1 RID: 17105 RVA: 0x001038E0 File Offset: 0x00101AE0
	public string ID
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x17000D1F RID: 3359
	// (get) Token: 0x060042D2 RID: 17106 RVA: 0x001038E8 File Offset: 0x00101AE8
	// (set) Token: 0x060042D3 RID: 17107 RVA: 0x001038F0 File Offset: 0x00101AF0
	public virtual bool IsEndTag { get; set; }

	// Token: 0x17000D20 RID: 3360
	// (get) Token: 0x060042D4 RID: 17108 RVA: 0x001038FC File Offset: 0x00101AFC
	// (set) Token: 0x060042D5 RID: 17109 RVA: 0x00103904 File Offset: 0x00101B04
	public virtual bool IsClosedTag { get; set; }

	// Token: 0x17000D21 RID: 3361
	// (get) Token: 0x060042D6 RID: 17110 RVA: 0x00103910 File Offset: 0x00101B10
	// (set) Token: 0x060042D7 RID: 17111 RVA: 0x00103918 File Offset: 0x00101B18
	public virtual bool IsInline { get; set; }

	// Token: 0x17000D22 RID: 3362
	// (get) Token: 0x060042D8 RID: 17112 RVA: 0x00103924 File Offset: 0x00101B24
	// (set) Token: 0x060042D9 RID: 17113 RVA: 0x0010392C File Offset: 0x00101B2C
	public dfRichTextLabel Owner
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
				dfMarkupTag dfMarkupTag = base.ChildNodes[i] as dfMarkupTag;
				if (dfMarkupTag != null)
				{
					dfMarkupTag.Owner = value;
				}
			}
		}
	}

	// Token: 0x060042DA RID: 17114 RVA: 0x0010397C File Offset: 0x00101B7C
	internal override void Release()
	{
		base.Release();
	}

	// Token: 0x060042DB RID: 17115 RVA: 0x00103984 File Offset: 0x00101B84
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
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

	// Token: 0x060042DC RID: 17116 RVA: 0x001039CC File Offset: 0x00101BCC
	protected dfMarkupStyle applyTextStyleAttributes(dfMarkupStyle style)
	{
		dfMarkupAttribute dfMarkupAttribute = this.findAttribute(new string[]
		{
			"font",
			"font-family"
		});
		if (dfMarkupAttribute != null)
		{
			style.Font = dfDynamicFont.FindByName(dfMarkupAttribute.Value);
		}
		dfMarkupAttribute dfMarkupAttribute2 = this.findAttribute(new string[]
		{
			"style",
			"font-style"
		});
		if (dfMarkupAttribute2 != null)
		{
			style.FontStyle = dfMarkupStyle.ParseFontStyle(dfMarkupAttribute2.Value, style.FontStyle);
		}
		dfMarkupAttribute dfMarkupAttribute3 = this.findAttribute(new string[]
		{
			"size",
			"font-size"
		});
		if (dfMarkupAttribute3 != null)
		{
			style.FontSize = dfMarkupStyle.ParseSize(dfMarkupAttribute3.Value, style.FontSize);
		}
		dfMarkupAttribute dfMarkupAttribute4 = this.findAttribute(new string[]
		{
			"color"
		});
		if (dfMarkupAttribute4 != null)
		{
			Color color = dfMarkupStyle.ParseColor(dfMarkupAttribute4.Value, style.Color);
			color.a = style.Opacity;
			style.Color = color;
		}
		dfMarkupAttribute dfMarkupAttribute5 = this.findAttribute(new string[]
		{
			"align",
			"text-align"
		});
		if (dfMarkupAttribute5 != null)
		{
			style.Align = dfMarkupStyle.ParseTextAlignment(dfMarkupAttribute5.Value);
		}
		dfMarkupAttribute dfMarkupAttribute6 = this.findAttribute(new string[]
		{
			"valign",
			"vertical-align"
		});
		if (dfMarkupAttribute6 != null)
		{
			style.VerticalAlign = dfMarkupStyle.ParseVerticalAlignment(dfMarkupAttribute6.Value);
		}
		dfMarkupAttribute dfMarkupAttribute7 = this.findAttribute(new string[]
		{
			"line-height"
		});
		if (dfMarkupAttribute7 != null)
		{
			style.LineHeight = dfMarkupStyle.ParseSize(dfMarkupAttribute7.Value, style.LineHeight);
		}
		dfMarkupAttribute dfMarkupAttribute8 = this.findAttribute(new string[]
		{
			"text-decoration"
		});
		if (dfMarkupAttribute8 != null)
		{
			style.TextDecoration = dfMarkupStyle.ParseTextDecoration(dfMarkupAttribute8.Value);
		}
		dfMarkupAttribute dfMarkupAttribute9 = this.findAttribute(new string[]
		{
			"background",
			"background-color"
		});
		if (dfMarkupAttribute9 != null)
		{
			style.BackgroundColor = dfMarkupStyle.ParseColor(dfMarkupAttribute9.Value, Color.clear);
			style.BackgroundColor.a = style.Opacity;
		}
		return style;
	}

	// Token: 0x060042DD RID: 17117 RVA: 0x00103BF4 File Offset: 0x00101DF4
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

	// Token: 0x060042DE RID: 17118 RVA: 0x00103D04 File Offset: 0x00101F04
	protected dfMarkupAttribute findAttribute(params string[] names)
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

	// Token: 0x0400234B RID: 9035
	private static int ELEMENTID;

	// Token: 0x0400234C RID: 9036
	public List<dfMarkupAttribute> Attributes;

	// Token: 0x0400234D RID: 9037
	private dfRichTextLabel owner;

	// Token: 0x0400234E RID: 9038
	private string id;
}
