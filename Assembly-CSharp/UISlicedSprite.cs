using System;
using UnityEngine;

// Token: 0x020008EF RID: 2287
[AddComponentMenu("NGUI/UI/Sprite (Sliced)")]
[ExecuteInEditMode]
public class UISlicedSprite : global::UIGeometricSprite
{
	// Token: 0x06004E1B RID: 19995 RVA: 0x00137E58 File Offset: 0x00136058
	public UISlicedSprite() : this(global::UIWidget.WidgetFlags.CustomBorder)
	{
	}

	// Token: 0x06004E1C RID: 19996 RVA: 0x00137E64 File Offset: 0x00136064
	protected UISlicedSprite(global::UIWidget.WidgetFlags additionalFlags) : base(additionalFlags)
	{
	}

	// Token: 0x17000EED RID: 3821
	// (get) Token: 0x06004E1D RID: 19997 RVA: 0x00137E70 File Offset: 0x00136070
	public new Vector4 border
	{
		get
		{
			global::UIAtlas.Sprite sprite = base.sprite;
			if (sprite == null)
			{
				return Vector4.zero;
			}
			Rect rect = sprite.outer;
			Rect rect2 = sprite.inner;
			Texture mainTexture = base.mainTexture;
			if (base.atlas.coordinates == global::UIAtlas.Coordinates.TexCoords && mainTexture != null)
			{
				rect = global::NGUIMath.ConvertToPixels(rect, mainTexture.width, mainTexture.height, true);
				rect2 = global::NGUIMath.ConvertToPixels(rect2, mainTexture.width, mainTexture.height, true);
			}
			return new Vector4(rect2.xMin - rect.xMin, rect2.yMin - rect.yMin, rect.xMax - rect2.xMax, rect.yMax - rect2.yMax) * base.atlas.pixelSize;
		}
	}

	// Token: 0x17000EEE RID: 3822
	// (get) Token: 0x06004E1E RID: 19998 RVA: 0x00137F3C File Offset: 0x0013613C
	protected override Vector4 customBorder
	{
		get
		{
			return this.border;
		}
	}
}
