using System;
using UnityEngine;

// Token: 0x0200080A RID: 2058
[global::dfMarkupTagInfo("img")]
public class dfMarkupTagImg : global::dfMarkupTag
{
	// Token: 0x06004749 RID: 18249 RVA: 0x0010DB70 File Offset: 0x0010BD70
	public dfMarkupTagImg() : base("img")
	{
		this.IsClosedTag = true;
	}

	// Token: 0x0600474A RID: 18250 RVA: 0x0010DB84 File Offset: 0x0010BD84
	public dfMarkupTagImg(global::dfMarkupTag original) : base(original)
	{
		this.IsClosedTag = true;
	}

	// Token: 0x0600474B RID: 18251 RVA: 0x0010DB94 File Offset: 0x0010BD94
	protected override void _PerformLayoutImpl(global::dfMarkupBox container, global::dfMarkupStyle style)
	{
		if (base.Owner == null)
		{
			Debug.LogError("Tag has no parent: " + this);
			return;
		}
		style = this.applyStyleAttributes(style);
		global::dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
		{
			"src"
		});
		if (dfMarkupAttribute == null)
		{
			return;
		}
		string value = dfMarkupAttribute.Value;
		global::dfMarkupBox dfMarkupBox = this.createImageBox(base.Owner.Atlas, value, style);
		if (dfMarkupBox == null)
		{
			return;
		}
		Vector2 size = Vector2.zero;
		global::dfMarkupAttribute dfMarkupAttribute2 = base.findAttribute(new string[]
		{
			"height"
		});
		if (dfMarkupAttribute2 != null)
		{
			size.y = (float)global::dfMarkupStyle.ParseSize(dfMarkupAttribute2.Value, (int)dfMarkupBox.Size.y);
		}
		global::dfMarkupAttribute dfMarkupAttribute3 = base.findAttribute(new string[]
		{
			"width"
		});
		if (dfMarkupAttribute3 != null)
		{
			size.x = (float)global::dfMarkupStyle.ParseSize(dfMarkupAttribute3.Value, (int)dfMarkupBox.Size.x);
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

	// Token: 0x0600474C RID: 18252 RVA: 0x0010DD38 File Offset: 0x0010BF38
	private global::dfMarkupStyle applyStyleAttributes(global::dfMarkupStyle style)
	{
		global::dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
		{
			"valign"
		});
		if (dfMarkupAttribute != null)
		{
			style.VerticalAlign = global::dfMarkupStyle.ParseVerticalAlignment(dfMarkupAttribute.Value);
		}
		global::dfMarkupAttribute dfMarkupAttribute2 = base.findAttribute(new string[]
		{
			"color"
		});
		if (dfMarkupAttribute2 != null)
		{
			Color color = global::dfMarkupStyle.ParseColor(dfMarkupAttribute2.Value, base.Owner.Color);
			color.a = style.Opacity;
			style.Color = color;
		}
		return style;
	}

	// Token: 0x0600474D RID: 18253 RVA: 0x0010DDC0 File Offset: 0x0010BFC0
	private global::dfMarkupBox createImageBox(global::dfAtlas atlas, string source, global::dfMarkupStyle style)
	{
		if (source.ToLowerInvariant().StartsWith("http://"))
		{
			return null;
		}
		if (atlas != null && atlas[source] != null)
		{
			global::dfMarkupBoxSprite dfMarkupBoxSprite = new global::dfMarkupBoxSprite(this, global::dfMarkupDisplayType.inline, style);
			dfMarkupBoxSprite.LoadImage(atlas, source);
			return dfMarkupBoxSprite;
		}
		Texture texture = global::dfMarkupImageCache.Load(source);
		if (texture != null)
		{
			global::dfMarkupBoxTexture dfMarkupBoxTexture = new global::dfMarkupBoxTexture(this, global::dfMarkupDisplayType.inline, style);
			dfMarkupBoxTexture.LoadTexture(texture);
			return dfMarkupBoxTexture;
		}
		return null;
	}
}
