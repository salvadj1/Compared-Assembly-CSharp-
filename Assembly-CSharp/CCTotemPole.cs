using System;
using UnityEngine;

// Token: 0x0200029D RID: 669
public sealed class CCTotemPole : CCTotem<CCTotem.TotemPole, CCTotemPole>
{
	// Token: 0x1400000D RID: 13
	// (add) Token: 0x060017D8 RID: 6104 RVA: 0x0005D064 File Offset: 0x0005B264
	// (remove) Token: 0x060017D9 RID: 6105 RVA: 0x0005D080 File Offset: 0x0005B280
	public event CCTotem.PositionBinder OnBindPosition;

	// Token: 0x1400000E RID: 14
	// (add) Token: 0x060017DA RID: 6106 RVA: 0x0005D09C File Offset: 0x0005B29C
	// (remove) Token: 0x060017DB RID: 6107 RVA: 0x0005D0E8 File Offset: 0x0005B2E8
	public event CCTotem.ConfigurationBinder OnConfigurationBinding
	{
		add
		{
			if (!object.ReferenceEquals(this.ConfigurationBinder, value))
			{
				if (!object.ReferenceEquals(this.ConfigurationBinder, null))
				{
					this.ExecuteAllBindings(false);
					this.ConfigurationBinder = null;
				}
				this.ConfigurationBinder = value;
				this.ExecuteAllBindings(true);
			}
		}
		remove
		{
			if (object.ReferenceEquals(this.ConfigurationBinder, value))
			{
				this.ExecuteAllBindings(false);
				if (object.ReferenceEquals(this.ConfigurationBinder, value))
				{
					this.ConfigurationBinder = null;
				}
			}
		}
	}

	// Token: 0x170006EB RID: 1771
	// (get) Token: 0x060017DC RID: 6108 RVA: 0x0005D128 File Offset: 0x0005B328
	public bool Exists
	{
		get
		{
			return !object.ReferenceEquals(this.totemicObject, null);
		}
	}

	// Token: 0x170006EC RID: 1772
	// (get) Token: 0x060017DD RID: 6109 RVA: 0x0005D13C File Offset: 0x0005B33C
	public float MinimumHeight
	{
		get
		{
			return this.minimumHeight;
		}
	}

	// Token: 0x170006ED RID: 1773
	// (get) Token: 0x060017DE RID: 6110 RVA: 0x0005D144 File Offset: 0x0005B344
	public float MaximumHeight
	{
		get
		{
			return this.maximumHeight;
		}
	}

	// Token: 0x170006EE RID: 1774
	// (get) Token: 0x060017DF RID: 6111 RVA: 0x0005D14C File Offset: 0x0005B34C
	public float Height
	{
		get
		{
			return (!this.Exists) ? (this.minimumHeight + this.initialHeightFraction * (this.maximumHeight - this.minimumHeight)) : this.totemicObject.Expansion.Value;
		}
	}

	// Token: 0x170006EF RID: 1775
	// (get) Token: 0x060017E0 RID: 6112 RVA: 0x0005D18C File Offset: 0x0005B38C
	private CCTotem.Initialization Members
	{
		get
		{
			return new CCTotem.Initialization(this, this.prefab, this.minimumHeight, this.maximumHeight, this.minimumHeight + (this.maximumHeight - this.minimumHeight) * this.initialHeightFraction, this.bottomBufferUnits);
		}
	}

	// Token: 0x170006F0 RID: 1776
	// (get) Token: 0x060017E1 RID: 6113 RVA: 0x0005D1C8 File Offset: 0x0005B3C8
	public bool isGrounded
	{
		get
		{
			return this.Exists && this.totemicObject.isGrounded;
		}
	}

	// Token: 0x170006F1 RID: 1777
	// (get) Token: 0x060017E2 RID: 6114 RVA: 0x0005D1E4 File Offset: 0x0005B3E4
	public Vector3 velocity
	{
		get
		{
			return (!this.Exists) ? Vector3.zero : this.totemicObject.velocity;
		}
	}

	// Token: 0x170006F2 RID: 1778
	// (get) Token: 0x060017E3 RID: 6115 RVA: 0x0005D214 File Offset: 0x0005B414
	public CollisionFlags collisionFlags
	{
		get
		{
			return (!this.Exists) ? 0 : this.totemicObject.collisionFlags;
		}
	}

	// Token: 0x170006F3 RID: 1779
	// (get) Token: 0x060017E4 RID: 6116 RVA: 0x0005D234 File Offset: 0x0005B434
	public float stepOffset
	{
		get
		{
			return (!this.Exists) ? this.prefab.stepOffset : this.totemicObject.stepOffset;
		}
	}

