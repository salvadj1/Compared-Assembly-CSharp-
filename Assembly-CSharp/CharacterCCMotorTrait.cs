using System;
using UnityEngine;

// Token: 0x0200011F RID: 287
public class CharacterCCMotorTrait : global::CharacterTrait
{
	// Token: 0x17000179 RID: 377
	// (get) Token: 0x0600075F RID: 1887 RVA: 0x000211D4 File Offset: 0x0001F3D4
	public global::CCTotemPole prefab
	{
		get
		{
			return this._prefab;
		}
	}

	// Token: 0x1700017A RID: 378
	// (get) Token: 0x06000760 RID: 1888 RVA: 0x000211DC File Offset: 0x0001F3DC
	public global::CCMotorSettings settings
	{
		get
		{
			return this._settings;
		}
	}

	// Token: 0x1700017B RID: 379
	// (get) Token: 0x06000761 RID: 1889 RVA: 0x000211E4 File Offset: 0x0001F3E4
	public bool canControl
	{
		get
		{
			return this._canControl;
		}
	}

	// Token: 0x1700017C RID: 380
	// (get) Token: 0x06000762 RID: 1890 RVA: 0x000211EC File Offset: 0x0001F3EC
	public bool sendFallMessage
	{
		get
		{
			return this._sendFallMessage;
		}
	}

	// Token: 0x1700017D RID: 381
	// (get) Token: 0x06000763 RID: 1891 RVA: 0x000211F4 File Offset: 0x0001F3F4
	public bool sendLandMessage
	{
		get
		{
			return this._sendLandMessage;
		}
	}

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x06000764 RID: 1892 RVA: 0x000211FC File Offset: 0x0001F3FC
	public bool sendJumpMessage
	{
		get
		{
			return this._sendJumpMessage;
		}
	}

	// Token: 0x1700017F RID: 383
	// (get) Token: 0x06000765 RID: 1893 RVA: 0x00021204 File Offset: 0x0001F404
	public bool sendExternalVelocityMessage
	{
		get
		{
			return this._sendExternalVelocityMessage;
		}
	}

	// Token: 0x17000180 RID: 384
	// (get) Token: 0x06000766 RID: 1894 RVA: 0x0002120C File Offset: 0x0001F40C
	public bool sendJumpFailureMessage
	{
		get
		{
			return this._sendJumpFailureMessage;
		}
	}

	// Token: 0x17000181 RID: 385
	// (get) Token: 0x06000767 RID: 1895 RVA: 0x00021214 File Offset: 0x0001F414
	public global::CCMotor.StepMode stepMode
	{
		get
		{
			return this._stepMode;
		}
	}

	// Token: 0x17000182 RID: 386
	// (get) Token: 0x06000768 RID: 1896 RVA: 0x0002121C File Offset: 0x0001F41C
	public float minTimeBetweenJumps
	{
		get
		{
			return this._minTimeBetweenJumps;
		}
	}

	// Token: 0x17000183 RID: 387
	// (get) Token: 0x06000769 RID: 1897 RVA: 0x00021224 File Offset: 0x0001F424
	public bool enableColliderOnInit
	{
		get
		{
			return this._enableColliderOnInit;
		}
	}

	// Token: 0x040005B5 RID: 1461
	[SerializeField]
	private global::CCMotorSettings _settings;

	// Token: 0x040005B6 RID: 1462
	[SerializeField]
	private global::CCTotemPole _prefab;

	// Token: 0x040005B7 RID: 1463
	[SerializeField]
	private bool _canControl = true;

	// Token: 0x040005B8 RID: 1464
	[SerializeField]
	private bool _sendFallMessage;

	// Token: 0x040005B9 RID: 1465
	[SerializeField]
	private bool _sendLandMessage;

	// Token: 0x040005BA RID: 1466
	[SerializeField]
	private bool _sendJumpMessage;

	// Token: 0x040005BB RID: 1467
	[SerializeField]
	private bool _sendExternalVelocityMessage;

	// Token: 0x040005BC RID: 1468
	[SerializeField]
	private bool _sendJumpFailureMessage;

	// Token: 0x040005BD RID: 1469
	[SerializeField]
	private bool _enableColliderOnInit = true;

	// Token: 0x040005BE RID: 1470
	[SerializeField]
	private float _minTimeBetweenJumps = 1f;

	// Token: 0x040005BF RID: 1471
	[SerializeField]
	private global::CCMotor.StepMode _stepMode = global::CCMotor.StepMode.Elsewhere;
}
