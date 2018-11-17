using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000569 RID: 1385
public class GameGizmo : ScriptableObject
{
	// Token: 0x170009CD RID: 2509
	// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x000ACA24 File Offset: 0x000AAC24
	public float minAlpha
	{
		get
		{
			return this._minAlpha;
		}
	}

	// Token: 0x170009CE RID: 2510
	// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x000ACA2C File Offset: 0x000AAC2C
	public float maxAlpha
	{
		get
		{
			return this._maxAlpha;
		}
	}

	// Token: 0x170009CF RID: 2511
	// (get) Token: 0x06002DC3 RID: 11715 RVA: 0x000ACA34 File Offset: 0x000AAC34
	public Color goodColor
	{
		get
		{
			return this._good;
		}
	}

	// Token: 0x170009D0 RID: 2512
	// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x000ACA3C File Offset: 0x000AAC3C
	public Color badColor
	{
		get
		{
			return this._bad;
		}
	}

	// Token: 0x06002DC5 RID: 11717 RVA: 0x000ACA44 File Offset: 0x000AAC44
	protected virtual global::GameGizmo.Instance ConstructInstance()
	{
		return new global::GameGizmo.Instance(this);
	}

	// Token: 0x06002DC6 RID: 11718 RVA: 0x000ACA4C File Offset: 0x000AAC4C
	private bool CreateInstance(out global::GameGizmo.Instance instance, Type type)
	{
		try
		{
			instance = this.ConstructInstance();
			if (object.ReferenceEquals(instance, null))
			{
				return false;
			}
			if (this._instances == null)
			{
				this._instances = new HashSet<global::GameGizmo.Instance>();
			}
			this._instances.Add(instance);
			if (!type.IsAssignableFrom(instance.GetType()))
			{
				this.DestroyInstance(instance);
				throw new InvalidCastException();
			}
		}
		catch (Exception ex)
		{
			Debug.LogException(ex, this);
			instance = null;
			return false;
		}
		return true;
	}

	// Token: 0x06002DC7 RID: 11719 RVA: 0x000ACAF8 File Offset: 0x000AACF8
	public bool Create<TInstance>(out TInstance instance) where TInstance : global::GameGizmo.Instance
	{
		global::GameGizmo.Instance instance2;
		if (this.CreateInstance(out instance2, typeof(TInstance)))
		{
			instance = (TInstance)((object)instance2);
			return true;
		}
		instance = (TInstance)((object)null);
		return false;
	}

	// Token: 0x06002DC8 RID: 11720 RVA: 0x000ACB38 File Offset: 0x000AAD38
	protected virtual void DeconstructInstance(global::GameGizmo.Instance instance)
	{
	}

