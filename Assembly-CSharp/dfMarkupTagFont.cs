using System;
using UnityEngine;

// Token: 0x0200080B RID: 2059
[global::dfMarkupTagInfo("font")]
public class dfMarkupTagFont : global::dfMarkupTag
{
	// Token: 0x0600474E RID: 18254 RVA: 0x0010DE3C File Offset: 0x0010C03C
	public dfMarkupTagFont() : base("font")
	{
	}

	// Token: 0x0600474F RID: 18255 RVA: 0x0010DE4C File Offset: 0x0010C04C
	public dfMarkupTagFont(global::dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x06004750 RID: 18256 RVA: 0x0010DE58 File Offset: 0x0010C058
	protected override void _PerformLayoutImpl(global::dfMarkupBox container, global::dfMarkupStyle style)
	{
		global::dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
		{
			"name",
			"face"
		});
		if (dfMarkupAttribute != null)
		{
			style.Font = (global::dfDynamicFont.FindByName(dfMarkupAttribute.Value) ?? style.Font);
		}
		global::dfMarkupAttribute dfMarkupAttribute2 = base.findAttribute(new string[]
		{
			"size",
			"font-size"
		});
		if (dfMarkupAttribute2 != null)
		{
			style.FontSize = global::dfMarkupStyle.ParseSize(dfMarkupAttribute2.Value, style.FontSize);
		}
		global::dfMarkupAttribute dfMarkupAttribute3 = base.findAttribute(new string[]
		{
			"color"
		});
		if (dfMarkupAttribute3 != null)
		{
			style.Color = global::dfMarkupStyle.ParseColor(dfMarkupAttribute3.Value, Color.red);
			style.Color.a = style.Opacity;
		}
		global::dfMarkupAttribute dfMarkupAttribute4 = base.findAttribute(new string[]
		{
			"style"
		});
		if (dfMarkupAttribute4 != null)
		{
			style.FontStyle = global::dfMarkupStyle.ParseFontStyle(dfMarkupAttribute4.Value, style.FontStyle);
		}
		base._PerformLayoutImpl(container, style);
	}
}
