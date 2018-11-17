using System;
using UnityEngine;

// Token: 0x0200072E RID: 1838
[dfMarkupTagInfo("img")]
public class dfMarkupTagImg : dfMarkupTag
{
	// Token: 0x06004305 RID: 17157 RVA: 0x00104860 File Offset: 0x00102A60
	public dfMarkupTagImg() : base("img")
	{
		this.IsClosedTag = true;
	}

	// Token: 0x06004306 RID: 17158 RVA: 0x00104874 File Offset: 0x00102A74
	public dfMarkupTagImg(dfMarkupTag original) : base(original)
	{
		this.IsClosedTag = true;
	}

	// Token: 0x06004307 RID: 17159 RVA: 0x00104884 File Offset: 0x00102A84
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		if (base.Owner == null)
		{
			Debug.LogError("Tag has no parent: " + this);
			return;
		}
		style = this.applyStyleAttributes(style);
		dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
		{
			"src"
		});
		if (dfMarkupAttribute == null)
		{
			return;
		}
		string value = dfMarkupAttribute.Value;
		dfMarkupBox dfMarkupBox = this.createImageBox(base.Owner.Atlas, value, style);
		if (dfMarkupBox == null)
		{
			return;
		}
		Vector2 size = Vector2.zero;
		dfMarkupAttribute dfMarkupAttribute2 = base.findAttribute(new string[]
		{
			"height"
		});
		if (dfMarkupAttribute2 != null)
		{
			size.y = (float)dfMarkupStyle.ParseSize(dfMarkupAttribute2.Value, (int)dfMarkupBox.Size.y);
		}
		dfMarkupAttribute dfMarkupAttribute3 = base.findAttribute(new string[]
		{
			"width"
		});
		if (dfMarkupAttribute3 != null)
		{
			size.x = (float)dfMarkupStyle.ParseSize(dfMarkupAttribute3.Value, (int)dfMarkupBox.Size.x);
		}
		if (size.sqrMagnitude <= 1.401298E-45f)
		{
			size = dfMarkupBox.Size;
		}
		else if (size.x <= 1.401298E-45f)
		{
			size.x = size.y * (dfMarkupBox.Size.x / dfMarkupBox.Size.y);
		}
		else if (size.y <= 1.401298E-45f)
		{
			size.y = size.x * (dfMarkupBox.Size.y / dfMarkupBox.Size.x);
		}
		dfMarkupBox.Size = size;
		dfMarkupBox.Baseline = (int)size.y;
		container.AddChild(dfMarkupBox);
	}

	// Token: 0x06004308 RID: 17160 RVA: 0x00104A28 File Offset: 0x00102C28
	private dfMarkupStyle applyStyleAttributes(dfMarkupStyle style)
	{
		dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
		{
			"valign"
		});
		if (dfMarkupAttribute != null)
		{
			style.VerticalAlign = dfMarkupStyle.ParseVerticalAlignment(dfMarkupAttribute.Value);
		}
		dfMarkupAttribute dfMarkupAttribute2 = base.findAttribute(new string[]
		{
			"color"
		});
		if (dfMarkupAttribute2 != null)
		{
			Color color = dfMarkupStyle.ParseColor(dfMarkupAttribute2.Value, base.Owner.Color);
			color.a = style.Opacity;
			style.Color = color;
		}
		return style;
	}

	// Token: 0x06004309 RID: 17161 RVA: 0x00104AB0 File Offset: 0x00102CB0
	private dfMarkupBox createImageBox(dfAtlas atlas, string source, dfMarkupStyle style)
	{
		if (source.ToLowerInvariant().StartsWith("http://"))
		{
			return null;
		}
		if (atlas != null && atlas[source] != null)
		{
			dfMarkupBoxSprite dfMarkupBoxSprite = new dfMarkupBoxSprite(this, dfMarkupDisplayType.inline, style);
			dfMarkupBoxSprite.LoadImage(atlas, source);
			return dfMarkupBoxSprite;
		}
		Texture texture = dfMarkupImageCache.Load(source);
		if (texture != null)
		{
			dfMarkupBoxTexture dfMarkupBoxTexture = new dfMarkupBoxTexture(this, dfMarkupDisplayType.inline, style);
			dfMarkupBoxTexture.LoadTexture(texture);
			return dfMarkupBoxTexture;
		}
		return null;
	}
}
