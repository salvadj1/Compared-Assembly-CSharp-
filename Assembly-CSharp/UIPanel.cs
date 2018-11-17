using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x020008EA RID: 2282
[AddComponentMenu("NGUI/UI/Panel")]
[ExecuteInEditMode]
public class UIPanel : MonoBehaviour
{
	// Token: 0x06004DD2 RID: 19922 RVA: 0x00136050 File Offset: 0x00134250
	public static void GlobalUpdate()
	{
		global::UIPanel.Global.PanelUpdate();
	}

	// Token: 0x17000EE0 RID: 3808
	// (get) Token: 0x06004DD3 RID: 19923 RVA: 0x00136058 File Offset: 0x00134258
	// (set) Token: 0x06004DD4 RID: 19924 RVA: 0x00136078 File Offset: 0x00134278
	public global::UIPanel RootPanel
	{
		get
		{
			return (!this._rootPanel) ? this : this._rootPanel;
		}
		set
		{
			if (value == this)
			{
				this._rootPanel = null;
			}
			else
			{
				this._rootPanel = value;
			}
		}
	}

	// Token: 0x17000EE1 RID: 3809
	// (get) Token: 0x06004DD5 RID: 19925 RVA: 0x0013609C File Offset: 0x0013429C
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

	// Token: 0x17000EE2 RID: 3810
	// (get) Token: 0x06004DD6 RID: 19926 RVA: 0x001360C4 File Offset: 0x001342C4
	public bool changedLastFrame
	{
		get
		{
			return this.mChangedLastFrame;
		}
	}

	// Token: 0x17000EE3 RID: 3811
	// (get) Token: 0x06004DD7 RID: 19927 RVA: 0x001360CC File Offset: 0x001342CC
	// (set) Token: 0x06004DD8 RID: 19928 RVA: 0x001360D4 File Offset: 0x001342D4
	public global::UIPanel.DebugInfo debugInfo
	{
		get
		{
			return this.mDebugInfo;
		}
		set
		{
			if (this.mDebugInfo != value)
			{
				this.mDebugInfo = value;
				global::UIDrawCall.Iterator iterator = (global::UIDrawCall.Iterator)this.mDrawCalls;
				HideFlags hideFlags = (this.mDebugInfo != global::UIPanel.DebugInfo.Geometry) ? 13 : 12;
				while (iterator.Has)
				{
					GameObject gameObject = iterator.Current.gameObject;
					iterator = iterator.Next;
					gameObject.SetActive(false);
					gameObject.hideFlags = hideFlags;
					gameObject.SetActive(true);
				}
			}
		}
	}