	// Token: 0x06002DC9 RID: 11721 RVA: 0x000ACB3C File Offset: 0x000AAD3C
	private bool DestroyInstance(global::GameGizmo.Instance instance)
	{
		if (!object.ReferenceEquals(instance, null) && this._instances != null && this._instances.Remove(instance))
		{
			try
			{
				instance.ClearResources();
				this.DeconstructInstance(instance);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex, this);
			}
			return true;
		}
		return false;
	}

	// Token: 0x06002DCA RID: 11722 RVA: 0x000ACBB0 File Offset: 0x000AADB0
	public bool Destroy<TInstance>(ref TInstance instance) where TInstance : global::GameGizmo.Instance
	{
		if (this.DestroyInstance(instance))
		{
			instance = (TInstance)((object)null);
			return true;
		}
		return false;
	}

	// Token: 0x040017BD RID: 6077
	[SerializeField]
	private Mesh _mesh;

	// Token: 0x040017BE RID: 6078
	[SerializeField]
	private Material[] _materials;

	// Token: 0x040017BF RID: 6079
	[SerializeField]
	private bool _castShadows;

	// Token: 0x040017C0 RID: 6080
	[SerializeField]
	private bool _receiveShadows;

	// Token: 0x040017C1 RID: 6081
	[SerializeField]
	private int _layer;

	// Token: 0x040017C2 RID: 6082
	[SerializeField]
	private Color _good = Color.green;

	// Token: 0x040017C3 RID: 6083
	[SerializeField]
	private Color _bad = Color.red;

	// Token: 0x040017C4 RID: 6084
	[SerializeField]
	private float _minAlpha = 0.9f;

	// Token: 0x040017C5 RID: 6085
	[SerializeField]
	private float _maxAlpha = 1f;

	// Token: 0x040017C6 RID: 6086
	[SerializeField]
	private Vector3 alternateArrowDirection;

	// Token: 0x040017C7 RID: 6087
	private HashSet<global::GameGizmo.Instance> _instances;

	// Token: 0x0200056A RID: 1386
	public class Instance
	{
		// Token: 0x06002DCB RID: 11723 RVA: 0x000ACBD8 File Offset: 0x000AADD8
		protected internal Instance(global::GameGizmo gizmo)
		{
			this.localPosition = Vector3.zero;
			this.localRotation = Quaternion.identity;
			this.localScale = Vector3.one;
			this.gameGizmo = gizmo;
			this.propertyBlock = new MaterialPropertyBlock();
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06002DCC RID: 11724 RVA: 0x000ACC2C File Offset: 0x000AAE2C
		protected int layer
		{
			get
			{
				return this.gameGizmo._layer;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06002DCD RID: 11725 RVA: 0x000ACC3C File Offset: 0x000AAE3C
		protected bool castShadows
		{
			get
			{
				return this.gameGizmo._castShadows;
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x000ACC4C File Offset: 0x000AAE4C
		protected bool receiveShadows
		{
			get
			{
				return this.gameGizmo._receiveShadows;
			}
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x000ACC5C File Offset: 0x000AAE5C
		public void AddResourceToDelete(Object resource)
		{
			if (resource)
			{
				List<Object> list;
				if ((list = this.resources) == null)
				{
					list = (this.resources = new List<Object>());
				}
				list.Add(resource);
			}
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x000ACC98 File Offset: 0x000AAE98
		internal void ClearResources()
		{
			List<Object> list = this.resources;
			if (list != null)
			{
				this.resources = null;
				foreach (Object @object in list)
				{
					Object.Destroy(@object);
				}
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06002DD1 RID: 11729 RVA: 0x000ACD0C File Offset: 0x000AAF0C
		// (set) Token: 0x06002DD2 RID: 11730 RVA: 0x000ACD48 File Offset: 0x000AAF48
		public Vector3 position
		{
			get
			{
				return (!this._parent) ? this.localPosition : this._parent.TransformPoint(this.localPosition);
			}
			set
			{
				if (this._parent)
				{
					this.localPosition = this._parent.InverseTransformPoint(value);
				}
				else
				{
					this.localPosition = value;
				}
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06002DD3 RID: 11731 RVA: 0x000ACD84 File Offset: 0x000AAF84
		// (set) Token: 0x06002DD4 RID: 11732 RVA: 0x000ACDB8 File Offset: 0x000AAFB8
		public Quaternion rotation
		{
			get
			{
				return (!this._parent) ? this.localRotation : (this.localRotation * this._parent.rotation);
			}
			set
			{
				if (this._parent)
				{
					this.localRotation = Quaternion.Inverse(this._parent.rotation) * value;
				}
				else
				{
					this.localRotation = value;
				}
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06002DD5 RID: 11733 RVA: 0x000ACE00 File Offset: 0x000AB000
		// (set) Token: 0x06002DD6 RID: 11734 RVA: 0x000ACE08 File Offset: 0x000AB008
		public Transform parent
		{
			get
			{
				return this._parent;
			}
			set
			{
				if (value != this._parent)
				{
					if (value)
					{
						this.localPosition = value.InverseTransformPoint(this.position);
						this.localRotation = Quaternion.Inverse(value.rotation) * this.rotation;
						this._parent = value;
					}
					else
					{
						this._parent = null;
					}
				}
			}
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000ACE74 File Offset: 0x000AB074
		protected Matrix4x4 DefaultMatrix()
		{
			Matrix4x4 matrix4x = Matrix4x4.TRS(this.localPosition, this.localRotation, this.localScale);
			if (this._parent)
			{
				matrix4x = this._parent.localToWorldMatrix * matrix4x;
			}
			return matrix4x;
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000ACEBC File Offset: 0x000AB0BC
		public void Render()
		{
			this.Render(false, null);
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000ACEC8 File Offset: 0x000AB0C8
		public void Render(Camera camera)
		{
			this.Render(camera, camera);
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000ACED8 File Offset: 0x000AB0D8
		protected virtual void Render(bool useCamera, Camera camera)
		{
			if (this.hideMesh)
			{
				return;
			}
			Mesh mesh = this.gameGizmo._mesh;
			if (!mesh)
			{
				return;
			}
			Material[] materials = this.gameGizmo._materials;
			int num;
			if (materials == null || (num = materials.Length) == 0)
			{
				return;
			}
			Matrix4x4? matrix4x = this.ultimateMatrix;
			Matrix4x4 matrix4x2;
			if (matrix4x != null)
			{
				matrix4x2 = matrix4x.Value;
			}
			else
			{
				Matrix4x4? matrix4x3 = this.overrideMatrix;
				matrix4x2 = ((matrix4x3 == null) ? this.DefaultMatrix() : matrix4x3.Value);
			}
			Matrix4x4 matrix4x4 = matrix4x2;
			if (this.gameGizmo.alternateArrowDirection != Vector3.zero)
			{
				matrix4x4 *= Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(this.gameGizmo.alternateArrowDirection), Vector3.one);
			}
			for (int i = 0; i < mesh.subMeshCount; i++)
			{
				Graphics.DrawMesh(mesh, matrix4x4, materials[i % num], this.gameGizmo._layer, camera, i, this.propertyBlock, this.gameGizmo._castShadows, this.gameGizmo._receiveShadows);
			}
		}

		// Token: 0x040017C8 RID: 6088
		[NonSerialized]
		public readonly global::GameGizmo gameGizmo;

		// Token: 0x040017C9 RID: 6089
		[NonSerialized]
		public readonly MaterialPropertyBlock propertyBlock;

		// Token: 0x040017CA RID: 6090
		[NonSerialized]
		public Vector3 localPosition;

		// Token: 0x040017CB RID: 6091
		[NonSerialized]
		public Quaternion localRotation;

		// Token: 0x040017CC RID: 6092
		[NonSerialized]
		public Vector3 localScale;

		// Token: 0x040017CD RID: 6093
		[NonSerialized]
		public Matrix4x4? overrideMatrix;

		// Token: 0x040017CE RID: 6094
		[NonSerialized]
		public MeshRenderer carrierRenderer;

		// Token: 0x040017CF RID: 6095
		protected Matrix4x4? ultimateMatrix;

		// Token: 0x040017D0 RID: 6096
		protected bool hideMesh;

		// Token: 0x040017D1 RID: 6097
		private List<Object> resources = new List<Object>();

		// Token: 0x040017D2 RID: 6098
		private Transform _parent;
	}
}
