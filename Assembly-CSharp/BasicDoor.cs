using System;
using Facepunch;
using UnityEngine;

// Token: 0x02000711 RID: 1809
[global::NGCAutoAddScript]
[AddComponentMenu("")]
public abstract class BasicDoor : NetBehaviour, global::IServerSaveable, global::IActivatable, global::IActivatableToggle, global::IContextRequestable, global::IContextRequestableMenu, global::IContextRequestableQuick, global::IContextRequestableStatus, global::IContextRequestableText, global::IContextRequestableSoleAccess, global::IContextRequestablePointText, global::IComponentInterface<global::IActivatable, MonoBehaviour, global::Activatable>, global::IComponentInterface<global::IActivatable, MonoBehaviour>, global::IComponentInterface<global::IActivatable>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour, global::Contextual>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour>, global::IComponentInterface<global::IContextRequestable>
{
	// Token: 0x06003C29 RID: 15401 RVA: 0x000D6F08 File Offset: 0x000D5108
	global::ActivationResult global::IActivatableToggle.ActTrigger(global::Character instigator, global::ActivationToggleState toggleTarget, ulong timestamp)
	{
		return this.ActTrigger(instigator, toggleTarget, timestamp);
	}

	// Token: 0x06003C2A RID: 15402 RVA: 0x000D6F14 File Offset: 0x000D5114
	global::ActivationToggleState global::IActivatableToggle.ActGetToggleState()
	{
		return this.ActGetToggleState();
	}

	// Token: 0x06003C2B RID: 15403 RVA: 0x000D6F1C File Offset: 0x000D511C
	global::ActivationResult global::IActivatable.ActTrigger(global::Character instigator, ulong timestamp)
	{
		return this.ActTrigger(instigator, (!this.on) ? global::ActivationToggleState.On : global::ActivationToggleState.Off, timestamp);
	}

	// Token: 0x06003C2C RID: 15404 RVA: 0x000D6F38 File Offset: 0x000D5138
	string global::IContextRequestableText.ContextText(global::Controllable localControllable)
	{
		return this.ContextText(localControllable);
	}

	// Token: 0x06003C2D RID: 15405 RVA: 0x000D6F44 File Offset: 0x000D5144
	bool global::IContextRequestablePointText.ContextTextPoint(out Vector3 worldPoint)
	{
		return this.ContextTextPoint(out worldPoint);
	}

	// Token: 0x06003C2E RID: 15406 RVA: 0x000D6F50 File Offset: 0x000D5150
	global::ContextStatusFlags global::IContextRequestableStatus.ContextStatusPoll()
	{
		return this.ContextStatusPoll();
	}

	// Token: 0x17000B8C RID: 2956
	// (get) Token: 0x06003C2F RID: 15407 RVA: 0x000D6F58 File Offset: 0x000D5158
	// (set) Token: 0x06003C30 RID: 15408 RVA: 0x000D6F68 File Offset: 0x000D5168
	public bool startsOpened
	{
		get
		{
			return (this.startConfig & global::BasicDoor.RunFlags.OpenedForward) == global::BasicDoor.RunFlags.OpenedForward;
		}
		protected set
		{
			if (value)
			{
				this.startConfig |= global::BasicDoor.RunFlags.OpenedForward;
			}
			else
			{
				this.startConfig &= (global::BasicDoor.RunFlags)(-2);
			}
		}
	}

	// Token: 0x17000B8D RID: 2957
	// (get) Token: 0x06003C31 RID: 15409 RVA: 0x000D6FA0 File Offset: 0x000D51A0
	// (set) Token: 0x06003C32 RID: 15410 RVA: 0x000D6FB0 File Offset: 0x000D51B0
	public bool defaultReversed
	{
		get
		{
			return (this.startConfig & (global::BasicDoor.RunFlags)18) == global::BasicDoor.RunFlags.ClosedReverse;
		}
		protected set
		{
			if (value)
			{
				this.startConfig |= global::BasicDoor.RunFlags.ClosedReverse;
			}
			else
			{
				this.startConfig &= (global::BasicDoor.RunFlags)(-3);
			}
		}
	}

