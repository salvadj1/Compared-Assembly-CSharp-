using System;
using System.Collections.Generic;
using Facepunch;
using RustProto;
using RustProto.Helpers;
using uLink;
using UnityEngine;

// Token: 0x02000655 RID: 1621
[NGCAutoAddScript]
public class FireBarrel : LootableObject, IServerSaveable, IActivatable, IActivatableToggle, IContextRequestable, IContextRequestableMenu, IContextRequestableQuick, IContextRequestableText, IContextRequestablePointText, IComponentInterface<IActivatable, MonoBehaviour, Activatable>, IComponentInterface<IActivatable, MonoBehaviour>, IComponentInterface<IActivatable>, IComponentInterface<IContextRequestable, MonoBehaviour, Contextual>, IComponentInterface<IContextRequestable, MonoBehaviour>, IComponentInterface<IContextRequestable>
{
	// Token: 0x0600387F RID: 14463 RVA: 0x000CF72C File Offset: 0x000CD92C
	protected virtual float GetCookDuration()
	{
		return 60f;
	}

	// Token: 0x06003880 RID: 14464 RVA: 0x000CF734 File Offset: 0x000CD934
	public void Awake()
	{
		this._lightPosInitial = this.fireLight.transform.localPosition;
		this._lightPosCurrent = this._lightPosInitial;
		this._lightPosTarget = this._lightPosCurrent;
		this._lightIntensityInitial = this.fireLight.intensity;
	}

	// Token: 0x06003881 RID: 14465 RVA: 0x000CF780 File Offset: 0x000CD980
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

	// Token: 0x06003882 RID: 14466 RVA: 0x000CF7A8 File Offset: 0x000CD9A8
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

	// Token: 0x06003883 RID: 14467 RVA: 0x000CF7F8 File Offset: 0x000CD9F8
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

	// Token: 0x06003884 RID: 14468 RVA: 0x000CF874 File Offset: 0x000CDA74
	protected new void OnDestroy()
	{
		this.TurnOff();
		base.OnDestroy();
	}

	// Token: 0x06003885 RID: 14469 RVA: 0x000CF884 File Offset: 0x000CDA84
	private void PlayerUse(Controllable looter)
	{
		this.TrySetOn(!this.isOn);
	}

	// Token: 0x06003886 RID: 14470 RVA: 0x000CF898 File Offset: 0x000CDA98
	[RPC]
	protected void ReceiveNetState(bool on)
	{
		this.SetOn(on);
	}

	// Token: 0x06003887 RID: 14471 RVA: 0x000CF8A4 File Offset: 0x000CDAA4
	private void NewFlickerTarget()
	{
		this._lightFlickerTarget = this._lightIntensityInitial * Random.Range(0.75f, 1.25f);
	}

	// Token: 0x06003888 RID: 14472 RVA: 0x000CF8C4 File Offset: 0x000CDAC4
	public virtual bool HasFuel()
	{
		return this.FindFuel() != null;
	}

	// Token: 0x06003889 RID: 14473 RVA: 0x000CF8D4 File Offset: 0x000CDAD4
	public IFlammableItem FindFuel()
	{
		foreach (IFlammableItem flammableItem in this._inventory.FindItems<IFlammableItem>())
		{
			if (flammableItem.flammable)
			{
				return flammableItem;
			}
		}
		return null;
	}

	// Token: 0x0600388A RID: 14474 RVA: 0x000CF94C File Offset: 0x000CDB4C
	public void InvItemAdded()
	{
	}

	// Token: 0x0600388B RID: 14475 RVA: 0x000CF950 File Offset: 0x000CDB50
	public void InvItemRemoved()
	{
	}

	// Token: 0x0600388C RID: 14476 RVA: 0x000CF954 File Offset: 0x000CDB54
	public void FuelRemoveCheck()
	{
		if (!this.HasFuel())
		{
			this.SetOn(false);
		}
	}

	// Token: 0x0600388D RID: 14477 RVA: 0x000CF968 File Offset: 0x000CDB68
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

