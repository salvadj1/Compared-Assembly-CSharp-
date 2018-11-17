using System;
using System.Collections.Generic;
using Facepunch;
using RustProto;
using RustProto.Helpers;
using uLink;
using UnityEngine;

// Token: 0x02000718 RID: 1816
[global::NGCAutoAddScript]
public class FireBarrel : global::LootableObject, global::IServerSaveable, global::IActivatable, global::IActivatableToggle, global::IContextRequestable, global::IContextRequestableMenu, global::IContextRequestableQuick, global::IContextRequestableText, global::IContextRequestablePointText, global::IComponentInterface<global::IActivatable, MonoBehaviour, global::Activatable>, global::IComponentInterface<global::IActivatable, MonoBehaviour>, global::IComponentInterface<global::IActivatable>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour, global::Contextual>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour>, global::IComponentInterface<global::IContextRequestable>
{
	// Token: 0x06003C6B RID: 15467 RVA: 0x000D7FDC File Offset: 0x000D61DC
	protected virtual float GetCookDuration()
	{
		return 60f;
	}

	// Token: 0x06003C6C RID: 15468 RVA: 0x000D7FE4 File Offset: 0x000D61E4
	public void Awake()
	{
		this._lightPosInitial = this.fireLight.transform.localPosition;
		this._lightPosCurrent = this._lightPosInitial;
		this._lightPosTarget = this._lightPosCurrent;
		this._lightIntensityInitial = this.fireLight.intensity;
	}

	// Token: 0x06003C6D RID: 15469 RVA: 0x000D8030 File Offset: 0x000D6230
	public void SetOn(bool on)
	{
		this.isOn = on;
		if (this.isOn)
		{
			this.TurnOn();
		}
		else
		{
			this.TurnOff();
		}
	}

	// Token: 0x06003C6E RID: 15470 RVA: 0x000D8058 File Offset: 0x000D6258
	private void TurnOn()
	{
		this.fireLight.enabled = true;
		foreach (ParticleSystem particleSystem in this.emitters)
		{
			particleSystem.Play();
		}
		base.audio.Play();
		this.NewFlickerTarget();
	}

	// Token: 0x06003C6F RID: 15471 RVA: 0x000D80A8 File Offset: 0x000D62A8
	private void TurnOff()
	{
		if (base.audio)
		{
			base.audio.Stop();
		}
		foreach (ParticleSystem particleSystem in this.emitters)
		{
			particleSystem.Stop();
		}
		if (this.fireLight)
		{
			this.fireLight.enabled = false;
			this.fireLight.intensity = 0f;
		}
	}

	// Token: 0x06003C70 RID: 15472 RVA: 0x000D8124 File Offset: 0x000D6324
	protected new void OnDestroy()
	{
		this.TurnOff();
		base.OnDestroy();
	}

	// Token: 0x06003C71 RID: 15473 RVA: 0x000D8134 File Offset: 0x000D6334
	private void PlayerUse(global::Controllable looter)
	{
		this.TrySetOn(!this.isOn);
	}

	// Token: 0x06003C72 RID: 15474 RVA: 0x000D8148 File Offset: 0x000D6348
	[RPC]
	protected void ReceiveNetState(bool on)
	{
		this.SetOn(on);
	}

	// Token: 0x06003C73 RID: 15475 RVA: 0x000D8154 File Offset: 0x000D6354
	private void NewFlickerTarget()
	{
		this._lightFlickerTarget = this._lightIntensityInitial * Random.Range(0.75f, 1.25f);
	}

	// Token: 0x06003C74 RID: 15476 RVA: 0x000D8174 File Offset: 0x000D6374
	public virtual bool HasFuel()
	{
		return this.FindFuel() != null;
	}

	// Token: 0x06003C75 RID: 15477 RVA: 0x000D8184 File Offset: 0x000D6384
	public global::IFlammableItem FindFuel()
	{
		foreach (global::IFlammableItem flammableItem in this._inventory.FindItems<global::IFlammableItem>())
		{
			if (flammableItem.flammable)
			{
				return flammableItem;
			}
		}
		return null;
	}

	// Token: 0x06003C76 RID: 15478 RVA: 0x000D81FC File Offset: 0x000D63FC
	public void InvItemAdded()
	{
	}

	// Token: 0x06003C77 RID: 15479 RVA: 0x000D8200 File Offset: 0x000D6400
	public void InvItemRemoved()
	{
	}

	// Token: 0x06003C78 RID: 15480 RVA: 0x000D8204 File Offset: 0x000D6404
	public void FuelRemoveCheck()
	{
		if (!this.HasFuel())
		{
			this.SetOn(false);
		}
	}

