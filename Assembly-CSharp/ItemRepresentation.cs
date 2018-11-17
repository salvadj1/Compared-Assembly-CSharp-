using System;
using System.Collections.Generic;
using Facepunch;
using Facepunch.Movement;
using uLink;
using UnityEngine;

// Token: 0x02000668 RID: 1640
[RequireComponent(typeof(uLinkNetworkView))]
public class ItemRepresentation : IDMain, global::IInterpTimedEventReceiver
{
	// Token: 0x06003858 RID: 14424 RVA: 0x000C801C File Offset: 0x000C621C
	public ItemRepresentation() : base(2)
	{
		this.stateSignalReceive = new global::CharacterStateSignal(this.StateSignalReceive);
	}

	// Token: 0x06003859 RID: 14425 RVA: 0x000C8038 File Offset: 0x000C6238
	void global::IInterpTimedEventReceiver.OnInterpTimedEvent()
	{
		this.OnInterpTimedEvent();
	}

	// Token: 0x17000AD0 RID: 2768
	// (get) Token: 0x0600385A RID: 14426 RVA: 0x000C8040 File Offset: 0x000C6240
	public global::ItemModFlags modFlags
	{
		get
		{
			return this._modFlags;
		}
	}

	// Token: 0x17000AD1 RID: 2769
	// (get) Token: 0x0600385B RID: 14427 RVA: 0x000C8048 File Offset: 0x000C6248
	public global::HeldItemDataBlock datablock
	{
		get
		{
			return this._datablock;
		}
	}

	// Token: 0x0600385C RID: 14428 RVA: 0x000C8050 File Offset: 0x000C6250
	private void BindModAsLocal(ref global::ItemRepresentation.ItemModPair pair, ref global::ModViewModelAddArgs a)
	{
		if ((int)pair.bindState == 2)
		{
			this.UnBindModAsProxy(ref pair);
		}
		if ((int)pair.bindState == 1 || (int)pair.bindState == 3)
		{
			a.modRep = pair.representation;
			pair.dataBlock.BindAsLocal(ref a);
			pair.bindState = global::ItemRepresentation.BindState.Local;
		}
	}

	// Token: 0x0600385D RID: 14429 RVA: 0x000C80AC File Offset: 0x000C62AC
	private void UnBindModAsLocal(ref global::ItemRepresentation.ItemModPair pair, ref global::ModViewModelRemoveArgs a)
	{
		if ((int)pair.bindState == 3)
		{
			a.modRep = pair.representation;
			pair.dataBlock.UnBindAsLocal(ref a);
			pair.bindState = global::ItemRepresentation.BindState.None;
		}
	}

	// Token: 0x0600385E RID: 14430 RVA: 0x000C80E8 File Offset: 0x000C62E8
	private void BindModAsProxy(ref global::ItemRepresentation.ItemModPair pair)
	{
		if ((int)pair.bindState == 1)
		{
			pair.dataBlock.BindAsProxy(pair.representation);
			pair.bindState = global::ItemRepresentation.BindState.World;
		}
	}

	// Token: 0x0600385F RID: 14431 RVA: 0x000C8110 File Offset: 0x000C6310
	private void UnBindModAsProxy(ref global::ItemRepresentation.ItemModPair pair)
	{
		if ((int)pair.bindState == 2)
		{
			pair.dataBlock.UnBindAsProxy(pair.representation);
			pair.bindState = global::ItemRepresentation.BindState.None;
		}
	}

	// Token: 0x06003860 RID: 14432 RVA: 0x000C8138 File Offset: 0x000C6338
	protected void Awake()
	{
	}

	// Token: 0x17000AD2 RID: 2770
	// (get) Token: 0x06003861 RID: 14433 RVA: 0x000C813C File Offset: 0x000C633C
	public string worldAnimationGroupName
	{
		get
		{
			return this.worldAnimationGroupNameOverride ?? this.datablock.animationGroupName;
		}
	}

	// Token: 0x06003862 RID: 14434 RVA: 0x000C8158 File Offset: 0x000C6358
	public bool PlayWorldAnimation(GroupEvent GroupEvent, float speed, float animationTime)
	{
		if (this._characterSignalee)
		{
			global::PlayerAnimation component = this._characterSignalee.GetComponent<global::PlayerAnimation>();
			if (component)
			{
				return component.PlayAnimation(GroupEvent, speed, animationTime);
			}
		}
		return false;
	}

