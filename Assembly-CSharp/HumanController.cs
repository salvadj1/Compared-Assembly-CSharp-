using System;
using Facepunch.Clocks.Counters;
using Facepunch.Cursor;
using uLink;
using UnityEngine;

// Token: 0x020000A2 RID: 162
public class HumanController : Controller, RagdollTransferInfoProvider
{
	// Token: 0x06000373 RID: 883 RVA: 0x00011080 File Offset: 0x0000F280
	public HumanController() : this((Controller.ControllerFlags)8385)
	{
	}

	// Token: 0x06000374 RID: 884 RVA: 0x00011090 File Offset: 0x0000F290
	protected HumanController(Controller.ControllerFlags controllerFlags) : base(controllerFlags)
	{
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x06000375 RID: 885 RVA: 0x000110E0 File Offset: 0x0000F2E0
	RagdollTransferInfo RagdollTransferInfoProvider.RagdollTransferInfo
	{
		get
		{
			return "RustPlayer_Pelvis/RustPlayer_Spine/RustPlayer_Spine1/RustPlayer_Spine2/RustPlayer_Spine4/RustPlayer_Neck1/RustPlayer_Head1";
		}
	}

	// Token: 0x06000376 RID: 886 RVA: 0x000110EC File Offset: 0x0000F2EC
	private void CheckBeltUsage()
	{
		if (UIUnityEvents.shouldBlockButtonInput)
		{
			return;
		}
		if (!base.enabled)
		{
			return;
		}
		if (ConsoleWindow.IsVisible())
		{
			return;
		}
		Inventory inventory = this.inventory;
		if (!inventory)
		{
			return;
		}
		InventoryHolder inventoryHolder = inventory.inventoryHolder;
		if (!inventoryHolder)
		{
			return;
		}
		int num = HumanController.InputSample.PollItemButtons();
		if (num != -1)
		{
			inventoryHolder.BeltUse(num);
		}
	}

	// Token: 0x06000377 RID: 887 RVA: 0x00011158 File Offset: 0x0000F358
	protected void UpdateInput()
	{
		InventoryHolder inventoryHolder = this.inventoryHolder;
		PlayerClient.InputFunction(base.gameObject);
		bool noLamp;
		bool noLaser;
		if (inventoryHolder)
		{
			ItemModFlags modFlags = inventoryHolder.modFlags;
			noLamp = ((modFlags & ItemModFlags.Lamp) == ItemModFlags.Other);
			noLaser = ((modFlags & ItemModFlags.Laser) == ItemModFlags.Other);
		}
		else
		{
			noLaser = (noLamp = true);
		}
		HumanController.InputSample inputSample = HumanController.InputSample.Poll(noLamp, noLaser);
		inputSample.info__crouchBlocked = this.crouch_was_blocked;
		bool flag = base.GetLocal<FallDamage>().GetLegInjury() > 0f;
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
		HumanController.InputSample.MovementScale = num;
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

	// Token: 0x06000378 RID: 888 RVA: 0x000112E8 File Offset: 0x0000F4E8
	private void ProcessInput(ref HumanController.InputSample sample)
	{
		CCMotor ccmotor = base.ccmotor;
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
					base.idMain.GetLocal<FallDamage>().SendFallImpact(this.lastFrameVelocity);
				}
				this.lastFrameVelocity = Vector3.zero;
				this.midairStartPos = Vector3.zero;
			}
			bool flag3 = sample.crouch || sample.info__crouchBlocked;
			CCMotor.InputFrame input;
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
				if (HumanController.InputSample.MovementScale < 1f)
				{
					if (HumanController.InputSample.MovementScale > 0f)
					{
						input.moveDirection *= HumanController.InputSample.MovementScale;
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
			if (DebugInput.GetKey(104))
			{
				input.moveDirection *= 100f;
			}
			ccmotor.input = input;
			if (ccmotor.stepMode == CCMotor.StepMode.Elsewhere)
			{
				ccmotor.Step();
			}
		}
		else
		{
			flag2 = false;
			flag = true;
		}
		Character idMain = base.idMain;
		Crouchable crouchable = idMain.crouchable;
		if (idMain)
		{
			Angle2 eyesAngles = base.eyesAngles;
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
			RPOS.Toggle();
		}
		if (Input.GetKeyDown(27))
		{
			RPOS.Hide();
		}
	}

	// Token: 0x06000379 RID: 889 RVA: 0x000119C0 File Offset: 0x0000FBC0
	protected void SprintingStarted()
	{
		this.wasSprinting = true;
	}

	// Token: 0x0600037A RID: 890 RVA: 0x000119CC File Offset: 0x0000FBCC
	protected void SprintingStopped()
	{
		this.wasSprinting = false;
	}

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x0600037B RID: 891 RVA: 0x000119D8 File Offset: 0x0000FBD8
	public bool bleeding
	{
		get
		{
			return (!this.clientVitalsSync) ? base.stateFlags.bleeding : this.clientVitalsSync.bleeding;
		}
	}

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x0600037C RID: 892 RVA: 0x00011A14 File Offset: 0x0000FC14
	protected HumanControlConfiguration controlConfig
	{
		get
		{
			if (!this._didControlConfigTest)
			{
				this._controlConfig = base.GetTrait<HumanControlConfiguration>();
				this._didControlConfigTest = true;
			}
			return this._controlConfig;
		}
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x0600037D RID: 893 RVA: 0x00011A48 File Offset: 0x0000FC48
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
						Character idMain = base.idMain;
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

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x0600037E RID: 894 RVA: 0x00011AF8 File Offset: 0x0000FCF8
	public InventoryHolder inventoryHolder
	{
		get
		{
			Inventory inventory = this.inventory;
			return (!inventory) ? null : inventory.inventoryHolder;
		}
	}

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x0600037F RID: 895 RVA: 0x00011B24 File Offset: 0x0000FD24
	public Inventory inventory
	{
		get
		{
			if (!this.__inventory.cached)
			{
				this.__inventory = base.GetLocal<Inventory>();
			}
			return this.__inventory.value;
		}
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x06000380 RID: 896 RVA: 0x00011B60 File Offset: 0x0000FD60
	private PlayerInventory _inventory
	{
		get
		{
			return this.inventory as PlayerInventory;
		}
	}

	// Token: 0x06000381 RID: 897 RVA: 0x00011B70 File Offset: 0x0000FD70
	protected void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		this.instantiatedPlayerClient = base.playerClient;
		if (this.instantiatedPlayerClient)
		{
			base.name = string.Format("{0}{1}", this.instantiatedPlayerClient.name, info.networkView.localPrefab);
		}
		try
		{
			this.deathTransfer = base.AddAddon<DeathTransfer>();
		}
		catch (Exception ex)
		{
			Debug.LogException(ex, this);
		}
		if (base.networkView.isMine)
		{
			CameraMount.ClearTemporaryCameraMount();
			Object.Destroy(base.GetComponent<ApplyCrouch>());
			base.CreateCCMotor();
			base.CreateOverlay();
		}
		else
		{
			if (base.CreateInterpolator())
			{
				base.interpolator.running = true;
			}
			Object.Destroy(base.GetComponent<LocalDamageDisplay>());
		}
	}

	// Token: 0x06000382 RID: 898 RVA: 0x00011C50 File Offset: 0x0000FE50
	private void SetLocalOnlyComponentsEnabled(bool enable)
	{
		CCMotor component = base.GetComponent<CCMotor>();
		if (component)
		{
			component.enabled = enable;
			CharacterController characterController = base.collider as CharacterController;
			if (characterController)
			{
				characterController.enabled = enable;
			}
		}
		CameraMount componentInChildren = base.GetComponentInChildren<CameraMount>();
		if (componentInChildren)
		{
			componentInChildren.open = enable;
			HeadBob component2 = componentInChildren.GetComponent<HeadBob>();
			if (component2)
			{
				component2.enabled = enable;
			}
			LazyCam component3 = componentInChildren.GetComponent<LazyCam>();
			if (component3)
			{
				component3.enabled = enable;
			}
		}
		LocalDamageDisplay component4 = base.GetComponent<LocalDamageDisplay>();
		if (component4)
		{
			component4.enabled = enable;
		}
	}

	// Token: 0x06000383 RID: 899 RVA: 0x00011D00 File Offset: 0x0000FF00
	protected override void OnControlEnter()
	{
		base.OnControlEnter();
		if (base.localControlled)
		{
			this.clientVitalsSync = base.AddAddon<ClientVitalsSync>();
			ImageEffectManager.GetInstance<GameFullscreen>().fadeColor = Color.black;
			ImageEffectManager.GetInstance<GameFullscreen>().tintColor = Color.white;
			RPOS.DoFade(2f, 2.5f, Color.clear);
			RPOS.SetCurrentFade(Color.black);
			RPOS.HealthUpdate(base.health);
			RPOS.ObservedPlayer = base.controllable;
		}
	}

	// Token: 0x06000384 RID: 900 RVA: 0x00011D7C File Offset: 0x0000FF7C
	protected override void OnControlEngauge()
	{
		base.OnControlEngauge();
		if (base.localControlled)
		{
			CameraMount componentInChildren = base.GetComponentInChildren<CameraMount>();
			if (componentInChildren)
			{
				componentInChildren.open = true;
			}
			this.contextProbe = ((!this.contextProbe) ? base.AddAddon<ContextProbe>() : this.contextProbe);
			this.localRadiation = ((!this.localRadiation) ? base.AddAddon<LocalRadiationEffect>() : this.localRadiation);
			if (this.onceEngaged)
			{
				if (this.proxyTest)
				{
					this.proxyTest.treatAsProxy = false;
				}
			}
			else
			{
				this.proxyTest = base.GetComponent<PlayerProxyTest>();
				this.onceEngaged = true;
			}
			base.enabled = true;
		}
	}

	// Token: 0x06000385 RID: 901 RVA: 0x00011E48 File Offset: 0x00010048
	protected override void OnControlCease()
	{
		if (base.localControlled)
		{
			CameraMount componentInChildren = base.GetComponentInChildren<CameraMount>();
			if (componentInChildren)
			{
				componentInChildren.open = false;
			}
		}
		base.RemoveAddon<ContextProbe>(ref this.contextProbe);
		base.RemoveAddon<LocalRadiationEffect>(ref this.localRadiation);
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

	// Token: 0x06000386 RID: 902 RVA: 0x00011EE0 File Offset: 0x000100E0
	protected override void OnControlExit()
	{
		base.RemoveAddon<ClientVitalsSync>(ref this.clientVitalsSync);
		base.OnControlExit();
	}

	// Token: 0x06000387 RID: 903 RVA: 0x00011EF4 File Offset: 0x000100F4
	protected override void OnLocalPlayerPreRender()
	{
		InventoryHolder inventoryHolder = this.inventoryHolder;
		if (inventoryHolder)
		{
			inventoryHolder.InvokeInputItemPreRender();
		}
	}

	// Token: 0x06000388 RID: 904 RVA: 0x00011F1C File Offset: 0x0001011C
	[Obsolete("Make sure the only thing calling this is Update!")]
	protected void SendToServer()
	{
		Character idMain = base.idMain;
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
		base.networkView.RPC("GetClientMove", NetworkPlayer.server, new object[]
		{
			idMain.origin,
			idMain.eyesAngles.encoded,
			(ushort)num
		});
	}

	// Token: 0x06000389 RID: 905 RVA: 0x00012018 File Offset: 0x00010218
	[RPC]
	private void GetClientMove(Vector3 origin, int encoded, ushort stateFlags, NetworkMessageInfo info)
	{
	}

	// Token: 0x0600038A RID: 906 RVA: 0x0001201C File Offset: 0x0001021C
	[RPC]
	private void ReadClientMove(Vector3 origin, int encoded, ushort stateFlags, float timeAfterServerReceived, NetworkMessageInfo info)
	{
		this.UpdateStateNew(origin, new Angle2
		{
			encoded = encoded
		}, stateFlags, info.timestamp);
	}

	// Token: 0x0600038B RID: 907 RVA: 0x0001204C File Offset: 0x0001024C
	private void UpdateStateNew(Vector3 origin, Angle2 eyesAngles, ushort stateFlags, double timestamp)
	{
		Character idMain = base.idMain;
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
			CCMotor ccmotor = base.ccmotor;
			if (ccmotor)
			{
				ccmotor.Teleport(origin);
			}
		}
		else
		{
			CharacterInterpolatorBase interpolator = base.interpolator;
			if (interpolator)
			{
				IStateInterpolator<CharacterStateInterpolatorData> stateInterpolator = interpolator as IStateInterpolator<CharacterStateInterpolatorData>;
				if (stateInterpolator != null)
				{
					CharacterStateInterpolatorData characterStateInterpolatorData;
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

	// Token: 0x0600038C RID: 908 RVA: 0x0001213C File Offset: 0x0001033C
	protected void OnEnable()
	{
		this.SetLocalOnlyComponentsEnabled(true);
		LockCursorManager.IsLocked(true);
		this.onceClock = false;
		this.clock = SystemTimestamp.Restart;
	}

	// Token: 0x0600038D RID: 909 RVA: 0x0001216C File Offset: 0x0001036C
	protected void OnDisable()
	{
		if (Application.isPlaying)
		{
			Character idMain = base.idMain;
			if (idMain)
			{
				Character character = idMain;
				character.stateFlags.flags = (character.stateFlags.flags & 7856);
			}
			this.SetLocalOnlyComponentsEnabled(false);
		}
		this.sprinting = false;
		this.exitingSprint = true;
	}

	// Token: 0x0600038E RID: 910 RVA: 0x000121C4 File Offset: 0x000103C4
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
			if ((!this.onceClock || this.clock.ElapsedSeconds > NetCull.sendInterval) && !base.dead)
			{
				this.onceClock = true;
				this.SendToServer();
				this.clock = SystemTimestamp.Restart;
			}
		}
	}

	// Token: 0x040002CB RID: 715
	private const string kHeadPath = "RustPlayer_Pelvis/RustPlayer_Spine/RustPlayer_Spine1/RustPlayer_Spine2/RustPlayer_Spine4/RustPlayer_Neck1/RustPlayer_Head1";

	// Token: 0x040002CC RID: 716
	private const long clearOnDisableCharacterStateFlags = 335L;

	// Token: 0x040002CD RID: 717
	private const ushort doNotClearOnDisableCharacterStateFlags = 7856;

	// Token: 0x040002CE RID: 718
	protected const Controller.ControllerFlags kControllerFlags = (Controller.ControllerFlags)8385;

	// Token: 0x040002CF RID: 719
	private const bool stepMotorHere = true;

	// Token: 0x040002D0 RID: 720
	[NonSerialized]
	private bool crouch_was_blocked;

	// Token: 0x040002D1 RID: 721
	[NonSerialized]
	private Crouchable.Smoothing crouch_smoothing;

	// Token: 0x040002D2 RID: 722
	private Vector3 lastFrameVelocity;

	// Token: 0x040002D3 RID: 723
	private Vector3 midairStartPos;

	// Token: 0x040002D4 RID: 724
	[NonSerialized]
	private ContextProbe contextProbe;

	// Token: 0x040002D5 RID: 725
	[NonSerialized]
	private LocalRadiationEffect localRadiation;

	// Token: 0x040002D6 RID: 726
	[NonSerialized]
	private CacheRef<Inventory> __inventory;

	// Token: 0x040002D7 RID: 727
	[NonSerialized]
	private ClientVitalsSync clientVitalsSync;

	// Token: 0x040002D8 RID: 728
	[NonSerialized]
	private DeathTransfer deathTransfer;

	// Token: 0x040002D9 RID: 729
	[NonSerialized]
	protected int badPacketCount;

	// Token: 0x040002DA RID: 730
	[NonSerialized]
	private bool firstState = true;

	// Token: 0x040002DB RID: 731
	[NonSerialized]
	private HumanControlConfiguration _controlConfig;

	// Token: 0x040002DC RID: 732
	[NonSerialized]
	private bool _didControlConfigTest;

	// Token: 0x040002DD RID: 733
	[NonSerialized]
	private bool? thatsRightPatWeDontNeedComments;

	// Token: 0x040002DE RID: 734
	[NonSerialized]
	private float sprintInMulTime = 1f;

	// Token: 0x040002DF RID: 735
	[NonSerialized]
	private float crouchInMulTime = 1f;

	// Token: 0x040002E0 RID: 736
	[NonSerialized]
	private Vector3 server_last_pos = Vector3.zero;

	// Token: 0x040002E1 RID: 737
	[NonSerialized]
	private bool server_was_grounded = true;

	// Token: 0x040002E2 RID: 738
	[NonSerialized]
	private float server_next_fall_damage_time;

	// Token: 0x040002E3 RID: 739
	[NonSerialized]
	private float magnitudeAir;

	// Token: 0x040002E4 RID: 740
	[NonSerialized]
	private bool wasInAir;

	// Token: 0x040002E5 RID: 741
	[NonSerialized]
	private bool onceEngaged;

	// Token: 0x040002E6 RID: 742
	[NonSerialized]
	private float landingSpeedPenaltyTime = float.MaxValue;

	// Token: 0x040002E7 RID: 743
	[NonSerialized]
	private bool onceClock;

	// Token: 0x040002E8 RID: 744
	[NonSerialized]
	private SystemTimestamp clock;

	// Token: 0x040002E9 RID: 745
	[NonSerialized]
	private Transform _headBone;

	// Token: 0x040002EA RID: 746
	[NonSerialized]
	private bool sprinting;

	// Token: 0x040002EB RID: 747
	[NonSerialized]
	private bool exitingSprint;

	// Token: 0x040002EC RID: 748
	[NonSerialized]
	private bool crouching;

	// Token: 0x040002ED RID: 749
	[NonSerialized]
	private bool exitingCrouch;

	// Token: 0x040002EE RID: 750
	[NonSerialized]
	private bool wasSprinting;

	// Token: 0x040002EF RID: 751
	[NonSerialized]
	private float sprintTime;

	// Token: 0x040002F0 RID: 752
	[NonSerialized]
	private float crouchTime;

	// Token: 0x040002F1 RID: 753
	[NonSerialized]
	private PlayerProxyTest proxyTest;

	// Token: 0x040002F2 RID: 754
	[NonSerialized]
	private PlayerClient instantiatedPlayerClient;

	// Token: 0x020000A3 RID: 163
	public struct InputSample
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000390 RID: 912 RVA: 0x000122B0 File Offset: 0x000104B0
		public bool is_sprinting
		{
			get
			{
				return this.sprint && !this.aim && this.walk != 0f;
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000122DC File Offset: 0x000104DC
		public static HumanController.InputSample Poll()
		{
			return HumanController.InputSample.Poll(false, false);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000122E8 File Offset: 0x000104E8
		public static HumanController.InputSample Poll(bool noLamp, bool noLaser)
		{
			if (ConsoleWindow.IsVisible())
			{
				return default(HumanController.InputSample);
			}
			if (MainMenu.IsVisible())
			{
				return default(HumanController.InputSample);
			}
			if (ChatUI.IsVisible())
			{
				return default(HumanController.InputSample);
			}
			if (LockEntry.IsVisible())
			{
				return default(HumanController.InputSample);
			}
			HumanController.InputSample result;
			if (!LockCursorManager.IsLocked(true))
			{
				result = default(HumanController.InputSample);
				if (!UIUnityEvents.shouldBlockButtonInput)
				{
					result.inventory = GameInput.GetButton("Inventory").IsPressed();
				}
				result.lamp = HumanController.InputSample.saved.lamp;
				result.laser = HumanController.InputSample.saved.laser;
			}
			else
			{
				float deltaTime = Time.deltaTime;
				result.info__crouchBlocked = false;
				result.walk = 0f;
				if (GameInput.GetButton("Up").IsDown())
				{
					result.walk += 1f;
				}
				if (GameInput.GetButton("Down").IsDown())
				{
					result.walk -= 1f;
				}
				result.strafe = 0f;
				if (GameInput.GetButton("Right").IsDown())
				{
					result.strafe += 1f;
				}
				if (GameInput.GetButton("Left").IsDown())
				{
					result.strafe -= 1f;
				}
				result.yaw = GameInput.mouseDeltaX + HumanController.InputSample.yawSensitivityJoy * Input.GetAxis("Yaw") * deltaTime;
				result.pitch = GameInput.mouseDeltaY + HumanController.InputSample.pitchSensitivityJoy * Input.GetAxis("Pitch") * deltaTime;
				if (input.flipy)
				{
					result.pitch *= -1f;
				}
				result.jump = GameInput.GetButton("Jump").IsDown();
				result.crouch = GameInput.GetButton("Duck").IsDown();
				result.sprint = GameInput.GetButton("Sprint").IsDown();
				result.aim = false;
				result.attack = GameInput.GetButton("Fire").IsDown();
				result.attack2 = GameInput.GetButton("AltFire").IsDown();
				result.reload = GameInput.GetButton("Reload").IsDown();
				result.inventory = GameInput.GetButton("Inventory").IsPressed();
				result.lamp = ((!noLamp) ? HumanController.InputSample.saved.GetLamp(GameInput.GetButton("Flashlight").IsPressed()) : HumanController.InputSample.saved.lamp);
				result.laser = ((!noLaser) ? HumanController.InputSample.saved.GetLaser(GameInput.GetButton("Laser").IsPressed()) : HumanController.InputSample.saved.laser);
			}
			if (GameInput.GetButton("Chat").IsPressed())
			{
				ChatUI.Open();
			}
			return result;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000125CC File Offset: 0x000107CC
		public static int PollItemButtons()
		{
			if (LockCursorManager.keySubsetEnabled)
			{
				for (int i = 0; i < HumanController.InputSample.kUseButtons.Length; i++)
				{
					if (Input.GetButtonDown(HumanController.InputSample.kUseButtons[i]))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x040002F3 RID: 755
		public const string kButtonAim = "Aim";

		// Token: 0x040002F4 RID: 756
		public const string kRawYaw = "Mouse X";

		// Token: 0x040002F5 RID: 757
		public const string kRawPitch = "Mouse Y";

		// Token: 0x040002F6 RID: 758
		public const string kYaw = "Yaw";

		// Token: 0x040002F7 RID: 759
		public const string kPitch = "Pitch";

		// Token: 0x040002F8 RID: 760
		public const string kButtonUse = "WorldUse";

		// Token: 0x040002F9 RID: 761
		public static float MovementScale = 1f;

		// Token: 0x040002FA RID: 762
		public float walk;

		// Token: 0x040002FB RID: 763
		public float strafe;

		// Token: 0x040002FC RID: 764
		public float yaw;

		// Token: 0x040002FD RID: 765
		public float pitch;

		// Token: 0x040002FE RID: 766
		public bool jump;

		// Token: 0x040002FF RID: 767
		public bool crouch;

		// Token: 0x04000300 RID: 768
		public bool sprint;

		// Token: 0x04000301 RID: 769
		public bool aim;

		// Token: 0x04000302 RID: 770
		public bool attack;

		// Token: 0x04000303 RID: 771
		public bool attack2;

		// Token: 0x04000304 RID: 772
		public bool reload;

		// Token: 0x04000305 RID: 773
		public bool inventory;

		// Token: 0x04000306 RID: 774
		public bool lamp;

		// Token: 0x04000307 RID: 775
		public bool laser;

		// Token: 0x04000308 RID: 776
		public bool info__crouchBlocked;

		// Token: 0x04000309 RID: 777
		private static float yawSensitivityJoy = 30f;

		// Token: 0x0400030A RID: 778
		private static float pitchSensitivityJoy = 30f;

		// Token: 0x0400030B RID: 779
		private static readonly string[] kUseButtons = new string[]
		{
			"UseItem1",
			"UseItem2",
			"UseItem3",
			"UseItem4",
			"UseItem5",
			"UseItem6"
		};

		// Token: 0x020000A4 RID: 164
		private static class saved
		{
			// Token: 0x06000395 RID: 917 RVA: 0x0001264C File Offset: 0x0001084C
			public static bool GetLamp(bool pressed)
			{
				if (pressed)
				{
					HumanController.InputSample.saved.lamp = !HumanController.InputSample.saved.lamp;
					PlayerPrefs.SetInt("LAMP", (!HumanController.InputSample.saved.lamp) ? 0 : 1);
				}
				return HumanController.InputSample.saved.lamp;
			}

			// Token: 0x06000396 RID: 918 RVA: 0x00012684 File Offset: 0x00010884
			public static bool GetLaser(bool pressed)
			{
				if (pressed)
				{
					HumanController.InputSample.saved.laser = !HumanController.InputSample.saved.laser;
					PlayerPrefs.SetInt("LASER", (!HumanController.InputSample.saved.laser) ? 0 : 1);
				}
				return HumanController.InputSample.saved.laser;
			}

			// Token: 0x0400030C RID: 780
			public static bool lamp = PlayerPrefs.GetInt("LAMP", 1) != 0;

			// Token: 0x0400030D RID: 781
			public static bool laser = PlayerPrefs.GetInt("LASER", 1) != 0;
		}
	}
}