	// Token: 0x06003C79 RID: 15481 RVA: 0x000D8218 File Offset: 0x000D6418
	public void Update()
	{
		if (this.isOn)
		{
			if (this.fireLight.transform.localPosition == this._lightPosTarget)
			{
				this._lightPosTarget = this._lightPosInitial + Random.insideUnitSphere * 10f;
				this._lightPosCurrent = this.fireLight.transform.localPosition;
			}
			this.fireLight.intensity = Mathf.Lerp(this.fireLight.intensity, this._lightFlickerTarget, Time.deltaTime * 10f);
			if ((double)Mathf.Abs(this.fireLight.intensity - this._lightFlickerTarget) < 0.05)
			{
				this.NewFlickerTarget();
			}
		}
	}

	// Token: 0x06003C7A RID: 15482 RVA: 0x000D82E0 File Offset: 0x000D64E0
	protected IEnumerable<global::ContextActionPrototype> ContextQueryMenu_FireBarrel(global::Controllable controllable, ulong timestamp)
	{
		if (this._currentlyUsingPlayer == uLink.NetworkPlayer.unassigned)
		{
			if (this.isOn)
			{
				yield return global::FireBarrel.optionExtinguish;
			}
			else if (this.HasFuel())
			{
				yield return global::FireBarrel.optionIgnite;
			}
			yield return global::FireBarrel.optionOpen;
		}
		yield break;
	}

	// Token: 0x06003C7B RID: 15483 RVA: 0x000D8304 File Offset: 0x000D6504
	protected global::ContextResponse ContextRespondQuick_FireBarrel(global::Controllable controllable, ulong timestamp)
	{
		if (this.isOn)
		{
			return this.ContextRespond_SetFireBarrelOn(controllable, timestamp, false);
		}
		if (this.HasFuel())
		{
			return this.ContextRespond_SetFireBarrelOn(controllable, timestamp, true);
		}
		return this.ContextRespond_OpenLoot(controllable, timestamp);
	}

	// Token: 0x06003C7C RID: 15484 RVA: 0x000D8344 File Offset: 0x000D6544
	public virtual void TrySetOn(bool on)
	{
		this.SetOn(on);
	}

	// Token: 0x06003C7D RID: 15485 RVA: 0x000D8350 File Offset: 0x000D6550
	protected global::ContextResponse ContextRespond_SetFireBarrelOn(global::Controllable controllable, ulong timestamp, bool turnOn)
	{
		if (this.isOn == turnOn)
		{
			return global::ContextResponse.DoneBreak;
		}
		if (this.isOn && !this.HasFuel())
		{
			return global::ContextResponse.FailBreak;
		}
		this.TrySetOn(!this.isOn);
		if (this.isOn != turnOn)
		{
			return global::ContextResponse.DoneBreak;
		}
		return global::ContextResponse.FailBreak;
	}

	// Token: 0x06003C7E RID: 15486 RVA: 0x000D83A4 File Offset: 0x000D65A4
	protected global::ContextResponse ContextRespondMenu_FireBarrel(global::Controllable controllable, global::FireBarrel.FireBarrelPrototype action, ulong timestamp)
	{
		bool flag = action == global::FireBarrel.optionIgnite;
		if (flag || action == global::FireBarrel.optionExtinguish)
		{
			return this.ContextRespond_SetFireBarrelOn(controllable, timestamp, flag);
		}
		if (action == global::FireBarrel.optionOpen)
		{
			return this.ContextRespond_OpenLoot(controllable, timestamp);
		}
		return global::ContextResponse.FailBreak;
	}

	// Token: 0x06003C7F RID: 15487 RVA: 0x000D83EC File Offset: 0x000D65EC
	public override string ContextText(global::Controllable localControllable)
	{
		if (this._currentlyUsingPlayer == uLink.NetworkPlayer.unassigned)
		{
			return "Use";
		}
		if (this.occupierText == null)
		{
			global::PlayerClient playerClient;
			if (!global::PlayerClient.Find(this._currentlyUsingPlayer, out playerClient))
			{
				this.occupierText = "Occupied";
			}
			else
			{
				this.occupierText = string.Format("Occupied by {0}", playerClient.userName);
			}
		}
		return this.occupierText;
	}

	// Token: 0x06003C80 RID: 15488 RVA: 0x000D8460 File Offset: 0x000D6660
	public override bool ContextTextPoint(out Vector3 worldPoint)
	{
		global::ContextRequestable.PointUtil.SpriteOrOrigin(this, out worldPoint);
		return true;
	}

	// Token: 0x06003C81 RID: 15489 RVA: 0x000D846C File Offset: 0x000D666C
	public global::ActivationResult ActTrigger(global::Character instigator, global::ActivationToggleState toggleTarget, ulong timestamp)
	{
		if (toggleTarget != global::ActivationToggleState.On)
		{
			if (toggleTarget != global::ActivationToggleState.Off)
			{
				return global::ActivationResult.Fail_BadToggle;
			}
			if (!this.isOn)
			{
				return global::ActivationResult.Fail_Redundant;
			}
			this.PlayerUse(null);
			return (!this.isOn) ? global::ActivationResult.Success : global::ActivationResult.Fail_Busy;
		}
		else
		{
			if (this.isOn)
			{
				return global::ActivationResult.Fail_Redundant;
			}
			this.PlayerUse(null);
			return (!this.isOn) ? global::ActivationResult.Fail_Busy : global::ActivationResult.Success;
		}
	}