	// Token: 0x06003863 RID: 14435 RVA: 0x000C8198 File Offset: 0x000C6398
	public bool PlayWorldAnimation(GroupEvent GroupEvent, float speed)
	{
		if (this._characterSignalee)
		{
			global::PlayerAnimation component = this._characterSignalee.GetComponent<global::PlayerAnimation>();
			if (component)
			{
				return component.PlayAnimation(GroupEvent, speed);
			}
		}
		return false;
	}

	// Token: 0x06003864 RID: 14436 RVA: 0x000C81D8 File Offset: 0x000C63D8
	public bool PlayWorldAnimation(GroupEvent GroupEvent)
	{
		if (this._characterSignalee)
		{
			global::PlayerAnimation component = this._characterSignalee.GetComponent<global::PlayerAnimation>();
			if (component)
			{
				return component.PlayAnimation(GroupEvent);
			}
		}
		return false;
	}

	// Token: 0x06003865 RID: 14437 RVA: 0x000C8218 File Offset: 0x000C6418
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

	// Token: 0x06003866 RID: 14438 RVA: 0x000C8284 File Offset: 0x000C6484
	protected void OnDrawGizmosSelected()
	{
		this.muzzle.DrawGizmos("muzzle");
	}

	// Token: 0x06003867 RID: 14439 RVA: 0x000C8298 File Offset: 0x000C6498
	private void KillModRep(ref global::ItemModRepresentation rep, bool fromCallback)
	{
		if (!fromCallback && rep)
		{
			global::ItemModRepresentation itemModRepresentation = this.destroyingRep;
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

	// Token: 0x06003868 RID: 14440 RVA: 0x000C82FC File Offset: 0x000C64FC
	protected void OnDestroy()
	{
		try
		{
			global::InterpTimedEvent.Remove(this, true);
			this.ClearMods();
		}
		finally
		{
			this._parentViewID = uLink.NetworkViewID.unassigned;
			this.ClearSignals();
			base.OnDestroy();
		}
	}

	// Token: 0x06003869 RID: 14441 RVA: 0x000C8350 File Offset: 0x000C6550
	public virtual void SetParent(GameObject parentGameObject)
	{
		Transform transform = parentGameObject.transform;
		if (!base.transform.IsChildOf(transform))
		{
			base.transform.parent = transform;
		}
	}

	// Token: 0x0600386A RID: 14442 RVA: 0x000C8384 File Offset: 0x000C6584
	protected bool CheckParent()
	{
		if (this._parentView)
		{
			return true;
		}
		if (this._parentViewID != uLink.NetworkViewID.unassigned)
		{
			this._parentView = Facepunch.NetworkView.Find(this._parentViewID);
			if (this._parentView)
			{
				this._parentMain = null;
				global::PlayerAnimation component = this._parentView.GetComponent<global::PlayerAnimation>();
				global::Socket.LocalSpace itemAttachment = component.itemAttachment;
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
					this.worldModels = global::actor.forceThirdPerson;
				}
				this.FindSignalee();
				return true;
			}
		}
		this.ClearSignals();
		return false;
	}

	// Token: 0x0600386B RID: 14443 RVA: 0x000C84EC File Offset: 0x000C66EC
	protected void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		this._parentViewID = info.networkView.initialData.ReadNetworkViewID();
		int uniqueID = info.networkView.initialData.ReadInt32();
		this._datablock = (global::HeldItemDataBlock)global::DatablockDictionary.GetByUniqueID(uniqueID);
		if (!this.CheckParent())
		{
			Debug.Log("No parent for item rep (yet)", this);
		}
	}

	// Token: 0x0600386C RID: 14444 RVA: 0x000C8548 File Offset: 0x000C6748
	[RPC]
	protected void Mods(byte[] data)
	{
		this.ClearMods();
		BitStream bitStream = new BitStream(data, false);
		byte b = bitStream.ReadByte();
		if (b > 0)
		{
			global::CharacterStateFlags characterStateFlags = this.GetCharacterStateFlags();
			for (int i = 0; i < (int)b; i++)
			{
				int uniqueID = bitStream.ReadInt32();
				global::ItemModDataBlock itemModDataBlock = (global::ItemModDataBlock)global::DatablockDictionary.GetByUniqueID(uniqueID);
				this._itemMods.InstallMod(i, this, itemModDataBlock, characterStateFlags);
				this._modFlags |= itemModDataBlock.modFlag;
			}
		}
	}

