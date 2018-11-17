using System;
using System.Collections.Generic;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000317 RID: 791
public abstract class RigidObj : IDMain, IInterpTimedEventReceiver
{
	// Token: 0x06001E6D RID: 7789 RVA: 0x00077730 File Offset: 0x00075930
	protected RigidObj(RigidObj.FeatureFlags classFeatures) : base(2)
	{
		this.featureFlags = classFeatures;
	}

	// Token: 0x06001E6E RID: 7790 RVA: 0x0007774C File Offset: 0x0007594C
	void IInterpTimedEventReceiver.OnInterpTimedEvent()
	{
		if (!this.OnInterpTimedEvent())
		{
			InterpTimedEvent.MarkUnhandled();
		}
	}

	// Token: 0x1700079F RID: 1951
	// (get) Token: 0x06001E6F RID: 7791 RVA: 0x00077760 File Offset: 0x00075960
	public bool expectsInitialVelocity
	{
		get
		{
			return (byte)(this.featureFlags & RigidObj.FeatureFlags.StreamInitialVelocity) == 1;
		}
	}

	// Token: 0x170007A0 RID: 1952
	// (get) Token: 0x06001E70 RID: 7792 RVA: 0x00077770 File Offset: 0x00075970
	public bool expectsOwner
	{
		get
		{
			return (byte)(this.featureFlags & RigidObj.FeatureFlags.StreamOwnerViewID) == 2;
		}
	}

	// Token: 0x170007A1 RID: 1953
	// (get) Token: 0x06001E71 RID: 7793 RVA: 0x00077780 File Offset: 0x00075980
	public bool serverSideCollisions
	{
		get
		{
			return (byte)(this.featureFlags & RigidObj.FeatureFlags.ServerCollisions) == 128;
		}
	}

	// Token: 0x170007A2 RID: 1954
	// (get) Token: 0x06001E72 RID: 7794 RVA: 0x00077798 File Offset: 0x00075998
	// (set) Token: 0x06001E73 RID: 7795 RVA: 0x000777A4 File Offset: 0x000759A4
	public bool showing
	{
		get
		{
			return !this.__hiding;
		}
		protected set
		{
			if (this.__hiding == value)
			{
				this.__hiding = !value;
				if (this.__hiding)
				{
					this.OnHide();
				}
				else
				{
					this.OnShow();
				}
			}
		}
	}

	// Token: 0x170007A3 RID: 1955
	// (get) Token: 0x06001E74 RID: 7796 RVA: 0x000777E4 File Offset: 0x000759E4
	public NetworkView ownerView
	{
		get
		{
			return (!this.__ownerView) ? (this.__ownerView = NetworkView.Find(this.ownerViewID)) : this.__ownerView;
		}
	}

	// Token: 0x06001E75 RID: 7797 RVA: 0x00077820 File Offset: 0x00075A20
	protected void Awake()
	{
		this.rigidbody = base.rigidbody;
		this._interp = base.GetComponent<RigidbodyInterpolator>();
	}

	// Token: 0x06001E76 RID: 7798 RVA: 0x0007783C File Offset: 0x00075A3C
	private void __invoke_do_network()
	{
		if (this.__calling_from_do_network)
		{
			return;
		}
		try
		{
			this.__calling_from_do_network = true;
			this.DoNetwork();
		}
		finally
		{
			this.__calling_from_do_network = false;
		}
	}

	// Token: 0x06001E77 RID: 7799 RVA: 0x0007788C File Offset: 0x00075A8C
	protected virtual void DoNetwork()
	{
		base.networkView.RPC("RecieveNetwork", 10, new object[]
		{
			this.rigidbody.position,
			this.rigidbody.rotation
		});
		this.serverLastUpdateTimestamp = NetCull.time;
	}

	// Token: 0x06001E78 RID: 7800 RVA: 0x000778E4 File Offset: 0x00075AE4
	[RPC]
	protected void RecieveNetwork(Vector3 pos, Quaternion rot, NetworkMessageInfo info)
	{
		if (this.hasInterp && this._interp)
		{
			PosRot frame;
			frame.position = pos;
			frame.rotation = rot;
			this.rigidbody.isKinematic = true;
			this._interp.SetGoals(frame, info.timestamp);
			this._interp.running = true;
		}
	}

