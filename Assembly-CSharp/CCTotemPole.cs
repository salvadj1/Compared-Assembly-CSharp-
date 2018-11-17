using System;
using UnityEngine;

// Token: 0x020002DA RID: 730
public sealed class CCTotemPole : global::CCTotem<global::CCTotem.TotemPole, global::CCTotemPole>
{
	// Token: 0x1400000D RID: 13
	// (add) Token: 0x06001968 RID: 6504 RVA: 0x000619D8 File Offset: 0x0005FBD8
	// (remove) Token: 0x06001969 RID: 6505 RVA: 0x000619F4 File Offset: 0x0005FBF4
	public event global::CCTotem.PositionBinder OnBindPosition;

	// Token: 0x1400000E RID: 14
	// (add) Token: 0x0600196A RID: 6506 RVA: 0x00061A10 File Offset: 0x0005FC10
	// (remove) Token: 0x0600196B RID: 6507 RVA: 0x00061A5C File Offset: 0x0005FC5C
	public event global::CCTotem.ConfigurationBinder OnConfigurationBinding
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

	// Token: 0x1700073F RID: 1855
	// (get) Token: 0x0600196C RID: 6508 RVA: 0x00061A9C File Offset: 0x0005FC9C
	public bool Exists
	{
		get
		{
			return !object.ReferenceEquals(this.totemicObject, null);
		}
	}

	// Token: 0x17000740 RID: 1856
	// (get) Token: 0x0600196D RID: 6509 RVA: 0x00061AB0 File Offset: 0x0005FCB0
	public float MinimumHeight
	{
		get
		{
			return this.minimumHeight;
		}
	}

	// Token: 0x17000741 RID: 1857
	// (get) Token: 0x0600196E RID: 6510 RVA: 0x00061AB8 File Offset: 0x0005FCB8
	public float MaximumHeight
	{
		get
		{
			return this.maximumHeight;
		}
	}

	// Token: 0x17000742 RID: 1858
	// (get) Token: 0x0600196F RID: 6511 RVA: 0x00061AC0 File Offset: 0x0005FCC0
	public float Height
	{
		get
		{
			return (!this.Exists) ? (this.minimumHeight + this.initialHeightFraction * (this.maximumHeight - this.minimumHeight)) : this.totemicObject.Expansion.Value;
		}
	}

	// Token: 0x17000743 RID: 1859
	// (get) Token: 0x06001970 RID: 6512 RVA: 0x00061B00 File Offset: 0x0005FD00
	private global::CCTotem.Initialization Members
	{
		get
		{
			return new global::CCTotem.Initialization(this, this.prefab, this.minimumHeight, this.maximumHeight, this.minimumHeight + (this.maximumHeight - this.minimumHeight) * this.initialHeightFraction, this.bottomBufferUnits);
		}
	}

	// Token: 0x17000744 RID: 1860
	// (get) Token: 0x06001971 RID: 6513 RVA: 0x00061B3C File Offset: 0x0005FD3C
	public bool isGrounded
	{
		get
		{
			return this.Exists && this.totemicObject.isGrounded;
		}
	}

	// Token: 0x17000745 RID: 1861
	// (get) Token: 0x06001972 RID: 6514 RVA: 0x00061B58 File Offset: 0x0005FD58
	public Vector3 velocity
	{
		get
		{
			return (!this.Exists) ? Vector3.zero : this.totemicObject.velocity;
		}
	}

	// Token: 0x17000746 RID: 1862
	// (get) Token: 0x06001973 RID: 6515 RVA: 0x00061B88 File Offset: 0x0005FD88
	public CollisionFlags collisionFlags
	{
		get
		{
			return (!this.Exists) ? 0 : this.totemicObject.collisionFlags;
		}
	}

	// Token: 0x17000747 RID: 1863
	// (get) Token: 0x06001974 RID: 6516 RVA: 0x00061BA8 File Offset: 0x0005FDA8
	public float stepOffset
	{
		get
		{
			return (!this.Exists) ? this.prefab.stepOffset : this.totemicObject.stepOffset;
		}
	}

	// Token: 0x17000748 RID: 1864
	// (get) Token: 0x06001975 RID: 6517 RVA: 0x00061BDC File Offset: 0x0005FDDC
	public float slopeLimit
	{
		get
		{
			return (!this.Exists) ? this.prefab.slopeLimit : this.totemicObject.slopeLimit;
		}
	}

	// Token: 0x17000749 RID: 1865
	// (get) Token: 0x06001976 RID: 6518 RVA: 0x00061C10 File Offset: 0x0005FE10
	public Vector3 center
	{
		get
		{
			return (!this.Exists) ? this.prefab.center : this.totemicObject.center;
		}
	}

	// Token: 0x1700074A RID: 1866
	// (get) Token: 0x06001977 RID: 6519 RVA: 0x00061C44 File Offset: 0x0005FE44
	[Obsolete("this is the height of the character controller. prefer this.Height")]
	public float height
	{
		get
		{
			return (!this.Exists) ? this.prefab.height : this.totemicObject.height;
		}
	}

