using System;
using UnityEngine;

// Token: 0x02000558 RID: 1368
[ExecuteInEditMode]
public class CompassRenderProxy : MonoBehaviour
{
	// Token: 0x06002D80 RID: 11648 RVA: 0x000AB7F0 File Offset: 0x000A99F0
	private void OnBecameVisible()
	{
		base.enabled = true;
		this.BindFrame();
	}

	// Token: 0x06002D81 RID: 11649 RVA: 0x000AB800 File Offset: 0x000A9A00
	private void OnBecameInvisible()
	{
		base.enabled = false;
	}

	// Token: 0x06002D82 RID: 11650 RVA: 0x000AB80C File Offset: 0x000A9A0C
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
			this.propBlock.AddVector(global::CompassRenderProxy.g.kPropLensUp, vector);
		}
		if (this.bindWest)
		{
			this.propBlock.AddVector(global::CompassRenderProxy.g.kPropLensRight, vector2);
		}
		if (this.bindForward)
		{
			this.propBlock.AddVector(global::CompassRenderProxy.g.kPropLensDir, this.forward);
		}
		base.renderer.SetPropertyBlock(this.propBlock);
	}

	// Token: 0x06002D83 RID: 11651 RVA: 0x000AB910 File Offset: 0x000A9B10
	private void LateUpdate()
	{
		this.BindFrame();
	}

	// Token: 0x04001774 RID: 6004
	public float scalar = 0.7f;

	// Token: 0x04001775 RID: 6005
	public Vector3 north = Vector3.up;

	// Token: 0x04001776 RID: 6006
	public Vector3 forward = Vector3.forward;

	// Token: 0x04001777 RID: 6007
	public float back = 0.3f;

	// Token: 0x04001778 RID: 6008
	public bool bindNorth;

	// Token: 0x04001779 RID: 6009
	public bool bindWest;

	// Token: 0x0400177A RID: 6010
	public bool bindForward;

	// Token: 0x0400177B RID: 6011
	private MaterialPropertyBlock propBlock;

	// Token: 0x02000559 RID: 1369
	private static class g
	{
		// Token: 0x0400177C RID: 6012
		public static readonly int kPropLensRight = Shader.PropertyToID("_LensRight");

		// Token: 0x0400177D RID: 6013
		public static readonly int kPropLensUp = Shader.PropertyToID("_LensUp");

		// Token: 0x0400177E RID: 6014
		public static readonly int kPropLensDir = Shader.PropertyToID("_LensForward");
	}
}
