using System;
using System.Collections.Generic;
using System.Text;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x020008DC RID: 2268
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Font")]
public class UIFont : MonoBehaviour
{
	// Token: 0x17000EAE RID: 3758
	// (get) Token: 0x06004D1D RID: 19741 RVA: 0x001302A0 File Offset: 0x0012E4A0
	public global::BMFont bmFont
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mFont : this.mReplacement.bmFont;
		}
	}

	// Token: 0x17000EAF RID: 3759
	// (get) Token: 0x06004D1E RID: 19742 RVA: 0x001302CC File Offset: 0x0012E4CC
	public int texWidth
	{
		get
		{
			return (!(this.mReplacement != null)) ? ((this.mFont == null) ? 1 : this.mFont.texWidth) : this.mReplacement.texWidth;
		}
	}

	// Token: 0x17000EB0 RID: 3760
	// (get) Token: 0x06004D1F RID: 19743 RVA: 0x0013030C File Offset: 0x0012E50C
	public int texHeight
	{
		get
		{
			return (!(this.mReplacement != null)) ? ((this.mFont == null) ? 1 : this.mFont.texHeight) : this.mReplacement.texHeight;
		}
	}

	// Token: 0x17000EB1 RID: 3761
	// (get) Token: 0x06004D20 RID: 19744 RVA: 0x0013034C File Offset: 0x0012E54C
	// (set) Token: 0x06004D21 RID: 19745 RVA: 0x00130378 File Offset: 0x0012E578
	public global::UIAtlas atlas
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mAtlas : this.mReplacement.atlas;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.atlas = value;
			}
			else if (this.mAtlas != value)
			{
				if (value == null)
				{
					if (this.mAtlas != null)
					{
						this.mMat = this.mAtlas.spriteMaterial;
					}
					if (this.sprite != null)
					{
						this.mUVRect = this.uvRect;
					}
				}
				this.mAtlas = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000EB2 RID: 3762
	// (get) Token: 0x06004D22 RID: 19746 RVA: 0x0013040C File Offset: 0x0012E60C
	// (set) Token: 0x06004D23 RID: 19747 RVA: 0x00130460 File Offset: 0x0012E660
	public Material material
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.material;
			}
			return (!(this.mAtlas != null)) ? this.mMat : this.mAtlas.spriteMaterial;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.material = value;
			}
			else if (this.mAtlas == null && this.mMat != value)
			{
				this.mMat = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000EB3 RID: 3763
	// (get) Token: 0x06004D24 RID: 19748 RVA: 0x001304C0 File Offset: 0x0012E6C0
	public Texture2D texture
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.texture;
			}
			Material material = this.material;
			return (!(material != null)) ? null : (material.mainTexture as Texture2D);
		}
	}

	// Token: 0x17000EB4 RID: 3764
	// (get) Token: 0x06004D25 RID: 19749 RVA: 0x00130510 File Offset: 0x0012E710
	// (set) Token: 0x06004D26 RID: 19750 RVA: 0x00130684 File Offset: 0x0012E884
	public Rect uvRect
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.uvRect;
			}
			if (this.mAtlas != null && this.mSprite == null && this.sprite != null)
			{
				Texture texture = this.mAtlas.texture;
				if (texture != null)
				{
					this.mUVRect = this.mSprite.outer;
					if (this.mAtlas.coordinates == global::UIAtlas.Coordinates.Pixels)
					{
						this.mUVRect = global::NGUIMath.ConvertToTexCoords(this.mUVRect, texture.width, texture.height);
					}
					if (this.mSprite.hasPadding)
					{
						Rect rect = this.mUVRect;
						this.mUVRect.xMin = rect.xMin - this.mSprite.paddingLeft * rect.width;
						this.mUVRect.yMin = rect.yMin - this.mSprite.paddingBottom * rect.height;
						this.mUVRect.xMax = rect.xMax + this.mSprite.paddingRight * rect.width;
						this.mUVRect.yMax = rect.yMax + this.mSprite.paddingTop * rect.height;
					}
					if (this.mSprite.hasPadding)
					{
						this.Trim();
					}
				}
			}
			return this.mUVRect;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.uvRect = value;
			}
			else if (this.sprite == null && this.mUVRect != value)
			{
				this.mUVRect = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000EB5 RID: 3765
	// (get) Token: 0x06004D27 RID: 19751 RVA: 0x001306DC File Offset: 0x0012E8DC
	// (set) Token: 0x06004D28 RID: 19752 RVA: 0x00130718 File Offset: 0x0012E918
	public string spriteName
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mFont.spriteName : this.mReplacement.spriteName;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.spriteName = value;
			}
			else if (this.mFont.spriteName != value)
			{
				this.mFont.spriteName = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000EB6 RID: 3766
	// (get) Token: 0x06004D29 RID: 19753 RVA: 0x00130770 File Offset: 0x0012E970
	// (set) Token: 0x06004D2A RID: 19754 RVA: 0x0013079C File Offset: 0x0012E99C
	public int horizontalSpacing
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mSpacingX : this.mReplacement.horizontalSpacing;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.horizontalSpacing = value;
			}
			else if (this.mSpacingX != value)
			{
				this.mSpacingX = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000EB7 RID: 3767
	// (get) Token: 0x06004D2B RID: 19755 RVA: 0x001307DC File Offset: 0x0012E9DC
	// (set) Token: 0x06004D2C RID: 19756 RVA: 0x00130808 File Offset: 0x0012EA08
	public int verticalSpacing
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mSpacingY : this.mReplacement.verticalSpacing;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.verticalSpacing = value;
			}
			else if (this.mSpacingY != value)
			{
				this.mSpacingY = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000EB8 RID: 3768
	// (get) Token: 0x06004D2D RID: 19757 RVA: 0x00130848 File Offset: 0x0012EA48
	public int size
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mFont.charSize : this.mReplacement.size;
		}
	}

	// Token: 0x17000EB9 RID: 3769
	// (get) Token: 0x06004D2E RID: 19758 RVA: 0x00130884 File Offset: 0x0012EA84
	public global::UIAtlas.Sprite sprite
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.sprite;
			}
			if (!this.mSpriteSet)
			{
				this.mSprite = null;
			}
			if (this.mSprite == null && this.mAtlas != null && !string.IsNullOrEmpty(this.mFont.spriteName))
			{
				this.mSprite = this.mAtlas.GetSprite(this.mFont.spriteName);
				if (this.mSprite == null)
				{
					this.mSprite = this.mAtlas.GetSprite(base.name);
				}
				this.mSpriteSet = true;
				if (this.mSprite == null)
				{
					Debug.LogError("Can't find the sprite '" + this.mFont.spriteName + "' in UIAtlas on " + global::NGUITools.GetHierarchy(this.mAtlas.gameObject));
					this.mFont.spriteName = null;
				}
			}
			return this.mSprite;
		}
	}

	// Token: 0x17000EBA RID: 3770
	// (get) Token: 0x06004D2F RID: 19759 RVA: 0x00130984 File Offset: 0x0012EB84
	// (set) Token: 0x06004D30 RID: 19760 RVA: 0x0013098C File Offset: 0x0012EB8C
	public global::UIFont replacement
	{
		get
		{
			return this.mReplacement;
		}
		set
		{
			global::UIFont uifont = value;
			if (uifont == this)
			{
				uifont = null;
			}
			if (this.mReplacement != uifont)
			{
				if (uifont != null && uifont.replacement == this)
				{
					uifont.replacement = null;
				}
				if (this.mReplacement != null)
				{
					this.MarkAsDirty();
				}
				this.mReplacement = uifont;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x06004D31 RID: 19761 RVA: 0x00130A04 File Offset: 0x0012EC04
	private void Trim()
	{
		Texture texture = this.mAtlas.texture;
		if (texture != null && this.mSprite != null)
		{
			Rect rect = global::NGUIMath.ConvertToPixels(this.mUVRect, this.texture.width, this.texture.height, true);
			Rect rect2 = (this.mAtlas.coordinates != global::UIAtlas.Coordinates.TexCoords) ? this.mSprite.outer : global::NGUIMath.ConvertToPixels(this.mSprite.outer, texture.width, texture.height, true);
			int xMin = Mathf.RoundToInt(rect2.xMin - rect.xMin);
			int yMin = Mathf.RoundToInt(rect2.yMin - rect.yMin);
			int xMax = Mathf.RoundToInt(rect2.xMax - rect.xMin);
			int yMax = Mathf.RoundToInt(rect2.yMax - rect.yMin);
			this.mFont.Trim(xMin, yMin, xMax, yMax);
		}
	}

	// Token: 0x06004D32 RID: 19762 RVA: 0x00130B00 File Offset: 0x0012ED00
	private bool References(global::UIFont font)
	{
		return !(font == null) && (font == this || (this.mReplacement != null && this.mReplacement.References(font)));
	}

	// Token: 0x06004D33 RID: 19763 RVA: 0x00130B4C File Offset: 0x0012ED4C
	public static bool CheckIfRelated(global::UIFont a, global::UIFont b)
	{
		return !(a == null) && !(b == null) && (a == b || a.References(b) || b.References(a));
	}

	// Token: 0x06004D34 RID: 19764 RVA: 0x00130B98 File Offset: 0x0012ED98
	public void MarkAsDirty()
	{
		this.mSprite = null;
		global::UILabel[] array = global::NGUITools.FindActive<global::UILabel>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			global::UILabel uilabel = array[i];
			if (uilabel.enabled && uilabel.gameObject.activeInHierarchy && global::UIFont.CheckIfRelated(this, uilabel.font))
			{
				global::UIFont font = uilabel.font;
				uilabel.font = null;
				uilabel.font = font;
			}
			i++;
		}
	}

	// Token: 0x06004D35 RID: 19765 RVA: 0x00130C10 File Offset: 0x0012EE10
	public Vector2 CalculatePrintedSize(string text, bool encoding, global::UIFont.SymbolStyle symbolStyle)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.CalculatePrintedSize(text, encoding, symbolStyle);
		}
		Vector2 zero = Vector2.zero;
		if (this.mFont != null && this.mFont.isValid && !string.IsNullOrEmpty(text))
		{
			if (encoding)
			{
				text = global::NGUITools.StripSymbols(text);
			}
			int length = text.Length;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = this.mFont.charSize + this.mSpacingY;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				global::BMSymbol bmsymbol;
				global::BMGlyph bmglyph;
				if (c == '\n')
				{
					if (num2 > num)
					{
						num = num2;
					}
					num2 = 0;
					num3 += num5;
					num4 = 0;
				}
				else if (c < ' ')
				{
					num4 = 0;
				}
				else if (this.mFont.MatchSymbol(text, i, length, out bmsymbol))
				{
					num2 += this.mSpacingX + bmsymbol.width;
					i += bmsymbol.sequence.Length - 1;
					num4 = 0;
				}
				else if (this.mFont.GetGlyph((int)c, out bmglyph))
				{
					num2 += this.mSpacingX + ((num4 == 0) ? bmglyph.advance : (bmglyph.advance + bmglyph.GetKerning(num4)));
					num4 = (int)c;
				}
			}
			float num6 = (this.mFont.charSize <= 0) ? 1f : (1f / (float)this.mFont.charSize);
			zero.x = num6 * (float)((num2 <= num) ? num : num2);
			zero.y = num6 * (float)(num3 + num5);
		}
		return zero;
	}

	// Token: 0x06004D36 RID: 19766 RVA: 0x00130DDC File Offset: 0x0012EFDC
	private static global::UITextMod EndLine(ref StringBuilder s)
	{
		int num = s.Length - 1;
		if (num > 0 && s[num] == ' ')
		{
			s[num] = '\n';
			return global::UITextMod.Replaced;
		}
		s.Append('\n');
		return global::UITextMod.Added;
	}

	// Token: 0x17000EBB RID: 3771
	// (get) Token: 0x06004D37 RID: 19767 RVA: 0x00130E20 File Offset: 0x0012F020
	public static List<global::UITextMarkup> tempMarkup
	{
		get
		{
			List<global::UITextMarkup> result;
			if ((result = global::UIFont._tempMarkup) == null)
			{
				result = (global::UIFont._tempMarkup = new List<global::UITextMarkup>());
			}
			return result;
		}
	}

	// Token: 0x06004D38 RID: 19768 RVA: 0x00130E3C File Offset: 0x0012F03C
	public string WrapText(List<global::UITextMarkup> markups, string text, float maxWidth, int maxLineCount, bool encoding, global::UIFont.SymbolStyle symbolStyle)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.WrapText(markups, text, maxWidth, maxLineCount, encoding, symbolStyle);
		}
		markups = (markups ?? global::UIFont.tempMarkup);
		markups.Clear();
		int num = Mathf.RoundToInt(maxWidth * (float)this.size);
		if (num < 1)
		{
			return text;
		}
		StringBuilder stringBuilder = new StringBuilder();
		int length = text.Length;
		int num2 = num;
		int num3 = 0;
		int num4 = 0;
		int i = 0;
		bool flag = true;
		bool flag2 = maxLineCount != 1;
		int num5 = 1;
		global::BMSymbol bmsymbol = null;
		int num6 = 0;
		while (i < length)
		{
			char c = text[i];
			if (num3 == 92 && c == 'n')
			{
				if (num4 < i - 1)
				{
					stringBuilder.Append(text.Substring(num4, i - (num4 + 2)));
				}
				else
				{
					stringBuilder.Append(c);
				}
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
				markups.Add(new global::UITextMarkup
				{
					index = i - 1,
					mod = global::UITextMod.Removed
				});
				markups.Add(new global::UITextMarkup
				{
					index = i,
					mod = global::UITextMod.Replaced,
					value = '\n'
				});
				num4 = i + 1;
				c = '\n';
			}
			if (c == '\n')
			{
				if (!flag2 || num5 == maxLineCount)
				{
					markups.Add(new global::UITextMarkup
					{
						index = i
					});
					break;
				}
				num2 = num;
				if (num4 < i)
				{
					stringBuilder.Append(text.Substring(num4, i - num4 + 1));
				}
				else
				{
					stringBuilder.Append(c);
				}
				flag = true;
				num5++;
				num4 = i + 1;
				num3 = 0;
			}
			else
			{
				if (c == ' ' && num3 != 32 && num4 < i)
				{
					stringBuilder.Append(text.Substring(num4, i - num4 + 1));
					flag = false;
					num4 = i + 1;
					num3 = (int)c;
				}
				if (encoding && c == '[' && i + 2 < length)
				{
					if (text[i + 2] == ']')
					{
						if (text[i + 1] == '-')
						{
							if (num6 == 0)
							{
								markups.Add(new global::UITextMarkup
								{
									index = i,
									mod = global::UITextMod.Removed
								});
								markups.Add(new global::UITextMarkup
								{
									index = i + 1,
									mod = global::UITextMod.Removed
								});
								markups.Add(new global::UITextMarkup
								{
									index = i + 2,
									mod = global::UITextMod.Removed
								});
								i += 2;
								goto IL_8B0;
							}
						}
						else if (text[i + 1] == '»')
						{
							if (num6++ == 0)
							{
								markups.Add(new global::UITextMarkup
								{
									index = i,
									mod = global::UITextMod.Removed
								});
								markups.Add(new global::UITextMarkup
								{
									index = i + 1,
									mod = global::UITextMod.Removed
								});
								markups.Add(new global::UITextMarkup
								{
									index = i + 2,
									mod = global::UITextMod.Removed
								});
								i += 2;
								goto IL_8B0;
							}
						}
						else if (text[i + 1] == '«' && --num6 == 0)
						{
							markups.Add(new global::UITextMarkup
							{
								index = i,
								mod = global::UITextMod.Removed
							});
							markups.Add(new global::UITextMarkup
							{
								index = i + 1,
								mod = global::UITextMod.Removed
							});
							markups.Add(new global::UITextMarkup
							{
								index = i + 2,
								mod = global::UITextMod.Removed
							});
							i += 2;
							goto IL_8B0;
						}
					}
					else if (i + 7 < length && text[i + 7] == ']' && num6 == 0)
					{
						markups.Add(new global::UITextMarkup
						{
							index = i,
							mod = global::UITextMod.Removed
						});
						markups.Add(new global::UITextMarkup
						{
							index = i + 1,
							mod = global::UITextMod.Removed
						});
						markups.Add(new global::UITextMarkup
						{
							index = i + 2,
							mod = global::UITextMod.Removed
						});
						markups.Add(new global::UITextMarkup
						{
							index = i + 3,
							mod = global::UITextMod.Removed
						});
						markups.Add(new global::UITextMarkup
						{
							index = i + 4,
							mod = global::UITextMod.Removed
						});
						markups.Add(new global::UITextMarkup
						{
							index = i + 5,
							mod = global::UITextMod.Removed
						});
						markups.Add(new global::UITextMarkup
						{
							index = i + 6,
							mod = global::UITextMod.Removed
						});
						markups.Add(new global::UITextMarkup
						{
							index = i + 7,
							mod = global::UITextMod.Removed
						});
						i += 7;
						goto IL_8B0;
					}
				}
				bool flag3 = encoding && symbolStyle != global::UIFont.SymbolStyle.None && this.mFont.MatchSymbol(text, i, length, out bmsymbol);
				int num7;
				if (flag3)
				{
					num7 = this.mSpacingX + bmsymbol.width;
				}
				else
				{
					global::BMGlyph bmglyph;
					if (!this.mFont.GetGlyph((int)c, out bmglyph))
					{
						markups.Add(new global::UITextMarkup
						{
							index = i,
							mod = global::UITextMod.Removed
						});
						goto IL_8B0;
					}
					num7 = this.mSpacingX + ((num3 == 0) ? bmglyph.advance : (bmglyph.advance + bmglyph.GetKerning(num3)));
				}
				num2 -= num7;
				if (num2 < 0)
				{
					if (flag || !flag2 || num5 == maxLineCount)
					{
						stringBuilder.Append(text.Substring(num4, Mathf.Max(0, i - num4)));
						if (!flag2 || num5 == maxLineCount)
						{
							num4 = i;
							markups.Add(new global::UITextMarkup
							{
								index = i
							});
							break;
						}
						global::UITextMod uitextMod = global::UIFont.EndLine(ref stringBuilder);
						if (uitextMod != global::UITextMod.Replaced)
						{
							if (uitextMod == global::UITextMod.Added)
							{
								markups.Add(new global::UITextMarkup
								{
									index = i,
									mod = global::UITextMod.Added
								});
							}
						}
						else
						{
							markups.Add(new global::UITextMarkup
							{
								index = i,
								mod = global::UITextMod.Replaced,
								value = '\n'
							});
						}
						flag = true;
						num5++;
						if (c == ' ')
						{
							num4 = i + 1;
							num2 = num;
							markups.Add(new global::UITextMarkup
							{
								index = i,
								mod = global::UITextMod.Removed
							});
						}
						else
						{
							num4 = i;
							num2 = num - num7;
						}
						num3 = 0;
					}
					else
					{
						while (num4 < length && text[num4] == ' ')
						{
							markups.Add(new global::UITextMarkup
							{
								index = num4,
								mod = global::UITextMod.Removed
							});
							num4++;
						}
						flag = true;
						num2 = num;
						i = num4 - 1;
						int count = markups.Count;
						for (int j = count - 1; j >= 0; j--)
						{
							if (markups[j].index < i)
							{
								break;
							}
							markups.RemoveAt(j);
						}
						num3 = 0;
						if (!flag2 || num5 == maxLineCount)
						{
							markups.Add(new global::UITextMarkup
							{
								index = i
							});
							break;
						}
						num5++;
						global::UITextMod uitextMod = global::UIFont.EndLine(ref stringBuilder);
						if (uitextMod != global::UITextMod.Replaced)
						{
							if (uitextMod == global::UITextMod.Added)
							{
								markups.Add(new global::UITextMarkup
								{
									index = i,
									mod = global::UITextMod.Added
								});
							}
						}
						else
						{
							markups.Add(new global::UITextMarkup
							{
								index = i,
								mod = global::UITextMod.Replaced,
								value = '\n'
							});
						}
						goto IL_8B0;
					}
				}
				else
				{
					num3 = (int)c;
				}
				if (flag3)
				{
					for (int k = 0; k < bmsymbol.sequence.Length; k++)
					{
						markups.Add(new global::UITextMarkup
						{
							index = i + k,
							mod = global::UITextMod.Removed
						});
					}
					i += bmsymbol.sequence.Length - 1;
					num3 = 0;
				}
			}
			IL_8B0:
			i++;
		}
		if (num4 < i)
		{
			stringBuilder.Append(text.Substring(num4, i - num4));
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06004D39 RID: 19769 RVA: 0x0013172C File Offset: 0x0012F92C
	public string WrapText(List<global::UITextMarkup> markups, string text, float maxWidth, int maxLineCount, bool encoding)
	{
		return this.WrapText(markups, text, maxWidth, maxLineCount, encoding, global::UIFont.SymbolStyle.None);
	}

	// Token: 0x06004D3A RID: 19770 RVA: 0x0013173C File Offset: 0x0012F93C
	public string WrapText(List<global::UITextMarkup> markups, string text, float maxWidth, int maxLineCount)
	{
		return this.WrapText(markups, text, maxWidth, maxLineCount, false, global::UIFont.SymbolStyle.None);
	}

	// Token: 0x06004D3B RID: 19771 RVA: 0x0013174C File Offset: 0x0012F94C
	private void MangleSort(int len)
	{
		global::UIFont.mangleSort.SetLineSizing((double)this.bmFont.charSize, (double)this.verticalSpacing);
		Array.Sort<Vector3, int>(global::UIFont.manglePoints, global::UIFont.mangleIndices, 0, len, global::UIFont.mangleSort);
	}

	// Token: 0x06004D3C RID: 19772 RVA: 0x0013178C File Offset: 0x0012F98C
	private int FillMangle(Vector2[] points, int pointsOffset, global::UITextPosition[] positions, int positionsOffset, int len)
	{
		if (positions == null || points == null)
		{
			return 0;
		}
		if (points.Length - pointsOffset < len || positions.Length - positionsOffset < len)
		{
			throw new ArgumentOutOfRangeException();
		}
		if (len > global::UIFont.mangleIndices.Length)
		{
			Array.Resize<Vector3>(ref global::UIFont.manglePoints, len);
			Array.Resize<int>(ref global::UIFont.mangleIndices, len);
			Array.Resize<global::UITextPosition>(ref global::UIFont.manglePositions, len);
		}
		for (int i = 0; i < len; i++)
		{
			global::UIFont.manglePoints[i].x = points[i + pointsOffset].x;
			global::UIFont.manglePoints[i].y = points[i + pointsOffset].y;
			global::UIFont.manglePoints[i].z = 0f;
			global::UIFont.mangleIndices[i] = i;
		}
		return len;
	}

	// Token: 0x06004D3D RID: 19773 RVA: 0x00131868 File Offset: 0x0012FA68
	private int FillMangle(Vector3[] points, int pointsOffset, global::UITextPosition[] positions, int positionsOffset, int len)
	{
		if (points == null)
		{
			throw new ArgumentNullException("null array", "points");
		}
		if (points.Length - pointsOffset < len)
		{
			throw new ArgumentException("not large enough", "points");
		}
		if (positions != null && positions.Length - positionsOffset < len)
		{
			throw new ArgumentException("not large enough", "positions");
		}
		if (len > global::UIFont.mangleIndices.Length)
		{
			Array.Resize<Vector3>(ref global::UIFont.manglePoints, len);
			Array.Resize<int>(ref global::UIFont.mangleIndices, len);
			Array.Resize<global::UITextPosition>(ref global::UIFont.manglePositions, len);
		}
		for (int i = 0; i < len; i++)
		{
			global::UIFont.manglePoints[i] = points[i + pointsOffset];
			global::UIFont.mangleIndices[i] = i;
		}
		return len;
	}

	// Token: 0x06004D3E RID: 19774 RVA: 0x00131938 File Offset: 0x0012FB38
	private int ProcessShared(int len, ref global::UITextPosition[] positions, string text)
	{
		if (this.mFont.charSize > 0)
		{
			for (int i = 0; i < len; i++)
			{
				Vector3[] array = global::UIFont.manglePoints;
				int num = i;
				array[num].x = array[num].x * (float)this.mFont.charSize;
				Vector3[] array2 = global::UIFont.manglePoints;
				int num2 = i;
				array2[num2].y = array2[num2].y * (float)this.mFont.charSize;
			}
		}
		this.MangleSort(len);
		len = this.ProcessPlacement(len, text);
		if (len > 0)
		{
			if (positions == null)
			{
				positions = new global::UITextPosition[len];
			}
			for (int j = 0; j < len; j++)
			{
				positions[global::UIFont.mangleIndices[j]] = global::UIFont.manglePositions[j];
			}
		}
		return len;
	}

	// Token: 0x06004D3F RID: 19775 RVA: 0x00131A0C File Offset: 0x0012FC0C
	[Obsolete("You must specify some point", true)]
	public global::UITextPosition[] CalculatePlacement(string text)
	{
		return global::UIFont.empty;
	}

	// Token: 0x06004D40 RID: 19776 RVA: 0x00131A14 File Offset: 0x0012FC14
	private int CalculatePlacement(Vector2[] points, ref global::UITextPosition[] positions, string text)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.CalculatePlacement(points, positions, text);
		}
		int num = this.FillMangle(points, 0, positions, 0, Mathf.Min(points.Length, positions.Length));
		if (num > 0)
		{
			return this.ProcessShared(num, ref positions, text);
		}
		return num;
	}

	// Token: 0x06004D41 RID: 19777 RVA: 0x00131A6C File Offset: 0x0012FC6C
	public int CalculatePlacement(Vector2[] points, global::UITextPosition[] positions, string text)
	{
		if (positions == null)
		{
			throw new ArgumentNullException("positions");
		}
		return this.CalculatePlacement(points, ref positions, text, base.transform);
	}

	// Token: 0x06004D42 RID: 19778 RVA: 0x00131A90 File Offset: 0x0012FC90
	public global::UITextPosition CalculatePlacement(string text, Vector2 point)
	{
		Vector2[] points = new Vector2[]
		{
			point
		};
		global::UITextPosition[] array = new global::UITextPosition[]
		{
			default(global::UITextPosition)
		};
		this.CalculatePlacement(points, array, text);
		return array[0];
	}

	// Token: 0x06004D43 RID: 19779 RVA: 0x00131AE4 File Offset: 0x0012FCE4
	public global::UITextPosition[] CalculatePlacement(string text, params Vector2[] points)
	{
		global::UITextPosition[] array = null;
		return (this.CalculatePlacement(points, ref array, text) <= 0) ? global::UIFont.empty : array;
	}

	// Token: 0x06004D44 RID: 19780 RVA: 0x00131B10 File Offset: 0x0012FD10
	private int CalculatePlacement(Vector3[] points, ref global::UITextPosition[] positions, string text)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.CalculatePlacement(points, positions, text);
		}
		int num = this.FillMangle(points, 0, positions, 0, Mathf.Min(points.Length, positions.Length));
		if (num > 0)
		{
			return this.ProcessShared(num, ref positions, text);
		}
		return num;
	}

	// Token: 0x06004D45 RID: 19781 RVA: 0x00131B68 File Offset: 0x0012FD68
	public int CalculatePlacement(Vector3[] points, global::UITextPosition[] positions, string text)
	{
		if (positions == null)
		{
			throw new ArgumentNullException("positions");
		}
		return this.CalculatePlacement(points, ref positions, text, base.transform);
	}

	// Token: 0x06004D46 RID: 19782 RVA: 0x00131B8C File Offset: 0x0012FD8C
	public global::UITextPosition[] CalculatePlacement(string text, params Vector3[] points)
	{
		global::UITextPosition[] array = null;
		return (this.CalculatePlacement(points, ref array, text) <= 0) ? global::UIFont.empty : array;
	}

	// Token: 0x06004D47 RID: 19783 RVA: 0x00131BB8 File Offset: 0x0012FDB8
	private int CalculatePlacement(Vector3[] points, ref global::UITextPosition[] positions, string text, Matrix4x4 transform)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.CalculatePlacement(points, positions, text);
		}
		int num = this.FillMangle(points, 0, positions, 0, points.Length);
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				global::UIFont.manglePoints[i] = transform.MultiplyPoint(global::UIFont.manglePoints[i]);
			}
			return this.ProcessShared(num, ref positions, text);
		}
		return num;
	}

	// Token: 0x06004D48 RID: 19784 RVA: 0x00131C40 File Offset: 0x0012FE40
	public int CalculatePlacement(Vector3[] points, global::UITextPosition[] positions, string text, Matrix4x4 transform)
	{
		if (positions == null)
		{
			throw new ArgumentNullException("positions");
		}
		return this.CalculatePlacement(points, ref positions, text, transform);
	}

	// Token: 0x06004D49 RID: 19785 RVA: 0x00131C60 File Offset: 0x0012FE60
	public global::UITextPosition CalculatePlacement(string text, Matrix4x4 transform, Vector3 point)
	{
		Vector3[] points = new Vector3[]
		{
			point
		};
		global::UITextPosition[] array = new global::UITextPosition[]
		{
			default(global::UITextPosition)
		};
		this.CalculatePlacement(points, array, text, transform);
		return array[0];
	}

	// Token: 0x06004D4A RID: 19786 RVA: 0x00131CB4 File Offset: 0x0012FEB4
	public global::UITextPosition[] CalculatePlacement(string text, Matrix4x4 transform, params Vector3[] points)
	{
		global::UITextPosition[] array = null;
		if (points == null)
		{
			return null;
		}
		if (points.Length == 0)
		{
			return global::UIFont.empty;
		}
		return (this.CalculatePlacement(points, ref array, text, transform) <= 0) ? global::UIFont.empty : array;
	}

	// Token: 0x06004D4B RID: 19787 RVA: 0x00131CF8 File Offset: 0x0012FEF8
	private int CalculatePlacement(Vector2[] points, ref global::UITextPosition[] positions, string text, Matrix4x4 transform)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.CalculatePlacement(points, positions, text);
		}
		int num = this.FillMangle(points, 0, positions, 0, Mathf.Min(points.Length, positions.Length));
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				global::UIFont.manglePoints[i] = transform.MultiplyPoint(global::UIFont.manglePoints[i]);
			}
			return this.ProcessShared(num, ref positions, text);
		}
		return num;
	}

	// Token: 0x06004D4C RID: 19788 RVA: 0x00131D8C File Offset: 0x0012FF8C
	public int CalculatePlacement(Vector2[] points, global::UITextPosition[] positions, string text, Matrix4x4 transform)
	{
		if (positions == null)
		{
			throw new ArgumentNullException("positions");
		}
		return this.CalculatePlacement(points, ref positions, text, transform);
	}

	// Token: 0x06004D4D RID: 19789 RVA: 0x00131DAC File Offset: 0x0012FFAC
	public global::UITextPosition[] CalculatePlacement(string text, Matrix4x4 transform, params Vector2[] points)
	{
		global::UITextPosition[] array = null;
		if (points == null)
		{
			return null;
		}
		if (points.Length == 0)
		{
			return global::UIFont.empty;
		}
		return (this.CalculatePlacement(points, ref array, text, transform) <= 0) ? global::UIFont.empty : array;
	}

	// Token: 0x06004D4E RID: 19790 RVA: 0x00131DF0 File Offset: 0x0012FFF0
	private int CalculatePlacement(Vector3[] points, ref global::UITextPosition[] positions, string text, Transform self)
	{
		if (!self)
		{
			return this.CalculatePlacement(points, positions, text);
		}
		return this.CalculatePlacement(points, positions, text, self.worldToLocalMatrix);
	}

	// Token: 0x06004D4F RID: 19791 RVA: 0x00131E28 File Offset: 0x00130028
	public int CalculatePlacement(Vector3[] points, global::UITextPosition[] positions, string text, Transform self)
	{
		if (positions == null)
		{
			throw new ArgumentNullException("positions");
		}
		return this.CalculatePlacement(points, ref positions, text, base.transform);
	}

	// Token: 0x06004D50 RID: 19792 RVA: 0x00131E4C File Offset: 0x0013004C
	private int CalculatePlacement(Vector2[] points, ref global::UITextPosition[] positions, string text, Transform self)
	{
		if (!self)
		{
			return this.CalculatePlacement(points, positions, text);
		}
		return this.CalculatePlacement(points, positions, text, self.worldToLocalMatrix);
	}

	// Token: 0x06004D51 RID: 19793 RVA: 0x00131E84 File Offset: 0x00130084
	public int CalculatePlacement(Vector2[] points, global::UITextPosition[] positions, string text, Transform self)
	{
		if (positions == null)
		{
			throw new ArgumentNullException("positions");
		}
		return this.CalculatePlacement(points, ref positions, text, base.transform);
	}

	// Token: 0x06004D52 RID: 19794 RVA: 0x00131EA8 File Offset: 0x001300A8
	public global::UITextPosition CalculatePlacement(string text, Transform self, Vector2 point)
	{
		Vector2[] points = new Vector2[]
		{
			point
		};
		global::UITextPosition[] array = new global::UITextPosition[]
		{
			default(global::UITextPosition)
		};
		this.CalculatePlacement(points, array, text, self);
		return array[0];
	}

	// Token: 0x06004D53 RID: 19795 RVA: 0x00131EFC File Offset: 0x001300FC
	public global::UITextPosition[] CalculatePlacement(string text, Transform self, params Vector2[] points)
	{
		global::UITextPosition[] array = null;
		if (points == null)
		{
			return null;
		}
		if (points.Length == 0)
		{
			return global::UIFont.empty;
		}
		return (this.CalculatePlacement(points, ref array, text, self) <= 0) ? global::UIFont.empty : array;
	}

	// Token: 0x06004D54 RID: 19796 RVA: 0x00131F40 File Offset: 0x00130140
	public global::UITextPosition CalculatePlacement(string text, Vector3 point)
	{
		Vector3[] points = new Vector3[]
		{
			point
		};
		global::UITextPosition[] array = new global::UITextPosition[]
		{
			default(global::UITextPosition)
		};
		this.CalculatePlacement(points, array, text);
		return array[0];
	}

	// Token: 0x06004D55 RID: 19797 RVA: 0x00131F94 File Offset: 0x00130194
	public global::UITextPosition CalculatePlacement(string text, Matrix4x4 transform, Vector2 point)
	{
		Vector2[] points = new Vector2[]
		{
			point
		};
		global::UITextPosition[] array = new global::UITextPosition[]
		{
			default(global::UITextPosition)
		};
		this.CalculatePlacement(points, array, text, transform);
		return array[0];
	}

	// Token: 0x06004D56 RID: 19798 RVA: 0x00131FE8 File Offset: 0x001301E8
	public global::UITextPosition CalculatePlacement(string text, Transform self, Vector3 point)
	{
		Vector3[] points = new Vector3[]
		{
			point
		};
		global::UITextPosition[] array = new global::UITextPosition[]
		{
			default(global::UITextPosition)
		};
		this.CalculatePlacement(points, array, text, self);
		return array[0];
	}

	// Token: 0x06004D57 RID: 19799 RVA: 0x0013203C File Offset: 0x0013023C
	public global::UITextPosition[] CalculatePlacement(string text, Transform self, params Vector3[] points)
	{
		global::UITextPosition[] array = null;
		if (points == null)
		{
			return null;
		}
		if (points.Length == 0)
		{
			return global::UIFont.empty;
		}
		return (this.CalculatePlacement(points, ref array, text, self) <= 0) ? global::UIFont.empty : array;
	}

	// Token: 0x06004D58 RID: 19800 RVA: 0x00132080 File Offset: 0x00130280
	private int ProcessPlacement(int count, string text)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.ProcessPlacement(count, text);
		}
		int i = 0;
		if (global::UIFont.manglePoints[global::UIFont.mangleIndices[i]].y < 0f)
		{
			do
			{
				global::UIFont.manglePositions[i] = new global::UITextPosition(global::UITextRegion.Before);
			}
			while (++i < count && global::UIFont.manglePoints[global::UIFont.mangleIndices[i]].y < 0f);
			if (i >= count)
			{
				return count;
			}
		}
		int length = text.Length;
		int num = this.verticalSpacing + this.bmFont.charSize;
		if (length == 0)
		{
			while (global::UIFont.manglePoints[global::UIFont.mangleIndices[i]].y <= (float)num)
			{
				if (global::UIFont.manglePoints[global::UIFont.mangleIndices[i]].x < 0f)
				{
					global::UIFont.manglePositions[i] = new global::UITextPosition(global::UITextRegion.Pre);
				}
				else
				{
					global::UIFont.manglePositions[i] = new global::UITextPosition(global::UITextRegion.Past);
				}
				if (++i >= count)
				{
					return count;
				}
			}
			while (i < count)
			{
				global::UIFont.manglePositions[i++] = new global::UITextPosition(global::UITextRegion.End);
			}
			return count;
		}
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = -1;
		int num7 = 0;
		int column = 0;
		bool flag = false;
		bool flag2 = false;
		IL_389:
		while (i < count)
		{
			Vector3 vector = global::UIFont.manglePoints[global::UIFont.mangleIndices[i]];
			int num8 = Mathf.FloorToInt(vector.y);
			int num9 = num8 / num;
			IL_19E:
			while (!flag2)
			{
				if (num9 > num2)
				{
					flag = false;
					for (;;)
					{
						while (text[num4] != '\n')
						{
							if (++num4 >= length)
							{
								goto Block_12;
							}
							num3++;
						}
						num2++;
						num3 = 0;
						column = 0;
						num6 = num4;
						num4 = (num5 = num4 + 1);
						num7 = 0;
						if (num9 <= num2)
						{
							goto Block_14;
						}
					}
					Block_12:
					flag2 = true;
					continue;
					Block_14:
					goto IL_389;
				}
				if (vector.x < 0f)
				{
					global::UIFont.manglePositions[i++] = new global::UITextPosition(num2, 0, num5, global::UITextRegion.Pre);
					goto IL_389;
				}
				if (flag)
				{
					global::UIFont.manglePositions[i++] = new global::UITextPosition(num2, column, num6, global::UITextRegion.Past);
					goto IL_389;
				}
				while ((float)num7 < vector.x)
				{
					if (num4 >= length)
					{
						flag2 = true;
						goto IL_19E;
					}
					int num10 = (int)text[num4];
					if (num10 == 10)
					{
						num6 = num4;
						num4 = (num5 = num4 + 1);
						column = num3;
						num3 = 0;
						flag = true;
						goto IL_19E;
					}
					global::BMGlyph bmglyph;
					if (this.mFont.GetGlyph(num10, out bmglyph))
					{
						if (num6 >= num5)
						{
							num7 += bmglyph.GetKerning((int)text[num6]);
						}
						num7 += this.mSpacingX + bmglyph.advance;
					}
					num6 = num4++;
					column = num3++;
				}
				global::UIFont.manglePositions[i++] = new global::UITextPosition(num2, column, num6, global::UITextRegion.Inside);
				goto IL_389;
			}
			if (num9 == num2)
			{
				global::UIFont.manglePositions[i++] = new global::UITextPosition(num2, num3, num4, global::UITextRegion.Past);
			}
			else
			{
				while (i < count)
				{
					global::UIFont.manglePositions[i++] = new global::UITextPosition(num2, num3, num4, global::UITextRegion.End);
				}
			}
		}
		if (i < count)
		{
			Debug.LogError(" skipped " + (count - i));
		}
		return count;
	}

	// Token: 0x06004D59 RID: 19801 RVA: 0x0013243C File Offset: 0x0013063C
	private void Align(ref global::UIFont.PrintContext ctx)
	{
		if (this.mFont.charSize > 0)
		{
			int num;
			switch (ctx.alignment)
			{
			case global::UIFont.Alignment.Left:
				num = 0;
				break;
			case global::UIFont.Alignment.Center:
				num = Mathf.Max(0, Mathf.RoundToInt((float)(ctx.lineWidth - ctx.x) * 0.5f));
				break;
			case global::UIFont.Alignment.Right:
				num = Mathf.Max(0, Mathf.RoundToInt((float)(ctx.lineWidth - ctx.x)));
				break;
			case global::UIFont.Alignment.LeftOverflowRight:
				num = Mathf.Max(0, Mathf.RoundToInt((float)(ctx.x - ctx.lineWidth)));
				break;
			default:
				throw new NotImplementedException();
			}
			if (num == 0)
			{
				return;
			}
			float num2 = (float)((double)num / (double)this.mFont.charSize);
			for (int i = ctx.indexOffset; i < ctx.m.vSize; i++)
			{
				NGUI.Meshing.Vertex[] v = ctx.m.v;
				int num3 = i;
				v[num3].x = v[num3].x + num2;
			}
		}
	}

	// Token: 0x06004D5A RID: 19802 RVA: 0x00132544 File Offset: 0x00130744
	public void Print(string text, Color color, NGUI.Meshing.MeshBuffer m, bool encoding, global::UIFont.SymbolStyle symbolStyle, global::UIFont.Alignment alignment, int lineWidth)
	{
		global::UITextSelection uitextSelection = default(global::UITextSelection);
		this.Print(text, color, m, encoding, symbolStyle, alignment, lineWidth, ref uitextSelection, '\0', color, Color.clear, '\0', -1f);
	}

	// Token: 0x06004D5B RID: 19803 RVA: 0x0013257C File Offset: 0x0013077C
	public void Print(string text, Color normalColor, NGUI.Meshing.MeshBuffer m, bool encoding, global::UIFont.SymbolStyle symbolStyle, global::UIFont.Alignment alignment, int lineWidth, ref global::UITextSelection selection, char carratChar, Color highlightTextColor, Color highlightBarColor, char highlightChar, float highlightSplit)
	{
		if (this.mReplacement != null)
		{
			this.mReplacement.Print(text, normalColor, m, encoding, symbolStyle, alignment, lineWidth, ref selection, carratChar, highlightTextColor, highlightBarColor, highlightChar, highlightSplit);
		}
		else if (this.mFont != null && text != null)
		{
			if (!this.mFont.isValid)
			{
				Debug.LogError("Attempting to print using an invalid font!");
				return;
			}
			int num = 0;
			this.mColors.Clear();
			this.mColors.Add(normalColor);
			global::UIFont.PrintContext printContext;
			printContext.m = m;
			printContext.lineWidth = lineWidth;
			printContext.alignment = alignment;
			printContext.scale.x = ((this.mFont.charSize <= 0) ? 1f : (1f / (float)this.mFont.charSize));
			printContext.scale.y = printContext.scale.x;
			printContext.normalColor = normalColor;
			printContext.indexOffset = printContext.m.vSize;
			printContext.maxX = 0;
			printContext.x = 0;
			printContext.y = 0;
			printContext.prev = 0;
			printContext.lineHeight = this.mFont.charSize + this.mSpacingY;
			printContext.v0 = default(Vector3);
			printContext.v1 = default(Vector3);
			printContext.u0 = default(Vector2);
			printContext.u1 = default(Vector2);
			printContext.invX = this.uvRect.width / (float)this.mFont.texWidth;
			printContext.invY = this.uvRect.height / (float)this.mFont.texHeight;
			printContext.textLength = text.Length;
			printContext.nonHighlightColor = normalColor;
			printContext.carratChar = carratChar;
			if (printContext.carratChar == '\0')
			{
				printContext.carratIndex = -1;
				printContext.carratGlyph = null;
			}
			else if ((printContext.carratIndex = selection.carratIndex) == -1)
			{
				printContext.carratGlyph = null;
				printContext.carratChar = '\0';
			}
			else if (!this.mFont.GetGlyph((int)carratChar, out printContext.carratGlyph))
			{
				printContext.carratIndex = -1;
			}
			printContext.highlightChar = highlightChar;
			printContext.highlightBarColor = highlightBarColor;
			printContext.highlightTextColor = highlightTextColor;
			printContext.highlightSplit = highlightSplit;
			printContext.highlightBarDraw = (printContext.highlightChar != '\0' && printContext.highlightSplit >= 0f && printContext.highlightSplit <= 1f && highlightBarColor.a > 0f);
			if (!printContext.highlightBarDraw && printContext.highlightTextColor == printContext.normalColor)
			{
				printContext.highlight = global::UIHighlight.invalid;
				printContext.highlightGlyph = null;
			}
			else if (!selection.GetHighlight(out printContext.highlight))
			{
				printContext.highlightGlyph = null;
				printContext.highlightBarDraw = false;
			}
			else if ((printContext.highlightChar != printContext.carratChar) ? (!this.mFont.GetGlyph((int)printContext.highlightChar, out printContext.highlightGlyph)) : ((printContext.highlightGlyph = printContext.carratGlyph) == null))
			{
				printContext.highlight = global::UIHighlight.invalid;
			}
			printContext.j = 0;
			printContext.previousX = 0;
			printContext.isLineEnd = false;
			printContext.highlightVertex = -1;
			printContext.glyph = null;
			printContext.c = '\0';
			printContext.skipSymbols = (!encoding || symbolStyle == global::UIFont.SymbolStyle.None);
			printContext.printChar = false;
			printContext.printColor = normalColor;
			printContext.symbol = null;
			printContext.text = text;
			printContext.i = 0;
			while (printContext.i < printContext.textLength)
			{
				printContext.c = printContext.text[printContext.i];
				if (printContext.c == '\n')
				{
					printContext.isLineEnd = true;
					goto IL_B6F;
				}
				if (printContext.c >= ' ')
				{
					if (encoding && printContext.c == '[')
					{
						int num2 = global::NGUITools.ParseSymbol(text, printContext.i, this.mColors, ref num);
						if (num2 > 0)
						{
							printContext.nonHighlightColor = this.mColors[this.mColors.Count - 1];
							printContext.i += num2 - 1;
							goto IL_E19;
						}
					}
					if (printContext.skipSymbols || !this.mFont.MatchSymbol(printContext.text, printContext.i, printContext.textLength, out printContext.symbol))
					{
						if (!this.mFont.GetGlyph((int)printContext.c, out printContext.glyph))
						{
							goto IL_B6F;
						}
						bool flag = printContext.prev != 0;
						if (flag)
						{
							printContext.previousX = printContext.x;
							printContext.x += printContext.glyph.GetKerning(printContext.prev);
						}
						if (printContext.c == ' ')
						{
							if (!flag)
							{
								printContext.previousX = printContext.x;
							}
							printContext.x += this.mSpacingX + printContext.glyph.advance;
							printContext.prev = (int)printContext.c;
							goto IL_B6F;
						}
						printContext.v0.x = printContext.scale.x * (float)(printContext.x + printContext.glyph.offsetX);
						printContext.v0.y = -printContext.scale.y * (float)(printContext.y + printContext.glyph.offsetY);
						printContext.v1.x = printContext.v0.x + printContext.scale.x * (float)printContext.glyph.width;
						printContext.v1.y = printContext.v0.y - printContext.scale.y * (float)printContext.glyph.height;
						printContext.u0.x = this.mUVRect.xMin + printContext.invX * (float)printContext.glyph.x;
						printContext.u0.y = this.mUVRect.yMax - printContext.invY * (float)printContext.glyph.y;
						printContext.u1.x = printContext.u0.x + printContext.invX * (float)printContext.glyph.width;
						printContext.u1.y = printContext.u0.y - printContext.invY * (float)printContext.glyph.height;
						if (!flag)
						{
							printContext.previousX = printContext.x;
						}
						printContext.x += this.mSpacingX + printContext.glyph.advance;
						printContext.prev = (int)printContext.c;
						if (printContext.glyph.channel == 0 || printContext.glyph.channel == 15)
						{
							printContext.printColor = ((printContext.highlight.b.i <= printContext.j || printContext.highlight.a.i > printContext.j) ? printContext.nonHighlightColor : printContext.highlightTextColor);
						}
						else
						{
							Color color = (printContext.highlight.b.i <= printContext.j || printContext.highlight.a.i > printContext.j) ? printContext.nonHighlightColor : printContext.highlightTextColor;
							color *= 0.49f;
							switch (printContext.glyph.channel)
							{
							case 1:
								color.b += 0.51f;
								break;
							case 2:
								color.g += 0.51f;
								break;
							case 4:
								color.r += 0.51f;
								break;
							case 8:
								color.a += 0.51f;
								break;
							}
							printContext.printColor = color;
						}
					}
					else
					{
						printContext.v0.x = printContext.scale.x * (float)printContext.x;
						printContext.v0.y = -printContext.scale.y * (float)printContext.y;
						printContext.v1.x = printContext.v0.x + printContext.scale.x * (float)printContext.symbol.width;
						printContext.v1.y = printContext.v0.y - printContext.scale.y * (float)printContext.symbol.height;
						printContext.u0.x = this.mUVRect.xMin + printContext.invX * (float)printContext.symbol.x;
						printContext.u0.y = this.mUVRect.yMax - printContext.invY * (float)printContext.symbol.y;
						printContext.u1.x = printContext.u0.x + printContext.invX * (float)printContext.symbol.width;
						printContext.u1.y = printContext.u0.y - printContext.invY * (float)printContext.symbol.height;
						printContext.previousX = printContext.x;
						printContext.x += this.mSpacingX + printContext.symbol.width;
						printContext.i += printContext.symbol.sequence.Length - 1;
						printContext.prev = 0;
						if (symbolStyle == global::UIFont.SymbolStyle.Colored)
						{
							printContext.printColor = ((printContext.highlight.b.i <= printContext.j || printContext.highlight.a.i > printContext.j) ? printContext.nonHighlightColor : printContext.highlightTextColor);
						}
						else
						{
							printContext.printColor = ((printContext.highlight.b.i <= printContext.j || printContext.highlight.a.i > printContext.j) ? new Color(1f, 1f, 1f, printContext.nonHighlightColor.a) : printContext.highlightTextColor);
						}
					}
					printContext.printChar = true;
					goto IL_B6F;
				}
				printContext.prev = 0;
				IL_E19:
				printContext.i++;
				continue;
				IL_B6F:
				if (printContext.highlight.b.i == printContext.j)
				{
					if (printContext.printChar)
					{
						printContext.m.FastQuad(printContext.v0, printContext.v1, printContext.u0, printContext.u1, printContext.printColor);
						printContext.printChar = false;
					}
					if (printContext.highlightBarDraw)
					{
						this.PutHighlightEnd(ref printContext);
					}
				}
				else if (printContext.highlight.a.i == printContext.j)
				{
					if (printContext.highlightBarDraw)
					{
						this.PutHighlightStart(ref printContext);
					}
					if (printContext.printChar)
					{
						printContext.m.FastQuad(printContext.v0, printContext.v1, printContext.u0, printContext.u1, printContext.printColor);
						printContext.printChar = false;
					}
				}
				else if (printContext.carratIndex == printContext.j)
				{
					if (printContext.printChar)
					{
						printContext.m.FastQuad(printContext.v0, printContext.v1, printContext.u0, printContext.u1, printContext.printColor);
						printContext.printChar = false;
					}
					this.DrawCarat(ref printContext);
				}
				else if (printContext.printChar)
				{
					printContext.m.FastQuad(printContext.v0, printContext.v1, printContext.u0, printContext.u1, printContext.printColor);
					printContext.printChar = false;
				}
				printContext.j++;
				if (!printContext.isLineEnd)
				{
					goto IL_E19;
				}
				printContext.isLineEnd = false;
				if (printContext.x > printContext.maxX)
				{
					printContext.maxX = printContext.x;
				}
				bool flag2 = printContext.highlightBarDraw && printContext.highlightVertex != -1;
				if (flag2)
				{
					this.PutHighlightEnd(ref printContext);
				}
				if (printContext.indexOffset < printContext.m.vSize)
				{
					this.Align(ref printContext);
					printContext.indexOffset = printContext.m.vSize;
				}
				printContext.previousX = printContext.x;
				printContext.x = 0;
				printContext.y += printContext.lineHeight;
				printContext.prev = 0;
				if (flag2)
				{
					this.PutHighlightStart(ref printContext);
					goto IL_E19;
				}
				goto IL_E19;
			}
			printContext.previousX = printContext.x;
			if (printContext.highlightVertex != -1)
			{
				this.PutHighlightEnd(ref printContext);
			}
			else if (printContext.j == printContext.carratIndex)
			{
				this.DrawCarat(ref printContext);
			}
			if (printContext.indexOffset < printContext.m.vSize)
			{
				this.Align(ref printContext);
				printContext.indexOffset = printContext.m.vSize;
			}
		}
	}

	// Token: 0x06004D5C RID: 19804 RVA: 0x0013343C File Offset: 0x0013163C
	private void PutHighlightStart(ref global::UIFont.PrintContext ctx)
	{
		if (ctx.highlightVertex != -1)
		{
			this.PutHighlightEnd(ref ctx);
		}
		float num = ctx.scale.x * ((float)ctx.highlightGlyph.width * ctx.highlightSplit);
		Vector2 xy;
		xy.x = ctx.scale.x * (float)(ctx.previousX + ctx.highlightGlyph.offsetX) - num;
		xy.y = -ctx.scale.y * (float)(ctx.y + ctx.highlightGlyph.offsetY);
		Vector2 xy2;
		xy2.x = xy.x + num;
		float num2 = xy2.x - xy.x;
		xy.x += num2;
		xy2.x += num2;
		xy2.y = xy.y - ctx.scale.y * (float)ctx.highlightGlyph.height;
		Vector2 uv;
		uv.x = this.mUVRect.xMin + ctx.invX * (float)ctx.highlightGlyph.x;
		uv.y = this.mUVRect.yMax - ctx.invY * (float)ctx.highlightGlyph.y;
		Vector2 uv2;
		uv2.x = uv.x + ctx.invX * (float)ctx.highlightGlyph.width * ctx.highlightSplit;
		uv2.y = uv.y - ctx.invY * (float)ctx.highlightGlyph.height;
		ctx.m.FastQuad(xy, xy2, uv, uv2, ctx.highlightBarColor);
		ctx.highlightVertex = ctx.m.FastQuad(new Vector2(xy2.x, xy.y), xy2, new Vector2(uv2.x, uv.y), uv2, ctx.highlightBarColor);
		float x = xy2.x;
		xy2.x = xy.x + ctx.scale.x * (float)ctx.highlightGlyph.width;
		xy.x = x;
		x = uv2.x;
		uv2.x = uv.x + ctx.invX * (float)ctx.highlightGlyph.width;
		uv.x = x;
		ctx.m.FastQuad(xy, xy2, uv, uv2, ctx.highlightBarColor);
	}

	// Token: 0x06004D5D RID: 19805 RVA: 0x001336A8 File Offset: 0x001318A8
	private void PutHighlightEnd(ref global::UIFont.PrintContext ctx)
	{
		if (ctx.highlightVertex == -1)
		{
			return;
		}
		float num = ctx.scale.x * (float)(ctx.previousX + ctx.highlightGlyph.offsetX) - ctx.m.v[ctx.highlightVertex].x;
		if (num > 0f)
		{
			NGUI.Meshing.Vertex[] v = ctx.m.v;
			int highlightVertex = ctx.highlightVertex;
			v[highlightVertex].x = v[highlightVertex].x + num;
			NGUI.Meshing.Vertex[] v2 = ctx.m.v;
			int num2 = ctx.highlightVertex + 1;
			v2[num2].x = v2[num2].x + num;
			NGUI.Meshing.Vertex[] v3 = ctx.m.v;
			int num3 = ctx.highlightVertex + 4;
			v3[num3].x = v3[num3].x + num;
			NGUI.Meshing.Vertex[] v4 = ctx.m.v;
			int num4 = ctx.highlightVertex + 4 + 1;
			v4[num4].x = v4[num4].x + num;
			NGUI.Meshing.Vertex[] v5 = ctx.m.v;
			int num5 = ctx.highlightVertex + 4 + 2;
			v5[num5].x = v5[num5].x + num;
			NGUI.Meshing.Vertex[] v6 = ctx.m.v;
			int num6 = ctx.highlightVertex + 4 + 3;
			v6[num6].x = v6[num6].x + num;
		}
		ctx.highlightVertex = -1;
	}

	// Token: 0x06004D5E RID: 19806 RVA: 0x001337F4 File Offset: 0x001319F4
	private void DrawCarat(ref global::UIFont.PrintContext ctx)
	{
		Vector2 xy;
		xy.x = ctx.scale.x * (float)(ctx.previousX + ctx.carratGlyph.offsetX);
		xy.y = -ctx.scale.y * (float)(ctx.y + ctx.carratGlyph.offsetY);
		Vector2 xy2;
		xy2.x = xy.x + ctx.scale.x * (float)ctx.carratGlyph.width;
		xy2.y = xy.y - ctx.scale.y * (float)ctx.carratGlyph.height;
		Vector2 uv;
		uv.x = this.mUVRect.xMin + ctx.invX * (float)ctx.carratGlyph.x;
		uv.y = this.mUVRect.yMax - ctx.invY * (float)ctx.carratGlyph.y;
		Vector2 uv2;
		uv2.x = uv.x + ctx.invX * (float)ctx.carratGlyph.width;
		uv2.y = uv.y - ctx.invY * (float)ctx.carratGlyph.height;
		ctx.m.FastQuad(xy, xy2, uv, uv2, ctx.normalColor);
	}

	// Token: 0x04002B0D RID: 11021
	private const int mangleStartSize = 8;

	// Token: 0x04002B0E RID: 11022
	[HideInInspector]
	[SerializeField]
	private Material mMat;

	// Token: 0x04002B0F RID: 11023
	[HideInInspector]
	[SerializeField]
	private Rect mUVRect = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x04002B10 RID: 11024
	[SerializeField]
	[HideInInspector]
	private global::BMFont mFont = new global::BMFont();

	// Token: 0x04002B11 RID: 11025
	[HideInInspector]
	[SerializeField]
	private int mSpacingX;

	// Token: 0x04002B12 RID: 11026
	[HideInInspector]
	[SerializeField]
	private int mSpacingY;

	// Token: 0x04002B13 RID: 11027
	[HideInInspector]
	[SerializeField]
	private global::UIAtlas mAtlas;

	// Token: 0x04002B14 RID: 11028
	[SerializeField]
	[HideInInspector]
	private global::UIFont mReplacement;

	// Token: 0x04002B15 RID: 11029
	private global::UIAtlas.Sprite mSprite;

	// Token: 0x04002B16 RID: 11030
	private bool mSpriteSet;

	// Token: 0x04002B17 RID: 11031
	private List<Color> mColors = new List<Color>();

	// Token: 0x04002B18 RID: 11032
	private static List<global::UITextMarkup> _tempMarkup;

	// Token: 0x04002B19 RID: 11033
	private static Vector3[] manglePoints = new Vector3[8];

	// Token: 0x04002B1A RID: 11034
	private static int[] mangleIndices = new int[8];

	// Token: 0x04002B1B RID: 11035
	private static global::UITextPosition[] manglePositions = new global::UITextPosition[8];

	// Token: 0x04002B1C RID: 11036
	private static readonly global::UIFont.MangleSorter mangleSort = new global::UIFont.MangleSorter();

	// Token: 0x04002B1D RID: 11037
	private static readonly global::UITextPosition[] empty = new global::UITextPosition[0];

	// Token: 0x020008DD RID: 2269
	public enum Alignment
	{
		// Token: 0x04002B1F RID: 11039
		Left,
		// Token: 0x04002B20 RID: 11040
		Center,
		// Token: 0x04002B21 RID: 11041
		Right,
		// Token: 0x04002B22 RID: 11042
		LeftOverflowRight
	}

	// Token: 0x020008DE RID: 2270
	public enum SymbolStyle
	{
		// Token: 0x04002B24 RID: 11044
		None,
		// Token: 0x04002B25 RID: 11045
		Uncolored,
		// Token: 0x04002B26 RID: 11046
		Colored
	}

	// Token: 0x020008DF RID: 2271
	private class MangleSorter : Comparer<Vector3>
	{
		// Token: 0x06004D60 RID: 19808 RVA: 0x00133978 File Offset: 0x00131B78
		public void SetLineSizing(double height, double spacing)
		{
			if (height == 0.0)
			{
				if (spacing == 0.0)
				{
					this.noLineSize = true;
				}
				else
				{
					this.lineHeight = spacing;
					this.noVSpacing = true;
					this.noLineSize = false;
				}
			}
			else
			{
				this.lineHeight = height;
				if (spacing == 0.0)
				{
					this.noVSpacing = true;
					this.noLineSize = false;
				}
				else if (spacing == -height)
				{
					this.noLineSize = true;
					this.noVSpacing = true;
				}
				else
				{
					this.vSpacing = spacing;
					this.noLineSize = false;
					this.noVSpacing = false;
				}
			}
		}

		// Token: 0x06004D61 RID: 19809 RVA: 0x00133A24 File Offset: 0x00131C24
		public override int Compare(Vector3 x, Vector3 y)
		{
			int num3;
			if (!this.noLineSize)
			{
				double num = (double)x.y / this.lineHeight;
				double num2 = (double)y.y / this.lineHeight;
				if (!this.noVSpacing)
				{
					if (num >= 1.0 || num <= -1.0)
					{
						num = ((double)x.y - this.lineHeight) / (this.lineHeight + this.vSpacing);
					}
					if (num2 >= 1.0 || num2 <= -1.0)
					{
						num2 = ((double)y.y - this.lineHeight) / (this.lineHeight + this.vSpacing);
					}
				}
				if (num < 0.0)
				{
					num = -Math.Ceiling(-num);
				}
				else
				{
					num = Math.Floor(num);
				}
				if (num2 < 0.0)
				{
					num2 = -Math.Ceiling(-num2);
				}
				else
				{
					num2 = Math.Floor(num2);
				}
				num3 = num.CompareTo(num2);
			}
			else
			{
				num3 = x.y.CompareTo(y.y);
			}
			if (num3 == 0)
			{
				num3 = x.x.CompareTo(y.x);
				if (num3 == 0)
				{
					num3 = x.z.CompareTo(y.z);
				}
			}
			return num3;
		}

		// Token: 0x04002B27 RID: 11047
		public double lineHeight = 12.0;

		// Token: 0x04002B28 RID: 11048
		public double vSpacing = 12.0;

		// Token: 0x04002B29 RID: 11049
		private bool noLineSize;

		// Token: 0x04002B2A RID: 11050
		private bool noVSpacing;
	}

	// Token: 0x020008E0 RID: 2272
	private struct PrintContext
	{
		// Token: 0x04002B2B RID: 11051
		public NGUI.Meshing.MeshBuffer m;

		// Token: 0x04002B2C RID: 11052
		public global::BMGlyph glyph;

		// Token: 0x04002B2D RID: 11053
		public global::BMGlyph highlightGlyph;

		// Token: 0x04002B2E RID: 11054
		public global::BMGlyph carratGlyph;

		// Token: 0x04002B2F RID: 11055
		public global::BMSymbol symbol;

		// Token: 0x04002B30 RID: 11056
		public string text;

		// Token: 0x04002B31 RID: 11057
		public global::UIHighlight highlight;

		// Token: 0x04002B32 RID: 11058
		public Color printColor;

		// Token: 0x04002B33 RID: 11059
		public Color nonHighlightColor;

		// Token: 0x04002B34 RID: 11060
		public Color normalColor;

		// Token: 0x04002B35 RID: 11061
		public Color highlightTextColor;

		// Token: 0x04002B36 RID: 11062
		public Color highlightBarColor;

		// Token: 0x04002B37 RID: 11063
		public Vector3 v0;

		// Token: 0x04002B38 RID: 11064
		public Vector3 v1;

		// Token: 0x04002B39 RID: 11065
		public Vector2 u0;

		// Token: 0x04002B3A RID: 11066
		public Vector2 u1;

		// Token: 0x04002B3B RID: 11067
		public Vector2 scale;

		// Token: 0x04002B3C RID: 11068
		public float invX;

		// Token: 0x04002B3D RID: 11069
		public float invY;

		// Token: 0x04002B3E RID: 11070
		public float highlightSplit;

		// Token: 0x04002B3F RID: 11071
		public int x;

		// Token: 0x04002B40 RID: 11072
		public int maxX;

		// Token: 0x04002B41 RID: 11073
		public int previousX;

		// Token: 0x04002B42 RID: 11074
		public int y;

		// Token: 0x04002B43 RID: 11075
		public int highlightVertex;

		// Token: 0x04002B44 RID: 11076
		public int prev;

		// Token: 0x04002B45 RID: 11077
		public int lineHeight;

		// Token: 0x04002B46 RID: 11078
		public int lineWidth;

		// Token: 0x04002B47 RID: 11079
		public int indexOffset;

		// Token: 0x04002B48 RID: 11080
		public int textLength;

		// Token: 0x04002B49 RID: 11081
		public int i;

		// Token: 0x04002B4A RID: 11082
		public int carratIndex;

		// Token: 0x04002B4B RID: 11083
		public int j;

		// Token: 0x04002B4C RID: 11084
		public global::UIFont.Alignment alignment;

		// Token: 0x04002B4D RID: 11085
		public char carratChar;

		// Token: 0x04002B4E RID: 11086
		public char highlightChar;

		// Token: 0x04002B4F RID: 11087
		public char c;

		// Token: 0x04002B50 RID: 11088
		public bool highlightBarDraw;

		// Token: 0x04002B51 RID: 11089
		public bool isLineEnd;

		// Token: 0x04002B52 RID: 11090
		public bool skipSymbols;

		// Token: 0x04002B53 RID: 11091
		public bool printChar;
	}
}
