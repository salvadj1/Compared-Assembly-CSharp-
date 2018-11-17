using System;
using System.Collections.Generic;
using Facepunch;
using Facepunch.Movement;
using uLink;
using UnityEngine;

// Token: 0x020005AA RID: 1450
[RequireComponent(typeof(uLinkNetworkView))]
public class ItemRepresentation : IDMain, IInterpTimedEventReceiver
{
	// Token: 0x06003490 RID: 13456 RVA: 0x000BFDC0 File Offset: 0x000BDFC0
	public ItemRepresentation() : base(2)
	{
		this.stateSignalReceive = new CharacterStateSignal(this.StateSignalReceive);
	}

	// Token: 0x06003491 RID: 13457 RVA: 0x000BFDDC File Offset: 0x000BDFDC
	void IInterpTimedEventReceiver.OnInterpTimedEvent()
	{
		this.OnInterpTimedEvent();
	}

	// Token: 0x17000A5A RID: 2650
	// (get) Token: 0x06003492 RID: 13458 RVA: 0x000BFDE4 File Offset: 0x000BDFE4
	public ItemModFlags modFlags
	{
		get
		{
			return this._modFlags;
		}
	}

	// Token: 0x17000A5B RID: 2651
	// (get) Token: 0x06003493 RID: 13459 RVA: 0x000BFDEC File Offset: 0x000BDFEC
	public HeldItemDataBlock datablock
	{
		get
		{
			return this._datablock;
		}
	}

	// Token: 0x06003494 RID: 13460 RVA: 0x000BFDF4 File Offset: 0x000BDFF4
	private void BindModAsLocal(ref ItemRepresentation.ItemModPair pair, ref ModViewModelAddArgs a)
	{
		if ((int)pair.bindState == 2)
		{
			this.UnBindModAsProxy(ref pair);
		}
		if ((int)pair.bindState == 1 || (int)pair.bindState == 3)
		{
			a.modRep = pair.representation;
			pair.dataBlock.BindAsLocal(ref a);
			pair.bindState = ItemRepresentation.BindState.Local;
		}
	}

	// Token: 0x06003495 RID: 13461 RVA: 0x000BFE50 File Offset: 0x000BE050
	private void UnBindModAsLocal(ref ItemRepresentation.ItemModPair pair, ref ModViewModelRemoveArgs a)
	{
		if ((int)pair.bindState == 3)
		{
			a.modRep = pair.representation;
			pair.dataBlock.UnBindAsLocal(ref a);
			pair.bindState = ItemRepresentation.BindState.None;
		}
	}

	// Token: 0x06003496 RID: 13462 RVA: 0x000BFE8C File Offset: 0x000BE08C
	private void BindModAsProxy(ref ItemRepresentation.ItemModPair pair)
	{
		if ((int)pair.bindState == 1)
		{
			pair.dataBlock.BindAsProxy(pair.representation);
			pair.bindState = ItemRepresentation.BindState.World;
		}
	}

	// Token: 0x06003497 RID: 13463 RVA: 0x000BFEB4 File Offset: 0x000BE0B4
	private void UnBindModAsProxy(ref ItemRepresentation.ItemModPair pair)
	{
		if ((int)pair.bindState == 2)
		{
			pair.dataBlock.UnBindAsProxy(pair.representation);
			pair.bindState = ItemRepresentation.BindState.None;
		}
	}

	// Token: 0x06003498 RID: 13464 RVA: 0x000BFEDC File Offset: 0x000BE0DC
	protected void Awake()
	{
	}

	// Token: 0x17000A5C RID: 2652
	// (get) Token: 0x06003499 RID: 13465 RVA: 0x000BFEE0 File Offset: 0x000BE0E0
	public string worldAnimationGroupName
	{
		get
		{
			return this.worldAnimationGroupNameOverride ?? this.datablock.animationGroupName;
		}
	}

	// Token: 0x0600349A RID: 13466 RVA: 0x000BFEFC File Offset: 0x000BE0FC
	public bool PlayWorldAnimation(GroupEvent GroupEvent, float speed, float animationTime)
	{
		if (this._characterSignalee)
		{
			PlayerAnimation component = this._characterSignalee.GetComponent<PlayerAnimation>();
			if (component)
			{
				return component.PlayAnimation(GroupEvent, speed, animationTime);
			}
		}
		return false;
	}

	// Token: 0x0600349B RID: 13467 RVA: 0x000BFF3C File Offset: 0x000BE13C
	public bool PlayWorldAnimation(GroupEvent GroupEvent, float speed)
	{
		if (this._characterSignalee)
		{
			PlayerAnimation component = this._characterSignalee.GetComponent<PlayerAnimation>();
			if (component)
			{
				return component.PlayAnimation(GroupEvent, speed);
			}
		}
		return false;
	}

