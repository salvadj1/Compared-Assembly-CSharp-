using System;
using System.Collections.Generic;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x02000895 RID: 2197
public abstract class UIWidget : MonoBehaviour
{
	// Token: 0x06004B64 RID: 19300 RVA: 0x00126748 File Offset: 0x00124948
	protected UIWidget(global::UIWidget.WidgetFlags flags)
	{
		this.widgetFlags = flags;
	}

	// Token: 0x06004B66 RID: 19302 RVA: 0x001267D8 File Offset: 0x001249D8
	public static void GlobalUpdate()
	{
		global::UIWidget.Global.WidgetUpdate();
	}

	// Token: 0x06004B67 RID: 19303 RVA: 0x001267E0 File Offset: 0x001249E0
	public void MarkAsChangedForced()
	{
		this.MarkAsChanged();
		this.mForcedChanged = true;
	}

	// Token: 0x17000E38 RID: 3640
	// (get) Token: 0x06004B68 RID: 19304 RVA: 0x001267F0 File Offset: 0x001249F0
	// (set) Token: 0x06004B69 RID: 19305 RVA: 0x001267F8 File Offset: 0x001249F8
	public bool alphaUnchecked
	{
		get
		{
			return this.mAlphaUnchecked;
		}
		set
		{
			if (value)
			{
				if (!this.mAlphaUnchecked)
				{
					this.mAlphaUnchecked = true;
					if (global::NGUITools.ZeroAlpha(this.mColor.a))
					{
						this.mChangedCall = true;
					}
				}
			}
			else if (this.mAlphaUnchecked)
			{
				this.mAlphaUnchecked = false;
				if (global::NGUITools.ZeroAlpha(this.mColor.a))
				{
					this.mChangedCall = true;
				}
			}
		}
	}

	// Token: 0x17000E39 RID: 3641
	// (get) Token: 0x06004B6A RID: 19306 RVA: 0x0012686C File Offset: 0x00124A6C
	public bool changesQueued
	{
		get
		{
			return this.mChangedCall || this.mForcedChanged;
		}
	}

	// Token: 0x06004B6B RID: 19307 RVA: 0x00126884 File Offset: 0x00124A84
	protected void ChangedAuto()
	{
		this.mChangedCall = true;
	}

	// Token: 0x17000E3A RID: 3642
	// (get) Token: 0x06004B6C RID: 19308 RVA: 0x00126890 File Offset: 0x00124A90
	private global::UIGeometry mGeom
	{
		get
		{
			global::UIGeometry result;
			if ((result = this.__mGeom) == null)
			{
				result = (this.__mGeom = new global::UIGeometry());
			}
			return result;
		}
	}

	// Token: 0x17000E3B RID: 3643
	// (get) Token: 0x06004B6D RID: 19309 RVA: 0x001268B8 File Offset: 0x00124AB8
	// (set) Token: 0x06004B6E RID: 19310 RVA: 0x001268C0 File Offset: 0x00124AC0
	public Color color
	{
		get
		{
			return this.mColor;
		}
		set
		{
			if (this.mColor != value)
			{
				this.mColor = value;
				this.mChangedCall = true;
			}
		}
	}

	// Token: 0x17000E3C RID: 3644
	// (get) Token: 0x06004B6F RID: 19311 RVA: 0x001268E4 File Offset: 0x00124AE4
	// (set) Token: 0x06004B70 RID: 19312 RVA: 0x001268F4 File Offset: 0x00124AF4
	public float alpha
	{
		get
		{
			return this.mColor.a;
		}
		set
		{
			Color color = this.mColor;
			color.a = value;
			this.color = color;
		}
	}

	// Token: 0x17000E3D RID: 3645
	// (get) Token: 0x06004B71 RID: 19313 RVA: 0x00126918 File Offset: 0x00124B18
	// (set) Token: 0x06004B72 RID: 19314 RVA: 0x00126920 File Offset: 0x00124B20
	public global::UIWidget.Pivot pivot
	{
		get
		{
			return this.mPivot;
		}
		set
		{
			if (this.mPivot != value)
			{
				this.mPivot = value;
				this.mChangedCall = true;
			}
		}
	}

