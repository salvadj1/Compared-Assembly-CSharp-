using System;
using System.Collections.Generic;
using System.Text;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x020007EB RID: 2027
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Font")]
public class UIFont : MonoBehaviour
{
	// Token: 0x17000E14 RID: 3604
	// (get) Token: 0x06004872 RID: 18546 RVA: 0x0012633C File Offset: 0x0012453C
	public BMFont bmFont
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mFont : this.mReplacement.bmFont;
		}
	}

	// Token: 0x17000E15 RID: 3605
	// (get) Token: 0x06004873 RID: 18547 RVA: 0x00126368 File Offset: 0x00124568
	public int texWidth
	{
		get
		{
			return (!(this.mReplacement != null)) ? ((this.mFont == null) ? 1 : this.mFont.texWidth) : this.mReplacement.texWidth;
		}
	}

	// Token: 0x17000E16 RID: 3606
	// (get) Token: 0x06004874 RID: 18548 RVA: 0x001263A8 File Offset: 0x001245A8
	public int texHeight
	{
		get
		{
			return (!(this.mReplacement != null)) ? ((this.mFont == null) ? 1 : this.mFont.texHeight) : this.mReplacement.texHeight;
		}
	}

	// Token: 0x17000E17 RID: 3607
	// (get) Token: 0x06004875 RID: 18549 RVA: 0x001263E8 File Offset: 0x001245E8
	// (set) Token: 0x06004876 RID: 18550 RVA: 0x00126414 File Offset: 0x00124614
	public UIAtlas atlas
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

	// Token: 0x17000E18 RID: 3608
	// (get) Token: 0x06004877 RID: 18551 RVA: 0x001264A8 File Offset: 0x001246A8
	// (set) Token: 0x06004878 RID: 18552 RVA: 0x001264FC File Offset: 0x001246FC
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

	// Token: 0x17000E19 RID: 3609
	// (get) Token: 0x06004879 RID: 18553 RVA: 0x0012655C File Offset: 0x0012475C
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

	// Token: 0x17000E1A RID: 3610
	// (get) Token: 0x0600487A RID: 18554 RVA: 0x001265AC File Offset: 0x001247AC
	// (set) Token: 0x0600487B RID: 18555 RVA: 0x00126720 File Offset: 0x00124920
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
					if (this.mAtlas.coordinates == UIAtlas.Coordinates.Pixels)
					{
						this.mUVRect = NGUIMath.ConvertToTexCoords(this.mUVRect, texture.width, texture.height);
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

	// Token: 0x17000E1B RID: 3611
	// (get) Token: 0x0600487C RID: 18556 RVA: 0x00126778 File Offset: 0x00124978
	// (set) Token: 0x0600487D RID: 18557 RVA: 0x001267B4 File Offset: 0x001249B4
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

	// Token: 0x17000E1C RID: 3612
	// (get) Token: 0x0600487E RID: 18558 RVA: 0x0012680C File Offset: 0x00124A0C
	// (set) Token: 0x0600487F RID: 18559 RVA: 0x00126838 File Offset: 0x00124A38
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

	// Token: 0x17000E1D RID: 3613
	// (get) Token: 0x06004880 RID: 18560 RVA: 0x00126878 File Offset: 0x00124A78
	// (set) Token: 0x06004881 RID: 18561 RVA: 0x001268A4 File Offset: 0x00124AA4
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

	// Token: 0x17000E1E RID: 3614
	// (get) Token: 0x06004882 RID: 18562 RVA: 0x001268E4 File Offset: 0x00124AE4
	public int size
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mFont.charSize : this.mReplacement.size;
		}
	}

	// Token: 0x17000E1F RID: 3615
	// (get) Token: 0x06004883 RID: 18563 RVA: 0x00126920 File Offset: 0x00124B20
	public UIAtlas.Sprite sprite
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
					Debug.LogError("Can't find the sprite '" + this.mFont.spriteName + "' in UIAtlas on " + NGUITools.GetHierarchy(this.mAtlas.gameObject));
					this.mFont.spriteName = null;
				}
			}
			return this.mSprite;
		}
	}

	// Token: 0x17000E20 RID: 3616
	// (get) Token: 0x06004884 RID: 18564 RVA: 0x00126A20 File Offset: 0x00124C20
	// (set) Token: 0x06004885 RID: 18565 RVA: 0x00126A28 File Offset: 0x00124C28
	public UIFont replacement
	{
		get
		{
			return this.mReplacement;
		}
		set
		{
			UIFont uifont = value;
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

	// Token: 0x06004886 RID: 18566 RVA: 0x00126AA0 File Offset: 0x00124CA0
	private void Trim()
	{
		Texture texture = this.mAtlas.texture;
		if (texture != null && this.mSprite != null)
		{
			Rect rect = NGUIMath.ConvertToPixels(this.mUVRect, this.texture.width, this.texture.height, true);
			Rect rect2 = (this.mAtlas.coordinates != UIAtlas.Coordinates.TexCoords) ? this.mSprite.outer : NGUIMath.ConvertToPixels(this.mSprite.outer, texture.width, texture.height, true);
			int xMin = Mathf.RoundToInt(rect2.xMin - rect.xMin);
			int yMin = Mathf.RoundToInt(rect2.yMin - rect.yMin);
			int xMax = Mathf.RoundToInt(rect2.xMax - rect.xMin);
			int yMax = Mathf.RoundToInt(rect2.yMax - rect.yMin);
			this.mFont.Trim(xMin, yMin, xMax, yMax);
		}
	}

	// Token: 0x06004887 RID: 18567 RVA: 0x00126B9C File Offset: 0x00124D9C
	private bool References(UIFont font)
	{
		return !(font == null) && (font == this || (this.mReplacement != null && this.mReplacement.References(font)));
	}

	// Token: 0x06004888 RID: 18568 RVA: 0x00126BE8 File Offset: 0x00124DE8
	public static bool CheckIfRelated(UIFont a, UIFont b)
	{
		return !(a == null) && !(b == null) && (a == b || a.References(b) || b.References(a));
	}

	// Token: 0x06004889 RID: 18569 RVA: 0x00126C34 File Offset: 0x00124E34
	public void MarkAsDirty()
	{
		this.mSprite = null;
		UILabel[] array = NGUITools.FindActive<UILabel>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			UILabel uilabel = array[i];
			if (uilabel.enabled && uilabel.gameObject.activeInHierarchy && UIFont.CheckIfRelated(this, uilabel.font))
			{
				UIFont font = uilabel.font;
				uilabel.font = null;
				uilabel.font = font;
			}
			i++;
		}
	}

	// Token: 0x0600488A RID: 18570 RVA: 0x00126CAC File Offset: 0x00124EAC
	public Vector2 CalculatePrintedSize(string text, bool encoding, UIFont.SymbolStyle symbolStyle)
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
				text = NGUITools.StripSymbols(text);
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
				BMSymbol bmsymbol;
				BMGlyph bmglyph;
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

	// Token: 0x0600488B RID: 18571 RVA: 0x00126E78 File Offset: 0x00125078
	private static UITextMod EndLine(ref StringBuilder s)
	{
		int num = s.Length - 1;
		if (num > 0 && s[num] == ' ')
		{
			s[num] = '\n';
			return UITextMod.Replaced;
		}
		s.Append('\n');
		return UITextMod.Added;
	}

	// Token: 0x17000E21 RID: 3617
	// (get) Token: 0x0600488C RID: 18572 RVA: 0x00126EBC File Offset: 0x001250BC
	public static List<UITextMarkup> tempMarkup
	{
		get
		{
			List<UITextMarkup> result;
			if ((result = UIFont._tempMarkup) == null)
			{
				result = (UIFont._tempMarkup = new List<UITextMarkup>());
			}
			return result;
		}
	}

	// Token: 0x0600488D RID: 18573 RVA: 0x00126ED8 File Offset: 0x001250D8
	public string WrapText(List<UITextMarkup> markups, string text, float maxWidth, int maxLineCount, bool encoding, UIFont.SymbolStyle symbolStyle)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.WrapText(markups, text, maxWidth, maxLineCount, encoding, symbolStyle);
		}
		markups = (markups ?? UIFont.tempMarkup);
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
		BMSymbol bmsymbol = null;
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
				markups.Add(new UITextMarkup
				{
					index = i - 1,
					mod = UITextMod.Removed
				});
				markups.Add(new UITextMarkup
				{
					index = i,
					mod = UITextMod.Replaced,
					value = '\n'
				});
				num4 = i + 1;
				c = '\n';
			}
			if (c == '\n')
			{
				if (!flag2 || num5 == maxLineCount)
				{
					markups.Add(new UITextMarkup
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
								markups.Add(new UITextMarkup
								{
									index = i,
									mod = UITextMod.Removed
								});
								markups.Add(new UITextMarkup
								{
									index = i + 1,
									mod = UITextMod.Removed
								});
								markups.Add(new UITextMarkup
								{
									index = i + 2,
									mod = UITextMod.Removed
								});
								i += 2;
								goto IL_8B0;
							}
						}
						else if (text[i + 1] == '»')
						{
							if (num6++ == 0)
							{
								markups.Add(new UITextMarkup
								{
									index = i,
									mod = UITextMod.Removed
								});
								markups.Add(new UITextMarkup
								{
									index = i + 1,
									mod = UITextMod.Removed
								});
								markups.Add(new UITextMarkup
								{
									index = i + 2,
									mod = UITextMod.Removed
								});
								i += 2;
								goto IL_8B0;
							}
						}
						else if (text[i + 1] == '«' && --num6 == 0)
						{
							markups.Add(new UITextMarkup
							{
								index = i,
								mod = UITextMod.Removed
							});
							markups.Add(new UITextMarkup
							{
								index = i + 1,
								mod = UITextMod.Removed
							});
							markups.Add(new UITextMarkup
							{
								index = i + 2,
								mod = UITextMod.Removed
							});
							i += 2;
							goto IL_8B0;
						}
					}
					else if (i + 7 < length && text[i + 7] == ']' && num6 == 0)
					{
						markups.Add(new UITextMarkup
						{
							index = i,
							mod = UITextMod.Removed
						});
						markups.Add(new UITextMarkup
						{
							index = i + 1,
							mod = UITextMod.Removed
						});
						markups.Add(new UITextMarkup
						{
							index = i + 2,
							mod = UITextMod.Removed
						});
						markups.Add(new UITextMarkup
						{
							index = i + 3,
							mod = UITextMod.Removed
						});
						markups.Add(new UITextMarkup
						{
							index = i + 4,
							mod = UITextMod.Removed
						});
						markups.Add(new UITextMarkup
						{
							index = i + 5,
							mod = UITextMod.Removed
						});
						markups.Add(new UITextMarkup
						{
							index = i + 6,
							mod = UITextMod.Removed
						});
						markups.Add(new UITextMarkup
						{
							index = i + 7,
							mod = UITextMod.Removed
						});
						i += 7;
						goto IL_8B0;
					}
				}
				bool flag3 = encoding && symbolStyle != UIFont.SymbolStyle.None && this.mFont.MatchSymbol(text, i, length, out bmsymbol);
				int num7;
				if (flag3)
				{
					num7 = this.mSpacingX + bmsymbol.width;
				}
				else
				{
					BMGlyph bmglyph;
					if (!this.mFont.GetGlyph((int)c, out bmglyph))
					{
						markups.Add(new UITextMarkup
						{
							index = i,
							mod = UITextMod.Removed
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
							markups.Add(new UITextMarkup
							{
								index = i
							});
							break;
						}
						UITextMod uitextMod = UIFont.EndLine(ref stringBuilder);
						if (uitextMod != UITextMod.Replaced)
						{
							if (uitextMod == UITextMod.Added)
							{
								markups.Add(new UITextMarkup
								{
									index = i,
									mod = UITextMod.Added
								});
							}
						}
						else
						{
							markups.Add(new UITextMarkup
							{
								index = i,
								mod = UITextMod.Replaced,
								value = '\n'
							});
						}
						flag = true;
						num5++;
						if (c == ' ')
						{
							num4 = i + 1;
							num2 = num;
							markups.Add(new UITextMarkup
							{
								index = i,
								mod = UITextMod.Removed
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
							markups.Add(new UITextMarkup
							{
								index = num4,
								mod = UITextMod.Removed
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
							markups.Add(new UITextMarkup
							{
								index = i
							});
							break;
						}
						num5++;
						UITextMod uitextMod = UIFont.EndLine(ref stringBuilder);
						if (uitextMod != UITextMod.Replaced)
						{
							if (uitextMod == UITextMod.Added)
							{
								markups.Add(new UITextMarkup
								{
									index = i,
									mod = UITextMod.Added
								});
							}
						}
						else
						{
							markups.Add(new UITextMarkup
							{
								index = i,
								mod = UITextMod.Replaced,
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
						markups.Add(new UITextMarkup
						{
							index = i + k,
							mod = UITextMod.Removed
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

	// Token: 0x0600488E RID: 18574 RVA: 0x001277C8 File Offset: 0x001259C8
	public string WrapText(List<UITextMarkup> markups, string text, float maxWidth, int maxLineCount, bool encoding)
	{
		return this.WrapText(markups, text, maxWidth, maxLineCount, encoding, UIFont.SymbolStyle.None);
	}

	// Token: 0x0600488F RID: 18575 RVA: 0x001277D8 File Offset: 0x001259D8
	public string WrapText(List<UITextMarkup> markups, string text, float maxWidth, int maxLineCount)
	{
		return this.WrapText(markups, text, maxWidth, maxLineCount, false, UIFont.SymbolStyle.None);
	}

	// Token: 0x06004890 RID: 18576 RVA: 0x001277E8 File Offset: 0x001259E8
	private void MangleSort(int len)
	{
		UIFont.mangleSort.SetLineSizing((double)this.bmFont.charSize, (double)this.verticalSpacing);
		Array.Sort<Vector3, int>(UIFont.manglePoints, UIFont.mangleIndices, 0, len, UIFont.mangleSort);
	}

	// Token: 0x06004891 RID: 18577 RVA: 0x00127828 File Offset: 0x00125A28
	private int FillMangle(Vector2[] points, int pointsOffset, UITextPosition[] positions, int positionsOffset, int len)
	{
		if (positions == null || points == null)
		{
			return 0;
		}
		if (points.Length - pointsOffset < len || positions.Length - positionsOffset < len)
		{
			throw new ArgumentOutOfRangeException();
		}
		if (len > UIFont.mangleIndices.Length)
		{
			Array.Resize<Vector3>(ref UIFont.manglePoints, len);
			Array.Resize<int>(ref UIFont.mangleIndices, len);
			Array.Resize<UITextPosition>(ref UIFont.manglePositions, len);
		}
		for (int i = 0; i < len; i++)
		{
			UIFont.manglePoints[i].x = points[i + pointsOffset].x;
			UIFont.manglePoints[i].y = points[i + pointsOffset].y;
			UIFont.manglePoints[i].z = 0f;
			UIFont.mangleIndices[i] = i;
		}
		return len;
	}

	// Token: 0x06004892 RID: 18578 RVA: 0x00127904 File Offset: 0x00125B04
	private int FillMangle(Vector3[] points, int pointsOffset, UITextPosition[] positions, int positionsOffset, int len)
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
		if (len > UIFont.mangleIndices.Length)
		{
			Array.Resize<Vector3>(ref UIFont.manglePoints, len);
			Array.Resize<int>(ref UIFont.mangleIndices, len);
			Array.Resize<UITextPosition>(ref UIFont.manglePositions, len);
		}
		for (int i = 0; i < len; i++)
		{
			UIFont.manglePoints[i] = points[i + pointsOffset];
			UIFont.mangleIndices[i] = i;
		}
		return len;
	}

	// Token: 0x06004893 RID: 18579 RVA: 0x001279D4 File Offset: 0x00125BD4
	private int ProcessShared(int len, ref UITextPosition[] positions, string text)
	{
		if (this.mFont.charSize > 0)
		{
			for (int i = 0; i < len; i++)
			{
				Vector3[] array = UIFont.manglePoints;
				int num = i;
				array[num].x = array[num].x * (float)this.mFont.charSize;
				Vector3[] array2 = UIFont.manglePoints;
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
				positions = new UITextPosition[len];
			}
			for (int j = 0; j < len; j++)
			{
				positions[UIFont.mangleIndices[j]] = UIFont.manglePositions[j];
			}
		}
		return len;
	}

	// Token: 0x06004894 RID: 18580 RVA: 0x00127AA8 File Offset: 0x00125CA8
	[Obsolete("You must specify some point", true)]
	public UITextPosition[] CalculatePlacement(string text)
	{
		return UIFont.empty;
	}

	// Token: 0x06004895 RID: 18581 RVA: 0x00127AB0 File Offset: 0x00125CB0
	private int CalculatePlacement(Vector2[] points, ref UITextPosition[] positions, string text)
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

	// Token: 0x06004896 RID: 18582 RVA: 0x00127B08 File Offset: 0x00125D08
	public int CalculatePlacement(Vector2[] points, UITextPosition[] positions, string text)
	{
		if (positions == null)
		{
			throw new ArgumentNullException("positions");
		}
		return this.CalculatePlacement(points, ref positions, text, base.transform);
	}

	// Token: 0x06004897 RID: 18583 RVA: 0x00127B2C File Offset: 0x00125D2C
	public UITextPosition CalculatePlacement(string text, Vector2 point)
	{
		Vector2[] points = new Vector2[]
		{
			point
		};
		UITextPosition[] array = new UITextPosition[]
		{
			default(UITextPosition)
		};
		this.CalculatePlacement(points, array, text);
		return array[0];
	}

	// Token: 0x06004898 RID: 18584 RVA: 0x00127B80 File Offset: 0x00125D80
	public UITextPosition[] CalculatePlacement(string text, params Vector2[] points)
	{
		UITextPosition[] array = null;
		return (this.CalculatePlacement(points, ref array, text) <= 0) ? UIFont.empty : array;
	}

	// Token: 0x06004899 RID: 18585 RVA: 0x00127BAC File Offset: 0x00125DAC
	private int CalculatePlacement(Vector3[] points, ref UITextPosition[] positions, string text)
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

	// Token: 0x0600489A RID: 18586 RVA: 0x00127C04 File Offset: 0x00125E04
	public int CalculatePlacement(Vector3[] points, UITextPosition[] positions, string text)
	{
		if (positions == null)
		{
			throw new ArgumentNullException("positions");
		}
		return this.CalculatePlacement(points, ref positions, text, base.transform);
	}

	// Token: 0x0600489B RID: 18587 RVA: 0x00127C28 File Offset: 0x00125E28
	public UITextPosition[] CalculatePlacement(string text, params Vector3[] points)
	{
		UITextPosition[] array = null;
		return (this.CalculatePlacement(points, ref array, text) <= 0) ? UIFont.empty : array;
	}

	// Token: 0x0600489C RID: 18588 RVA: 0x00127C54 File Offset: 0x00125E54
	private int CalculatePlacement(Vector3[] points, ref UITextPosition[] positions, string text, Matrix4x4 transform)
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
				UIFont.manglePoints[i] = transform.MultiplyPoint(UIFont.manglePoints[i]);
			}
			return this.ProcessShared(num, ref positions, text);
		}
		return num;
	}

	// Token: 0x0600489D RID: 18589 RVA: 0x00127CDC File Offset: 0x00125EDC
	public int CalculatePlacement(Vector3[] points, UITextPosition[] positions, string text, Matrix4x4 transform)
	{
		if (positions == null)
		{
			throw new ArgumentNullException("positions");
		}
		return this.CalculatePlacement(points, ref positions, text, transform);
	}

	// Token: 0x0600489E RID: 18590 RVA: 0x00127CFC File Offset: 0x00125EFC
	public UITextPosition CalculatePlacement(string text, Matrix4x4 transform, Vector3 point)
	{
		Vector3[] points = new Vector3[]
		{
			point
		};
		UITextPosition[] array = new UITextPosition[]
		{
			default(UITextPosition)
		};
		this.CalculatePlacement(points, array, text, transform);
		return array[0];
	}

	// Token: 0x0600489F RID: 18591 RVA: 0x00127D50 File Offset: 0x00125F50
	public UITextPosition[] CalculatePlacement(string text, Matrix4x4 transform, params Vector3[] points)
	{
		UITextPosition[] array = null;
		if (points == null)
		{
			return null;
		}
		if (points.Length == 0)
		{
			return UIFont.empty;
		}
		return (this.CalculatePlacement(points, ref array, text, transform) <= 0) ? UIFont.empty : array;
	}

	// Token: 0x060048A0 RID: 18592 RVA: 0x00127D94 File Offset: 0x00125F94
	private int CalculatePlacement(Vector2[] points, ref UITextPosition[] positions, string text, Matrix4x4 transform)
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
				UIFont.manglePoints[i] = transform.MultiplyPoint(UIFont.manglePoints[i]);
			}
			return this.ProcessShared(num, ref positions, text);
		}
		return num;
	}

	// Token: 0x060048A1 RID: 18593 RVA: 0x00127E28 File Offset: 0x00126028
	public int CalculatePlacement(Vector2[] points, UITextPosition[] positions, string text, Matrix4x4 transform)
	{
		if (positions == null)
		{
			throw new ArgumentNullException("positions");
		}
		return this.CalculatePlacement(points, ref positions, text, transform);
	}

	// Token: 0x060048A2 RID: 18594 RVA: 0x00127E48 File Offset: 0x00126048
	public UITextPosition[] CalculatePlacement(string text, Matrix4x4 transform, params Vector2[] points)
	{
		UITextPosition[] array = null;
		if (points == null)
		{
			return null;
		}
		if (points.Length == 0)
		{
			return UIFont.empty;
		}
		return (this.CalculatePlacement(points, ref array, text, transform) <= 0) ? UIFont.empty : array;
	}

	// Token: 0x060048A3 RID: 18595 RVA: 0x00127E8C File Offset: 0x0012608C
	private int CalculatePlacement(Vector3[] points, ref UITextPosition[] positions, string text, Transform self)
	{
		if (!self)
		{
			return this.CalculatePlacement(points, positions, text);
		}
		return this.CalculatePlacement(points, positions, text, self.worldToLocalMatrix);
	}

	// Token: 0x060048A4 RID: 18596 RVA: 0x00127EC4 File Offset: 0x001260C4
	public int CalculatePlacement(Vector3[] points, UITextPosition[] positions, string text, Transform self)
	{
		if (positions == null)
		{
			throw new ArgumentNullException("positions");
		}
		return this.CalculatePlacement(points, ref positions, text, base.transform);
	}

	// Token: 0x060048A5 RID: 18597 RVA: 0x00127EE8 File Offset: 0x001260E8
	private int CalculatePlacement(Vector2[] points, ref UITextPosition[] positions, string text, Transform self)
	{
		if (!self)
		{
			return this.CalculatePlacement(points, positions, text);
		}
		return this.CalculatePlacement(points, positions, text, self.worldToLocalMatrix);
	}

	// Token: 0x060048A6 RID: 18598 RVA: 0x00127F20 File Offset: 0x00126120
	public int CalculatePlacement(Vector2[] points, UITextPosition[] positions, string text, Transform self)
	{
		if (positions == null)
		{
			throw new ArgumentNullException("positions");
		}
		return this.CalculatePlacement(points, ref positions, text, base.transform);
	}

	// Token: 0x060048A7 RID: 18599 RVA: 0x00127F44 File Offset: 0x00126144
	public UITextPosition CalculatePlacement(string text, Transform self, Vector2 point)
	{
		Vector2[] points = new Vector2[]
		{
			point
		};
		UITextPosition[] array = new UITextPosition[]
		{
			default(UITextPosition)
		};
		this.CalculatePlacement(points, array, text, self);
		return array[0];
	}

	// Token: 0x060048A8 RID: 18600 RVA: 0x00127F98 File Offset: 0x00126198
	public UITextPosition[] CalculatePlacement(string text, Transform self, params Vector2[] points)
	{
		UITextPosition[] array = null;
		if (points == null)
		{
			return null;
		}
		if (points.Length == 0)
		{
			return UIFont.empty;
		}
		return (this.CalculatePlacement(points, ref array, text, self) <= 0) ? UIFont.empty : array;
	}

	// Token: 0x060048A9 RID: 18601 RVA: 0x00127FDC File Offset: 0x001261DC
	public UITextPosition CalculatePlacement(string text, Vector3 point)
	{
		Vector3[] points = new Vector3[]
		{
			point
		};
		UITextPosition[] array = new UITextPosition[]
		{
			default(UITextPosition)
		};
		this.CalculatePlacement(points, array, text);
		return array[0];
	}

	// Token: 0x060048AA RID: 18602 RVA: 0x00128030 File Offset: 0x00126230
	public UITextPosition CalculatePlacement(string text, Matrix4x4 transform, Vector2 point)
	{
		Vector2[] points = new Vector2[]
		{
			point
		};
		UITextPosition[] array = new UITextPosition[]
		{
			default(UITextPosition)
		};
		this.CalculatePlacement(points, array, text, transform);
		return array[0];
	}

	// Token: 0x060048AB RID: 18603 RVA: 0x00128084 File Offset: 0x00126284
	public UITextPosition CalculatePlacement(string text, Transform self, Vector3 point)
	{
		Vector3[] points = new Vector3[]
		{
			point
		};
		UITextPosition[] array = new UITextPosition[]
		{
			default(UITextPosition)
		};
		this.CalculatePlacement(points, array, text, self);
		return array[0];
	}

	// Token: 0x060048AC RID: 18604 RVA: 0x001280D8 File Offset: 0x001262D8
	public UITextPosition[] CalculatePlacement(string text, Transform self, params Vector3[] points)
	{
		UITextPosition[] array = null;
		if (points == null)
		{
			return null;
		}
		if (points.Length == 0)
		{
			return UIFont.empty;
		}
		return (this.CalculatePlacement(points, ref array, text, self) <= 0) ? UIFont.empty : array;
	}

	// Token: 0x060048AD RID: 18605 RVA: 0x0012811C File Offset: 0x0012631C
	private int ProcessPlacement(int count, string text)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.ProcessPlacement(count, text);
		}
		int i = 0;
		if (UIFont.manglePoints[UIFont.mangleIndices[i]].y < 0f)
		{
			do
			{
				UIFont.manglePositions[i] = new UITextPosition(UITextRegion.Before);
			}
			while (++i < count && UIFont.manglePoints[UIFont.mangleIndices[i]].y < 0f);
			if (i >= count)
			{
				return count;
			}
		}
		int length = text.Length;
		int num = this.verticalSpacing + this.bmFont.charSize;
		if (length == 0)
		{
			while (UIFont.manglePoints[UIFont.mangleIndices[i]].y <= (float)num)
			{
				if (UIFont.manglePoints[UIFont.mangleIndices[i]].x < 0f)
				{
					UIFont.manglePositions[i] = new UITextPosition(UITextRegion.Pre);
				}
				else
				{
					UIFont.manglePositions[i] = new UITextPosition(UITextRegion.Past);
				}
				if (++i >= count)
				{
					return count;
				}
			}
			while (i < count)
			{
				UIFont.manglePositions[i++] = new UITextPosition(UITextRegion.End);
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
			Vector3 vector = UIFont.manglePoints[UIFont.mangleIndices[i]];
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
					UIFont.manglePositions[i++] = new UITextPosition(num2, 0, num5, UITextRegion.Pre);
					goto IL_389;
				}
				if (flag)
				{
					UIFont.manglePositions[i++] = new UITextPosition(num2, column, num6, UITextRegion.Past);
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
					BMGlyph bmglyph;
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
				UIFont.manglePositions[i++] = new UITextPosition(num2, column, num6, UITextRegion.Inside);
				goto IL_389;
			}
			if (num9 == num2)
			{
				UIFont.manglePositions[i++] = new UITextPosition(num2, num3, num4, UITextRegion.Past);
			}
			else
			{
				while (i < count)
				{
					UIFont.manglePositions[i++] = new UITextPosition(num2, num3, num4, UITextRegion.End);
				}
			}
		}
		if (i < count)
		{
			Debug.LogError(" skipped " + (count - i));
		}
		return count;
	}

	// Token: 0x060048AE RID: 18606 RVA: 0x001284D8 File Offset: 0x001266D8
	private void Align(ref UIFont.PrintContext ctx)
	{
		if (this.mFont.charSize > 0)
		{
			int num;
			switch (ctx.alignment)
			{
			case UIFont.Alignment.Left:
				num = 0;
				break;
			case UIFont.Alignment.Center:
				num = Mathf.Max(0, Mathf.RoundToInt((float)(ctx.lineWidth - ctx.x) * 0.5f));
				break;
			case UIFont.Alignment.Right:
				num = Mathf.Max(0, Mathf.RoundToInt((float)(ctx.lineWidth - ctx.x)));
				break;
			case UIFont.Alignment.LeftOverflowRight:
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
				Vertex[] v = ctx.m.v;
				int num3 = i;
				v[num3].x = v[num3].x + num2;
			}
		}
	}

	// Token: 0x060048AF RID: 18607 RVA: 0x001285E0 File Offset: 0x001267E0
	public void Print(string text, Color color, MeshBuffer m, bool encoding, UIFont.SymbolStyle symbolStyle, UIFont.Alignment alignment, int lineWidth)
	{
		UITextSelection uitextSelection = default(UITextSelection);
		this.Print(text, color, m, encoding, symbolStyle, alignment, lineWidth, ref uitextSelection, '\0', color, Color.clear, '\0', -1f);
	}

	// Token: 0x060048B0 RID: 18608 RVA: 0x00128618 File Offset: 0x00126818
	public void Print(string text, Color normalColor, MeshBuffer m, bool encoding, UIFont.SymbolStyle symbolStyle, UIFont.Alignment alignment, int lineWidth, ref UITextSelection selection, char carratChar, Color highlightTextColor, Color highlightBarColor, char highlightChar, float highlightSplit)
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
			UIFont.PrintContext printContext;
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
				printContext.highlight = UIHighlight.invalid;
				printContext.highlightGlyph = null;
			}
			else if (!selection.GetHighlight(out printContext.highlight))
			{
				printContext.highlightGlyph = null;
				printContext.highlightBarDraw = false;
			}
			else if ((printContext.highlightChar != printContext.carratChar) ? (!this.mFont.GetGlyph((int)printContext.highlightChar, out printContext.highlightGlyph)) : ((printContext.highlightGlyph = printContext.carratGlyph) == null))
			{
				printContext.highlight = UIHighlight.invalid;
			}
			printContext.j = 0;
			printContext.previousX = 0;
			printContext.isLineEnd = false;
			printContext.highlightVertex = -1;
			printContext.glyph = null;
			printContext.c = '\0';
			printContext.skipSymbols = (!encoding || symbolStyle == UIFont.SymbolStyle.None);
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
						int num2 = NGUITools.ParseSymbol(text, printContext.i, this.mColors, ref num);
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
						if (symbolStyle == UIFont.SymbolStyle.Colored)
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

	// Token: 0x060048B1 RID: 18609 RVA: 0x001294D8 File Offset: 0x001276D8
	private void PutHighlightStart(ref UIFont.PrintContext ctx)
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

	// Token: 0x060048B2 RID: 18610 RVA: 0x00129744 File Offset: 0x00127944
	private void PutHighlightEnd(ref UIFont.PrintContext ctx)
	{
		if (ctx.highlightVertex == -1)
		{
			return;
		}
		float num = ctx.scale.x * (float)(ctx.previousX + ctx.highlightGlyph.offsetX) - ctx.m.v[ctx.highlightVertex].x;
		if (num > 0f)
		{
			Vertex[] v = ctx.m.v;
			int highlightVertex = ctx.highlightVertex;
			v[highlightVertex].x = v[highlightVertex].x + num;
			Vertex[] v2 = ctx.m.v;
			int num2 = ctx.highlightVertex + 1;
			v2[num2].x = v2[num2].x + num;
			Vertex[] v3 = ctx.m.v;
			int num3 = ctx.highlightVertex + 4;
			v3[num3].x = v3[num3].x + num;
			Vertex[] v4 = ctx.m.v;
			int num4 = ctx.highlightVertex + 4 + 1;
			v4[num4].x = v4[num4].x + num;
			Vertex[] v5 = ctx.m.v;
			int num5 = ctx.highlightVertex + 4 + 2;
			v5[num5].x = v5[num5].x + num;
			Vertex[] v6 = ctx.m.v;
			int num6 = ctx.highlightVertex + 4 + 3;
			v6[num6].x = v6[num6].x + num;
		}
		ctx.highlightVertex = -1;
	}

	// Token: 0x060048B3 RID: 18611 RVA: 0x00129890 File Offset: 0x00127A90
	private void DrawCarat(ref UIFont.PrintContext ctx)
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

	// Token: 0x040028BF RID: 10431
	private const int mangleStartSize = 8;

	// Token: 0x040028C0 RID: 10432
	[SerializeField]
	[HideInInspector]
	private Material mMat;

	// Token: 0x040028C1 RID: 10433
	[SerializeField]
	[HideInInspector]
	private Rect mUVRect = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x040028C2 RID: 10434
	[HideInInspector]
	[SerializeField]
	private BMFont mFont = new BMFont();

	// Token: 0x040028C3 RID: 10435
	[HideInInspector]
	[SerializeField]
	private int mSpacingX;

	// Token: 0x040028C4 RID: 10436
	[SerializeField]
	[HideInInspector]
	private int mSpacingY;

	// Token: 0x040028C5 RID: 10437
	[SerializeField]
	[HideInInspector]
	private UIAtlas mAtlas;

	// Token: 0x040028C6 RID: 10438
	[SerializeField]
	[HideInInspector]
	private UIFont mReplacement;

	// Token: 0x040028C7 RID: 10439
	private UIAtlas.Sprite mSprite;

	// Token: 0x040028C8 RID: 10440
	private bool mSpriteSet;

	// Token: 0x040028C9 RID: 10441
	private List<Color> mColors = new List<Color>();

	// Token: 0x040028CA RID: 10442
	private static List<UITextMarkup> _tempMarkup;

	// Token: 0x040028CB RID: 10443
	private static Vector3[] manglePoints = new Vector3[8];

	// Token: 0x040028CC RID: 10444
	private static int[] mangleIndices = new int[8];

	// Token: 0x040028CD RID: 10445
	private static UITextPosition[] manglePositions = new UITextPosition[8];

	// Token: 0x040028CE RID: 10446
	private static readonly UIFont.MangleSorter mangleSort = new UIFont.MangleSorter();

	// Token: 0x040028CF RID: 10447
	private static readonly UITextPosition[] empty = new UITextPosition[0];

	// Token: 0x020007EC RID: 2028
	public enum Alignment
	{
		// Token: 0x040028D1 RID: 10449
		Left,
		// Token: 0x040028D2 RID: 10450
		Center,
		// Token: 0x040028D3 RID: 10451
		Right,
		// Token: 0x040028D4 RID: 10452
		LeftOverflowRight
	}

	// Token: 0x020007ED RID: 2029
	public enum SymbolStyle
	{
		// Token: 0x040028D6 RID: 10454
		None,
		// Token: 0x040028D7 RID: 10455
		Uncolored,
		// Token: 0x040028D8 RID: 10456
		Colored
	}

	// Token: 0x020007EE RID: 2030
	private class MangleSorter : Comparer<Vector3>
	{
		// Token: 0x060048B5 RID: 18613 RVA: 0x00129A14 File Offset: 0x00127C14
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

		// Token: 0x060048B6 RID: 18614 RVA: 0x00129AC0 File Offset: 0x00127CC0
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

		// Token: 0x040028D9 RID: 10457
		public double lineHeight = 12.0;

		// Token: 0x040028DA RID: 10458
		public double vSpacing = 12.0;

		// Token: 0x040028DB RID: 10459
		private bool noLineSize;

		// Token: 0x040028DC RID: 10460
		private bool noVSpacing;
	}

	// Token: 0x020007EF RID: 2031
	private struct PrintContext
	{
		// Token: 0x040028DD RID: 10461
		public MeshBuffer m;

		// Token: 0x040028DE RID: 10462
		public BMGlyph glyph;

		// Token: 0x040028DF RID: 10463
		public BMGlyph highlightGlyph;

		// Token: 0x040028E0 RID: 10464
		public BMGlyph carratGlyph;

		// Token: 0x040028E1 RID: 10465
		public BMSymbol symbol;

		// Token: 0x040028E2 RID: 10466
		public string text;

		// Token: 0x040028E3 RID: 10467
		public UIHighlight highlight;

		// Token: 0x040028E4 RID: 10468
		public Color printColor;

		// Token: 0x040028E5 RID: 10469
		public Color nonHighlightColor;

		// Token: 0x040028E6 RID: 10470
		public Color normalColor;

		// Token: 0x040028E7 RID: 10471
		public Color highlightTextColor;

		// Token: 0x040028E8 RID: 10472
		public Color highlightBarColor;

		// Token: 0x040028E9 RID: 10473
		public Vector3 v0;

		// Token: 0x040028EA RID: 10474
		public Vector3 v1;

		// Token: 0x040028EB RID: 10475
		public Vector2 u0;

		// Token: 0x040028EC RID: 10476
		public Vector2 u1;

		// Token: 0x040028ED RID: 10477
		public Vector2 scale;

		// Token: 0x040028EE RID: 10478
		public float invX;

		// Token: 0x040028EF RID: 10479
		public float invY;

		// Token: 0x040028F0 RID: 10480
		public float highlightSplit;

		// Token: 0x040028F1 RID: 10481
		public int x;

		// Token: 0x040028F2 RID: 10482
		public int maxX;

		// Token: 0x040028F3 RID: 10483
		public int previousX;

		// Token: 0x040028F4 RID: 10484
		public int y;

		// Token: 0x040028F5 RID: 10485
		public int highlightVertex;

		// Token: 0x040028F6 RID: 10486
		public int prev;

		// Token: 0x040028F7 RID: 10487
		public int lineHeight;

		// Token: 0x040028F8 RID: 10488
		public int lineWidth;

		// Token: 0x040028F9 RID: 10489
		public int indexOffset;

		// Token: 0x040028FA RID: 10490
		public int textLength;

		// Token: 0x040028FB RID: 10491
		public int i;

		// Token: 0x040028FC RID: 10492
		public int carratIndex;

		// Token: 0x040028FD RID: 10493
		public int j;

		// Token: 0x040028FE RID: 10494
		public UIFont.Alignment alignment;

		// Token: 0x040028FF RID: 10495
		public char carratChar;

		// Token: 0x04002900 RID: 10496
		public char highlightChar;

		// Token: 0x04002901 RID: 10497
		public char c;

		// Token: 0x04002902 RID: 10498
		public bool highlightBarDraw;

		// Token: 0x04002903 RID: 10499
		public bool isLineEnd;

		// Token: 0x04002904 RID: 10500
		public bool skipSymbols;

		// Token: 0x04002905 RID: 10501
		public bool printChar;
	}
}
