using System;
using System.Collections.Generic;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020003C0 RID: 960
public abstract class RigidObj : IDMain, global::IInterpTimedEventReceiver
{
	// Token: 0x060021AF RID: 8623 RVA: 0x0007C1B0 File Offset: 0x0007A3B0
	protected RigidObj(global::RigidObj.FeatureFlags classFeatures) : base(2)
	{
		this.featureFlags = classFeatures;
	}

	// Token: 0x060021B0 RID: 8624 RVA: 0x0007C1CC File Offset: 0x0007A3CC
	void global::IInterpTimedEventReceiver.OnInterpTimedEvent()
	{
		if (!this.OnInterpTimedEvent())
		{
			global::InterpTimedEvent.MarkUnhandled();
		}
	}

	// Token: 0x170007F5 RID: 2037
	// (get) Token: 0x060021B1 RID: 8625 RVA: 0x0007C1E0 File Offset: 0x0007A3E0
	public bool expectsInitialVelocity
	{
		get
		{
			return (byte)(this.featureFlags & global::RigidObj.FeatureFlags.StreamInitialVelocity) == 1;
		}
	}

	// Token: 0x170007F6 RID: 2038
	// (get) Token: 0x060021B2 RID: 8626 RVA: 0x0007C1F0 File Offset: 0x0007A3F0
	public bool expectsOwner
	{
		get
		{
			return (byte)(this.featureFlags & global::RigidObj.FeatureFlags.StreamOwnerViewID) == 2;
		}
	}

	// Token: 0x170007F7 RID: 2039
	// (get) Token: 0x060021B3 RID: 8627 RVA: 0x0007C200 File Offset: 0x0007A400
	public bool serverSideCollisions
	{
		get
		{
			return (byte)(this.featureFlags & global::RigidObj.FeatureFlags.ServerCollisions) == 128;
		}
	}

	// Token: 0x170007F8 RID: 2040
	// (get) Token: 0x060021B4 RID: 8628 RVA: 0x0007C218 File Offset: 0x0007A418
	// (set) Token: 0x060021B5 RID: 8629 RVA: 0x0007C224 File Offset: 0x0007A424
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

	// Token: 0x170007F9 RID: 2041
	// (get) Token: 0x060021B6 RID: 8630 RVA: 0x0007C264 File Offset: 0x0007A464
	public Facepunch.NetworkView ownerView
	{
		get
		{
			return (!this.__ownerView) ? (this.__ownerView = Facepunch.NetworkView.Find(this.ownerViewID)) : this.__ownerView;
		}
	}

	// Token: 0x060021B7 RID: 8631 RVA: 0x0007C2A0 File Offset: 0x0007A4A0
	protected void Awake()
	{
		this.rigidbody = base.rigidbody;
		this._interp = base.GetComponent<global::RigidbodyInterpolator>();
	}

	// Token: 0x060021B8 RID: 8632 RVA: 0x0007C2BC File Offset: 0x0007A4BC
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

	// Token: 0x060021B9 RID: 8633 RVA: 0x0007C30C File Offset: 0x0007A50C
	protected virtual void DoNetwork()
	{
		base.networkView.RPC("RecieveNetwork", 10, new object[]
		{
			this.rigidbody.position,
			this.rigidbody.rotation
		});
		this.serverLastUpdateTimestamp = global::NetCull.time;
	}

	// Token: 0x060021BA RID: 8634 RVA: 0x0007C364 File Offset: 0x0007A564
	[RPC]
	protected void RecieveNetwork(Vector3 pos, Quaternion rot, uLink.NetworkMessageInfo info)
	{
		if (this.hasInterp && this._interp)
		{
			global::PosRot frame;
			frame.position = pos;
			frame.rotation = rot;
			this.rigidbody.isKinematic = true;
			this._interp.SetGoals(frame, info.timestamp);
			this._interp.running = true;
		}
	}

