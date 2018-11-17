using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

// Token: 0x02000722 RID: 1826
public struct dfMarkupStyle
{
	// Token: 0x060042C1 RID: 17089 RVA: 0x00103330 File Offset: 0x00101530
	public dfMarkupStyle(dfDynamicFont Font, int FontSize, FontStyle FontStyle)
	{
		this.Host = null;
		this.Atlas = null;
		this.Font = Font;
		this.FontSize = FontSize;
		this.FontStyle = FontStyle;
		this.Align = dfMarkupTextAlign.Left;
		this.VerticalAlign = dfMarkupVerticalAlign.Baseline;
		this.Color = Color.white;
		this.BackgroundColor = Color.clear;
		this.TextDecoration = dfMarkupTextDecoration.None;
		this.PreserveWhitespace = false;
		this.Preformatted = false;
		this.WordSpacing = 0;
		this.CharacterSpacing = 0;
		this.lineHeight = 0;
		this.Opacity = 1f;
	}

	// Token: 0x17000D1C RID: 3356
	// (get) Token: 0x060042C3 RID: 17091 RVA: 0x0010356C File Offset: 0x0010176C
	// (set) Token: 0x060042C4 RID: 17092 RVA: 0x00103598 File Offset: 0x00101798
	public int LineHeight
	{
		get
		{
			if (this.lineHeight == 0)
			{
				return Mathf.CeilToInt((float)this.FontSize);
			}
			return Mathf.Max(this.FontSize, this.lineHeight);
		}
		set
		{
			this.lineHeight = value;
		}
	}

	// Token: 0x060042C5 RID: 17093 RVA: 0x001035A4 File Offset: 0x001017A4
	public static dfMarkupTextDecoration ParseTextDecoration(string value)
	{
		if (value == "underline")
		{
			return dfMarkupTextDecoration.Underline;
		}
		if (value == "overline")
		{
			return dfMarkupTextDecoration.Overline;
		}
		if (value == "line-through")
		{
			return dfMarkupTextDecoration.LineThrough;
		}
		return dfMarkupTextDecoration.None;
	}

	// Token: 0x060042C6 RID: 17094 RVA: 0x001035E0 File Offset: 0x001017E0
	public static dfMarkupVerticalAlign ParseVerticalAlignment(string value)
	{
		if (value == "top")
		{
			return dfMarkupVerticalAlign.Top;
		}
		if (value == "center" || value == "middle")
		{
			return dfMarkupVerticalAlign.Middle;
		}
		if (value == "bottom")
		{
			return dfMarkupVerticalAlign.Bottom;
		}
		return dfMarkupVerticalAlign.Baseline;
	}

	// Token: 0x060042C7 RID: 17095 RVA: 0x00103634 File Offset: 0x00101834
	public static dfMarkupTextAlign ParseTextAlignment(string value)
	{
		if (value == "right")
		{
			return dfMarkupTextAlign.Right;
		}
		if (value == "center")
		{
			return dfMarkupTextAlign.Center;
		}
		if (value == "justify")
		{
			return dfMarkupTextAlign.Justify;
		}
		return dfMarkupTextAlign.Left;
	}

	// Token: 0x060042C8 RID: 17096 RVA: 0x00103670 File Offset: 0x00101870
	public static FontStyle ParseFontStyle(string value, FontStyle baseStyle)
	{
		if (value == "normal")
		{
			return 0;
		}
		if (value == "bold")
		{
			if (baseStyle == null)
			{
				return 1;
			}
			if (baseStyle == 2)
			{
				return 3;
			}
		}
		else if (value == "italic")
		{
			if (baseStyle == null)
			{
				return 2;
			}
			if (baseStyle == 1)
			{
				return 3;
			}
		}
		return baseStyle;
	}

	// Token: 0x060042C9 RID: 17097 RVA: 0x001036D8 File Offset: 0x001018D8
	public static int ParseSize(string value, int baseValue)
	{
		int num;
		if (value.Length > 1 && value.EndsWith("%") && int.TryParse(value.TrimEnd(new char[]
		{
			'%'
		}), out num))
		{
			return (int)((float)baseValue * ((float)num / 100f));
		}
		if (value.EndsWith("px"))
		{
			value = value.Substring(0, value.Length - 2);
		}
		int result;
		if (int.TryParse(value, out result))
		{
			return result;
		}
		return baseValue;
	}