	// Token: 0x1700074B RID: 1867
	// (get) Token: 0x06001978 RID: 6520 RVA: 0x00061C78 File Offset: 0x0005FE78
	public float radius
	{
		get
		{
			return (!this.Exists) ? this.prefab.radius : this.totemicObject.radius;
		}
	}

	// Token: 0x06001979 RID: 6521 RVA: 0x00061CAC File Offset: 0x0005FEAC
	public bool UpdateConfiguration()
	{
		this.LastException = null;
		global::CCTotem.Initialization members = this.Members;
		bool result;
		try
		{
			this.LastGoodConfiguration = new global::CCTotem.Configuration(ref members);
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

	// Token: 0x0600197A RID: 6522 RVA: 0x00061D18 File Offset: 0x0005FF18
	private void Awake()
	{
		if (!this.UpdateConfiguration())
		{
			Debug.LogException(this.LastException, this);
			return;
		}
		this.CreatePhysics();
	}

	// Token: 0x0600197B RID: 6523 RVA: 0x00061D38 File Offset: 0x0005FF38
	private void CreatePhysics()
	{
		if (!this.HasLastGoodConfiguration && !this.UpdateConfiguration())
		{
			Debug.LogException(this.LastException, this);
			return;
		}
		base.AssignTotemicObject(new global::CCTotem.TotemPole(ref this.LastGoodConfiguration));
		this.totemicObject.Create();
	}

	// Token: 0x0600197C RID: 6524 RVA: 0x00061D84 File Offset: 0x0005FF84
	internal void DestroyCCDesc(ref global::CCDesc CCDesc)
	{
		if (CCDesc)
		{
			global::CCDesc ccdesc = CCDesc;
			CCDesc = null;
			this.ExecuteBinding(ccdesc, false);
			Object.Destroy(ccdesc.gameObject);
		}
	}

	// Token: 0x0600197D RID: 6525 RVA: 0x00061DB8 File Offset: 0x0005FFB8
	internal void ExecuteBinding(global::CCDesc CCDesc, bool Bind)
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

	// Token: 0x0600197E RID: 6526 RVA: 0x00061E24 File Offset: 0x00060024
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

	// Token: 0x0600197F RID: 6527 RVA: 0x00061E8C File Offset: 0x0006008C
	public global::CCTotem.MoveInfo Move(Vector3 motion)
	{
		return this.Move(motion, this.Height);
	}

	// Token: 0x06001980 RID: 6528 RVA: 0x00061E9C File Offset: 0x0006009C
	public global::CCTotem.MoveInfo Move(Vector3 motion, float height)
	{
		global::CCTotem.TotemPole totemicObject = this.totemicObject;
		if (object.ReferenceEquals(totemicObject, null))
		{
			throw new InvalidOperationException("Exists == false");
		}
		global::CCTotem.MoveInfo result = totemicObject.Move(motion, height);
		this.BindPositions(result.PositionPlacement);
		return result;
	}

	// Token: 0x06001981 RID: 6529 RVA: 0x00061EE0 File Offset: 0x000600E0
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
		global::CCDesc ccdesc = this.totemicObject.CCDesc;
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
		this.BindPositions(new global::CCTotem.PositionPlacement(this.totemicObject.CCDesc.worldSkinnedBottom, this.totemicObject.CCDesc.worldSkinnedTop, this.totemicObject.CCDesc.transform.position, this.totemicObject.Configuration.poleExpandedHeight));
		return true;
	}

	// Token: 0x06001982 RID: 6530 RVA: 0x00062134 File Offset: 0x00060334
	public void Teleport(Vector3 origin)
	{
		if (this.Exists)
		{
			base.ClearTotemicObject();
		}
		base.transform.position = origin;
		this.CreatePhysics();
	}

	// Token: 0x06001983 RID: 6531 RVA: 0x00062164 File Offset: 0x00060364
	private void BindPositions(global::CCTotem.PositionPlacement PositionPlacement)
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

	// Token: 0x06001984 RID: 6532 RVA: 0x000621C0 File Offset: 0x000603C0
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

	// Token: 0x04000DE9 RID: 3561
	[SerializeField]
	private global::CCDesc prefab;

	// Token: 0x04000DEA RID: 3562
	[SerializeField]
	private float minimumHeight = 0.6f;

	// Token: 0x04000DEB RID: 3563
	[SerializeField]
	private float maximumHeight = 2.08f;

	// Token: 0x04000DEC RID: 3564
	[SerializeField]
	private float initialHeightFraction = 1f;

	// Token: 0x04000DED RID: 3565
	[SerializeField]
	private float bottomBufferUnits = 0.1f;

	// Token: 0x04000DEE RID: 3566
	[NonSerialized]
	private bool HasLastGoodConfiguration;

	// Token: 0x04000DEF RID: 3567
	[NonSerialized]
	private global::CCTotem.Configuration LastGoodConfiguration;

	// Token: 0x04000DF0 RID: 3568
	[NonSerialized]
	private new global::CCTotem.ConfigurationBinder ConfigurationBinder;

	// Token: 0x04000DF1 RID: 3569
	[NonSerialized]
	public ArgumentException LastException;

	// Token: 0x04000DF2 RID: 3570
	[NonSerialized]
	public object Tag;
}