	// Token: 0x060021BB RID: 8635 RVA: 0x0007C3C8 File Offset: 0x0007A5C8
	protected void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		this.view = (Facepunch.NetworkView)info.networkView;
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
		this.updateInterval = 1.0 / ((double)global::NetCull.sendRate * (double)Mathf.Max(1f, this.updateRate));
		this.hasInterp = this._interp;
		if (this.hasInterp)
		{
			this._interp.running = false;
		}
		this.rigidbody.isKinematic = true;
		this.__hiding = (this.spawnTime > global::Interpolation.time);
		if (this.__hiding)
		{
			this.OnHide();
			if (this.hasInterp)
			{
				global::PosRot frame;
				frame.position = this.view.position;
				frame.rotation = this.view.rotation;
				this._interp.SetGoals(frame, this.spawnTime);
			}
			global::InterpTimedEvent.Queue(this, "_init", ref info);
		}
		else
		{
			this.OnShow();
		}
	}

	// Token: 0x060021BC RID: 8636 RVA: 0x0007C508 File Offset: 0x0007A708
	[RPC]
	[Obsolete("Do not call manually")]
	protected void RODone(uLink.NetworkMessageInfo info)
	{
		if (!this.__done)
		{
			global::NetCull.DontDestroyWithNetwork(this);
			global::InterpTimedEvent.Queue(this, "_done", ref info);
		}
	}

	// Token: 0x060021BD RID: 8637
	protected abstract void OnHide();

	// Token: 0x060021BE RID: 8638
	protected abstract void OnShow();

	// Token: 0x060021BF RID: 8639
	protected abstract void OnDone();

	// Token: 0x060021C0 RID: 8640 RVA: 0x0007C52C File Offset: 0x0007A72C
	protected virtual void OnServerCollisionEnter(Collision collision)
	{
	}

	// Token: 0x060021C1 RID: 8641 RVA: 0x0007C530 File Offset: 0x0007A730
	protected virtual void OnServerCollisionStay(Collision collision)
	{
	}

	// Token: 0x060021C2 RID: 8642 RVA: 0x0007C534 File Offset: 0x0007A734
	protected virtual void OnServerCollisionExit(Collision collision)
	{
	}

	// Token: 0x060021C3 RID: 8643 RVA: 0x0007C538 File Offset: 0x0007A738
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

	// Token: 0x060021C4 RID: 8644 RVA: 0x0007C588 File Offset: 0x0007A788
	protected virtual bool OnInterpTimedEvent()
	{
		string tag = global::InterpTimedEvent.Tag;
		if (tag != null)
		{
			if (global::RigidObj.<>f__switch$map5 == null)
			{
				global::RigidObj.<>f__switch$map5 = new Dictionary<string, int>(2)
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
			if (global::RigidObj.<>f__switch$map5.TryGetValue(tag, out num))
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

	// Token: 0x04000FEA RID: 4074
	private const string kDoNetworkMethodName = "__invoke_do_network";

	// Token: 0x04000FEB RID: 4075
	[NonSerialized]
	public Rigidbody rigidbody;

	// Token: 0x04000FEC RID: 4076
	[NonSerialized]
	protected readonly global::RigidObj.FeatureFlags featureFlags;

	// Token: 0x04000FED RID: 4077
	[SerializeField]
	private float updateRate = 2f;

	// Token: 0x04000FEE RID: 4078
	private double updateInterval;

	// Token: 0x04000FEF RID: 4079
	private double serverLastUpdateTimestamp;

	// Token: 0x04000FF0 RID: 4080
	protected Facepunch.NetworkView view;

	// Token: 0x04000FF1 RID: 4081
	private global::RigidbodyInterpolator _interp;

	// Token: 0x04000FF2 RID: 4082
	private global::RigidObjServerCollision _serverCollision;

	// Token: 0x04000FF3 RID: 4083
	private bool hasInterp;

	// Token: 0x04000FF4 RID: 4084
	private bool __hiding;

	// Token: 0x04000FF5 RID: 4085
	private bool __done;

	// Token: 0x04000FF6 RID: 4086
	private bool __calling_from_do_network;

	// Token: 0x04000FF7 RID: 4087
	protected Vector3 initialVelocity;

	// Token: 0x04000FF8 RID: 4088
	protected double spawnTime;

	// Token: 0x04000FF9 RID: 4089
	protected uLink.NetworkViewID ownerViewID;

	// Token: 0x04000FFA RID: 4090
	private Facepunch.NetworkView __ownerView;

	// Token: 0x020003C1 RID: 961
	[Flags]
	protected enum FeatureFlags : byte
	{
		// Token: 0x04000FFD RID: 4093
		StreamInitialVelocity = 1,
		// Token: 0x04000FFE RID: 4094
		StreamOwnerViewID = 2,
		// Token: 0x04000FFF RID: 4095
		ServerCollisions = 128
	}
}
