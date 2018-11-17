using System;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x0200080B RID: 2059
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Sprite (Tiled)")]
public class UITiledSprite : UIGeometricSprite
{
	// Token: 0x060049CE RID: 18894 RVA: 0x0013A7AC File Offset: 0x001389AC
	public UITiledSprite() : base((UIWidget.WidgetFlags)0)
	{
	}

	// Token: 0x060049CF RID: 18895 RVA: 0x0013A7B8 File Offset: 0x001389B8
	public override void MakePixelPerfect()
	{
		Vector3 localPosition = base.cachedTransform.localPosition;
		localPosition.x = (float)Mathf.RoundToInt(localPosition.x);
		localPosition.y = (float)Mathf.RoundToInt(localPosition.y);
		localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
		base.cachedTransform.localPosition = localPosition;
		Vector3 localScale = base.cachedTransform.localScale;
		localScale.x = (float)Mathf.RoundToInt(localScale.x);
		localScale.y = (float)Mathf.RoundToInt(localScale.y);
		localScale.z = 1f;
		base.cachedTransform.localScale = localScale;
	}

	// Token: 0x060049D0 RID: 18896 RVA: 0x0013A868 File Offset: 0x00138A68
	public override void OnFill(MeshBuffer m)
	{
		Texture mainTexture = base.material.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		Rect rect = this.mInner;
		if (base.atlas.coordinates == UIAtlas.Coordinates.TexCoords)
		{
			rect = NGUIMath.ConvertToPixels(rect, mainTexture.width, mainTexture.height, true);
		}
		Vector2 vector = base.cachedTransform.localScale;
		float pixelSize = base.atlas.pixelSize;
		float num = Mathf.Abs(rect.width / vector.x) * pixelSize;
		float num2 = Mathf.Abs(rect.height / vector.y) * pixelSize;
		if (num < 0.01f || num2 < 0.01f)
		{
			Debug.LogWarning("The tiled sprite (" + NGUITools.GetHierarchy(base.gameObject) + ") is too small.\nConsider using a bigger one.");
			num = 0.01f;
			num2 = 0.01f;
		}
		Vector2 vector2;
		vector2..ctor(rect.xMin / (float)mainTexture.width, rect.yMin / (float)mainTexture.height);
		Vector2 vector3;
		vector3..ctor(rect.xMax / (float)mainTexture.width, rect.yMax / (float)mainTexture.height);
		Vector2 vector4 = vector3;
		float num3 = 0f;
		Color color = base.color;
		Vertex a;
		Vertex b;
		Vertex c;
		Vertex d;
		a.r = (b.r = (c.r = (d.r = color.r)));
		a.g = (b.g = (c.g = (d.g = color.g)));
		a.b = (b.b = (c.b = (d.b = color.b)));
		a.a = (b.a = (c.a = (d.a = color.a)));
		a.z = (b.z = (c.z = (d.z = 0f)));
		while (num3 < 1f)
		{
			float num4 = 0f;
			vector4.x = vector3.x;
			float num5 = num3 + num2;
			if (num5 > 1f)
			{
				vector4.y = vector2.y + (vector3.y - vector2.y) * (1f - num3) / (num5 - num3);
				num5 = 1f;
			}
			while (num4 < 1f)
			{
				float num6 = num4 + num;
				if (num6 > 1f)
				{
					vector4.x = vector2.x + (vector3.x - vector2.x) * (1f - num4) / (num6 - num4);
					num6 = 1f;
				}
				a.x = num6;
				a.y = -num3;
				b.x = num6;
				b.y = -num5;
				c.x = num4;
				c.y = -num5;
				d.x = num4;
				d.y = -num3;
				a.u = vector4.x;
				a.v = 1f - vector2.y;
				b.u = vector4.x;
				b.v = 1f - vector4.y;
				c.u = vector2.x;
				c.v = 1f - vector4.y;
				d.u = vector2.x;
				d.v = 1f - vector2.y;
				m.Quad(a, b, c, d);
				num4 += num;
			}
			num3 += num2;
		}
	}
}
