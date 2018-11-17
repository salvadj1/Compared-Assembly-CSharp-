using System;
using Facepunch;
using UnityEngine;

// Token: 0x0200064E RID: 1614
[NGCAutoAddScript]
[AddComponentMenu("")]
public abstract class BasicDoor : NetBehaviour, IServerSaveable, IActivatable, IActivatableToggle, IContextRequestable, IContextRequestableMenu, IContextRequestableQuick, IContextRequestableStatus, IContextRequestableText, IContextRequestableSoleAccess, IContextRequestablePointText, IComponentInterface<IActivatable, MonoBehaviour, Activatable>, IComponentInterface<IActivatable, MonoBehaviour>, IComponentInterface<IActivatable>, IComponentInterface<IContextRequestable, MonoBehaviour, Contextual>, IComponentInterface<IContextRequestable, MonoBehaviour>, IComponentInterface<IContextRequestable>
{
	// Token: 0x0600383D RID: 14397 RVA: 0x000CE658 File Offset: 0x000CC858
	ActivationResult IActivatableToggle.ActTrigger(Character instigator, ActivationToggleState toggleTarget, ulong timestamp)
	{
		return this.ActTrigger(instigator, toggleTarget, timestamp);
	}

	// Token: 0x0600383E RID: 14398 RVA: 0x000CE664 File Offset: 0x000CC864
	ActivationToggleState IActivatableToggle.ActGetToggleState()
	{
		return this.ActGetToggleState();
	}

	// Token: 0x0600383F RID: 14399 RVA: 0x000CE66C File Offset: 0x000CC86C
	ActivationResult IActivatable.ActTrigger(Character instigator, ulong timestamp)
	{
		return this.ActTrigger(instigator, (!this.on) ? ActivationToggleState.On : ActivationToggleState.Off, timestamp);
	}

	// Token: 0x06003840 RID: 14400 RVA: 0x000CE688 File Offset: 0x000CC888
	string IContextRequestableText.ContextText(Controllable localControllable)
	{
		return this.ContextText(localControllable);
	}

	// Token: 0x06003841 RID: 14401 RVA: 0x000CE694 File Offset: 0x000CC894
	bool IContextRequestablePointText.ContextTextPoint(out Vector3 worldPoint)
	{
		return this.ContextTextPoint(out worldPoint);
	}

	// Token: 0x06003842 RID: 14402 RVA: 0x000CE6A0 File Offset: 0x000CC8A0
	ContextStatusFlags IContextRequestableStatus.ContextStatusPoll()
	{
		return this.ContextStatusPoll();
	}

	// Token: 0x17000B0C RID: 2828
	// (get) Token: 0x06003843 RID: 14403 RVA: 0x000CE6A8 File Offset: 0x000CC8A8
	// (set) Token: 0x06003844 RID: 14404 RVA: 0x000CE6B8 File Offset: 0x000CC8B8
	public bool startsOpened
	{
		get
		{
			return (this.startConfig & BasicDoor.RunFlags.OpenedForward) == BasicDoor.RunFlags.OpenedForward;
		}
		protected set
		{
			if (value)
			{
				this.startConfig |= BasicDoor.RunFlags.OpenedForward;
			}
			else
			{
				this.startConfig &= (BasicDoor.RunFlags)(-2);
			}
		}
	}

	// Token: 0x17000B0D RID: 2829
	// (get) Token: 0x06003845 RID: 14405 RVA: 0x000CE6F0 File Offset: 0x000CC8F0
	// (set) Token: 0x06003846 RID: 14406 RVA: 0x000CE700 File Offset: 0x000CC900
	public bool defaultReversed
	{
		get
		{
			return (this.startConfig & (BasicDoor.RunFlags)18) == BasicDoor.RunFlags.ClosedReverse;
		}
		protected set
		{
			if (value)
			{
				this.startConfig |= BasicDoor.RunFlags.ClosedReverse;
			}
			else
			{
				this.startConfig &= (BasicDoor.RunFlags)(-3);
			}
		}
	}