	// Token: 0x06001E79 RID: 7801 RVA: 0x00077948 File Offset: 0x00075B48
	protected void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		this.view = (NetworkView)info.networkView;
		BitStream initialData = this.view.initialData;
		if (this.expectsInitialVelocity)
		{
			this.initialVelocity = initialData.ReadVector3();
		}
		if (this.expectsOwner)
		{
			this.ownerViewID = initialData.ReadNetworkViewID();
		}
		this.spawnTime = info.timestamp;
		this.updateInterval = 1.0 / ((double)NetCull.sendRate * (double)Mathf.Max(1f, this.updateRate));
		this.hasInterp = this._interp;
		if (this.hasInterp)
		{
			this._interp.running = false;
		}
		this.rigidbody.isKinematic = true;
		this.__hiding = (this.spawnTime > Interpolation.time);
		if (this.__hiding)
		{
			this.OnHide();
			if (this.hasInterp)
			{
				PosRot frame;
				frame.position = this.view.position;
				frame.rotation = this.view.rotation;
				this._interp.SetGoals(frame, this.spawnTime);
			}
			InterpTimedEvent.Queue(this, "_init", ref info);
		}
		else
		{
			this.OnShow();
		}
	}

	// Token: 0x06001E7A RID: 7802 RVA: 0x00077A88 File Offset: 0x00075C88
	[RPC]
	[Obsolete("Do not call manually")]
	protected void RODone(NetworkMessageInfo info)
	{
		if (!this.__done)
		{
			NetCull.DontDestroyWithNetwork(this);
			InterpTimedEvent.Queue(this, "_done", ref info);
		}
	}

	// Token: 0x06001E7B RID: 7803
	protected abstract void OnHide();

	// Token: 0x06001E7C RID: 7804
	protected abstract void OnShow();

	// Token: 0x06001E7D RID: 7805
	protected abstract void OnDone();

	// Token: 0x06001E7E RID: 7806 RVA: 0x00077AAC File Offset: 0x00075CAC
	protected virtual void OnServerCollisionEnter(Collision collision)
	{
	}

	// Token: 0x06001E7F RID: 7807 RVA: 0x00077AB0 File Offset: 0x00075CB0
	protected virtual void OnServerCollisionStay(Collision collision)
	{
	}

	// Token: 0x06001E80 RID: 7808 RVA: 0x00077AB4 File Offset: 0x00075CB4
	protected virtual void OnServerCollisionExit(Collision collision)
	{
	}

	// Token: 0x06001E81 RID: 7809 RVA: 0x00077AB8 File Offset: 0x00075CB8
	internal void OnServerCollision(byte kind, Collision collision)
	{
		switch (kind)
		{
		case 0:
			this.OnServerCollisionEnter(collision);
			break;
		case 1:
			this.OnServerCollisionExit(collision);
			break;
		case 2:
			this.OnServerCollisionStay(collision);
			break;
		default:
			throw new NotImplementedException();
		}
	}

	// Token: 0x06001E82 RID: 7810 RVA: 0x00077B08 File Offset: 0x00075D08
	protected virtual bool OnInterpTimedEvent()
	{
		string tag = InterpTimedEvent.Tag;
		if (tag != null)
		{
			if (RigidObj.<>f__switch$map5 == null)
			{
				RigidObj.<>f__switch$map5 = new Dictionary<string, int>(2)
				{
					{
						"_init",
						0
					},
					{
						"_done",
						1
					}
				};
			}
			int num;
			if (RigidObj.<>f__switch$map5.TryGetValue(tag, out num))
			{
				if (num == 0)
				{
					this.showing = true;
					if (this.expectsInitialVelocity)
					{
						this.rigidbody.isKinematic = false;
						this.rigidbody.velocity = this.initialVelocity;
					}
					return true;
				}
				if (num == 1)
				{
					try
					{
						this.OnDone();
					}
					finally
					{
						try
						{
							this.showing = false;
						}
						finally
						{
							Object.Destroy(base.gameObject);
						}
					}
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x04000EAA RID: 3754
	private const string kDoNetworkMethodName = "__invoke_do_network";

	// Token: 0x04000EAB RID: 3755
	[NonSerialized]
	public Rigidbody rigidbody;

	// Token: 0x04000EAC RID: 3756
	[NonSerialized]
	protected readonly RigidObj.FeatureFlags featureFlags;

	// Token: 0x04000EAD RID: 3757
	[SerializeField]
	private float updateRate = 2f;

	// Token: 0x04000EAE RID: 3758
	private double updateInterval;

	// Token: 0x04000EAF RID: 3759
	private double serverLastUpdateTimestamp;

	// Token: 0x04000EB0 RID: 3760
	protected NetworkView view;

	// Token: 0x04000EB1 RID: 3761
	private RigidbodyInterpolator _interp;

	// Token: 0x04000EB2 RID: 3762
	private RigidObjServerCollision _serverCollision;

	// Token: 0x04000EB3 RID: 3763
	private bool hasInterp;

	// Token: 0x04000EB4 RID: 3764
	private bool __hiding;

	// Token: 0x04000EB5 RID: 3765
	private bool __done;

	// Token: 0x04000EB6 RID: 3766
	private bool __calling_from_do_network;

	// Token: 0x04000EB7 RID: 3767
	protected Vector3 initialVelocity;

	// Token: 0x04000EB8 RID: 3768
	protected double spawnTime;

	// Token: 0x04000EB9 RID: 3769
	protected NetworkViewID ownerViewID;

	// Token: 0x04000EBA RID: 3770
	private NetworkView __ownerView;

	// Token: 0x02000318 RID: 792
	[Flags]
	protected enum FeatureFlags : byte
	{
		// Token: 0x04000EBD RID: 3773
		StreamInitialVelocity = 1,
		// Token: 0x04000EBE RID: 3774
		StreamOwnerViewID = 2,
		// Token: 0x04000EBF RID: 3775
		ServerCollisions = 128
	}
}
