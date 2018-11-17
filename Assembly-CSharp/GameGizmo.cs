using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004AE RID: 1198
public class GameGizmo : ScriptableObject
{
	// Token: 0x1700095D RID: 2397
	// (get) Token: 0x06002A0F RID: 10767 RVA: 0x000A4C8C File Offset: 0x000A2E8C
	public float minAlpha
	{
		get
		{
			return this._minAlpha;
		}
	}

	// Token: 0x1700095E RID: 2398
	// (get) Token: 0x06002A10 RID: 10768 RVA: 0x000A4C94 File Offset: 0x000A2E94
	public float maxAlpha
	{
		get
		{
			return this._maxAlpha;
		}
	}

	// Token: 0x1700095F RID: 2399
	// (get) Token: 0x06002A11 RID: 10769 RVA: 0x000A4C9C File Offset: 0x000A2E9C
	public Color goodColor
	{
		get
		{
			return this._good;
		}
	}

	// Token: 0x17000960 RID: 2400
	// (get) Token: 0x06002A12 RID: 10770 RVA: 0x000A4CA4 File Offset: 0x000A2EA4
	public Color badColor
	{
		get
		{
			return this._bad;
		}
	}

	// Token: 0x06002A13 RID: 10771 RVA: 0x000A4CAC File Offset: 0x000A2EAC
	protected virtual GameGizmo.Instance ConstructInstance()
	{
		return new GameGizmo.Instance(this);
	}

	// Token: 0x06002A14 RID: 10772 RVA: 0x000A4CB4 File Offset: 0x000A2EB4
	private bool CreateInstance(out GameGizmo.Instance instance, Type type)
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
				this._instances = new HashSet<GameGizmo.Instance>();
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

	// Token: 0x06002A15 RID: 10773 RVA: 0x000A4D60 File Offset: 0x000A2F60
	public bool Create<TInstance>(out TInstance instance) where TInstance : GameGizmo.Instance
	{
		GameGizmo.Instance instance2;
		if (this.CreateInstance(out instance2, typeof(TInstance)))
		{
			instance = (TInstance)((object)instance2);
			return true;
		}
		instance = (TInstance)((object)null);
		return false;
	}

	// Token: 0x06002A16 RID: 10774 RVA: 0x000A4DA0 File Offset: 0x000A2FA0
	protected virtual void DeconstructInstance(GameGizmo.Instance instance)
	{
	}

	// Token: 0x06002A17 RID: 10775 RVA: 0x000A4DA4 File Offset: 0x000A2FA4
	private bool DestroyInstance(GameGizmo.Instance instance)
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

	// Token: 0x06002A18 RID: 10776 RVA: 0x000A4E18 File Offset: 0x000A3018
	public bool Destroy<TInstance>(ref TInstance instance) where TInstance : GameGizmo.Instance
	{
		if (this.DestroyInstance(instance))
		{
			instance = (TInstance)((object)null);
			return true;
		}
		return false;
	}

	// Token: 0x04001600 RID: 5632
	[SerializeField]
	private Mesh _mesh;

	// Token: 0x04001601 RID: 5633
	[SerializeField]
	private Material[] _materials;

	// Token: 0x04001602 RID: 5634
	[SerializeField]
	private bool _castShadows;

	// Token: 0x04001603 RID: 5635
	[SerializeField]
	private bool _receiveShadows;

	// Token: 0x04001604 RID: 5636
	[SerializeField]
	private int _layer;

	// Token: 0x04001605 RID: 5637
	[SerializeField]
	private Color _good = Color.green;

	// Token: 0x04001606 RID: 5638
	[SerializeField]
	private Color _bad = Color.red;

	// Token: 0x04001607 RID: 5639
	[SerializeField]
	private float _minAlpha = 0.9f;

	// Token: 0x04001608 RID: 5640
	[SerializeField]
	private float _maxAlpha = 1f;