	// Token: 0x0600388E RID: 14478 RVA: 0x000CFA30 File Offset: 0x000CDC30
	protected IEnumerable<ContextActionPrototype> ContextQueryMenu_FireBarrel(Controllable controllable, ulong timestamp)
	{
		if (this._currentlyUsingPlayer == NetworkPlayer.unassigned)
		{
			if (this.isOn)
			{
				yield return FireBarrel.optionExtinguish;
			}
			else if (this.HasFuel())
			{
				yield return FireBarrel.optionIgnite;
			}
			yield return FireBarrel.optionOpen;
		}
		yield break;
	}

	// Token: 0x0600388F RID: 14479 RVA: 0x000CFA54 File Offset: 0x000CDC54
	protected ContextResponse ContextRespondQuick_FireBarrel(Controllable controllable, ulong timestamp)
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

	// Token: 0x06003890 RID: 14480 RVA: 0x000CFA94 File Offset: 0x000CDC94
	public virtual void TrySetOn(bool on)
	{
		this.SetOn(on);
	}

	// Token: 0x06003891 RID: 14481 RVA: 0x000CFAA0 File Offset: 0x000CDCA0
	protected ContextResponse ContextRespond_SetFireBarrelOn(Controllable controllable, ulong timestamp, bool turnOn)
	{
		if (this.isOn == turnOn)
		{
			return ContextResponse.DoneBreak;
		}
		if (this.isOn && !this.HasFuel())
		{
			return ContextResponse.FailBreak;
		}
		this.TrySetOn(!this.isOn);
		if (this.isOn != turnOn)
		{
			return ContextResponse.DoneBreak;
		}
		return ContextResponse.FailBreak;
	}

	// Token: 0x06003892 RID: 14482 RVA: 0x000CFAF4 File Offset: 0x000CDCF4
	protected ContextResponse ContextRespondMenu_FireBarrel(Controllable controllable, FireBarrel.FireBarrelPrototype action, ulong timestamp)
	{
		bool flag = action == FireBarrel.optionIgnite;
		if (flag || action == FireBarrel.optionExtinguish)
		{
			return this.ContextRespond_SetFireBarrelOn(controllable, timestamp, flag);
		}
		if (action == FireBarrel.optionOpen)
		{
			return this.ContextRespond_OpenLoot(controllable, timestamp);
		}
		return ContextResponse.FailBreak;
	}

