using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F7 RID: 247
public class Character : IDMain
{
	// Token: 0x060005A0 RID: 1440 RVA: 0x0001BCC4 File Offset: 0x00019EC4
	public Character() : this(1)
	{
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x0001BCD0 File Offset: 0x00019ED0
	protected Character(IDFlags flags) : base(flags)
	{
	}

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x060005A2 RID: 1442 RVA: 0x0001BD08 File Offset: 0x00019F08
	// (remove) Token: 0x060005A3 RID: 1443 RVA: 0x0001BD0C File Offset: 0x00019F0C
	public event CharacterDeathSignal signal_death
	{
		add
		{
		}
		remove
		{
			if (!this._signaledDeath)
			{
				this.signals_death = (CharacterDeathSignal)Delegate.Remove(this.signals_death, value);
			}
		}
	}

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x060005A4 RID: 1444 RVA: 0x0001BD3C File Offset: 0x00019F3C
	// (remove) Token: 0x060005A5 RID: 1445 RVA: 0x0001BD6C File Offset: 0x00019F6C
	public event CharacterStateSignal signal_state
	{
		add
		{
			if (!this._signaledDeath)
			{
				this.signals_state = (CharacterStateSignal)Delegate.Combine(this.signals_state, value);
			}
		}
		remove
		{
			if (!this._signaledDeath)
			{
				this.signals_state = (CharacterStateSignal)Delegate.Remove(this.signals_state, value);
			}
		}
	}

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001BD9C File Offset: 0x00019F9C
	[Obsolete("this is the character")]
	public Character character
	{
		get
		{
			return this;
		}
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x0001BDA0 File Offset: 0x00019FA0
	protected void Awake()
	{
		if (!this._originSetup)
		{
			this.OriginSetup();
		}
		if (!this._eyesSetup)
		{
			this.EyesSetup();
		}
	}

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001BDD0 File Offset: 0x00019FD0
	public HitBoxSystem hitBoxSystem
	{
		get
		{
			if (!this.didHitBoxSystemTest)
			{
				Character.SeekIDRemoteComponentInChildren<Character, HitBoxSystem>(this, ref this._hitBoxSystem);
				this.didHitBoxSystemTest = true;
			}
			return this._hitBoxSystem;
		}
	}

	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0001BDF8 File Offset: 0x00019FF8
	public RecoilSimulation recoilSimulation
	{
		get
		{
			if (!this.didRecoilSimulationTest)
			{
				Character.SeekIDLocalComponentInChildren<Character, RecoilSimulation>(this, ref this._recoilSimulation);
				this.didRecoilSimulationTest = true;
			}
			return this._recoilSimulation;
		}
	}

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x060005AA RID: 1450 RVA: 0x0001BE20 File Offset: 0x0001A020
	public bool controlled
	{
		get
		{
			return this.controllable && this._controllable.controlled;
		}
	}

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x060005AB RID: 1451 RVA: 0x0001BE40 File Offset: 0x0001A040
	public bool playerControlled
	{
		get
		{
			return this.controllable && this._controllable.playerControlled;
		}
	}

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x060005AC RID: 1452 RVA: 0x0001BE60 File Offset: 0x0001A060
	public bool aiControlled
	{
		get
		{
			return this.controllable && this._controllable.aiControlled;
		}
	}

	// Token: 0x170000ED RID: 237
	// (get) Token: 0x060005AD RID: 1453 RVA: 0x0001BE80 File Offset: 0x0001A080
	public bool localPlayerControlled
	{
		get
		{
			return this.controllable && this._controllable.localPlayerControlled;
		}
	}

	// Token: 0x170000EE RID: 238
	// (get) Token: 0x060005AE RID: 1454 RVA: 0x0001BEA0 File Offset: 0x0001A0A0
	public bool remotePlayerControlled
	{
		get
		{
			return this.controllable && this._controllable.remotePlayerControlled;
		}
	}

	// Token: 0x170000EF RID: 239
	// (get) Token: 0x060005AF RID: 1455 RVA: 0x0001BEC0 File Offset: 0x0001A0C0
	public bool localAIControlled
	{
		get
		{
			return this.controllable && this._controllable.localAIControlled;
		}
	}

	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001BEE0 File Offset: 0x0001A0E0
	public bool remoteAIControlled
	{
		get
		{
			return this.controllable && this._controllable.remoteAIControlled;
		}
	}

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001BF00 File Offset: 0x0001A100
	public bool localControlled
	{
		get
		{
			return this.controllable && this._controllable.localControlled;
		}
	}

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001BF20 File Offset: 0x0001A120
	public bool remoteControlled
	{
		get
		{
			return this.controllable && this._controllable.remoteControlled;
		}
	}

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001BF40 File Offset: 0x0001A140
	public bool core
	{
		get
		{
			return this.controllable && this._controllable.core;
		}
	}

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001BF60 File Offset: 0x0001A160
	public bool vessel
	{
		get
		{
			return this.controllable && this._controllable.vessel;
		}
	}

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0001BF80 File Offset: 0x0001A180
	public Controllable controllable
	{
		get
		{
			if (!this.didControllableTest)
			{
				Character.SeekComponentInChildren<Character, Controllable>(this, ref this._controllable);
				this.didControllableTest = true;
			}
			return this._controllable;
		}
	}

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001BFA8 File Offset: 0x0001A1A8
	public Controllable controlledControllable
	{
		get
		{
			return (!this.controllable || !this._controllable.controlled) ? null : this._controllable;
		}
	}

	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0001BFE4 File Offset: 0x0001A1E4
	public Controllable playerControlledControllable
	{
		get
		{
			return (!this.controllable || !this._controllable.playerControlled) ? null : this._controllable;
		}
	}

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001C020 File Offset: 0x0001A220
	public Controllable aiControlledControllable
	{
		get
		{
			return (!this.controllable || !this._controllable.aiControlled) ? null : this._controllable;
		}
	}

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0001C05C File Offset: 0x0001A25C
	public Controllable localPlayerControlledControllable
	{
		get
		{
			return (!this.controllable || !this._controllable.localPlayerControlled) ? null : this._controllable;
		}
	}

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001C098 File Offset: 0x0001A298
	public Controllable localAIControlledControllable
	{
		get
		{
			return (!this.controllable || !this._controllable.localAIControlled) ? null : this._controllable;
		}
	}

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x060005BB RID: 1467 RVA: 0x0001C0D4 File Offset: 0x0001A2D4
	public Controllable remotePlayerControlledControllable
	{
		get
		{
			return (!this.controllable || !this._controllable.remotePlayerControlled) ? null : this._controllable;
		}
	}

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x060005BC RID: 1468 RVA: 0x0001C110 File Offset: 0x0001A310
	public Controllable remoteAIControlledControllable
	{
		get
		{
			return (!this.controllable || !this._controllable.remoteAIControlled) ? null : this._controllable;
		}
	}

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x060005BD RID: 1469 RVA: 0x0001C14C File Offset: 0x0001A34C
	public PlayerClient playerClient
	{
		get
		{
			return (!this._controllable) ? null : this._controllable.playerClient;
		}
	}

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x060005BE RID: 1470 RVA: 0x0001C170 File Offset: 0x0001A370
	public string npcName
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.npcName;
		}
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x060005BF RID: 1471 RVA: 0x0001C194 File Offset: 0x0001A394
	public bool controlOverridden
	{
		get
		{
			return this.controllable && this._controllable.controlOverridden;
		}
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x0001C1B4 File Offset: 0x0001A3B4
	public bool ControlOverriddenBy(Controllable controllable)
	{
		return this.controllable && this._controllable.ControlOverriddenBy(controllable);
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x0001C1D8 File Offset: 0x0001A3D8
	public bool ControlOverriddenBy(Controller controller)
	{
		return this.controllable && this._controllable.ControlOverriddenBy(controller);
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x0001C1FC File Offset: 0x0001A3FC
	public bool ControlOverriddenBy(Character character)
	{
		return this.controllable && this._controllable.ControlOverriddenBy(character);
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x0001C220 File Offset: 0x0001A420
	public bool ControlOverriddenBy(IDMain main)
	{
		return this.controllable && this._controllable.ControlOverriddenBy(main);
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x0001C244 File Offset: 0x0001A444
	public bool ControlOverriddenBy(IDBase idBase)
	{
		return this.controllable && this._controllable.ControlOverriddenBy(idBase);
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x0001C268 File Offset: 0x0001A468
	public bool ControlOverriddenBy(IDLocalCharacter idLocal)
	{
		return this.controllable && this._controllable.ControlOverriddenBy(idLocal);
	}

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0001C28C File Offset: 0x0001A48C
	public bool overridingControl
	{
		get
		{
			return this.controllable && this._controllable.overridingControl;
		}
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x0001C2AC File Offset: 0x0001A4AC
	public bool OverridingControlOf(Controllable controllable)
	{
		return this.controllable && this._controllable.OverridingControlOf(controllable);
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x0001C2D0 File Offset: 0x0001A4D0
	public bool OverridingControlOf(Controller controller)
	{
		return this.controllable && this._controllable.OverridingControlOf(controller);
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x0001C2F4 File Offset: 0x0001A4F4
	public bool OverridingControlOf(Character character)
	{
		return this.controllable && this._controllable.OverridingControlOf(character);
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x0001C318 File Offset: 0x0001A518
	public bool OverridingControlOf(IDMain main)
	{
		return this.controllable && this._controllable.OverridingControlOf(main);
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x0001C33C File Offset: 0x0001A53C
	public bool OverridingControlOf(IDBase idBase)
	{
		return this.controllable && this._controllable.OverridingControlOf(idBase);
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x0001C360 File Offset: 0x0001A560
	public bool OverridingControlOf(IDLocalCharacter idLocal)
	{
		return this.controllable && this._controllable.OverridingControlOf(idLocal);
	}

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x060005CD RID: 1485 RVA: 0x0001C384 File Offset: 0x0001A584
	public bool assignedControl
	{
		get
		{
			return this.controllable && this._controllable.assignedControl;
		}
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x0001C3A4 File Offset: 0x0001A5A4
	public bool AssignedControlOf(Controllable controllable)
	{
		return this.controllable && this._controllable.AssignedControlOf(controllable);
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x0001C3C8 File Offset: 0x0001A5C8
	public bool AssignedControlOf(Controller controller)
	{
		return this.controllable && this._controllable.AssignedControlOf(controller);
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x0001C3EC File Offset: 0x0001A5EC
	public bool AssignedControlOf(IDMain character)
	{
		return this.controllable && this._controllable.AssignedControlOf(character);
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x0001C410 File Offset: 0x0001A610
	public bool AssignedControlOf(IDBase idBase)
	{
		return this.controllable && this._controllable.AssignedControlOf(idBase);
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x0001C434 File Offset: 0x0001A634
	public RelativeControl RelativeControlTo(Controllable controllable)
	{
		return (!this.controllable) ? RelativeControl.Incompatible : this._controllable.RelativeControlTo(controllable);
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x0001C464 File Offset: 0x0001A664
	public RelativeControl RelativeControlTo(Controller controller)
	{
		return (!this.controllable) ? RelativeControl.Incompatible : this._controllable.RelativeControlTo(controller);
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x0001C494 File Offset: 0x0001A694
	public RelativeControl RelativeControlTo(Character character)
	{
		return (!this.controllable) ? RelativeControl.Incompatible : this._controllable.RelativeControlTo(character);
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x0001C4C4 File Offset: 0x0001A6C4
	public RelativeControl RelativeControlTo(IDMain main)
	{
		return (!this.controllable) ? RelativeControl.Incompatible : this._controllable.RelativeControlTo(main);
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x0001C4F4 File Offset: 0x0001A6F4
	public RelativeControl RelativeControlTo(IDLocalCharacter idLocal)
	{
		return (!this.controllable) ? RelativeControl.Incompatible : this._controllable.RelativeControlTo(idLocal);
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x0001C524 File Offset: 0x0001A724
	public RelativeControl RelativeControlTo(IDBase idBase)
	{
		return (!this.controllable) ? RelativeControl.Incompatible : this._controllable.RelativeControlTo(idBase);
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x0001C554 File Offset: 0x0001A754
	public RelativeControl RelativeControlFrom(Controllable controllable)
	{
		return (!this.controllable) ? RelativeControl.Incompatible : this._controllable.RelativeControlFrom(controllable);
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x0001C584 File Offset: 0x0001A784
	public RelativeControl RelativeControlFrom(Controller controller)
	{
		return (!this.controllable) ? RelativeControl.Incompatible : this._controllable.RelativeControlFrom(controller);
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x0001C5B4 File Offset: 0x0001A7B4
	public RelativeControl RelativeControlFrom(Character character)
	{
		return (!this.controllable) ? RelativeControl.Incompatible : this._controllable.RelativeControlFrom(character);
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x0001C5E4 File Offset: 0x0001A7E4
	public RelativeControl RelativeControlFrom(IDMain main)
	{
		return (!this.controllable) ? RelativeControl.Incompatible : this._controllable.RelativeControlFrom(main);
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x0001C614 File Offset: 0x0001A814
	public RelativeControl RelativeControlFrom(IDLocalCharacter idLocal)
	{
		return (!this.controllable) ? RelativeControl.Incompatible : this._controllable.RelativeControlFrom(idLocal);
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x0001C644 File Offset: 0x0001A844
	public RelativeControl RelativeControlFrom(IDBase idBase)
	{
		return (!this.controllable) ? RelativeControl.Incompatible : this._controllable.RelativeControlFrom(idBase);
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x060005DE RID: 1502 RVA: 0x0001C674 File Offset: 0x0001A874
	public Controllable masterControllable
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.masterControllable;
		}
	}

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x060005DF RID: 1503 RVA: 0x0001C698 File Offset: 0x0001A898
	public Controllable rootControllable
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.rootControllable;
		}
	}

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0001C6BC File Offset: 0x0001A8BC
	public Controllable nextControllable
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.nextControllable;
		}
	}

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0001C6E0 File Offset: 0x0001A8E0
	public Controllable previousControllable
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.previousControllable;
		}
	}

	// Token: 0x17000106 RID: 262
	// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0001C704 File Offset: 0x0001A904
	public Character masterCharacter
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.masterCharacter;
		}
	}

	// Token: 0x17000107 RID: 263
	// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0001C728 File Offset: 0x0001A928
	public Character rootCharacter
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.rootCharacter;
		}
	}

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0001C74C File Offset: 0x0001A94C
	public Character nextCharacter
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.nextCharacter;
		}
	}

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0001C770 File Offset: 0x0001A970
	public Character previousCharacter
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.previousCharacter;
		}
	}

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0001C794 File Offset: 0x0001A994
	public int controlDepth
	{
		get
		{
			return (!this.controllable) ? -1 : this._controllable.controlDepth;
		}
	}

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0001C7B8 File Offset: 0x0001A9B8
	public int controlCount
	{
		get
		{
			return (!this.controllable) ? 0 : this._controllable.controlCount;
		}
	}

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0001C7DC File Offset: 0x0001A9DC
	public string controllerClassName
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.controllerClassName;
		}
	}

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0001C800 File Offset: 0x0001AA00
	public Controller controller
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.controller;
		}
	}

	// Token: 0x1700010E RID: 270
	// (get) Token: 0x060005EA RID: 1514 RVA: 0x0001C824 File Offset: 0x0001AA24
	public Controller controlledController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.controlledController;
		}
	}

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x060005EB RID: 1515 RVA: 0x0001C848 File Offset: 0x0001AA48
	public Controller playerControlledController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.playerControlledController;
		}
	}

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x060005EC RID: 1516 RVA: 0x0001C86C File Offset: 0x0001AA6C
	public Controller aiControlledController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.aiControlledController;
		}
	}

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x060005ED RID: 1517 RVA: 0x0001C890 File Offset: 0x0001AA90
	public Controller localPlayerControlledController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.localPlayerControlledController;
		}
	}

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x060005EE RID: 1518 RVA: 0x0001C8B4 File Offset: 0x0001AAB4
	public Controller localAIControlledController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.localAIControlledController;
		}
	}

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x060005EF RID: 1519 RVA: 0x0001C8D8 File Offset: 0x0001AAD8
	public Controller remotePlayerControlledController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.remotePlayerControlledController;
		}
	}

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0001C8FC File Offset: 0x0001AAFC
	public Controller remoteAIControlledController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.remoteAIControlledController;
		}
	}

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0001C920 File Offset: 0x0001AB20
	public Controller masterController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.masterController;
		}
	}

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0001C944 File Offset: 0x0001AB44
	public Controller rootController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.rootController;
		}
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001C968 File Offset: 0x0001AB68
	public Controller nextController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.nextController;
		}
	}

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0001C98C File Offset: 0x0001AB8C
	public Controller previousController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.previousController;
		}
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0001C9B0 File Offset: 0x0001ABB0
	public TakeDamage takeDamage
	{
		get
		{
			if (!this.didTakeDamageTest)
			{
				Character.SeekIDLocalComponentInChildren<Character, TakeDamage>(this, ref this._takeDamage);
				this.didTakeDamageTest = true;
			}
			return this._takeDamage;
		}
	}

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0001C9D8 File Offset: 0x0001ABD8
	public float health
	{
		get
		{
			return (!this.takeDamage) ? float.PositiveInfinity : this._takeDamage.health;
		}
	}

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0001CA00 File Offset: 0x0001AC00
	public float healthFraction
	{
		get
		{
			return (!this.takeDamage) ? 1f : this._takeDamage.healthFraction;
		}
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0001CA28 File Offset: 0x0001AC28
	public bool alive
	{
		get
		{
			return !this.takeDamage || this._takeDamage.alive;
		}
	}

	// Token: 0x1700011D RID: 285
	// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0001CA4C File Offset: 0x0001AC4C
	public bool dead
	{
		get
		{
			return this.takeDamage && this._takeDamage.dead;
		}
	}

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x060005FA RID: 1530 RVA: 0x0001CA70 File Offset: 0x0001AC70
	public float healthLoss
	{
		get
		{
			return (!this.takeDamage) ? 0f : this._takeDamage.healthLoss;
		}
	}

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x060005FB RID: 1531 RVA: 0x0001CA98 File Offset: 0x0001AC98
	public float healthLossFraction
	{
		get
		{
			return (!this.takeDamage) ? 0f : this._takeDamage.healthLossFraction;
		}
	}

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x060005FC RID: 1532 RVA: 0x0001CAC0 File Offset: 0x0001ACC0
	public float maxHealth
	{
		get
		{
			return (!this.takeDamage) ? float.PositiveInfinity : this._takeDamage.maxHealth;
		}
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x0001CAE8 File Offset: 0x0001ACE8
	public void AdjustClientSideHealth(float newHealthValue)
	{
		if (this.takeDamage)
		{
			this._takeDamage.health = newHealthValue;
		}
	}

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001CB08 File Offset: 0x0001AD08
	public float maxPitch
	{
		get
		{
			return this._maxPitch;
		}
	}

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x060005FF RID: 1535 RVA: 0x0001CB10 File Offset: 0x0001AD10
	public float minPitch
	{
		get
		{
			return this._minPitch;
		}
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x0001CB18 File Offset: 0x0001AD18
	public float ClampPitch(float v)
	{
		return (v >= this._minPitch) ? ((v <= this._maxPitch) ? v : this._maxPitch) : this._minPitch;
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x0001CB4C File Offset: 0x0001AD4C
	public void ClampPitch(ref float v)
	{
		if (v < this._minPitch)
		{
			v = this._minPitch;
		}
		else if (v > this._maxPitch)
		{
			v = this._maxPitch;
		}
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x0001CB80 File Offset: 0x0001AD80
	public Angle2 ClampPitch(Angle2 v)
	{
		this.ClampPitch(ref v.pitch);
		return v;
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x0001CB90 File Offset: 0x0001AD90
	public void ClampPitch(ref Angle2 v)
	{
		this.ClampPitch(ref v.pitch);
	}

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x06000604 RID: 1540 RVA: 0x0001CBA0 File Offset: 0x0001ADA0
	public Crouchable crouchable
	{
		get
		{
			if (!this.didCrouchableTest)
			{
				Character.SeekIDLocalComponentInChildren<Character, Crouchable>(this, ref this._crouchable);
				this.didCrouchableTest = true;
			}
			return this._crouchable;
		}
	}

	// Token: 0x17000124 RID: 292
	// (get) Token: 0x06000605 RID: 1541 RVA: 0x0001CBC8 File Offset: 0x0001ADC8
	public bool crouched
	{
		get
		{
			return this.crouchable && this.crouchable.crouched;
		}
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x0001CBF4 File Offset: 0x0001ADF4
	protected static bool SeekIDRemoteComponentInChildren<M, T>(M main, ref T component) where M : IDMain where T : IDRemote
	{
		if (component)
		{
			if (component.idMain == main)
			{
				return true;
			}
			if (!component.idMain)
			{
				return true;
			}
		}
		component = main.GetComponentInChildren<T>();
		if (component)
		{
			if (component.idMain == main)
			{
				return true;
			}
			if (!component.idMain)
			{
				return true;
			}
			T[] componentsInChildren = main.GetComponentsInChildren<T>();
			if (componentsInChildren.Length <= 1)
			{
				component = (T)((object)null);
				return false;
			}
			foreach (T t in componentsInChildren)
			{
				if (t.idMain == main)
				{
					component = t;
					return true;
				}
			}
			component = (T)((object)null);
		}
		return false;
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x0001CD20 File Offset: 0x0001AF20
	protected static bool SeekIDLocalComponentInChildren<M, T>(M main, ref T component) where M : IDMain where T : IDLocal
	{
		if (component)
		{
			if (component.idMain == main)
			{
				return true;
			}
			if (!component.idMain)
			{
				return true;
			}
		}
		component = main.GetComponent<T>();
		if (component)
		{
			if (component.idMain == main)
			{
				return true;
			}
			if (!component.idMain)
			{
				return true;
			}
			T[] components = main.GetComponents<T>();
			if (components.Length <= 1)
			{
				component = (T)((object)null);
				return false;
			}
			foreach (T t in components)
			{
				if (t.idMain == main)
				{
					component = t;
					return true;
				}
			}
			component = (T)((object)null);
		}
		return false;
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x0001CE5C File Offset: 0x0001B05C
	protected static bool SeekComponentInChildren<M, T>(M main, ref T component) where M : IDMain where T : Component
	{
		if (!component)
		{
			component = main.GetComponent<T>();
			return component;
		}
		return true;
	}

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x06000609 RID: 1545 RVA: 0x0001CEA4 File Offset: 0x0001B0A4
	public Vector3 initialEyesOffset
	{
		get
		{
			return this._initialEyesOffset;
		}
	}

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x0600060A RID: 1546 RVA: 0x0001CEAC File Offset: 0x0001B0AC
	public float initialEyesOffsetX
	{
		get
		{
			return this._initialEyesOffset.x;
		}
	}

	// Token: 0x17000127 RID: 295
	// (get) Token: 0x0600060B RID: 1547 RVA: 0x0001CEBC File Offset: 0x0001B0BC
	public float initialEyesOffsetY
	{
		get
		{
			return this._initialEyesOffset.y;
		}
	}

	// Token: 0x17000128 RID: 296
	// (get) Token: 0x0600060C RID: 1548 RVA: 0x0001CECC File Offset: 0x0001B0CC
	public float initialEyesOffsetZ
	{
		get
		{
			return this._initialEyesOffset.z;
		}
	}

	// Token: 0x17000129 RID: 297
	// (get) Token: 0x0600060D RID: 1549 RVA: 0x0001CEDC File Offset: 0x0001B0DC
	// (set) Token: 0x0600060E RID: 1550 RVA: 0x0001CEFC File Offset: 0x0001B0FC
	public float eyesPitch
	{
		get
		{
			if (!this._eyesSetup)
			{
				this.EyesSetup();
			}
			return this._eyesAngles.pitch;
		}
		set
		{
			if (this.lockLook)
			{
				return;
			}
			if (!this._eyesSetup)
			{
				this.EyesSetup();
			}
			if (this._eyesAngles.pitch != value)
			{
				this._eyesAngles.pitch = value;
				this.InvalidateEyesAngles();
			}
		}
	}

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x0600060F RID: 1551 RVA: 0x0001CF4C File Offset: 0x0001B14C
	// (set) Token: 0x06000610 RID: 1552 RVA: 0x0001CF6C File Offset: 0x0001B16C
	public float eyesYaw
	{
		get
		{
			if (!this._eyesSetup)
			{
				this.EyesSetup();
			}
			return this._eyesAngles.yaw;
		}
		set
		{
			if (this.lockLook)
			{
				return;
			}
			if (!this._eyesSetup)
			{
				this.EyesSetup();
			}
			if (this._eyesAngles.yaw != value)
			{
				this._eyesAngles.yaw = value;
				this.InvalidateEyesAngles();
			}
		}
	}

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001CFBC File Offset: 0x0001B1BC
	// (set) Token: 0x06000612 RID: 1554 RVA: 0x0001CFD8 File Offset: 0x0001B1D8
	public Angle2 eyesAngles
	{
		get
		{
			if (!this._eyesSetup)
			{
				this.EyesSetup();
			}
			return this._eyesAngles;
		}
		set
		{
			if (this.lockLook)
			{
				return;
			}
			if (!this._eyesSetup)
			{
				this.EyesSetup();
			}
			if (this._eyesAngles.x != value.x || this._eyesAngles.y != value.y)
			{
				this._eyesAngles = value;
				this.InvalidateEyesAngles();
			}
		}
	}

	// Token: 0x1700012C RID: 300
	// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001D040 File Offset: 0x0001B240
	public Vector3 eyesOrigin
	{
		get
		{
			return this._eyesTransform.position;
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x06000614 RID: 1556 RVA: 0x0001D050 File Offset: 0x0001B250
	public Vector3 eyesOriginAtInitialOffset
	{
		get
		{
			if (!this._eyesSetup)
			{
				this.EyesSetup();
			}
			return base.transform.TransformPoint(this._initialEyesOffset);
		}
	}

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x06000615 RID: 1557 RVA: 0x0001D080 File Offset: 0x0001B280
	// (set) Token: 0x06000616 RID: 1558 RVA: 0x0001D09C File Offset: 0x0001B29C
	public Vector3 eyesOffset
	{
		get
		{
			if (!this._eyesSetup)
			{
				this.EyesSetup();
			}
			return this._eyesOffset;
		}
		set
		{
			if (this.lockLook)
			{
				return;
			}
			if (!this._eyesSetup)
			{
				this.EyesSetup();
			}
			if (this._eyesOffset != value)
			{
				this._eyesOffset = value;
				this.InvalidateEyesOffset();
			}
		}
	}

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x06000617 RID: 1559 RVA: 0x0001D0DC File Offset: 0x0001B2DC
	public Ray eyesRay
	{
		get
		{
			if (!this._eyesSetup)
			{
				this.EyesSetup();
			}
			return new Ray(this._eyesTransform.position, this._eyesTransform.forward);
		}
	}

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x06000618 RID: 1560 RVA: 0x0001D118 File Offset: 0x0001B318
	// (set) Token: 0x06000619 RID: 1561 RVA: 0x0001D138 File Offset: 0x0001B338
	public Quaternion eyesRotation
	{
		get
		{
			if (!this._eyesSetup)
			{
				this.EyesSetup();
			}
			return this._eyesAngles.quat;
		}
		set
		{
			this.rotation = value;
			Quaternion quaternion = Quaternion.Euler(0f, this._eyesAngles.yaw, 0f);
			Vector3 vector = value * Quaternion.Inverse(quaternion) * Vector3.forward;
			vector.Normalize();
			if (vector.y < 0f)
			{
				this.eyesPitch = -Vector3.Angle(vector, Vector3.forward);
			}
			else
			{
				this.eyesPitch = Vector3.Angle(vector, Vector3.forward);
			}
		}
	}

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x0600061A RID: 1562 RVA: 0x0001D1C0 File Offset: 0x0001B3C0
	public Transform eyesTransformReadOnly
	{
		get
		{
			if (!this._eyesSetup)
			{
				this.EyesSetup();
			}
			return this._eyesTransform;
		}
	}

	// Token: 0x17000132 RID: 306
	// (get) Token: 0x0600061B RID: 1563 RVA: 0x0001D1DC File Offset: 0x0001B3DC
	// (set) Token: 0x0600061C RID: 1564 RVA: 0x0001D1EC File Offset: 0x0001B3EC
	public Vector3 origin
	{
		get
		{
			return base.transform.localPosition;
		}
		set
		{
			if (this.lockMovement)
			{
				return;
			}
			base.transform.localPosition = value;
		}
	}

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x0600061D RID: 1565 RVA: 0x0001D208 File Offset: 0x0001B408
	public Vector3 forward
	{
		get
		{
			if (!this._eyesSetup)
			{
				this.EyesSetup();
			}
			return Quaternion.Euler(0f, this._eyesAngles.yaw, 0f) * Vector3.forward;
		}
	}

	// Token: 0x17000134 RID: 308
	// (get) Token: 0x0600061E RID: 1566 RVA: 0x0001D240 File Offset: 0x0001B440
	public Vector3 right
	{
		get
		{
			if (!this._eyesSetup)
			{
				this.EyesSetup();
			}
			return Quaternion.Euler(0f, this._eyesAngles.yaw, 0f) * Vector3.right;
		}
	}

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x0600061F RID: 1567 RVA: 0x0001D278 File Offset: 0x0001B478
	public Vector3 up
	{
		get
		{
			return Vector3.up;
		}
	}

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x06000620 RID: 1568 RVA: 0x0001D280 File Offset: 0x0001B480
	// (set) Token: 0x06000621 RID: 1569 RVA: 0x0001D2B0 File Offset: 0x0001B4B0
	public Quaternion rotation
	{
		get
		{
			if (!this._eyesSetup)
			{
				this.EyesSetup();
			}
			return Quaternion.Euler(0f, this._eyesAngles.yaw, 0f);
		}
		set
		{
			Vector3 vector = value * Vector3.forward;
			Vector2 vector2;
			vector2.x = vector.x;
			vector2.y = vector.z;
			if (Mathf.Approximately(vector2.x, 0f) && Mathf.Approximately(vector2.y, 0f))
			{
				vector = value * Vector3.right;
				vector2.x = -vector.z;
				vector2.y = vector.x;
				if (Mathf.Approximately(vector2.x, 0f) && Mathf.Approximately(vector2.y, 0f))
				{
					return;
				}
			}
			this.eyesYaw = Mathf.Atan2(-vector2.x, vector2.y) * -57.29578f;
		}
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x0001D388 File Offset: 0x0001B588
	private void EyesSetup()
	{
		if (!this._originSetup)
		{
			this.OriginSetup();
		}
		if (this._eyesTransform == null || this._eyesTransform.parent != base.transform)
		{
			Debug.LogError("eyes Transform is null or it is not a direct child of our transform.", this);
			return;
		}
		this._initialEyesOffset = (this._eyesOffset = this._eyesTransform.localPosition);
		this._eyesAngles.x = -this._eyesTransform.localEulerAngles.x;
		this._eyesAngles.y = base.transform.localEulerAngles.y;
		this._eyesTransform.localEulerAngles = this._eyesAngles.pitchEulerAngles;
		this._eyesSetup = true;
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0001D454 File Offset: 0x0001B654
	protected void InvalidateEyesAngles()
	{
		base.transform.localEulerAngles = this._eyesAngles.yawEulerAngles;
		this._eyesTransform.localEulerAngles = this._eyesAngles.pitchEulerAngles;
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x0001D490 File Offset: 0x0001B690
	protected virtual void AlterEyesLocalOrigin(ref Vector3 localPosition)
	{
		if (this.crouchable)
		{
			this._crouchable.ApplyCrouch(ref localPosition);
		}
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x0001D4B0 File Offset: 0x0001B6B0
	protected internal void InvalidateEyesOffset()
	{
		Vector3 eyesOffset = this._eyesOffset;
		this.AlterEyesLocalOrigin(ref eyesOffset);
		this._eyesTransform.localPosition = eyesOffset;
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x0001D4D8 File Offset: 0x0001B6D8
	private void OriginSetup()
	{
		this._originSetup = true;
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x0001D4E4 File Offset: 0x0001B6E4
	public void ApplyAdditiveEyeAngles(Angle2 angles)
	{
		float num = this._eyesAngles.pitch + angles.pitch;
		this.ClampPitch(ref num);
		if (angles.yaw != 0f)
		{
			this._eyesAngles.yaw = Mathf.DeltaAngle(0f, this._eyesAngles.yaw + angles.yaw);
			this._eyesAngles.pitch = num;
			this.InvalidateEyesAngles();
		}
		else if (num != angles.pitch)
		{
			this._eyesAngles.pitch = num;
			this.InvalidateEyesAngles();
		}
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x0001D57C File Offset: 0x0001B77C
	protected virtual void DoDestroy()
	{
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x0001D580 File Offset: 0x0001B780
	private void OnDestroy()
	{
		try
		{
			this.DoDestroy();
		}
		finally
		{
			if (this.signals_state != null)
			{
				this.signals_state = null;
			}
			base.OnDestroy();
		}
	}

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x0600062A RID: 1578 RVA: 0x0001D5D0 File Offset: 0x0001B7D0
	public bool signaledDeath
	{
		get
		{
			return this._signaledDeath;
		}
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x0001D5D8 File Offset: 0x0001B7D8
	private void signal_death_now(CharacterDeathSignalReason reason)
	{
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x0001D5DC File Offset: 0x0001B7DC
	public void Signal_ServerCharacterDeath()
	{
		this.signal_death_now(CharacterDeathSignalReason.Died);
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x0001D5E8 File Offset: 0x0001B7E8
	public void Signal_ServerCharacterDeathReset()
	{
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0001D5EC File Offset: 0x0001B7EC
	public void Signal_State_FlagsChanged(bool asFirst)
	{
		if (this.signals_state != null)
		{
			try
			{
				this.signals_state(this, asFirst);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex, this);
			}
		}
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0001D640 File Offset: 0x0001B840
	private void LoadTraitMap()
	{
		this._traitMapLoaded = TraitMap<CharacterTrait, CharacterTraitMap>.ByName(this._traitMapName, out this._traitMap);
		this._attemptedTraitMapLoad = true;
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x0001D660 File Offset: 0x0001B860
	protected void LoadTraitMapNonNetworked()
	{
		if (!this._traitMapLoaded)
		{
			this.LoadTraitMap();
		}
	}

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x06000631 RID: 1585 RVA: 0x0001D674 File Offset: 0x0001B874
	private CharacterTraitMap traitMap
	{
		get
		{
			if (!this._attemptedTraitMapLoad)
			{
				this.LoadTraitMap();
			}
			return this._traitMap;
		}
	}

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001D690 File Offset: 0x0001B890
	private bool traitMapLoaded
	{
		get
		{
			if (!this._attemptedTraitMapLoad)
			{
				this.LoadTraitMap();
			}
			return this._traitMapLoaded;
		}
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x0001D6AC File Offset: 0x0001B8AC
	public CharacterTrait GetTrait(Type characterTraitType)
	{
		return (!this.traitMapLoaded) ? null : this._traitMap.GetTrait(characterTraitType);
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x0001D6CC File Offset: 0x0001B8CC
	public TCharacterTrait GetTrait<TCharacterTrait>() where TCharacterTrait : CharacterTrait
	{
		return (!this.traitMapLoaded) ? ((TCharacterTrait)((object)null)) : this._traitMap.GetTrait<TCharacterTrait>();
	}

	// Token: 0x1700013A RID: 314
	// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001D6F0 File Offset: 0x0001B8F0
	public IDLocalCharacterIdleControl idleControl
	{
		get
		{
			if (!this.didIdleControlTest)
			{
				Character.SeekIDLocalComponentInChildren<Character, IDLocalCharacterIdleControl>(this, ref this._idleControl);
				this.didIdleControlTest = true;
			}
			return this._idleControl;
		}
	}

	// Token: 0x1700013B RID: 315
	// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001D718 File Offset: 0x0001B918
	public bool? idle
	{
		get
		{
			if (this.idleControl)
			{
				return new bool?(this._idleControl);
			}
			return null;
		}
	}

	// Token: 0x1700013C RID: 316
	// (get) Token: 0x06000637 RID: 1591 RVA: 0x0001D750 File Offset: 0x0001B950
	public VisNode visNode
	{
		get
		{
			if (!this.didVisNodeTest)
			{
				Character.SeekIDLocalComponentInChildren<Character, VisNode>(this, ref this._visNode);
				this.didVisNodeTest = true;
			}
			return this._visNode;
		}
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x0001D778 File Offset: 0x0001B978
	public bool CanSee(VisNode other)
	{
		return this.visNode && this._visNode.CanSee(other);
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x0001D79C File Offset: 0x0001B99C
	public bool CanSee(Character other)
	{
		return this.visNode && other && other.visNode && this._visNode.CanSee(other._visNode);
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x0001D7E8 File Offset: 0x0001B9E8
	public bool CanSee(IDMain other)
	{
		if (other is Character)
		{
			return this.CanSee((Character)other);
		}
		return other && this.CanSee(other.GetLocal<VisNode>());
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x0001D828 File Offset: 0x0001BA28
	public bool CanSeeUnobstructed(VisNode other)
	{
		return this.visNode && this._visNode.CanSeeUnobstructed(other);
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x0001D84C File Offset: 0x0001BA4C
	public bool CanSeeUnobstructed(Character other)
	{
		return this.visNode && other && other.visNode && this._visNode.CanSeeUnobstructed(other._visNode);
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0001D898 File Offset: 0x0001BA98
	public bool CanSeeUnobstructed(IDMain other)
	{
		if (other is Character)
		{
			return this.CanSeeUnobstructed((Character)other);
		}
		return other && this.CanSeeUnobstructed(other.GetLocal<VisNode>());
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0001D8D8 File Offset: 0x0001BAD8
	public bool CanSee(VisNode other, bool unobstructed)
	{
		return (!unobstructed) ? this.CanSee(other) : this.CanSeeUnobstructed(other);
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x0001D8F4 File Offset: 0x0001BAF4
	public bool CanSee(Character other, bool unobstructed)
	{
		return (!unobstructed) ? this.CanSee(other) : this.CanSeeUnobstructed(other);
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0001D910 File Offset: 0x0001BB10
	public bool CanSee(IDMain other, bool unobstructed)
	{
		return (!unobstructed) ? this.CanSee(other) : this.CanSeeUnobstructed(other);
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x0001D92C File Offset: 0x0001BB2C
	public bool AudibleMessage(Vector3 point, float hearRadius, string message, object arg)
	{
		return VisNode.AudibleMessage(this._visNode, point, hearRadius, message, arg);
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0001D940 File Offset: 0x0001BB40
	public bool AudibleMessage(Vector3 point, float hearRadius, string message)
	{
		return VisNode.AudibleMessage(this.visNode, point, hearRadius, message);
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0001D950 File Offset: 0x0001BB50
	public bool AudibleMessage(float hearRadius, string message, object arg)
	{
		return VisNode.AudibleMessage(this.visNode, hearRadius, message, arg);
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x0001D960 File Offset: 0x0001BB60
	public bool AudibleMessage(float hearRadius, string message)
	{
		return VisNode.AudibleMessage(this.visNode, hearRadius, message);
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0001D970 File Offset: 0x0001BB70
	public bool GestureMessage(string message)
	{
		return VisNode.GestureMessage(this.visNode, message, null);
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x0001D980 File Offset: 0x0001BB80
	public bool GestureMessage(string message, object arg)
	{
		return VisNode.GestureMessage(this.visNode, message, arg);
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x0001D990 File Offset: 0x0001BB90
	public bool AttentionMessage(string message)
	{
		return VisNode.AttentionMessage(this.visNode, message, null);
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x0001D9A0 File Offset: 0x0001BBA0
	public bool AttentionMessage(string message, object arg)
	{
		return VisNode.AttentionMessage(this.visNode, message, arg);
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0001D9B0 File Offset: 0x0001BBB0
	public bool ContactMessage(string message)
	{
		return VisNode.ContactMessage(this.visNode, message, null);
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x0001D9C0 File Offset: 0x0001BBC0
	public bool ContactMessage(string message, object arg)
	{
		return VisNode.ContactMessage(this.visNode, message, arg);
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0001D9D0 File Offset: 0x0001BBD0
	public bool StealthMessage(string message)
	{
		return VisNode.StealthMessage(this.visNode, message, null);
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0001D9E0 File Offset: 0x0001BBE0
	public bool StealthMessage(string message, object arg)
	{
		return VisNode.StealthMessage(this.visNode, message, arg);
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0001D9F0 File Offset: 0x0001BBF0
	public bool PreyMessage(string message)
	{
		return VisNode.PreyMessage(this.visNode, message, null);
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0001DA00 File Offset: 0x0001BC00
	public bool PreyMessage(string message, object arg)
	{
		return VisNode.PreyMessage(this.visNode, message, arg);
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0001DA10 File Offset: 0x0001BC10
	public bool ObliviousMessage(string message)
	{
		return VisNode.ObliviousMessage(this.visNode, message, null);
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x0001DA20 File Offset: 0x0001BC20
	public bool ObliviousMessage(string message, object arg)
	{
		return VisNode.ObliviousMessage(this.visNode, message, arg);
	}

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x06000651 RID: 1617 RVA: 0x0001DA30 File Offset: 0x0001BC30
	// (set) Token: 0x06000652 RID: 1618 RVA: 0x0001DA64 File Offset: 0x0001BC64
	public Vis.Mask viewMask
	{
		get
		{
			if (this.visNode)
			{
				return this._visNode.viewMask;
			}
			return default(Vis.Mask);
		}
		set
		{
			if (this.visNode)
			{
				this._visNode.viewMask = value;
			}
			else if (value.data != 0)
			{
				Debug.Log("no visnode", this);
			}
		}
	}

	// Token: 0x1700013E RID: 318
	// (get) Token: 0x06000653 RID: 1619 RVA: 0x0001DAAC File Offset: 0x0001BCAC
	// (set) Token: 0x06000654 RID: 1620 RVA: 0x0001DAE0 File Offset: 0x0001BCE0
	public Vis.Mask traitMask
	{
		get
		{
			if (this.visNode)
			{
				return this._visNode.traitMask;
			}
			return default(Vis.Mask);
		}
		set
		{
			if (this.visNode)
			{
				this._visNode.traitMask = value;
			}
			else if (value.data != 0)
			{
				Debug.Log("no visnode", this);
			}
		}
	}

	// Token: 0x1700013F RID: 319
	// (get) Token: 0x06000655 RID: 1621 RVA: 0x0001DB28 File Offset: 0x0001BD28
	// (set) Token: 0x06000656 RID: 1622 RVA: 0x0001DB48 File Offset: 0x0001BD48
	public bool blind
	{
		get
		{
			return !this.visNode || this._visNode.blind;
		}
		set
		{
			if (this.visNode)
			{
				this._visNode.blind = value;
			}
			else if (!value)
			{
				Debug.LogError("no visnode", this);
			}
		}
	}

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x06000657 RID: 1623 RVA: 0x0001DB88 File Offset: 0x0001BD88
	// (set) Token: 0x06000658 RID: 1624 RVA: 0x0001DBA8 File Offset: 0x0001BDA8
	public bool deaf
	{
		get
		{
			return !this.visNode || this._visNode.deaf;
		}
		set
		{
			if (this.visNode)
			{
				this._visNode.deaf = value;
			}
			else if (!value)
			{
				Debug.LogError("no visnode", this);
			}
		}
	}

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x06000659 RID: 1625 RVA: 0x0001DBE8 File Offset: 0x0001BDE8
	// (set) Token: 0x0600065A RID: 1626 RVA: 0x0001DC08 File Offset: 0x0001BE08
	public bool mute
	{
		get
		{
			return !this.visNode || this._visNode.mute;
		}
		set
		{
			if (this.visNode)
			{
				this._visNode.mute = value;
			}
			else if (!value)
			{
				Debug.LogError("no visnode", this);
			}
		}
	}

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x0600065B RID: 1627 RVA: 0x0001DC48 File Offset: 0x0001BE48
	public NavMeshAgent agent
	{
		get
		{
			return this._agent;
		}
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x0001DC50 File Offset: 0x0001BE50
	public bool CreateNavMeshAgent()
	{
		if (this._agent)
		{
			return true;
		}
		CharacterNavAgentTrait trait = this.GetTrait<CharacterNavAgentTrait>();
		if (!trait)
		{
			return false;
		}
		this._agent = base.GetComponent<NavMeshAgent>();
		if (!this._agent)
		{
			this._agent = base.gameObject.AddComponent<NavMeshAgent>();
		}
		trait.CopyTo(this._agent);
		return true;
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x0001DCC0 File Offset: 0x0001BEC0
	public void DestroyNavMeshAgent()
	{
		Object.Destroy(this._agent);
		this._agent = null;
	}

	// Token: 0x17000143 RID: 323
	// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001DCD4 File Offset: 0x0001BED4
	public CharacterInterpolatorBase interpolator
	{
		get
		{
			return this._interpolator;
		}
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x0001DCDC File Offset: 0x0001BEDC
	public bool CreateInterpolator()
	{
		if (this._interpolator)
		{
			return true;
		}
		CharacterInterpolatorTrait trait = this.GetTrait<CharacterInterpolatorTrait>();
		if (!trait)
		{
			return false;
		}
		this._interpolator = this.AddAddon<CharacterInterpolatorBase>(trait.interpolatorComponentTypeName);
		return this._interpolator;
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x0001DD2C File Offset: 0x0001BF2C
	public void DestroyInterpolator()
	{
		this.RemoveAddon<CharacterInterpolatorBase>(ref this._interpolator);
	}

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x06000661 RID: 1633 RVA: 0x0001DD3C File Offset: 0x0001BF3C
	public CCMotor ccmotor
	{
		get
		{
			return this._ccmotor;
		}
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x0001DD44 File Offset: 0x0001BF44
	public bool CreateCCMotor()
	{
		if (this._ccmotor)
		{
			return true;
		}
		CharacterCCMotorTrait trait = this.GetTrait<CharacterCCMotorTrait>();
		CCTotemPole cctotemPole = (CCTotemPole)Object.Instantiate(trait.prefab, this.origin, Quaternion.identity);
		this._ccmotor = cctotemPole.GetComponent<CCMotor>();
		if (!this._ccmotor)
		{
			this._ccmotor = cctotemPole.gameObject.AddComponent<CCMotor>();
			if (!this._ccmotor)
			{
				return false;
			}
		}
		this._ccmotor.InitializeSetup(this, cctotemPole, trait);
		return this._ccmotor;
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x0001DDE0 File Offset: 0x0001BFE0
	public void DestroyCCMotor()
	{
	}

	// Token: 0x17000145 RID: 325
	// (get) Token: 0x06000664 RID: 1636 RVA: 0x0001DDE4 File Offset: 0x0001BFE4
	public IDLocalCharacterAddon overlay
	{
		get
		{
			return this._overlay;
		}
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x0001DDEC File Offset: 0x0001BFEC
	public bool CreateOverlay()
	{
		if (this._overlay)
		{
			return true;
		}
		CharacterOverlayTrait trait = this.GetTrait<CharacterOverlayTrait>();
		if (!trait || string.IsNullOrEmpty(trait.overlayComponentName))
		{
			return false;
		}
		this._overlay = this.AddAddon(trait.overlayComponentName);
		return this._overlay;
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x0001DE4C File Offset: 0x0001C04C
	public void DestroyOverlay()
	{
		this.RemoveAddon<IDLocalCharacterAddon>(ref this._overlay);
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x0001DE5C File Offset: 0x0001C05C
	public T AddAddon<T>() where T : IDLocalCharacterAddon, new()
	{
		if (!Character.AddonRegistry<T>.valid)
		{
			throw new ArgumentOutOfRangeException("T");
		}
		T t = base.GetLocal<T>();
		if (!t)
		{
			t = base.gameObject.AddComponent<T>();
		}
		return (!this.InitAddon(t)) ? ((T)((object)null)) : t;
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x0001DEC0 File Offset: 0x0001C0C0
	public TBase AddAddon<TBase, T>() where TBase : IDLocalCharacterAddon where T : TBase, new()
	{
		return (TBase)((object)this.AddAddon<T>());
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x0001DED4 File Offset: 0x0001C0D4
	public IDLocalCharacterAddon AddAddon(Type addonType)
	{
		if (!Character.AddonRegistry.Validate(addonType))
		{
			throw new ArgumentOutOfRangeException("addonType", Convert.ToString(addonType));
		}
		IDLocalCharacterAddon idlocalCharacterAddon = (IDLocalCharacterAddon)base.GetComponent(addonType);
		if (!idlocalCharacterAddon)
		{
			idlocalCharacterAddon = (IDLocalCharacterAddon)base.gameObject.AddComponent(addonType);
		}
		return (!this.InitAddon(idlocalCharacterAddon)) ? null : idlocalCharacterAddon;
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x0001DF3C File Offset: 0x0001C13C
	public TBase AddAddon<TBase>(Type addonType) where TBase : IDLocalCharacterAddon
	{
		if (!typeof(TBase).IsAssignableFrom(addonType))
		{
			throw new ArgumentOutOfRangeException("addonType", Convert.ToString(addonType));
		}
		if (!Character.AddonRegistry.Validate(addonType))
		{
			throw new ArgumentOutOfRangeException("addonType", Convert.ToString(addonType));
		}
		TBase tbase = base.GetComponent<TBase>();
		if (!tbase)
		{
			tbase = (TBase)((object)base.gameObject.AddComponent(addonType));
		}
		else if (!addonType.IsAssignableFrom(tbase.GetType()))
		{
			throw new InvalidOperationException("The existing TBase component was not assignable to addonType");
		}
		return (!this.InitAddon(tbase)) ? ((TBase)((object)null)) : tbase;
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x0001DFFC File Offset: 0x0001C1FC
	public IDLocalCharacterAddon AddAddon(Type addonType, Type minimumType)
	{
		if (!minimumType.IsAssignableFrom(addonType))
		{
			throw new ArgumentOutOfRangeException("minimumType", Convert.ToString(addonType));
		}
		return this.AddAddon(addonType);
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x0001E030 File Offset: 0x0001C230
	public IDLocalCharacterAddon AddAddon(string addonTypeName)
	{
		if (!Character.AddonStringRegistry.Validate(addonTypeName))
		{
			throw new ArgumentOutOfRangeException("addonTypeName", addonTypeName);
		}
		IDLocalCharacterAddon idlocalCharacterAddon = (IDLocalCharacterAddon)base.GetComponent(addonTypeName);
		if (!idlocalCharacterAddon)
		{
			idlocalCharacterAddon = (IDLocalCharacterAddon)base.gameObject.AddComponent(addonTypeName);
		}
		return (!this.InitAddon(idlocalCharacterAddon)) ? null : idlocalCharacterAddon;
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x0001E094 File Offset: 0x0001C294
	public IDLocalCharacterAddon AddAddon(string addonTypeName, Type minimumType)
	{
		if (!Character.AddonStringRegistry.Validate(addonTypeName, minimumType))
		{
			throw new ArgumentOutOfRangeException("addonTypeName", addonTypeName);
		}
		IDLocalCharacterAddon idlocalCharacterAddon = (IDLocalCharacterAddon)base.GetComponent(addonTypeName);
		if (!idlocalCharacterAddon)
		{
			idlocalCharacterAddon = (IDLocalCharacterAddon)base.gameObject.AddComponent(addonTypeName);
		}
		return (!this.InitAddon(idlocalCharacterAddon)) ? null : idlocalCharacterAddon;
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x0001E0F8 File Offset: 0x0001C2F8
	public TBase AddAddon<TBase>(string addonTypeName) where TBase : IDLocalCharacterAddon
	{
		Type type;
		if (!Character.AddonStringRegistry.Validate<TBase>(addonTypeName, out type))
		{
			throw new ArgumentOutOfRangeException("TBase", addonTypeName);
		}
		TBase tbase = base.GetLocal<TBase>();
		if (!tbase)
		{
			tbase = (TBase)((object)base.gameObject.AddComponent(addonTypeName));
		}
		else if (!type.IsAssignableFrom(tbase.GetType()))
		{
			throw new InvalidOperationException("The existing TBase component was not assignable to addonType");
		}
		return (!this.InitAddon(tbase)) ? ((TBase)((object)null)) : tbase;
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x0001E18C File Offset: 0x0001C38C
	public void RemoveAddon(IDLocalCharacterAddon addon)
	{
		if (addon)
		{
			addon.RemoveAddon();
		}
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0001E1A0 File Offset: 0x0001C3A0
	public void RemoveAddon<T>(ref T addon) where T : IDLocalCharacterAddon
	{
		this.RemoveAddon(addon);
		addon = (T)((object)null);
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x0001E1C0 File Offset: 0x0001C3C0
	private bool InitAddon(IDLocalCharacterAddon addon)
	{
		byte b = addon.InitializeAddon(this);
		if ((b & 8) == 8)
		{
			return false;
		}
		if ((b & 2) == 2)
		{
			addon.PostInitializeAddon();
		}
		return true;
	}

	// Token: 0x17000146 RID: 326
	// (get) Token: 0x06000672 RID: 1650 RVA: 0x0001E1F0 File Offset: 0x0001C3F0
	public static IEnumerable<Character> PlayerRootCharacters
	{
		get
		{
			foreach (PlayerClient pc in PlayerClient.All)
			{
				Controllable controllable = pc.rootControllable;
				if (controllable)
				{
					yield return controllable.idMain;
				}
			}
			yield break;
		}
	}

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001E20C File Offset: 0x0001C40C
	public static IEnumerable<Character> PlayerCurrentCharacters
	{
		get
		{
			foreach (PlayerClient pc in PlayerClient.All)
			{
				Controllable controllable = pc.controllable;
				if (controllable)
				{
					yield return controllable.idMain;
				}
			}
			yield break;
		}
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x0001E228 File Offset: 0x0001C428
	public static IEnumerable<Character> RootCharacters(IEnumerable<PlayerClient> playerClients)
	{
		foreach (PlayerClient pc in playerClients)
		{
			Controllable controllable = pc.rootControllable;
			if (controllable)
			{
				yield return controllable.idMain;
			}
		}
		yield break;
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x0001E254 File Offset: 0x0001C454
	public static IEnumerable<Character> CurrentCharacters(IEnumerable<PlayerClient> playerClients)
	{
		foreach (PlayerClient pc in playerClients)
		{
			Controllable controllable = pc.controllable;
			if (controllable)
			{
				yield return controllable.idMain;
			}
		}
		yield break;
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x0001E280 File Offset: 0x0001C480
	public static IEnumerable<TCharacter> RootCharacters<TCharacter>(IEnumerable<PlayerClient> playerClients) where TCharacter : Character
	{
		foreach (PlayerClient pc in playerClients)
		{
			Controllable controllable = pc.rootControllable;
			if (controllable)
			{
				TCharacter character = controllable.idMain as TCharacter;
				if (character)
				{
					yield return character;
				}
			}
		}
		yield break;
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x0001E2AC File Offset: 0x0001C4AC
	public static IEnumerable<TCharacter> CurrentCharacters<TCharacter>(IEnumerable<PlayerClient> playerClients) where TCharacter : Character
	{
		foreach (PlayerClient pc in playerClients)
		{
			Controllable controllable = pc.controllable;
			if (controllable)
			{
				TCharacter character = controllable.idMain as TCharacter;
				if (character)
				{
					yield return character;
				}
			}
		}
		yield break;
	}

	// Token: 0x040004BC RID: 1212
	[PrefetchChildComponent(NameMask = "*Eyes")]
	[SerializeField]
	private Transform _eyesTransform;

	// Token: 0x040004BD RID: 1213
	private Angle2 _eyesAngles;

	// Token: 0x040004BE RID: 1214
	private Vector3 _eyesOffset;

	// Token: 0x040004BF RID: 1215
	private Vector3 _initialEyesOffset;

	// Token: 0x040004C0 RID: 1216
	[PrefetchComponent]
	[SerializeField]
	private Controllable _controllable;

	// Token: 0x040004C1 RID: 1217
	[SerializeField]
	[PrefetchChildComponent]
	private HitBoxSystem _hitBoxSystem;

	// Token: 0x040004C2 RID: 1218
	[PrefetchComponent]
	[SerializeField]
	private TakeDamage _takeDamage;

	// Token: 0x040004C3 RID: 1219
	[PrefetchComponent]
	[SerializeField]
	private RecoilSimulation _recoilSimulation;

	// Token: 0x040004C4 RID: 1220
	[PrefetchComponent]
	[SerializeField]
	private VisNode _visNode;

	// Token: 0x040004C5 RID: 1221
	[SerializeField]
	[PrefetchComponent]
	private Crouchable _crouchable;

	// Token: 0x040004C6 RID: 1222
	[PrefetchComponent]
	[SerializeField]
	private IDLocalCharacterIdleControl _idleControl;

	// Token: 0x040004C7 RID: 1223
	[NonSerialized]
	private IDLocalCharacterAddon _overlay;

	// Token: 0x040004C8 RID: 1224
	[NonSerialized]
	private CCMotor _ccmotor;

	// Token: 0x040004C9 RID: 1225
	[NonSerialized]
	private NavMeshAgent _agent;

	// Token: 0x040004CA RID: 1226
	[NonSerialized]
	private CharacterInterpolatorBase _interpolator;

	// Token: 0x040004CB RID: 1227
	[SerializeField]
	private string _traitMapName = "Default";

	// Token: 0x040004CC RID: 1228
	[NonSerialized]
	private bool _attemptedTraitMapLoad;

	// Token: 0x040004CD RID: 1229
	[NonSerialized]
	private bool _traitMapLoaded;

	// Token: 0x040004CE RID: 1230
	[NonSerialized]
	private CharacterTraitMap _traitMap;

	// Token: 0x040004CF RID: 1231
	[NonSerialized]
	private bool _signaledDeath;

	// Token: 0x040004D0 RID: 1232
	[NonSerialized]
	public bool lockMovement;

	// Token: 0x040004D1 RID: 1233
	[NonSerialized]
	public bool lockLook;

	// Token: 0x040004D2 RID: 1234
	[NonSerialized]
	private bool _eyesSetup;

	// Token: 0x040004D3 RID: 1235
	[NonSerialized]
	private bool _originSetup;

	// Token: 0x040004D4 RID: 1236
	[NonSerialized]
	private bool didHitBoxSystemTest;

	// Token: 0x040004D5 RID: 1237
	[NonSerialized]
	private bool didTakeDamageTest;

	// Token: 0x040004D6 RID: 1238
	[NonSerialized]
	private bool didControllableTest;

	// Token: 0x040004D7 RID: 1239
	[NonSerialized]
	private bool didRecoilSimulationTest;

	// Token: 0x040004D8 RID: 1240
	[NonSerialized]
	private bool didVisNodeTest;

	// Token: 0x040004D9 RID: 1241
	[NonSerialized]
	private bool didCrouchableTest;

	// Token: 0x040004DA RID: 1242
	[NonSerialized]
	private bool didIdleControlTest;

	// Token: 0x040004DB RID: 1243
	[SerializeField]
	private float _maxPitch = 89.9f;

	// Token: 0x040004DC RID: 1244
	[SerializeField]
	private float _minPitch = -89.9f;

	// Token: 0x040004DD RID: 1245
	[NonSerialized]
	private CharacterDeathSignal signals_death;

	// Token: 0x040004DE RID: 1246
	[NonSerialized]
	private CharacterStateSignal signals_state;

	// Token: 0x040004DF RID: 1247
	[NonSerialized]
	public CharacterStateFlags stateFlags;

	// Token: 0x020000F8 RID: 248
	private static class AddonRegistry<T> where T : IDLocalCharacterAddon, new()
	{
		// Token: 0x040004E0 RID: 1248
		public static readonly bool valid = Character.AddonRegistry.Validate(typeof(T));
	}

	// Token: 0x020000F9 RID: 249
	private static class AddonRegistry
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x0001E2FC File Offset: 0x0001C4FC
		public static bool Validate(Type type)
		{
			if (type == null)
			{
				return false;
			}
			bool flag;
			if (!Character.AddonRegistry.validatedCache.TryGetValue(type, out flag))
			{
				if (!typeof(IDLocalCharacterAddon).IsAssignableFrom(type))
				{
					Debug.LogError(string.Format("Type {0} is not a valid IDLocalCharacterAddon type", type));
				}
				else if (type.IsAbstract)
				{
					Debug.LogError(string.Format("Type {0} is abstract, thus not a valid IDLocalCharacterAddon type", type));
				}
				else if (Attribute.IsDefined(type, typeof(RequireComponent), false))
				{
					Debug.LogWarning(string.Format("Type {0} uses the RequireComponent attribute which could be dangerous with addons", type));
					flag = true;
				}
				else
				{
					flag = true;
				}
				Character.AddonRegistry.validatedCache[type] = flag;
			}
			return flag;
		}

		// Token: 0x040004E1 RID: 1249
		private static readonly Dictionary<Type, bool> validatedCache = new Dictionary<Type, bool>();
	}

	// Token: 0x020000FA RID: 250
	private static class AddonStringRegistry
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x0001E3D4 File Offset: 0x0001C5D4
		private static bool Validate(string typeName, out Type type)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				type = null;
				return false;
			}
			Character.AddonStringRegistry.TypePair typePair;
			if (!Character.AddonStringRegistry.validatedCache.TryGetValue(typeName, out typePair))
			{
				bool flag = TypeUtility.TryParse(typeName, out type) && Character.AddonRegistry.Validate(type);
				if (!flag)
				{
					foreach (string str in Character.AddonStringRegistry.assemblyStrings)
					{
						if (TypeUtility.TryParse(typeName + str, out type) && Character.AddonRegistry.Validate(type))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						type = null;
						Debug.LogError(string.Format("Couldnt associate string \"{0}\" with any valid addon type", typeName));
					}
				}
				Character.AddonStringRegistry.validatedCache[typeName] = new Character.AddonStringRegistry.TypePair(type, flag);
				return flag;
			}
			type = typePair.type;
			return typePair.valid;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001E4A8 File Offset: 0x0001C6A8
		public static bool Validate(string typeName)
		{
			Type type;
			return Character.AddonStringRegistry.Validate(typeName, out type);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001E4C0 File Offset: 0x0001C6C0
		public static bool Validate<TBase>(string typeName)
		{
			Type c;
			return Character.AddonStringRegistry.Validate(typeName, out c) && typeof(TBase).IsAssignableFrom(c);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001E4F0 File Offset: 0x0001C6F0
		public static bool Validate<TBase>(string typeName, out Type type)
		{
			return Character.AddonStringRegistry.Validate(typeName, out type) && typeof(TBase).IsAssignableFrom(type);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001E520 File Offset: 0x0001C720
		public static bool Validate(string typeName, Type minimumType)
		{
			Type c;
			return Character.AddonStringRegistry.Validate(typeName, out c) && minimumType.IsAssignableFrom(c);
		}

		// Token: 0x040004E2 RID: 1250
		private static readonly Dictionary<string, Character.AddonStringRegistry.TypePair> validatedCache = new Dictionary<string, Character.AddonStringRegistry.TypePair>();

		// Token: 0x040004E3 RID: 1251
		private static readonly string[] assemblyStrings = new string[]
		{
			", Assembly-CSharp-firstpass",
			", Assembly-CSharp"
		};

		// Token: 0x020000FB RID: 251
		private struct TypePair
		{
			// Token: 0x06000681 RID: 1665 RVA: 0x0001E544 File Offset: 0x0001C744
			public TypePair(Type type, bool valid)
			{
				this.type = type;
				this.valid = valid;
			}

			// Token: 0x040004E4 RID: 1252
			public readonly Type type;

			// Token: 0x040004E5 RID: 1253
			public readonly bool valid;
		}
	}
}