	// Token: 0x17000B8E RID: 2958
	// (get) Token: 0x06003C33 RID: 15411 RVA: 0x000D6FE8 File Offset: 0x000D51E8
	// (set) Token: 0x06003C34 RID: 15412 RVA: 0x000D6FF8 File Offset: 0x000D51F8
	public bool reverseOpenDisabled
	{
		get
		{
			return (this.startConfig & global::BasicDoor.RunFlags.ClosedNoReverse) == global::BasicDoor.RunFlags.ClosedNoReverse;
		}
		protected set
		{
			if (value)
			{
				this.startConfig |= global::BasicDoor.RunFlags.ClosedNoReverse;
			}
			else
			{
				this.startConfig &= (global::BasicDoor.RunFlags)(-17);
			}
		}
	}

	// Token: 0x17000B8F RID: 2959
	// (get) Token: 0x06003C35 RID: 15413 RVA: 0x000D7024 File Offset: 0x000D5224
	// (set) Token: 0x06003C36 RID: 15414 RVA: 0x000D7030 File Offset: 0x000D5230
	public bool canOpenReverse
	{
		get
		{
			return !this.reverseOpenDisabled;
		}
		protected set
		{
			this.reverseOpenDisabled = !value;
		}
	}

	// Token: 0x17000B90 RID: 2960
	// (get) Token: 0x06003C37 RID: 15415 RVA: 0x000D703C File Offset: 0x000D523C
	// (set) Token: 0x06003C38 RID: 15416 RVA: 0x000D704C File Offset: 0x000D524C
	public bool fixedUpdate
	{
		get
		{
			return (this.startConfig & global::BasicDoor.RunFlags.FixedUpdateClosedForward) == global::BasicDoor.RunFlags.FixedUpdateClosedForward;
		}
		protected set
		{
			if (value)
			{
				this.startConfig |= global::BasicDoor.RunFlags.FixedUpdateClosedForward;
			}
			else
			{
				this.startConfig &= (global::BasicDoor.RunFlags)(-9);
			}
		}
	}

	// Token: 0x17000B91 RID: 2961
	// (get) Token: 0x06003C39 RID: 15417 RVA: 0x000D7084 File Offset: 0x000D5284
	// (set) Token: 0x06003C3A RID: 15418 RVA: 0x000D7094 File Offset: 0x000D5294
	public bool pointText
	{
		get
		{
			return (this.startConfig & global::BasicDoor.RunFlags.ClosedForwardWithPointText) == global::BasicDoor.RunFlags.ClosedForwardWithPointText;
		}
		protected set
		{
			if (value)
			{
				this.startConfig |= global::BasicDoor.RunFlags.ClosedForwardWithPointText;
			}
			else
			{
				this.startConfig &= (global::BasicDoor.RunFlags)(-5);
			}
		}
	}

	// Token: 0x17000B92 RID: 2962
	// (get) Token: 0x06003C3B RID: 15419 RVA: 0x000D70CC File Offset: 0x000D52CC
	// (set) Token: 0x06003C3C RID: 15420 RVA: 0x000D70DC File Offset: 0x000D52DC
	public bool waitsTarget
	{
		get
		{
			return (this.startConfig & global::BasicDoor.RunFlags.ClosedForwardWaits) == global::BasicDoor.RunFlags.ClosedForwardWaits;
		}
		protected set
		{
			if (value)
			{
				this.startConfig |= global::BasicDoor.RunFlags.ClosedForwardWaits;
			}
			else
			{
				this.startConfig &= (global::BasicDoor.RunFlags)(-33);
			}
		}
	}

	// Token: 0x06003C3D RID: 15421 RVA: 0x000D7108 File Offset: 0x000D5308
	protected global::ActivationToggleState ActGetToggleState()
	{
		return (!this.on) ? global::ActivationToggleState.Off : global::ActivationToggleState.On;
	}

	// Token: 0x06003C3E RID: 15422 RVA: 0x000D711C File Offset: 0x000D531C
	protected global::ActivationResult ActTrigger(global::Character instigator, global::ActivationToggleState toggleTarget, ulong timestamp)
	{
		if (toggleTarget != global::ActivationToggleState.On)
		{
			if (toggleTarget != global::ActivationToggleState.Off)
			{
				return global::ActivationResult.Fail_BadToggle;
			}
			if (!this.on)
			{
				return global::ActivationResult.Fail_Redundant;
			}
			this.ToggleStateServer(timestamp, instigator);
			return (!this.on) ? global::ActivationResult.Success : global::ActivationResult.Fail_Busy;
		}
		else
		{
			if (this.on)
			{
				return global::ActivationResult.Fail_Redundant;
			}
			this.ToggleStateServer(timestamp, instigator);
			return (!this.on) ? global::ActivationResult.Fail_Busy : global::ActivationResult.Success;
		}
	}