	// Token: 0x17000AD3 RID: 2771
	// (get) Token: 0x0600386D RID: 14445 RVA: 0x000C85C8 File Offset: 0x000C67C8
	// (set) Token: 0x0600386E RID: 14446 RVA: 0x000C85D4 File Offset: 0x000C67D4
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

	// Token: 0x0600386F RID: 14447 RVA: 0x000C86A8 File Offset: 0x000C68A8
	protected virtual void StateSignalReceive(global::Character character, bool treatedAsFirst)
	{
		global::CharacterStateFlags stateFlags = character.stateFlags;
		if (this.lastCharacterStateFlags != null && this.lastCharacterStateFlags.Value.Equals(stateFlags))
		{
			return;
		}
		this.lastCharacterStateFlags = new global::CharacterStateFlags?(stateFlags);
		for (int i = 0; i < 5; i++)
		{
			if (this._itemMods[i].representation)
			{
				this._itemMods[i].representation.HandleChangedStateFlags(stateFlags, !treatedAsFirst);
			}
		}
	}

	// Token: 0x06003870 RID: 14448 RVA: 0x000C8744 File Offset: 0x000C6944
	[RPC]
	protected void InterpDestroy(uLink.NetworkMessageInfo info)
	{
		if (base.networkView && base.networkView.isMine)
		{
			global::InterpTimedEvent.Remove(this, true);
		}
		else
		{
			global::InterpTimedEvent.Queue(this, "InterpDestroy", ref info);
			global::NetCull.DontDestroyWithNetwork(this);
		}
	}

	// Token: 0x06003871 RID: 14449 RVA: 0x000C8794 File Offset: 0x000C6994
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

	// Token: 0x06003872 RID: 14450 RVA: 0x000C87E4 File Offset: 0x000C69E4
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

	// Token: 0x06003873 RID: 14451 RVA: 0x000C8834 File Offset: 0x000C6A34
	public void Action(int number, uLink.RPCMode mode)
	{
		base.networkView.RPC(global::ItemRepresentation.ActionRPC(number), mode, new object[0]);
	}

	// Token: 0x06003874 RID: 14452 RVA: 0x000C8850 File Offset: 0x000C6A50
	public void Action<T>(int number, uLink.RPCMode mode, T argument)
	{
		base.networkView.RPC<T>(global::ItemRepresentation.ActionRPC(number), mode, argument);
	}

	// Token: 0x06003875 RID: 14453 RVA: 0x000C8868 File Offset: 0x000C6A68
	public void Action(int number, uLink.RPCMode mode, params object[] arguments)
	{
		base.networkView.RPC(global::ItemRepresentation.ActionRPC(number), mode, arguments);
	}

	// Token: 0x06003876 RID: 14454 RVA: 0x000C8880 File Offset: 0x000C6A80
	public void Action(int number, uLink.NetworkPlayer target)
	{
		base.networkView.RPC(global::ItemRepresentation.ActionRPC(number), target, new object[0]);
	}

	// Token: 0x06003877 RID: 14455 RVA: 0x000C889C File Offset: 0x000C6A9C
	public void Action<T>(int number, uLink.NetworkPlayer target, T argument)
	{
		base.networkView.RPC<T>(global::ItemRepresentation.ActionRPC(number), target, argument);
	}

	// Token: 0x06003878 RID: 14456 RVA: 0x000C88B4 File Offset: 0x000C6AB4
	public void Action(int number, uLink.NetworkPlayer target, params object[] arguments)
	{
		base.networkView.RPC(global::ItemRepresentation.ActionRPC(number), target, arguments);
	}

	// Token: 0x06003879 RID: 14457 RVA: 0x000C88CC File Offset: 0x000C6ACC
	public void Action(int number, IEnumerable<uLink.NetworkPlayer> targets)
	{
		base.networkView.RPC(global::ItemRepresentation.ActionRPC(number), targets, new object[0]);
	}