	// Token: 0x06003893 RID: 14483 RVA: 0x000CFB3C File Offset: 0x000CDD3C
	public override string ContextText(Controllable localControllable)
	{
		if (this._currentlyUsingPlayer == NetworkPlayer.unassigned)
		{
			return "Use";
		}
		if (this.occupierText == null)
		{
			PlayerClient playerClient;
			if (!PlayerClient.Find(this._currentlyUsingPlayer, out playerClient))
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

	// Token: 0x06003894 RID: 14484 RVA: 0x000CFBB0 File Offset: 0x000CDDB0
	public override bool ContextTextPoint(out Vector3 worldPoint)
	{
		ContextRequestable.PointUtil.SpriteOrOrigin(this, out worldPoint);
		return true;
	}

	// Token: 0x06003895 RID: 14485 RVA: 0x000CFBBC File Offset: 0x000CDDBC
	public ActivationResult ActTrigger(Character instigator, ActivationToggleState toggleTarget, ulong timestamp)
	{
		if (toggleTarget != ActivationToggleState.On)
		{
			if (toggleTarget != ActivationToggleState.Off)
			{
				return ActivationResult.Fail_BadToggle;
			}
			if (!this.isOn)
			{
				return ActivationResult.Fail_Redundant;
			}
			this.PlayerUse(null);
			return (!this.isOn) ? ActivationResult.Success : ActivationResult.Fail_Busy;
		}
		else
		{
			if (this.isOn)
			{
				return ActivationResult.Fail_Redundant;
			}
			this.PlayerUse(null);
			return (!this.isOn) ? ActivationResult.Fail_Busy : ActivationResult.Success;
		}
	}

	// Token: 0x06003896 RID: 14486 RVA: 0x000CFC30 File Offset: 0x000CDE30
	public ActivationToggleState ActGetToggleState()
	{
		return (!this.isOn) ? ActivationToggleState.Off : ActivationToggleState.On;
	}

	// Token: 0x06003897 RID: 14487 RVA: 0x000CFC44 File Offset: 0x000CDE44
	public ActivationResult ActTrigger(Character instigator, ulong timestamp)
	{
		return this.ActTrigger(instigator, (!this.isOn) ? ActivationToggleState.On : ActivationToggleState.Off, timestamp);
	}

	// Token: 0x06003898 RID: 14488 RVA: 0x000CFC60 File Offset: 0x000CDE60
	public void WriteObjectSave(ref SavedObject.Builder saveobj)
	{
		using (Recycler<objectFireBarrel, objectFireBarrel.Builder> recycler = objectFireBarrel.Recycler())
		{
			objectFireBarrel.Builder builder = recycler.OpenBuilder();
			builder.SetOnFire(this.isOn);
			saveobj.SetFireBarrel(builder);
		}
	}

	// Token: 0x06003899 RID: 14489 RVA: 0x000CFCC0 File Offset: 0x000CDEC0
	public void ReadObjectSave(ref SavedObject saveobj)
	{
		if (!saveobj.HasFireBarrel)
		{
			return;
		}
		this.SetOn(saveobj.FireBarrel.OnFire);
	}

	// Token: 0x04001CC6 RID: 7366
	public Light fireLight;

	// Token: 0x04001CC7 RID: 7367
	public ParticleSystem[] emitters;

	// Token: 0x04001CC8 RID: 7368
	public bool isOn;

	// Token: 0x04001CC9 RID: 7369
	public bool startOn;

	// Token: 0x04001CCA RID: 7370
	private Vector3 _lightPosInitial;

	// Token: 0x04001CCB RID: 7371
	private Vector3 _lightPosCurrent;

	// Token: 0x04001CCC RID: 7372
	private Vector3 _lightPosTarget;

	// Token: 0x04001CCD RID: 7373
	private float _lightFlickerTarget = 1f;

	// Token: 0x04001CCE RID: 7374
	private float _lightIntensityInitial = 1f;

	// Token: 0x04001CCF RID: 7375
	public HeatZone _heatZone;

	// Token: 0x04001CD0 RID: 7376
	public static float decayResetRange = 5f;

	// Token: 0x04001CD1 RID: 7377
	private DeployableObject _deployable;

	// Token: 0x04001CD2 RID: 7378
	public int myTemp = 1;

	// Token: 0x04001CD3 RID: 7379
	private static readonly FireBarrel.FireBarrelPrototype optionIgnite = new FireBarrel.FireBarrelPrototype(FireBarrel.FireBarrelAction.Ignite);

	// Token: 0x04001CD4 RID: 7380
	private static readonly FireBarrel.FireBarrelPrototype optionExtinguish = new FireBarrel.FireBarrelPrototype(FireBarrel.FireBarrelAction.Extinguish);

	// Token: 0x04001CD5 RID: 7381
	private static readonly FireBarrel.FireBarrelPrototype optionOpen = new FireBarrel.FireBarrelPrototype(FireBarrel.FireBarrelAction.Open);

	// Token: 0x02000656 RID: 1622
	public static class DefaultItems
	{
		// Token: 0x04001CD6 RID: 7382
		public static Datablock.Ident fuel = "Wood";

		// Token: 0x04001CD7 RID: 7383
		public static Datablock.Ident byProduct = "Charcoal";
	}

	// Token: 0x02000657 RID: 1623
	protected enum FireBarrelAction
	{
		// Token: 0x04001CD9 RID: 7385
		Ignite,
		// Token: 0x04001CDA RID: 7386
		Extinguish,
		// Token: 0x04001CDB RID: 7387
		Open
	}

	// Token: 0x02000658 RID: 1624
	protected class FireBarrelPrototype : ContextActionPrototype
	{
		// Token: 0x0600389B RID: 14491 RVA: 0x000CFD0C File Offset: 0x000CDF0C
		public FireBarrelPrototype(FireBarrel.FireBarrelAction action)
		{
			this.name = (int)action;
			this.text = action.ToString();
			this.action = action;
		}

		// Token: 0x04001CDC RID: 7388
		public FireBarrel.FireBarrelAction action;
	}
}