	// Token: 0x17000B0E RID: 2830
	// (get) Token: 0x06003847 RID: 14407 RVA: 0x000CE738 File Offset: 0x000CC938
	// (set) Token: 0x06003848 RID: 14408 RVA: 0x000CE748 File Offset: 0x000CC948
	public bool reverseOpenDisabled
	{
		get
		{
			return (this.startConfig & BasicDoor.RunFlags.ClosedNoReverse) == BasicDoor.RunFlags.ClosedNoReverse;
		}
		protected set
		{
			if (value)
			{
				this.startConfig |= BasicDoor.RunFlags.ClosedNoReverse;
			}
			else
			{
				this.startConfig &= (BasicDoor.RunFlags)(-17);
			}
		}
	}

	// Token: 0x17000B0F RID: 2831
	// (get) Token: 0x06003849 RID: 14409 RVA: 0x000CE774 File Offset: 0x000CC974
	// (set) Token: 0x0600384A RID: 14410 RVA: 0x000CE780 File Offset: 0x000CC980
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

	// Token: 0x17000B10 RID: 2832
	// (get) Token: 0x0600384B RID: 14411 RVA: 0x000CE78C File Offset: 0x000CC98C
	// (set) Token: 0x0600384C RID: 14412 RVA: 0x000CE79C File Offset: 0x000CC99C
	public bool fixedUpdate
	{
		get
		{
			return (this.startConfig & BasicDoor.RunFlags.FixedUpdateClosedForward) == BasicDoor.RunFlags.FixedUpdateClosedForward;
		}
		protected set
		{
			if (value)
			{
				this.startConfig |= BasicDoor.RunFlags.FixedUpdateClosedForward;
			}
			else
			{
				this.startConfig &= (BasicDoor.RunFlags)(-9);
			}
		}
	}

	// Token: 0x17000B11 RID: 2833
	// (get) Token: 0x0600384D RID: 14413 RVA: 0x000CE7D4 File Offset: 0x000CC9D4
	// (set) Token: 0x0600384E RID: 14414 RVA: 0x000CE7E4 File Offset: 0x000CC9E4
	public bool pointText
	{
		get
		{
			return (this.startConfig & BasicDoor.RunFlags.ClosedForwardWithPointText) == BasicDoor.RunFlags.ClosedForwardWithPointText;
		}
		protected set
		{
			if (value)
			{
				this.startConfig |= BasicDoor.RunFlags.ClosedForwardWithPointText;
			}
			else
			{
				this.startConfig &= (BasicDoor.RunFlags)(-5);
			}
		}
	}

	// Token: 0x17000B12 RID: 2834
	// (get) Token: 0x0600384F RID: 14415 RVA: 0x000CE81C File Offset: 0x000CCA1C
	// (set) Token: 0x06003850 RID: 14416 RVA: 0x000CE82C File Offset: 0x000CCA2C
	public bool waitsTarget
	{
		get
		{
			return (this.startConfig & BasicDoor.RunFlags.ClosedForwardWaits) == BasicDoor.RunFlags.ClosedForwardWaits;
		}
		protected set
		{
			if (value)
			{
				this.startConfig |= BasicDoor.RunFlags.ClosedForwardWaits;
			}
			else
			{
				this.startConfig &= (BasicDoor.RunFlags)(-33);
			}
		}
	}

	// Token: 0x06003851 RID: 14417 RVA: 0x000CE858 File Offset: 0x000CCA58
	protected ActivationToggleState ActGetToggleState()
	{
		return (!this.on) ? ActivationToggleState.Off : ActivationToggleState.On;
	}