	// Token: 0x170006F4 RID: 1780
	// (get) Token: 0x060017E5 RID: 6117 RVA: 0x0005D268 File Offset: 0x0005B468
	public float slopeLimit
	{
		get
		{
			return (!this.Exists) ? this.prefab.slopeLimit : this.totemicObject.slopeLimit;
		}
	}

	// Token: 0x170006F5 RID: 1781
	// (get) Token: 0x060017E6 RID: 6118 RVA: 0x0005D29C File Offset: 0x0005B49C
	public Vector3 center
	{
		get
		{
			return (!this.Exists) ? this.prefab.center : this.totemicObject.center;
		}
	}

	// Token: 0x170006F6 RID: 1782
	// (get) Token: 0x060017E7 RID: 6119 RVA: 0x0005D2D0 File Offset: 0x0005B4D0
	[Obsolete("this is the height of the character controller. prefer this.Height")]
	public float height
	{
		get
		{
			return (!this.Exists) ? this.prefab.height : this.totemicObject.height;
		}
	}

	// Token: 0x170006F7 RID: 1783
	// (get) Token: 0x060017E8 RID: 6120 RVA: 0x0005D304 File Offset: 0x0005B504
	public float radius
	{
		get
		{
			return (!this.Exists) ? this.prefab.radius : this.totemicObject.radius;
		}
	}

	// Token: 0x060017E9 RID: 6121 RVA: 0x0005D338 File Offset: 0x0005B538
	public bool UpdateConfiguration()
	{
		this.LastException = null;
		CCTotem.Initialization members = this.Members;
		bool result;
		try
		{
			this.LastGoodConfiguration = new CCTotem.Configuration(ref members);
			this.HasLastGoodConfiguration = true;
			result = true;
		}
		catch (ArgumentException lastException)
		{
			this.LastException = lastException;
			result = false;
		}
		return result;
	}

	// Token: 0x060017EA RID: 6122 RVA: 0x0005D3A4 File Offset: 0x0005B5A4
	private void Awake()
	{
		if (!this.UpdateConfiguration())
		{
			Debug.LogException(this.LastException, this);
			return;
		}
		this.CreatePhysics();
	}

	// Token: 0x060017EB RID: 6123 RVA: 0x0005D3C4 File Offset: 0x0005B5C4
	private void CreatePhysics()
	{
		if (!this.HasLastGoodConfiguration && !this.UpdateConfiguration())
		{
			Debug.LogException(this.LastException, this);
			return;
		}
		base.AssignTotemicObject(new CCTotem.TotemPole(ref this.LastGoodConfiguration));
		this.totemicObject.Create();
	}

	// Token: 0x060017EC RID: 6124 RVA: 0x0005D410 File Offset: 0x0005B610
	internal void DestroyCCDesc(ref CCDesc CCDesc)
	{
		if (CCDesc)
		{
			CCDesc ccdesc = CCDesc;
			CCDesc = null;
			this.ExecuteBinding(ccdesc, false);
			Object.Destroy(ccdesc.gameObject);
		}
	}

