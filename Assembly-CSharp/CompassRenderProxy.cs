using System;
using UnityEngine;

// Token: 0x0200049D RID: 1181
[ExecuteInEditMode]
public class CompassRenderProxy : MonoBehaviour
{
	// Token: 0x060029CE RID: 10702 RVA: 0x000A3A58 File Offset: 0x000A1C58
	private void OnBecameVisible()
	{
		base.enabled = true;
		this.BindFrame();
	}

	// Token: 0x060029CF RID: 10703 RVA: 0x000A3A68 File Offset: 0x000A1C68
	private void OnBecameInvisible()
	{
		base.enabled = false;
	}

	// Token: 0x060029D0 RID: 10704 RVA: 0x000A3A74 File Offset: 0x000A1C74
	private void BindFrame()
	{
		if (this.propBlock != null)
		{
			this.propBlock.Clear();
		}
		else
		{
			this.propBlock = new MaterialPropertyBlock();
		}
		Vector2 vector = base.transform.worldToLocalMatrix.MultiplyVector(this.north);
		vector.Normalize();
		Vector2 vector2;
		vector2..ctor(-vector.y, vector.x);
		vector2 *= this.scalar;
		vector *= this.scalar;
		if (this.bindNorth)
		{
			this.propBlock.AddVector(CompassRenderProxy.g.kPropLensUp, vector);
		}
		if (this.bindWest)
		{
			this.propBlock.AddVector(CompassRenderProxy.g.kPropLensRight, vector2);
		}
		if (this.bindForward)
		{
			this.propBlock.AddVector(CompassRenderProxy.g.kPropLensDir, this.forward);
		}
		base.renderer.SetPropertyBlock(this.propBlock);
	}

	// Token: 0x060029D1 RID: 10705 RVA: 0x000A3B78 File Offset: 0x000A1D78
	private void LateUpdate()
	{
		this.BindFrame();
	}

	// Token: 0x040015B7 RID: 5559
	public float scalar = 0.7f;

	// Token: 0x040015B8 RID: 5560
	public Vector3 north = Vector3.up;

	// Token: 0x040015B9 RID: 5561
	public Vector3 forward = Vector3.forward;

	// Token: 0x040015BA RID: 5562
	public float back = 0.3f;

	// Token: 0x040015BB RID: 5563
	public bool bindNorth;

	// Token: 0x040015BC RID: 5564
	public bool bindWest;

	// Token: 0x040015BD RID: 5565
	public bool bindForward;

	// Token: 0x040015BE RID: 5566
	private MaterialPropertyBlock propBlock;

	// Token: 0x0200049E RID: 1182
	private static class g
	{
		// Token: 0x040015BF RID: 5567
		public static readonly int kPropLensRight = Shader.PropertyToID("_LensRight");

		// Token: 0x040015C0 RID: 5568
		public static readonly int kPropLensUp = Shader.PropertyToID("_LensUp");

		// Token: 0x040015C1 RID: 5569
		public static readonly int kPropLensDir = Shader.PropertyToID("_LensForward");
	}
}