	// Token: 0x06003C82 RID: 15490 RVA: 0x000D84E0 File Offset: 0x000D66E0
	public global::ActivationToggleState ActGetToggleState()
	{
		return (!this.isOn) ? global::ActivationToggleState.Off : global::ActivationToggleState.On;
	}

	// Token: 0x06003C83 RID: 15491 RVA: 0x000D84F4 File Offset: 0x000D66F4
	public global::ActivationResult ActTrigger(global::Character instigator, ulong timestamp)
	{
		return this.ActTrigger(instigator, (!this.isOn) ? global::ActivationToggleState.On : global::ActivationToggleState.Off, timestamp);
	}

	// Token: 0x06003C84 RID: 15492 RVA: 0x000D8510 File Offset: 0x000D6710
	public void WriteObjectSave(ref RustProto.SavedObject.Builder saveobj)
	{
		using (RustProto.Helpers.Recycler<RustProto.objectFireBarrel, RustProto.objectFireBarrel.Builder> recycler = RustProto.objectFireBarrel.Recycler())
		{
			RustProto.objectFireBarrel.Builder builder = recycler.OpenBuilder();
			builder.SetOnFire(this.isOn);
			saveobj.SetFireBarrel(builder);
		}
	}

	// Token: 0x06003C85 RID: 15493 RVA: 0x000D8570 File Offset: 0x000D6770
	public void ReadObjectSave(ref RustProto.SavedObject saveobj)
	{
		if (!saveobj.HasFireBarrel)
		{
			return;
		}
		this.SetOn(saveobj.FireBarrel.OnFire);
	}

	// Token: 0x04001EBB RID: 7867
	public Light fireLight;

	// Token: 0x04001EBC RID: 7868
	public ParticleSystem[] emitters;

	// Token: 0x04001EBD RID: 7869
	public bool isOn;

	// Token: 0x04001EBE RID: 7870
	public bool startOn;

	// Token: 0x04001EBF RID: 7871
	private Vector3 _lightPosInitial;

	// Token: 0x04001EC0 RID: 7872
	private Vector3 _lightPosCurrent;

	// Token: 0x04001EC1 RID: 7873
	private Vector3 _lightPosTarget;

	// Token: 0x04001EC2 RID: 7874
	private float _lightFlickerTarget = 1f;

	// Token: 0x04001EC3 RID: 7875
	private float _lightIntensityInitial = 1f;

	// Token: 0x04001EC4 RID: 7876
	public global::HeatZone _heatZone;

	// Token: 0x04001EC5 RID: 7877
	public static float decayResetRange = 5f;

	// Token: 0x04001EC6 RID: 7878
	private global::DeployableObject _deployable;

	// Token: 0x04001EC7 RID: 7879
	public int myTemp = 1;

	// Token: 0x04001EC8 RID: 7880
	private static readonly global::FireBarrel.FireBarrelPrototype optionIgnite = new global::FireBarrel.FireBarrelPrototype(global::FireBarrel.FireBarrelAction.Ignite);

	// Token: 0x04001EC9 RID: 7881
	private static readonly global::FireBarrel.FireBarrelPrototype optionExtinguish = new global::FireBarrel.FireBarrelPrototype(global::FireBarrel.FireBarrelAction.Extinguish);

	// Token: 0x04001ECA RID: 7882
	private static readonly global::FireBarrel.FireBarrelPrototype optionOpen = new global::FireBarrel.FireBarrelPrototype(global::FireBarrel.FireBarrelAction.Open);

	// Token: 0x02000719 RID: 1817
	public static class DefaultItems
	{
		// Token: 0x04001ECB RID: 7883
		public static global::Datablock.Ident fuel = "Wood";

		// Token: 0x04001ECC RID: 7884
		public static global::Datablock.Ident byProduct = "Charcoal";
	}

	// Token: 0x0200071A RID: 1818
	protected enum FireBarrelAction
	{
		// Token: 0x04001ECE RID: 7886
		Ignite,
		// Token: 0x04001ECF RID: 7887
		Extinguish,
		// Token: 0x04001ED0 RID: 7888
		Open
	}

	// Token: 0x0200071B RID: 1819
	protected class FireBarrelPrototype : global::ContextActionPrototype
	{
		// Token: 0x06003C87 RID: 15495 RVA: 0x000D85BC File Offset: 0x000D67BC
		public FireBarrelPrototype(global::FireBarrel.FireBarrelAction action)
		{
			this.name = (int)action;
			this.text = action.ToString();
			this.action = action;
		}

		// Token: 0x04001ED1 RID: 7889
		public global::FireBarrel.FireBarrelAction action;
	}
}