	// Token: 0x0600349C RID: 13468 RVA: 0x000BFF7C File Offset: 0x000BE17C
	public bool PlayWorldAnimation(GroupEvent GroupEvent)
	{
		if (this._characterSignalee)
		{
			PlayerAnimation component = this._characterSignalee.GetComponent<PlayerAnimation>();
			if (component)
			{
				return component.PlayAnimation(GroupEvent);
			}
		}
		return false;
	}

	// Token: 0x0600349D RID: 13469 RVA: 0x000BFFBC File Offset: 0x000BE1BC
	public bool OverrideAnimationGroupName(string newGroupName)
	{
		if (string.IsNullOrEmpty(newGroupName))
		{
			newGroupName = null;
		}
		if (this.worldAnimationGroupNameOverride != newGroupName)
		{
			if (this._holder)
			{
				this._holder.ClearItemRepresentation(this);
				this.worldAnimationGroupNameOverride = newGroupName;
				this._holder.SetItemRepresentation(this);
			}
			else
			{
				this.worldAnimationGroupNameOverride = newGroupName;
			}
			return true;
		}
		return false;
	}

	// Token: 0x0600349E RID: 13470 RVA: 0x000C0028 File Offset: 0x000BE228
	protected void OnDrawGizmosSelected()
	{
		this.muzzle.DrawGizmos("muzzle");
	}

	// Token: 0x0600349F RID: 13471 RVA: 0x000C003C File Offset: 0x000BE23C
	private void KillModRep(ref ItemModRepresentation rep, bool fromCallback)
	{
		if (!fromCallback && rep)
		{
			ItemModRepresentation itemModRepresentation = this.destroyingRep;
			try
			{
				this.destroyingRep = rep;
				Object.Destroy(rep);
			}
			finally
			{
				this.destroyingRep = itemModRepresentation;
			}
		}
		rep = null;
	}

	// Token: 0x060034A0 RID: 13472 RVA: 0x000C00A0 File Offset: 0x000BE2A0
	protected void OnDestroy()
	{
		try
		{
			InterpTimedEvent.Remove(this, true);
			this.ClearMods();
		}
		finally
		{
			this._parentViewID = NetworkViewID.unassigned;
			this.ClearSignals();
			base.OnDestroy();
		}
	}

	// Token: 0x060034A1 RID: 13473 RVA: 0x000C00F4 File Offset: 0x000BE2F4
	public virtual void SetParent(GameObject parentGameObject)
	{
		Transform transform = parentGameObject.transform;
		if (!base.transform.IsChildOf(transform))
		{
			base.transform.parent = transform;
		}
	}

	// Token: 0x060034A2 RID: 13474 RVA: 0x000C0128 File Offset: 0x000BE328
	protected bool CheckParent()
	{
		if (this._parentView)
		{
			return true;
		}
		if (this._parentViewID != NetworkViewID.unassigned)
		{
			this._parentView = NetworkView.Find(this._parentViewID);
			if (this._parentView)
			{
				this._parentMain = null;
				PlayerAnimation component = this._parentView.GetComponent<PlayerAnimation>();
				Socket.LocalSpace itemAttachment = component.itemAttachment;
				if (itemAttachment != null)
				{
					Vector3 offsetFromThisSocket;
					Quaternion rotationOffsetFromThisSocket;
					if (this.hand.parent && this.hand.parent != base.transform)
					{
						offsetFromThisSocket = base.transform.InverseTransformPoint(this.hand.position);
						Quaternion rotation = this.hand.rotation;
						Vector3 vector = rotation * Vector3.forward;
						Vector3 vector2 = rotation * Vector3.up;
						vector = base.transform.InverseTransformDirection(vector);
						vector2 = base.transform.InverseTransformDirection(vector2);
						rotationOffsetFromThisSocket = Quaternion.LookRotation(vector, vector2);
					}
					else
					{
						offsetFromThisSocket = this.hand.offset;
						rotationOffsetFromThisSocket = Quaternion.Euler(this.hand.eulerRotate);
					}
					itemAttachment.AddChildWithCoords(base.transform, offsetFromThisSocket, rotationOffsetFromThisSocket);
				}
				if (base.networkView.isMine)
				{
					this.worldModels = actor.forceThirdPerson;
				}
				this.FindSignalee();
				return true;
			}
		}
		this.ClearSignals();
		return false;
	}

