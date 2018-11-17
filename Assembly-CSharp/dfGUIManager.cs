using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020006CC RID: 1740
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(dfInputManager))]
[RequireComponent(typeof(MeshRenderer))]
[AddComponentMenu("Daikon Forge/User Interface/GUI Manager")]
[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
[Serializable]
public class dfGUIManager : MonoBehaviour
{
	// Token: 0x14000049 RID: 73
	// (add) Token: 0x06003D2C RID: 15660 RVA: 0x000E61D4 File Offset: 0x000E43D4
	// (remove) Token: 0x06003D2D RID: 15661 RVA: 0x000E61EC File Offset: 0x000E43EC
	public static event dfGUIManager.RenderCallback BeforeRender;

	// Token: 0x1400004A RID: 74
	// (add) Token: 0x06003D2E RID: 15662 RVA: 0x000E6204 File Offset: 0x000E4404
	// (remove) Token: 0x06003D2F RID: 15663 RVA: 0x000E621C File Offset: 0x000E441C
	public static event dfGUIManager.RenderCallback AfterRender;

	// Token: 0x17000BF5 RID: 3061
	// (get) Token: 0x06003D30 RID: 15664 RVA: 0x000E6234 File Offset: 0x000E4434
	// (set) Token: 0x06003D31 RID: 15665 RVA: 0x000E623C File Offset: 0x000E443C
	public int TotalDrawCalls { get; private set; }

	// Token: 0x17000BF6 RID: 3062
	// (get) Token: 0x06003D32 RID: 15666 RVA: 0x000E6248 File Offset: 0x000E4448
	// (set) Token: 0x06003D33 RID: 15667 RVA: 0x000E6250 File Offset: 0x000E4450
	public int TotalTriangles { get; private set; }

	// Token: 0x17000BF7 RID: 3063
	// (get) Token: 0x06003D34 RID: 15668 RVA: 0x000E625C File Offset: 0x000E445C
	// (set) Token: 0x06003D35 RID: 15669 RVA: 0x000E6264 File Offset: 0x000E4464
	public int ControlsRendered { get; private set; }

	// Token: 0x17000BF8 RID: 3064
	// (get) Token: 0x06003D36 RID: 15670 RVA: 0x000E6270 File Offset: 0x000E4470
	// (set) Token: 0x06003D37 RID: 15671 RVA: 0x000E6278 File Offset: 0x000E4478
	public int FramesRendered { get; private set; }

	// Token: 0x17000BF9 RID: 3065
	// (get) Token: 0x06003D38 RID: 15672 RVA: 0x000E6284 File Offset: 0x000E4484
	public static dfControl ActiveControl
	{
		get
		{
			return dfGUIManager.activeControl;
		}
	}

	// Token: 0x17000BFA RID: 3066
	// (get) Token: 0x06003D39 RID: 15673 RVA: 0x000E628C File Offset: 0x000E448C
	// (set) Token: 0x06003D3A RID: 15674 RVA: 0x000E6294 File Offset: 0x000E4494
	public float UIScale
	{
		get
		{
			return this.uiScale;
		}
		set
		{
			if (!Mathf.Approximately(value, this.uiScale))
			{
				this.uiScale = value;
				this.onResolutionChanged();
			}
		}
	}

	// Token: 0x17000BFB RID: 3067
	// (get) Token: 0x06003D3B RID: 15675 RVA: 0x000E62B4 File Offset: 0x000E44B4
	// (set) Token: 0x06003D3C RID: 15676 RVA: 0x000E62BC File Offset: 0x000E44BC
	public Vector2 UIOffset
	{
		get
		{
			return this.uiOffset;
		}
		set
		{
			if (!object.Equals(this.uiOffset, value))
			{
				this.uiOffset = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000BFC RID: 3068
	// (get) Token: 0x06003D3D RID: 15677 RVA: 0x000E62F4 File Offset: 0x000E44F4
	// (set) Token: 0x06003D3E RID: 15678 RVA: 0x000E62FC File Offset: 0x000E44FC
	public Camera RenderCamera
	{
		get
		{
			return this.renderCamera;
		}
		set
		{
			if (!object.ReferenceEquals(this.renderCamera, value))
			{
				this.renderCamera = value;
				this.Invalidate();
				if (value != null && value.gameObject.GetComponent<dfGUICamera>() == null)
				{
					value.gameObject.AddComponent<dfGUICamera>();
				}
				if (this.inputManager != null)
				{
					this.inputManager.RenderCamera = value;
				}
			}
		}
	}

	// Token: 0x17000BFD RID: 3069
	// (get) Token: 0x06003D3F RID: 15679 RVA: 0x000E6374 File Offset: 0x000E4574
	// (set) Token: 0x06003D40 RID: 15680 RVA: 0x000E637C File Offset: 0x000E457C
	public bool MergeMaterials
	{
		get
		{
			return this.mergeMaterials;
		}
		set
		{
			if (value != this.mergeMaterials)
			{
				this.mergeMaterials = value;
				this.invalidateAllControls();
			}
		}
	}

	// Token: 0x17000BFE RID: 3070
	// (get) Token: 0x06003D41 RID: 15681 RVA: 0x000E6398 File Offset: 0x000E4598
	// (set) Token: 0x06003D42 RID: 15682 RVA: 0x000E63A0 File Offset: 0x000E45A0
	public bool GenerateNormals
	{
		get
		{
			return this.generateNormals;
		}
		set
		{
			if (value != this.generateNormals)
			{
				this.generateNormals = value;
				if (this.renderMesh != null)
				{
					this.renderMesh[0].Clear();
					this.renderMesh[1].Clear();
				}
				dfRenderData.FlushObjectPool();
				this.invalidateAllControls();
			}
		}
	}

	// Token: 0x17000BFF RID: 3071
	// (get) Token: 0x06003D43 RID: 15683 RVA: 0x000E63F0 File Offset: 0x000E45F0
	// (set) Token: 0x06003D44 RID: 15684 RVA: 0x000E63F8 File Offset: 0x000E45F8
	public bool PixelPerfectMode
	{
		get
		{
			return this.pixelPerfectMode;
		}
		set
		{
			if (value != this.pixelPerfectMode)
			{
				this.pixelPerfectMode = value;
				this.onResolutionChanged();
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C00 RID: 3072
	// (get) Token: 0x06003D45 RID: 15685 RVA: 0x000E641C File Offset: 0x000E461C
	// (set) Token: 0x06003D46 RID: 15686 RVA: 0x000E6424 File Offset: 0x000E4624
	public dfAtlas DefaultAtlas
	{
		get
		{
			return this.atlas;
		}
		set
		{
			if (!dfAtlas.Equals(value, this.atlas))
			{
				this.atlas = value;
				this.invalidateAllControls();
			}
		}
	}

	// Token: 0x17000C01 RID: 3073
	// (get) Token: 0x06003D47 RID: 15687 RVA: 0x000E6444 File Offset: 0x000E4644
	// (set) Token: 0x06003D48 RID: 15688 RVA: 0x000E644C File Offset: 0x000E464C
	public dfFont DefaultFont
	{
		get
		{
			return this.defaultFont;
		}
		set
		{
			if (value != this.defaultFont)
			{
				this.defaultFont = value;
				this.invalidateAllControls();
			}
		}
	}

	// Token: 0x17000C02 RID: 3074
	// (get) Token: 0x06003D49 RID: 15689 RVA: 0x000E646C File Offset: 0x000E466C
	// (set) Token: 0x06003D4A RID: 15690 RVA: 0x000E6474 File Offset: 0x000E4674
	public int FixedWidth
	{
		get
		{
			return this.fixedWidth;
		}
		set
		{
			if (value != this.fixedWidth)
			{
				this.fixedWidth = value;
				this.onResolutionChanged();
			}
		}
	}

	// Token: 0x17000C03 RID: 3075
	// (get) Token: 0x06003D4B RID: 15691 RVA: 0x000E6490 File Offset: 0x000E4690
	// (set) Token: 0x06003D4C RID: 15692 RVA: 0x000E6498 File Offset: 0x000E4698
	public int FixedHeight
	{
		get
		{
			return this.fixedHeight;
		}
		set
		{
			if (value != this.fixedHeight)
			{
				int oldSize = this.fixedHeight;
				this.fixedHeight = value;
				this.onResolutionChanged(oldSize, value);
			}
		}
	}

	// Token: 0x17000C04 RID: 3076
	// (get) Token: 0x06003D4D RID: 15693 RVA: 0x000E64C8 File Offset: 0x000E46C8
	// (set) Token: 0x06003D4E RID: 15694 RVA: 0x000E64D0 File Offset: 0x000E46D0
	public bool ConsumeMouseEvents
	{
		get
		{
			return this.consumeMouseEvents;
		}
		set
		{
			this.consumeMouseEvents = value;
		}
	}

	// Token: 0x17000C05 RID: 3077
	// (get) Token: 0x06003D4F RID: 15695 RVA: 0x000E64DC File Offset: 0x000E46DC
	// (set) Token: 0x06003D50 RID: 15696 RVA: 0x000E64E4 File Offset: 0x000E46E4
	public bool OverrideCamera
	{
		get
		{
			return this.overrideCamera;
		}
		set
		{
			this.overrideCamera = value;
		}
	}

	// Token: 0x06003D51 RID: 15697 RVA: 0x000E64F0 File Offset: 0x000E46F0
	public void OnGUI()
	{
		if (this.overrideCamera || !this.consumeMouseEvents || !Application.isPlaying || this.occluders == null)
		{
			return;
		}
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.y = (float)Screen.height - mousePosition.y;
		if (dfGUIManager.modalControlStack.Count > 0)
		{
			GUI.Box(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), GUIContent.none, GUIStyle.none);
		}
		for (int i = 0; i < this.occluders.Count; i++)
		{
			if (this.occluders[i].Contains(mousePosition))
			{
				GUI.Box(this.occluders[i], GUIContent.none, GUIStyle.none);
				break;
			}
		}
		if (Event.current.isMouse && Input.touchCount > 0)
		{
			int touchCount = Input.touchCount;
			for (int j = 0; j < touchCount; j++)
			{
				Touch touch = Input.GetTouch(j);
				if (touch.phase == null)
				{
					Vector2 touchPosition = touch.position;
					touchPosition.y = (float)Screen.height - touchPosition.y;
					if (this.occluders.Any((Rect x) => x.Contains(touchPosition)))
					{
						Event.current.Use();
						break;
					}
				}
			}
		}
	}

	// Token: 0x06003D52 RID: 15698 RVA: 0x000E667C File Offset: 0x000E487C
	public virtual void OnDestroy()
	{
		if (this.meshRenderer != null)
		{
			this.renderFilter.sharedMesh = null;
			Object.DestroyImmediate(this.renderMesh[0]);
			Object.DestroyImmediate(this.renderMesh[1]);
			this.renderMesh = null;
		}
	}

	// Token: 0x06003D53 RID: 15699 RVA: 0x000E66C8 File Offset: 0x000E48C8
	public virtual void Awake()
	{
		dfRenderData.FlushObjectPool();
	}

	// Token: 0x06003D54 RID: 15700 RVA: 0x000E66D0 File Offset: 0x000E48D0
	public virtual void OnEnable()
	{
		this.FramesRendered = 0;
		this.TotalDrawCalls = 0;
		this.TotalTriangles = 0;
		if (this.meshRenderer != null)
		{
			this.meshRenderer.enabled = true;
		}
		if (Application.isPlaying)
		{
			this.onResolutionChanged();
		}
	}

	// Token: 0x06003D55 RID: 15701 RVA: 0x000E6720 File Offset: 0x000E4920
	public virtual void OnDisable()
	{
		if (this.meshRenderer != null)
		{
			this.meshRenderer.enabled = false;
		}
	}

	// Token: 0x06003D56 RID: 15702 RVA: 0x000E6740 File Offset: 0x000E4940
	public virtual void Start()
	{
		Camera[] array = Object.FindObjectsOfType(typeof(Camera)) as Camera[];
		for (int i = 0; i < array.Length; i++)
		{
			array[i].eventMask &= ~(1 << base.gameObject.layer);
		}
		this.inputManager = (base.GetComponent<dfInputManager>() ?? base.gameObject.AddComponent<dfInputManager>());
		this.inputManager.RenderCamera = this.RenderCamera;
		this.FramesRendered = 0;
		this.invalidateAllControls();
		this.updateRenderOrder(null);
		if (this.meshRenderer != null)
		{
			this.meshRenderer.enabled = true;
		}
	}

	// Token: 0x06003D57 RID: 15703 RVA: 0x000E67FC File Offset: 0x000E49FC
	public virtual void Update()
	{
		if (this.renderCamera == null || !base.enabled)
		{
			if (this.meshRenderer != null)
			{
				this.meshRenderer.enabled = false;
			}
			return;
		}
		if (this.renderMesh == null || this.renderMesh.Length == 0)
		{
			this.initialize();
			if (Application.isEditor && !Application.isPlaying)
			{
				this.Render();
			}
		}
		if (this.cachedChildCount != base.transform.childCount)
		{
			this.cachedChildCount = base.transform.childCount;
			this.Invalidate();
		}
		Vector2 screenSize = this.GetScreenSize();
		if ((screenSize - this.cachedScreenSize).sqrMagnitude > 1.401298E-45f)
		{
			this.onResolutionChanged(this.cachedScreenSize, screenSize);
			this.cachedScreenSize = screenSize;
		}
	}

	// Token: 0x06003D58 RID: 15704 RVA: 0x000E68E4 File Offset: 0x000E4AE4
	public virtual void LateUpdate()
	{
		if (this.renderMesh == null || this.renderMesh.Length == 0)
		{
			this.initialize();
		}
		if (!Application.isPlaying)
		{
			BoxCollider boxCollider = base.collider as BoxCollider;
			if (boxCollider != null)
			{
				Vector2 vector = this.GetScreenSize() * this.PixelsToUnits();
				boxCollider.center = Vector3.zero;
				boxCollider.size = vector;
			}
		}
		if (this.isDirty)
		{
			this.isDirty = false;
			this.Render();
		}
	}

	// Token: 0x06003D59 RID: 15705 RVA: 0x000E6974 File Offset: 0x000E4B74
	public dfControl HitTest(Vector2 screenPosition)
	{
		Ray ray = this.renderCamera.ScreenPointToRay(screenPosition);
		float num = this.renderCamera.farClipPlane - this.renderCamera.nearClipPlane;
		RaycastHit[] array = Physics.RaycastAll(ray, num, this.renderCamera.cullingMask);
		Array.Sort<RaycastHit>(array, new Comparison<RaycastHit>(dfInputManager.raycastHitSorter));
		return this.inputManager.clipCast(array);
	}

	// Token: 0x06003D5A RID: 15706 RVA: 0x000E69DC File Offset: 0x000E4BDC
	public Vector2 WorldPointToGUI(Vector3 worldPoint)
	{
		Vector2 screenSize = this.GetScreenSize();
		Camera main = Camera.main;
		Vector3 vector = Camera.main.WorldToScreenPoint(worldPoint);
		vector.x = screenSize.x * (vector.x / main.pixelWidth);
		vector.y = screenSize.y * (vector.y / main.pixelHeight);
		return this.ScreenToGui(vector);
	}

	// Token: 0x06003D5B RID: 15707 RVA: 0x000E6A48 File Offset: 0x000E4C48
	public float PixelsToUnits()
	{
		float num = 2f / (float)this.FixedHeight;
		return num * this.UIScale;
	}

	// Token: 0x06003D5C RID: 15708 RVA: 0x000E6A6C File Offset: 0x000E4C6C
	public virtual Plane[] GetClippingPlanes()
	{
		Vector3[] array = this.GetCorners();
		Vector3 vector = base.transform.TransformDirection(Vector3.right);
		Vector3 vector2 = base.transform.TransformDirection(Vector3.left);
		Vector3 vector3 = base.transform.TransformDirection(Vector3.up);
		Vector3 vector4 = base.transform.TransformDirection(Vector3.down);
		return new Plane[]
		{
			new Plane(vector, array[0]),
			new Plane(vector2, array[1]),
			new Plane(vector3, array[2]),
			new Plane(vector4, array[0])
		};
	}

	// Token: 0x06003D5D RID: 15709 RVA: 0x000E6B44 File Offset: 0x000E4D44
	public Vector3[] GetCorners()
	{
		float num = this.PixelsToUnits();
		Vector2 vector = this.GetScreenSize() * num;
		float x = vector.x;
		float y = vector.y;
		Vector3 vector2;
		vector2..ctor(-x * 0.5f, y * 0.5f);
		Vector3 vector3 = vector2 + new Vector3(x, 0f);
		Vector3 vector4 = vector2 + new Vector3(0f, -y);
		Vector3 vector5 = vector3 + new Vector3(0f, -y);
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		this.corners[0] = localToWorldMatrix.MultiplyPoint(vector2);
		this.corners[1] = localToWorldMatrix.MultiplyPoint(vector3);
		this.corners[2] = localToWorldMatrix.MultiplyPoint(vector5);
		this.corners[3] = localToWorldMatrix.MultiplyPoint(vector4);
		return this.corners;
	}

	// Token: 0x06003D5E RID: 15710 RVA: 0x000E6C44 File Offset: 0x000E4E44
	public Vector2 GetScreenSize()
	{
		Camera camera = this.RenderCamera;
		bool flag = Application.isPlaying && camera != null;
		if (flag)
		{
			float num = (!this.PixelPerfectMode) ? (camera.pixelHeight / (float)this.fixedHeight * this.uiScale) : 1f;
			return (new Vector2(camera.pixelWidth, camera.pixelHeight) / num).CeilToInt();
		}
		return new Vector2((float)this.FixedWidth, (float)this.FixedHeight);
	}

	// Token: 0x06003D5F RID: 15711 RVA: 0x000E6CD0 File Offset: 0x000E4ED0
	public T AddControl<T>() where T : dfControl
	{
		return (T)((object)this.AddControl(typeof(T)));
	}

	// Token: 0x06003D60 RID: 15712 RVA: 0x000E6CE8 File Offset: 0x000E4EE8
	public dfControl AddControl(Type type)
	{
		if (!typeof(dfControl).IsAssignableFrom(type))
		{
			throw new InvalidCastException();
		}
		dfControl dfControl = new GameObject(type.Name, new Type[]
		{
			type
		})
		{
			transform = 
			{
				parent = base.transform
			},
			layer = base.gameObject.layer
		}.GetComponent(type) as dfControl;
		dfControl.ZOrder = this.getMaxZOrder() + 1;
		return dfControl;
	}

	// Token: 0x06003D61 RID: 15713 RVA: 0x000E6D64 File Offset: 0x000E4F64
	private int getMaxZOrder()
	{
		int num = -1;
		using (dfList<dfControl> topLevelControls = this.getTopLevelControls())
		{
			for (int i = 0; i < topLevelControls.Count; i++)
			{
				num = Mathf.Max(num, topLevelControls[i].ZOrder);
			}
		}
		return num;
	}

	// Token: 0x06003D62 RID: 15714 RVA: 0x000E6DD4 File Offset: 0x000E4FD4
	public dfRenderData GetDrawCallBuffer(int drawCallNumber)
	{
		return this.drawCallBuffers[drawCallNumber];
	}

	// Token: 0x06003D63 RID: 15715 RVA: 0x000E6DE4 File Offset: 0x000E4FE4
	public static dfControl GetModalControl()
	{
		return (dfGUIManager.modalControlStack.Count <= 0) ? null : dfGUIManager.modalControlStack.Peek().control;
	}

	// Token: 0x06003D64 RID: 15716 RVA: 0x000E6E1C File Offset: 0x000E501C
	public Vector2 ScreenToGui(Vector2 position)
	{
		position.y = this.GetScreenSize().y - position.y;
		return position;
	}

	// Token: 0x06003D65 RID: 15717 RVA: 0x000E6E48 File Offset: 0x000E5048
	public static void PushModal(dfControl control, dfGUIManager.ModalPoppedCallback callback = null)
	{
		if (control == null)
		{
			throw new NullReferenceException("Cannot call PushModal() with a null reference");
		}
		dfGUIManager.modalControlStack.Push(new dfGUIManager.ModalControlReference
		{
			control = control,
			callback = callback
		});
	}

	// Token: 0x06003D66 RID: 15718 RVA: 0x000E6E90 File Offset: 0x000E5090
	public static void PopModal()
	{
		if (dfGUIManager.modalControlStack.Count == 0)
		{
			throw new InvalidOperationException("Modal stack is empty");
		}
		dfGUIManager.ModalControlReference modalControlReference = dfGUIManager.modalControlStack.Pop();
		if (modalControlReference.callback != null)
		{
			modalControlReference.callback(modalControlReference.control);
		}
	}

	// Token: 0x06003D67 RID: 15719 RVA: 0x000E6EE4 File Offset: 0x000E50E4
	public static void SetFocus(dfControl control)
	{
		if (dfGUIManager.activeControl == control)
		{
			return;
		}
		dfControl dfControl = dfGUIManager.activeControl;
		dfGUIManager.activeControl = control;
		dfFocusEventArgs args = new dfFocusEventArgs(control, dfControl);
		dfList<dfControl> prevFocusChain = dfList<dfControl>.Obtain();
		if (dfControl != null)
		{
			dfControl dfControl2 = dfControl;
			while (dfControl2 != null)
			{
				prevFocusChain.Add(dfControl2);
				dfControl2 = dfControl2.Parent;
			}
		}
		dfList<dfControl> newFocusChain = dfList<dfControl>.Obtain();
		if (control != null)
		{
			dfControl dfControl3 = control;
			while (dfControl3 != null)
			{
				newFocusChain.Add(dfControl3);
				dfControl3 = dfControl3.Parent;
			}
		}
		if (dfControl != null)
		{
			prevFocusChain.ForEach(delegate(dfControl c)
			{
				if (!newFocusChain.Contains(c))
				{
					c.OnLeaveFocus(args);
				}
			});
			dfControl.OnLostFocus(args);
		}
		if (control != null)
		{
			newFocusChain.ForEach(delegate(dfControl c)
			{
				if (!prevFocusChain.Contains(c))
				{
					c.OnEnterFocus(args);
				}
			});
			control.OnGotFocus(args);
		}
		newFocusChain.Release();
		prevFocusChain.Release();
	}

	// Token: 0x06003D68 RID: 15720 RVA: 0x000E7010 File Offset: 0x000E5210
	public static bool HasFocus(dfControl control)
	{
		return !(control == null) && dfGUIManager.activeControl == control;
	}

	// Token: 0x06003D69 RID: 15721 RVA: 0x000E702C File Offset: 0x000E522C
	public static bool ContainsFocus(dfControl control)
	{
		return dfGUIManager.activeControl == control || (!(dfGUIManager.activeControl == null) && !(control == null) && dfGUIManager.activeControl.transform.IsChildOf(control.transform));
	}

	// Token: 0x06003D6A RID: 15722 RVA: 0x000E7080 File Offset: 0x000E5280
	public void BringToFront(dfControl control)
	{
		if (control.Parent != null)
		{
			control = control.GetRootContainer();
		}
		using (dfList<dfControl> topLevelControls = this.getTopLevelControls())
		{
			int zorder = 0;
			for (int i = 0; i < topLevelControls.Count; i++)
			{
				dfControl dfControl = topLevelControls[i];
				if (dfControl != control)
				{
					dfControl.ZOrder = zorder++;
				}
			}
			control.ZOrder = zorder;
			this.Invalidate();
		}
	}

	// Token: 0x06003D6B RID: 15723 RVA: 0x000E7120 File Offset: 0x000E5320
	public void SendToBack(dfControl control)
	{
		if (control.Parent != null)
		{
			control = control.GetRootContainer();
		}
		using (dfList<dfControl> topLevelControls = this.getTopLevelControls())
		{
			int num = 1;
			for (int i = 0; i < topLevelControls.Count; i++)
			{
				dfControl dfControl = topLevelControls[i];
				if (dfControl != control)
				{
					dfControl.ZOrder = num++;
				}
			}
			control.ZOrder = 0;
			this.Invalidate();
		}
	}

	// Token: 0x06003D6C RID: 15724 RVA: 0x000E71C0 File Offset: 0x000E53C0
	public void Invalidate()
	{
		if (this.isDirty)
		{
			return;
		}
		this.isDirty = true;
		this.updateRenderSettings();
	}

	// Token: 0x06003D6D RID: 15725 RVA: 0x000E71DC File Offset: 0x000E53DC
	public static void RefreshAll(bool force = false)
	{
		dfGUIManager[] array = Object.FindObjectsOfType(typeof(dfGUIManager)) as dfGUIManager[];
		for (int i = 0; i < array.Length; i++)
		{
			array[i].invalidateAllControls();
			if (force || !Application.isPlaying)
			{
				array[i].Render();
			}
		}
	}

	// Token: 0x06003D6E RID: 15726 RVA: 0x000E7234 File Offset: 0x000E5434
	public void Render()
	{
		this.FramesRendered++;
		if (dfGUIManager.BeforeRender != null)
		{
			dfGUIManager.BeforeRender(this);
		}
		try
		{
			this.updateRenderSettings();
			this.ControlsRendered = 0;
			this.occluders.Clear();
			this.TotalDrawCalls = 0;
			this.TotalTriangles = 0;
			if (this.RenderCamera == null || !base.enabled)
			{
				if (this.meshRenderer != null)
				{
					this.meshRenderer.enabled = false;
				}
			}
			else
			{
				if (this.meshRenderer != null && !this.meshRenderer.enabled)
				{
					this.meshRenderer.enabled = true;
				}
				if (this.renderMesh == null || this.renderMesh.Length == 0)
				{
					Debug.LogError("GUI Manager not initialized before Render() called");
				}
				else
				{
					this.resetDrawCalls();
					dfRenderData dfRenderData = null;
					this.clipStack.Clear();
					this.clipStack.Push(dfGUIManager.ClipRegion.Obtain());
					uint start_VALUE = dfChecksumUtil.START_VALUE;
					using (dfList<dfControl> topLevelControls = this.getTopLevelControls())
					{
						this.updateRenderOrder(topLevelControls);
						for (int i = 0; i < topLevelControls.Count; i++)
						{
							dfControl control = topLevelControls[i];
							this.renderControl(ref dfRenderData, control, start_VALUE, 1f);
						}
					}
					this.drawCallBuffers.RemoveAll((dfRenderData x) => x.Vertices.Count == 0);
					this.drawCallCount = this.drawCallBuffers.Count;
					this.TotalDrawCalls = this.drawCallCount;
					if (this.drawCallBuffers.Count == 0)
					{
						if (this.renderFilter.sharedMesh != null)
						{
							this.renderFilter.sharedMesh.Clear();
						}
					}
					else
					{
						this.meshRenderer.sharedMaterials = this.gatherMaterials();
						dfRenderData dfRenderData2 = this.compileMasterBuffer();
						this.TotalTriangles = dfRenderData2.Triangles.Count / 3;
						Mesh mesh = this.getRenderMesh();
						this.renderFilter.sharedMesh = mesh;
						Mesh mesh2 = mesh;
						mesh2.Clear();
						mesh2.vertices = dfRenderData2.Vertices.Items;
						mesh2.uv = dfRenderData2.UV.Items;
						mesh2.colors32 = dfRenderData2.Colors.Items;
						if (this.generateNormals && dfRenderData2.Normals.Items.Length == dfRenderData2.Vertices.Items.Length)
						{
							mesh2.normals = dfRenderData2.Normals.Items;
							mesh2.tangents = dfRenderData2.Tangents.Items;
						}
						mesh2.subMeshCount = this.submeshes.Count;
						for (int j = 0; j < this.submeshes.Count; j++)
						{
							int num = this.submeshes[j];
							int num2 = dfRenderData2.Triangles.Count - num;
							if (j < this.submeshes.Count - 1)
							{
								num2 = this.submeshes[j + 1] - num;
							}
							int[] array = new int[num2];
							dfRenderData2.Triangles.CopyTo(num, array, 0, num2);
							mesh2.SetTriangles(array, j);
						}
						if (this.clipStack.Count != 1)
						{
							Debug.LogError("Clip stack not properly maintained");
						}
						this.clipStack.Pop().Release();
						this.clipStack.Clear();
					}
				}
			}
		}
		catch (dfAbortRenderingException)
		{
			this.isDirty = true;
		}
		finally
		{
			if (dfGUIManager.AfterRender != null)
			{
				dfGUIManager.AfterRender(this);
			}
		}
	}

	// Token: 0x06003D6F RID: 15727 RVA: 0x000E7638 File Offset: 0x000E5838
	private dfList<dfControl> getTopLevelControls()
	{
		int childCount = base.transform.childCount;
		dfList<dfControl> dfList = dfList<dfControl>.Obtain(childCount);
		for (int i = 0; i < childCount; i++)
		{
			dfControl component = base.transform.GetChild(i).GetComponent<dfControl>();
			if (component != null)
			{
				dfList.Add(component);
			}
		}
		dfList.Sort();
		return dfList;
	}

	// Token: 0x06003D70 RID: 15728 RVA: 0x000E7698 File Offset: 0x000E5898
	private void updateRenderSettings()
	{
		Camera camera = this.RenderCamera;
		if (camera == null)
		{
			return;
		}
		if (!this.overrideCamera)
		{
			this.updateRenderCamera(camera);
		}
		if (base.transform.hasChanged)
		{
			Vector3 localScale = base.transform.localScale;
			bool flag = localScale.x < float.Epsilon || !Mathf.Approximately(localScale.x, localScale.y) || !Mathf.Approximately(localScale.x, localScale.z);
			if (flag)
			{
				localScale.y = (localScale.z = (localScale.x = Mathf.Max(localScale.x, 0.001f)));
				base.transform.localScale = localScale;
			}
		}
		if (!this.overrideCamera)
		{
			if (Application.isPlaying && this.PixelPerfectMode)
			{
				float num = camera.pixelHeight / (float)this.fixedHeight;
				camera.orthographicSize = num;
				camera.fieldOfView = 60f * num;
			}
			else
			{
				camera.orthographicSize = 1f;
				camera.fieldOfView = 60f;
			}
		}
		camera.transparencySortMode = 2;
		if (this.cachedScreenSize.sqrMagnitude <= 1.401298E-45f)
		{
			this.cachedScreenSize = new Vector2((float)this.FixedWidth, (float)this.FixedHeight);
		}
		base.transform.hasChanged = false;
	}

	// Token: 0x06003D71 RID: 15729 RVA: 0x000E780C File Offset: 0x000E5A0C
	private void updateRenderCamera(Camera camera)
	{
		if (Application.isPlaying && camera.targetTexture != null)
		{
			camera.clearFlags = 2;
			camera.backgroundColor = Color.clear;
		}
		else
		{
			camera.clearFlags = 3;
		}
		Vector3 vector = (!Application.isPlaying) ? Vector3.zero : (-this.uiOffset * this.PixelsToUnits());
		if (camera.isOrthoGraphic)
		{
			camera.nearClipPlane = Mathf.Min(camera.nearClipPlane, -1f);
			camera.farClipPlane = Mathf.Max(camera.farClipPlane, 1f);
		}
		else
		{
			float num = camera.fieldOfView * 0.0174532924f;
			Vector3[] array = this.GetCorners();
			float num2 = Vector3.Distance(array[3], array[0]);
			float num3 = num2 / (2f * Mathf.Tan(num / 2f));
			Vector3 vector2 = base.transform.TransformDirection(Vector3.back) * num3;
			camera.farClipPlane = Mathf.Max(num3 * 2f, camera.farClipPlane);
			vector += vector2;
		}
		if (Application.isPlaying && this.needHalfPixelOffset())
		{
			float pixelHeight = camera.pixelHeight;
			float num4 = 2f / pixelHeight * (pixelHeight / (float)this.FixedHeight);
			Vector3 vector3;
			vector3..ctor(num4 * 0.5f, num4 * -0.5f, 0f);
			vector += vector3;
		}
		if (!this.overrideCamera)
		{
			if (Vector3.SqrMagnitude(camera.transform.localPosition - vector) > 1.401298E-45f)
			{
				camera.transform.localPosition = vector;
			}
			camera.transform.hasChanged = false;
		}
	}

	// Token: 0x06003D72 RID: 15730 RVA: 0x000E79DC File Offset: 0x000E5BDC
	private dfRenderData compileMasterBuffer()
	{
		this.submeshes.Clear();
		dfGUIManager.masterBuffer.Clear();
		for (int i = 0; i < this.drawCallCount; i++)
		{
			this.submeshes.Add(dfGUIManager.masterBuffer.Triangles.Count);
			dfRenderData dfRenderData = this.drawCallBuffers[i];
			if (this.generateNormals && dfRenderData.Normals.Count == 0)
			{
				this.generateNormalsAndTangents(dfRenderData);
			}
			dfGUIManager.masterBuffer.Merge(dfRenderData, false);
		}
		dfGUIManager.masterBuffer.ApplyTransform(base.transform.worldToLocalMatrix);
		return dfGUIManager.masterBuffer;
	}

	// Token: 0x06003D73 RID: 15731 RVA: 0x000E7A84 File Offset: 0x000E5C84
	private void generateNormalsAndTangents(dfRenderData buffer)
	{
		Vector3 normalized = buffer.Transform.MultiplyVector(Vector3.back).normalized;
		Vector4 item = buffer.Transform.MultiplyVector(Vector3.right).normalized;
		item.w = -1f;
		for (int i = 0; i < buffer.Vertices.Count; i++)
		{
			buffer.Normals.Add(normalized);
			buffer.Tangents.Add(item);
		}
	}

	// Token: 0x06003D74 RID: 15732 RVA: 0x000E7B14 File Offset: 0x000E5D14
	private bool needHalfPixelOffset()
	{
		if (this.applyHalfPixelOffset != null)
		{
			return this.applyHalfPixelOffset.Value;
		}
		RuntimePlatform platform = Application.platform;
		bool flag = this.pixelPerfectMode && (platform == 2 || platform == 5 || platform == 7) && SystemInfo.graphicsShaderLevel < 40;
		this.applyHalfPixelOffset = new bool?(Application.isEditor || flag);
		return flag;
	}

	// Token: 0x06003D75 RID: 15733 RVA: 0x000E7B8C File Offset: 0x000E5D8C
	private Material[] gatherMaterials()
	{
		int num = this.renderQueueBase;
		dfGUIManager.MaterialCache.Reset();
		int num2 = this.drawCallBuffers.Matching((dfRenderData buff) => buff != null && buff.Material != null);
		int num3 = 0;
		Material[] array = new Material[num2];
		for (int i = 0; i < this.drawCallBuffers.Count; i++)
		{
			if (!(this.drawCallBuffers[i].Material == null))
			{
				Material material = dfGUIManager.MaterialCache.Lookup(this.drawCallBuffers[i].Material);
				material.renderQueue = num++;
				array[num3++] = material;
			}
		}
		return array;
	}

	// Token: 0x06003D76 RID: 15734 RVA: 0x000E7C48 File Offset: 0x000E5E48
	private void resetDrawCalls()
	{
		this.drawCallCount = 0;
		for (int i = 0; i < this.drawCallBuffers.Count; i++)
		{
			this.drawCallBuffers[i].Release();
		}
		this.drawCallBuffers.Clear();
	}

	// Token: 0x06003D77 RID: 15735 RVA: 0x000E7C94 File Offset: 0x000E5E94
	private dfRenderData getDrawCallBuffer(Material material)
	{
		dfRenderData dfRenderData;
		if (this.MergeMaterials && material != null)
		{
			dfRenderData = this.findDrawCallBufferByMaterial(material);
			if (dfRenderData != null)
			{
				return dfRenderData;
			}
		}
		dfRenderData = dfRenderData.Obtain();
		dfRenderData.Material = material;
		this.drawCallBuffers.Add(dfRenderData);
		this.drawCallCount++;
		return dfRenderData;
	}

	// Token: 0x06003D78 RID: 15736 RVA: 0x000E7CF4 File Offset: 0x000E5EF4
	private dfRenderData findDrawCallBufferByMaterial(Material material)
	{
		for (int i = 0; i < this.drawCallCount; i++)
		{
			if (this.drawCallBuffers[i].Material == material)
			{
				return this.drawCallBuffers[i];
			}
		}
		return null;
	}

	// Token: 0x06003D79 RID: 15737 RVA: 0x000E7D44 File Offset: 0x000E5F44
	private Mesh getRenderMesh()
	{
		this.activeRenderMesh = ((this.activeRenderMesh != 1) ? 1 : 0);
		return this.renderMesh[this.activeRenderMesh];
	}

	// Token: 0x06003D7A RID: 15738 RVA: 0x000E7D78 File Offset: 0x000E5F78
	private void renderControl(ref dfRenderData buffer, dfControl control, uint checksum, float opacity)
	{
		if (!control.GetIsVisibleRaw())
		{
			return;
		}
		float opacity2 = opacity * control.Opacity;
		if (opacity <= 0.005f)
		{
			return;
		}
		dfGUIManager.ClipRegion clipRegion = this.clipStack.Peek();
		checksum = dfChecksumUtil.Calculate(checksum, control.Version);
		Bounds bounds = control.GetBounds();
		bool flag = false;
		if (!(control is IDFMultiRender))
		{
			dfRenderData dfRenderData = control.Render();
			if (dfRenderData == null)
			{
				return;
			}
			if (this.processRenderData(ref buffer, dfRenderData, bounds, checksum, clipRegion))
			{
				flag = true;
			}
		}
		else
		{
			dfList<dfRenderData> dfList = ((IDFMultiRender)control).RenderMultiple();
			if (dfList != null)
			{
				for (int i = 0; i < dfList.Count; i++)
				{
					dfRenderData controlData = dfList[i];
					if (this.processRenderData(ref buffer, controlData, bounds, checksum, clipRegion))
					{
						flag = true;
					}
				}
			}
		}
		if (flag)
		{
			this.ControlsRendered++;
			this.occluders.Add(this.getControlOccluder(control));
		}
		if (control.ClipChildren)
		{
			clipRegion = dfGUIManager.ClipRegion.Obtain(clipRegion, control);
			this.clipStack.Push(clipRegion);
		}
		for (int j = 0; j < control.Controls.Count; j++)
		{
			dfControl control2 = control.Controls[j];
			this.renderControl(ref buffer, control2, checksum, opacity2);
		}
		if (control.ClipChildren)
		{
			this.clipStack.Pop().Release();
		}
	}

	// Token: 0x06003D7B RID: 15739 RVA: 0x000E7EE8 File Offset: 0x000E60E8
	private Rect getControlOccluder(dfControl control)
	{
		Rect screenRect = control.GetScreenRect();
		Vector2 vector;
		vector..ctor(screenRect.width * control.HotZoneScale.x, screenRect.height * control.HotZoneScale.y);
		Vector2 vector2 = new Vector2(vector.x - screenRect.width, vector.y - screenRect.height) * 0.5f;
		return new Rect(screenRect.x - vector2.x, screenRect.y - vector2.y, vector.x, vector.y);
	}

	// Token: 0x06003D7C RID: 15740 RVA: 0x000E7F90 File Offset: 0x000E6190
	private bool processRenderData(ref dfRenderData buffer, dfRenderData controlData, Bounds bounds, uint checksum, dfGUIManager.ClipRegion clipInfo)
	{
		bool flag = buffer == null || (controlData.IsValid() && (!object.Equals(buffer.Shader, controlData.Shader) || (controlData.Material != null && !controlData.Material.Equals(buffer.Material))));
		if (flag && controlData.IsValid())
		{
			buffer = this.getDrawCallBuffer(controlData.Material);
		}
		return controlData != null && controlData.IsValid() && clipInfo.PerformClipping(buffer, bounds, checksum, controlData);
	}

	// Token: 0x06003D7D RID: 15741 RVA: 0x000E8040 File Offset: 0x000E6240
	private void initialize()
	{
		if (this.renderCamera == null)
		{
			Debug.LogError("No camera is assigned to the GUIManager");
			return;
		}
		this.meshRenderer = base.GetComponent<MeshRenderer>();
		this.meshRenderer.hideFlags = 2;
		this.renderFilter = base.GetComponent<MeshFilter>();
		this.renderFilter.hideFlags = 2;
		this.renderMesh = new Mesh[]
		{
			new Mesh
			{
				hideFlags = 4
			},
			new Mesh
			{
				hideFlags = 4
			}
		};
		this.renderMesh[0].MarkDynamic();
		this.renderMesh[1].MarkDynamic();
		if (this.fixedWidth < 0)
		{
			this.fixedWidth = Mathf.RoundToInt((float)this.fixedHeight * 1.33333f);
			base.GetComponentsInChildren<dfControl>().ToList<dfControl>().ForEach(delegate(dfControl x)
			{
				x.ResetLayout(false, false);
			});
		}
	}

	// Token: 0x06003D7E RID: 15742 RVA: 0x000E8134 File Offset: 0x000E6334
	private dfGUICamera findCameraComponent()
	{
		if (this.guiCamera != null)
		{
			return this.guiCamera;
		}
		if (this.renderCamera == null)
		{
			return null;
		}
		this.guiCamera = this.renderCamera.GetComponent<dfGUICamera>();
		if (this.guiCamera == null)
		{
			this.guiCamera = this.renderCamera.gameObject.AddComponent<dfGUICamera>();
			this.guiCamera.transform.position = base.transform.position;
		}
		return this.guiCamera;
	}

	// Token: 0x06003D7F RID: 15743 RVA: 0x000E81C8 File Offset: 0x000E63C8
	private void onResolutionChanged()
	{
		int currentSize = (!Application.isPlaying) ? this.FixedHeight : ((int)this.renderCamera.pixelHeight);
		this.onResolutionChanged(this.FixedHeight, currentSize);
	}

	// Token: 0x06003D80 RID: 15744 RVA: 0x000E8204 File Offset: 0x000E6404
	private void onResolutionChanged(int oldSize, int currentSize)
	{
		float aspect = this.RenderCamera.aspect;
		float num = (float)oldSize * aspect;
		float num2 = (float)currentSize * aspect;
		Vector2 oldSize2;
		oldSize2..ctor(num, (float)oldSize);
		Vector2 currentSize2;
		currentSize2..ctor(num2, (float)currentSize);
		this.onResolutionChanged(oldSize2, currentSize2);
	}

	// Token: 0x06003D81 RID: 15745 RVA: 0x000E8244 File Offset: 0x000E6444
	private void onResolutionChanged(Vector2 oldSize, Vector2 currentSize)
	{
		this.cachedScreenSize = currentSize;
		this.applyHalfPixelOffset = null;
		float aspect = this.RenderCamera.aspect;
		float num = oldSize.y * aspect;
		float num2 = currentSize.y * aspect;
		Vector2 previousResolution;
		previousResolution..ctor(num, oldSize.y);
		Vector2 currentResolution;
		currentResolution..ctor(num2, currentSize.y);
		dfControl[] componentsInChildren = base.GetComponentsInChildren<dfControl>();
		Array.Sort<dfControl>(componentsInChildren, new Comparison<dfControl>(this.renderSortFunc));
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (this.pixelPerfectMode && componentsInChildren[i].Parent == null)
			{
				componentsInChildren[i].MakePixelPerfect(true);
			}
			componentsInChildren[i].OnResolutionChanged(previousResolution, currentResolution);
		}
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			componentsInChildren[j].PerformLayout();
		}
		int num3 = 0;
		while (num3 < componentsInChildren.Length && this.pixelPerfectMode)
		{
			if (componentsInChildren[num3].Parent == null)
			{
				componentsInChildren[num3].MakePixelPerfect(true);
			}
			num3++;
		}
	}

	// Token: 0x06003D82 RID: 15746 RVA: 0x000E837C File Offset: 0x000E657C
	private void invalidateAllControls()
	{
		dfControl[] componentsInChildren = base.GetComponentsInChildren<dfControl>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Invalidate();
		}
		this.updateRenderOrder(null);
	}

	// Token: 0x06003D83 RID: 15747 RVA: 0x000E83B4 File Offset: 0x000E65B4
	private int renderSortFunc(dfControl lhs, dfControl rhs)
	{
		return lhs.RenderOrder.CompareTo(rhs.RenderOrder);
	}

	// Token: 0x06003D84 RID: 15748 RVA: 0x000E83D8 File Offset: 0x000E65D8
	private void updateRenderOrder(dfList<dfControl> list = null)
	{
		dfList<dfControl> dfList = (list == null) ? this.getTopLevelControls() : list;
		dfList.Sort();
		int num = 0;
		for (int i = 0; i < dfList.Count; i++)
		{
			dfControl dfControl = dfList[i];
			if (dfControl.Parent == null)
			{
				dfControl.setRenderOrder(ref num);
			}
		}
	}

	// Token: 0x0400203E RID: 8254
	[SerializeField]
	protected float uiScale = 1f;

	// Token: 0x0400203F RID: 8255
	[SerializeField]
	protected dfInputManager inputManager;

	// Token: 0x04002040 RID: 8256
	[SerializeField]
	protected int fixedWidth = -1;

	// Token: 0x04002041 RID: 8257
	[SerializeField]
	protected int fixedHeight = 600;

	// Token: 0x04002042 RID: 8258
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x04002043 RID: 8259
	[SerializeField]
	protected dfFont defaultFont;

	// Token: 0x04002044 RID: 8260
	[SerializeField]
	protected bool mergeMaterials;

	// Token: 0x04002045 RID: 8261
	[SerializeField]
	protected bool pixelPerfectMode = true;

	// Token: 0x04002046 RID: 8262
	[SerializeField]
	protected Camera renderCamera;

	// Token: 0x04002047 RID: 8263
	[SerializeField]
	protected bool generateNormals;

	// Token: 0x04002048 RID: 8264
	[SerializeField]
	protected bool consumeMouseEvents = true;

	// Token: 0x04002049 RID: 8265
	[SerializeField]
	protected bool overrideCamera;

	// Token: 0x0400204A RID: 8266
	[SerializeField]
	protected int renderQueueBase = 3000;

	// Token: 0x0400204B RID: 8267
	private static dfControl activeControl = null;

	// Token: 0x0400204C RID: 8268
	private static Stack<dfGUIManager.ModalControlReference> modalControlStack = new Stack<dfGUIManager.ModalControlReference>();

	// Token: 0x0400204D RID: 8269
	private dfGUICamera guiCamera;

	// Token: 0x0400204E RID: 8270
	private Mesh[] renderMesh;

	// Token: 0x0400204F RID: 8271
	private MeshFilter renderFilter;

	// Token: 0x04002050 RID: 8272
	private MeshRenderer meshRenderer;

	// Token: 0x04002051 RID: 8273
	private int activeRenderMesh;

	// Token: 0x04002052 RID: 8274
	private int cachedChildCount;

	// Token: 0x04002053 RID: 8275
	private bool isDirty;

	// Token: 0x04002054 RID: 8276
	private Vector2 cachedScreenSize;

	// Token: 0x04002055 RID: 8277
	private Vector3[] corners = new Vector3[4];

	// Token: 0x04002056 RID: 8278
	private dfList<Rect> occluders = new dfList<Rect>(256);

	// Token: 0x04002057 RID: 8279
	private Stack<dfGUIManager.ClipRegion> clipStack = new Stack<dfGUIManager.ClipRegion>();

	// Token: 0x04002058 RID: 8280
	private static dfRenderData masterBuffer = new dfRenderData(4096);

	// Token: 0x04002059 RID: 8281
	private dfList<dfRenderData> drawCallBuffers = new dfList<dfRenderData>();

	// Token: 0x0400205A RID: 8282
	private List<int> submeshes = new List<int>();

	// Token: 0x0400205B RID: 8283
	private int drawCallCount;

	// Token: 0x0400205C RID: 8284
	private Vector2 uiOffset = Vector2.zero;

	// Token: 0x0400205D RID: 8285
	private bool? applyHalfPixelOffset;

	// Token: 0x020006CD RID: 1741
	private class ClipRegion
	{
		// Token: 0x06003D88 RID: 15752 RVA: 0x000E846C File Offset: 0x000E666C
		private ClipRegion()
		{
			this.planes = new dfList<Plane>();
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x000E848C File Offset: 0x000E668C
		public static dfGUIManager.ClipRegion Obtain()
		{
			return (dfGUIManager.ClipRegion.pool.Count <= 0) ? new dfGUIManager.ClipRegion() : dfGUIManager.ClipRegion.pool.Dequeue();
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x000E84C0 File Offset: 0x000E66C0
		public static dfGUIManager.ClipRegion Obtain(dfGUIManager.ClipRegion parent, dfControl control)
		{
			dfGUIManager.ClipRegion clipRegion = (dfGUIManager.ClipRegion.pool.Count <= 0) ? new dfGUIManager.ClipRegion() : dfGUIManager.ClipRegion.pool.Dequeue();
			clipRegion.planes.AddRange(control.GetClippingPlanes());
			if (parent != null)
			{
				clipRegion.planes.AddRange(parent.planes);
			}
			return clipRegion;
		}

		// Token: 0x06003D8C RID: 15756 RVA: 0x000E851C File Offset: 0x000E671C
		public void Release()
		{
			this.planes.Clear();
			dfGUIManager.ClipRegion.pool.Enqueue(this);
		}

		// Token: 0x06003D8D RID: 15757 RVA: 0x000E8534 File Offset: 0x000E6734
		public bool PerformClipping(dfRenderData dest, Bounds bounds, uint checksum, dfRenderData controlData)
		{
			if (controlData.Checksum == checksum)
			{
				if (controlData.Intersection == dfIntersectionType.Inside)
				{
					dest.Merge(controlData, true);
					return true;
				}
				if (controlData.Intersection == dfIntersectionType.None)
				{
					return false;
				}
			}
			bool result = false;
			dfIntersectionType dfIntersectionType;
			using (dfList<Plane> dfList = this.TestIntersection(bounds, out dfIntersectionType))
			{
				if (dfIntersectionType == dfIntersectionType.Inside)
				{
					dest.Merge(controlData, true);
					result = true;
				}
				else if (dfIntersectionType == dfIntersectionType.Intersecting)
				{
					this.clipToPlanes(dfList, controlData, dest, checksum);
					result = true;
				}
				controlData.Checksum = checksum;
				controlData.Intersection = dfIntersectionType;
			}
			return result;
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x000E85EC File Offset: 0x000E67EC
		public dfList<Plane> TestIntersection(Bounds bounds, out dfIntersectionType type)
		{
			if (this.planes == null || this.planes.Count == 0)
			{
				type = dfIntersectionType.Inside;
				return null;
			}
			dfList<Plane> dfList = dfList<Plane>.Obtain(this.planes.Count);
			Vector3 center = bounds.center;
			Vector3 extents = bounds.extents;
			bool flag = false;
			for (int i = 0; i < this.planes.Count; i++)
			{
				Plane item = this.planes[i];
				Vector3 normal = item.normal;
				float distance = item.distance;
				float num = extents.x * Mathf.Abs(normal.x) + extents.y * Mathf.Abs(normal.y) + extents.z * Mathf.Abs(normal.z);
				float num2 = Vector3.Dot(normal, center) + distance;
				if (Mathf.Abs(num2) <= num)
				{
					flag = true;
					dfList.Add(item);
				}
				else if (num2 < -num)
				{
					type = dfIntersectionType.None;
					dfList.Release();
					return null;
				}
			}
			if (flag)
			{
				type = dfIntersectionType.Intersecting;
				return dfList;
			}
			type = dfIntersectionType.Inside;
			dfList.Release();
			return null;
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x000E8714 File Offset: 0x000E6914
		public void clipToPlanes(dfList<Plane> planes, dfRenderData data, dfRenderData dest, uint controlChecksum)
		{
			if (data == null || data.Vertices.Count == 0)
			{
				return;
			}
			if (planes == null || planes.Count == 0)
			{
				dest.Merge(data, true);
				return;
			}
			dfClippingUtil.Clip(planes, data, dest);
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x000E875C File Offset: 0x000E695C
		private static int sortClipPlanes(Plane lhs, Plane rhs)
		{
			return lhs.distance.CompareTo(rhs.distance);
		}

		// Token: 0x04002067 RID: 8295
		private static Queue<dfGUIManager.ClipRegion> pool = new Queue<dfGUIManager.ClipRegion>();

		// Token: 0x04002068 RID: 8296
		private dfList<Plane> planes;
	}

	// Token: 0x020006CE RID: 1742
	private struct ModalControlReference
	{
		// Token: 0x04002069 RID: 8297
		public dfControl control;

		// Token: 0x0400206A RID: 8298
		public dfGUIManager.ModalPoppedCallback callback;
	}

	// Token: 0x020006CF RID: 1743
	private class MaterialCache
	{
		// Token: 0x06003D93 RID: 15763 RVA: 0x000E8794 File Offset: 0x000E6994
		public static Material Lookup(Material BaseMaterial)
		{
			if (BaseMaterial == null)
			{
				Debug.LogError("Cache lookup on null material");
				return null;
			}
			dfGUIManager.MaterialCache.Cache cache = null;
			if (dfGUIManager.MaterialCache.cache.TryGetValue(BaseMaterial, out cache))
			{
				return cache.Obtain();
			}
			dfGUIManager.MaterialCache.Cache cache2 = new dfGUIManager.MaterialCache.Cache(BaseMaterial);
			dfGUIManager.MaterialCache.cache[BaseMaterial] = cache2;
			cache = cache2;
			return cache.Obtain();
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x000E87F0 File Offset: 0x000E69F0
		public static void Reset()
		{
			dfGUIManager.MaterialCache.Cache.ResetAll();
		}

		// Token: 0x0400206B RID: 8299
		private static Dictionary<Material, dfGUIManager.MaterialCache.Cache> cache = new Dictionary<Material, dfGUIManager.MaterialCache.Cache>();

		// Token: 0x020006D0 RID: 1744
		private class Cache
		{
			// Token: 0x06003D95 RID: 15765 RVA: 0x000E87F8 File Offset: 0x000E69F8
			private Cache()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06003D96 RID: 15766 RVA: 0x000E8814 File Offset: 0x000E6A14
			public Cache(Material BaseMaterial)
			{
				this.baseMaterial = BaseMaterial;
				this.instances.Add(BaseMaterial);
				dfGUIManager.MaterialCache.Cache.cacheInstances.Add(this);
			}

			// Token: 0x06003D98 RID: 15768 RVA: 0x000E8854 File Offset: 0x000E6A54
			public static void ResetAll()
			{
				for (int i = 0; i < dfGUIManager.MaterialCache.Cache.cacheInstances.Count; i++)
				{
					dfGUIManager.MaterialCache.Cache.cacheInstances[i].Reset();
				}
			}

			// Token: 0x06003D99 RID: 15769 RVA: 0x000E888C File Offset: 0x000E6A8C
			public Material Obtain()
			{
				if (this.currentIndex < this.instances.Count)
				{
					return this.instances[this.currentIndex++];
				}
				this.currentIndex++;
				Material material = new Material(this.baseMaterial)
				{
					hideFlags = 4,
					name = string.Format("{0} (Copy {1})", this.baseMaterial.name, this.currentIndex)
				};
				this.instances.Add(material);
				return material;
			}

			// Token: 0x06003D9A RID: 15770 RVA: 0x000E8924 File Offset: 0x000E6B24
			public void Reset()
			{
				this.currentIndex = 0;
			}

			// Token: 0x0400206C RID: 8300
			private static List<dfGUIManager.MaterialCache.Cache> cacheInstances = new List<dfGUIManager.MaterialCache.Cache>();

			// Token: 0x0400206D RID: 8301
			private Material baseMaterial;

			// Token: 0x0400206E RID: 8302
			private List<Material> instances = new List<Material>(10);

			// Token: 0x0400206F RID: 8303
			private int currentIndex;
		}
	}

	// Token: 0x020008DD RID: 2269
	// (Invoke) Token: 0x06004D4C RID: 19788
	[dfEventCategory("Modal Dialog")]
	public delegate void ModalPoppedCallback(dfControl control);

	// Token: 0x020008DE RID: 2270
	// (Invoke) Token: 0x06004D50 RID: 19792
	[dfEventCategory("Global Callbacks")]
	public delegate void RenderCallback(dfGUIManager manager);
}
