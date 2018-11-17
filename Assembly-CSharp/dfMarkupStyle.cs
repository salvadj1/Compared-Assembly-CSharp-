using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

// Token: 0x020007FE RID: 2046
public struct dfMarkupStyle
{
	// Token: 0x06004705 RID: 18181 RVA: 0x0010C640 File Offset: 0x0010A840
	public dfMarkupStyle(global::dfDynamicFont Font, int FontSize, FontStyle FontStyle)
	{
		this.Host = null;
		this.Atlas = null;
		this.Font = Font;
		this.FontSize = FontSize;
		this.FontStyle = FontStyle;
		this.Align = global::dfMarkupTextAlign.Left;
		this.VerticalAlign = global::dfMarkupVerticalAlign.Baseline;
		this.Color = Color.white;
		this.BackgroundColor = Color.clear;
		this.TextDecoration = global::dfMarkupTextDecoration.None;
		this.PreserveWhitespace = false;
		this.Preformatted = false;
		this.WordSpacing = 0;
		this.CharacterSpacing = 0;
		this.lineHeight = 0;
		this.Opacity = 1f;
	}

	// Token: 0x17000DA6 RID: 3494
	// (get) Token: 0x06004707 RID: 18183 RVA: 0x0010C87C File Offset: 0x0010AA7C
	// (set) Token: 0x06004708 RID: 18184 RVA: 0x0010C8A8 File Offset: 0x0010AAA8
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

	// Token: 0x06004709 RID: 18185 RVA: 0x0010C8B4 File Offset: 0x0010AAB4
	public static global::dfMarkupTextDecoration ParseTextDecoration(string value)
	{
		if (value == "underline")
		{
			return global::dfMarkupTextDecoration.Underline;
		}
		if (value == "overline")
		{
			return global::dfMarkupTextDecoration.Overline;
		}
		if (value == "line-through")
		{
			return global::dfMarkupTextDecoration.LineThrough;
		}
		return global::dfMarkupTextDecoration.None;
	}

	// Token: 0x0600470A RID: 18186 RVA: 0x0010C8F0 File Offset: 0x0010AAF0
	public static global::dfMarkupVerticalAlign ParseVerticalAlignment(string value)
	{
		if (value == "top")
		{
			return global::dfMarkupVerticalAlign.Top;
		}
		if (value == "center" || value == "middle")
		{
			return global::dfMarkupVerticalAlign.Middle;
		}
		if (value == "bottom")
		{
			return global::dfMarkupVerticalAlign.Bottom;
		}
		return global::dfMarkupVerticalAlign.Baseline;
	}

	// Token: 0x0600470B RID: 18187 RVA: 0x0010C944 File Offset: 0x0010AB44
	public static global::dfMarkupTextAlign ParseTextAlignment(string value)
	{
		if (value == "right")
		{
			return global::dfMarkupTextAlign.Right;
		}
		if (value == "center")
		{
			return global::dfMarkupTextAlign.Center;
		}
		if (value == "justify")
		{
			return global::dfMarkupTextAlign.Justify;
		}
		return global::dfMarkupTextAlign.Left;
	}

	// Token: 0x0600470C RID: 18188 RVA: 0x0010C980 File Offset: 0x0010AB80
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

	// Token: 0x0600470D RID: 18189 RVA: 0x0010C9E8 File Offset: 0x0010ABE8
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

	// Token: 0x0600470E RID: 18190 RVA: 0x0010CA6C File Offset: 0x0010AC6C
	public static Color ParseColor(string color, Color defaultColor)
	{
		Color result = defaultColor;
		Color color3;
		if (color.StartsWith("#"))
		{
			uint color2 = 0u;
			if (uint.TryParse(color.Substring(1), NumberStyles.HexNumber, null, out color2))
			{
				result = global::dfMarkupStyle.UIntToColor(color2);
			}
			else
			{
				result = Color.red;
			}
		}
		else if (global::dfMarkupStyle.namedColors.TryGetValue(color.ToLowerInvariant(), out color3))
		{
			result = color3;
		}
		return result;
	}

	// Token: 0x0600470F RID: 18191 RVA: 0x0010CADC File Offset: 0x0010ACDC
	private static Color32 UIntToColor(uint color)
	{
		byte b = (byte)(color >> 16);
		byte b2 = (byte)(color >> 8);
		byte b3 = (byte)color;
		return new Color32(b, b2, b3, byte.MaxValue);
	}

	// Token: 0x0400255D RID: 9565
	private static Dictionary<string, Color> namedColors = new Dictionary<string, Color>
	{
		{
			"aqua",
			global::dfMarkupStyle.UIntToColor(65535u)
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
			global::dfMarkupStyle.UIntToColor(16711935u)
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
			global::dfMarkupStyle.UIntToColor(65280u)
		},
		{
			"magenta",
			Color.magenta
		},
		{
			"maroon",
			global::dfMarkupStyle.UIntToColor(8388608u)
		},
		{
			"navy",
			global::dfMarkupStyle.UIntToColor(128u)
		},
		{
			"olive",
			global::dfMarkupStyle.UIntToColor(8421376u)
		},
		{
			"orange",
			global::dfMarkupStyle.UIntToColor(16753920u)
		},
		{
			"purple",
			global::dfMarkupStyle.UIntToColor(8388736u)
		},
		{
			"red",
			Color.red
		},
		{
			"silver",
			global::dfMarkupStyle.UIntToColor(12632256u)
		},
		{
			"teal",
			global::dfMarkupStyle.UIntToColor(32896u)
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

	// Token: 0x0400255E RID: 9566
	public global::dfRichTextLabel Host;

	// Token: 0x0400255F RID: 9567
	public global::dfAtlas Atlas;

	// Token: 0x04002560 RID: 9568
	public global::dfDynamicFont Font;

	// Token: 0x04002561 RID: 9569
	public int FontSize;

	// Token: 0x04002562 RID: 9570
	public FontStyle FontStyle;

	// Token: 0x04002563 RID: 9571
	public global::dfMarkupTextDecoration TextDecoration;

	// Token: 0x04002564 RID: 9572
	public global::dfMarkupTextAlign Align;

	// Token: 0x04002565 RID: 9573
	public global::dfMarkupVerticalAlign VerticalAlign;

	// Token: 0x04002566 RID: 9574
	public Color Color;

	// Token: 0x04002567 RID: 9575
	public Color BackgroundColor;

	// Token: 0x04002568 RID: 9576
	public float Opacity;

	// Token: 0x04002569 RID: 9577
	public bool PreserveWhitespace;

	// Token: 0x0400256A RID: 9578
	public bool Preformatted;

	// Token: 0x0400256B RID: 9579
	public int WordSpacing;

	// Token: 0x0400256C RID: 9580
	public int CharacterSpacing;

	// Token: 0x0400256D RID: 9581
	private int lineHeight;
}