	// Token: 0x17000B93 RID: 2963
	// (get) Token: 0x06003C3F RID: 15423 RVA: 0x000D7194 File Offset: 0x000D5394
	protected static ulong time
	{
		get
		{
			return global::NetCull.timeInMillis;
		}
	}

	// Token: 0x17000B94 RID: 2964
	// (get) Token: 0x06003C40 RID: 15424 RVA: 0x000D719C File Offset: 0x000D539C
	protected double elapsed
	{
		get
		{
			if (this.timeStampChanged != null)
			{
				return (global::BasicDoor.time - this.timeStampChanged.Value) / 1000.0;
			}
			return double.PositiveInfinity;
		}
	}

	// Token: 0x06003C41 RID: 15425 RVA: 0x000D71D8 File Offset: 0x000D53D8
	private void CaptureOriginals()
	{
		if (!this.capturedOriginals)
		{
			this.originalLocalRotation = base.transform.localRotation;
			this.originalLocalPosition = base.transform.localPosition;
			this.originalLocalScale = base.transform.localScale;
			this.capturedOriginals = true;
		}
	}

	// Token: 0x06003C42 RID: 15426 RVA: 0x000D722C File Offset: 0x000D542C
	protected void StartOpeningOrClosing(sbyte open, ulong timestamp)
	{
		bool flag = this.openingInReverse;
		global::BasicDoor.State state;
		long num2;
		if ((int)open != 0)
		{
			if (this.state == global::BasicDoor.State.Closed)
			{
				flag = (this.canOpenReverse && (int)open == 2);
			}
			state = global::BasicDoor.State.Opened;
			if (state == this.state || state == this.state + 1)
			{
				return;
			}
			double elapsed = this.elapsed;
			double num = ((double)this.durationClose > 0.0) ? ((elapsed < (double)this.durationClose) ? (1.0 - elapsed / (double)this.durationClose) : 0.0) : 0.0;
			num2 = (long)(num * (double)this.durationOpen * 1000.0);
		}
		else
		{
			state = global::BasicDoor.State.Closed;
			if (state == this.state || state == this.state + 1)
			{
				return;
			}
			double elapsed2 = this.elapsed;
			double num3 = ((double)this.durationOpen > 0.0) ? ((elapsed2 < (double)this.durationOpen) ? (elapsed2 / (double)this.durationOpen) : 1.0) : 1.0;
			num2 = (long)((1.0 - num3) * (double)this.durationClose * 1000.0);
		}
		if (num2 > (long)timestamp)
		{
			this.timeStampChanged = null;
		}
		else
		{
			this.timeStampChanged = new ulong?(timestamp - (ulong)num2);
		}
		base.enabled = true;
		this.openingInReverse = flag;
		this.target = state;
	}

	// Token: 0x06003C43 RID: 15427 RVA: 0x000D73CC File Offset: 0x000D55CC
	protected void DoDoorFraction(double fractionOpen)
	{
		if (this.openingInReverse)
		{
			this.OnDoorFraction(-fractionOpen);
		}
		else
		{
			this.OnDoorFraction(fractionOpen);
		}
	}

	// Token: 0x06003C44 RID: 15428
	protected abstract void OnDoorFraction(double fractionOpen);

