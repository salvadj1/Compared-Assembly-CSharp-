using System;
using UnityEngine;

// Token: 0x020006F1 RID: 1777
[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Daikon Forge/User Interface/Sprite/Sliced")]
[Serializable]
public class dfSlicedSprite : dfSprite
{
	// Token: 0x0600401A RID: 16410 RVA: 0x000F4EE8 File Offset: 0x000F30E8
	// Note: this type is marked as 'beforefieldinit'.
	static dfSlicedSprite()
	{
		int[][] array = new int[4][];
		int num = 0;
		int[] array2 = new int[4];
		array2[0] = 11;
		array2[1] = 8;
		array2[2] = 3;
		array[num] = array2;
		array[1] = new int[]
		{
			10,
			9,
			2,
			1
		};
		array[2] = new int[]
		{
			15,
			12,
			7,
			4
		};
		array[3] = new int[]
		{
			14,
			13,
			6,
			5
		};
		dfSlicedSprite.vertFill = array;
		dfSlicedSprite.fillIndices = new int[][]
		{
			new int[4],
			new int[4],
			new int[4],
			new int[4]
		};
		dfSlicedSprite.verts = new Vector3[16];
		dfSlicedSprite.uv = new Vector2[16];
	}

	// Token: 0x0600401B RID: 16411 RVA: 0x000F500C File Offset: 0x000F320C
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
		if (spriteInfo.border.horizontal == 0 && spriteInfo.border.vertical == 0)
		{
			base.OnRebuildRenderData();
			return;
		}
		Color32 color = base.ApplyOpacity((!base.IsEnabled) ? this.disabledColor : this.color);
		dfSprite.RenderOptions options = new dfSprite.RenderOptions
		{
			atlas = this.atlas,
			color = color,
			fillAmount = this.fillAmount,
			fillDirection = this.fillDirection,
			flip = this.flip,
			invertFill = this.invertFill,
			offset = this.pivot.TransformToUpperLeft(base.Size),
			pixelsToUnits = base.PixelsToUnits(),
			size = base.Size,
			spriteInfo = base.SpriteInfo
		};
		dfSlicedSprite.renderSprite(this.renderData, options);
	}

	// Token: 0x0600401C RID: 16412 RVA: 0x000F5140 File Offset: 0x000F3340
	internal new static void renderSprite(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		options.baseIndex = renderData.Vertices.Count;
		dfSlicedSprite.rebuildTriangles(renderData, options);
		dfSlicedSprite.rebuildVertices(renderData, options);
		dfSlicedSprite.rebuildUV(renderData, options);
		dfSlicedSprite.rebuildColors(renderData, options);
		if (options.fillAmount < 1f)
		{
			dfSlicedSprite.doFill(renderData, options);
		}
	}

	// Token: 0x0600401D RID: 16413 RVA: 0x000F5194 File Offset: 0x000F3394
	private static void rebuildTriangles(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		int baseIndex = options.baseIndex;
		dfList<int> triangles = renderData.Triangles;
		for (int i = 0; i < dfSlicedSprite.triangleIndices.Length; i++)
		{
			triangles.Add(baseIndex + dfSlicedSprite.triangleIndices[i]);
		}
	}

	// Token: 0x0600401E RID: 16414 RVA: 0x000F51D8 File Offset: 0x000F33D8
	private static void doFill(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		int baseIndex = options.baseIndex;
		dfList<Vector3> vertices = renderData.Vertices;
		dfList<Vector2> dfList = renderData.UV;
		int[][] array = dfSlicedSprite.getFillIndices(options.fillDirection, baseIndex);
		bool flag = options.invertFill;
		if (options.fillDirection == dfFillDirection.Vertical)
		{
			flag = !flag;
		}
		if (flag)
		{
			for (int i = 0; i < array.Length; i++)
			{
				Array.Reverse(array[i]);
			}
		}
		int num = (options.fillDirection != dfFillDirection.Horizontal) ? 1 : 0;
		float num2 = vertices[array[0][flag ? 3 : 0]][num];
		float num3 = vertices[array[0][flag ? 0 : 3]][num];
		float num4 = Mathf.Abs(num3 - num2);
		float num5 = flag ? (num3 - options.fillAmount * num4) : (num2 + options.fillAmount * num4);
		for (int j = 0; j < array.Length; j++)
		{
			if (!flag)
			{
				for (int k = 3; k > 0; k--)
				{
					float num6 = vertices[array[j][k]][num];
					if (num6 >= num5)
					{
						Vector3 value = vertices[array[j][k]];
						value[num] = num5;
						vertices[array[j][k]] = value;
						float num7 = vertices[array[j][k - 1]][num];
						if (num7 <= num5)
						{
							float num8 = num6 - num7;
							float num9 = (num5 - num7) / num8;
							float num10 = dfList[array[j][k]][num];
							float num11 = dfList[array[j][k - 1]][num];
							Vector2 value2 = dfList[array[j][k]];
							value2[num] = Mathf.Lerp(num11, num10, num9);
							dfList[array[j][k]] = value2;
						}
					}
				}
			}
			else
			{
				for (int l = 1; l < 4; l++)
				{
					float num12 = vertices[array[j][l]][num];
					if (num12 <= num5)
					{
						Vector3 value3 = vertices[array[j][l]];
						value3[num] = num5;
						vertices[array[j][l]] = value3;
						float num13 = vertices[array[j][l - 1]][num];
						if (num13 >= num5)
						{
							float num14 = num12 - num13;
							float num15 = (num5 - num13) / num14;
							float num16 = dfList[array[j][l]][num];
							float num17 = dfList[array[j][l - 1]][num];
							Vector2 value4 = dfList[array[j][l]];
							value4[num] = Mathf.Lerp(num17, num16, num15);
							dfList[array[j][l]] = value4;
						}
					}
				}
			}
		}
	}

	// Token: 0x0600401F RID: 16415 RVA: 0x000F551C File Offset: 0x000F371C
	private static int[][] getFillIndices(dfFillDirection fillDirection, int baseIndex)
	{
		int[][] array = (fillDirection != dfFillDirection.Horizontal) ? dfSlicedSprite.vertFill : dfSlicedSprite.horzFill;
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				dfSlicedSprite.fillIndices[i][j] = baseIndex + array[i][j];
			}
		}
		return dfSlicedSprite.fillIndices;
	}

	// Token: 0x06004020 RID: 16416 RVA: 0x000F5578 File Offset: 0x000F3778
	private static void rebuildVertices(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = Mathf.Ceil(options.size.x);
		float num4 = Mathf.Ceil(-options.size.y);
		dfAtlas.ItemInfo spriteInfo = options.spriteInfo;
		float num5 = (float)spriteInfo.border.left;
		float num6 = (float)spriteInfo.border.top;
		float num7 = (float)spriteInfo.border.right;
		float num8 = (float)spriteInfo.border.bottom;
		if (options.flip.IsSet(dfSpriteFlip.FlipHorizontal))
		{
			float num9 = num7;
			num7 = num5;
			num5 = num9;
		}
		if (options.flip.IsSet(dfSpriteFlip.FlipVertical))
		{
			float num10 = num8;
			num8 = num6;
			num6 = num10;
		}
		dfSlicedSprite.verts[0] = new Vector3(num, num2, 0f) + options.offset;
		dfSlicedSprite.verts[1] = dfSlicedSprite.verts[0] + new Vector3(num5, 0f, 0f);
		dfSlicedSprite.verts[2] = dfSlicedSprite.verts[0] + new Vector3(num5, -num6, 0f);
		dfSlicedSprite.verts[3] = dfSlicedSprite.verts[0] + new Vector3(0f, -num6, 0f);
		dfSlicedSprite.verts[4] = new Vector3(num3 - num7, num2, 0f) + options.offset;
		dfSlicedSprite.verts[5] = dfSlicedSprite.verts[4] + new Vector3(num7, 0f, 0f);
		dfSlicedSprite.verts[6] = dfSlicedSprite.verts[4] + new Vector3(num7, -num6, 0f);
		dfSlicedSprite.verts[7] = dfSlicedSprite.verts[4] + new Vector3(0f, -num6, 0f);
		dfSlicedSprite.verts[8] = new Vector3(num, num4 + num8, 0f) + options.offset;
		dfSlicedSprite.verts[9] = dfSlicedSprite.verts[8] + new Vector3(num5, 0f, 0f);
		dfSlicedSprite.verts[10] = dfSlicedSprite.verts[8] + new Vector3(num5, -num8, 0f);
		dfSlicedSprite.verts[11] = dfSlicedSprite.verts[8] + new Vector3(0f, -num8, 0f);
		dfSlicedSprite.verts[12] = new Vector3(num3 - num7, num4 + num8, 0f) + options.offset;
		dfSlicedSprite.verts[13] = dfSlicedSprite.verts[12] + new Vector3(num7, 0f, 0f);
		dfSlicedSprite.verts[14] = dfSlicedSprite.verts[12] + new Vector3(num7, -num8, 0f);
		dfSlicedSprite.verts[15] = dfSlicedSprite.verts[12] + new Vector3(0f, -num8, 0f);
		for (int i = 0; i < dfSlicedSprite.verts.Length; i++)
		{
			renderData.Vertices.Add((dfSlicedSprite.verts[i] * options.pixelsToUnits).Quantize(options.pixelsToUnits));
		}
	}

	// Token: 0x06004021 RID: 16417 RVA: 0x000F59C4 File Offset: 0x000F3BC4
	private static void rebuildUV(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		dfAtlas atlas = options.atlas;
		Vector2 vector;
		vector..ctor((float)atlas.Texture.width, (float)atlas.Texture.height);
		dfAtlas.ItemInfo spriteInfo = options.spriteInfo;
		float num = (float)spriteInfo.border.top / vector.y;
		float num2 = (float)spriteInfo.border.bottom / vector.y;
		float num3 = (float)spriteInfo.border.left / vector.x;
		float num4 = (float)spriteInfo.border.right / vector.x;
		Rect region = spriteInfo.region;
		dfSlicedSprite.uv[0] = new Vector2(region.x, region.yMax);
		dfSlicedSprite.uv[1] = new Vector2(region.x + num3, region.yMax);
		dfSlicedSprite.uv[2] = new Vector2(region.x + num3, region.yMax - num);
		dfSlicedSprite.uv[3] = new Vector2(region.x, region.yMax - num);
		dfSlicedSprite.uv[4] = new Vector2(region.xMax - num4, region.yMax);
		dfSlicedSprite.uv[5] = new Vector2(region.xMax, region.yMax);
		dfSlicedSprite.uv[6] = new Vector2(region.xMax, region.yMax - num);
		dfSlicedSprite.uv[7] = new Vector2(region.xMax - num4, region.yMax - num);
		dfSlicedSprite.uv[8] = new Vector2(region.x, region.y + num2);
		dfSlicedSprite.uv[9] = new Vector2(region.x + num3, region.y + num2);
		dfSlicedSprite.uv[10] = new Vector2(region.x + num3, region.y);
		dfSlicedSprite.uv[11] = new Vector2(region.x, region.y);
		dfSlicedSprite.uv[12] = new Vector2(region.xMax - num4, region.y + num2);
		dfSlicedSprite.uv[13] = new Vector2(region.xMax, region.y + num2);
		dfSlicedSprite.uv[14] = new Vector2(region.xMax, region.y);
		dfSlicedSprite.uv[15] = new Vector2(region.xMax - num4, region.y);
		if (options.flip != dfSpriteFlip.None)
		{
			for (int i = 0; i < dfSlicedSprite.uv.Length; i += 4)
			{
				Vector2 vector2 = Vector2.zero;
				if (options.flip.IsSet(dfSpriteFlip.FlipHorizontal))
				{
					vector2 = dfSlicedSprite.uv[i];
					dfSlicedSprite.uv[i] = dfSlicedSprite.uv[i + 1];
					dfSlicedSprite.uv[i + 1] = vector2;
					vector2 = dfSlicedSprite.uv[i + 2];
					dfSlicedSprite.uv[i + 2] = dfSlicedSprite.uv[i + 3];
					dfSlicedSprite.uv[i + 3] = vector2;
				}
				if (options.flip.IsSet(dfSpriteFlip.FlipVertical))
				{
					vector2 = dfSlicedSprite.uv[i];
					dfSlicedSprite.uv[i] = dfSlicedSprite.uv[i + 3];
					dfSlicedSprite.uv[i + 3] = vector2;
					vector2 = dfSlicedSprite.uv[i + 1];
					dfSlicedSprite.uv[i + 1] = dfSlicedSprite.uv[i + 2];
					dfSlicedSprite.uv[i + 2] = vector2;
				}
			}
			if (options.flip.IsSet(dfSpriteFlip.FlipHorizontal))
			{
				Vector2[] array = new Vector2[dfSlicedSprite.uv.Length];
				Array.Copy(dfSlicedSprite.uv, array, dfSlicedSprite.uv.Length);
				Array.Copy(dfSlicedSprite.uv, 0, dfSlicedSprite.uv, 4, 4);
				Array.Copy(array, 4, dfSlicedSprite.uv, 0, 4);
				Array.Copy(dfSlicedSprite.uv, 8, dfSlicedSprite.uv, 12, 4);
				Array.Copy(array, 12, dfSlicedSprite.uv, 8, 4);
			}
			if (options.flip.IsSet(dfSpriteFlip.FlipVertical))
			{
				Vector2[] array2 = new Vector2[dfSlicedSprite.uv.Length];
				Array.Copy(dfSlicedSprite.uv, array2, dfSlicedSprite.uv.Length);
				Array.Copy(dfSlicedSprite.uv, 0, dfSlicedSprite.uv, 8, 4);
				Array.Copy(array2, 8, dfSlicedSprite.uv, 0, 4);
				Array.Copy(dfSlicedSprite.uv, 4, dfSlicedSprite.uv, 12, 4);
				Array.Copy(array2, 12, dfSlicedSprite.uv, 4, 4);
			}
		}
		for (int j = 0; j < dfSlicedSprite.uv.Length; j++)
		{
			renderData.UV.Add(dfSlicedSprite.uv[j]);
		}
	}

	// Token: 0x06004022 RID: 16418 RVA: 0x000F5F74 File Offset: 0x000F4174
	private static void rebuildColors(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		for (int i = 0; i < 16; i++)
		{
			renderData.Colors.Add(options.color);
		}
	}

	// Token: 0x04002217 RID: 8727
	private static int[] triangleIndices = new int[]
	{
		0,
		1,
		2,
		2,
		3,
		0,
		4,
		5,
		6,
		6,
		7,
		4,
		8,
		9,
		10,
		10,
		11,
		8,
		12,
		13,
		14,
		14,
		15,
		12,
		1,
		4,
		7,
		7,
		2,
		1,
		9,
		12,
		15,
		15,
		10,
		9,
		3,
		2,
		9,
		9,
		8,
		3,
		7,
		6,
		13,
		13,
		12,
		7,
		2,
		7,
		12,
		12,
		9,
		2
	};

	// Token: 0x04002218 RID: 8728
	private static int[][] horzFill = new int[][]
	{
		new int[]
		{
			0,
			1,
			4,
			5
		},
		new int[]
		{
			3,
			2,
			7,
			6
		},
		new int[]
		{
			8,
			9,
			12,
			13
		},
		new int[]
		{
			11,
			10,
			15,
			14
		}
	};

	// Token: 0x04002219 RID: 8729
	private static int[][] vertFill;

	// Token: 0x0400221A RID: 8730
	private static int[][] fillIndices;

	// Token: 0x0400221B RID: 8731
	private static Vector3[] verts;

	// Token: 0x0400221C RID: 8732
	private static Vector2[] uv;
}
