using System;
using uLink;
using UnityEngine;

// Token: 0x02000641 RID: 1601
public class BaseWeaponModDataBlock : global::ItemModDataBlock
{
	// Token: 0x06003578 RID: 13688 RVA: 0x000C4384 File Offset: 0x000C2584
	protected BaseWeaponModDataBlock(Type minimumItemModRepresentationType) : base(minimumItemModRepresentationType)
	{
		if (!typeof(global::WeaponModRep).IsAssignableFrom(minimumItemModRepresentationType))
		{
			throw new ArgumentOutOfRangeException("minimumItemModRepresentationType", minimumItemModRepresentationType, "!typeof(WeaponModRep).IsAssignableFrom(minimumItemModRepresentationType)");
		}
	}

	// Token: 0x06003579 RID: 13689 RVA: 0x000C43E0 File Offset: 0x000C25E0
	public BaseWeaponModDataBlock() : this(typeof(global::WeaponModRep))
	{
	}

	// Token: 0x0600357A RID: 13690 RVA: 0x000C43F4 File Offset: 0x000C25F4
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::BaseWeaponModDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x0600357B RID: 13691 RVA: 0x000C43FC File Offset: 0x000C25FC
	protected override void InstallToItemModRepresentation(global::ItemModRepresentation modRep)
	{
		base.InstallToItemModRepresentation(modRep);
		if (this.attachObjectRep != null)
		{
			GameObject gameObject = modRep.itemRep.muzzle.InstantiateAsChild(this.attachObjectRep, false);
			gameObject.name = this.attachObjectRep.name;
			((global::WeaponModRep)modRep).SetAttached(gameObject, false);
		}
	}

	// Token: 0x0600357C RID: 13692 RVA: 0x000C4458 File Offset: 0x000C2658
	protected override void UninstallFromItemModRepresentation(global::ItemModRepresentation rep)
	{
		global::WeaponModRep weaponModRep = (global::WeaponModRep)rep;
		GameObject attached = weaponModRep.attached;
		if (attached)
		{
			weaponModRep.SetAttached(null, false);
			Object.Destroy(attached);
		}
		base.UninstallFromItemModRepresentation(rep);
	}

	// Token: 0x0600357D RID: 13693 RVA: 0x000C4494 File Offset: 0x000C2694
	protected override bool InstallToViewModel(ref global::ModViewModelAddArgs a)
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
			global::WeaponModRep weaponModRep = (global::WeaponModRep)a.modRep;
			GameObject gameObject;
			if (a.isMesh)
			{
				global::Socket socketByName = this.GetSocketByName(a.vm, this.attachSocketName);
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
			global::ViewModelAttachment component = gameObject.GetComponent<global::ViewModelAttachment>();
			if (component)
			{
				if (this.socketOverrideName != string.Empty && component is global::VMAttachmentSocketOverride)
				{
					global::VMAttachmentSocketOverride vmattachmentSocketOverride = (global::VMAttachmentSocketOverride)component;
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

	// Token: 0x0600357E RID: 13694 RVA: 0x000C4658 File Offset: 0x000C2858
	protected override void UninstallFromViewModel(ref global::ModViewModelRemoveArgs a)
	{
		if (this.attachObjectVM != null)
		{
			global::WeaponModRep weaponModRep = (global::WeaponModRep)a.modRep;
			GameObject attached = weaponModRep.attached;
			global::ViewModelAttachment component = attached.GetComponent<global::ViewModelAttachment>();
			if (component)
			{
				component.viewModel = null;
			}
			global::Socket socketByName = this.GetSocketByName(a.vm, this.attachSocketName);
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

	// Token: 0x0600357F RID: 13695 RVA: 0x000C46F0 File Offset: 0x000C28F0
	public void SetSocketByname(global::Socket.Mapped vm, string name, global::Socket newSocket)
	{
		vm.socketMap.ReplaceSocket(name, newSocket);
	}

	// Token: 0x06003580 RID: 13696 RVA: 0x000C4700 File Offset: 0x000C2900
	public global::Socket GetSocketByName(global::Socket.Mapped vm, string name)
	{
		return vm.socketMap[name].socket;
	}

	// Token: 0x06003581 RID: 13697 RVA: 0x000C4724 File Offset: 0x000C2924
	protected override void SecureWriteMemberValues(BitStream stream)
	{
		base.SecureWriteMemberValues(stream);
		stream.Write<string>(this.socketOverrideName, new object[0]);
		stream.Write<float>(this.zoomOffsetZ, new object[0]);
		stream.Write<bool>(this.isMesh, new object[0]);
		stream.Write<float>(this.punchScalar, new object[0]);
		stream.Write<bool>(this.modifyZoomOffset, new object[0]);
	}

	// Token: 0x04001BA3 RID: 7075
	public string attachSocketName = "muzzle";

	// Token: 0x04001BA4 RID: 7076
	public GameObject attachObjectVM;

	// Token: 0x04001BA5 RID: 7077
	public GameObject attachObjectRep;

	// Token: 0x04001BA6 RID: 7078
	public bool isMesh;

	// Token: 0x04001BA7 RID: 7079
	public string socketOverrideName = string.Empty;

	// Token: 0x04001BA8 RID: 7080
	public float punchScalar = 1f;

	// Token: 0x04001BA9 RID: 7081
	public float zoomOffsetZ;

	// Token: 0x04001BAA RID: 7082
	public bool modifyZoomOffset;

	// Token: 0x02000642 RID: 1602
	private sealed class ITEM_TYPE : global::ItemModItem<global::BaseWeaponModDataBlock>, global::IInventoryItem, global::IItemModItem
	{
		// Token: 0x06003582 RID: 13698 RVA: 0x000C4794 File Offset: 0x000C2994
		public ITEM_TYPE(global::BaseWeaponModDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06003583 RID: 13699 RVA: 0x000C47A0 File Offset: 0x000C29A0
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x000C47A8 File Offset: 0x000C29A8
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x000C47B0 File Offset: 0x000C29B0
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x000C47B8 File Offset: 0x000C29B8
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x000C47C0 File Offset: 0x000C29C0
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x000C47CC File Offset: 0x000C29CC
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x000C47D8 File Offset: 0x000C29D8
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x000C47E4 File Offset: 0x000C29E4
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x000C47F0 File Offset: 0x000C29F0
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x000C47FC File Offset: 0x000C29FC
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x000C4808 File Offset: 0x000C2A08
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x000C4814 File Offset: 0x000C2A14
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x000C4820 File Offset: 0x000C2A20
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x000C4828 File Offset: 0x000C2A28
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x000C4830 File Offset: 0x000C2A30
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x000C4838 File Offset: 0x000C2A38
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x000C4840 File Offset: 0x000C2A40
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x000C4848 File Offset: 0x000C2A48
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000C4850 File Offset: 0x000C2A50
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x000C4858 File Offset: 0x000C2A58
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x000C4860 File Offset: 0x000C2A60
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000C486C File Offset: 0x000C2A6C
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x000C4874 File Offset: 0x000C2A74
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x000C487C File Offset: 0x000C2A7C
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x000C4884 File Offset: 0x000C2A84
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x000C488C File Offset: 0x000C2A8C
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x000C4894 File Offset: 0x000C2A94
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x000C489C File Offset: 0x000C2A9C
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
