using System;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x0200087B RID: 2171
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Draw Call")]
public class UIDrawCall : MonoBehaviour
{
	// Token: 0x17000E26 RID: 3622
	// (get) Token: 0x06004AB0 RID: 19120 RVA: 0x00121168 File Offset: 0x0011F368
	// (set) Token: 0x06004AB1 RID: 19121 RVA: 0x00121170 File Offset: 0x0011F370
	public bool depthPass
	{
		get
		{
			return this.mDepthPass;
		}
		set
		{
			if (this.mDepthPass != value)
			{
				this.mDepthPass = value;
				this.mReset = true;
			}
		}
	}

	// Token: 0x17000E27 RID: 3623
	// (get) Token: 0x06004AB2 RID: 19122 RVA: 0x0012118C File Offset: 0x0011F38C
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x17000E28 RID: 3624
	// (get) Token: 0x06004AB3 RID: 19123 RVA: 0x001211B4 File Offset: 0x0011F3B4
	// (set) Token: 0x06004AB4 RID: 19124 RVA: 0x001211BC File Offset: 0x0011F3BC
	public global::UIMaterial material
	{
		get
		{
			return this.mSharedMat;
		}
		set
		{
			this.mSharedMat = value;
		}
	}

	// Token: 0x17000E29 RID: 3625
	// (get) Token: 0x06004AB5 RID: 19125 RVA: 0x001211C8 File Offset: 0x0011F3C8
	public int triangles
	{
		get
		{
			Mesh mesh = (!this.mEven) ? this.mMesh1 : this.mMesh0;
			return (!(mesh != null)) ? 0 : (mesh.vertexCount >> 1);
		}
	}

	// Token: 0x17000E2A RID: 3626
	// (get) Token: 0x06004AB6 RID: 19126 RVA: 0x0012120C File Offset: 0x0011F40C
	// (set) Token: 0x06004AB7 RID: 19127 RVA: 0x00121214 File Offset: 0x0011F414
	public global::UIDrawCall.Clipping clipping
	{
		get
		{
			return this.mClipping;
		}
		set
		{
			if (this.mClipping != value)
			{
				this.mClipping = value;
				this.mReset = true;
			}
		}
	}

	// Token: 0x17000E2B RID: 3627
	// (get) Token: 0x06004AB8 RID: 19128 RVA: 0x00121230 File Offset: 0x0011F430
	// (set) Token: 0x06004AB9 RID: 19129 RVA: 0x00121238 File Offset: 0x0011F438
	public Vector4 clipRange
	{
		get
		{
			return this.mClipRange;
		}
		set
		{
			this.mClipRange = value;
		}
	}

	// Token: 0x17000E2C RID: 3628
	// (get) Token: 0x06004ABA RID: 19130 RVA: 0x00121244 File Offset: 0x0011F444
	// (set) Token: 0x06004ABB RID: 19131 RVA: 0x0012124C File Offset: 0x0011F44C
	public Vector2 clipSoftness
	{
		get
		{
			return this.mClipSoft;
		}
		set
		{
			this.mClipSoft = value;
		}
	}

	// Token: 0x17000E2D RID: 3629
	// (get) Token: 0x06004ABC RID: 19132 RVA: 0x00121258 File Offset: 0x0011F458
	// (set) Token: 0x06004ABD RID: 19133 RVA: 0x00121260 File Offset: 0x0011F460
	public global::UIPanelMaterialPropertyBlock panelPropertyBlock
	{
		get
		{
			return this.mPanelPropertyBlock;
		}
		set
		{
			this.mPanelPropertyBlock = value;
		}
	}

	// Token: 0x06004ABE RID: 19134 RVA: 0x0012126C File Offset: 0x0011F46C
	private Mesh GetMesh(ref bool rebuildIndices, int vertexCount)
	{
		this.mEven = !this.mEven;
		if (this.mEven)
		{
			if (this.mMesh0 == null)
			{
				this.mMesh0 = new Mesh();
				this.mMesh0.hideFlags = 4;
				rebuildIndices = true;
			}
			else if (rebuildIndices || this.mMesh0.vertexCount != vertexCount)
			{
				rebuildIndices = true;
				this.mMesh0.Clear();
			}
			return this.mMesh0;
		}
		if (this.mMesh1 == null)
		{
			this.mMesh1 = new Mesh();
			this.mMesh1.hideFlags = 4;
			rebuildIndices = true;
		}
		else if (rebuildIndices || this.mMesh1.vertexCount != vertexCount)
		{
			rebuildIndices = true;
			this.mMesh1.Clear();
		}
		return this.mMesh1;
	}