	// Token: 0x060042CA RID: 17098 RVA: 0x0010375C File Offset: 0x0010195C
	public static Color ParseColor(string color, Color defaultColor)
	{
		Color result = defaultColor;
		Color color3;
		if (color.StartsWith("#"))
		{
			uint color2 = 0u;
			if (uint.TryParse(color.Substring(1), NumberStyles.HexNumber, null, out color2))
			{
				result = dfMarkupStyle.UIntToColor(color2);
			}
			else
			{
				result = Color.red;
			}
		}
		else if (dfMarkupStyle.namedColors.TryGetValue(color.ToLowerInvariant(), out color3))
		{
			result = color3;
		}
		return result;
	}

	// Token: 0x060042CB RID: 17099 RVA: 0x001037CC File Offset: 0x001019CC
	private static Color32 UIntToColor(uint color)
	{
		byte b = (byte)(color >> 16);
		byte b2 = (byte)(color >> 8);
		byte b3 = (byte)color;
		return new Color32(b, b2, b3, byte.MaxValue);
	}

	// Token: 0x0400233A RID: 9018
	private static Dictionary<string, Color> namedColors = new Dictionary<string, Color>
	{
		{
			"aqua",
			dfMarkupStyle.UIntToColor(65535u)
		},
		{
			"black",
			Color.black
		},
		{
			"blue",
			Color.blue
		},
		{
			"cyan",
			Color.cyan
		},
		{
			"fuchsia",
			dfMarkupStyle.UIntToColor(16711935u)
		},
		{
			"gray",
			Color.gray
		},
		{
			"green",
			Color.green
		},
		{
			"lime",
			dfMarkupStyle.UIntToColor(65280u)
		},
		{
			"magenta",
			Color.magenta
		},
		{
			"maroon",
			dfMarkupStyle.UIntToColor(8388608u)
		},
		{
			"navy",
			dfMarkupStyle.UIntToColor(128u)
		},
		{
			"olive",
			dfMarkupStyle.UIntToColor(8421376u)
		},
		{
			"orange",
			dfMarkupStyle.UIntToColor(16753920u)
		},
		{
			"purple",
			dfMarkupStyle.UIntToColor(8388736u)
		},
		{
			"red",
			Color.red
		},
		{
			"silver",
			dfMarkupStyle.UIntToColor(12632256u)
		},
		{
			"teal",
			dfMarkupStyle.UIntToColor(32896u)
		},
		{
			"white",
			Color.white
		},
		{
			"yellow",
			Color.yellow
		}
	};

	// Token: 0x0400233B RID: 9019
	public dfRichTextLabel Host;

	// Token: 0x0400233C RID: 9020
	public dfAtlas Atlas;

	// Token: 0x0400233D RID: 9021
	public dfDynamicFont Font;

	// Token: 0x0400233E RID: 9022
	public int FontSize;

	// Token: 0x0400233F RID: 9023
	public FontStyle FontStyle;

	// Token: 0x04002340 RID: 9024
	public dfMarkupTextDecoration TextDecoration;

	// Token: 0x04002341 RID: 9025
	public dfMarkupTextAlign Align;

	// Token: 0x04002342 RID: 9026
	public dfMarkupVerticalAlign VerticalAlign;

	// Token: 0x04002343 RID: 9027
	public Color Color;

	// Token: 0x04002344 RID: 9028
	public Color BackgroundColor;

	// Token: 0x04002345 RID: 9029
	public float Opacity;

	// Token: 0x04002346 RID: 9030
	public bool PreserveWhitespace;

	// Token: 0x04002347 RID: 9031
	public bool Preformatted;

	// Token: 0x04002348 RID: 9032
	public int WordSpacing;

	// Token: 0x04002349 RID: 9033
	public int CharacterSpacing;

	// Token: 0x0400234A RID: 9034
	private int lineHeight;
}
