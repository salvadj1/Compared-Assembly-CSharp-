using System;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x02000796 RID: 1942
[AddComponentMenu("NGUI/Internal/Draw Call")]
[ExecuteInEditMode]
public class UIDrawCall : MonoBehaviour
{
	// Token: 0x17000D96 RID: 3478
	// (get) Token: 0x06004643 RID: 17987 RVA: 0x001177E8 File Offset: 0x001159E8
	// (set) Token: 0x06004644 RID: 17988 RVA: 0x001177F0 File Offset: 0x001159F0
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

	// Token: 0x17000D97 RID: 3479
	// (get) Token: 0x06004645 RID: 17989 RVA: 0x0011780C File Offset: 0x00115A0C
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

	// Token: 0x17000D98 RID: 3480
	// (get) Token: 0x06004646 RID: 17990 RVA: 0x00117834 File Offset: 0x00115A34
	// (set) Token: 0x06004647 RID: 17991 RVA: 0x0011783C File Offset: 0x00115A3C
	public UIMaterial material
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

	// Token: 0x17000D99 RID: 3481
	// (get) Token: 0x06004648 RID: 17992 RVA: 0x00117848 File Offset: 0x00115A48
	public int triangles
	{
		get
		{
			Mesh mesh = (!this.mEven) ? this.mMesh1 : this.mMesh0;
			return (!(mesh != null)) ? 0 : (mesh.vertexCount >> 1);
		}
	}

	// Token: 0x17000D9A RID: 3482
	// (get) Token: 0x06004649 RID: 17993 RVA: 0x0011788C File Offset: 0x00115A8C
	// (set) Token: 0x0600464A RID: 17994 RVA: 0x00117894 File Offset: 0x00115A94
	public UIDrawCall.Clipping clipping
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

	// Token: 0x17000D9B RID: 3483
	// (get) Token: 0x0600464B RID: 17995 RVA: 0x001178B0 File Offset: 0x00115AB0
	// (set) Token: 0x0600464C RID: 17996 RVA: 0x001178B8 File Offset: 0x00115AB8
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

	// Token: 0x17000D9C RID: 3484
	// (get) Token: 0x0600464D RID: 17997 RVA: 0x001178C4 File Offset: 0x00115AC4
	// (set) Token: 0x0600464E RID: 17998 RVA: 0x001178CC File Offset: 0x00115ACC
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

	// Token: 0x17000D9D RID: 3485
	// (get) Token: 0x0600464F RID: 17999 RVA: 0x001178D8 File Offset: 0x00115AD8
	// (set) Token: 0x06004650 RID: 18000 RVA: 0x001178E0 File Offset: 0x00115AE0
	public UIPanelMaterialPropertyBlock panelPropertyBlock
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

	// Token: 0x06004651 RID: 18001 RVA: 0x001178EC File Offset: 0x00115AEC
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

	// Token: 0x06004652 RID: 18002 RVA: 0x001179CC File Offset: 0x00115BCC
	private void UpdateMaterials()
	{
		if (this.mDepthPass)
		{
			if (this.mDepthMat == null)
			{
				Shader shader = Shader.Find("Depth");
				this.mDepthMat = UIMaterial.Create(new Material(shader)
				{
					hideFlags = 4,
					mainTexture = this.mSharedMat.mainTexture
				}, true, this.mClipping);
			}
		}
		else if (this.mDepthMat != null)
		{
			NGUITools.Destroy(this.mDepthMat);
			this.mDepthMat = null;
		}
		Material material = this.mSharedMat[this.mClipping];
		if (this.mDepthMat != null)
		{
			UIDrawCall.materialBuffer2[0] = this.mDepthMat[this.mClipping];
			UIDrawCall.materialBuffer2[1] = material;
			this.mRen.sharedMaterials = UIDrawCall.materialBuffer2;
			UIDrawCall.materialBuffer2[0] = (UIDrawCall.materialBuffer2[1] = null);
		}
		else if (this.mRen.sharedMaterial != material)
		{
			UIDrawCall.materialBuffer1[0] = material;
			this.mRen.sharedMaterials = UIDrawCall.materialBuffer1;
			UIDrawCall.materialBuffer1[0] = null;
		}
	}

	// Token: 0x06004653 RID: 18003 RVA: 0x00117AFC File Offset: 0x00115CFC
	public void Set(MeshBuffer m)
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