	// Token: 0x0600387A RID: 14458 RVA: 0x000C88E8 File Offset: 0x000C6AE8
	public void Action<T>(int number, IEnumerable<uLink.NetworkPlayer> targets, T argument)
	{
		base.networkView.RPC<T>(global::ItemRepresentation.ActionRPC(number), targets, argument);
	}

	// Token: 0x0600387B RID: 14459 RVA: 0x000C8900 File Offset: 0x000C6B00
	public void Action(int number, IEnumerable<uLink.NetworkPlayer> targets, params object[] arguments)
	{
		base.networkView.RPC(global::ItemRepresentation.ActionRPC(number), targets, arguments);
	}

	// Token: 0x0600387C RID: 14460 RVA: 0x000C8918 File Offset: 0x000C6B18
	public void ActionStream(int number, IEnumerable<uLink.NetworkPlayer> targets, BitStream stream)
	{
		base.networkView.RPC<byte[]>(global::ItemRepresentation.ActionRPCBitstream(number), targets, stream.GetDataByteArray());
	}

	// Token: 0x0600387D RID: 14461 RVA: 0x000C8940 File Offset: 0x000C6B40
	public void ActionStream(int number, uLink.NetworkPlayer target, BitStream stream)
	{
		base.networkView.RPC<byte[]>(global::ItemRepresentation.ActionRPCBitstream(number), target, stream.GetDataByteArray());
	}

	// Token: 0x0600387E RID: 14462 RVA: 0x000C8968 File Offset: 0x000C6B68
	public void ActionStream(int number, uLink.RPCMode mode, BitStream stream)
	{
		base.networkView.RPC<byte[]>(global::ItemRepresentation.ActionRPCBitstream(number), mode, stream.GetDataByteArray());
	}

	// Token: 0x0600387F RID: 14463 RVA: 0x000C8990 File Offset: 0x000C6B90
	private void RunAction(int number, BitStream stream, ref uLink.NetworkMessageInfo info)
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

	// Token: 0x06003880 RID: 14464 RVA: 0x000C89F4 File Offset: 0x000C6BF4
	[RPC]
	protected void Action1(BitStream stream, uLink.NetworkMessageInfo info)
	{
		global::InterpTimedEvent.Queue(this, "Action1", ref info, new object[]
		{
			stream
		});
	}

	// Token: 0x06003881 RID: 14465 RVA: 0x000C8A10 File Offset: 0x000C6C10
	[RPC]
	protected void Action2(BitStream stream, uLink.NetworkMessageInfo info)
	{
		global::InterpTimedEvent.Queue(this, "Action2", ref info, new object[]
		{
			stream
		});
	}

	// Token: 0x06003882 RID: 14466 RVA: 0x000C8A2C File Offset: 0x000C6C2C
	[RPC]
	protected void Action3(BitStream stream, uLink.NetworkMessageInfo info)
	{
		global::InterpTimedEvent.Queue(this, "Action3", ref info, new object[]
		{
			stream
		});
	}

	// Token: 0x06003883 RID: 14467 RVA: 0x000C8A48 File Offset: 0x000C6C48
	[RPC]
	protected void Action1B(byte[] data, uLink.NetworkMessageInfo info)
	{
		this.Action1(new BitStream(data, false), info);
	}

	// Token: 0x06003884 RID: 14468 RVA: 0x000C8A58 File Offset: 0x000C6C58
	[RPC]
	protected void Action2B(byte[] data, uLink.NetworkMessageInfo info)
	{
		this.Action2(new BitStream(data, false), info);
	}

	// Token: 0x06003885 RID: 14469 RVA: 0x000C8A68 File Offset: 0x000C6C68
	[RPC]
	protected void Action3B(byte[] data, uLink.NetworkMessageInfo info)
	{
		this.Action3(new BitStream(data, false), info);
	}

