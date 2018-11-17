using System;
using UnityEngine;

// Token: 0x02000042 RID: 66
[ExecuteInEditMode]
public class FPGrassPatch : MonoBehaviour, IFPGrassAsset
{
	// Token: 0x06000244 RID: 580 RVA: 0x0000CCE0 File Offset: 0x0000AEE0
	private void OnEnable()
	{
		this.transform = base.transform;
		this.lastRenderPosition.x = (this.lastRenderPosition.y = (this.lastRenderPosition.z = float.NegativeInfinity));
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0000CD28 File Offset: 0x0000AF28
	private void OnDestroy()
	{
	}

	// Token: 0x06000246 RID: 582 RVA: 0x0000CD2C File Offset: 0x0000AF2C
	internal void Render(ref FPGrass.RenderArguments renderArgs)
	{
		if (renderArgs.terrain == null)
		{
			return;
		}
		Vector3 vector = renderArgs.center + this.transform.position;
		Bounds bounds;
		bool flag;
		if (vector.x == this.lastRenderPosition.x && vector.y == this.lastRenderPosition.y && vector.z == this.lastRenderPosition.z)
		{
			bounds = this.lastBounds;
			flag = false;
		}
		else
		{
			Vector3 vector2 = vector;
			Vector3 vector3 = vector2;
			Vector3 vector4 = vector2;
			Vector3 vector5 = vector2;
			float num = this.patchSize * 0.5f;
			vector2.x -= num;
			vector2.z += num;
			vector3.x -= num;
			vector3.z -= num;
			vector4.x += num;
			vector4.z += num;
			vector5.x += num;
			vector5.z -= num;
			float num2 = renderArgs.terrain.SampleHeight(vector2);
			float num3 = renderArgs.terrain.SampleHeight(vector3);
			float num4 = renderArgs.terrain.SampleHeight(vector4);
			float num5 = renderArgs.terrain.SampleHeight(vector5);
			float num6;
			float num7;
			if (num2 < num3)
			{
				num6 = num2;
				num7 = num3;
			}
			else
			{
				num6 = num3;
				num7 = num2;
			}
			float num8;
			float num9;
			if (num4 < num5)
			{
				num8 = num4;
				num9 = num5;
			}
			else
			{
				num8 = num5;
				num9 = num4;
			}
			float num10;
			if (num6 < num8)
			{
				num10 = num6;
			}
			else
			{
				num10 = num8;
			}
			float num11;
			if (num7 > num9)
			{
				num11 = num7;
			}
			else
			{
				num11 = num9;
			}
			vector3.y = num10 - 5f;
			vector4.y = num11 + 5f;
			bounds = default(Bounds);
			bounds.SetMinMax(vector3, vector4);
			flag = (bounds != this.lastBounds);
		}
		if (!this.runtimeMesh)
		{
			this.runtimeMesh = this.mesh;
		}
		if (flag)
		{
			this.runtimeMesh.bounds = new Bounds(bounds.center - vector, bounds.size);
			this.lastBounds = bounds;
		}
		if (GeometryUtility.TestPlanesAABB(renderArgs.frustum, bounds))
		{
			this.level.Draw(this, this.runtimeMesh, ref vector, ref renderArgs);
		}
	}

	// Token: 0x04000179 RID: 377
	[SerializeField]
	private Mesh mesh;

	// Token: 0x0400017A RID: 378
	[SerializeField]
	private float patchSize;

	// Token: 0x0400017B RID: 379
	[SerializeField]
	public FPGrassLevel level;

	// Token: 0x0400017C RID: 380
	[NonSerialized]
	private Vector3 lastRenderPosition;

	// Token: 0x0400017D RID: 381
	[NonSerialized]
	private Mesh runtimeMesh;

	// Token: 0x0400017E RID: 382
	[NonSerialized]
	private Bounds lastBounds;

	// Token: 0x0400017F RID: 383
	[NonSerialized]
	public Transform transform;
}