	// Token: 0x17000E3E RID: 3646
	// (get) Token: 0x06004B73 RID: 19315 RVA: 0x0012693C File Offset: 0x00124B3C
	// (set) Token: 0x06004B74 RID: 19316 RVA: 0x00126944 File Offset: 0x00124B44
	public int depth
	{
		get
		{
			return this.mDepth;
		}
		set
		{
			if (this.mDepth != value)
			{
				this.mDepth = value;
				if (this.mPanel != null)
				{
					this.mPanel.MarkMaterialAsChanged(this.material, true);
				}
			}
		}
	}

	// Token: 0x17000E3F RID: 3647
	// (get) Token: 0x06004B75 RID: 19317 RVA: 0x00126988 File Offset: 0x00124B88
	public Transform cachedTransform
	{
		get
		{
			if (!this.gotCachedTransform)
			{
				this.mTrans = base.transform;
				this.gotCachedTransform = true;
			}
			return this.mTrans;
		}
	}

	// Token: 0x17000E40 RID: 3648
	// (get) Token: 0x06004B76 RID: 19318 RVA: 0x001269BC File Offset: 0x00124BBC
	// (set) Token: 0x06004B77 RID: 19319 RVA: 0x001269DC File Offset: 0x00124BDC
	public global::UIMaterial material
	{
		get
		{
			if ((byte)(this.widgetFlags & global::UIWidget.WidgetFlags.CustomMaterialGet) == 4)
			{
				return this.customMaterial;
			}
			return this.baseMaterial;
		}
		set
		{
			if ((byte)(this.widgetFlags & global::UIWidget.WidgetFlags.CustomMaterialSet) == 8)
			{
				this.customMaterial = value;
			}
			else
			{
				this.baseMaterial = value;
			}
		}
	}

	// Token: 0x17000E41 RID: 3649
	// (get) Token: 0x06004B78 RID: 19320 RVA: 0x00126A0C File Offset: 0x00124C0C
	// (set) Token: 0x06004B79 RID: 19321 RVA: 0x00126A1C File Offset: 0x00124C1C
	protected global::UIMaterial baseMaterial
	{
		get
		{
			return (global::UIMaterial)this.mMat;
		}
		set
		{
			global::UIMaterial uimaterial = (global::UIMaterial)this.mMat;
			if (uimaterial != value)
			{
				if (uimaterial != null && this.mPanel != null)
				{
					this.mPanel.RemoveWidget(this);
				}
				this.mPanel = null;
				this.mMat = (Material)value;
				this.mTex = null;
				if (this.mMat != null)
				{
					this.CreatePanel();
				}
			}
		}
	}

	// Token: 0x06004B7A RID: 19322 RVA: 0x00126A9C File Offset: 0x00124C9C
	public void ForceReloadMaterial()
	{
		if (this.mMat)
		{
			if (this.mPanel)
			{
				this.mPanel.RemoveWidget(this);
			}
			this.mPanel = null;
			this.mTex = null;
			if (this.mMat)
			{
				this.CreatePanel();
			}
		}
	}

