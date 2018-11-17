using System;
using UnityEngine;

// Token: 0x02000100 RID: 256
public class CharacterCCMotorTrait : CharacterTrait
{
	// Token: 0x1700014B RID: 331
	// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001E600 File Offset: 0x0001C800
	public CCTotemPole prefab
	{
		get
		{
			return this._prefab;
		}
	}

	// Token: 0x1700014C RID: 332
	// (get) Token: 0x0600068E RID: 1678 RVA: 0x0001E608 File Offset: 0x0001C808
	public CCMotorSettings settings
	{
		get
		{
			return this._settings;
		}
	}

	// Token: 0x1700014D RID: 333
	// (get) Token: 0x0600068F RID: 1679 RVA: 0x0001E610 File Offset: 0x0001C810
	public bool canControl
	{
		get
		{
			return this._canControl;
		}
	}

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x06000690 RID: 1680 RVA: 0x0001E618 File Offset: 0x0001C818
	public bool sendFallMessage
	{
		get
		{
			return this._sendFallMessage;
		}
	}

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001E620 File Offset: 0x0001C820
	public bool sendLandMessage
	{
		get
		{
			return this._sendLandMessage;
		}
	}

	// Token: 0x17000150 RID: 336
	// (get) Token: 0x06000692 RID: 1682 RVA: 0x0001E628 File Offset: 0x0001C828
	public bool sendJumpMessage
	{
		get
		{
			return this._sendJumpMessage;
		}
	}

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x06000693 RID: 1683 RVA: 0x0001E630 File Offset: 0x0001C830
	public bool sendExternalVelocityMessage
	{
		get
		{
			return this._sendExternalVelocityMessage;
		}
	}

	// Token: 0x17000152 RID: 338
	// (get) Token: 0x06000694 RID: 1684 RVA: 0x0001E638 File Offset: 0x0001C838
	public bool sendJumpFailureMessage
	{
		get
		{
			return this._sendJumpFailureMessage;
		}
	}

	// Token: 0x17000153 RID: 339
	// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001E640 File Offset: 0x0001C840
	public CCMotor.StepMode stepMode
	{
		get
		{
			return this._stepMode;
		}
	}

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x06000696 RID: 1686 RVA: 0x0001E648 File Offset: 0x0001C848
	public float minTimeBetweenJumps
	{
		get
		{
			return this._minTimeBetweenJumps;
		}
	}

	// Token: 0x17000155 RID: 341
	// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001E650 File Offset: 0x0001C850
	public bool enableColliderOnInit
	{
		get
		{
			return this._enableColliderOnInit;
		}
	}

	// Token: 0x040004EA RID: 1258
	[SerializeField]
	private CCMotorSettings _settings;

	// Token: 0x040004EB RID: 1259
	[SerializeField]
	private CCTotemPole _prefab;

	// Token: 0x040004EC RID: 1260
	[SerializeField]
	private bool _canControl = true;

	// Token: 0x040004ED RID: 1261
	[SerializeField]
	private bool _sendFallMessage;

	// Token: 0x040004EE RID: 1262
	[SerializeField]
	private bool _sendLandMessage;

	// Token: 0x040004EF RID: 1263
	[SerializeField]
	private bool _sendJumpMessage;

	// Token: 0x040004F0 RID: 1264
	[SerializeField]
	private bool _sendExternalVelocityMessage;

	// Token: 0x040004F1 RID: 1265
	[SerializeField]
	private bool _sendJumpFailureMessage;

	// Token: 0x040004F2 RID: 1266
	[SerializeField]
	private bool _enableColliderOnInit = true;

	// Token: 0x040004F3 RID: 1267
	[SerializeField]
	private float _minTimeBetweenJumps = 1f;

	// Token: 0x040004F4 RID: 1268
	[SerializeField]
	private CCMotor.StepMode _stepMode = CCMotor.StepMode.Elsewhere;
}