	// Token: 0x06003852 RID: 14418 RVA: 0x000CE86C File Offset: 0x000CCA6C
	protected ActivationResult ActTrigger(Character instigator, ActivationToggleState toggleTarget, ulong timestamp)
	{
		if (toggleTarget != ActivationToggleState.On)
		{
			if (toggleTarget != ActivationToggleState.Off)
			{
				return ActivationResult.Fail_BadToggle;
			}
			if (!this.on)
			{
				return ActivationResult.Fail_Redundant;
			}
			this.ToggleStateServer(timestamp, instigator);
			return (!this.on) ? ActivationResult.Success : ActivationResult.Fail_Busy;
		}
		else
		{
			if (this.on)
			{
				return ActivationResult.Fail_Redundant;
			}
			this.ToggleStateServer(timestamp, instigator);
			return (!this.on) ? ActivationResult.Fail_Busy : ActivationResult.Success;
		}
	}

	// Token: 0x17000B13 RID: 2835
	// (get) Token: 0x06003853 RID: 14419 RVA: 0x000CE8E4 File Offset: 0x000CCAE4
	protected static ulong time
	{
		get
		{
			return NetCull.timeInMillis;
		}
	}

	// Token: 0x17000B14 RID: 2836
	// (get) Token: 0x06003854 RID: 14420 RVA: 0x000CE8EC File Offset: 0x000CCAEC
	protected double elapsed
	{
		get
		{
			if (this.timeStampChanged != null)
			{
				return (BasicDoor.time - this.timeStampChanged.Value) / 1000.0;
			}
			return double.PositiveInfinity;
		}
	}

	// Token: 0x06003855 RID: 14421 RVA: 0x000CE928 File Offset: 0x000CCB28
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

	// Token: 0x06003856 RID: 14422 RVA: 0x000CE97C File Offset: 0x000CCB7C
	protected void StartOpeningOrClosing(sbyte open, ulong timestamp)
	{
		bool flag = this.openingInReverse;
		BasicDoor.State state;
		long num2;
		if ((int)open != 0)
		{
			if (this.state == BasicDoor.State.Closed)
			{
				flag = (this.canOpenReverse && (int)open == 2);
			}
			state = BasicDoor.State.Opened;
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
			state = BasicDoor.State.Closed;
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

	// Token: 0x06003857 RID: 14423 RVA: 0x000CEB1C File Offset: 0x000CCD1C
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

	// Token: 0x06003858 RID: 14424
	protected abstract void OnDoorFraction(double fractionOpen);

	// Token: 0x06003859 RID: 14425 RVA: 0x000CEB40 File Offset: 0x000CCD40
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
		case BasicDoor.State.Opened:
			if (elapsed >= (double)this.durationOpen)
			{
				base.enabled = false;
				this.state = BasicDoor.State.Opened;
				this.DoDoorFraction(1.0);
				if (flag)
				{
					this.OnDoorEndOpen();
				}
			}
			else
			{
				if (this.state == BasicDoor.State.Closed)
				{
					this.OnDoorStartOpen();
				}
				this.state = BasicDoor.State.Opening;
				this.DoDoorFraction(elapsed / (double)this.durationOpen);
			}
			break;
		case BasicDoor.State.Closed:
			if (elapsed >= (double)this.durationClose)
			{
				base.enabled = false;
				this.state = BasicDoor.State.Closed;
				this.DoDoorFraction(0.0);
				if (flag)
				{
					this.OnDoorEndClose();
				}
			}
			else
			{
				if (this.state == BasicDoor.State.Opened)
				{
					this.OnDoorStartClose();
				}
				this.state = BasicDoor.State.Closing;
				this.DoDoorFraction(1.0 - elapsed / (double)this.durationClose);
			}
			break;
		}
	}

	// Token: 0x0600385A RID: 14426 RVA: 0x000CEC70 File Offset: 0x000CCE70
	protected void LateUpdate()
	{
		if (!this.fixedUpdate)
		{
			this.DoorUpdate();
		}
	}