	// Token: 0x17000EE4 RID: 3812
	// (get) Token: 0x06004DD9 RID: 19929 RVA: 0x00136154 File Offset: 0x00134354
	// (set) Token: 0x06004DDA RID: 19930 RVA: 0x0013615C File Offset: 0x0013435C
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
				this.mCheckVisibility = true;
				this.mClipping = value;
				this.UpdateDrawcalls();
			}
		}
	}

	// Token: 0x17000EE5 RID: 3813
	// (get) Token: 0x06004DDB RID: 19931 RVA: 0x0013618C File Offset: 0x0013438C
	// (set) Token: 0x06004DDC RID: 19932 RVA: 0x00136194 File Offset: 0x00134394
	public Vector4 clipRange
	{
		get
		{
			return this.mClipRange;
		}
		set
		{
			if (this.mClipRange != value)
			{
				this.mCullTime = ((this.mCullTime != 0f) ? (Time.realtimeSinceStartup + 0.15f) : 0.001f);
				this.mCheckVisibility = true;
				this.mClipRange = value;
				this.UpdateDrawcalls();
			}
		}
	}

	// Token: 0x17000EE6 RID: 3814
	// (get) Token: 0x06004DDD RID: 19933 RVA: 0x001361F4 File Offset: 0x001343F4
	// (set) Token: 0x06004DDE RID: 19934 RVA: 0x001361FC File Offset: 0x001343FC
	public Vector2 clipSoftness
	{
		get
		{
			return this.mClipSoftness;
		}
		set
		{
			if (this.mClipSoftness != value)
			{
				this.mClipSoftness = value;
				this.UpdateDrawcalls();
			}
		}
	}

	// Token: 0x17000EE7 RID: 3815
	// (get) Token: 0x06004DDF RID: 19935 RVA: 0x0013621C File Offset: 0x0013441C
	public List<global::UIWidget> widgets
	{
		get
		{
			return this.mWidgets;
		}
	}

	// Token: 0x17000EE8 RID: 3816
	// (get) Token: 0x06004DE0 RID: 19936 RVA: 0x00136224 File Offset: 0x00134424
	public global::UIDrawCall.Iterator drawCalls
	{
		get
		{
			return (global::UIDrawCall.Iterator)this.mDrawCalls;
		}
	}

	// Token: 0x17000EE9 RID: 3817
	// (get) Token: 0x06004DE1 RID: 19937 RVA: 0x00136234 File Offset: 0x00134434
	public int drawCallCount
	{
		get
		{
			return this.mDrawCallCount;
		}
	}

	// Token: 0x17000EEA RID: 3818
	// (get) Token: 0x06004DE2 RID: 19938 RVA: 0x0013623C File Offset: 0x0013443C
	public bool manUp
	{
		get
		{
			return this.manualPanelUpdate;
		}
	}

	// Token: 0x06004DE3 RID: 19939 RVA: 0x00136244 File Offset: 0x00134444
	private global::UINode GetNode(Transform t)
	{
		global::UINode result = null;
		if (t != null && this.mChildren.Contains(t))
		{
			result = (global::UINode)this.mChildren[t];
		}
		return result;
	}

	// Token: 0x06004DE4 RID: 19940 RVA: 0x00136284 File Offset: 0x00134484
	private bool IsVisible(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		this.UpdateTransformMatrix();
		a = this.mWorldToLocal.MultiplyPoint3x4(a);
		b = this.mWorldToLocal.MultiplyPoint3x4(b);
		c = this.mWorldToLocal.MultiplyPoint3x4(c);
		d = this.mWorldToLocal.MultiplyPoint3x4(d);
		global::UIPanel.mTemp[0] = a.x;
		global::UIPanel.mTemp[1] = b.x;
		global::UIPanel.mTemp[2] = c.x;
		global::UIPanel.mTemp[3] = d.x;
		float num = Mathf.Min(global::UIPanel.mTemp);
		float num2 = Mathf.Max(global::UIPanel.mTemp);
		global::UIPanel.mTemp[0] = a.y;
		global::UIPanel.mTemp[1] = b.y;
		global::UIPanel.mTemp[2] = c.y;
		global::UIPanel.mTemp[3] = d.y;
		float num3 = Mathf.Min(global::UIPanel.mTemp);
		float num4 = Mathf.Max(global::UIPanel.mTemp);
		return num2 >= this.mMin.x && num4 >= this.mMin.y && num <= this.mMax.x && num3 <= this.mMax.y;
	}

	// Token: 0x06004DE5 RID: 19941 RVA: 0x001363BC File Offset: 0x001345BC
	public bool IsVisible(global::UIWidget w)
	{
		if (!w.enabled || !w.gameObject.activeInHierarchy || w.color.a < 0.001f)
		{
			return false;
		}
		if (this.mClipping == global::UIDrawCall.Clipping.None)
		{
			return true;
		}
		Vector2 relativeSize = w.relativeSize;
		Vector2 vector = Vector2.Scale(w.pivotOffset, relativeSize);
		Vector2 vector2 = vector;
		vector.x += relativeSize.x;
		vector.y -= relativeSize.y;
		Transform cachedTransform = w.cachedTransform;
		Vector3 a = cachedTransform.TransformPoint(vector);
		Vector3 b = cachedTransform.TransformPoint(new Vector2(vector.x, vector2.y));
		Vector3 c = cachedTransform.TransformPoint(new Vector2(vector2.x, vector.y));
		Vector3 d = cachedTransform.TransformPoint(vector2);
		return this.IsVisible(a, b, c, d);
	}

	// Token: 0x06004DE6 RID: 19942 RVA: 0x001364C0 File Offset: 0x001346C0
	public void MarkMaterialAsChanged(global::UIMaterial mat, bool sort)
	{
		if (mat)
		{
			if (sort)
			{
				this.mDepthChanged = true;
			}
			if (this.mChanged.Add(mat))
			{
				this.mChangedLastFrame = true;
			}
		}
	}

	// Token: 0x06004DE7 RID: 19943 RVA: 0x00136500 File Offset: 0x00134700
	public bool WatchesTransform(Transform t)
	{
		return t == this.cachedTransform || this.mChildren.Contains(t);
	}

	// Token: 0x06004DE8 RID: 19944 RVA: 0x00136530 File Offset: 0x00134730
	private global::UINode AddTransform(Transform t)
	{
		global::UINode uinode = null;
		while (t != null && t != this.cachedTransform)
		{
			if (this.mChildren.Contains(t))
			{
				if (uinode == null)
				{
					uinode = (global::UINode)this.mChildren[t];
				}
				break;
			}
			global::UINode uinode2 = new global::UINode(t);
			if (uinode == null)
			{
				uinode = uinode2;
			}
			this.mChildren.Add(t, uinode2);
			t = t.parent;
		}
		return uinode;
	}

	// Token: 0x06004DE9 RID: 19945 RVA: 0x001365B8 File Offset: 0x001347B8
	private void RemoveTransform(Transform t)
	{
		if (t != null)
		{
			while (this.mChildren.Contains(t))
			{
				this.mChildren.Remove(t);
				t = t.parent;
				if (t == null || t == this.mTrans || t.childCount > 1)
				{
					break;
				}
			}
		}
	}

	// Token: 0x06004DEA RID: 19946 RVA: 0x0013662C File Offset: 0x0013482C
	public void AddWidget(global::UIWidget w)
	{
		if (w != null)
		{
			global::UINode uinode = this.AddTransform(w.cachedTransform);
			if (uinode != null)
			{
				uinode.widget = w;
				if (!this.mWidgets.Contains(w))
				{
					this.mWidgets.Add(w);
					if (!this.mChanged.Contains(w.material))
					{
						this.mChanged.Add(w.material);
						this.mChangedLastFrame = true;
					}
					this.mDepthChanged = true;
				}
			}
			else
			{
				Debug.LogError("Unable to find an appropriate UIRoot for " + global::NGUITools.GetHierarchy(w.gameObject) + "\nPlease make sure that there is at least one game object above this widget!", w.gameObject);
			}
		}
	}

	// Token: 0x06004DEB RID: 19947 RVA: 0x001366DC File Offset: 0x001348DC
	public void RemoveWidget(global::UIWidget w)
	{
		if (w != null)
		{
			global::UINode node = this.GetNode(w.cachedTransform);
			if (node != null)
			{
				if (node.visibleFlag == 1 && !this.mChanged.Contains(w.material))
				{
					this.mChanged.Add(w.material);
					this.mChangedLastFrame = true;
				}
				this.RemoveTransform(w.cachedTransform);
			}
			this.mWidgets.Remove(w);
		}
	}

	// Token: 0x06004DEC RID: 19948 RVA: 0x0013675C File Offset: 0x0013495C
	private global::UIDrawCall.Iterator GetDrawCall(global::UIMaterial mat, bool createIfMissing)
	{
		global::UIDrawCall.Iterator result = (global::UIDrawCall.Iterator)this.mDrawCalls;
		while (result.Has)
		{
			if (result.Current.material == mat)
			{
				return result;
			}
			result = result.Next;
		}
		global::UIDrawCall uidrawCall = null;
		if (createIfMissing)
		{
			uidrawCall = new GameObject("_UIDrawCall [" + mat.name + "]")
			{
				hideFlags = 4,
				layer = base.gameObject.layer
			}.AddComponent<global::UIDrawCall>();
			uidrawCall.material = mat;
			uidrawCall.LinkedList__Insert(ref this.mDrawCalls);
			this.mDrawCallCount++;
		}
		return (global::UIDrawCall.Iterator)uidrawCall;
	}

	// Token: 0x06004DED RID: 19949 RVA: 0x00136810 File Offset: 0x00134A10
	protected void Awake()
	{
		global::UIPanel.Global.RegisterPanel(this);
	}

	// Token: 0x06004DEE RID: 19950 RVA: 0x00136818 File Offset: 0x00134A18
	protected void Start()
	{
		this.mLayer = base.gameObject.layer;
		global::UICamera uicamera = global::UICamera.FindCameraForLayer(this.mLayer);
		this.mCam = ((!(uicamera != null)) ? global::NGUITools.FindCameraForLayer(this.mLayer) : uicamera.cachedCamera);
	}

	// Token: 0x06004DEF RID: 19951 RVA: 0x0013686C File Offset: 0x00134A6C
	protected void OnEnable()
	{
		global::UIPanel.Global.PanelEnabled(this);
		if (this.mHotSpots != null)
		{
			foreach (global::UIHotSpot uihotSpot in this.mHotSpots)
			{
				uihotSpot.OnPanelEnable();
			}
		}
		int i = 0;
		int count = this.mWidgets.Count;
		while (i < count)
		{
			this.AddWidget(this.mWidgets[i]);
			i++;
		}
		this.mRebuildAll = true;
	}

	// Token: 0x06004DF0 RID: 19952 RVA: 0x0013691C File Offset: 0x00134B1C
	protected void OnDisable()
	{
		global::UIPanel.Global.PanelDisabled(this);
		if (this.mHotSpots != null)
		{
			foreach (global::UIHotSpot uihotSpot in this.mHotSpots)
			{
				uihotSpot.OnPanelDisable();
			}
		}
		global::UIDrawCall.Iterator iterator = (global::UIDrawCall.Iterator)this.mDrawCalls;
		while (iterator.Has)
		{
			global::UIDrawCall current = iterator.Current;
			iterator = iterator.Next;
			global::NGUITools.DestroyImmediate(current.gameObject);
		}
		this.mDrawCalls = null;
		this.mChanged.Clear();
		this.mChildren.Clear();
	}

	// Token: 0x06004DF1 RID: 19953 RVA: 0x001369E8 File Offset: 0x00134BE8
	protected void OnDestroy()
	{
		global::UIPanel.Global.UnregisterPanel(this);
		if (this.mHotSpots != null)
		{
			HashSet<global::UIHotSpot> hashSet = this.mHotSpots;
			this.mHotSpots = null;
			foreach (global::UIHotSpot uihotSpot in hashSet)
			{
				uihotSpot.OnPanelDestroy();
			}
		}
	}

	// Token: 0x06004DF2 RID: 19954 RVA: 0x00136A68 File Offset: 0x00134C68
	private int GetChangeFlag(global::UINode start)
	{
		int num = start.changeFlag;
		if (num == -1)
		{
			Transform parent = start.trans.parent;
			while (this.mChildren.Contains(parent))
			{
				global::UINode uinode = (global::UINode)this.mChildren[parent];
				num = uinode.changeFlag;
				parent = parent.parent;
				if (num != -1)
				{
					IL_7D:
					int i = 0;
					int count = global::UIPanel.mHierarchy.Count;
					while (i < count)
					{
						global::UINode uinode2 = global::UIPanel.mHierarchy[i];
						uinode2.changeFlag = num;
						i++;
					}
					global::UIPanel.mHierarchy.Clear();
					return num;
				}
				global::UIPanel.mHierarchy.Add(uinode);
			}
			num = 0;
			goto IL_7D;
		}
		return num;
	}

	// Token: 0x06004DF3 RID: 19955 RVA: 0x00136B34 File Offset: 0x00134D34
	private void UpdateTransformMatrix()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (realtimeSinceStartup == 0f || this.mMatrixTime != realtimeSinceStartup)
		{
			this.mMatrixTime = realtimeSinceStartup;
			this.mWorldToLocal = this.cachedTransform.worldToLocalMatrix;
			if (this.mClipping != global::UIDrawCall.Clipping.None)
			{
				Vector2 vector;
				vector..ctor(this.mClipRange.z, this.mClipRange.w);
				if (vector.x == 0f)
				{
					vector.x = ((!(this.mCam == null)) ? this.mCam.pixelWidth : ((float)Screen.width));
				}
				if (vector.y == 0f)
				{
					vector.y = ((!(this.mCam == null)) ? this.mCam.pixelHeight : ((float)Screen.height));
				}
				vector *= 0.5f;
				this.mMin.x = this.mClipRange.x - vector.x;
				this.mMin.y = this.mClipRange.y - vector.y;
				this.mMax.x = this.mClipRange.x + vector.x;
				this.mMax.y = this.mClipRange.y + vector.y;
			}
		}
	}

	// Token: 0x06004DF4 RID: 19956 RVA: 0x00136CA0 File Offset: 0x00134EA0
	private void UpdateTransforms()
	{
		this.mChangedLastFrame = false;
		bool flag = false;
		bool flag2 = Time.realtimeSinceStartup > this.mCullTime;
		if (!this.widgetsAreStatic || flag2 != this.mCulled)
		{
			int i = 0;
			int count = this.mChildren.Count;
			while (i < count)
			{
				global::UINode uinode = (global::UINode)this.mChildren[i];
				if (uinode.trans == null)
				{
					this.mRemoved.Add(uinode.trans);
				}
				else if (uinode.HasChanged())
				{
					uinode.changeFlag = 1;
					flag = true;
				}
				else
				{
					uinode.changeFlag = -1;
				}
				i++;
			}
			int j = 0;
			int count2 = this.mRemoved.Count;
			while (j < count2)
			{
				this.mChildren.Remove(this.mRemoved[j]);
				j++;
			}
			this.mRemoved.Clear();
		}
		if (!this.mCulled && flag2)
		{
			this.mCheckVisibility = true;
		}
		if (this.mCheckVisibility || flag || this.mRebuildAll)
		{
			int k = 0;
			int count3 = this.mChildren.Count;
			while (k < count3)
			{
				global::UINode uinode2 = (global::UINode)this.mChildren[k];
				if (uinode2.widget != null)
				{
					int num = 1;
					if (flag2 || flag)
					{
						if (uinode2.changeFlag == -1)
						{
							uinode2.changeFlag = this.GetChangeFlag(uinode2);
						}
						if (flag2)
						{
							num = ((!this.mCheckVisibility && uinode2.changeFlag != 1) ? uinode2.visibleFlag : ((!this.IsVisible(uinode2.widget)) ? 0 : 1));
						}
					}
					if (uinode2.visibleFlag != num)
					{
						uinode2.changeFlag = 1;
					}
					if (uinode2.changeFlag == 1 && (num == 1 || uinode2.visibleFlag != 0))
					{
						uinode2.visibleFlag = num;
						global::UIMaterial material = uinode2.widget.material;
						if (!this.mChanged.Contains(material))
						{
							this.mChanged.Add(material);
							this.mChangedLastFrame = true;
						}
					}
				}
				k++;
			}
		}
		this.mCulled = flag2;
		this.mCheckVisibility = false;
	}

	// Token: 0x06004DF5 RID: 19957 RVA: 0x00136F10 File Offset: 0x00135110
	private void UpdateWidgets()
	{
		int i = 0;
		int count = this.mChildren.Count;
		while (i < count)
		{
			global::UINode uinode = (global::UINode)this.mChildren[i];
			global::UIWidget widget = uinode.widget;
			if (uinode.visibleFlag == 1 && widget != null && widget.UpdateGeometry(ref this.mWorldToLocal, uinode.changeFlag == 1, this.generateNormals) && !this.mChanged.Contains(widget.material))
			{
				this.mChanged.Add(widget.material);
				this.mChangedLastFrame = true;
			}
			uinode.changeFlag = 0;
			i++;
		}
	}

	// Token: 0x06004DF6 RID: 19958 RVA: 0x00136FC4 File Offset: 0x001351C4
	public void UpdateDrawcalls()
	{
		Vector4 zero = Vector4.zero;
		if (this.mClipping != global::UIDrawCall.Clipping.None)
		{
			zero..ctor(this.mClipRange.x, this.mClipRange.y, this.mClipRange.z * 0.5f, this.mClipRange.w * 0.5f);
		}
		if (zero.z == 0f)
		{
			zero.z = (float)Screen.width * 0.5f;
		}
		if (zero.w == 0f)
		{
			zero.w = (float)Screen.height * 0.5f;
		}
		RuntimePlatform platform = Application.platform;
		if (platform == 2 || platform == 5 || platform == 7)
		{
			zero.x -= 0.5f;
			zero.y += 0.5f;
		}
		Vector3 position = this.cachedTransform.position;
		Quaternion rotation = this.cachedTransform.rotation;
		Vector3 lossyScale = this.cachedTransform.lossyScale;
		global::UIDrawCall.Iterator iterator = (global::UIDrawCall.Iterator)this.mDrawCalls;
		while (iterator.Has)
		{
			global::UIDrawCall current = iterator.Current;
			iterator = iterator.Next;
			current.clipping = this.mClipping;
			current.clipRange = zero;
			current.clipSoftness = this.mClipSoftness;
			current.depthPass = this.depthPass;
			current.panelPropertyBlock = this.propertyBlock;
			Transform transform = current.transform;
			transform.position = position;
			transform.rotation = rotation;
			transform.localScale = lossyScale;
		}
	}

	// Token: 0x06004DF7 RID: 19959 RVA: 0x00137160 File Offset: 0x00135360
	private void Fill(global::UIMaterial mat)
	{
		int i = this.mWidgets.Count;
		while (i > 0)
		{
			if (this.mWidgets[--i] == null)
			{
				this.mWidgets.RemoveAt(i);
			}
		}
		int j = 0;
		int count = this.mWidgets.Count;
		while (j < count)
		{
			global::UIWidget uiwidget = this.mWidgets[j];
			if (uiwidget.visibleFlag == 1 && uiwidget.material == mat)
			{
				global::UINode node = this.GetNode(uiwidget.cachedTransform);
				if (node != null)
				{
					uiwidget.WriteToBuffers(this.mCacheBuffer);
				}
				else
				{
					Debug.LogError("No transform found for " + global::NGUITools.GetHierarchy(uiwidget.gameObject), this);
				}
			}
			j++;
		}
		if (this.mCacheBuffer.vSize > 0)
		{
			global::UIDrawCall current = this.GetDrawCall(mat, true).Current;
			current.depthPass = this.depthPass;
			current.panelPropertyBlock = this.propertyBlock;
			current.Set(this.mCacheBuffer);
		}
		else
		{
			global::UIDrawCall.Iterator drawCall = this.GetDrawCall(mat, false);
			if (drawCall.Has)
			{
				this.Delete(ref drawCall);
			}
		}
		this.mCacheBuffer.Clear();
	}

	// Token: 0x06004DF8 RID: 19960 RVA: 0x001372B0 File Offset: 0x001354B0
	private void Delete(ref global::UIDrawCall.Iterator iter)
	{
		if (iter.Has)
		{
			global::UIDrawCall current = iter.Current;
			if (object.ReferenceEquals(current, this.mDrawCalls))
			{
				this.mDrawCalls = iter.Next.Current;
			}
			iter = iter.Next;
			current.LinkedList__Remove();
			this.mDrawCallCount--;
			global::NGUITools.DestroyImmediate(current.gameObject);
		}
	}

	// Token: 0x06004DF9 RID: 19961 RVA: 0x00137320 File Offset: 0x00135520
	private void PanelUpdate(bool letFill)
	{
		this.UpdateTransformMatrix();
		this.UpdateTransforms();
		if (this.mLayer != base.gameObject.layer)
		{
			this.mLayer = base.gameObject.layer;
			global::UICamera uicamera = global::UICamera.FindCameraForLayer(this.mLayer);
			this.mCam = ((!(uicamera != null)) ? global::NGUITools.FindCameraForLayer(this.mLayer) : uicamera.cachedCamera);
			global::UIPanel.SetChildLayer(this.cachedTransform, this.mLayer);
			global::UIDrawCall.Iterator iterator = (global::UIDrawCall.Iterator)this.mDrawCalls;
			while (iterator.Has)
			{
				iterator.Current.gameObject.layer = this.mLayer;
				iterator = iterator.Next;
			}
		}
		this.UpdateWidgets();
		if (this.mDepthChanged)
		{
			this.mDepthChanged = false;
			this.mWidgets.Sort(new Comparison<global::UIWidget>(global::UIWidget.CompareFunc));
		}
		if (letFill)
		{
			this.FillUpdate();
		}
		else
		{
			this.UpdateDrawcalls();
		}
		this.mRebuildAll = false;
	}

	// Token: 0x06004DFA RID: 19962 RVA: 0x00137430 File Offset: 0x00135630
	public bool Contains(global::UIDrawCall drawcall)
	{
		global::UIDrawCall.Iterator iterator = (global::UIDrawCall.Iterator)this.mDrawCalls;
		while (iterator.Has)
		{
			if (object.ReferenceEquals(drawcall, iterator.Current))
			{
				return true;
			}
			iterator = iterator.Next;
		}
		return false;
	}

	// Token: 0x06004DFB RID: 19963 RVA: 0x00137478 File Offset: 0x00135678
	private void FillUpdate()
	{
		foreach (global::UIMaterial mat in this.mChanged)
		{
			this.Fill(mat);
		}
		this.UpdateDrawcalls();
		this.mChanged.Clear();
	}

	// Token: 0x06004DFC RID: 19964 RVA: 0x001374F0 File Offset: 0x001356F0
	private void DefaultLateUpdate()
	{
		if (!this.manualPanelUpdate)
		{
			this.PanelUpdate(true);
		}
		else
		{
			this.FillUpdate();
		}
	}

	// Token: 0x06004DFD RID: 19965 RVA: 0x00137510 File Offset: 0x00135710
	public bool ManualPanelUpdate()
	{
		if (this.manualPanelUpdate && base.enabled)
		{
			this.PanelUpdate(false);
			return true;
		}
		return false;
	}

	// Token: 0x06004DFE RID: 19966 RVA: 0x00137540 File Offset: 0x00135740
	public void Refresh()
	{
		base.BroadcastMessage("Update", 1);
		this.DefaultLateUpdate();
	}

	// Token: 0x06004DFF RID: 19967 RVA: 0x00137554 File Offset: 0x00135754
	private static bool CheckRayEnterClippingRect(Ray ray, Transform transform, Vector4 clipRange)
	{
		Plane plane;
		plane..ctor(transform.forward, transform.position);
		float num;
		if (plane.Raycast(ray, ref num))
		{
			Vector3 vector = transform.InverseTransformPoint(ray.GetPoint(num));
			clipRange.z = Mathf.Abs(clipRange.z);
			clipRange.w = Mathf.Abs(clipRange.w);
			Rect rect;
			rect..ctor(clipRange.x - clipRange.z / 2f, clipRange.y - clipRange.w / 2f, clipRange.z, clipRange.w);
			return rect.Contains(vector);
		}
		return false;
	}

	// Token: 0x06004E00 RID: 19968 RVA: 0x00137604 File Offset: 0x00135804
	internal bool InsideClippingRect(Ray ray, int traceID)
	{
		if (this.clipping == global::UIDrawCall.Clipping.None)
		{
			return true;
		}
		if (traceID != this.traceID || ray.origin != this.lastRayTrace.origin || ray.direction != this.lastRayTrace.direction)
		{
			this.traceID = traceID;
			this.lastRayTrace = ray;
			this.lastRayTraceInside = global::UIPanel.CheckRayEnterClippingRect(ray, base.transform, this.clipRange);
		}
		return this.lastRayTraceInside;
	}

	// Token: 0x06004E01 RID: 19969 RVA: 0x00137690 File Offset: 0x00135890
	public Vector3 CalculateConstrainOffset(Vector2 min, Vector2 max)
	{
		float num = this.clipRange.z * 0.5f;
		float num2 = this.clipRange.w * 0.5f;
		Vector2 minRect;
		minRect..ctor(min.x, min.y);
		Vector2 maxRect;
		maxRect..ctor(max.x, max.y);
		Vector2 minArea;
		minArea..ctor(this.clipRange.x - num, this.clipRange.y - num2);
		Vector2 maxArea;
		maxArea..ctor(this.clipRange.x + num, this.clipRange.y + num2);
		if (this.clipping == global::UIDrawCall.Clipping.SoftClip)
		{
			minArea.x += this.clipSoftness.x;
			minArea.y += this.clipSoftness.y;
			maxArea.x -= this.clipSoftness.x;
			maxArea.y -= this.clipSoftness.y;
		}
		return global::NGUIMath.ConstrainRect(minRect, maxRect, minArea, maxArea);
	}

	// Token: 0x06004E02 RID: 19970 RVA: 0x001377D8 File Offset: 0x001359D8
	public bool ConstrainTargetToBounds(Transform target, ref global::AABBox targetBounds, bool immediate)
	{
		Vector3 vector = this.CalculateConstrainOffset(targetBounds.min, targetBounds.max);
		if (vector.magnitude > 0f)
		{
			if (immediate)
			{
				target.localPosition += vector;
				targetBounds.center += vector;
				global::SpringPosition component = target.GetComponent<global::SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
			else
			{
				global::SpringPosition springPosition = global::SpringPosition.Begin(target.gameObject, target.localPosition + vector, 13f);
				springPosition.ignoreTimeScale = true;
				springPosition.worldSpace = false;
			}
			return true;
		}
		return false;
	}

	// Token: 0x06004E03 RID: 19971 RVA: 0x0013788C File Offset: 0x00135A8C
	public bool ConstrainTargetToBounds(Transform target, bool immediate)
	{
		global::AABBox aabbox = global::NGUIMath.CalculateRelativeWidgetBounds(this.cachedTransform, target);
		return this.ConstrainTargetToBounds(target, ref aabbox, immediate);
	}

	// Token: 0x06004E04 RID: 19972 RVA: 0x001378B0 File Offset: 0x00135AB0
	private static void SetChildLayer(Transform t, int layer)
	{
		for (int i = 0; i < t.childCount; i++)
		{
			Transform child = t.GetChild(i);
			if (child.GetComponent<global::UIPanel>() == null)
			{
				child.gameObject.layer = layer;
				global::UIPanel.SetChildLayer(child, layer);
			}
		}
	}

	// Token: 0x06004E05 RID: 19973 RVA: 0x00137900 File Offset: 0x00135B00
	public static global::UIPanel Find(Transform trans, bool createIfMissing)
	{
		Transform transform = trans;
		global::UIPanel uipanel = null;
		while (uipanel == null && trans != null)
		{
			uipanel = trans.GetComponent<global::UIPanel>();
			if (uipanel != null)
			{
				break;
			}
			if (trans.parent == null)
			{
				break;
			}
			trans = trans.parent;
		}
		if (createIfMissing && uipanel == null && trans != transform)
		{
			uipanel = trans.gameObject.AddComponent<global::UIPanel>();
			global::UIPanel.SetChildLayer(uipanel.cachedTransform, uipanel.gameObject.layer);
		}
		return uipanel;
	}

	// Token: 0x06004E06 RID: 19974 RVA: 0x001379A8 File Offset: 0x00135BA8
	public static global::UIPanel Find(Transform trans)
	{
		return global::UIPanel.Find(trans, true);
	}

	// Token: 0x06004E07 RID: 19975 RVA: 0x001379B4 File Offset: 0x00135BB4
	public static global::UIPanel FindRoot(Transform trans)
	{
		global::UIPanel uipanel = global::UIPanel.Find(trans);
		return (!uipanel) ? null : uipanel.RootPanel;
	}

	// Token: 0x06004E08 RID: 19976 RVA: 0x001379E0 File Offset: 0x00135BE0
	internal static void RegisterHotSpot(global::UIPanel panel, global::UIHotSpot hotSpot)
	{
		if (panel.mHotSpots == null)
		{
			panel.mHotSpots = new HashSet<global::UIHotSpot>();
		}
		if (panel.mHotSpots.Add(hotSpot))
		{
			if (panel.enabled)
			{
				hotSpot.OnPanelEnable();
			}
			else
			{
				hotSpot.OnPanelDisable();
			}
		}
	}

	// Token: 0x06004E09 RID: 19977 RVA: 0x00137A30 File Offset: 0x00135C30
	internal static void UnregisterHotSpot(global::UIPanel panel, global::UIHotSpot hotSpot)
	{
		if (panel.mHotSpots == null || !panel.mHotSpots.Remove(hotSpot))
		{
			return;
		}
		if (panel.enabled)
		{
			hotSpot.OnPanelDisable();
		}
	}

	// Token: 0x17000EEB RID: 3819
	// (get) Token: 0x06004E0A RID: 19978 RVA: 0x00137A6C File Offset: 0x00135C6C
	// (set) Token: 0x06004E0B RID: 19979 RVA: 0x00137A74 File Offset: 0x00135C74
	public global::UIPanelMaterialPropertyBlock propertyBlock
	{
		get
		{
			return this._propertyBlock;
		}
		set
		{
			this._propertyBlock = value;
		}
	}

	// Token: 0x04002B9E RID: 11166
	[SerializeField]
	private global::UIPanel _rootPanel;

	// Token: 0x04002B9F RID: 11167
	public bool showInPanelTool = true;

	// Token: 0x04002BA0 RID: 11168
	public bool generateNormals;

	// Token: 0x04002BA1 RID: 11169
	public bool depthPass;

	// Token: 0x04002BA2 RID: 11170
	public bool widgetsAreStatic;

	// Token: 0x04002BA3 RID: 11171
	[HideInInspector]
	[SerializeField]
	private global::UIPanel.DebugInfo mDebugInfo = global::UIPanel.DebugInfo.Gizmos;

	// Token: 0x04002BA4 RID: 11172
	[HideInInspector]
	[SerializeField]
	private global::UIDrawCall.Clipping mClipping;

	// Token: 0x04002BA5 RID: 11173
	[SerializeField]
	[HideInInspector]
	private Vector4 mClipRange = Vector4.zero;

	// Token: 0x04002BA6 RID: 11174
	[HideInInspector]
	[SerializeField]
	private Vector2 mClipSoftness = new Vector2(40f, 40f);

	// Token: 0x04002BA7 RID: 11175
	[SerializeField]
	[HideInInspector]
	private bool manualPanelUpdate;

	// Token: 0x04002BA8 RID: 11176
	private OrderedDictionary mChildren = new OrderedDictionary();

	// Token: 0x04002BA9 RID: 11177
	private List<global::UIWidget> mWidgets = new List<global::UIWidget>();

	// Token: 0x04002BAA RID: 11178
	private HashSet<global::UIMaterial> mChanged = new HashSet<global::UIMaterial>();

	// Token: 0x04002BAB RID: 11179
	private global::UIDrawCall mDrawCalls;

	// Token: 0x04002BAC RID: 11180
	private int mDrawCallCount;

	// Token: 0x04002BAD RID: 11181
	private NGUI.Meshing.MeshBuffer mCacheBuffer = new NGUI.Meshing.MeshBuffer();

	// Token: 0x04002BAE RID: 11182
	private HashSet<global::UIHotSpot> mHotSpots;

	// Token: 0x04002BAF RID: 11183
	private Transform mTrans;

	// Token: 0x04002BB0 RID: 11184
	private Camera mCam;

	// Token: 0x04002BB1 RID: 11185
	private int mLayer = -1;

	// Token: 0x04002BB2 RID: 11186
	private bool mDepthChanged;

	// Token: 0x04002BB3 RID: 11187
	private bool mRebuildAll;

	// Token: 0x04002BB4 RID: 11188
	private bool mChangedLastFrame;

	// Token: 0x04002BB5 RID: 11189
	private float mMatrixTime;

	// Token: 0x04002BB6 RID: 11190
	private Matrix4x4 mWorldToLocal = Matrix4x4.identity;

	// Token: 0x04002BB7 RID: 11191
	private static float[] mTemp = new float[4];

	// Token: 0x04002BB8 RID: 11192
	private Vector2 mMin = Vector2.zero;

	// Token: 0x04002BB9 RID: 11193
	private Vector2 mMax = Vector2.zero;

	// Token: 0x04002BBA RID: 11194
	private List<Transform> mRemoved = new List<Transform>();

	// Token: 0x04002BBB RID: 11195
	private bool mCheckVisibility;

	// Token: 0x04002BBC RID: 11196
	private float mCullTime;

	// Token: 0x04002BBD RID: 11197
	private bool mCulled;

	// Token: 0x04002BBE RID: 11198
	private int globalIndex = -1;

	// Token: 0x04002BBF RID: 11199
	private static List<global::UINode> mHierarchy = new List<global::UINode>();

	// Token: 0x04002BC0 RID: 11200
	private int traceID;

	// Token: 0x04002BC1 RID: 11201
	private Ray lastRayTrace;

	// Token: 0x04002BC2 RID: 11202
	private bool lastRayTraceInside;

	// Token: 0x04002BC3 RID: 11203
	[NonSerialized]
	private global::UIPanelMaterialPropertyBlock _propertyBlock;

	// Token: 0x020008EB RID: 2283
	private static class Global
	{
		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06004E0C RID: 19980 RVA: 0x00137A80 File Offset: 0x00135C80
		public static bool noGlobal
		{
			get
			{
				return !Application.isPlaying;
			}
		}

		// Token: 0x06004E0D RID: 19981 RVA: 0x00137A8C File Offset: 0x00135C8C
		public static void RegisterPanel(global::UIPanel panel)
		{
			if (global::UIPanel.Global.noGlobal)
			{
				return;
			}
			global::UIGlobal.EnsureGlobal();
			if (panel.globalIndex == -1)
			{
				panel.globalIndex = global::UIPanel.Global.g.allPanels.Count;
				global::UIPanel.Global.g.allPanels.Add(panel);
			}
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x00137AC8 File Offset: 0x00135CC8
		public static void UnregisterPanel(global::UIPanel panel)
		{
			if (global::UIPanel.Global.noGlobal)
			{
				return;
			}
			if (panel.globalIndex != -1)
			{
				global::UIPanel.Global.g.allPanels.RemoveAt(panel.globalIndex);
				int i = panel.globalIndex;
				int count = global::UIPanel.Global.g.allPanels.Count;
				while (i < count)
				{
					global::UIPanel.Global.g.allPanels[i].globalIndex = i;
					i++;
				}
				panel.globalIndex = -1;
			}
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x00137B38 File Offset: 0x00135D38
		public static void PanelEnabled(global::UIPanel panel)
		{
			if (global::UIPanel.Global.noGlobal)
			{
				return;
			}
			global::UIPanel.Global.g.allEnabled.Add(panel);
		}

		// Token: 0x06004E10 RID: 19984 RVA: 0x00137B54 File Offset: 0x00135D54
		public static void PanelDisabled(global::UIPanel panel)
		{
			if (global::UIPanel.Global.noGlobal)
			{
				return;
			}
			global::UIPanel.Global.g.allEnabled.Remove(panel);
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x00137B70 File Offset: 0x00135D70
		public static void PanelUpdate()
		{
			if (global::UIPanel.Global.noGlobal)
			{
				return;
			}
			try
			{
				global::UIPanel.Global.g.allEnableSwap.AddRange(global::UIPanel.Global.g.allEnabled);
				foreach (global::UIPanel uipanel in global::UIPanel.Global.g.allEnableSwap)
				{
					if (uipanel && uipanel.enabled)
					{
						uipanel.DefaultLateUpdate();
					}
				}
			}
			finally
			{
				global::UIPanel.Global.g.allEnableSwap.Clear();
			}
		}

		// Token: 0x020008EC RID: 2284
		private static class g
		{
			// Token: 0x06004E12 RID: 19986 RVA: 0x00137C2C File Offset: 0x00135E2C
			static g()
			{
				global::UIGlobal.EnsureGlobal();
			}

			// Token: 0x04002BC4 RID: 11204
			public static HashSet<global::UIPanel> allEnabled = new HashSet<global::UIPanel>();

			// Token: 0x04002BC5 RID: 11205
			public static List<global::UIPanel> allEnableSwap = new List<global::UIPanel>();

			// Token: 0x04002BC6 RID: 11206
			public static List<global::UIPanel> allPanels = new List<global::UIPanel>();
		}
	}

	// Token: 0x020008ED RID: 2285
	public enum DebugInfo
	{
		// Token: 0x04002BC8 RID: 11208
		None,
		// Token: 0x04002BC9 RID: 11209
		Gizmos,
		// Token: 0x04002BCA RID: 11210
		Geometry
	}
}