	// Token: 0x17000E42 RID: 3650
	// (get) Token: 0x06004B7B RID: 19323 RVA: 0x00126AFC File Offset: 0x00124CFC
	// (set) Token: 0x06004B7C RID: 19324 RVA: 0x00126B04 File Offset: 0x00124D04
	protected virtual global::UIMaterial customMaterial
	{
		get
		{
			throw new NotSupportedException();
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	// Token: 0x17000E43 RID: 3651
	// (get) Token: 0x06004B7D RID: 19325 RVA: 0x00126B0C File Offset: 0x00124D0C
	public Texture mainTexture
	{
		get
		{
			if (!this.mTex)
			{
				global::UIMaterial material = this.material;
				if (material != null)
				{
					this.mTex = material.mainTexture;
				}
			}
			return this.mTex;
		}
	}

	// Token: 0x17000E44 RID: 3652
	// (get) Token: 0x06004B7E RID: 19326 RVA: 0x00126B50 File Offset: 0x00124D50
	// (set) Token: 0x06004B7F RID: 19327 RVA: 0x00126B70 File Offset: 0x00124D70
	public global::UIPanel panel
	{
		get
		{
			if (!this.mPanel)
			{
				this.CreatePanel();
			}
			return this.mPanel;
		}
		set
		{
			this.mPanel = value;
		}
	}

	// Token: 0x17000E45 RID: 3653
	// (get) Token: 0x06004B80 RID: 19328 RVA: 0x00126B7C File Offset: 0x00124D7C
	// (set) Token: 0x06004B81 RID: 19329 RVA: 0x00126B84 File Offset: 0x00124D84
	public int visibleFlag
	{
		get
		{
			return this.mVisibleFlag;
		}
		set
		{
			this.mVisibleFlag = value;
		}
	}

	// Token: 0x06004B82 RID: 19330 RVA: 0x00126B90 File Offset: 0x00124D90
	public static int CompareFunc(global::UIWidget left, global::UIWidget right)
	{
		if (left.mDepth > right.mDepth)
		{
			return 1;
		}
		if (left.mDepth < right.mDepth)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06004B83 RID: 19331 RVA: 0x00126BBC File Offset: 0x00124DBC
	public virtual void MarkAsChanged()
	{
		this.mChangedCall = true;
		if (this.mPanel != null && base.enabled && base.gameObject.activeInHierarchy && !Application.isPlaying && this.material != null)
		{
			this.mPanel.AddWidget(this);
			this.CheckLayer();
		}
	}

	// Token: 0x06004B84 RID: 19332 RVA: 0x00126C2C File Offset: 0x00124E2C
	private void CreatePanel()
	{
		if (!this.mPanel && base.enabled && base.gameObject.activeInHierarchy && this.material != null)
		{
			this.mPanel = global::UIPanel.Find(this.cachedTransform);
			if (this.mPanel != null)
			{
				this.CheckLayer();
				this.mPanel.AddWidget(this);
				this.mChangedCall = true;
			}
		}
	}

	// Token: 0x06004B85 RID: 19333 RVA: 0x00126CB0 File Offset: 0x00124EB0
	private void CheckLayer()
	{
		if (this.mPanel != null && this.mPanel.gameObject.layer != base.gameObject.layer)
		{
			Debug.LogWarning("You can't place widgets on a layer different than the UIPanel that manages them.\nIf you want to move widgets to a different layer, parent them to a new panel instead.", this);
			base.gameObject.layer = this.mPanel.gameObject.layer;
		}
	}

	// Token: 0x06004B86 RID: 19334 RVA: 0x00126D14 File Offset: 0x00124F14
	private void CheckParent()
	{
		if (this.mPanel != null)
		{
			bool flag = true;
			Transform parent = this.cachedTransform.parent;
			while (parent != null)
			{
				if (parent == this.mPanel.cachedTransform)
				{
					break;
				}
				if (!this.mPanel.WatchesTransform(parent))
				{
					flag = false;
					break;
				}
				parent = parent.parent;
			}
			if (!flag)
			{
				if ((byte)(this.widgetFlags & global::UIWidget.WidgetFlags.KeepsMaterial) != 64)
				{
					this.material = null;
				}
				this.mPanel = null;
				this.CreatePanel();
			}
		}
	}

	// Token: 0x06004B87 RID: 19335 RVA: 0x00126DB8 File Offset: 0x00124FB8
	protected void Awake()
	{
		this.mPlayMode = Application.isPlaying;
		global::UIWidget.Global.RegisterWidget(this);
	}

	// Token: 0x06004B88 RID: 19336 RVA: 0x00126DCC File Offset: 0x00124FCC
	private void OnEnable()
	{
		global::UIWidget.Global.WidgetEnabled(this);
		this.mChangedCall = true;
		if ((byte)(this.widgetFlags & global::UIWidget.WidgetFlags.KeepsMaterial) != 64)
		{
			this.mMat = null;
			this.mTex = null;
		}
		if (this.mPanel != null && this.material != null)
		{
			this.mPanel.MarkMaterialAsChanged(this.material, false);
		}
	}

	// Token: 0x06004B89 RID: 19337 RVA: 0x00126E3C File Offset: 0x0012503C
	private void Start()
	{
		this.OnStart();
		this.CreatePanel();
	}

	// Token: 0x06004B8A RID: 19338 RVA: 0x00126E4C File Offset: 0x0012504C
	private void DefaultUpdate()
	{
		if (!this.mPanel)
		{
			this.CreatePanel();
		}
		Vector3 localScale = this.cachedTransform.localScale;
		if (localScale.z != 1f)
		{
			localScale.z = 1f;
			this.mTrans.localScale = localScale;
		}
	}

	// Token: 0x06004B8B RID: 19339 RVA: 0x00126EA4 File Offset: 0x001250A4
	private void OnDisable()
	{
		global::UIWidget.Global.WidgetDisabled(this);
		if ((byte)(this.widgetFlags & global::UIWidget.WidgetFlags.KeepsMaterial) != 64)
		{
			this.material = null;
		}
		else if (this.mPanel != null)
		{
			this.mPanel.RemoveWidget(this);
		}
		this.mPanel = null;
	}

	// Token: 0x06004B8C RID: 19340 RVA: 0x00126EF8 File Offset: 0x001250F8
	private void OnDestroy()
	{
		global::UIWidget.Global.UnregisterWidget(this);
		if (this.mPanel != null)
		{
			this.mPanel.RemoveWidget(this);
			this.mPanel = null;
		}
		this.__mGeom = null;
	}

	// Token: 0x06004B8D RID: 19341 RVA: 0x00126F2C File Offset: 0x0012512C
	public bool UpdateGeometry(ref Matrix4x4 worldToPanel, bool parentMoved, bool generateNormals)
	{
		if (!this.material)
		{
			return false;
		}
		global::UIGeometry mGeom = this.mGeom;
		if (this.OnUpdate() || this.mChangedCall || this.mForcedChanged)
		{
			this.mChangedCall = false;
			this.mForcedChanged = false;
			mGeom.Clear();
			if (this.mAlphaUnchecked || !global::NGUITools.ZeroAlpha(this.mColor.a))
			{
				this.OnFill(mGeom.meshBuffer);
			}
			if (mGeom.hasVertices)
			{
				Vector2 vector;
				Vector2 vector2;
				switch ((byte)(this.widgetFlags & (global::UIWidget.WidgetFlags.CustomPivotOffset | global::UIWidget.WidgetFlags.CustomRelativeSize)))
				{
				case 1:
					global::UIWidget.tempWidgetFlags[0] = global::UIWidget.WidgetFlags.CustomPivotOffset;
					this.GetCustomVector2s(0, 1, global::UIWidget.tempWidgetFlags, global::UIWidget.tempVector2s);
					vector = global::UIWidget.tempVector2s[0];
					vector2.x = (vector2.y = 1f);
					break;
				case 2:
					global::UIWidget.tempWidgetFlags[0] = global::UIWidget.WidgetFlags.CustomRelativeSize;
					this.GetCustomVector2s(0, 1, global::UIWidget.tempWidgetFlags, global::UIWidget.tempVector2s);
					vector2 = global::UIWidget.tempVector2s[0];
					vector = global::UIWidget.DefaultPivot(this.mPivot);
					break;
				case 3:
					global::UIWidget.tempWidgetFlags[0] = global::UIWidget.WidgetFlags.CustomPivotOffset;
					global::UIWidget.tempWidgetFlags[1] = global::UIWidget.WidgetFlags.CustomRelativeSize;
					this.GetCustomVector2s(0, 2, global::UIWidget.tempWidgetFlags, global::UIWidget.tempVector2s);
					vector = global::UIWidget.tempVector2s[0];
					vector2 = global::UIWidget.tempVector2s[1];
					break;
				default:
					vector = global::UIWidget.DefaultPivot(this.mPivot);
					vector2.x = (vector2.y = 1f);
					break;
				}
				Vector3 vector3;
				vector3.x = vector.x * vector2.x;
				vector3.y = vector.y * vector2.y;
				vector3.z = 0f;
				Matrix4x4 matrix4x = worldToPanel * this.cachedTransform.localToWorldMatrix;
				mGeom.Apply(ref vector3, ref matrix4x);
			}
			return true;
		}
		if (mGeom.hasVertices && parentMoved)
		{
			Matrix4x4 matrix4x2 = worldToPanel * this.cachedTransform.localToWorldMatrix;
			mGeom.Apply(ref matrix4x2);
		}
		return false;
	}

	// Token: 0x06004B8E RID: 19342 RVA: 0x00127168 File Offset: 0x00125368
	public void WriteToBuffers(NGUI.Meshing.MeshBuffer m)
	{
		this.mGeom.WriteToBuffers(m);
	}

	// Token: 0x06004B8F RID: 19343 RVA: 0x00127178 File Offset: 0x00125378
	public virtual void MakePixelPerfect()
	{
		Vector3 localScale = this.cachedTransform.localScale;
		int num = Mathf.RoundToInt(localScale.x);
		int num2 = Mathf.RoundToInt(localScale.y);
		localScale.x = (float)num;
		localScale.y = (float)num2;
		localScale.z = 1f;
		Vector3 localPosition = this.cachedTransform.localPosition;
		localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
		if (num % 2 == 1 && (this.pivot == global::UIWidget.Pivot.Top || this.pivot == global::UIWidget.Pivot.Center || this.pivot == global::UIWidget.Pivot.Bottom))
		{
			localPosition.x = Mathf.Floor(localPosition.x) + 0.5f;
		}
		else
		{
			localPosition.x = Mathf.Round(localPosition.x);
		}
		if (num2 % 2 == 1 && (this.pivot == global::UIWidget.Pivot.Left || this.pivot == global::UIWidget.Pivot.Center || this.pivot == global::UIWidget.Pivot.Right))
		{
			localPosition.y = Mathf.Ceil(localPosition.y) - 0.5f;
		}
		else
		{
			localPosition.y = Mathf.Round(localPosition.y);
		}
		this.cachedTransform.localPosition = localPosition;
		this.cachedTransform.localScale = localScale;
	}

	// Token: 0x06004B90 RID: 19344 RVA: 0x001272C0 File Offset: 0x001254C0
	protected static Vector2 DefaultPivot(global::UIWidget.Pivot pivot)
	{
		Vector2 result;
		switch (pivot)
		{
		case global::UIWidget.Pivot.TopLeft:
			result.x = 0f;
			result.y = 0f;
			break;
		case global::UIWidget.Pivot.Top:
			result.y = -0.5f;
			result.x = -1f;
			break;
		case global::UIWidget.Pivot.TopRight:
			result.y = 0f;
			result.x = -1f;
			break;
		case global::UIWidget.Pivot.Left:
			result.x = 0f;
			result.y = 0.5f;
			break;
		case global::UIWidget.Pivot.Center:
			result.x = -0.5f;
			result.y = 0.5f;
			break;
		case global::UIWidget.Pivot.Right:
			result.x = -1f;
			result.y = 0.5f;
			break;
		case global::UIWidget.Pivot.BottomLeft:
			result.x = 0f;
			result.y = 1f;
			break;
		case global::UIWidget.Pivot.Bottom:
			result.x = -0.5f;
			result.y = 1f;
			break;
		case global::UIWidget.Pivot.BottomRight:
			result.x = -1f;
			result.y = 1f;
			break;
		default:
			throw new NotImplementedException();
		}
		return result;
	}

	// Token: 0x17000E46 RID: 3654
	// (get) Token: 0x06004B91 RID: 19345 RVA: 0x0012740C File Offset: 0x0012560C
	public Vector2 pivotOffset
	{
		get
		{
			if ((byte)(this.widgetFlags & global::UIWidget.WidgetFlags.CustomPivotOffset) == 1)
			{
				global::UIWidget.tempWidgetFlags[0] = global::UIWidget.WidgetFlags.CustomPivotOffset;
				this.GetCustomVector2s(0, 1, global::UIWidget.tempWidgetFlags, global::UIWidget.tempVector2s);
				return global::UIWidget.tempVector2s[0];
			}
			return global::UIWidget.DefaultPivot(this.mPivot);
		}
	}

	// Token: 0x17000E47 RID: 3655
	// (get) Token: 0x06004B92 RID: 19346 RVA: 0x00127460 File Offset: 0x00125660
	[Obsolete("Use 'relativeSize' instead")]
	public Vector2 visibleSize
	{
		get
		{
			return this.relativeSize;
		}
	}

	// Token: 0x06004B93 RID: 19347 RVA: 0x00127468 File Offset: 0x00125668
	protected virtual void GetCustomVector2s(int start, int end, global::UIWidget.WidgetFlags[] flags, Vector2[] v)
	{
		throw new NotSupportedException("Only call base.GetCustomVector2s when its something not supported by your implementation, otherwise the custructor for your class is incorrect in its usage.");
	}

	// Token: 0x17000E48 RID: 3656
	// (get) Token: 0x06004B94 RID: 19348 RVA: 0x00127474 File Offset: 0x00125674
	public Vector2 relativeSize
	{
		get
		{
			if ((byte)(this.widgetFlags & global::UIWidget.WidgetFlags.CustomRelativeSize) == 2)
			{
				global::UIWidget.tempWidgetFlags[0] = global::UIWidget.WidgetFlags.CustomRelativeSize;
				this.GetCustomVector2s(0, 1, global::UIWidget.tempWidgetFlags, global::UIWidget.tempVector2s);
				return global::UIWidget.tempVector2s[0];
			}
			return Vector2.one;
		}
	}

	// Token: 0x17000E49 RID: 3657
	// (get) Token: 0x06004B95 RID: 19349 RVA: 0x001274C0 File Offset: 0x001256C0
	public bool keepMaterial
	{
		get
		{
			return (byte)(this.widgetFlags & global::UIWidget.WidgetFlags.KeepsMaterial) == 64;
		}
	}

	// Token: 0x06004B96 RID: 19350 RVA: 0x001274D0 File Offset: 0x001256D0
	protected virtual void OnStart()
	{
	}

	// Token: 0x06004B97 RID: 19351 RVA: 0x001274D4 File Offset: 0x001256D4
	public virtual bool OnUpdate()
	{
		return false;
	}

	// Token: 0x06004B98 RID: 19352
	public abstract void OnFill(NGUI.Meshing.MeshBuffer m);

	// Token: 0x06004B99 RID: 19353 RVA: 0x001274D8 File Offset: 0x001256D8
	public void GetPivotOffsetAndRelativeSize(out Vector2 pivotOffset, out Vector2 relativeSize)
	{
		switch ((byte)(this.widgetFlags & (global::UIWidget.WidgetFlags.CustomPivotOffset | global::UIWidget.WidgetFlags.CustomRelativeSize)))
		{
		case 1:
			global::UIWidget.tempWidgetFlags[0] = global::UIWidget.WidgetFlags.CustomPivotOffset;
			this.GetCustomVector2s(0, 1, global::UIWidget.tempWidgetFlags, global::UIWidget.tempVector2s);
			pivotOffset = global::UIWidget.tempVector2s[0];
			relativeSize.x = (relativeSize.y = 1f);
			break;
		case 2:
			global::UIWidget.tempWidgetFlags[0] = global::UIWidget.WidgetFlags.CustomRelativeSize;
			this.GetCustomVector2s(0, 1, global::UIWidget.tempWidgetFlags, global::UIWidget.tempVector2s);
			relativeSize = global::UIWidget.tempVector2s[0];
			pivotOffset = global::UIWidget.DefaultPivot(this.mPivot);
			break;
		case 3:
			global::UIWidget.tempWidgetFlags[0] = global::UIWidget.WidgetFlags.CustomPivotOffset;
			global::UIWidget.tempWidgetFlags[1] = global::UIWidget.WidgetFlags.CustomRelativeSize;
			this.GetCustomVector2s(0, 2, global::UIWidget.tempWidgetFlags, global::UIWidget.tempVector2s);
			pivotOffset = global::UIWidget.tempVector2s[0];
			relativeSize = global::UIWidget.tempVector2s[1];
			break;
		default:
			pivotOffset = global::UIWidget.DefaultPivot(this.mPivot);
			relativeSize.x = (relativeSize.y = 1f);
			break;
		}
	}

	// Token: 0x04002933 RID: 10547
	[SerializeField]
	[HideInInspector]
	private Material mMat;

	// Token: 0x04002934 RID: 10548
	[SerializeField]
	[HideInInspector]
	private Color mColor = Color.white;

	// Token: 0x04002935 RID: 10549
	[HideInInspector]
	[SerializeField]
	private global::UIWidget.Pivot mPivot = global::UIWidget.Pivot.Center;

	// Token: 0x04002936 RID: 10550
	[HideInInspector]
	[SerializeField]
	private int mDepth;

	// Token: 0x04002937 RID: 10551
	[SerializeField]
	[HideInInspector]
	private bool mAlphaUnchecked;

	// Token: 0x04002938 RID: 10552
	[NonSerialized]
	private bool mForcedChanged;

	// Token: 0x04002939 RID: 10553
	private Transform mTrans;

	// Token: 0x0400293A RID: 10554
	private Texture mTex;

	// Token: 0x0400293B RID: 10555
	private global::UIPanel mPanel;

	// Token: 0x0400293C RID: 10556
	private bool mChangedCall = true;

	// Token: 0x0400293D RID: 10557
	protected bool mPlayMode = true;

	// Token: 0x0400293E RID: 10558
	private bool gotCachedTransform;

	// Token: 0x0400293F RID: 10559
	[NonSerialized]
	protected readonly global::UIWidget.WidgetFlags widgetFlags;

	// Token: 0x04002940 RID: 10560
	private Vector3 mDiffPos;

	// Token: 0x04002941 RID: 10561
	private Quaternion mDiffRot;

	// Token: 0x04002942 RID: 10562
	private Vector3 mDiffScale;

	// Token: 0x04002943 RID: 10563
	private int mVisibleFlag = -1;

	// Token: 0x04002944 RID: 10564
	private int globalIndex = -1;

	// Token: 0x04002945 RID: 10565
	private global::UIGeometry __mGeom;

	// Token: 0x04002946 RID: 10566
	private static Vector2[] tempVector2s = new Vector2[]
	{
		default(Vector2),
		default(Vector2)
	};

	// Token: 0x04002947 RID: 10567
	private static global::UIWidget.WidgetFlags[] tempWidgetFlags = new global::UIWidget.WidgetFlags[2];

	// Token: 0x02000896 RID: 2198
	[Flags]
	protected enum WidgetFlags : byte
	{
		// Token: 0x04002949 RID: 10569
		CustomPivotOffset = 1,
		// Token: 0x0400294A RID: 10570
		CustomRelativeSize = 2,
		// Token: 0x0400294B RID: 10571
		CustomMaterialGet = 4,
		// Token: 0x0400294C RID: 10572
		CustomMaterialSet = 8,
		// Token: 0x0400294D RID: 10573
		CustomBorder = 16,
		// Token: 0x0400294E RID: 10574
		KeepsMaterial = 64,
		// Token: 0x0400294F RID: 10575
		Reserved = 128
	}

	// Token: 0x02000897 RID: 2199
	public enum Pivot
	{
		// Token: 0x04002951 RID: 10577
		TopLeft,
		// Token: 0x04002952 RID: 10578
		Top,
		// Token: 0x04002953 RID: 10579
		TopRight,
		// Token: 0x04002954 RID: 10580
		Left,
		// Token: 0x04002955 RID: 10581
		Center,
		// Token: 0x04002956 RID: 10582
		Right,
		// Token: 0x04002957 RID: 10583
		BottomLeft,
		// Token: 0x04002958 RID: 10584
		Bottom,
		// Token: 0x04002959 RID: 10585
		BottomRight
	}

	// Token: 0x02000898 RID: 2200
	private static class Global
	{
		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06004B9A RID: 19354 RVA: 0x00127614 File Offset: 0x00125814
		private static bool noGlobal
		{
			get
			{
				return !Application.isPlaying;
			}
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x00127620 File Offset: 0x00125820
		public static void RegisterWidget(global::UIWidget widget)
		{
			if (global::UIWidget.Global.noGlobal)
			{
				return;
			}
			global::UIGlobal.EnsureGlobal();
			if (widget.globalIndex == -1)
			{
				widget.globalIndex = global::UIWidget.Global.g.allWidgets.Count;
				global::UIWidget.Global.g.allWidgets.Add(widget);
			}
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x0012765C File Offset: 0x0012585C
		public static void UnregisterWidget(global::UIWidget widget)
		{
			if (global::UIWidget.Global.noGlobal)
			{
				return;
			}
			if (widget.globalIndex != -1)
			{
				global::UIWidget.Global.g.allWidgets.RemoveAt(widget.globalIndex);
				int i = widget.globalIndex;
				int count = global::UIWidget.Global.g.allWidgets.Count;
				while (i < count)
				{
					global::UIWidget.Global.g.allWidgets[i].globalIndex = i;
					i++;
				}
				widget.globalIndex = -1;
			}
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x001276CC File Offset: 0x001258CC
		public static void WidgetEnabled(global::UIWidget widget)
		{
			if (global::UIWidget.Global.noGlobal)
			{
				return;
			}
			global::UIWidget.Global.g.enabledWidgets.Add(widget);
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x001276E8 File Offset: 0x001258E8
		public static void WidgetDisabled(global::UIWidget widget)
		{
			if (global::UIWidget.Global.noGlobal)
			{
				return;
			}
			global::UIWidget.Global.g.enabledWidgets.Remove(widget);
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x00127704 File Offset: 0x00125904
		public static void WidgetUpdate()
		{
			if (global::UIWidget.Global.noGlobal)
			{
				return;
			}
			try
			{
				global::UIWidget.Global.g.enableWidgetsSwap.AddRange(global::UIWidget.Global.g.enabledWidgets);
				foreach (global::UIWidget uiwidget in global::UIWidget.Global.g.enableWidgetsSwap)
				{
					if (uiwidget && uiwidget.enabled)
					{
						uiwidget.DefaultUpdate();
					}
				}
			}
			finally
			{
				global::UIWidget.Global.g.enableWidgetsSwap.Clear();
			}
		}

		// Token: 0x02000899 RID: 2201
		public static class g
		{
			// Token: 0x06004BA0 RID: 19360 RVA: 0x001277C0 File Offset: 0x001259C0
			static g()
			{
				global::UIGlobal.EnsureGlobal();
			}

			// Token: 0x0400295A RID: 10586
			public static List<global::UIWidget> allWidgets = new List<global::UIWidget>();

			// Token: 0x0400295B RID: 10587
			public static HashSet<global::UIWidget> enabledWidgets = new HashSet<global::UIWidget>();

			// Token: 0x0400295C RID: 10588
			public static List<global::UIWidget> enableWidgetsSwap = new List<global::UIWidget>();
		}
	}
}