	// Token: 0x06003C45 RID: 15429 RVA: 0x000D73F0 File Offset: 0x000D55F0
	private void DoorUpdate()
	{
		double elapsed = this.elapsed;
		if (elapsed <= 0.0)
		{
			return;
		}
		bool flag = this.state != this.target;
		switch (this.target)
		{
		case global::BasicDoor.State.Opened:
			if (elapsed >= (double)this.durationOpen)
			{
				base.enabled = false;
				this.state = global::BasicDoor.State.Opened;
				this.DoDoorFraction(1.0);
				if (flag)
				{
					this.OnDoorEndOpen();
				}
			}
			else
			{
				if (this.state == global::BasicDoor.State.Closed)
				{
					this.OnDoorStartOpen();
				}
				this.state = global::BasicDoor.State.Opening;
				this.DoDoorFraction(elapsed / (double)this.durationOpen);
			}
			break;
		case global::BasicDoor.State.Closed:
			if (elapsed >= (double)this.durationClose)
			{
				base.enabled = false;
				this.state = global::BasicDoor.State.Closed;
				this.DoDoorFraction(0.0);
				if (flag)
				{
					this.OnDoorEndClose();
				}
			}
			else
			{
				if (this.state == global::BasicDoor.State.Opened)
				{
					this.OnDoorStartClose();
				}
				this.state = global::BasicDoor.State.Closing;
				this.DoDoorFraction(1.0 - elapsed / (double)this.durationClose);
			}
			break;
		}
	}

	// Token: 0x06003C46 RID: 15430 RVA: 0x000D7520 File Offset: 0x000D5720
	protected void LateUpdate()
	{
		if (!this.fixedUpdate)
		{
			this.DoorUpdate();
		}
	}

	// Token: 0x06003C47 RID: 15431 RVA: 0x000D7534 File Offset: 0x000D5734
	protected void FixedUpdate()
	{
		if (this.fixedUpdate)
		{
			this.DoorUpdate();
		}
	}

	// Token: 0x06003C48 RID: 15432 RVA: 0x000D7548 File Offset: 0x000D5748
	protected string ContextText(global::Controllable localControllable)
	{
		switch (this.state)
		{
		case global::BasicDoor.State.Opened:
			return this.textClose;
		case global::BasicDoor.State.Closed:
			return this.textOpen;
		}
		return null;
	}

	// Token: 0x06003C49 RID: 15433 RVA: 0x000D7584 File Offset: 0x000D5784
	protected bool ContextTextPoint(out Vector3 worldPoint)
	{
		if (this.pointText)
		{
			switch (this.state)
			{
			case global::BasicDoor.State.Opened:
				worldPoint = base.transform.TransformPoint(this.pointTextPointOpened);
				return true;
			case global::BasicDoor.State.Closed:
				worldPoint = base.transform.TransformPoint(this.pointTextPointClosed);
				return true;
			}
		}
		worldPoint = default(Vector3);
		return false;
	}

	// Token: 0x06003C4A RID: 15434 RVA: 0x000D7604 File Offset: 0x000D5804
	protected global::ContextStatusFlags ContextStatusPoll()
	{
		switch (this.state)
		{
		case global::BasicDoor.State.Opened:
		case global::BasicDoor.State.Closed:
			return (global::ContextStatusFlags)0;
		default:
			return global::ContextStatusFlags.ObjectBusy | global::ContextStatusFlags.SpriteFlag0;
		}
	}

	// Token: 0x06003C4B RID: 15435 RVA: 0x000D7638 File Offset: 0x000D5838
	private void PlaySound(AudioClip clip)
	{
		if (clip)
		{
			clip.Play(base.transform.position, 1f, 5f, 20f);
		}
	}

	// Token: 0x06003C4C RID: 15436 RVA: 0x000D7670 File Offset: 0x000D5870
	protected void OnDoorStartOpen()
	{
		this.PlaySound(this.openSound);
	}

	// Token: 0x06003C4D RID: 15437 RVA: 0x000D7680 File Offset: 0x000D5880
	protected void OnDoorEndOpen()
	{
		this.PlaySound(this.openedSound);
		this.DisableObstacle();
	}

	// Token: 0x06003C4E RID: 15438 RVA: 0x000D7694 File Offset: 0x000D5894
	protected virtual void OnDoorStartClose()
	{
		this.PlaySound(this.closeSound);
	}

	// Token: 0x06003C4F RID: 15439 RVA: 0x000D76A4 File Offset: 0x000D58A4
	protected virtual void OnDoorEndClose()
	{
		this.PlaySound(this.closedSound);
		this.EnableObstacle();
	}

	// Token: 0x17000B95 RID: 2965
	// (get) Token: 0x06003C50 RID: 15440 RVA: 0x000D76B8 File Offset: 0x000D58B8
	private bool on
	{
		get
		{
			return this.target == global::BasicDoor.State.Opened || this.target == global::BasicDoor.State.Opening;
		}
	}