	// Token: 0x06004654 RID: 18004 RVA: 0x00117C5C File Offset: 0x00115E5C
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
		if (this.mClipping != UIDrawCall.Clipping.None)
		{
			Vector4 vector;
			vector.z = -this.mClipRange.x / this.mClipRange.z;
			vector.w = -this.mClipRange.y / this.mClipRange.w;
			vector.x = 1f / this.mClipRange.z;
			vector.y = 1f / this.mClipRange.w;
			this.mBlock.AddVector(UIDrawCall.FastProperties.kProp_ClippingRegion, vector);
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
			this.mBlock.AddVector(UIDrawCall.FastProperties.kProp_ClipSharpness, vector2);
		}
		base.renderer.SetPropertyBlock(this.mBlock);
	}

	// Token: 0x06004655 RID: 18005 RVA: 0x00117E10 File Offset: 0x00116010
	private void OnDestroy()
	{
		NGUITools.DestroyImmediate(this.mMesh0);
		NGUITools.DestroyImmediate(this.mMesh1);
		NGUITools.DestroyImmediate(this.mDepthMat);
	}

	// Token: 0x06004656 RID: 18006 RVA: 0x00117E34 File Offset: 0x00116034
	internal void LinkedList__Insert(ref UIDrawCall list)
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

	// Token: 0x06004657 RID: 18007 RVA: 0x00117E84 File Offset: 0x00116084
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

	// Token: 0x04002673 RID: 9843
	private Transform mTrans;

	// Token: 0x04002674 RID: 9844
	private UIMaterial mSharedMat;

	// Token: 0x04002675 RID: 9845
	private Mesh mMesh0;

	// Token: 0x04002676 RID: 9846
	private Mesh mMesh1;

	// Token: 0x04002677 RID: 9847
	private MeshFilter mFilter;

	// Token: 0x04002678 RID: 9848
	private MeshRenderer mRen;

	// Token: 0x04002679 RID: 9849
	private UIDrawCall.Clipping mClipping;

	// Token: 0x0400267A RID: 9850
	private Vector4 mClipRange;

	// Token: 0x0400267B RID: 9851
	private Vector2 mClipSoft;

	// Token: 0x0400267C RID: 9852
	private UIMaterial mDepthMat;

	// Token: 0x0400267D RID: 9853
	private int[] mIndices;

	// Token: 0x0400267E RID: 9854
	private Vector3[] mVerts;

	// Token: 0x0400267F RID: 9855
	private Vector2[] mUVs;

	// Token: 0x04002680 RID: 9856
	private Color[] mColors;

	// Token: 0x04002681 RID: 9857
	private UIDrawCall mNext;

	// Token: 0x04002682 RID: 9858
	private UIDrawCall mPrev;

	// Token: 0x04002683 RID: 9859
	private bool mHasNext;

	// Token: 0x04002684 RID: 9860
	private bool mHasPrev;

	// Token: 0x04002685 RID: 9861
	private UIPanelMaterialPropertyBlock mPanelPropertyBlock;

	// Token: 0x04002686 RID: 9862
	private MaterialPropertyBlock mBlock;

	// Token: 0x04002687 RID: 9863
	private bool mDepthPass;

	// Token: 0x04002688 RID: 9864
	private bool mReset = true;

	// Token: 0x04002689 RID: 9865
	private bool mEven = true;

	// Token: 0x0400268A RID: 9866
	private static Material[] materialBuffer2 = new Material[2];

	// Token: 0x0400268B RID: 9867
	private static Material[] materialBuffer1 = new Material[1];

	// Token: 0x02000797 RID: 1943
	public enum Clipping
	{
		// Token: 0x0400268D RID: 9869
		None,
		// Token: 0x0400268E RID: 9870
		HardClip,
		// Token: 0x0400268F RID: 9871
		AlphaClip,
		// Token: 0x04002690 RID: 9872
		SoftClip
	}

	// Token: 0x02000798 RID: 1944
	public struct Iterator
	{
		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06004658 RID: 18008 RVA: 0x00117F0C File Offset: 0x0011610C
		public UIDrawCall.Iterator Next
		{
			get
			{
				if (this.Has)
				{
					UIDrawCall.Iterator result;
					result.Has = this.Current.mHasNext;
					result.Current = this.Current.mNext;
					return result;
				}
				return default(UIDrawCall.Iterator);
			}
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06004659 RID: 18009 RVA: 0x00117F54 File Offset: 0x00116154
		public UIDrawCall.Iterator Prev
		{
			get
			{
				if (this.Has)
				{
					UIDrawCall.Iterator result;
					result.Has = this.Current.mHasPrev;
					result.Current = this.Current.mPrev;
					return result;
				}
				return default(UIDrawCall.Iterator);
			}
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x00117F9C File Offset: 0x0011619C
		public static explicit operator UIDrawCall.Iterator(UIDrawCall call)
		{
			UIDrawCall.Iterator result;
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

		// Token: 0x04002691 RID: 9873
		public UIDrawCall Current;

		// Token: 0x04002692 RID: 9874
		public bool Has;
	}

	// Token: 0x02000799 RID: 1945
	private static class FastProperties
	{
		// Token: 0x04002693 RID: 9875
		public static readonly int kProp_ClippingRegion = Shader.PropertyToID("_MainTex_ST");

		// Token: 0x04002694 RID: 9876
		public static readonly int kProp_ClipSharpness = Shader.PropertyToID("_ClipSharpness");
	}
}
