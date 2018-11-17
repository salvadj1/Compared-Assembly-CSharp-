using System;
using Facepunch.Clocks.Counters;
using Facepunch.Cursor;
using uLink;
using UnityEngine;

// Token: 0x020000B5 RID: 181
public class HumanController : global::Controller, global::RagdollTransferInfoProvider
{
	// Token: 0x060003EB RID: 1003 RVA: 0x00012870 File Offset: 0x00010A70
	public HumanController() : this((global::Controller.ControllerFlags)8385)
	{
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x00012880 File Offset: 0x00010A80
	protected HumanController(global::Controller.ControllerFlags controllerFlags) : base(controllerFlags)
	{
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x060003ED RID: 1005 RVA: 0x000128D0 File Offset: 0x00010AD0
	global::RagdollTransferInfo global::RagdollTransferInfoProvider.RagdollTransferInfo
	{
		get
		{
			return "RustPlayer_Pelvis/RustPlayer_Spine/RustPlayer_Spine1/RustPlayer_Spine2/RustPlayer_Spine4/RustPlayer_Neck1/RustPlayer_Head1";
		}
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x000128DC File Offset: 0x00010ADC
	private void CheckBeltUsage()
	{
		if (global::UIUnityEvents.shouldBlockButtonInput)
		{
			return;
		}
		if (!base.enabled)
		{
			return;
		}
		if (global::ConsoleWindow.IsVisible())
		{
			return;
		}
		global::Inventory inventory = this.inventory;
		if (!inventory)
		{
			return;
		}
		global::InventoryHolder inventoryHolder = inventory.inventoryHolder;
		if (!inventoryHolder)
		{
			return;
		}
		int num = global::HumanController.InputSample.PollItemButtons();
		if (num != -1)
		{
			inventoryHolder.BeltUse(num);
		}
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x00012948 File Offset: 0x00010B48
	protected void UpdateInput()
	{
		global::InventoryHolder inventoryHolder = this.inventoryHolder;
		global::PlayerClient.InputFunction(base.gameObject);
		bool noLamp;
		bool noLaser;
		if (inventoryHolder)
		{
			global::ItemModFlags modFlags = inventoryHolder.modFlags;
			noLamp = ((modFlags & global::ItemModFlags.Lamp) == global::ItemModFlags.Other);
			noLaser = ((modFlags & global::ItemModFlags.Laser) == global::ItemModFlags.Other);
		}
		else
		{
			noLaser = (noLamp = true);
		}
		global::HumanController.InputSample inputSample = global::HumanController.InputSample.Poll(noLamp, noLaser);
		inputSample.info__crouchBlocked = this.crouch_was_blocked;
		bool flag = base.GetLocal<global::FallDamage>().GetLegInjury() > 0f;
		if (flag)
		{
			inputSample.crouch = true;
			inputSample.jump = false;
		}
		if (inputSample.walk <= 0f || Mathf.Abs(inputSample.strafe) >= 0.05f || inputSample.attack2 || this._inventory.isCrafting || flag)
		{
			inputSample.sprint = false;
		}
		float num = 1f;
		if (this._inventory.isCrafting)
		{
			num *= 0.5f;
		}
		if (flag)
		{
			num *= 0.5f;
		}
		global::HumanController.InputSample.MovementScale = num;
		if (inventoryHolder)
		{
			object item = inventoryHolder.InvokeInputItemPreFrame(ref inputSample);
			this.ProcessInput(ref inputSample);
			inventoryHolder.InvokeInputItemPostFrame(item, ref inputSample);
		}
		else
		{
			this.ProcessInput(ref inputSample);
		}
		this.CheckBeltUsage();
		if (this.wasSprinting && !inputSample.sprint)
		{
			this.SprintingStopped();
		}
		else if (!this.wasSprinting && inputSample.sprint)
		{
			this.SprintingStarted();
		}
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00012AD8 File Offset: 0x00010CD8
	private void ProcessInput(ref global::HumanController.InputSample sample)
	{
		global::CCMotor ccmotor = base.ccmotor;
		bool flag;
		bool flag2;
		if (ccmotor)
		{
			flag = ccmotor.isGrounded;
			flag2 = ccmotor.isSliding;
			if (!flag && !flag2)
			{
				sample.sprint = false;
				sample.crouch = false;
				sample.aim = false;
				sample.info__crouchBlocked = false;
				if (!this.wasInAir)
				{
					this.wasInAir = true;
					this.magnitudeAir = ccmotor.input.moveDirection.magnitude;
					this.midairStartPos = base.transform.position;
				}
				this.lastFrameVelocity = ccmotor.velocity;
			}
			else if (this.wasInAir)
			{
				this.wasInAir = false;
				this.magnitudeAir = 1f;
				this.landingSpeedPenaltyTime = 0f;
				if (base.transform.position.y < this.midairStartPos.y && Mathf.Abs(base.transform.position.y - this.midairStartPos.y) > 2f)
				{
					base.idMain.GetLocal<global::FallDamage>().SendFallImpact(this.lastFrameVelocity);
				}
				this.lastFrameVelocity = Vector3.zero;
				this.midairStartPos = Vector3.zero;
			}
			bool flag3 = sample.crouch || sample.info__crouchBlocked;
			global::CCMotor.InputFrame input;
			input.jump = sample.jump;
			input.moveDirection.x = sample.strafe;
			input.moveDirection.y = 0f;
			input.moveDirection.z = sample.walk;
			input.crouchSpeed = ((!sample.crouch) ? 1f : -1f);
			if (input.moveDirection != Vector3.zero)
			{
				float num = input.moveDirection.magnitude;
				if (num < 1f)
				{
					input.moveDirection /= num;
					num *= num;
					input.moveDirection *= num;
				}
				else if (num > 1f)
				{
					input.moveDirection /= num;
				}
				if (global::HumanController.InputSample.MovementScale < 1f)
				{
					if (global::HumanController.InputSample.MovementScale > 0f)
					{
						input.moveDirection *= global::HumanController.InputSample.MovementScale;
					}
					else
					{
						input.moveDirection = Vector3.zero;
					}
				}
				Vector3 moveDirection = input.moveDirection;
				moveDirection.x *= this.controlConfig.sprintScaleX;
				moveDirection.z *= this.controlConfig.sprintScaleY;
				float advance;
				if (sample.sprint && !flag3 && !sample.aim)
				{
					advance = Time.deltaTime * this.sprintInMulTime;
				}
				else
				{
					sample.sprint = false;
					advance = -Time.deltaTime;
				}
				input.moveDirection += moveDirection * this.controlConfig.curveSprintAddSpeedByTime.EvaluateClampedTime(ref this.sprintTime, advance);
				float advance2;
				if (flag3)
				{
					advance2 = Time.deltaTime * this.crouchInMulTime;
				}
				else
				{
					advance2 = -Time.deltaTime;
				}
				input.moveDirection *= this.controlConfig.curveCrouchMulSpeedByTime.EvaluateClampedTime(ref this.crouchTime, advance2);
				input.moveDirection = base.transform.TransformDirection(input.moveDirection);
				if (this.wasInAir)
				{
					float magnitude = input.moveDirection.magnitude;
					if (!Mathf.Approximately(magnitude, this.magnitudeAir))
					{
						input.moveDirection /= magnitude;
						input.moveDirection *= this.magnitudeAir;
					}
				}
				else
				{
					input.moveDirection *= this.controlConfig.curveLandingSpeedPenalty.EvaluateClampedTime(ref this.landingSpeedPenaltyTime, Time.deltaTime);
				}
			}
			else
			{
				this.sprinting = false;
				this.exitingSprint = false;
				this.sprintTime = 0f;
				this.crouchTime = ((!sample.crouch) ? 0f : this.controlConfig.curveCrouchMulSpeedByTime.GetEndTime());
				this.magnitudeAir = 1f;
			}
			if (global::DebugInput.GetKey(104))
			{
				input.moveDirection *= 100f;
			}
			ccmotor.input = input;
			if (ccmotor.stepMode == global::CCMotor.StepMode.Elsewhere)
			{
				ccmotor.Step();
			}
		}
		else
		{
			flag2 = false;
			flag = true;
		}
		global::Character idMain = base.idMain;
		global::Crouchable crouchable = idMain.crouchable;
		if (idMain)
		{
			global::Angle2 eyesAngles = base.eyesAngles;
			eyesAngles.yaw = Mathf.DeltaAngle(0f, base.eyesAngles.yaw + sample.yaw);
			eyesAngles.pitch = base.ClampPitch(eyesAngles.pitch + sample.pitch);
			base.eyesAngles = eyesAngles;
			ushort flags = idMain.stateFlags.flags;
			if (crouchable)
			{
				this.crouch_smoothing.AddSeconds((double)Time.deltaTime);
				crouchable.LocalPlayerUpdateCrouchState(ccmotor, ref sample.crouch, ref sample.info__crouchBlocked, ref this.crouch_smoothing);
			}
			int num2 = ((!sample.aim) ? 0 : 4) | ((!sample.sprint) ? 0 : 2) | ((!sample.attack) ? 0 : 8) | ((!sample.attack2) ? 0 : 256) | ((!sample.crouch) ? 0 : 1) | ((sample.strafe == 0f && sample.walk == 0f) ? 0 : 64) | ((!LockCursorManager.IsLocked()) ? 128 : 0) | ((!flag) ? 16 : 0) | ((!flag2) ? 0 : 32) | ((!this.bleeding) ? 0 : 512) | ((!sample.lamp) ? 0 : 2048) | ((!sample.laser) ? 0 : 4096) | ((!sample.info__crouchBlocked) ? 0 : 1024);
			idMain.stateFlags = num2;
			if ((int)flags != num2)
			{
				idMain.Signal_State_FlagsChanged(false);
			}
		}
		this.crouch_was_blocked = sample.info__crouchBlocked;
		if (sample.inventory)
		{
			global::RPOS.Toggle();
		}
		if (Input.GetKeyDown(27))
		{
			global::RPOS.Hide();
		}
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x000131B0 File Offset: 0x000113B0
	protected void SprintingStarted()
	{
		this.wasSprinting = true;
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x000131BC File Offset: 0x000113BC
	protected void SprintingStopped()
	{
		this.wasSprinting = false;
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x060003F3 RID: 1011 RVA: 0x000131C8 File Offset: 0x000113C8
	public bool bleeding
	{
		get
		{
			return (!this.clientVitalsSync) ? base.stateFlags.bleeding : this.clientVitalsSync.bleeding;
		}
	}

	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00013204 File Offset: 0x00011404
	protected global::HumanControlConfiguration controlConfig
	{
		get
		{
			if (!this._didControlConfigTest)
			{
				this._controlConfig = base.GetTrait<global::HumanControlConfiguration>();
				this._didControlConfigTest = true;
			}
			return this._controlConfig;
		}
	}

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00013238 File Offset: 0x00011438
	private Transform headBone
	{
		get
		{
			if (!this._headBone)
			{
				this._headBone = base.transform.FindChild("RustPlayer_Pelvis/RustPlayer_Spine/RustPlayer_Spine1/RustPlayer_Spine2/RustPlayer_Spine4/RustPlayer_Neck1/RustPlayer_Head1");
				if (!this._headBone)
				{
					this._headBone = base.transform.FindChild("RustPlayer_Pelvis/RustPlayer_Spine/RustPlayer_Spine1/RustPlayer_Spine2/RustPlayer_Spine4/RustPlayer_Neck1/RustPlayer_Head1");
					if (!this._headBone)
					{
						global::Character idMain = base.idMain;
						if (idMain && idMain.eyesTransformReadOnly)
						{
							this._headBone = idMain.eyesTransformReadOnly;
						}
						else
						{
							this._headBone = base.transform;
						}
					}
				}
			}
			return this._headBone;
		}
	}

	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000132E8 File Offset: 0x000114E8
	public global::InventoryHolder inventoryHolder
	{
		get
		{
			global::Inventory inventory = this.inventory;
			return (!inventory) ? null : inventory.inventoryHolder;
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00013314 File Offset: 0x00011514
	public global::Inventory inventory
	{
		get
		{
			if (!this.__inventory.cached)
			{
				this.__inventory = base.GetLocal<global::Inventory>();
			}
			return this.__inventory.value;
		}
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00013350 File Offset: 0x00011550
	private global::PlayerInventory _inventory
	{
		get
		{
			return this.inventory as global::PlayerInventory;
		}
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00013360 File Offset: 0x00011560
	protected void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		this.instantiatedPlayerClient = base.playerClient;
		if (this.instantiatedPlayerClient)
		{
			base.name = string.Format("{0}{1}", this.instantiatedPlayerClient.name, info.networkView.localPrefab);
		}
		try
		{
			this.deathTransfer = base.AddAddon<global::DeathTransfer>();
		}
		catch (Exception ex)
		{
			Debug.LogException(ex, this);
		}
		if (base.networkView.isMine)
		{
			global::CameraMount.ClearTemporaryCameraMount();
			Object.Destroy(base.GetComponent<global::ApplyCrouch>());
			base.CreateCCMotor();
			base.CreateOverlay();
		}
		else
		{
			if (base.CreateInterpolator())
			{
				base.interpolator.running = true;
			}
			Object.Destroy(base.GetComponent<global::LocalDamageDisplay>());
		}
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x00013440 File Offset: 0x00011640
	private void SetLocalOnlyComponentsEnabled(bool enable)
	{
		global::CCMotor component = base.GetComponent<global::CCMotor>();
		if (component)
		{
			component.enabled = enable;
			CharacterController characterController = base.collider as CharacterController;
			if (characterController)
			{
				characterController.enabled = enable;
			}
		}
		global::CameraMount componentInChildren = base.GetComponentInChildren<global::CameraMount>();
		if (componentInChildren)
		{
			componentInChildren.open = enable;
			global::HeadBob component2 = componentInChildren.GetComponent<global::HeadBob>();
			if (component2)
			{
				component2.enabled = enable;
			}
			global::LazyCam component3 = componentInChildren.GetComponent<global::LazyCam>();
			if (component3)
			{
				component3.enabled = enable;
			}
		}
		global::LocalDamageDisplay component4 = base.GetComponent<global::LocalDamageDisplay>();
		if (component4)
		{
			component4.enabled = enable;
		}
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x000134F0 File Offset: 0x000116F0
	protected override void OnControlEnter()
	{
		base.OnControlEnter();
		if (base.localControlled)
		{
			this.clientVitalsSync = base.AddAddon<global::ClientVitalsSync>();
			global::ImageEffectManager.GetInstance<global::GameFullscreen>().fadeColor = Color.black;
			global::ImageEffectManager.GetInstance<global::GameFullscreen>().tintColor = Color.white;
			global::RPOS.DoFade(2f, 2.5f, Color.clear);
			global::RPOS.SetCurrentFade(Color.black);
			global::RPOS.HealthUpdate(base.health);
			global::RPOS.ObservedPlayer = base.controllable;
		}
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x0001356C File Offset: 0x0001176C
	protected override void OnControlEngauge()
	{
		base.OnControlEngauge();
		if (base.localControlled)
		{
			global::CameraMount componentInChildren = base.GetComponentInChildren<global::CameraMount>();
			if (componentInChildren)
			{
				componentInChildren.open = true;
			}
			this.contextProbe = ((!this.contextProbe) ? base.AddAddon<global::ContextProbe>() : this.contextProbe);
			this.localRadiation = ((!this.localRadiation) ? base.AddAddon<global::LocalRadiationEffect>() : this.localRadiation);
			if (this.onceEngaged)
			{
				if (this.proxyTest)
				{
					this.proxyTest.treatAsProxy = false;
				}
			}
			else
			{
				this.proxyTest = base.GetComponent<global::PlayerProxyTest>();
				this.onceEngaged = true;
			}
			base.enabled = true;
		}
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00013638 File Offset: 0x00011838
	protected override void OnControlCease()
	{
		if (base.localControlled)
		{
			global::CameraMount componentInChildren = base.GetComponentInChildren<global::CameraMount>();
			if (componentInChildren)
			{
				componentInChildren.open = false;
			}
		}
		base.RemoveAddon<global::ContextProbe>(ref this.contextProbe);
		base.RemoveAddon<global::LocalRadiationEffect>(ref this.localRadiation);
		base.enabled = false;
		if (base.localControlled)
		{
			if (this.proxyTest)
			{
				this.proxyTest.treatAsProxy = true;
			}
			if (this._inventory)
			{
				this._inventory.DeactivateItem();
			}
		}
		base.OnControlCease();
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x000136D0 File Offset: 0x000118D0
	protected override void OnControlExit()
	{
		base.RemoveAddon<global::ClientVitalsSync>(ref this.clientVitalsSync);
		base.OnControlExit();
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x000136E4 File Offset: 0x000118E4
	protected override void OnLocalPlayerPreRender()
	{
		global::InventoryHolder inventoryHolder = this.inventoryHolder;
		if (inventoryHolder)
		{
			inventoryHolder.InvokeInputItemPreRender();
		}
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x0001370C File Offset: 0x0001190C
	[Obsolete("Make sure the only thing calling this is Update!")]
	protected void SendToServer()
	{
		global::Character idMain = base.idMain;
		int num = (int)idMain.stateFlags.flags & -24577;
		if (Time.timeScale == 1f)
		{
			if (this.thatsRightPatWeDontNeedComments != null)
			{
				num |= ((!this.thatsRightPatWeDontNeedComments.Value) ? 16384 : 8192);
				this.thatsRightPatWeDontNeedComments = new bool?(!this.thatsRightPatWeDontNeedComments.Value);
			}
			else
			{
				this.thatsRightPatWeDontNeedComments = new bool?((base.playerClient.userName.GetHashCode() & 1) == 1);
			}
		}
		else
		{
			num |= 24576;
		}
		base.networkView.RPC("GetClientMove", uLink.NetworkPlayer.server, new object[]
		{
			idMain.origin,
			idMain.eyesAngles.encoded,
			(ushort)num
		});
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x00013808 File Offset: 0x00011A08
	[RPC]
	private void GetClientMove(Vector3 origin, int encoded, ushort stateFlags, uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x0001380C File Offset: 0x00011A0C
	[RPC]
	private void ReadClientMove(Vector3 origin, int encoded, ushort stateFlags, float timeAfterServerReceived, uLink.NetworkMessageInfo info)
	{
		this.UpdateStateNew(origin, new global::Angle2
		{
			encoded = encoded
		}, stateFlags, info.timestamp);
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x0001383C File Offset: 0x00011A3C
	private void UpdateStateNew(Vector3 origin, global::Angle2 eyesAngles, ushort stateFlags, double timestamp)
	{
		global::Character idMain = base.idMain;
		if (this.firstState)
		{
			this.firstState = false;
			idMain.origin = origin;
			idMain.eyesAngles = eyesAngles;
			idMain.stateFlags.flags = stateFlags;
			return;
		}
		if (base.networkView.isMine)
		{
			idMain.origin = origin;
			idMain.eyesAngles = eyesAngles;
			idMain.stateFlags.flags = stateFlags;
			global::CCMotor ccmotor = base.ccmotor;
			if (ccmotor)
			{
				ccmotor.Teleport(origin);
			}
		}
		else
		{
			global::CharacterInterpolatorBase interpolator = base.interpolator;
			if (interpolator)
			{
				global::IStateInterpolator<global::CharacterStateInterpolatorData> stateInterpolator = interpolator as global::IStateInterpolator<global::CharacterStateInterpolatorData>;
				if (stateInterpolator != null)
				{
					global::CharacterStateInterpolatorData characterStateInterpolatorData;
					characterStateInterpolatorData.origin = origin;
					characterStateInterpolatorData.state.flags = stateFlags;
					characterStateInterpolatorData.eyesAngles = eyesAngles;
					stateInterpolator.SetGoals(ref characterStateInterpolatorData, ref timestamp);
				}
				else
				{
					idMain.stateFlags.flags = stateFlags;
					interpolator.SetGoals(origin, eyesAngles.quat, timestamp);
				}
			}
		}
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x0001392C File Offset: 0x00011B2C
	protected void OnEnable()
	{
		this.SetLocalOnlyComponentsEnabled(true);
		LockCursorManager.IsLocked(true);
		this.onceClock = false;
		this.clock = Facepunch.Clocks.Counters.SystemTimestamp.Restart;
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x0001395C File Offset: 0x00011B5C
	protected void OnDisable()
	{
		if (Application.isPlaying)
		{
			global::Character idMain = base.idMain;
			if (idMain)
			{
				global::Character character = idMain;
				character.stateFlags.flags = (character.stateFlags.flags & 7856);
			}
			this.SetLocalOnlyComponentsEnabled(false);
		}
		this.sprinting = false;
		this.exitingSprint = true;
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x000139B4 File Offset: 0x00011BB4
	protected void Update()
	{
		if (base.dead)
		{
			return;
		}
		try
		{
			this.UpdateInput();
		}
		finally
		{
			if ((!this.onceClock || this.clock.ElapsedSeconds > global::NetCull.sendInterval) && !base.dead)
			{
				this.onceClock = true;
				this.SendToServer();
				this.clock = Facepunch.Clocks.Counters.SystemTimestamp.Restart;
			}
		}
	}

	// Token: 0x04000336 RID: 822
	private const string kHeadPath = "RustPlayer_Pelvis/RustPlayer_Spine/RustPlayer_Spine1/RustPlayer_Spine2/RustPlayer_Spine4/RustPlayer_Neck1/RustPlayer_Head1";

	// Token: 0x04000337 RID: 823
	private const long clearOnDisableCharacterStateFlags = 335L;

	// Token: 0x04000338 RID: 824
	private const ushort doNotClearOnDisableCharacterStateFlags = 7856;

	// Token: 0x04000339 RID: 825
	protected const global::Controller.ControllerFlags kControllerFlags = (global::Controller.ControllerFlags)8385;

	// Token: 0x0400033A RID: 826
	private const bool stepMotorHere = true;

	// Token: 0x0400033B RID: 827
	[NonSerialized]
	private bool crouch_was_blocked;

	// Token: 0x0400033C RID: 828
	[NonSerialized]
	private global::Crouchable.Smoothing crouch_smoothing;

	// Token: 0x0400033D RID: 829
	private Vector3 lastFrameVelocity;

	// Token: 0x0400033E RID: 830
	private Vector3 midairStartPos;

	// Token: 0x0400033F RID: 831
	[NonSerialized]
	private global::ContextProbe contextProbe;

	// Token: 0x04000340 RID: 832
	[NonSerialized]
	private global::LocalRadiationEffect localRadiation;

	// Token: 0x04000341 RID: 833
	[NonSerialized]
	private global::CacheRef<global::Inventory> __inventory;

	// Token: 0x04000342 RID: 834
	[NonSerialized]
	private global::ClientVitalsSync clientVitalsSync;

	// Token: 0x04000343 RID: 835
	[NonSerialized]
	private global::DeathTransfer deathTransfer;

	// Token: 0x04000344 RID: 836
	[NonSerialized]
	protected int badPacketCount;

	// Token: 0x04000345 RID: 837
	[NonSerialized]
	private bool firstState = true;

	// Token: 0x04000346 RID: 838
	[NonSerialized]
	private global::HumanControlConfiguration _controlConfig;

	// Token: 0x04000347 RID: 839
	[NonSerialized]
	private bool _didControlConfigTest;

	// Token: 0x04000348 RID: 840
	[NonSerialized]
	private bool? thatsRightPatWeDontNeedComments;

	// Token: 0x04000349 RID: 841
	[NonSerialized]
	private float sprintInMulTime = 1f;

	// Token: 0x0400034A RID: 842
	[NonSerialized]
	private float crouchInMulTime = 1f;

	// Token: 0x0400034B RID: 843
	[NonSerialized]
	private Vector3 server_last_pos = Vector3.zero;

	// Token: 0x0400034C RID: 844
	[NonSerialized]
	private bool server_was_grounded = true;

	// Token: 0x0400034D RID: 845
	[NonSerialized]
	private float server_next_fall_damage_time;

	// Token: 0x0400034E RID: 846
	[NonSerialized]
	private float magnitudeAir;

	// Token: 0x0400034F RID: 847
	[NonSerialized]
	private bool wasInAir;

	// Token: 0x04000350 RID: 848
	[NonSerialized]
	private bool onceEngaged;

	// Token: 0x04000351 RID: 849
	[NonSerialized]
	private float landingSpeedPenaltyTime = float.MaxValue;

	// Token: 0x04000352 RID: 850
	[NonSerialized]
	private bool onceClock;

	// Token: 0x04000353 RID: 851
	[NonSerialized]
	private Facepunch.Clocks.Counters.SystemTimestamp clock;

	// Token: 0x04000354 RID: 852
	[NonSerialized]
	private Transform _headBone;

	// Token: 0x04000355 RID: 853
	[NonSerialized]
	private bool sprinting;

	// Token: 0x04000356 RID: 854
	[NonSerialized]
	private bool exitingSprint;

	// Token: 0x04000357 RID: 855
	[NonSerialized]
	private bool crouching;

	// Token: 0x04000358 RID: 856
	[NonSerialized]
	private bool exitingCrouch;

	// Token: 0x04000359 RID: 857
	[NonSerialized]
	private bool wasSprinting;

	// Token: 0x0400035A RID: 858
	[NonSerialized]
	private float sprintTime;

	// Token: 0x0400035B RID: 859
	[NonSerialized]
	private float crouchTime;

	// Token: 0x0400035C RID: 860
	[NonSerialized]
	private global::PlayerProxyTest proxyTest;

	// Token: 0x0400035D RID: 861
	[NonSerialized]
	private global::PlayerClient instantiatedPlayerClient;

	// Token: 0x020000B6 RID: 182
	public struct InputSample
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00013AA0 File Offset: 0x00011CA0
		public bool is_sprinting
		{
			get
			{
				return this.sprint && !this.aim && this.walk != 0f;
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00013ACC File Offset: 0x00011CCC
		public static global::HumanController.InputSample Poll()
		{
			return global::HumanController.InputSample.Poll(false, false);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00013AD8 File Offset: 0x00011CD8
		public static global::HumanController.InputSample Poll(bool noLamp, bool noLaser)
		{
			if (global::ConsoleWindow.IsVisible())
			{
				return default(global::HumanController.InputSample);
			}
			if (global::MainMenu.IsVisible())
			{
				return default(global::HumanController.InputSample);
			}
			if (global::ChatUI.IsVisible())
			{
				return default(global::HumanController.InputSample);
			}
			if (global::LockEntry.IsVisible())
			{
				return default(global::HumanController.InputSample);
			}
			global::HumanController.InputSample result;
			if (!LockCursorManager.IsLocked(true))
			{
				result = default(global::HumanController.InputSample);
				if (!global::UIUnityEvents.shouldBlockButtonInput)
				{
					result.inventory = global::GameInput.GetButton("Inventory").IsPressed();
				}
				result.lamp = global::HumanController.InputSample.saved.lamp;
				result.laser = global::HumanController.InputSample.saved.laser;
			}
			else
			{
				float deltaTime = Time.deltaTime;
				result.info__crouchBlocked = false;
				result.walk = 0f;
				if (global::GameInput.GetButton("Up").IsDown())
				{
					result.walk += 1f;
				}
				if (global::GameInput.GetButton("Down").IsDown())
				{
					result.walk -= 1f;
				}
				result.strafe = 0f;
				if (global::GameInput.GetButton("Right").IsDown())
				{
					result.strafe += 1f;
				}
				if (global::GameInput.GetButton("Left").IsDown())
				{
					result.strafe -= 1f;
				}
				result.yaw = global::GameInput.mouseDeltaX + global::HumanController.InputSample.yawSensitivityJoy * Input.GetAxis("Yaw") * deltaTime;
				result.pitch = global::GameInput.mouseDeltaY + global::HumanController.InputSample.pitchSensitivityJoy * Input.GetAxis("Pitch") * deltaTime;
				if (global::input.flipy)
				{
					result.pitch *= -1f;
				}
				result.jump = global::GameInput.GetButton("Jump").IsDown();
				result.crouch = global::GameInput.GetButton("Duck").IsDown();
				result.sprint = global::GameInput.GetButton("Sprint").IsDown();
				result.aim = false;
				result.attack = global::GameInput.GetButton("Fire").IsDown();
				result.attack2 = global::GameInput.GetButton("AltFire").IsDown();
				result.reload = global::GameInput.GetButton("Reload").IsDown();
				result.inventory = global::GameInput.GetButton("Inventory").IsPressed();
				result.lamp = ((!noLamp) ? global::HumanController.InputSample.saved.GetLamp(global::GameInput.GetButton("Flashlight").IsPressed()) : global::HumanController.InputSample.saved.lamp);
				result.laser = ((!noLaser) ? global::HumanController.InputSample.saved.GetLaser(global::GameInput.GetButton("Laser").IsPressed()) : global::HumanController.InputSample.saved.laser);
			}
			if (global::GameInput.GetButton("Chat").IsPressed())
			{
				global::ChatUI.Open();
			}
			return result;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00013DBC File Offset: 0x00011FBC
		public static int PollItemButtons()
		{
			if (LockCursorManager.keySubsetEnabled)
			{
				for (int i = 0; i < global::HumanController.InputSample.kUseButtons.Length; i++)
				{
					if (Input.GetButtonDown(global::HumanController.InputSample.kUseButtons[i]))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0400035E RID: 862
		public const string kButtonAim = "Aim";

		// Token: 0x0400035F RID: 863
		public const string kRawYaw = "Mouse X";

		// Token: 0x04000360 RID: 864
		public const string kRawPitch = "Mouse Y";

		// Token: 0x04000361 RID: 865
		public const string kYaw = "Yaw";

		// Token: 0x04000362 RID: 866
		public const string kPitch = "Pitch";

		// Token: 0x04000363 RID: 867
		public const string kButtonUse = "WorldUse";

		// Token: 0x04000364 RID: 868
		public static float MovementScale = 1f;

		// Token: 0x04000365 RID: 869
		public float walk;

		// Token: 0x04000366 RID: 870
		public float strafe;

		// Token: 0x04000367 RID: 871
		public float yaw;

		// Token: 0x04000368 RID: 872
		public float pitch;

		// Token: 0x04000369 RID: 873
		public bool jump;

		// Token: 0x0400036A RID: 874
		public bool crouch;

		// Token: 0x0400036B RID: 875
		public bool sprint;

		// Token: 0x0400036C RID: 876
		public bool aim;

		// Token: 0x0400036D RID: 877
		public bool attack;

		// Token: 0x0400036E RID: 878
		public bool attack2;

		// Token: 0x0400036F RID: 879
		public bool reload;

		// Token: 0x04000370 RID: 880
		public bool inventory;

		// Token: 0x04000371 RID: 881
		public bool lamp;

		// Token: 0x04000372 RID: 882
		public bool laser;

		// Token: 0x04000373 RID: 883
		public bool info__crouchBlocked;

		// Token: 0x04000374 RID: 884
		private static float yawSensitivityJoy = 30f;

		// Token: 0x04000375 RID: 885
		private static float pitchSensitivityJoy = 30f;

		// Token: 0x04000376 RID: 886
		private static readonly string[] kUseButtons = new string[]
		{
			"UseItem1",
			"UseItem2",
			"UseItem3",
			"UseItem4",
			"UseItem5",
			"UseItem6"
		};

		// Token: 0x020000B7 RID: 183
		private static class saved
		{
			// Token: 0x0600040D RID: 1037 RVA: 0x00013E3C File Offset: 0x0001203C
			public static bool GetLamp(bool pressed)
			{
				if (pressed)
				{
					global::HumanController.InputSample.saved.lamp = !global::HumanController.InputSample.saved.lamp;
					PlayerPrefs.SetInt("LAMP", (!global::HumanController.InputSample.saved.lamp) ? 0 : 1);
				}
				return global::HumanController.InputSample.saved.lamp;
			}

			// Token: 0x0600040E RID: 1038 RVA: 0x00013E74 File Offset: 0x00012074
			public static bool GetLaser(bool pressed)
			{
				if (pressed)
				{
					global::HumanController.InputSample.saved.laser = !global::HumanController.InputSample.saved.laser;
					PlayerPrefs.SetInt("LASER", (!global::HumanController.InputSample.saved.laser) ? 0 : 1);
				}
				return global::HumanController.InputSample.saved.laser;
			}

			// Token: 0x04000377 RID: 887
			public static bool lamp = PlayerPrefs.GetInt("LAMP", 1) != 0;

			// Token: 0x04000378 RID: 888
			public static bool laser = PlayerPrefs.GetInt("LASER", 1) != 0;
		}
	}
}