	// Token: 0x06004ABF RID: 19135 RVA: 0x0012134C File Offset: 0x0011F54C
	private void UpdateMaterials()
	{
		if (this.mDepthPass)
		{
			if (this.mDepthMat == null)
			{
				Shader shader = Shader.Find("Depth");
				this.mDepthMat = global::UIMaterial.Create(new Material(shader)
				{
					hideFlags = 4,
					mainTexture = this.mSharedMat.mainTexture
				}, true, this.mClipping);
			}
		}
		else if (this.mDepthMat != null)
		{
			global::NGUITools.Destroy(this.mDepthMat);
			this.mDepthMat = null;
		}
		Material material = this.mSharedMat[this.mClipping];
		if (this.mDepthMat != null)
		{
			global::UIDrawCall.materialBuffer2[0] = this.mDepthMat[this.mClipping];
			global::UIDrawCall.materialBuffer2[1] = material;
			this.mRen.sharedMaterials = global::UIDrawCall.materialBuffer2;
			global::UIDrawCall.materialBuffer2[0] = (global::UIDrawCall.materialBuffer2[1] = null);
		}
		else if (this.mRen.sharedMaterial != material)
		{
			global::UIDrawCall.materialBuffer1[0] = material;
			this.mRen.sharedMaterials = global::UIDrawCall.materialBuffer1;
			global::UIDrawCall.materialBuffer1[0] = null;
		}
	}

	// Token: 0x06004AC0 RID: 19136 RVA: 0x0012147C File Offset: 0x0011F67C
	public void Set(NGUI.Meshing.MeshBuffer m)
	{
		if (this.mFilter == null)
		{
			this.mFilter = base.gameObject.GetComponent<MeshFilter>();
		}
		if (this.mFilter == null)
		{
			this.mFilter = base.gameObject.AddComponent<MeshFilter>();
		}
		if (this.mRen == null)
		{
			this.mRen = base.gameObject.GetComponent<MeshRenderer>();
		}
		if (this.mRen == null)
		{
			this.mRen = base.gameObject.AddComponent<MeshRenderer>();
			this.UpdateMaterials();
		}
		if (m.vSize < 65000)
		{
			bool flag = m.ExtractMeshBuffers(ref this.mVerts, ref this.mUVs, ref this.mColors, ref this.mIndices);
			Mesh mesh = this.GetMesh(ref flag, m.vSize);
			mesh.vertices = this.mVerts;
			mesh.uv = this.mUVs;
			mesh.colors = this.mColors;
			mesh.triangles = this.mIndices;
			mesh.RecalculateBounds();
			this.mFilter.mesh = mesh;
		}
		else
		{
			if (this.mFilter.mesh != null)
			{
				this.mFilter.mesh.Clear();
			}
			Debug.LogError("Too many vertices on one panel: " + m.vSize);
		}
	}

	// Token: 0x06004AC1 RID: 19137 RVA: 0x001215DC File Offset: 0x0011F7DC
	private void OnWillRenderObject()
	{
		if (this.mReset)
		{
			this.mReset = false;
			this.UpdateMaterials();
		}
		if (this.mBlock == null)
		{
			this.mBlock = new MaterialPropertyBlock();
		}
		else
		{
			this.mBlock.Clear();
		}
		if (this.mPanelPropertyBlock != null)
		{
			this.mPanelPropertyBlock.AddToMaterialPropertyBlock(this.mBlock);
		}
		if (this.mClipping != global::UIDrawCall.Clipping.None)
		{
			Vector4 vector;
			vector.z = -this.mClipRange.x / this.mClipRange.z;
			vector.w = -this.mClipRange.y / this.mClipRange.w;
			vector.x = 1f / this.mClipRange.z;
			vector.y = 1f / this.mClipRange.w;
			this.mBlock.AddVector(global::UIDrawCall.FastProperties.kProp_ClippingRegion, vector);
			Vector4 vector2;
			if (this.mClipSoft.x > 0f)
			{
				vector2.x = this.mClipRange.z / this.mClipSoft.x;
			}
			else
			{
				vector2.x = 1000f;
			}
			if (this.mClipSoft.y > 0f)
			{
				vector2.y = this.mClipRange.w / this.mClipSoft.y;
			}
			else
			{
				vector2.y = 1000f;
			}
			vector2.z = (vector2.w = 0f);
			this.mBlock.AddVector(global::UIDrawCall.FastProperties.kProp_ClipSharpness, vector2);
		}
		base.renderer.SetPropertyBlock(this.mBlock);
	}

	// Token: 0x06004AC2 RID: 19138 RVA: 0x00121790 File Offset: 0x0011F990
	private void OnDestroy()
	{
		global::NGUITools.DestroyImmediate(this.mMesh0);
		global::NGUITools.DestroyImmediate(this.mMesh1);
		global::NGUITools.DestroyImmediate(this.mDepthMat);
	}

	// Token: 0x06004AC3 RID: 19139 RVA: 0x001217B4 File Offset: 0x0011F9B4
	internal void LinkedList__Insert(ref global::UIDrawCall list)
	{
		this.mHasPrev = false;
		this.mHasNext = list;
		this.mNext = list;
		this.mPrev = null;
		if (this.mHasNext)
		{
			list.mHasPrev = true;
			list.mPrev = this;
		}
		list = this;
	}