	// Token: 0x06003886 RID: 14470 RVA: 0x000C8A78 File Offset: 0x000C6C78
	protected virtual void OnInterpTimedEvent()
	{
		string tag = global::InterpTimedEvent.Tag;
		if (tag != null)
		{
			if (global::ItemRepresentation.<>f__switch$mapB == null)
			{
				global::ItemRepresentation.<>f__switch$mapB = new Dictionary<string, int>(4)
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
			if (global::ItemRepresentation.<>f__switch$mapB.TryGetValue(tag, out num))
			{
				int number;
				BitStream stream;
				switch (num)
				{
				case 0:
					number = 1;
					stream = global::InterpTimedEvent.Argument<BitStream>(0);
					break;
				case 1:
					number = 2;
					stream = global::InterpTimedEvent.Argument<BitStream>(0);
					break;
				case 2:
					number = 3;
					stream = global::InterpTimedEvent.Argument<BitStream>(0);
					break;
				case 3:
					Object.Destroy(base.gameObject);
					return;
				default:
					goto IL_BF;
				}
				uLink.NetworkMessageInfo info = global::InterpTimedEvent.Info;
				this.RunAction(number, stream, ref info);
				return;
			}
		}
		IL_BF:
		global::InterpTimedEvent.MarkUnhandled();
	}

	// Token: 0x06003887 RID: 14471 RVA: 0x000C8B5C File Offset: 0x000C6D5C
	private void FindSignalee()
	{
		this._parentMain = this._parentView.idMain;
		if (this._parentMain is global::Character)
		{
			global::Character character = (global::Character)this._parentMain;
			this.SetSignalee(character);
			this._holder = character.GetLocal<global::InventoryHolder>();
			if (this._holder)
			{
				this._holder.SetItemRepresentation(this);
			}
			return;
		}
		this._holder = null;
		this.ClearSignals();
	}

	// Token: 0x06003888 RID: 14472 RVA: 0x000C8BD4 File Offset: 0x000C6DD4
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

	// Token: 0x06003889 RID: 14473 RVA: 0x000C8C2C File Offset: 0x000C6E2C
	private void SetSignalee(global::Character signalee)
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

	// Token: 0x0600388A RID: 14474 RVA: 0x000C8C84 File Offset: 0x000C6E84
	private void RunViewModelAdd(global::ViewModel vm, global::IHeldItem item, bool doMeshes)
	{
		global::ModViewModelAddArgs modViewModelAddArgs = new global::ModViewModelAddArgs(vm, item, doMeshes);
		for (int i = 0; i < 5; i++)
		{
			this._itemMods.BindAsLocal(i, ref modViewModelAddArgs, this);
		}
	}

	// Token: 0x0600388B RID: 14475 RVA: 0x000C8CBC File Offset: 0x000C6EBC
	internal void PrepareViewModel(global::ViewModel vm, global::IHeldItem item)
	{
		this.RunViewModelAdd(vm, item, true);
		this._lastViewModel = vm;
	}

	// Token: 0x0600388C RID: 14476 RVA: 0x000C8CD0 File Offset: 0x000C6ED0
	internal void BindViewModel(global::ViewModel vm, global::IHeldItem item)
	{
		this.RunViewModelAdd(vm, item, false);
		this._lastViewModel = vm;
	}

	// Token: 0x0600388D RID: 14477 RVA: 0x000C8CE4 File Offset: 0x000C6EE4
	internal void UnBindViewModel(global::ViewModel vm, global::IHeldItem item)
	{
		global::ModViewModelRemoveArgs modViewModelRemoveArgs = new global::ModViewModelRemoveArgs(vm, item);
		for (int i = 0; i < 5; i++)
		{
			this._itemMods.UnBindAsLocal(i, ref modViewModelRemoveArgs, this);
		}
		if (this._lastViewModel == vm)
		{
			this._lastViewModel = null;
		}
	}

	// Token: 0x0600388E RID: 14478 RVA: 0x000C8D34 File Offset: 0x000C6F34
	private void EraseModDatablock(ref global::ItemModDataBlock block)
	{
		block = null;
	}

	// Token: 0x0600388F RID: 14479 RVA: 0x000C8D3C File Offset: 0x000C6F3C
	private void ClearModPair(ref global::ItemRepresentation.ItemModPair pair)
	{
		this.KillModRep(ref pair.representation, false);
		this.EraseModDatablock(ref pair.dataBlock);
		pair = default(global::ItemRepresentation.ItemModPair);
	}

	// Token: 0x06003890 RID: 14480 RVA: 0x000C8D74 File Offset: 0x000C6F74
	private bool ClearMods()
	{
		bool flag = this.modLock;
		if (!this.modLock)
		{
			this._modFlags = global::ItemModFlags.Other;
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

	// Token: 0x06003891 RID: 14481 RVA: 0x000C8DEC File Offset: 0x000C6FEC
	internal void ItemModRepresentationDestroyed(global::ItemModRepresentation rep)
	{
		if (this.modLock || this.destroyingRep == rep)
		{
			return;
		}
		this._itemMods.KillModForRep(rep, this, true);
	}

	// Token: 0x06003892 RID: 14482 RVA: 0x000C8E28 File Offset: 0x000C7028
	private void InstallMod(ref global::ItemRepresentation.ItemModPair to, int slot, global::ItemModDataBlock datablock, global::CharacterStateFlags flags)
	{
		to.dataBlock = datablock;
		if (to.representation)
		{
			this.KillModRep(ref to.representation, false);
		}
		if (to.dataBlock.hasModRepresentation && to.dataBlock.AddModRepresentationComponent(base.gameObject, out to.representation))
		{
			to.bindState = global::ItemRepresentation.BindState.None;
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
				to.bindState = global::ItemRepresentation.BindState.Vacant;
				to.representation = null;
			}
		}
	}

	// Token: 0x06003893 RID: 14483 RVA: 0x000C8ED8 File Offset: 0x000C70D8
	protected global::CharacterStateFlags GetCharacterStateFlags()
	{
		if (this.CheckParent() && this._parentMain is global::Character)
		{
			global::CharacterStateFlags stateFlags = ((global::Character)this._parentMain).stateFlags;
			this.lastCharacterStateFlags = new global::CharacterStateFlags?(stateFlags);
			return stateFlags;
		}
		global::CharacterStateFlags? characterStateFlags = this.lastCharacterStateFlags;
		return (characterStateFlags == null) ? default(global::CharacterStateFlags) : characterStateFlags.Value;
	}

	// Token: 0x06003894 RID: 14484 RVA: 0x000C8F48 File Offset: 0x000C7148
	[Obsolete("This is dumb. The datablock shouldnt change")]
	internal void SetDataBlockFromHeldItem<T>(global::HeldItem<T> item) where T : global::HeldItemDataBlock
	{
		this._datablock = item.datablock;
	}

	// Token: 0x04001C0C RID: 7180
	private global::HeldItemDataBlock _datablock;

	// Token: 0x04001C0D RID: 7181
	private global::InventoryHolder _holder;

	// Token: 0x04001C0E RID: 7182
	[SerializeField]
	private GameObject[] visuals;

	// Token: 0x04001C0F RID: 7183
	internal global::ItemRepresentation.ItemModPairArray _itemMods;

	// Token: 0x04001C10 RID: 7184
	private Facepunch.NetworkView _parentView;

	// Token: 0x04001C11 RID: 7185
	private IDMain _parentMain;

	// Token: 0x04001C12 RID: 7186
	private uLink.NetworkViewID _parentViewID;

	// Token: 0x04001C13 RID: 7187
	private global::Character _characterSignalee;

	// Token: 0x04001C14 RID: 7188
	private global::ViewModel _lastViewModel;

	// Token: 0x04001C15 RID: 7189
	[NonSerialized]
	private string worldAnimationGroupNameOverride;

	// Token: 0x04001C16 RID: 7190
	public global::Socket.LocalSpace muzzle;

	// Token: 0x04001C17 RID: 7191
	public global::Socket.LocalSpace hand;

	// Token: 0x04001C18 RID: 7192
	private global::ItemModFlags _modFlags;

	// Token: 0x04001C19 RID: 7193
	private bool worldStateDisabled;

	// Token: 0x04001C1A RID: 7194
	private global::CharacterStateFlags? lastCharacterStateFlags;

	// Token: 0x04001C1B RID: 7195
	private readonly global::CharacterStateSignal stateSignalReceive;

	// Token: 0x04001C1C RID: 7196
	private bool modLock;

	// Token: 0x04001C1D RID: 7197
	[NonSerialized]
	private global::ItemModRepresentation destroyingRep;

	// Token: 0x02000669 RID: 1641
	internal struct ItemModPair
	{
		// Token: 0x04001C1F RID: 7199
		public global::ItemModDataBlock dataBlock;

		// Token: 0x04001C20 RID: 7200
		public global::ItemModRepresentation representation;

		// Token: 0x04001C21 RID: 7201
		public global::ItemRepresentation.BindState bindState;
	}

	// Token: 0x0200066A RID: 1642
	internal enum BindState : sbyte
	{
		// Token: 0x04001C23 RID: 7203
		Vacant,
		// Token: 0x04001C24 RID: 7204
		None,
		// Token: 0x04001C25 RID: 7205
		World,
		// Token: 0x04001C26 RID: 7206
		Local
	}

	// Token: 0x0200066B RID: 1643
	internal struct ItemModPairArray
	{
		// Token: 0x17000AD4 RID: 2772
		public global::ItemRepresentation.ItemModPair this[int slotNumber]
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

		// Token: 0x06003898 RID: 14488 RVA: 0x000C9028 File Offset: 0x000C7228
		public global::ItemModDataBlock ItemModDataBlock(int slotNumber)
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

		// Token: 0x06003899 RID: 14489 RVA: 0x000C9098 File Offset: 0x000C7298
		public void BindAsLocal(int slotNumber, ref global::ModViewModelAddArgs args, global::ItemRepresentation itemRep)
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

		// Token: 0x0600389A RID: 14490 RVA: 0x000C9128 File Offset: 0x000C7328
		public void UnBindAsLocal(int slotNumber, ref global::ModViewModelRemoveArgs args, global::ItemRepresentation itemRep)
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

		// Token: 0x0600389B RID: 14491 RVA: 0x000C91B8 File Offset: 0x000C73B8
		public void BindAsProxy(int slotNumber, global::ItemRepresentation itemRep)
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

		// Token: 0x0600389C RID: 14492 RVA: 0x000C9244 File Offset: 0x000C7444
		public void UnBindAsProxy(int slotNumber, global::ItemRepresentation itemRep)
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

		// Token: 0x0600389D RID: 14493 RVA: 0x000C92D0 File Offset: 0x000C74D0
		public void ClearModPair(int slotNumber, global::ItemRepresentation owner)
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

		// Token: 0x0600389E RID: 14494 RVA: 0x000C935C File Offset: 0x000C755C
		private static bool KillModForRep(ref global::ItemRepresentation.ItemModPair pair, global::ItemModRepresentation modRep, global::ItemRepresentation owner, bool fromCallback)
		{
			if (pair.representation == modRep)
			{
				owner.KillModRep(ref pair.representation, fromCallback);
				return true;
			}
			return true;
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000C9380 File Offset: 0x000C7580
		public bool KillModForRep(global::ItemModRepresentation modRep, global::ItemRepresentation owner, bool fromCallback)
		{
			switch (modRep.modSlot)
			{
			case 0:
				return global::ItemRepresentation.ItemModPairArray.KillModForRep(ref this.a, modRep, owner, fromCallback);
			case 1:
				return global::ItemRepresentation.ItemModPairArray.KillModForRep(ref this.b, modRep, owner, fromCallback);
			case 2:
				return global::ItemRepresentation.ItemModPairArray.KillModForRep(ref this.c, modRep, owner, fromCallback);
			case 3:
				return global::ItemRepresentation.ItemModPairArray.KillModForRep(ref this.d, modRep, owner, fromCallback);
			case 4:
				return global::ItemRepresentation.ItemModPairArray.KillModForRep(ref this.e, modRep, owner, fromCallback);
			default:
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000C9404 File Offset: 0x000C7604
		public void InstallMod(int slotNumber, global::ItemRepresentation owner, global::ItemModDataBlock datablock, global::CharacterStateFlags flags)
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

		// Token: 0x04001C27 RID: 7207
		private const int internalPairCount = 5;

		// Token: 0x04001C28 RID: 7208
		private global::ItemRepresentation.ItemModPair a;

		// Token: 0x04001C29 RID: 7209
		private global::ItemRepresentation.ItemModPair b;

		// Token: 0x04001C2A RID: 7210
		private global::ItemRepresentation.ItemModPair c;

		// Token: 0x04001C2B RID: 7211
		private global::ItemRepresentation.ItemModPair d;

		// Token: 0x04001C2C RID: 7212
		private global::ItemRepresentation.ItemModPair e;
	}
}