	// Token: 0x06003C51 RID: 15441 RVA: 0x000D76D4 File Offset: 0x000D58D4
	private global::BasicDoor.Side CalculateOpenWay()
	{
		return (!this.openingInReverse && this.canOpenReverse) ? global::BasicDoor.Side.Reverse : global::BasicDoor.Side.Forward;
	}

	// Token: 0x06003C52 RID: 15442 RVA: 0x000D76F4 File Offset: 0x000D58F4
	private global::BasicDoor.Side CalculateOpenWay(Vector3 worldPoint)
	{
		global::BasicDoor.IdealSide idealSide;
		if (!this.canOpenReverse || (int)(idealSide = this.IdealSideForPoint(worldPoint)) == 1)
		{
			return global::BasicDoor.Side.Forward;
		}
		if ((int)idealSide == 0)
		{
			return (!this.openingInReverse) ? global::BasicDoor.Side.Reverse : global::BasicDoor.Side.Forward;
		}
		return global::BasicDoor.Side.Reverse;
	}

	// Token: 0x06003C53 RID: 15443 RVA: 0x000D773C File Offset: 0x000D593C
	private global::BasicDoor.Side CalculateOpenWay(Vector3? worldPoint)
	{
		return (worldPoint == null) ? this.CalculateOpenWay() : this.CalculateOpenWay(worldPoint.Value);
	}

	// Token: 0x06003C54 RID: 15444
	protected abstract global::BasicDoor.IdealSide IdealSideForPoint(Vector3 worldPoint);

