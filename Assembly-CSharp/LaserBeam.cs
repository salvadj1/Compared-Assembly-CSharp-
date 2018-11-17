using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006C1 RID: 1729
[ExecuteInEditMode]
public sealed class LaserBeam : MonoBehaviour
{
	// Token: 0x06003AC8 RID: 15048 RVA: 0x000CE1C4 File Offset: 0x000CC3C4
	public static List<global::LaserBeam> Collect()
	{
		global::LaserBeam.g.currentRendering.Clear();
		global::LaserBeam.g.currentRendering.AddRange(global::LaserBeam.g.allActiveBeams);
		return global::LaserBeam.g.currentRendering;
	}

	// Token: 0x06003AC9 RID: 15049 RVA: 0x000CE1E4 File Offset: 0x000CC3E4
	private void OnEnable()
	{
		global::LaserBeam.g.allActiveBeams.Add(this);
		global::LaserGraphics.EnsureGraphicsExist();
	}

	// Token: 0x06003ACA RID: 15050 RVA: 0x000CE1F8 File Offset: 0x000CC3F8
	private void OnDisable()
	{
		global::LaserBeam.g.allActiveBeams.Remove(this);
	}

	// Token: 0x04001CE3 RID: 7395
	public float beamMaxDistance = 100f;

	// Token: 0x04001CE4 RID: 7396
	public Vector4 beamColor = Color.red;

	// Token: 0x04001CE5 RID: 7397
	public float beamOutput = 1f;

	// Token: 0x04001CE6 RID: 7398
	public float beamWidthStart = 0.1f;

	// Token: 0x04001CE7 RID: 7399
	public float beamWidthEnd = 0.2f;

	// Token: 0x04001CE8 RID: 7400
	public float dotRadiusStart = 0.15f;

	// Token: 0x04001CE9 RID: 7401
	public float dotRadiusEnd = 0.25f;

	// Token: 0x04001CEA RID: 7402
	public bool isViewModel;

	// Token: 0x04001CEB RID: 7403
	public Vector4 dotColor = Color.red;

	// Token: 0x04001CEC RID: 7404
	public Material beamMaterial;

	// Token: 0x04001CED RID: 7405
	public Material dotMaterial;

	// Token: 0x04001CEE RID: 7406
	public LayerMask beamLayers = 1;

	// Token: 0x04001CEF RID: 7407
	public LayerMask cullLayers = 1;

	// Token: 0x04001CF0 RID: 7408
	public global::LaserBeam.FrameData frame;

	// Token: 0x020006C2 RID: 1730
	public struct Quad<T>
	{
		// Token: 0x04001CF1 RID: 7409
		public T m0;

		// Token: 0x04001CF2 RID: 7410
		public T m1;

		// Token: 0x04001CF3 RID: 7411
		public T m2;

		// Token: 0x04001CF4 RID: 7412
		public T m3;
	}

	// Token: 0x020006C3 RID: 1731
	public struct FrameData
	{
		// Token: 0x04001CF5 RID: 7413
		public MaterialPropertyBlock block;

		// Token: 0x04001CF6 RID: 7414
		public Bounds bounds;

		// Token: 0x04001CF7 RID: 7415
		public bool hit;

		// Token: 0x04001CF8 RID: 7416
		public Vector3 hitPoint;

		// Token: 0x04001CF9 RID: 7417
		public Vector3 hitNormal;

		// Token: 0x04001CFA RID: 7418
		public global::LaserBeam.Quad<Vector3> beamVertices;

		// Token: 0x04001CFB RID: 7419
		public global::LaserBeam.Quad<Vector3> beamNormals;

		// Token: 0x04001CFC RID: 7420
		public global::LaserBeam.Quad<Vector2> beamUVs;

		// Token: 0x04001CFD RID: 7421
		public global::LaserBeam.Quad<Vector3> dotVertices1;

		// Token: 0x04001CFE RID: 7422
		public global::LaserBeam.Quad<Vector3> dotVertices2;

		// Token: 0x04001CFF RID: 7423
		public global::LaserBeam.Quad<Color> beamColor;

		// Token: 0x04001D00 RID: 7424
		public global::LaserBeam.Quad<Color> dotColor1;

		// Token: 0x04001D01 RID: 7425
		public global::LaserBeam.Quad<Color> dotColor2;

		// Token: 0x04001D02 RID: 7426
		public Vector3 direction;

		// Token: 0x04001D03 RID: 7427
		public Vector3 origin;

		// Token: 0x04001D04 RID: 7428
		public Vector3 point;

		// Token: 0x04001D05 RID: 7429
		public float distance;

		// Token: 0x04001D06 RID: 7430
		public float distanceFraction;

		// Token: 0x04001D07 RID: 7431
		public float pointWidth;

		// Token: 0x04001D08 RID: 7432
		public float originWidth;

		// Token: 0x04001D09 RID: 7433
		public float dotRadius;

		// Token: 0x04001D0A RID: 7434
		public bool didHit;

		// Token: 0x04001D0B RID: 7435
		public bool drawDot;

		// Token: 0x04001D0C RID: 7436
		public int beamsLayer;

		// Token: 0x04001D0D RID: 7437
		internal global::LaserGraphics.MeshBuffer bufBeam;

		// Token: 0x04001D0E RID: 7438
		internal global::LaserGraphics.MeshBuffer bufDot;
	}

	// Token: 0x020006C4 RID: 1732
	private static class g
	{
		// Token: 0x04001D0F RID: 7439
		public static HashSet<global::LaserBeam> allActiveBeams = new HashSet<global::LaserBeam>();

		// Token: 0x04001D10 RID: 7440
		public static List<global::LaserBeam> currentRendering = new List<global::LaserBeam>();
	}
}
