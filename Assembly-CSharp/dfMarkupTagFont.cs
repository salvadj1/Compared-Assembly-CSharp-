using System;
using UnityEngine;

// Token: 0x0200072F RID: 1839
[dfMarkupTagInfo("font")]
public class dfMarkupTagFont : dfMarkupTag
{
	// Token: 0x0600430A RID: 17162 RVA: 0x00104B2C File Offset: 0x00102D2C
	public dfMarkupTagFont() : base("font")
	{
	}

	// Token: 0x0600430B RID: 17163 RVA: 0x00104B3C File Offset: 0x00102D3C
	public dfMarkupTagFont(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x0600430C RID: 17164 RVA: 0x00104B48 File Offset: 0x00102D48
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
		{
			"name",
			"face"
		});
		if (dfMarkupAttribute != null)
		{
			style.Font = (dfDynamicFont.FindByName(dfMarkupAttribute.Value) ?? style.Font);
		}
		dfMarkupAttribute dfMarkupAttribute2 = base.findAttribute(new string[]
		{
			"size",
			"font-size"
		});
		if (dfMarkupAttribute2 != null)
		{
			style.FontSize = dfMarkupStyle.ParseSize(dfMarkupAttribute2.Value, style.FontSize);
		}
		dfMarkupAttribute dfMarkupAttribute3 = base.findAttribute(new string[]
		{
			"color"
		});
		if (dfMarkupAttribute3 != null)
		{
			style.Color = dfMarkupStyle.ParseColor(dfMarkupAttribute3.Value, Color.red);
			style.Color.a = style.Opacity;
		}
		dfMarkupAttribute dfMarkupAttribute4 = base.findAttribute(new string[]
		{
			"style"
		});
		if (dfMarkupAttribute4 != null)
		{
			style.FontStyle = dfMarkupStyle.ParseFontStyle(dfMarkupAttribute4.Value, style.FontStyle);
		}
		base._PerformLayoutImpl(container, style);
	}
}