	// Token: 0x04001609 RID: 5641
	[SerializeField]
	private Vector3 alternateArrowDirection;

	// Token: 0x0400160A RID: 5642
	private HashSet<GameGizmo.Instance> _instances;

	// Token: 0x020004AF RID: 1199
	public class Instance
	{
		// Token: 0x06002A19 RID: 10777 RVA: 0x000A4E40 File Offset: 0x000A3040
		protected internal Instance(GameGizmo gizmo)
		{
			this.localPosition = Vector3.zero;
			this.localRotation = Quaternion.identity;
			this.localScale = Vector3.one;
			this.gameGizmo = gizmo;
			this.propertyBlock = new MaterialPropertyBlock();
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06002A1A RID: 10778 RVA: 0x000A4E94 File Offset: 0x000A3094
		protected int layer
		{
			get
			{
				return this.gameGizmo._layer;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x000A4EA4 File Offset: 0x000A30A4
		protected bool castShadows
		{
			get
			{
				return this.gameGizmo._castShadows;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002A1C RID: 10780 RVA: 0x000A4EB4 File Offset: 0x000A30B4
		protected bool receiveShadows
		{
			get
			{
				return this.gameGizmo._receiveShadows;
			}
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x000A4EC4 File Offset: 0x000A30C4
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

		// Token: 0x06002A1E RID: 10782 RVA: 0x000A4F00 File Offset: 0x000A3100
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

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002A1F RID: 10783 RVA: 0x000A4F74 File Offset: 0x000A3174
		// (set) Token: 0x06002A20 RID: 10784 RVA: 0x000A4FB0 File Offset: 0x000A31B0
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

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002A21 RID: 10785 RVA: 0x000A4FEC File Offset: 0x000A31EC
		// (set) Token: 0x06002A22 RID: 10786 RVA: 0x000A5020 File Offset: 0x000A3220
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

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002A23 RID: 10787 RVA: 0x000A5068 File Offset: 0x000A3268
		// (set) Token: 0x06002A24 RID: 10788 RVA: 0x000A5070 File Offset: 0x000A3270
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

		// Token: 0x06002A25 RID: 10789 RVA: 0x000A50DC File Offset: 0x000A32DC
		protected Matrix4x4 DefaultMatrix()
		{
			Matrix4x4 matrix4x = Matrix4x4.TRS(this.localPosition, this.localRotation, this.localScale);
			if (this._parent)
			{
				matrix4x = this._parent.localToWorldMatrix * matrix4x;
			}
			return matrix4x;
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x000A5124 File Offset: 0x000A3324
		public void Render()
		{
			this.Render(false, null);
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x000A5130 File Offset: 0x000A3330
		public void Render(Camera camera)
		{
			this.Render(camera, camera);
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x000A5140 File Offset: 0x000A3340
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

		// Token: 0x0400160B RID: 5643
		[NonSerialized]
		public readonly GameGizmo gameGizmo;

		// Token: 0x0400160C RID: 5644
		[NonSerialized]
		public readonly MaterialPropertyBlock propertyBlock;

		// Token: 0x0400160D RID: 5645
		[NonSerialized]
		public Vector3 localPosition;

		// Token: 0x0400160E RID: 5646
		[NonSerialized]
		public Quaternion localRotation;

		// Token: 0x0400160F RID: 5647
		[NonSerialized]
		public Vector3 localScale;

		// Token: 0x04001610 RID: 5648
		[NonSerialized]
		public Matrix4x4? overrideMatrix;

		// Token: 0x04001611 RID: 5649
		[NonSerialized]
		public MeshRenderer carrierRenderer;

		// Token: 0x04001612 RID: 5650
		protected Matrix4x4? ultimateMatrix;

		// Token: 0x04001613 RID: 5651
		protected bool hideMesh;

		// Token: 0x04001614 RID: 5652
		private List<Object> resources = new List<Object>();

		// Token: 0x04001615 RID: 5653
		private Transform _parent;
	}
}