	// Token: 0x06004AC4 RID: 19140 RVA: 0x00121804 File Offset: 0x0011FA04
	internal void LinkedList__Remove()
	{
		if (this.mHasPrev)
		{
			this.mPrev.mHasNext = this.mHasNext;
			this.mPrev.mNext = this.mNext;
		}
		if (this.mHasNext)
		{
			this.mNext.mHasPrev = this.mHasPrev;
			this.mNext.mPrev = this.mPrev;
		}
		this.mHasNext = (this.mHasPrev = false);
		this.mNext = (this.mPrev = null);
	}

	// Token: 0x040028AA RID: 10410
	private Transform mTrans;

	// Token: 0x040028AB RID: 10411
	private global::UIMaterial mSharedMat;

	// Token: 0x040028AC RID: 10412
	private Mesh mMesh0;

	// Token: 0x040028AD RID: 10413
	private Mesh mMesh1;

	// Token: 0x040028AE RID: 10414
	private MeshFilter mFilter;

	// Token: 0x040028AF RID: 10415
	private MeshRenderer mRen;

	// Token: 0x040028B0 RID: 10416
	private global::UIDrawCall.Clipping mClipping;

	// Token: 0x040028B1 RID: 10417
	private Vector4 mClipRange;

	// Token: 0x040028B2 RID: 10418
	private Vector2 mClipSoft;

	// Token: 0x040028B3 RID: 10419
	private global::UIMaterial mDepthMat;

	// Token: 0x040028B4 RID: 10420
	private int[] mIndices;

	// Token: 0x040028B5 RID: 10421
	private Vector3[] mVerts;

	// Token: 0x040028B6 RID: 10422
	private Vector2[] mUVs;

	// Token: 0x040028B7 RID: 10423
	private Color[] mColors;

	// Token: 0x040028B8 RID: 10424
	private global::UIDrawCall mNext;

	// Token: 0x040028B9 RID: 10425
	private global::UIDrawCall mPrev;

	// Token: 0x040028BA RID: 10426
	private bool mHasNext;

	// Token: 0x040028BB RID: 10427
	private bool mHasPrev;

	// Token: 0x040028BC RID: 10428
	private global::UIPanelMaterialPropertyBlock mPanelPropertyBlock;

	// Token: 0x040028BD RID: 10429
	private MaterialPropertyBlock mBlock;

	// Token: 0x040028BE RID: 10430
	private bool mDepthPass;

	// Token: 0x040028BF RID: 10431
	private bool mReset = true;

	// Token: 0x040028C0 RID: 10432
	private bool mEven = true;

	// Token: 0x040028C1 RID: 10433
	private static Material[] materialBuffer2 = new Material[2];

	// Token: 0x040028C2 RID: 10434
	private static Material[] materialBuffer1 = new Material[1];

	// Token: 0x0200087C RID: 2172
	public enum Clipping
	{
		// Token: 0x040028C4 RID: 10436
		None,
		// Token: 0x040028C5 RID: 10437
		HardClip,
		// Token: 0x040028C6 RID: 10438
		AlphaClip,
		// Token: 0x040028C7 RID: 10439
		SoftClip
	}

	// Token: 0x0200087D RID: 2173
	public struct Iterator
	{
		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06004AC5 RID: 19141 RVA: 0x0012188C File Offset: 0x0011FA8C
		public global::UIDrawCall.Iterator Next
		{
			get
			{
				if (this.Has)
				{
					global::UIDrawCall.Iterator result;
					result.Has = this.Current.mHasNext;
					result.Current = this.Current.mNext;
					return result;
				}
				return default(global::UIDrawCall.Iterator);
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06004AC6 RID: 19142 RVA: 0x001218D4 File Offset: 0x0011FAD4
		public global::UIDrawCall.Iterator Prev
		{
			get
			{
				if (this.Has)
				{
					global::UIDrawCall.Iterator result;
					result.Has = this.Current.mHasPrev;
					result.Current = this.Current.mPrev;
					return result;
				}
				return default(global::UIDrawCall.Iterator);
			}
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x0012191C File Offset: 0x0011FB1C
		public static explicit operator global::UIDrawCall.Iterator(global::UIDrawCall call)
		{
			global::UIDrawCall.Iterator result;
			result.Has = call;
			if (result.Has)
			{
				result.Current = call;
			}
			else
			{
				result.Current = null;
			}
			return result;
		}

		// Token: 0x040028C8 RID: 10440
		public global::UIDrawCall Current;

		// Token: 0x040028C9 RID: 10441
		public bool Has;
	}

	// Token: 0x0200087E RID: 2174
	private static class FastProperties
	{
		// Token: 0x040028CA RID: 10442
		public static readonly int kProp_ClippingRegion = Shader.PropertyToID("_MainTex_ST");

		// Token: 0x040028CB RID: 10443
		public static readonly int kProp_ClipSharpness = Shader.PropertyToID("_ClipSharpness");
	}
}
