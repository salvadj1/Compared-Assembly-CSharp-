using System;
using uLink;
using UnityEngine;

// Token: 0x02000583 RID: 1411
public class BaseWeaponModDataBlock : ItemModDataBlock
{
	// Token: 0x060031B0 RID: 12720 RVA: 0x000BC128 File Offset: 0x000BA328
	protected BaseWeaponModDataBlock(Type minimumItemModRepresentationType) : base(minimumItemModRepresentationType)
	{
		if (!typeof(WeaponModRep).IsAssignableFrom(minimumItemModRepresentationType))
		{
			throw new ArgumentOutOfRangeException("minimumItemModRepresentationType", minimumItemModRepresentationType, "!typeof(WeaponModRep).IsAssignableFrom(minimumItemModRepresentationType)");
		}
	}

	// Token: 0x060031B1 RID: 12721 RVA: 0x000BC184 File Offset: 0x000BA384
	public BaseWeaponModDataBlock() : this(typeof(WeaponModRep))
	{
	}

	// Token: 0x060031B2 RID: 12722 RVA: 0x000BC198 File Offset: 0x000BA398
	protected override IInventoryItem ConstructItem()
	{
		return new BaseWeaponModDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x060031B3 RID: 12723 RVA: 0x000BC1A0 File Offset: 0x000BA3A0
	protected override void InstallToItemModRepresentation(ItemModRepresentation modRep)
	{
		base.InstallToItemModRepresentation(modRep);
		if (this.attachObjectRep != null)
		{
			GameObject gameObject = modRep.itemRep.muzzle.InstantiateAsChild(this.attachObjectRep, false);
			gameObject.name = this.attachObjectRep.name;
			((WeaponModRep)modRep).SetAttached(gameObject, false);
		}
	}

	// Token: 0x060031B4 RID: 12724 RVA: 0x000BC1FC File Offset: 0x000BA3FC
	protected override void UninstallFromItemModRepresentation(ItemModRepresentation rep)
	{
		WeaponModRep weaponModRep = (WeaponModRep)rep;
		GameObject attached = weaponModRep.attached;
		if (attached)
		{
			weaponModRep.SetAttached(null, false);
			Object.Destroy(attached);
		}
		base.UninstallFromItemModRepresentation(rep);
	}

	// Token: 0x060031B5 RID: 12725 RVA: 0x000BC238 File Offset: 0x000BA438
	protected override bool InstallToViewModel(ref ModViewModelAddArgs a)
	{
		if (this.isMesh && !a.isMesh)
		{
			return base.InstallToViewModel(ref a);
		}
		if (!this.isMesh && a.isMesh)
		{
			return base.InstallToViewModel(ref a);
		}
		if (a.vm == null)
		{
			Debug.Log("Viewmodel null for item attachment...");
		}
		if (this.attachObjectVM != null)
		{
			WeaponModRep weaponModRep = (WeaponModRep)a.modRep;
			GameObject gameObject;
			if (a.isMesh)
			{
				Socket socketByName = this.GetSocketByName(a.vm, this.attachSocketName);
				gameObject = (Object.Instantiate(this.attachObjectVM, socketByName.offset, Quaternion.Euler(socketByName.eulerRotate)) as GameObject);
				gameObject.transform.parent = socketByName.parent;
				gameObject.transform.localPosition = socketByName.offset;
				gameObject.transform.localEulerAngles = socketByName.eulerRotate;
			}
			else
			{
				gameObject = this.GetSocketByName(a.vm, this.attachSocketName).InstantiateAsChild(this.attachObjectVM, true);
			}
			gameObject.name = this.attachObjectVM.name;
			weaponModRep.SetAttached(gameObject, true);
			ViewModelAttachment component = gameObject.GetComponent<ViewModelAttachment>();
			if (component)
			{
				if (this.socketOverrideName != string.Empty && component is VMAttachmentSocketOverride)
				{
					VMAttachmentSocketOverride vmattachmentSocketOverride = (VMAttachmentSocketOverride)component;
					this.SetSocketByname(a.vm, this.socketOverrideName, vmattachmentSocketOverride.socketOverride);
					if (this.modifyZoomOffset)
					{
						a.vm.punchScalar = this.punchScalar;
						a.vm.zoomOffset.z = this.zoomOffsetZ;
					}
				}
				component.viewModel = a.vm;
			}
		}
		return true;
	}

	// Token: 0x060031B6 RID: 12726 RVA: 0x000BC3FC File Offset: 0x000BA5FC
	protected override void UninstallFromViewModel(ref ModViewModelRemoveArgs a)
	{
		if (this.attachObjectVM != null)
		{
			WeaponModRep weaponModRep = (WeaponModRep)a.modRep;
			GameObject attached = weaponModRep.attached;
			ViewModelAttachment component = attached.GetComponent<ViewModelAttachment>();
			if (component)
			{
				component.viewModel = null;
			}
			Socket socketByName = this.GetSocketByName(a.vm, this.attachSocketName);
			Transform transform = socketByName.attachParent;
			if (transform == null)
			{
				transform = socketByName.parent;
			}
			if (attached)
			{
				weaponModRep.SetAttached(null, true);
				Object.Destroy(attached.gameObject);
			}
		}
	}

	// Token: 0x060031B7 RID: 12727 RVA: 0x000BC494 File Offset: 0x000BA694
	public void SetSocketByname(Socket.Mapped vm, string name, Socket newSocket)
	{
		vm.socketMap.ReplaceSocket(name, newSocket);
	}

	// Token: 0x060031B8 RID: 12728 RVA: 0x000BC4A4 File Offset: 0x000BA6A4
	public Socket GetSocketByName(Socket.Mapped vm, string name)
	{
		return vm.socketMap[name].socket;
	}

	// Token: 0x060031B9 RID: 12729 RVA: 0x000BC4C8 File Offset: 0x000BA6C8
	protected override void SecureWriteMemberValues(BitStream stream)
	{
		base.SecureWriteMemberValues(stream);
		stream.Write<string>(this.socketOverrideName, new object[0]);
		stream.Write<float>(this.zoomOffsetZ, new object[0]);
		stream.Write<bool>(this.isMesh, new object[0]);
		stream.Write<float>(this.punchScalar, new object[0]);
		stream.Write<bool>(this.modifyZoomOffset, new object[0]);
	}

	// Token: 0x040019D2 RID: 6610
	public string attachSocketName = "muzzle";

	// Token: 0x040019D3 RID: 6611
	public GameObject attachObjectVM;

	// Token: 0x040019D4 RID: 6612
	public GameObject attachObjectRep;

	// Token: 0x040019D5 RID: 6613
	public bool isMesh;

	// Token: 0x040019D6 RID: 6614
	public string socketOverrideName = string.Empty;

	// Token: 0x040019D7 RID: 6615
	public float punchScalar = 1f;

	// Token: 0x040019D8 RID: 6616
	public float zoomOffsetZ;

	// Token: 0x040019D9 RID: 6617
	public bool modifyZoomOffset;

	// Token: 0x02000584 RID: 1412
	private sealed class ITEM_TYPE : ItemModItem<BaseWeaponModDataBlock>, IInventoryItem, IItemModItem
	{
		// Token: 0x060031BA RID: 12730 RVA: 0x000BC538 File Offset: 0x000BA738
		public ITEM_TYPE(BaseWeaponModDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060031BB RID: 12731 RVA: 0x000BC544 File Offset: 0x000BA744
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x000BC54C File Offset: 0x000BA74C
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000BC554 File Offset: 0x000BA754
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x000BC55C File Offset: 0x000BA75C
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x000BC564 File Offset: 0x000BA764
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x000BC570 File Offset: 0x000BA770
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000BC57C File Offset: 0x000BA77C
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x000BC588 File Offset: 0x000BA788
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000BC594 File Offset: 0x000BA794
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000BC5A0 File Offset: 0x000BA7A0
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000BC5AC File Offset: 0x000BA7AC
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x000BC5B8 File Offset: 0x000BA7B8
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x000BC5C4 File Offset: 0x000BA7C4
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x000BC5CC File Offset: 0x000BA7CC
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000BC5D4 File Offset: 0x000BA7D4
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000BC5DC File Offset: 0x000BA7DC
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000BC5E4 File Offset: 0x000BA7E4
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000BC5EC File Offset: 0x000BA7EC
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000BC5F4 File Offset: 0x000BA7F4
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000BC5FC File Offset: 0x000BA7FC
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000BC604 File Offset: 0x000BA804
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x000BC610 File Offset: 0x000BA810
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x000BC618 File Offset: 0x000BA818
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x000BC620 File Offset: 0x000BA820
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x000BC628 File Offset: 0x000BA828
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000BC630 File Offset: 0x000BA830
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x000BC638 File Offset: 0x000BA838
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000BC640 File Offset: 0x000BA840
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
