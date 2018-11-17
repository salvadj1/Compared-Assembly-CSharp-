using System;
using UnityEngine;

// Token: 0x020007FD RID: 2045
[AddComponentMenu("NGUI/UI/Sprite (Sliced)")]
[ExecuteInEditMode]
public class UISlicedSprite : UIGeometricSprite
{
	// Token: 0x0600496C RID: 18796 RVA: 0x0012DEF4 File Offset: 0x0012C0F4
	public UISlicedSprite() : this(UIWidget.WidgetFlags.CustomBorder)
	{
	}

	// Token: 0x0600496D RID: 18797 RVA: 0x0012DF00 File Offset: 0x0012C100
	protected UISlicedSprite(UIWidget.WidgetFlags additionalFlags) : base(additionalFlags)
	{
	}

	// Token: 0x17000E53 RID: 3667
	// (get) Token: 0x0600496E RID: 18798 RVA: 0x0012DF0C File Offset: 0x0012C10C
	public new Vector4 border
	{
		get
		{
			UIAtlas.Sprite sprite = base.sprite;
			if (sprite == null)
			{
				return Vector4.zero;
			}
			Rect rect = sprite.outer;
			Rect rect2 = sprite.inner;
			Texture mainTexture = base.mainTexture;
			if (base.atlas.coordinates == UIAtlas.Coordinates.TexCoords && mainTexture != null)
			{
				rect = NGUIMath.ConvertToPixels(rect, mainTexture.width, mainTexture.height, true);
				rect2 = NGUIMath.ConvertToPixels(rect2, mainTexture.width, mainTexture.height, true);
			}
			return new Vector4(rect2.xMin - rect.xMin, rect2.yMin - rect.yMin, rect.xMax - rect2.xMax, rect.yMax - rect2.yMax) * base.atlas.pixelSize;
		}
	}

	// Token: 0x17000E54 RID: 3668
	// (get) Token: 0x0600496F RID: 18799 RVA: 0x0012DFD8 File Offset: 0x0012C1D8
	protected override Vector4 customBorder
	{
		get
		{
			return this.border;
		}
	}
}