	// Token: 0x060034A3 RID: 13475 RVA: 0x000C0290 File Offset: 0x000BE490
	protected void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		this._parentViewID = info.networkView.initialData.ReadNetworkViewID();
		int uniqueID = info.networkView.initialData.ReadInt32();
		this._datablock = (HeldItemDataBlock)DatablockDictionary.GetByUniqueID(uniqueID);
		if (!this.CheckParent())
		{
			Debug.Log("No parent for item rep (yet)", this);
		}
	}

	// Token: 0x060034A4 RID: 13476 RVA: 0x000C02EC File Offset: 0x000BE4EC
	[RPC]
	protected void Mods(byte[] data)
	{
		this.ClearMods();
		BitStream bitStream = new BitStream(data, false);
		byte b = bitStream.ReadByte();
		if (b > 0)
		{
			CharacterStateFlags characterStateFlags = this.GetCharacterStateFlags();
			for (int i = 0; i < (int)b; i++)
			{
				int uniqueID = bitStream.ReadInt32();
				ItemModDataBlock itemModDataBlock = (ItemModDataBlock)DatablockDictionary.GetByUniqueID(uniqueID);
				this._itemMods.InstallMod(i, this, itemModDataBlock, characterStateFlags);
				this._modFlags |= itemModDataBlock.modFlag;
			}
		}
	}

	// Token: 0x17000A5D RID: 2653
	// (get) Token: 0x060034A5 RID: 13477 RVA: 0x000C036C File Offset: 0x000BE56C
	// (set) Token: 0x060034A6 RID: 13478 RVA: 0x000C0378 File Offset: 0x000BE578
	public bool worldModels
	{
		get
		{
			return !this.worldStateDisabled;
		}
		set
		{
			if (this.worldStateDisabled == value)
			{
				this.worldStateDisabled = !this.worldStateDisabled;
				if (this.visuals != null)
				{
					for (int i = 0; i < this.visuals.Length; i++)
					{
						if (this.visuals[i])
						{
							this.visuals[i].SetActive(value);
						}
					}
				}
				if (base.renderer)
				{
					base.renderer.enabled = value;
				}
				if (value)
				{
					for (int j = 0; j < 5; j++)
					{
						this._itemMods.BindAsProxy(j, this);
					}
				}
				else
				{
					for (int k = 0; k < 5; k++)
					{
						this._itemMods.UnBindAsProxy(k, this);
					}
				}
			}
		}
	}

	// Token: 0x060034A7 RID: 13479 RVA: 0x000C044C File Offset: 0x000BE64C
	protected virtual void StateSignalReceive(Character character, bool treatedAsFirst)
	{
		CharacterStateFlags stateFlags = character.stateFlags;
		if (this.lastCharacterStateFlags != null && this.lastCharacterStateFlags.Value.Equals(stateFlags))
		{
			return;
		}
		this.lastCharacterStateFlags = new CharacterStateFlags?(stateFlags);
		for (int i = 0; i < 5; i++)
		{
			if (this._itemMods[i].representation)
			{
				this._itemMods[i].representation.HandleChangedStateFlags(stateFlags, !treatedAsFirst);
			}
		}
	}

	// Token: 0x060034A8 RID: 13480 RVA: 0x000C04E8 File Offset: 0x000BE6E8
	[RPC]
	protected void InterpDestroy(NetworkMessageInfo info)
	{
		if (base.networkView && base.networkView.isMine)
		{
			InterpTimedEvent.Remove(this, true);
		}
		else
		{
			InterpTimedEvent.Queue(this, "InterpDestroy", ref info);
			NetCull.DontDestroyWithNetwork(this);
		}
	}

	// Token: 0x060034A9 RID: 13481 RVA: 0x000C0538 File Offset: 0x000BE738
	private static string ActionRPC(int number)
	{
		switch (number)
		{
		case 1:
			return "Action1";
		case 2:
			return "Action2";
		case 3:
			return "Action3";
		default:
			throw new ArgumentOutOfRangeException("number", number, "number must be at or between 1 and 3");
		}
	}

	// Token: 0x060034AA RID: 13482 RVA: 0x000C0588 File Offset: 0x000BE788
	private static string ActionRPCBitstream(int number)
	{
		switch (number)
		{
		case 1:
			return "Action1B";
		case 2:
			return "Action2B";
		case 3:
			return "Action3B";
		default:
			throw new ArgumentOutOfRangeException("number", number, "number must be at or between 1 and 3");
		}
	}

	// Token: 0x060034AB RID: 13483 RVA: 0x000C05D8 File Offset: 0x000BE7D8
	public void Action(int number, RPCMode mode)
	{
		base.networkView.RPC(ItemRepresentation.ActionRPC(number), mode, new object[0]);
	}

	// Token: 0x060034AC RID: 13484 RVA: 0x000C05F4 File Offset: 0x000BE7F4
	public void Action<T>(int number, RPCMode mode, T argument)
	{
		base.networkView.RPC<T>(ItemRepresentation.ActionRPC(number), mode, argument);
	}

	// Token: 0x060034AD RID: 13485 RVA: 0x000C060C File Offset: 0x000BE80C
	public void Action(int number, RPCMode mode, params object[] arguments)
	{
		base.networkView.RPC(ItemRepresentation.ActionRPC(number), mode, arguments);
	}

	// Token: 0x060034AE RID: 13486 RVA: 0x000C0624 File Offset: 0x000BE824
	public void Action(int number, NetworkPlayer target)
	{
		base.networkView.RPC(ItemRepresentation.ActionRPC(number), target, new object[0]);
	}

	// Token: 0x060034AF RID: 13487 RVA: 0x000C0640 File Offset: 0x000BE840
	public void Action<T>(int number, NetworkPlayer target, T argument)
	{
		base.networkView.RPC<T>(ItemRepresentation.ActionRPC(number), target, argument);
	}

	// Token: 0x060034B0 RID: 13488 RVA: 0x000C0658 File Offset: 0x000BE858
	public void Action(int number, NetworkPlayer target, params object[] arguments)
	{
		base.networkView.RPC(ItemRepresentation.ActionRPC(number), target, arguments);
	}

	// Token: 0x060034B1 RID: 13489 RVA: 0x000C0670 File Offset: 0x000BE870
	public void Action(int number, IEnumerable<NetworkPlayer> targets)
	{
		base.networkView.RPC(ItemRepresentation.ActionRPC(number), targets, new object[0]);
	}

	// Token: 0x060034B2 RID: 13490 RVA: 0x000C068C File Offset: 0x000BE88C
	public void Action<T>(int number, IEnumerable<NetworkPlayer> targets, T argument)
	{
		base.networkView.RPC<T>(ItemRepresentation.ActionRPC(number), targets, argument);
	}

	// Token: 0x060034B3 RID: 13491 RVA: 0x000C06A4 File Offset: 0x000BE8A4
	public void Action(int number, IEnumerable<NetworkPlayer> targets, params object[] arguments)
	{
		base.networkView.RPC(ItemRepresentation.ActionRPC(number), targets, arguments);
	}

	// Token: 0x060034B4 RID: 13492 RVA: 0x000C06BC File Offset: 0x000BE8BC
	public void ActionStream(int number, IEnumerable<NetworkPlayer> targets, BitStream stream)
	{
		base.networkView.RPC<byte[]>(ItemRepresentation.ActionRPCBitstream(number), targets, stream.GetDataByteArray());
	}

	// Token: 0x060034B5 RID: 13493 RVA: 0x000C06E4 File Offset: 0x000BE8E4
	public void ActionStream(int number, NetworkPlayer target, BitStream stream)
	{
		base.networkView.RPC<byte[]>(ItemRepresentation.ActionRPCBitstream(number), target, stream.GetDataByteArray());
	}

	// Token: 0x060034B6 RID: 13494 RVA: 0x000C070C File Offset: 0x000BE90C
	public void ActionStream(int number, RPCMode mode, BitStream stream)
	{
		base.networkView.RPC<byte[]>(ItemRepresentation.ActionRPCBitstream(number), mode, stream.GetDataByteArray());
	}

	// Token: 0x060034B7 RID: 13495 RVA: 0x000C0734 File Offset: 0x000BE934
	private void RunAction(int number, BitStream stream, ref NetworkMessageInfo info)
	{
		switch (number)
		{
		case 1:
			this.datablock.DoAction1(stream, this, ref info);
			break;
		case 2:
			this.datablock.DoAction2(stream, this, ref info);
			break;
		case 3:
			this.datablock.DoAction3(stream, this, ref info);
			break;
		}
	}

	// Token: 0x060034B8 RID: 13496 RVA: 0x000C0798 File Offset: 0x000BE998
	[RPC]
	protected void Action1(BitStream stream, NetworkMessageInfo info)
	{
		InterpTimedEvent.Queue(this, "Action1", ref info, new object[]
		{
			stream
		});
	}

	// Token: 0x060034B9 RID: 13497 RVA: 0x000C07B4 File Offset: 0x000BE9B4
	[RPC]
	protected void Action2(BitStream stream, NetworkMessageInfo info)
	{
		InterpTimedEvent.Queue(this, "Action2", ref info, new object[]
		{
			stream
		});
	}

	// Token: 0x060034BA RID: 13498 RVA: 0x000C07D0 File Offset: 0x000BE9D0
	[RPC]
	protected void Action3(BitStream stream, NetworkMessageInfo info)
	{
		InterpTimedEvent.Queue(this, "Action3", ref info, new object[]
		{
			stream
		});
	}

	// Token: 0x060034BB RID: 13499 RVA: 0x000C07EC File Offset: 0x000BE9EC
	[RPC]
	protected void Action1B(byte[] data, NetworkMessageInfo info)
	{
		this.Action1(new BitStream(data, false), info);
	}

	// Token: 0x060034BC RID: 13500 RVA: 0x000C07FC File Offset: 0x000BE9FC
	[RPC]
	protected void Action2B(byte[] data, NetworkMessageInfo info)
	{
		this.Action2(new BitStream(data, false), info);
	}

	// Token: 0x060034BD RID: 13501 RVA: 0x000C080C File Offset: 0x000BEA0C
	[RPC]
	protected void Action3B(byte[] data, NetworkMessageInfo info)
	{
		this.Action3(new BitStream(data, false), info);
	}

	// Token: 0x060034BE RID: 13502 RVA: 0x000C081C File Offset: 0x000BEA1C
	protected virtual void OnInterpTimedEvent()
	{
		string tag = InterpTimedEvent.Tag;
		if (tag != null)
		{
			if (ItemRepresentation.<>f__switch$mapB == null)
			{
				ItemRepresentation.<>f__switch$mapB = new Dictionary<string, int>(4)
				{
					{
						"Action1",
						0
					},
					{
						"Action2",
						1
					},
					{
						"Action3",
						2
					},
					{
						"InterpDestroy",
						3
					}
				};
			}
			int num;
			if (ItemRepresentation.<>f__switch$mapB.TryGetValue(tag, out num))
			{
				int number;
				BitStream stream;
				switch (num)
				{
				case 0:
					number = 1;
					stream = InterpTimedEvent.Argument<BitStream>(0);
					break;
				case 1:
					number = 2;
					stream = InterpTimedEvent.Argument<BitStream>(0);
					break;
				case 2:
					number = 3;
					stream = InterpTimedEvent.Argument<BitStream>(0);
					break;
				case 3:
					Object.Destroy(base.gameObject);
					return;
				default:
					goto IL_BF;
				}
				NetworkMessageInfo info = InterpTimedEvent.Info;
				this.RunAction(number, stream, ref info);
				return;
			}
		}
		IL_BF:
		InterpTimedEvent.MarkUnhandled();
	}

	// Token: 0x060034BF RID: 13503 RVA: 0x000C0900 File Offset: 0x000BEB00
	private void FindSignalee()
	{
		this._parentMain = this._parentView.idMain;
		if (this._parentMain is Character)
		{
			Character character = (Character)this._parentMain;
			this.SetSignalee(character);
			this._holder = character.GetLocal<InventoryHolder>();
			if (this._holder)
			{
				this._holder.SetItemRepresentation(this);
			}
			return;
		}
		this._holder = null;
		this.ClearSignals();
	}

	// Token: 0x060034C0 RID: 13504 RVA: 0x000C0978 File Offset: 0x000BEB78
	private void ClearSignals()
	{
		if (this._characterSignalee)
		{
			this._characterSignalee.signal_state -= this.stateSignalReceive;
		}
		if (this._holder)
		{
			this._holder.ClearItemRepresentation(this);
			this._holder = null;
		}
		this._characterSignalee = null;
	}

	// Token: 0x060034C1 RID: 13505 RVA: 0x000C09D0 File Offset: 0x000BEBD0
	private void SetSignalee(Character signalee)
	{
		if (!signalee)
		{
			this.ClearSignals();
		}
		else
		{
			if (this._characterSignalee && this._characterSignalee == signalee)
			{
				return;
			}
			signalee.signal_state += this.stateSignalReceive;
			this._characterSignalee = signalee;
		}
	}

	// Token: 0x060034C2 RID: 13506 RVA: 0x000C0A28 File Offset: 0x000BEC28
	private void RunViewModelAdd(ViewModel vm, IHeldItem item, bool doMeshes)
	{
		ModViewModelAddArgs modViewModelAddArgs = new ModViewModelAddArgs(vm, item, doMeshes);
		for (int i = 0; i < 5; i++)
		{
			this._itemMods.BindAsLocal(i, ref modViewModelAddArgs, this);
		}
	}

	// Token: 0x060034C3 RID: 13507 RVA: 0x000C0A60 File Offset: 0x000BEC60
	internal void PrepareViewModel(ViewModel vm, IHeldItem item)
	{
		this.RunViewModelAdd(vm, item, true);
		this._lastViewModel = vm;
	}

	// Token: 0x060034C4 RID: 13508 RVA: 0x000C0A74 File Offset: 0x000BEC74
	internal void BindViewModel(ViewModel vm, IHeldItem item)
	{
		this.RunViewModelAdd(vm, item, false);
		this._lastViewModel = vm;
	}

	// Token: 0x060034C5 RID: 13509 RVA: 0x000C0A88 File Offset: 0x000BEC88
	internal void UnBindViewModel(ViewModel vm, IHeldItem item)
	{
		ModViewModelRemoveArgs modViewModelRemoveArgs = new ModViewModelRemoveArgs(vm, item);
		for (int i = 0; i < 5; i++)
		{
			this._itemMods.UnBindAsLocal(i, ref modViewModelRemoveArgs, this);
		}
		if (this._lastViewModel == vm)
		{
			this._lastViewModel = null;
		}
	}

	// Token: 0x060034C6 RID: 13510 RVA: 0x000C0AD8 File Offset: 0x000BECD8
	private void EraseModDatablock(ref ItemModDataBlock block)
	{
		block = null;
	}

	// Token: 0x060034C7 RID: 13511 RVA: 0x000C0AE0 File Offset: 0x000BECE0
	private void ClearModPair(ref ItemRepresentation.ItemModPair pair)
	{
		this.KillModRep(ref pair.representation, false);
		this.EraseModDatablock(ref pair.dataBlock);
		pair = default(ItemRepresentation.ItemModPair);
	}

	// Token: 0x060034C8 RID: 13512 RVA: 0x000C0B18 File Offset: 0x000BED18
	private bool ClearMods()
	{
		bool flag = this.modLock;
		if (!this.modLock)
		{
			this._modFlags = ItemModFlags.Other;
			try
			{
				this.modLock = true;
				for (int i = 0; i < 5; i++)
				{
					this._itemMods.ClearModPair(i, this);
				}
			}
			finally
			{
				this.modLock = flag;
			}
			return true;
		}
		return false;
	}

	// Token: 0x060034C9 RID: 13513 RVA: 0x000C0B90 File Offset: 0x000BED90
	internal void ItemModRepresentationDestroyed(ItemModRepresentation rep)
	{
		if (this.modLock || this.destroyingRep == rep)
		{
			return;
		}
		this._itemMods.KillModForRep(rep, this, true);
	}

	// Token: 0x060034CA RID: 13514 RVA: 0x000C0BCC File Offset: 0x000BEDCC
	private void InstallMod(ref ItemRepresentation.ItemModPair to, int slot, ItemModDataBlock datablock, CharacterStateFlags flags)
	{
		to.dataBlock = datablock;
		if (to.representation)
		{
			this.KillModRep(ref to.representation, false);
		}
		if (to.dataBlock.hasModRepresentation && to.dataBlock.AddModRepresentationComponent(base.gameObject, out to.representation))
		{
			to.bindState = ItemRepresentation.BindState.None;
			to.representation.Initialize(this, slot, flags);
			if (to.representation)
			{
				if (this.worldModels)
				{
					this._itemMods.BindAsProxy(slot, this);
				}
			}
			else
			{
				to.bindState = ItemRepresentation.BindState.Vacant;
				to.representation = null;
			}
		}
	}

	// Token: 0x060034CB RID: 13515 RVA: 0x000C0C7C File Offset: 0x000BEE7C
	protected CharacterStateFlags GetCharacterStateFlags()
	{
		if (this.CheckParent() && this._parentMain is Character)
		{
			CharacterStateFlags stateFlags = ((Character)this._parentMain).stateFlags;
			this.lastCharacterStateFlags = new CharacterStateFlags?(stateFlags);
			return stateFlags;
		}
		CharacterStateFlags? characterStateFlags = this.lastCharacterStateFlags;
		return (characterStateFlags == null) ? default(CharacterStateFlags) : characterStateFlags.Value;
	}

	// Token: 0x060034CC RID: 13516 RVA: 0x000C0CEC File Offset: 0x000BEEEC
	[Obsolete("This is dumb. The datablock shouldnt change")]
	internal void SetDataBlockFromHeldItem<T>(HeldItem<T> item) where T : HeldItemDataBlock
	{
		this._datablock = item.datablock;
	}

	// Token: 0x04001A3B RID: 6715
	private HeldItemDataBlock _datablock;

	// Token: 0x04001A3C RID: 6716
	private InventoryHolder _holder;

	// Token: 0x04001A3D RID: 6717
	[SerializeField]
	private GameObject[] visuals;

	// Token: 0x04001A3E RID: 6718
	internal ItemRepresentation.ItemModPairArray _itemMods;

	// Token: 0x04001A3F RID: 6719
	private NetworkView _parentView;

	// Token: 0x04001A40 RID: 6720
	private IDMain _parentMain;

	// Token: 0x04001A41 RID: 6721
	private NetworkViewID _parentViewID;

	// Token: 0x04001A42 RID: 6722
	private Character _characterSignalee;

	// Token: 0x04001A43 RID: 6723
	private ViewModel _lastViewModel;

	// Token: 0x04001A44 RID: 6724
	[NonSerialized]
	private string worldAnimationGroupNameOverride;

	// Token: 0x04001A45 RID: 6725
	public Socket.LocalSpace muzzle;

	// Token: 0x04001A46 RID: 6726
	public Socket.LocalSpace hand;

	// Token: 0x04001A47 RID: 6727
	private ItemModFlags _modFlags;

	// Token: 0x04001A48 RID: 6728
	private bool worldStateDisabled;

	// Token: 0x04001A49 RID: 6729
	private CharacterStateFlags? lastCharacterStateFlags;

	// Token: 0x04001A4A RID: 6730
	private readonly CharacterStateSignal stateSignalReceive;

	// Token: 0x04001A4B RID: 6731
	private bool modLock;

	// Token: 0x04001A4C RID: 6732
	[NonSerialized]
	private ItemModRepresentation destroyingRep;

	// Token: 0x020005AB RID: 1451
	internal struct ItemModPair
	{
		// Token: 0x04001A4E RID: 6734
		public ItemModDataBlock dataBlock;

		// Token: 0x04001A4F RID: 6735
		public ItemModRepresentation representation;

		// Token: 0x04001A50 RID: 6736
		public ItemRepresentation.BindState bindState;
	}

	// Token: 0x020005AC RID: 1452
	internal enum BindState : sbyte
	{
		// Token: 0x04001A52 RID: 6738
		Vacant,
		// Token: 0x04001A53 RID: 6739
		None,
		// Token: 0x04001A54 RID: 6740
		World,
		// Token: 0x04001A55 RID: 6741
		Local
	}

	// Token: 0x020005AD RID: 1453
	internal struct ItemModPairArray
	{
		// Token: 0x17000A5E RID: 2654
		public ItemRepresentation.ItemModPair this[int slotNumber]
		{
			get
			{
				switch (slotNumber)
				{
				case 0:
					return this.a;
				case 1:
					return this.b;
				case 2:
					return this.c;
				case 3:
					return this.d;
				case 4:
					return this.e;
				default:
					throw new IndexOutOfRangeException();
				}
			}
			set
			{
				switch (slotNumber)
				{
				case 0:
					this.a = value;
					break;
				case 1:
					this.b = value;
					break;
				case 2:
					this.c = value;
					break;
				case 3:
					this.d = value;
					break;
				case 4:
					this.e = value;
					break;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000C0DCC File Offset: 0x000BEFCC
		public ItemModDataBlock ItemModDataBlock(int slotNumber)
		{
			switch (slotNumber)
			{
			case 0:
				return this.a.dataBlock;
			case 1:
				return this.b.dataBlock;
			case 2:
				return this.c.dataBlock;
			case 3:
				return this.d.dataBlock;
			case 4:
				return this.e.dataBlock;
			default:
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000C0E3C File Offset: 0x000BF03C
		public void BindAsLocal(int slotNumber, ref ModViewModelAddArgs args, ItemRepresentation itemRep)
		{
			switch (slotNumber)
			{
			case 0:
				itemRep.BindModAsLocal(ref this.a, ref args);
				break;
			case 1:
				itemRep.BindModAsLocal(ref this.b, ref args);
				break;
			case 2:
				itemRep.BindModAsLocal(ref this.c, ref args);
				break;
			case 3:
				itemRep.BindModAsLocal(ref this.d, ref args);
				break;
			case 4:
				itemRep.BindModAsLocal(ref this.e, ref args);
				break;
			default:
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000C0ECC File Offset: 0x000BF0CC
		public void UnBindAsLocal(int slotNumber, ref ModViewModelRemoveArgs args, ItemRepresentation itemRep)
		{
			switch (slotNumber)
			{
			case 0:
				itemRep.UnBindModAsLocal(ref this.a, ref args);
				break;
			case 1:
				itemRep.UnBindModAsLocal(ref this.b, ref args);
				break;
			case 2:
				itemRep.UnBindModAsLocal(ref this.c, ref args);
				break;
			case 3:
				itemRep.UnBindModAsLocal(ref this.d, ref args);
				break;
			case 4:
				itemRep.UnBindModAsLocal(ref this.e, ref args);
				break;
			default:
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000C0F5C File Offset: 0x000BF15C
		public void BindAsProxy(int slotNumber, ItemRepresentation itemRep)
		{
			switch (slotNumber)
			{
			case 0:
				itemRep.BindModAsProxy(ref this.a);
				break;
			case 1:
				itemRep.BindModAsProxy(ref this.b);
				break;
			case 2:
				itemRep.BindModAsProxy(ref this.c);
				break;
			case 3:
				itemRep.BindModAsProxy(ref this.d);
				break;
			case 4:
				itemRep.BindModAsProxy(ref this.e);
				break;
			default:
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000C0FE8 File Offset: 0x000BF1E8
		public void UnBindAsProxy(int slotNumber, ItemRepresentation itemRep)
		{
			switch (slotNumber)
			{
			case 0:
				itemRep.UnBindModAsProxy(ref this.a);
				break;
			case 1:
				itemRep.UnBindModAsProxy(ref this.b);
				break;
			case 2:
				itemRep.UnBindModAsProxy(ref this.c);
				break;
			case 3:
				itemRep.UnBindModAsProxy(ref this.d);
				break;
			case 4:
				itemRep.UnBindModAsProxy(ref this.e);
				break;
			default:
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000C1074 File Offset: 0x000BF274
		public void ClearModPair(int slotNumber, ItemRepresentation owner)
		{
			switch (slotNumber)
			{
			case 0:
				owner.ClearModPair(ref this.a);
				break;
			case 1:
				owner.ClearModPair(ref this.b);
				break;
			case 2:
				owner.ClearModPair(ref this.c);
				break;
			case 3:
				owner.ClearModPair(ref this.d);
				break;
			case 4:
				owner.ClearModPair(ref this.e);
				break;
			default:
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000C1100 File Offset: 0x000BF300
		private static bool KillModForRep(ref ItemRepresentation.ItemModPair pair, ItemModRepresentation modRep, ItemRepresentation owner, bool fromCallback)
		{
			if (pair.representation == modRep)
			{
				owner.KillModRep(ref pair.representation, fromCallback);
				return true;
			}
			return true;
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x000C1124 File Offset: 0x000BF324
		public bool KillModForRep(ItemModRepresentation modRep, ItemRepresentation owner, bool fromCallback)
		{
			switch (modRep.modSlot)
			{
			case 0:
				return ItemRepresentation.ItemModPairArray.KillModForRep(ref this.a, modRep, owner, fromCallback);
			case 1:
				return ItemRepresentation.ItemModPairArray.KillModForRep(ref this.b, modRep, owner, fromCallback);
			case 2:
				return ItemRepresentation.ItemModPairArray.KillModForRep(ref this.c, modRep, owner, fromCallback);
			case 3:
				return ItemRepresentation.ItemModPairArray.KillModForRep(ref this.d, modRep, owner, fromCallback);
			case 4:
				return ItemRepresentation.ItemModPairArray.KillModForRep(ref this.e, modRep, owner, fromCallback);
			default:
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000C11A8 File Offset: 0x000BF3A8
		public void InstallMod(int slotNumber, ItemRepresentation owner, ItemModDataBlock datablock, CharacterStateFlags flags)
		{
			switch (slotNumber)
			{
			case 0:
				owner.InstallMod(ref this.a, 0, datablock, flags);
				break;
			case 1:
				owner.InstallMod(ref this.b, 1, datablock, flags);
				break;
			case 2:
				owner.InstallMod(ref this.c, 2, datablock, flags);
				break;
			case 3:
				owner.InstallMod(ref this.d, 3, datablock, flags);
				break;
			case 4:
				owner.InstallMod(ref this.e, 4, datablock, flags);
				break;
			default:
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x04001A56 RID: 6742
		private const int internalPairCount = 5;

		// Token: 0x04001A57 RID: 6743
		private ItemRepresentation.ItemModPair a;

		// Token: 0x04001A58 RID: 6744
		private ItemRepresentation.ItemModPair b;

		// Token: 0x04001A59 RID: 6745
		private ItemRepresentation.ItemModPair c;

		// Token: 0x04001A5A RID: 6746
		private ItemRepresentation.ItemModPair d;

		// Token: 0x04001A5B RID: 6747
		private ItemRepresentation.ItemModPair e;
	}
}