	// Token: 0x0600385B RID: 14427 RVA: 0x000CEC84 File Offset: 0x000CCE84
	protected void FixedUpdate()
	{
		if (this.fixedUpdate)
		{
			this.DoorUpdate();
		}
	}

	// Token: 0x0600385C RID: 14428 RVA: 0x000CEC98 File Offset: 0x000CCE98
	protected string ContextText(Controllable localControllable)
	{
		switch (this.state)
		{
		case BasicDoor.State.Opened:
			return this.textClose;
		case BasicDoor.State.Closed:
			return this.textOpen;
		}
		return null;
	}

	// Token: 0x0600385D RID: 14429 RVA: 0x000CECD4 File Offset: 0x000CCED4
	protected bool ContextTextPoint(out Vector3 worldPoint)
	{
		if (this.pointText)
		{
			switch (this.state)
			{
			case BasicDoor.State.Opened:
				worldPoint = base.transform.TransformPoint(this.pointTextPointOpened);
				return true;
			case BasicDoor.State.Closed:
				worldPoint = base.transform.TransformPoint(this.pointTextPointClosed);
				return true;
			}
		}
		worldPoint = default(Vector3);
		return false;
	}

	// Token: 0x0600385E RID: 14430 RVA: 0x000CED54 File Offset: 0x000CCF54
	protected ContextStatusFlags ContextStatusPoll()
	{
		switch (this.state)
		{
		case BasicDoor.State.Opened:
		case BasicDoor.State.Closed:
			return (ContextStatusFlags)0;
		default:
			return ContextStatusFlags.ObjectBusy | ContextStatusFlags.SpriteFlag0;
		}
	}

	// Token: 0x0600385F RID: 14431 RVA: 0x000CED88 File Offset: 0x000CCF88
	private void PlaySound(AudioClip clip)
	{
		if (clip)
		{
			clip.Play(base.transform.position, 1f, 5f, 20f);
		}
	}

	// Token: 0x06003860 RID: 14432 RVA: 0x000CEDC0 File Offset: 0x000CCFC0
	protected void OnDoorStartOpen()
	{
		this.PlaySound(this.openSound);
	}

	// Token: 0x06003861 RID: 14433 RVA: 0x000CEDD0 File Offset: 0x000CCFD0
	protected void OnDoorEndOpen()
	{
		this.PlaySound(this.openedSound);
		this.DisableObstacle();
	}

	// Token: 0x06003862 RID: 14434 RVA: 0x000CEDE4 File Offset: 0x000CCFE4
	protected virtual void OnDoorStartClose()
	{
		this.PlaySound(this.closeSound);
	}

	// Token: 0x06003863 RID: 14435 RVA: 0x000CEDF4 File Offset: 0x000CCFF4
	protected virtual void OnDoorEndClose()
	{
		this.PlaySound(this.closedSound);
		this.EnableObstacle();
	}

	// Token: 0x17000B15 RID: 2837
	// (get) Token: 0x06003864 RID: 14436 RVA: 0x000CEE08 File Offset: 0x000CD008
	private bool on
	{
		get
		{
			return this.target == BasicDoor.State.Opened || this.target == BasicDoor.State.Opening;
		}
	}

	// Token: 0x06003865 RID: 14437 RVA: 0x000CEE24 File Offset: 0x000CD024
	private BasicDoor.Side CalculateOpenWay()
	{
		return (!this.openingInReverse && this.canOpenReverse) ? BasicDoor.Side.Reverse : BasicDoor.Side.Forward;
	}

	// Token: 0x06003866 RID: 14438 RVA: 0x000CEE44 File Offset: 0x000CD044
	private BasicDoor.Side CalculateOpenWay(Vector3 worldPoint)
	{
		BasicDoor.IdealSide idealSide;
		if (!this.canOpenReverse || (int)(idealSide = this.IdealSideForPoint(worldPoint)) == 1)
		{
			return BasicDoor.Side.Forward;
		}
		if ((int)idealSide == 0)
		{
			return (!this.openingInReverse) ? BasicDoor.Side.Reverse : BasicDoor.Side.Forward;
		}
		return BasicDoor.Side.Reverse;
	}

