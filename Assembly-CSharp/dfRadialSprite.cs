using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006E9 RID: 1769
[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Daikon Forge/User Interface/Sprite/Radial")]
[Serializable]
public class dfRadialSprite : dfSprite
{
	// Token: 0x17000C6D RID: 3181
	// (get) Token: 0x06003F5E RID: 16222 RVA: 0x000F10F4 File Offset: 0x000EF2F4
	// (set) Token: 0x06003F5F RID: 16223 RVA: 0x000F10FC File Offset: 0x000EF2FC
	public dfPivotPoint FillOrigin
	{
		get
		{
			return this.fillOrigin;
		}
		set
		{
			if (value != this.fillOrigin)
			{
				this.fillOrigin = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x06003F60 RID: 16224 RVA: 0x000F1118 File Offset: 0x000EF318
	protected override void OnRebuildRenderData()
	{
		if (base.Atlas == null)
		{
			return;
		}
		dfAtlas.ItemInfo spriteInfo = base.SpriteInfo;
		if (spriteInfo == null)
		{
			return;
		}
		this.renderData.Material = base.Atlas.Material;
		List<Vector3> list = null;
		List<int> list2 = null;
		List<Vector2> list3 = null;
		this.buildMeshData(ref list, ref list2, ref list3);
		Color32[] list4 = this.buildColors(list.Count);
		this.renderData.Vertices.AddRange(list);
		this.renderData.Triangles.AddRange(list2);
		this.renderData.UV.AddRange(list3);
		this.renderData.Colors.AddRange(list4);
	}

	// Token: 0x06003F61 RID: 16225 RVA: 0x000F11C8 File Offset: 0x000EF3C8
	private void buildMeshData(ref List<Vector3> verts, ref List<int> indices, ref List<Vector2> uv)
	{
		List<Vector3> list;
		verts = (list = new List<Vector3>());
		List<Vector3> list2 = list;
		verts.AddRange(dfRadialSprite.baseVerts);
		int num;
		int index;
		switch (this.fillOrigin)
		{
		case dfPivotPoint.TopLeft:
			num = 4;
			index = 5;
			list2.RemoveAt(6);
			list2.RemoveAt(0);
			break;
		case dfPivotPoint.TopCenter:
			num = 6;
			index = 0;
			break;
		case dfPivotPoint.TopRight:
			num = 4;
			index = 0;
			list2.RemoveAt(2);
			list2.RemoveAt(0);
			break;
		case dfPivotPoint.MiddleLeft:
			num = 6;
			index = 6;
			break;
		case dfPivotPoint.MiddleCenter:
			num = 8;
			list2.Add(list2[0]);
			list2.Insert(0, Vector3.zero);
			index = 0;
			break;
		case dfPivotPoint.MiddleRight:
			num = 6;
			index = 2;
			break;
		case dfPivotPoint.BottomLeft:
			num = 4;
			index = 4;
			list2.RemoveAt(6);
			list2.RemoveAt(4);
			break;
		case dfPivotPoint.BottomCenter:
			num = 6;
			index = 4;
			break;
		case dfPivotPoint.BottomRight:
			num = 4;
			index = 2;
			list2.RemoveAt(4);
			list2.RemoveAt(2);
			break;
		default:
			throw new NotImplementedException();
		}
		this.makeFirst(list2, index);
		List<int> list3;
		indices = (list3 = this.buildTriangles(list2));
		List<int> list4 = list3;
		float num2 = 1f / (float)num;
		float num3 = this.fillAmount.Quantize(num2);
		int num4 = Mathf.CeilToInt(num3 / num2) + 1;
		for (int i = num4; i < num; i++)
		{
			if (this.invertFill)
			{
				list4.RemoveRange(0, 3);
			}
			else
			{
				list2.RemoveAt(list2.Count - 1);
				list4.RemoveRange(list4.Count - 3, 3);
			}
		}
		if (this.fillAmount < 1f)
		{
			int index2 = list4[(!this.invertFill) ? (list4.Count - 2) : 2];
			int index3 = list4[(!this.invertFill) ? (list4.Count - 1) : 1];
			float num5 = (base.FillAmount - num3) / num2;
			list2[index3] = Vector3.Lerp(list2[index2], list2[index3], num5);
		}
		uv = this.buildUV(list2);
		float num6 = base.PixelsToUnits();
		Vector2 vector = num6 * this.size;
		Vector3 vector2 = this.pivot.TransformToCenter(this.size) * num6;
		for (int j = 0; j < list2.Count; j++)
		{
			list2[j] = Vector3.Scale(list2[j], vector) + vector2;
		}
	}

	// Token: 0x06003F62 RID: 16226 RVA: 0x000F1454 File Offset: 0x000EF654
	private void makeFirst(List<Vector3> list, int index)
	{
		if (index == 0)
		{
			return;
		}
		List<Vector3> range = list.GetRange(index, list.Count - index);
		list.RemoveRange(index, list.Count - index);
		list.InsertRange(0, range);
	}

	// Token: 0x06003F63 RID: 16227 RVA: 0x000F1490 File Offset: 0x000EF690
	private List<int> buildTriangles(List<Vector3> verts)
	{
		List<int> list = new List<int>();
		int count = verts.Count;
		for (int i = 1; i < count - 1; i++)
		{
			list.Add(0);
			list.Add(i);
			list.Add(i + 1);
		}
		return list;
	}

	// Token: 0x06003F64 RID: 16228 RVA: 0x000F14D8 File Offset: 0x000EF6D8
	private List<Vector2> buildUV(List<Vector3> verts)
	{
		dfAtlas.ItemInfo spriteInfo = base.SpriteInfo;
		if (spriteInfo == null)
		{
			return null;
		}
		Rect region = spriteInfo.region;
		if (this.flip.IsSet(dfSpriteFlip.FlipHorizontal))
		{
			region..ctor(region.xMax, region.y, -region.width, region.height);
		}
		if (this.flip.IsSet(dfSpriteFlip.FlipVertical))
		{
			region..ctor(region.x, region.yMax, region.width, -region.height);
		}
		Vector2 vector;
		vector..ctor(region.x, region.y);
		Vector2 vector2;
		vector2..ctor(0.5f, 0.5f);
		Vector2 vector3;
		vector3..ctor(region.width, region.height);
		List<Vector2> list = new List<Vector2>(verts.Count);
		for (int i = 0; i < verts.Count; i++)
		{
			Vector2 vector4 = verts[i] + vector2;
			list.Add(Vector2.Scale(vector4, vector3) + vector);
		}
		return list;
	}

	// Token: 0x06003F65 RID: 16229 RVA: 0x000F15FC File Offset: 0x000EF7FC
	private Color32[] buildColors(int vertCount)
	{
		Color32 color = base.ApplyOpacity((!base.IsEnabled) ? this.disabledColor : this.color);
		Color32[] array = new Color32[vertCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = color;
		}
		return array;
	}

	// Token: 0x040021D1 RID: 8657
	private static Vector3[] baseVerts = new Vector3[]
	{
		new Vector3(0f, 0.5f, 0f),
		new Vector3(0.5f, 0.5f, 0f),
		new Vector3(0.5f, 0f, 0f),
		new Vector3(0.5f, -0.5f, 0f),
		new Vector3(0f, -0.5f, 0f),
		new Vector3(-0.5f, -0.5f, 0f),
		new Vector3(-0.5f, 0f, 0f),
		new Vector3(-0.5f, 0.5f, 0f)
	};

	// Token: 0x040021D2 RID: 8658
	[SerializeField]
	protected dfPivotPoint fillOrigin = dfPivotPoint.MiddleCenter;
}