	// Token: 0x060017ED RID: 6125 RVA: 0x0005D444 File Offset: 0x0005B644
	internal void ExecuteBinding(CCDesc CCDesc, bool Bind)
	{
		if (CCDesc && !object.ReferenceEquals(this.ConfigurationBinder, null))
		{
			try
			{
				this.ConfigurationBinder(Bind, CCDesc, this.Tag);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex, this);
			}
		}
	}

	// Token: 0x060017EE RID: 6126 RVA: 0x0005D4B0 File Offset: 0x0005B6B0
	internal void ExecuteAllBindings(bool Bind)
	{
		if (this.Exists)
		{
			this.ExecuteBinding(this.totemicObject.CCDesc, Bind);
			for (int i = 0; i < this.totemicObject.Configuration.numRequiredTotemicFigures; i++)
			{
				this.ExecuteBinding(this.totemicObject.TotemicFigures[i].CCDesc, Bind);
			}
		}
	}

	// Token: 0x060017EF RID: 6127 RVA: 0x0005D518 File Offset: 0x0005B718
	public CCTotem.MoveInfo Move(Vector3 motion)
	{
		return this.Move(motion, this.Height);
	}

	// Token: 0x060017F0 RID: 6128 RVA: 0x0005D528 File Offset: 0x0005B728
	public CCTotem.MoveInfo Move(Vector3 motion, float height)
	{
		CCTotem.TotemPole totemicObject = this.totemicObject;
		if (object.ReferenceEquals(totemicObject, null))
		{
			throw new InvalidOperationException("Exists == false");
		}
		CCTotem.MoveInfo result = totemicObject.Move(motion, height);
		this.BindPositions(result.PositionPlacement);
		return result;
	}

	// Token: 0x060017F1 RID: 6129 RVA: 0x0005D56C File Offset: 0x0005B76C
	public bool SmudgeTo(Vector3 worldSkinnedBottom)
	{
		if (!this.Exists)
		{
			return false;
		}
		Vector3 position = base.transform.position;
		if (position == worldSkinnedBottom)
		{
			return true;
		}
		Vector3 vector = worldSkinnedBottom - position;
		CCDesc ccdesc = this.totemicObject.CCDesc;
		if (!ccdesc)
		{
			return false;
		}
		Vector3 vector2;
		vector2.x = (vector2.z = 0f);
		vector2.y = ccdesc.effectiveHeight * 0.5f - ccdesc.radius;
		Vector3 center = ccdesc.center;
		Vector3 vector3 = ccdesc.OffsetToWorld(center - vector2);
		Vector3 vector4 = ccdesc.OffsetToWorld(center + vector2);
		float magnitude = (ccdesc.OffsetToWorld(center + new Vector3(ccdesc.skinnedRadius, 0f, 0f)) - ccdesc.worldCenter).magnitude;
		float magnitude2 = vector.magnitude;
		float num = 1f / magnitude2;
		Vector3 vector5;
		vector5.x = vector.x * num;
		vector5.y = vector.y * num;
		vector5.z = vector.z * num;
		int num2 = 0;
		int layer = base.gameObject.layer;
		for (int i = 0; i < 32; i++)
		{
			if (!Physics.GetIgnoreLayerCollision(layer, i))
			{
				num2 |= 1 << i;
			}
		}
		if (Physics.CapsuleCast(vector3, vector4, magnitude, vector5, magnitude2, num2))
		{
			return false;
		}
		this.totemicObject.CCDesc.transform.position += vector;
		for (int j = 0; j < this.totemicObject.Configuration.numRequiredTotemicFigures; j++)
		{
			this.totemicObject.TotemicFigures[j].CCDesc.transform.position += vector;
		}
		this.BindPositions(new CCTotem.PositionPlacement(this.totemicObject.CCDesc.worldSkinnedBottom, this.totemicObject.CCDesc.worldSkinnedTop, this.totemicObject.CCDesc.transform.position, this.totemicObject.Configuration.poleExpandedHeight));
		return true;
	}

	// Token: 0x060017F2 RID: 6130 RVA: 0x0005D7C0 File Offset: 0x0005B9C0
	public void Teleport(Vector3 origin)
	{
		if (this.Exists)
		{
			base.ClearTotemicObject();
		}
		base.transform.position = origin;
		this.CreatePhysics();
	}

	// Token: 0x060017F3 RID: 6131 RVA: 0x0005D7F0 File Offset: 0x0005B9F0
	private void BindPositions(CCTotem.PositionPlacement PositionPlacement)
	{
		if (this.OnBindPosition != null)
		{
			try
			{
				this.OnBindPosition(ref PositionPlacement, this.Tag);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex, this);
			}
		}
	}

	// Token: 0x060017F4 RID: 6132 RVA: 0x0005D84C File Offset: 0x0005BA4C
	private new void OnDestroy()
	{
		try
		{
			base.OnDestroy();
		}
		finally
		{
			this.OnBindPosition = null;
			this.ConfigurationBinder = null;
			this.Tag = null;
		}
	}

	// Token: 0x04000CAE RID: 3246
	[SerializeField]
	private CCDesc prefab;

	// Token: 0x04000CAF RID: 3247
	[SerializeField]
	private float minimumHeight = 0.6f;

	// Token: 0x04000CB0 RID: 3248
	[SerializeField]
	private float maximumHeight = 2.08f;

	// Token: 0x04000CB1 RID: 3249
	[SerializeField]
	private float initialHeightFraction = 1f;

	// Token: 0x04000CB2 RID: 3250
	[SerializeField]
	private float bottomBufferUnits = 0.1f;

	// Token: 0x04000CB3 RID: 3251
	[NonSerialized]
	private bool HasLastGoodConfiguration;

	// Token: 0x04000CB4 RID: 3252
	[NonSerialized]
	private CCTotem.Configuration LastGoodConfiguration;

	// Token: 0x04000CB5 RID: 3253
	[NonSerialized]
	private new CCTotem.ConfigurationBinder ConfigurationBinder;

	// Token: 0x04000CB6 RID: 3254
	[NonSerialized]
	public ArgumentException LastException;

	// Token: 0x04000CB7 RID: 3255
	[NonSerialized]
	public object Tag;
}