	// Token: 0x06003C55 RID: 15445 RVA: 0x000D7770 File Offset: 0x000D5970
	private bool ToggleStateServer(Vector3? openerPoint, ulong timestamp, bool? fallbackReverse = null)
	{
		if (this.serverLastTimeStamp == null || timestamp > this.serverLastTimeStamp.Value)
		{
			if (this.waitsTarget && (this.state == global::BasicDoor.State.Opening || this.state == global::BasicDoor.State.Closing))
			{
				return false;
			}
			this.serverLastTimeStamp = new ulong?(timestamp);
			global::BasicDoor.State state = this.target;
			bool flag = this.openingInReverse;
			if (this.target == global::BasicDoor.State.Closed)
			{
				if (openerPoint != null || fallbackReverse == null)
				{
					if (this.CalculateOpenWay(openerPoint) == global::BasicDoor.Side.Forward)
					{
						this.StartOpeningOrClosing(1, timestamp);
					}
					else
					{
						this.StartOpeningOrClosing(2, timestamp);
					}
				}
				else
				{
					this.StartOpeningOrClosing((!((fallbackReverse == null) ? this.defaultReversed : fallbackReverse.Value)) ? 1 : 2, timestamp);
				}
			}
			else
			{
				this.StartOpeningOrClosing(0, timestamp);
			}
			if (state != this.target || flag != this.openingInReverse)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003C56 RID: 15446 RVA: 0x000D7880 File Offset: 0x000D5A80
	private bool ToggleStateServer(ulong timestamp, global::Character instigator)
	{
		if (instigator)
		{
			return this.ToggleStateServer(new Vector3?(instigator.eyesOrigin), timestamp, null);
		}
		return this.ToggleStateServer(null, timestamp, null);
	}

	// Token: 0x06003C57 RID: 15447 RVA: 0x000D78D0 File Offset: 0x000D5AD0
	private void InitializeObstacle()
	{
		NavMeshObstacle component = base.GetComponent<NavMeshObstacle>();
		if (component)
		{
			Object.Destroy(component);
		}
	}

	// Token: 0x06003C58 RID: 15448 RVA: 0x000D78F8 File Offset: 0x000D5AF8
	protected void EnableObstacle()
	{
	}

	// Token: 0x06003C59 RID: 15449 RVA: 0x000D78FC File Offset: 0x000D5AFC
	protected void DisableObstacle()
	{
	}

	// Token: 0x06003C5A RID: 15450 RVA: 0x000D7900 File Offset: 0x000D5B00
	[RPC]
	protected void DOo(sbyte open, ulong timestamp)
	{
		this.CaptureOriginals();
		if ((int)open != 0)
		{
			this.openingInReverse = ((int)open == 2);
		}
		this.StartOpeningOrClosing(open, timestamp);
	}

	// Token: 0x06003C5B RID: 15451 RVA: 0x000D7930 File Offset: 0x000D5B30
	[RPC]
	protected void DOc(sbyte open)
	{
		this.CaptureOriginals();
		long num;
		if ((int)open != 0)
		{
			this.state = (this.target = global::BasicDoor.State.Opened);
			num = (long)((double)this.durationOpen * 1000.0);
			this.openingInReverse = ((int)open == 2);
			this.DoDoorFraction(1.0);
		}
		else
		{
			this.state = (this.target = global::BasicDoor.State.Closed);
			num = (long)((double)this.durationOpen * 1000.0);
			this.DoDoorFraction(0.0);
		}
		ulong time = global::BasicDoor.time;
		if (num > (long)time)
		{
			this.timeStampChanged = new ulong?(time - (ulong)num);
		}
		else
		{
			this.timeStampChanged = null;
		}
	}

	// Token: 0x06003C5C RID: 15452 RVA: 0x000D79F0 File Offset: 0x000D5BF0
	protected void Awake()
	{
		this.CaptureOriginals();
		this.openingInReverse = this.defaultReversed;
		this.InitializeObstacle();
		if (this.startsOpened)
		{
			this.target = (this.state = global::BasicDoor.State.Opened);
			this.DoDoorFraction(1.0);
		}
		else
		{
			this.target = (this.state = global::BasicDoor.State.Closed);
			this.DoDoorFraction(0.0);
		}
		base.enabled = false;
	}

	// Token: 0x06003C5D RID: 15453 RVA: 0x000D7A6C File Offset: 0x000D5C6C
	protected void OnDestroy()
	{
	}

	// Token: 0x06003C5E RID: 15454 RVA: 0x000D7A70 File Offset: 0x000D5C70
	protected void PlayerConnected(global::PlayerClient player)
	{
	}

	// Token: 0x04001E42 RID: 7746
	private const global::BasicDoor.RunFlags kRF_StartOpen_Mask = global::BasicDoor.RunFlags.OpenedForward;

	// Token: 0x04001E43 RID: 7747
	private const global::BasicDoor.RunFlags kRF_StartOpen_Value = global::BasicDoor.RunFlags.OpenedForward;

	// Token: 0x04001E44 RID: 7748
	private const global::BasicDoor.RunFlags kRF_DefaultReverse_Mask = (global::BasicDoor.RunFlags)18;

	// Token: 0x04001E45 RID: 7749
	private const global::BasicDoor.RunFlags kRF_DefaultReverse_Value = global::BasicDoor.RunFlags.ClosedReverse;

	// Token: 0x04001E46 RID: 7750
	private const global::BasicDoor.RunFlags kRF_DisableReverse_Mask = global::BasicDoor.RunFlags.ClosedNoReverse;

	// Token: 0x04001E47 RID: 7751
	private const global::BasicDoor.RunFlags kRF_DisableReverse_Value = global::BasicDoor.RunFlags.ClosedNoReverse;

	// Token: 0x04001E48 RID: 7752
	private const global::BasicDoor.RunFlags kRF_FixedUpdate_Mask = global::BasicDoor.RunFlags.FixedUpdateClosedForward;

	// Token: 0x04001E49 RID: 7753
	private const global::BasicDoor.RunFlags kRF_FixedUpdate_Value = global::BasicDoor.RunFlags.FixedUpdateClosedForward;

	// Token: 0x04001E4A RID: 7754
	private const global::BasicDoor.RunFlags kRF_PointText_Mask = global::BasicDoor.RunFlags.ClosedForwardWithPointText;

	// Token: 0x04001E4B RID: 7755
	private const global::BasicDoor.RunFlags kRF_PointText_Value = global::BasicDoor.RunFlags.ClosedForwardWithPointText;

	// Token: 0x04001E4C RID: 7756
	private const global::BasicDoor.RunFlags kRF_WaitsTarget_Mask = global::BasicDoor.RunFlags.ClosedForwardWaits;

	// Token: 0x04001E4D RID: 7757
	private const global::BasicDoor.RunFlags kRF_WaitsTarget_Value = global::BasicDoor.RunFlags.ClosedForwardWaits;

	// Token: 0x04001E4E RID: 7758
	private const float kVolume = 1f;

	// Token: 0x04001E4F RID: 7759
	private const float kMinDistance = 5f;

	// Token: 0x04001E50 RID: 7760
	private const float kMaxDistance = 20f;

	// Token: 0x04001E51 RID: 7761
	private const sbyte kOpenForward = 1;

	// Token: 0x04001E52 RID: 7762
	private const sbyte kOpenBackward = 2;

	// Token: 0x04001E53 RID: 7763
	private const sbyte kClose = 0;

	// Token: 0x04001E54 RID: 7764
	private const string kRPCName_SetOpenOrClosed = "DOo";

	// Token: 0x04001E55 RID: 7765
	private const string kRPCName_ConnectSetup = "DOc";

	// Token: 0x04001E56 RID: 7766
	[SerializeField]
	private global::BasicDoor.RunFlags startConfig;

	// Token: 0x04001E57 RID: 7767
	[NonSerialized]
	protected Vector3 originalLocalPosition;

	// Token: 0x04001E58 RID: 7768
	[NonSerialized]
	protected Quaternion originalLocalRotation;

	// Token: 0x04001E59 RID: 7769
	[NonSerialized]
	protected Vector3 originalLocalScale;

	// Token: 0x04001E5A RID: 7770
	[NonSerialized]
	private ulong? timeStampChanged;

	// Token: 0x04001E5B RID: 7771
	[SerializeField]
	protected float durationClose = 1f;

	// Token: 0x04001E5C RID: 7772
	[SerializeField]
	protected float durationOpen = 1f;

	// Token: 0x04001E5D RID: 7773
	[NonSerialized]
	private bool capturedOriginals;

	// Token: 0x04001E5E RID: 7774
	[SerializeField]
	protected string textOpen = "Open";

	// Token: 0x04001E5F RID: 7775
	[SerializeField]
	protected string textClose = "Close";

	// Token: 0x04001E60 RID: 7776
	[SerializeField]
	protected Vector3 pointTextPointOpened;

	// Token: 0x04001E61 RID: 7777
	[SerializeField]
	protected Vector3 pointTextPointClosed;

	// Token: 0x04001E62 RID: 7778
	[SerializeField]
	protected AudioClip openSound;

	// Token: 0x04001E63 RID: 7779
	[SerializeField]
	protected AudioClip openedSound;

	// Token: 0x04001E64 RID: 7780
	[SerializeField]
	protected AudioClip closeSound;

	// Token: 0x04001E65 RID: 7781
	[SerializeField]
	protected AudioClip closedSound;

	// Token: 0x04001E66 RID: 7782
	[SerializeField]
	protected float minimumTimeBetweenOpenClose = 1f;

	// Token: 0x04001E67 RID: 7783
	[NonSerialized]
	private ulong? serverLastTimeStamp;

	// Token: 0x04001E68 RID: 7784
	[NonSerialized]
	private global::BasicDoor.State state;

	// Token: 0x04001E69 RID: 7785
	[NonSerialized]
	private global::BasicDoor.State target;

	// Token: 0x04001E6A RID: 7786
	[NonSerialized]
	private bool openingInReverse;

	// Token: 0x02000712 RID: 1810
	private enum RunFlags
	{
		// Token: 0x04001E6C RID: 7788
		ClosedForward,
		// Token: 0x04001E6D RID: 7789
		OpenedForward,
		// Token: 0x04001E6E RID: 7790
		ClosedReverse,
		// Token: 0x04001E6F RID: 7791
		OpenedReverse,
		// Token: 0x04001E70 RID: 7792
		ClosedForwardWithPointText,
		// Token: 0x04001E71 RID: 7793
		OpenedForwardWithPointText,
		// Token: 0x04001E72 RID: 7794
		ClosedReverseWithPointText,
		// Token: 0x04001E73 RID: 7795
		OpenedReverseWithPointText,
		// Token: 0x04001E74 RID: 7796
		FixedUpdateClosedForward,
		// Token: 0x04001E75 RID: 7797
		FixedUpdateOpenedForward,
		// Token: 0x04001E76 RID: 7798
		FixedUpdateClosedReverse,
		// Token: 0x04001E77 RID: 7799
		FixedUpdateOpenedReverse,
		// Token: 0x04001E78 RID: 7800
		FixedUpdateClosedForwardWithPointText,
		// Token: 0x04001E79 RID: 7801
		FixedUpdateOpenedForwardWithPointText,
		// Token: 0x04001E7A RID: 7802
		FixedUpdateClosedReverseWithPointText,
		// Token: 0x04001E7B RID: 7803
		FixedUpdateOpenedReverseWithPointText,
		// Token: 0x04001E7C RID: 7804
		ClosedNoReverse,
		// Token: 0x04001E7D RID: 7805
		OpenedNoReverse,
		// Token: 0x04001E7E RID: 7806
		ClosedNoReverseWithPointText = 20,
		// Token: 0x04001E7F RID: 7807
		OpenedNoReverseWithPointText,
		// Token: 0x04001E80 RID: 7808
		FixedUpdateClosedNoReverse = 24,
		// Token: 0x04001E81 RID: 7809
		FixedUpdateOpenedNoReverse,
		// Token: 0x04001E82 RID: 7810
		FixedUpdateClosedNoReverseWithPointText = 28,
		// Token: 0x04001E83 RID: 7811
		FixedUpdateOpenedNoReverseWithPointText,
		// Token: 0x04001E84 RID: 7812
		ClosedForwardWaits = 32,
		// Token: 0x04001E85 RID: 7813
		OpenedForwardWaits,
		// Token: 0x04001E86 RID: 7814
		ClosedReverseWaits,
		// Token: 0x04001E87 RID: 7815
		OpenedReverseWaits,
		// Token: 0x04001E88 RID: 7816
		ClosedForwardWaitsWithPointText,
		// Token: 0x04001E89 RID: 7817
		OpenedForwardWaitsWithPointText,
		// Token: 0x04001E8A RID: 7818
		ClosedReverseWaitsWithPointText,
		// Token: 0x04001E8B RID: 7819
		OpenedReverseWaitsWithPointText,
		// Token: 0x04001E8C RID: 7820
		FixedUpdateClosedForwardWaits,
		// Token: 0x04001E8D RID: 7821
		FixedUpdateOpenedForwardWaits,
		// Token: 0x04001E8E RID: 7822
		FixedUpdateClosedReverseWaits,
		// Token: 0x04001E8F RID: 7823
		FixedUpdateOpenedReverseWaits,
		// Token: 0x04001E90 RID: 7824
		FixedUpdateClosedForwardWaitsWithPointText,
		// Token: 0x04001E91 RID: 7825
		FixedUpdateOpenedForwardWaitsWithPointText,
		// Token: 0x04001E92 RID: 7826
		FixedUpdateClosedReverseWaitsWithPointText,
		// Token: 0x04001E93 RID: 7827
		FixedUpdateOpenedReverseWaitsWithPointText,
		// Token: 0x04001E94 RID: 7828
		ClosedNoReverseWaits,
		// Token: 0x04001E95 RID: 7829
		OpenedNoReverseWaits,
		// Token: 0x04001E96 RID: 7830
		ClosedNoReverseWithPointTextWaits = 52,
		// Token: 0x04001E97 RID: 7831
		OpenedNoReverseWithPointTextWaits,
		// Token: 0x04001E98 RID: 7832
		FixedUpdateClosedNoReverseWaits = 56,
		// Token: 0x04001E99 RID: 7833
		FixedUpdateOpenedNoReverseWaits,
		// Token: 0x04001E9A RID: 7834
		FixedUpdateClosedNoReverseWaitsWithPointText = 60,
		// Token: 0x04001E9B RID: 7835
		FixedUpdateOpenedNoReverseWaitsWithPointText
	}

	// Token: 0x02000713 RID: 1811
	private enum State : byte
	{
		// Token: 0x04001E9D RID: 7837
		Opening,
		// Token: 0x04001E9E RID: 7838
		Opened,
		// Token: 0x04001E9F RID: 7839
		Closing,
		// Token: 0x04001EA0 RID: 7840
		Closed
	}

	// Token: 0x02000714 RID: 1812
	private enum Side : byte
	{
		// Token: 0x04001EA2 RID: 7842
		Forward,
		// Token: 0x04001EA3 RID: 7843
		Reverse
	}

	// Token: 0x02000715 RID: 1813
	protected enum IdealSide : sbyte
	{
		// Token: 0x04001EA5 RID: 7845
		Unknown,
		// Token: 0x04001EA6 RID: 7846
		Reverse = -1,
		// Token: 0x04001EA7 RID: 7847
		Forward = 1
	}
}