	// Token: 0x06003867 RID: 14439 RVA: 0x000CEE8C File Offset: 0x000CD08C
	private BasicDoor.Side CalculateOpenWay(Vector3? worldPoint)
	{
		return (worldPoint == null) ? this.CalculateOpenWay() : this.CalculateOpenWay(worldPoint.Value);
	}

	// Token: 0x06003868 RID: 14440
	protected abstract BasicDoor.IdealSide IdealSideForPoint(Vector3 worldPoint);

	// Token: 0x06003869 RID: 14441 RVA: 0x000CEEC0 File Offset: 0x000CD0C0
	private bool ToggleStateServer(Vector3? openerPoint, ulong timestamp, bool? fallbackReverse = null)
	{
		if (this.serverLastTimeStamp == null || timestamp > this.serverLastTimeStamp.Value)
		{
			if (this.waitsTarget && (this.state == BasicDoor.State.Opening || this.state == BasicDoor.State.Closing))
			{
				return false;
			}
			this.serverLastTimeStamp = new ulong?(timestamp);
			BasicDoor.State state = this.target;
			bool flag = this.openingInReverse;
			if (this.target == BasicDoor.State.Closed)
			{
				if (openerPoint != null || fallbackReverse == null)
				{
					if (this.CalculateOpenWay(openerPoint) == BasicDoor.Side.Forward)
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

	// Token: 0x0600386A RID: 14442 RVA: 0x000CEFD0 File Offset: 0x000CD1D0
	private bool ToggleStateServer(ulong timestamp, Character instigator)
	{
		if (instigator)
		{
			return this.ToggleStateServer(new Vector3?(instigator.eyesOrigin), timestamp, null);
		}
		return this.ToggleStateServer(null, timestamp, null);
	}

	// Token: 0x0600386B RID: 14443 RVA: 0x000CF020 File Offset: 0x000CD220
	private void InitializeObstacle()
	{
		NavMeshObstacle component = base.GetComponent<NavMeshObstacle>();
		if (component)
		{
			Object.Destroy(component);
		}
	}

	// Token: 0x0600386C RID: 14444 RVA: 0x000CF048 File Offset: 0x000CD248
	protected void EnableObstacle()
	{
	}

	// Token: 0x0600386D RID: 14445 RVA: 0x000CF04C File Offset: 0x000CD24C
	protected void DisableObstacle()
	{
	}

	// Token: 0x0600386E RID: 14446 RVA: 0x000CF050 File Offset: 0x000CD250
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

	// Token: 0x0600386F RID: 14447 RVA: 0x000CF080 File Offset: 0x000CD280
	[RPC]
	protected void DOc(sbyte open)
	{
		this.CaptureOriginals();
		long num;
		if ((int)open != 0)
		{
			this.state = (this.target = BasicDoor.State.Opened);
			num = (long)((double)this.durationOpen * 1000.0);
			this.openingInReverse = ((int)open == 2);
			this.DoDoorFraction(1.0);
		}
		else
		{
			this.state = (this.target = BasicDoor.State.Closed);
			num = (long)((double)this.durationOpen * 1000.0);
			this.DoDoorFraction(0.0);
		}
		ulong time = BasicDoor.time;
		if (num > (long)time)
		{
			this.timeStampChanged = new ulong?(time - (ulong)num);
		}
		else
		{
			this.timeStampChanged = null;
		}
	}

	// Token: 0x06003870 RID: 14448 RVA: 0x000CF140 File Offset: 0x000CD340
	protected void Awake()
	{
		this.CaptureOriginals();
		this.openingInReverse = this.defaultReversed;
		this.InitializeObstacle();
		if (this.startsOpened)
		{
			this.target = (this.state = BasicDoor.State.Opened);
			this.DoDoorFraction(1.0);
		}
		else
		{
			this.target = (this.state = BasicDoor.State.Closed);
			this.DoDoorFraction(0.0);
		}
		base.enabled = false;
	}

	// Token: 0x06003871 RID: 14449 RVA: 0x000CF1BC File Offset: 0x000CD3BC
	protected void OnDestroy()
	{
	}

	// Token: 0x06003872 RID: 14450 RVA: 0x000CF1C0 File Offset: 0x000CD3C0
	protected void PlayerConnected(PlayerClient player)
	{
	}

	// Token: 0x04001C4D RID: 7245
	private const BasicDoor.RunFlags kRF_StartOpen_Mask = BasicDoor.RunFlags.OpenedForward;

	// Token: 0x04001C4E RID: 7246
	private const BasicDoor.RunFlags kRF_StartOpen_Value = BasicDoor.RunFlags.OpenedForward;

	// Token: 0x04001C4F RID: 7247
	private const BasicDoor.RunFlags kRF_DefaultReverse_Mask = (BasicDoor.RunFlags)18;

	// Token: 0x04001C50 RID: 7248
	private const BasicDoor.RunFlags kRF_DefaultReverse_Value = BasicDoor.RunFlags.ClosedReverse;

	// Token: 0x04001C51 RID: 7249
	private const BasicDoor.RunFlags kRF_DisableReverse_Mask = BasicDoor.RunFlags.ClosedNoReverse;

	// Token: 0x04001C52 RID: 7250
	private const BasicDoor.RunFlags kRF_DisableReverse_Value = BasicDoor.RunFlags.ClosedNoReverse;

	// Token: 0x04001C53 RID: 7251
	private const BasicDoor.RunFlags kRF_FixedUpdate_Mask = BasicDoor.RunFlags.FixedUpdateClosedForward;

	// Token: 0x04001C54 RID: 7252
	private const BasicDoor.RunFlags kRF_FixedUpdate_Value = BasicDoor.RunFlags.FixedUpdateClosedForward;

	// Token: 0x04001C55 RID: 7253
	private const BasicDoor.RunFlags kRF_PointText_Mask = BasicDoor.RunFlags.ClosedForwardWithPointText;

	// Token: 0x04001C56 RID: 7254
	private const BasicDoor.RunFlags kRF_PointText_Value = BasicDoor.RunFlags.ClosedForwardWithPointText;

	// Token: 0x04001C57 RID: 7255
	private const BasicDoor.RunFlags kRF_WaitsTarget_Mask = BasicDoor.RunFlags.ClosedForwardWaits;

	// Token: 0x04001C58 RID: 7256
	private const BasicDoor.RunFlags kRF_WaitsTarget_Value = BasicDoor.RunFlags.ClosedForwardWaits;

	// Token: 0x04001C59 RID: 7257
	private const float kVolume = 1f;

	// Token: 0x04001C5A RID: 7258
	private const float kMinDistance = 5f;

	// Token: 0x04001C5B RID: 7259
	private const float kMaxDistance = 20f;

	// Token: 0x04001C5C RID: 7260
	private const sbyte kOpenForward = 1;

	// Token: 0x04001C5D RID: 7261
	private const sbyte kOpenBackward = 2;

	// Token: 0x04001C5E RID: 7262
	private const sbyte kClose = 0;

	// Token: 0x04001C5F RID: 7263
	private const string kRPCName_SetOpenOrClosed = "DOo";

	// Token: 0x04001C60 RID: 7264
	private const string kRPCName_ConnectSetup = "DOc";

	// Token: 0x04001C61 RID: 7265
	[SerializeField]
	private BasicDoor.RunFlags startConfig;

	// Token: 0x04001C62 RID: 7266
	[NonSerialized]
	protected Vector3 originalLocalPosition;

	// Token: 0x04001C63 RID: 7267
	[NonSerialized]
	protected Quaternion originalLocalRotation;

	// Token: 0x04001C64 RID: 7268
	[NonSerialized]
	protected Vector3 originalLocalScale;

	// Token: 0x04001C65 RID: 7269
	[NonSerialized]
	private ulong? timeStampChanged;

	// Token: 0x04001C66 RID: 7270
	[SerializeField]
	protected float durationClose = 1f;

	// Token: 0x04001C67 RID: 7271
	[SerializeField]
	protected float durationOpen = 1f;

	// Token: 0x04001C68 RID: 7272
	[NonSerialized]
	private bool capturedOriginals;

	// Token: 0x04001C69 RID: 7273
	[SerializeField]
	protected string textOpen = "Open";

	// Token: 0x04001C6A RID: 7274
	[SerializeField]
	protected string textClose = "Close";

	// Token: 0x04001C6B RID: 7275
	[SerializeField]
	protected Vector3 pointTextPointOpened;

	// Token: 0x04001C6C RID: 7276
	[SerializeField]
	protected Vector3 pointTextPointClosed;

	// Token: 0x04001C6D RID: 7277
	[SerializeField]
	protected AudioClip openSound;

	// Token: 0x04001C6E RID: 7278
	[SerializeField]
	protected AudioClip openedSound;

	// Token: 0x04001C6F RID: 7279
	[SerializeField]
	protected AudioClip closeSound;

	// Token: 0x04001C70 RID: 7280
	[SerializeField]
	protected AudioClip closedSound;

	// Token: 0x04001C71 RID: 7281
	[SerializeField]
	protected float minimumTimeBetweenOpenClose = 1f;

	// Token: 0x04001C72 RID: 7282
	[NonSerialized]
	private ulong? serverLastTimeStamp;

	// Token: 0x04001C73 RID: 7283
	[NonSerialized]
	private BasicDoor.State state;

	// Token: 0x04001C74 RID: 7284
	[NonSerialized]
	private BasicDoor.State target;

	// Token: 0x04001C75 RID: 7285
	[NonSerialized]
	private bool openingInReverse;

	// Token: 0x0200064F RID: 1615
	private enum RunFlags
	{
		// Token: 0x04001C77 RID: 7287
		ClosedForward,
		// Token: 0x04001C78 RID: 7288
		OpenedForward,
		// Token: 0x04001C79 RID: 7289
		ClosedReverse,
		// Token: 0x04001C7A RID: 7290
		OpenedReverse,
		// Token: 0x04001C7B RID: 7291
		ClosedForwardWithPointText,
		// Token: 0x04001C7C RID: 7292
		OpenedForwardWithPointText,
		// Token: 0x04001C7D RID: 7293
		ClosedReverseWithPointText,
		// Token: 0x04001C7E RID: 7294
		OpenedReverseWithPointText,
		// Token: 0x04001C7F RID: 7295
		FixedUpdateClosedForward,
		// Token: 0x04001C80 RID: 7296
		FixedUpdateOpenedForward,
		// Token: 0x04001C81 RID: 7297
		FixedUpdateClosedReverse,
		// Token: 0x04001C82 RID: 7298
		FixedUpdateOpenedReverse,
		// Token: 0x04001C83 RID: 7299
		FixedUpdateClosedForwardWithPointText,
		// Token: 0x04001C84 RID: 7300
		FixedUpdateOpenedForwardWithPointText,
		// Token: 0x04001C85 RID: 7301
		FixedUpdateClosedReverseWithPointText,
		// Token: 0x04001C86 RID: 7302
		FixedUpdateOpenedReverseWithPointText,
		// Token: 0x04001C87 RID: 7303
		ClosedNoReverse,
		// Token: 0x04001C88 RID: 7304
		OpenedNoReverse,
		// Token: 0x04001C89 RID: 7305
		ClosedNoReverseWithPointText = 20,
		// Token: 0x04001C8A RID: 7306
		OpenedNoReverseWithPointText,
		// Token: 0x04001C8B RID: 7307
		FixedUpdateClosedNoReverse = 24,
		// Token: 0x04001C8C RID: 7308
		FixedUpdateOpenedNoReverse,
		// Token: 0x04001C8D RID: 7309
		FixedUpdateClosedNoReverseWithPointText = 28,
		// Token: 0x04001C8E RID: 7310
		FixedUpdateOpenedNoReverseWithPointText,
		// Token: 0x04001C8F RID: 7311
		ClosedForwardWaits = 32,
		// Token: 0x04001C90 RID: 7312
		OpenedForwardWaits,
		// Token: 0x04001C91 RID: 7313
		ClosedReverseWaits,
		// Token: 0x04001C92 RID: 7314
		OpenedReverseWaits,
		// Token: 0x04001C93 RID: 7315
		ClosedForwardWaitsWithPointText,
		// Token: 0x04001C94 RID: 7316
		OpenedForwardWaitsWithPointText,
		// Token: 0x04001C95 RID: 7317
		ClosedReverseWaitsWithPointText,
		// Token: 0x04001C96 RID: 7318
		OpenedReverseWaitsWithPointText,
		// Token: 0x04001C97 RID: 7319
		FixedUpdateClosedForwardWaits,
		// Token: 0x04001C98 RID: 7320
		FixedUpdateOpenedForwardWaits,
		// Token: 0x04001C99 RID: 7321
		FixedUpdateClosedReverseWaits,
		// Token: 0x04001C9A RID: 7322
		FixedUpdateOpenedReverseWaits,
		// Token: 0x04001C9B RID: 7323
		FixedUpdateClosedForwardWaitsWithPointText,
		// Token: 0x04001C9C RID: 7324
		FixedUpdateOpenedForwardWaitsWithPointText,
		// Token: 0x04001C9D RID: 7325
		FixedUpdateClosedReverseWaitsWithPointText,
		// Token: 0x04001C9E RID: 7326
		FixedUpdateOpenedReverseWaitsWithPointText,
		// Token: 0x04001C9F RID: 7327
		ClosedNoReverseWaits,
		// Token: 0x04001CA0 RID: 7328
		OpenedNoReverseWaits,
		// Token: 0x04001CA1 RID: 7329
		ClosedNoReverseWithPointTextWaits = 52,
		// Token: 0x04001CA2 RID: 7330
		OpenedNoReverseWithPointTextWaits,
		// Token: 0x04001CA3 RID: 7331
		FixedUpdateClosedNoReverseWaits = 56,
		// Token: 0x04001CA4 RID: 7332
		FixedUpdateOpenedNoReverseWaits,
		// Token: 0x04001CA5 RID: 7333
		FixedUpdateClosedNoReverseWaitsWithPointText = 60,
		// Token: 0x04001CA6 RID: 7334
		FixedUpdateOpenedNoReverseWaitsWithPointText
	}

	// Token: 0x02000650 RID: 1616
	private enum State : byte
	{
		// Token: 0x04001CA8 RID: 7336
		Opening,
		// Token: 0x04001CA9 RID: 7337
		Opened,
		// Token: 0x04001CAA RID: 7338
		Closing,
		// Token: 0x04001CAB RID: 7339
		Closed
	}

	// Token: 0x02000651 RID: 1617
	private enum Side : byte
	{
		// Token: 0x04001CAD RID: 7341
		Forward,
		// Token: 0x04001CAE RID: 7342
		Reverse
	}

	// Token: 0x02000652 RID: 1618
	protected enum IdealSide : sbyte
	{
		// Token: 0x04001CB0 RID: 7344
		Unknown,
		// Token: 0x04001CB1 RID: 7345
		Reverse = -1,
		// Token: 0x04001CB2 RID: 7346
		Forward = 1
	}
}
