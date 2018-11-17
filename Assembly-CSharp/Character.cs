using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000110 RID: 272
public class Character : IDMain
{
	// Token: 0x06000642 RID: 1602 RVA: 0x0001DE08 File Offset: 0x0001C008
	public Character() : this(1)
	{
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0001DE14 File Offset: 0x0001C014
	protected Character(IDFlags flags) : base(flags)
	{
	}

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000644 RID: 1604 RVA: 0x0001DE4C File Offset: 0x0001C04C
	// (remove) Token: 0x06000645 RID: 1605 RVA: 0x0001DE50 File Offset: 0x0001C050
	public event global::CharacterDeathSignal signal_death
	{
		add
		{
		}
		remove
		{
			if (!this._signaledDeath)
			{
				this.signals_death = (global::CharacterDeathSignal)Delegate.Remove(this.signals_death, value);
			}
		}
	}

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x06000646 RID: 1606 RVA: 0x0001DE80 File Offset: 0x0001C080
	// (remove) Token: 0x06000647 RID: 1607 RVA: 0x0001DEB0 File Offset: 0x0001C0B0
	public event global::CharacterStateSignal signal_state
	{
		add
		{
			if (!this._signaledDeath)
			{
				this.signals_state = (global::CharacterStateSignal)Delegate.Combine(this.signals_state, value);
			}
		}
		remove
		{
			if (!this._signaledDeath)
			{
				this.signals_state = (global::CharacterStateSignal)Delegate.Remove(this.signals_state, value);
			}
		}
	}

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x06000648 RID: 1608 RVA: 0x0001DEE0 File Offset: 0x0001C0E0
	[Obsolete("this is the character")]
	public global::Character character
	{
		get
		{
			return this;
		}
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0001DEE4 File Offset: 0x0001C0E4
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

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x0600064A RID: 1610 RVA: 0x0001DF14 File Offset: 0x0001C114
	public global::HitBoxSystem hitBoxSystem
	{
		get
		{
			if (!this.didHitBoxSystemTest)
			{
				global::Character.SeekIDRemoteComponentInChildren<global::Character, global::HitBoxSystem>(this, ref this._hitBoxSystem);
				this.didHitBoxSystemTest = true;
			}
			return this._hitBoxSystem;
		}
	}

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x0600064B RID: 1611 RVA: 0x0001DF3C File Offset: 0x0001C13C
	public global::RecoilSimulation recoilSimulation
	{
		get
		{
			if (!this.didRecoilSimulationTest)
			{
				global::Character.SeekIDLocalComponentInChildren<global::Character, global::RecoilSimulation>(this, ref this._recoilSimulation);
				this.didRecoilSimulationTest = true;
			}
			return this._recoilSimulation;
		}
	}

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x0600064C RID: 1612 RVA: 0x0001DF64 File Offset: 0x0001C164
	public bool controlled
	{
		get
		{
			return this.controllable && this._controllable.controlled;
		}
	}

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x0600064D RID: 1613 RVA: 0x0001DF84 File Offset: 0x0001C184
	public bool playerControlled
	{
		get
		{
			return this.controllable && this._controllable.playerControlled;
		}
	}

	// Token: 0x1700010E RID: 270
	// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001DFA4 File Offset: 0x0001C1A4
	public bool aiControlled
	{
		get
		{
			return this.controllable && this._controllable.aiControlled;
		}
	}

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x0600064F RID: 1615 RVA: 0x0001DFC4 File Offset: 0x0001C1C4
	public bool localPlayerControlled
	{
		get
		{
			return this.controllable && this._controllable.localPlayerControlled;
		}
	}

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x06000650 RID: 1616 RVA: 0x0001DFE4 File Offset: 0x0001C1E4
	public bool remotePlayerControlled
	{
		get
		{
			return this.controllable && this._controllable.remotePlayerControlled;
		}
	}

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x06000651 RID: 1617 RVA: 0x0001E004 File Offset: 0x0001C204
	public bool localAIControlled
	{
		get
		{
			return this.controllable && this._controllable.localAIControlled;
		}
	}

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001E024 File Offset: 0x0001C224
	public bool remoteAIControlled
	{
		get
		{
			return this.controllable && this._controllable.remoteAIControlled;
		}
	}

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x06000653 RID: 1619 RVA: 0x0001E044 File Offset: 0x0001C244
	public bool localControlled
	{
		get
		{
			return this.controllable && this._controllable.localControlled;
		}
	}

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x06000654 RID: 1620 RVA: 0x0001E064 File Offset: 0x0001C264
	public bool remoteControlled
	{
		get
		{
			return this.controllable && this._controllable.remoteControlled;
		}
	}

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x06000655 RID: 1621 RVA: 0x0001E084 File Offset: 0x0001C284
	public bool core
	{
		get
		{
			return this.controllable && this._controllable.core;
		}
	}

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001E0A4 File Offset: 0x0001C2A4
	public bool vessel
	{
		get
		{
			return this.controllable && this._controllable.vessel;
		}
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x06000657 RID: 1623 RVA: 0x0001E0C4 File Offset: 0x0001C2C4
	public global::Controllable controllable
	{
		get
		{
			if (!this.didControllableTest)
			{
				global::Character.SeekComponentInChildren<global::Character, global::Controllable>(this, ref this._controllable);
				this.didControllableTest = true;
			}
			return this._controllable;
		}
	}

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x06000658 RID: 1624 RVA: 0x0001E0EC File Offset: 0x0001C2EC
	public global::Controllable controlledControllable
	{
		get
		{
			return (!this.controllable || !this._controllable.controlled) ? null : this._controllable;
		}
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x06000659 RID: 1625 RVA: 0x0001E128 File Offset: 0x0001C328
	public global::Controllable playerControlledControllable
	{
		get
		{
			return (!this.controllable || !this._controllable.playerControlled) ? null : this._controllable;
		}
	}

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x0600065A RID: 1626 RVA: 0x0001E164 File Offset: 0x0001C364
	public global::Controllable aiControlledControllable
	{
		get
		{
			return (!this.controllable || !this._controllable.aiControlled) ? null : this._controllable;
		}
	}

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x0600065B RID: 1627 RVA: 0x0001E1A0 File Offset: 0x0001C3A0
	public global::Controllable localPlayerControlledControllable
	{
		get
		{
			return (!this.controllable || !this._controllable.localPlayerControlled) ? null : this._controllable;
		}
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x0600065C RID: 1628 RVA: 0x0001E1DC File Offset: 0x0001C3DC
	public global::Controllable localAIControlledControllable
	{
		get
		{
			return (!this.controllable || !this._controllable.localAIControlled) ? null : this._controllable;
		}
	}

	// Token: 0x1700011D RID: 285
	// (get) Token: 0x0600065D RID: 1629 RVA: 0x0001E218 File Offset: 0x0001C418
	public global::Controllable remotePlayerControlledControllable
	{
		get
		{
			return (!this.controllable || !this._controllable.remotePlayerControlled) ? null : this._controllable;
		}
	}

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001E254 File Offset: 0x0001C454
	public global::Controllable remoteAIControlledControllable
	{
		get
		{
			return (!this.controllable || !this._controllable.remoteAIControlled) ? null : this._controllable;
		}
	}

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x0600065F RID: 1631 RVA: 0x0001E290 File Offset: 0x0001C490
	public global::PlayerClient playerClient
	{
		get
		{
			return (!this._controllable) ? null : this._controllable.playerClient;
		}
	}

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x06000660 RID: 1632 RVA: 0x0001E2B4 File Offset: 0x0001C4B4
	public string npcName
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.npcName;
		}
	}

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x06000661 RID: 1633 RVA: 0x0001E2D8 File Offset: 0x0001C4D8
	public bool controlOverridden
	{
		get
		{
			return this.controllable && this._controllable.controlOverridden;
		}
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x0001E2F8 File Offset: 0x0001C4F8
	public bool ControlOverriddenBy(global::Controllable controllable)
	{
		return this.controllable && this._controllable.ControlOverriddenBy(controllable);
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x0001E31C File Offset: 0x0001C51C
	public bool ControlOverriddenBy(global::Controller controller)
	{
		return this.controllable && this._controllable.ControlOverriddenBy(controller);
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x0001E340 File Offset: 0x0001C540
	public bool ControlOverriddenBy(global::Character character)
	{
		return this.controllable && this._controllable.ControlOverriddenBy(character);
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x0001E364 File Offset: 0x0001C564
	public bool ControlOverriddenBy(IDMain main)
	{
		return this.controllable && this._controllable.ControlOverriddenBy(main);
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x0001E388 File Offset: 0x0001C588
	public bool ControlOverriddenBy(IDBase idBase)
	{
		return this.controllable && this._controllable.ControlOverriddenBy(idBase);
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x0001E3AC File Offset: 0x0001C5AC
	public bool ControlOverriddenBy(global::IDLocalCharacter idLocal)
	{
		return this.controllable && this._controllable.ControlOverriddenBy(idLocal);
	}

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x06000668 RID: 1640 RVA: 0x0001E3D0 File Offset: 0x0001C5D0
	public bool overridingControl
	{
		get
		{
			return this.controllable && this._controllable.overridingControl;
		}
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x0001E3F0 File Offset: 0x0001C5F0
	public bool OverridingControlOf(global::Controllable controllable)
	{
		return this.controllable && this._controllable.OverridingControlOf(controllable);
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x0001E414 File Offset: 0x0001C614
	public bool OverridingControlOf(global::Controller controller)
	{
		return this.controllable && this._controllable.OverridingControlOf(controller);
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x0001E438 File Offset: 0x0001C638
	public bool OverridingControlOf(global::Character character)
	{
		return this.controllable && this._controllable.OverridingControlOf(character);
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x0001E45C File Offset: 0x0001C65C
	public bool OverridingControlOf(IDMain main)
	{
		return this.controllable && this._controllable.OverridingControlOf(main);
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x0001E480 File Offset: 0x0001C680
	public bool OverridingControlOf(IDBase idBase)
	{
		return this.controllable && this._controllable.OverridingControlOf(idBase);
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x0001E4A4 File Offset: 0x0001C6A4
	public bool OverridingControlOf(global::IDLocalCharacter idLocal)
	{
		return this.controllable && this._controllable.OverridingControlOf(idLocal);
	}

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x0600066F RID: 1647 RVA: 0x0001E4C8 File Offset: 0x0001C6C8
	public bool assignedControl
	{
		get
		{
			return this.controllable && this._controllable.assignedControl;
		}
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0001E4E8 File Offset: 0x0001C6E8
	public bool AssignedControlOf(global::Controllable controllable)
	{
		return this.controllable && this._controllable.AssignedControlOf(controllable);
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x0001E50C File Offset: 0x0001C70C
	public bool AssignedControlOf(global::Controller controller)
	{
		return this.controllable && this._controllable.AssignedControlOf(controller);
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x0001E530 File Offset: 0x0001C730
	public bool AssignedControlOf(IDMain character)
	{
		return this.controllable && this._controllable.AssignedControlOf(character);
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x0001E554 File Offset: 0x0001C754
	public bool AssignedControlOf(IDBase idBase)
	{
		return this.controllable && this._controllable.AssignedControlOf(idBase);
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x0001E578 File Offset: 0x0001C778
	public global::RelativeControl RelativeControlTo(global::Controllable controllable)
	{
		return (!this.controllable) ? global::RelativeControl.Incompatible : this._controllable.RelativeControlTo(controllable);
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x0001E5A8 File Offset: 0x0001C7A8
	public global::RelativeControl RelativeControlTo(global::Controller controller)
	{
		return (!this.controllable) ? global::RelativeControl.Incompatible : this._controllable.RelativeControlTo(controller);
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x0001E5D8 File Offset: 0x0001C7D8
	public global::RelativeControl RelativeControlTo(global::Character character)
	{
		return (!this.controllable) ? global::RelativeControl.Incompatible : this._controllable.RelativeControlTo(character);
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x0001E608 File Offset: 0x0001C808
	public global::RelativeControl RelativeControlTo(IDMain main)
	{
		return (!this.controllable) ? global::RelativeControl.Incompatible : this._controllable.RelativeControlTo(main);
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x0001E638 File Offset: 0x0001C838
	public global::RelativeControl RelativeControlTo(global::IDLocalCharacter idLocal)
	{
		return (!this.controllable) ? global::RelativeControl.Incompatible : this._controllable.RelativeControlTo(idLocal);
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x0001E668 File Offset: 0x0001C868
	public global::RelativeControl RelativeControlTo(IDBase idBase)
	{
		return (!this.controllable) ? global::RelativeControl.Incompatible : this._controllable.RelativeControlTo(idBase);
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x0001E698 File Offset: 0x0001C898
	public global::RelativeControl RelativeControlFrom(global::Controllable controllable)
	{
		return (!this.controllable) ? global::RelativeControl.Incompatible : this._controllable.RelativeControlFrom(controllable);
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x0001E6C8 File Offset: 0x0001C8C8
	public global::RelativeControl RelativeControlFrom(global::Controller controller)
	{
		return (!this.controllable) ? global::RelativeControl.Incompatible : this._controllable.RelativeControlFrom(controller);
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x0001E6F8 File Offset: 0x0001C8F8
	public global::RelativeControl RelativeControlFrom(global::Character character)
	{
		return (!this.controllable) ? global::RelativeControl.Incompatible : this._controllable.RelativeControlFrom(character);
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0001E728 File Offset: 0x0001C928
	public global::RelativeControl RelativeControlFrom(IDMain main)
	{
		return (!this.controllable) ? global::RelativeControl.Incompatible : this._controllable.RelativeControlFrom(main);
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0001E758 File Offset: 0x0001C958
	public global::RelativeControl RelativeControlFrom(global::IDLocalCharacter idLocal)
	{
		return (!this.controllable) ? global::RelativeControl.Incompatible : this._controllable.RelativeControlFrom(idLocal);
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x0001E788 File Offset: 0x0001C988
	public global::RelativeControl RelativeControlFrom(IDBase idBase)
	{
		return (!this.controllable) ? global::RelativeControl.Incompatible : this._controllable.RelativeControlFrom(idBase);
	}

	// Token: 0x17000124 RID: 292
	// (get) Token: 0x06000680 RID: 1664 RVA: 0x0001E7B8 File Offset: 0x0001C9B8
	public global::Controllable masterControllable
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.masterControllable;
		}
	}

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001E7DC File Offset: 0x0001C9DC
	public global::Controllable rootControllable
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.rootControllable;
		}
	}

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001E800 File Offset: 0x0001CA00
	public global::Controllable nextControllable
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.nextControllable;
		}
	}

	// Token: 0x17000127 RID: 295
	// (get) Token: 0x06000683 RID: 1667 RVA: 0x0001E824 File Offset: 0x0001CA24
	public global::Controllable previousControllable
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.previousControllable;
		}
	}

	// Token: 0x17000128 RID: 296
	// (get) Token: 0x06000684 RID: 1668 RVA: 0x0001E848 File Offset: 0x0001CA48
	public global::Character masterCharacter
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.masterCharacter;
		}
	}

	// Token: 0x17000129 RID: 297
	// (get) Token: 0x06000685 RID: 1669 RVA: 0x0001E86C File Offset: 0x0001CA6C
	public global::Character rootCharacter
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.rootCharacter;
		}
	}

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001E890 File Offset: 0x0001CA90
	public global::Character nextCharacter
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.nextCharacter;
		}
	}

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001E8B4 File Offset: 0x0001CAB4
	public global::Character previousCharacter
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.previousCharacter;
		}
	}

	// Token: 0x1700012C RID: 300
	// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001E8D8 File Offset: 0x0001CAD8
	public int controlDepth
	{
		get
		{
			return (!this.controllable) ? -1 : this._controllable.controlDepth;
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001E8FC File Offset: 0x0001CAFC
	public int controlCount
	{
		get
		{
			return (!this.controllable) ? 0 : this._controllable.controlCount;
		}
	}

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001E920 File Offset: 0x0001CB20
	public string controllerClassName
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.controllerClassName;
		}
	}

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x0600068B RID: 1675 RVA: 0x0001E944 File Offset: 0x0001CB44
	public global::Controller controller
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.controller;
		}
	}

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x0600068C RID: 1676 RVA: 0x0001E968 File Offset: 0x0001CB68
	public global::Controller controlledController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.controlledController;
		}
	}

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001E98C File Offset: 0x0001CB8C
	public global::Controller playerControlledController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.playerControlledController;
		}
	}

	// Token: 0x17000132 RID: 306
	// (get) Token: 0x0600068E RID: 1678 RVA: 0x0001E9B0 File Offset: 0x0001CBB0
	public global::Controller aiControlledController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.aiControlledController;
		}
	}

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x0600068F RID: 1679 RVA: 0x0001E9D4 File Offset: 0x0001CBD4
	public global::Controller localPlayerControlledController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.localPlayerControlledController;
		}
	}

	// Token: 0x17000134 RID: 308
	// (get) Token: 0x06000690 RID: 1680 RVA: 0x0001E9F8 File Offset: 0x0001CBF8
	public global::Controller localAIControlledController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.localAIControlledController;
		}
	}

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001EA1C File Offset: 0x0001CC1C
	public global::Controller remotePlayerControlledController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.remotePlayerControlledController;
		}
	}

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x06000692 RID: 1682 RVA: 0x0001EA40 File Offset: 0x0001CC40
	public global::Controller remoteAIControlledController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.remoteAIControlledController;
		}
	}

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x06000693 RID: 1683 RVA: 0x0001EA64 File Offset: 0x0001CC64
	public global::Controller masterController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.masterController;
		}
	}

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x06000694 RID: 1684 RVA: 0x0001EA88 File Offset: 0x0001CC88
	public global::Controller rootController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.rootController;
		}
	}

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001EAAC File Offset: 0x0001CCAC
	public global::Controller nextController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.nextController;
		}
	}

	// Token: 0x1700013A RID: 314
	// (get) Token: 0x06000696 RID: 1686 RVA: 0x0001EAD0 File Offset: 0x0001CCD0
	public global::Controller previousController
	{
		get
		{
			return (!this.controllable) ? null : this._controllable.previousController;
		}
	}

	// Token: 0x1700013B RID: 315
	// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001EAF4 File Offset: 0x0001CCF4
	public global::TakeDamage takeDamage
	{
		get
		{
			if (!this.didTakeDamageTest)
			{
				global::Character.SeekIDLocalComponentInChildren<global::Character, global::TakeDamage>(this, ref this._takeDamage);
				this.didTakeDamageTest = true;
			}
			return this._takeDamage;
		}
	}

	// Token: 0x1700013C RID: 316
	// (get) Token: 0x06000698 RID: 1688 RVA: 0x0001EB1C File Offset: 0x0001CD1C
	public float health
	{
		get
		{
			return (!this.takeDamage) ? float.PositiveInfinity : this._takeDamage.health;
		}
	}

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x06000699 RID: 1689 RVA: 0x0001EB44 File Offset: 0x0001CD44
	public float healthFraction
	{
		get
		{
			return (!this.takeDamage) ? 1f : this._takeDamage.healthFraction;
		}
	}

	// Token: 0x1700013E RID: 318
	// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001EB6C File Offset: 0x0001CD6C
	public bool alive
	{
		get
		{
			return !this.takeDamage || this._takeDamage.alive;
		}
	}

	// Token: 0x1700013F RID: 319
	// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001EB90 File Offset: 0x0001CD90
	public bool dead
	{
		get
		{
			return this.takeDamage && this._takeDamage.dead;
		}
	}

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x0600069C RID: 1692 RVA: 0x0001EBB4 File Offset: 0x0001CDB4
	public float healthLoss
	{
		get
		{
			return (!this.takeDamage) ? 0f : this._takeDamage.healthLoss;
		}
	}

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x0600069D RID: 1693 RVA: 0x0001EBDC File Offset: 0x0001CDDC
	public float healthLossFraction
	{
		get
		{
			return (!this.takeDamage) ? 0f : this._takeDamage.healthLossFraction;
		}
	}

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x0600069E RID: 1694 RVA: 0x0001EC04 File Offset: 0x0001CE04
	public float maxHealth
	{
		get
		{
			return (!this.takeDamage) ? float.PositiveInfinity : this._takeDamage.maxHealth;
		}
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x0001EC2C File Offset: 0x0001CE2C
	public void AdjustClientSideHealth(float newHealthValue)
	{
		if (this.takeDamage)
		{
			this._takeDamage.health = newHealthValue;
		}
	}

	// Token: 0x17000143 RID: 323
	// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0001EC4C File Offset: 0x0001CE4C
	public float maxPitch
	{
		get
		{
			return this._maxPitch;
		}
	}

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0001EC54 File Offset: 0x0001CE54
	public float minPitch
	{
		get
		{
			return this._minPitch;
		}
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x0001EC5C File Offset: 0x0001CE5C
	public float ClampPitch(float v)
	{
		return (v >= this._minPitch) ? ((v <= this._maxPitch) ? v : this._maxPitch) : this._minPitch;
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x0001EC90 File Offset: 0x0001CE90
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

	// Token: 0x060006A4 RID: 1700 RVA: 0x0001ECC4 File Offset: 0x0001CEC4
	public global::Angle2 ClampPitch(global::Angle2 v)
	{
		this.ClampPitch(ref v.pitch);
		return v;
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x0001ECD4 File Offset: 0x0001CED4
	public void ClampPitch(ref global::Angle2 v)
	{
		this.ClampPitch(ref v.pitch);
	}

	// Token: 0x17000145 RID: 325
	// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0001ECE4 File Offset: 0x0001CEE4
	public global::Crouchable crouchable
	{
		get
		{
			if (!this.didCrouchableTest)
			{
				global::Character.SeekIDLocalComponentInChildren<global::Character, global::Crouchable>(this, ref this._crouchable);
				this.didCrouchableTest = true;
			}
			return this._crouchable;
		}
	}

	// Token: 0x17000146 RID: 326
	// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001ED0C File Offset: 0x0001CF0C
	public bool crouched
	{
		get
		{
			return this.crouchable && this.crouchable.crouched;
		}
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x0001ED38 File Offset: 0x0001CF38
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

	// Token: 0x060006A9 RID: 1705 RVA: 0x0001EE64 File Offset: 0x0001D064
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

	// Token: 0x060006AA RID: 1706 RVA: 0x0001EFA0 File Offset: 0x0001D1A0
	protected static bool SeekComponentInChildren<M, T>(M main, ref T component) where M : IDMain where T : Component
	{
		if (!component)
		{
			component = main.GetComponent<T>();
			return component;
		}
		return true;
	}

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001EFE8 File Offset: 0x0001D1E8
	public Vector3 initialEyesOffset
	{
		get
		{
			return this._initialEyesOffset;
		}
	}

	// Token: 0x17000148 RID: 328
	// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001EFF0 File Offset: 0x0001D1F0
	public float initialEyesOffsetX
	{
		get
		{
			return this._initialEyesOffset.x;
		}
	}

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001F000 File Offset: 0x0001D200
	public float initialEyesOffsetY
	{
		get
		{
			return this._initialEyesOffset.y;
		}
	}

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x060006AE RID: 1710 RVA: 0x0001F010 File Offset: 0x0001D210
	public float initialEyesOffsetZ
	{
		get
		{
			return this._initialEyesOffset.z;
		}
	}

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x060006AF RID: 1711 RVA: 0x0001F020 File Offset: 0x0001D220
	// (set) Token: 0x060006B0 RID: 1712 RVA: 0x0001F040 File Offset: 0x0001D240
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

	// Token: 0x1700014C RID: 332
	// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001F090 File Offset: 0x0001D290
	// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0001F0B0 File Offset: 0x0001D2B0
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

	// Token: 0x1700014D RID: 333
	// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001F100 File Offset: 0x0001D300
	// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0001F11C File Offset: 0x0001D31C
	public global::Angle2 eyesAngles
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

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0001F184 File Offset: 0x0001D384
	public Vector3 eyesOrigin
	{
		get
		{
			return this._eyesTransform.position;
		}
	}

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0001F194 File Offset: 0x0001D394
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

	// Token: 0x17000150 RID: 336
	// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001F1C4 File Offset: 0x0001D3C4
	// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0001F1E0 File Offset: 0x0001D3E0
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

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001F220 File Offset: 0x0001D420
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

	// Token: 0x17000152 RID: 338
	// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001F25C File Offset: 0x0001D45C
	// (set) Token: 0x060006BB RID: 1723 RVA: 0x0001F27C File Offset: 0x0001D47C
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

	// Token: 0x17000153 RID: 339
	// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001F304 File Offset: 0x0001D504
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

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001F320 File Offset: 0x0001D520
	// (set) Token: 0x060006BE RID: 1726 RVA: 0x0001F330 File Offset: 0x0001D530
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

	// Token: 0x17000155 RID: 341
	// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001F34C File Offset: 0x0001D54C
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

	// Token: 0x17000156 RID: 342
	// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001F384 File Offset: 0x0001D584
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

	// Token: 0x17000157 RID: 343
	// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001F3BC File Offset: 0x0001D5BC
	public Vector3 up
	{
		get
		{
			return Vector3.up;
		}
	}

	// Token: 0x17000158 RID: 344
	// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001F3C4 File Offset: 0x0001D5C4
	// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0001F3F4 File Offset: 0x0001D5F4
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

	// Token: 0x060006C4 RID: 1732 RVA: 0x0001F4CC File Offset: 0x0001D6CC
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

	// Token: 0x060006C5 RID: 1733 RVA: 0x0001F598 File Offset: 0x0001D798
	protected void InvalidateEyesAngles()
	{
		base.transform.localEulerAngles = this._eyesAngles.yawEulerAngles;
		this._eyesTransform.localEulerAngles = this._eyesAngles.pitchEulerAngles;
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x0001F5D4 File Offset: 0x0001D7D4
	protected virtual void AlterEyesLocalOrigin(ref Vector3 localPosition)
	{
		if (this.crouchable)
		{
			this._crouchable.ApplyCrouch(ref localPosition);
		}
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x0001F5F4 File Offset: 0x0001D7F4
	protected internal void InvalidateEyesOffset()
	{
		Vector3 eyesOffset = this._eyesOffset;
		this.AlterEyesLocalOrigin(ref eyesOffset);
		this._eyesTransform.localPosition = eyesOffset;
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x0001F61C File Offset: 0x0001D81C
	private void OriginSetup()
	{
		this._originSetup = true;
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x0001F628 File Offset: 0x0001D828
	public void ApplyAdditiveEyeAngles(global::Angle2 angles)
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

	// Token: 0x060006CA RID: 1738 RVA: 0x0001F6C0 File Offset: 0x0001D8C0
	protected virtual void DoDestroy()
	{
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x0001F6C4 File Offset: 0x0001D8C4
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

	// Token: 0x17000159 RID: 345
	// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001F714 File Offset: 0x0001D914
	public bool signaledDeath
	{
		get
		{
			return this._signaledDeath;
		}
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x0001F71C File Offset: 0x0001D91C
	private void signal_death_now(global::CharacterDeathSignalReason reason)
	{
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x0001F720 File Offset: 0x0001D920
	public void Signal_ServerCharacterDeath()
	{
		this.signal_death_now(global::CharacterDeathSignalReason.Died);
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x0001F72C File Offset: 0x0001D92C
	public void Signal_ServerCharacterDeathReset()
	{
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x0001F730 File Offset: 0x0001D930
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

	// Token: 0x060006D1 RID: 1745 RVA: 0x0001F784 File Offset: 0x0001D984
	private void LoadTraitMap()
	{
		this._traitMapLoaded = global::TraitMap<global::CharacterTrait, global::CharacterTraitMap>.ByName(this._traitMapName, out this._traitMap);
		this._attemptedTraitMapLoad = true;
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x0001F7A4 File Offset: 0x0001D9A4
	protected void LoadTraitMapNonNetworked()
	{
		if (!this._traitMapLoaded)
		{
			this.LoadTraitMap();
		}
	}

	// Token: 0x1700015A RID: 346
	// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0001F7B8 File Offset: 0x0001D9B8
	private global::CharacterTraitMap traitMap
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

	// Token: 0x1700015B RID: 347
	// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001F7D4 File Offset: 0x0001D9D4
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

	// Token: 0x060006D5 RID: 1749 RVA: 0x0001F7F0 File Offset: 0x0001D9F0
	public global::CharacterTrait GetTrait(Type characterTraitType)
	{
		return (!this.traitMapLoaded) ? null : this._traitMap.GetTrait(characterTraitType);
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x0001F810 File Offset: 0x0001DA10
	public TCharacterTrait GetTrait<TCharacterTrait>() where TCharacterTrait : global::CharacterTrait
	{
		return (!this.traitMapLoaded) ? ((TCharacterTrait)((object)null)) : this._traitMap.GetTrait<TCharacterTrait>();
	}

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001F834 File Offset: 0x0001DA34
	public global::IDLocalCharacterIdleControl idleControl
	{
		get
		{
			if (!this.didIdleControlTest)
			{
				global::Character.SeekIDLocalComponentInChildren<global::Character, global::IDLocalCharacterIdleControl>(this, ref this._idleControl);
				this.didIdleControlTest = true;
			}
			return this._idleControl;
		}
	}

	// Token: 0x1700015D RID: 349
	// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001F85C File Offset: 0x0001DA5C
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

	// Token: 0x1700015E RID: 350
	// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001F894 File Offset: 0x0001DA94
	public global::VisNode visNode
	{
		get
		{
			if (!this.didVisNodeTest)
			{
				global::Character.SeekIDLocalComponentInChildren<global::Character, global::VisNode>(this, ref this._visNode);
				this.didVisNodeTest = true;
			}
			return this._visNode;
		}
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x0001F8BC File Offset: 0x0001DABC
	public bool CanSee(global::VisNode other)
	{
		return this.visNode && this._visNode.CanSee(other);
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x0001F8E0 File Offset: 0x0001DAE0
	public bool CanSee(global::Character other)
	{
		return this.visNode && other && other.visNode && this._visNode.CanSee(other._visNode);
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x0001F92C File Offset: 0x0001DB2C
	public bool CanSee(IDMain other)
	{
		if (other is global::Character)
		{
			return this.CanSee((global::Character)other);
		}
		return other && this.CanSee(other.GetLocal<global::VisNode>());
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x0001F96C File Offset: 0x0001DB6C
	public bool CanSeeUnobstructed(global::VisNode other)
	{
		return this.visNode && this._visNode.CanSeeUnobstructed(other);
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x0001F990 File Offset: 0x0001DB90
	public bool CanSeeUnobstructed(global::Character other)
	{
		return this.visNode && other && other.visNode && this._visNode.CanSeeUnobstructed(other._visNode);
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x0001F9DC File Offset: 0x0001DBDC
	public bool CanSeeUnobstructed(IDMain other)
	{
		if (other is global::Character)
		{
			return this.CanSeeUnobstructed((global::Character)other);
		}
		return other && this.CanSeeUnobstructed(other.GetLocal<global::VisNode>());
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x0001FA1C File Offset: 0x0001DC1C
	public bool CanSee(global::VisNode other, bool unobstructed)
	{
		return (!unobstructed) ? this.CanSee(other) : this.CanSeeUnobstructed(other);
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x0001FA38 File Offset: 0x0001DC38
	public bool CanSee(global::Character other, bool unobstructed)
	{
		return (!unobstructed) ? this.CanSee(other) : this.CanSeeUnobstructed(other);
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x0001FA54 File Offset: 0x0001DC54
	public bool CanSee(IDMain other, bool unobstructed)
	{
		return (!unobstructed) ? this.CanSee(other) : this.CanSeeUnobstructed(other);
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x0001FA70 File Offset: 0x0001DC70
	public bool AudibleMessage(Vector3 point, float hearRadius, string message, object arg)
	{
		return global::VisNode.AudibleMessage(this._visNode, point, hearRadius, message, arg);
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x0001FA84 File Offset: 0x0001DC84
	public bool AudibleMessage(Vector3 point, float hearRadius, string message)
	{
		return global::VisNode.AudibleMessage(this.visNode, point, hearRadius, message);
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x0001FA94 File Offset: 0x0001DC94
	public bool AudibleMessage(float hearRadius, string message, object arg)
	{
		return global::VisNode.AudibleMessage(this.visNode, hearRadius, message, arg);
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x0001FAA4 File Offset: 0x0001DCA4
	public bool AudibleMessage(float hearRadius, string message)
	{
		return global::VisNode.AudibleMessage(this.visNode, hearRadius, message);
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x0001FAB4 File Offset: 0x0001DCB4
	public bool GestureMessage(string message)
	{
		return global::VisNode.GestureMessage(this.visNode, message, null);
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x0001FAC4 File Offset: 0x0001DCC4
	public bool GestureMessage(string message, object arg)
	{
		return global::VisNode.GestureMessage(this.visNode, message, arg);
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x0001FAD4 File Offset: 0x0001DCD4
	public bool AttentionMessage(string message)
	{
		return global::VisNode.AttentionMessage(this.visNode, message, null);
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x0001FAE4 File Offset: 0x0001DCE4
	public bool AttentionMessage(string message, object arg)
	{
		return global::VisNode.AttentionMessage(this.visNode, message, arg);
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x0001FAF4 File Offset: 0x0001DCF4
	public bool ContactMessage(string message)
	{
		return global::VisNode.ContactMessage(this.visNode, message, null);
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x0001FB04 File Offset: 0x0001DD04
	public bool ContactMessage(string message, object arg)
	{
		return global::VisNode.ContactMessage(this.visNode, message, arg);
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x0001FB14 File Offset: 0x0001DD14
	public bool StealthMessage(string message)
	{
		return global::VisNode.StealthMessage(this.visNode, message, null);
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x0001FB24 File Offset: 0x0001DD24
	public bool StealthMessage(string message, object arg)
	{
		return global::VisNode.StealthMessage(this.visNode, message, arg);
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x0001FB34 File Offset: 0x0001DD34
	public bool PreyMessage(string message)
	{
		return global::VisNode.PreyMessage(this.visNode, message, null);
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x0001FB44 File Offset: 0x0001DD44
	public bool PreyMessage(string message, object arg)
	{
		return global::VisNode.PreyMessage(this.visNode, message, arg);
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x0001FB54 File Offset: 0x0001DD54
	public bool ObliviousMessage(string message)
	{
		return global::VisNode.ObliviousMessage(this.visNode, message, null);
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x0001FB64 File Offset: 0x0001DD64
	public bool ObliviousMessage(string message, object arg)
	{
		return global::VisNode.ObliviousMessage(this.visNode, message, arg);
	}

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001FB74 File Offset: 0x0001DD74
	// (set) Token: 0x060006F4 RID: 1780 RVA: 0x0001FBA8 File Offset: 0x0001DDA8
	public global::Vis.Mask viewMask
	{
		get
		{
			if (this.visNode)
			{
				return this._visNode.viewMask;
			}
			return default(global::Vis.Mask);
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

	// Token: 0x17000160 RID: 352
	// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0001FBF0 File Offset: 0x0001DDF0
	// (set) Token: 0x060006F6 RID: 1782 RVA: 0x0001FC24 File Offset: 0x0001DE24
	public global::Vis.Mask traitMask
	{
		get
		{
			if (this.visNode)
			{
				return this._visNode.traitMask;
			}
			return default(global::Vis.Mask);
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

	// Token: 0x17000161 RID: 353
	// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001FC6C File Offset: 0x0001DE6C
	// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0001FC8C File Offset: 0x0001DE8C
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

	// Token: 0x17000162 RID: 354
	// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001FCCC File Offset: 0x0001DECC
	// (set) Token: 0x060006FA RID: 1786 RVA: 0x0001FCEC File Offset: 0x0001DEEC
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

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001FD2C File Offset: 0x0001DF2C
	// (set) Token: 0x060006FC RID: 1788 RVA: 0x0001FD4C File Offset: 0x0001DF4C
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

	// Token: 0x17000164 RID: 356
	// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001FD8C File Offset: 0x0001DF8C
	public NavMeshAgent agent
	{
		get
		{
			return this._agent;
		}
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x0001FD94 File Offset: 0x0001DF94
	public bool CreateNavMeshAgent()
	{
		if (this._agent)
		{
			return true;
		}
		global::CharacterNavAgentTrait trait = this.GetTrait<global::CharacterNavAgentTrait>();
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

	// Token: 0x060006FF RID: 1791 RVA: 0x0001FE04 File Offset: 0x0001E004
	public void DestroyNavMeshAgent()
	{
		Object.Destroy(this._agent);
		this._agent = null;
	}

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001FE18 File Offset: 0x0001E018
	public global::CharacterInterpolatorBase interpolator
	{
		get
		{
			return this._interpolator;
		}
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x0001FE20 File Offset: 0x0001E020
	public bool CreateInterpolator()
	{
		if (this._interpolator)
		{
			return true;
		}
		global::CharacterInterpolatorTrait trait = this.GetTrait<global::CharacterInterpolatorTrait>();
		if (!trait)
		{
			return false;
		}
		this._interpolator = this.AddAddon<global::CharacterInterpolatorBase>(trait.interpolatorComponentTypeName);
		return this._interpolator;
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x0001FE70 File Offset: 0x0001E070
	public void DestroyInterpolator()
	{
		this.RemoveAddon<global::CharacterInterpolatorBase>(ref this._interpolator);
	}

	// Token: 0x17000166 RID: 358
	// (get) Token: 0x06000703 RID: 1795 RVA: 0x0001FE80 File Offset: 0x0001E080
	public global::CCMotor ccmotor
	{
		get
		{
			return this._ccmotor;
		}
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x0001FE88 File Offset: 0x0001E088
	public bool CreateCCMotor()
	{
		if (this._ccmotor)
		{
			return true;
		}
		global::CharacterCCMotorTrait trait = this.GetTrait<global::CharacterCCMotorTrait>();
		global::CCTotemPole cctotemPole = (global::CCTotemPole)Object.Instantiate(trait.prefab, this.origin, Quaternion.identity);
		this._ccmotor = cctotemPole.GetComponent<global::CCMotor>();
		if (!this._ccmotor)
		{
			this._ccmotor = cctotemPole.gameObject.AddComponent<global::CCMotor>();
			if (!this._ccmotor)
			{
				return false;
			}
		}
		this._ccmotor.InitializeSetup(this, cctotemPole, trait);
		return this._ccmotor;
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x0001FF24 File Offset: 0x0001E124
	public void DestroyCCMotor()
	{
	}

	// Token: 0x17000167 RID: 359
	// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001FF28 File Offset: 0x0001E128
	public global::IDLocalCharacterAddon overlay
	{
		get
		{
			return this._overlay;
		}
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x0001FF30 File Offset: 0x0001E130
	public bool CreateOverlay()
	{
		if (this._overlay)
		{
			return true;
		}
		global::CharacterOverlayTrait trait = this.GetTrait<global::CharacterOverlayTrait>();
		if (!trait || string.IsNullOrEmpty(trait.overlayComponentName))
		{
			return false;
		}
		this._overlay = this.AddAddon(trait.overlayComponentName);
		return this._overlay;
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x0001FF90 File Offset: 0x0001E190
	public void DestroyOverlay()
	{
		this.RemoveAddon<global::IDLocalCharacterAddon>(ref this._overlay);
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x0001FFA0 File Offset: 0x0001E1A0
	public T AddAddon<T>() where T : global::IDLocalCharacterAddon, new()
	{
		if (!global::Character.AddonRegistry<T>.valid)
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

	// Token: 0x0600070A RID: 1802 RVA: 0x00020004 File Offset: 0x0001E204
	public TBase AddAddon<TBase, T>() where TBase : global::IDLocalCharacterAddon where T : TBase, new()
	{
		return (TBase)((object)this.AddAddon<T>());
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x00020018 File Offset: 0x0001E218
	public global::IDLocalCharacterAddon AddAddon(Type addonType)
	{
		if (!global::Character.AddonRegistry.Validate(addonType))
		{
			throw new ArgumentOutOfRangeException("addonType", Convert.ToString(addonType));
		}
		global::IDLocalCharacterAddon idlocalCharacterAddon = (global::IDLocalCharacterAddon)base.GetComponent(addonType);
		if (!idlocalCharacterAddon)
		{
			idlocalCharacterAddon = (global::IDLocalCharacterAddon)base.gameObject.AddComponent(addonType);
		}
		return (!this.InitAddon(idlocalCharacterAddon)) ? null : idlocalCharacterAddon;
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x00020080 File Offset: 0x0001E280
	public TBase AddAddon<TBase>(Type addonType) where TBase : global::IDLocalCharacterAddon
	{
		if (!typeof(TBase).IsAssignableFrom(addonType))
		{
			throw new ArgumentOutOfRangeException("addonType", Convert.ToString(addonType));
		}
		if (!global::Character.AddonRegistry.Validate(addonType))
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

	// Token: 0x0600070D RID: 1805 RVA: 0x00020140 File Offset: 0x0001E340
	public global::IDLocalCharacterAddon AddAddon(Type addonType, Type minimumType)
	{
		if (!minimumType.IsAssignableFrom(addonType))
		{
			throw new ArgumentOutOfRangeException("minimumType", Convert.ToString(addonType));
		}
		return this.AddAddon(addonType);
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x00020174 File Offset: 0x0001E374
	public global::IDLocalCharacterAddon AddAddon(string addonTypeName)
	{
		if (!global::Character.AddonStringRegistry.Validate(addonTypeName))
		{
			throw new ArgumentOutOfRangeException("addonTypeName", addonTypeName);
		}
		global::IDLocalCharacterAddon idlocalCharacterAddon = (global::IDLocalCharacterAddon)base.GetComponent(addonTypeName);
		if (!idlocalCharacterAddon)
		{
			idlocalCharacterAddon = (global::IDLocalCharacterAddon)base.gameObject.AddComponent(addonTypeName);
		}
		return (!this.InitAddon(idlocalCharacterAddon)) ? null : idlocalCharacterAddon;
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x000201D8 File Offset: 0x0001E3D8
	public global::IDLocalCharacterAddon AddAddon(string addonTypeName, Type minimumType)
	{
		if (!global::Character.AddonStringRegistry.Validate(addonTypeName, minimumType))
		{
			throw new ArgumentOutOfRangeException("addonTypeName", addonTypeName);
		}
		global::IDLocalCharacterAddon idlocalCharacterAddon = (global::IDLocalCharacterAddon)base.GetComponent(addonTypeName);
		if (!idlocalCharacterAddon)
		{
			idlocalCharacterAddon = (global::IDLocalCharacterAddon)base.gameObject.AddComponent(addonTypeName);
		}
		return (!this.InitAddon(idlocalCharacterAddon)) ? null : idlocalCharacterAddon;
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x0002023C File Offset: 0x0001E43C
	public TBase AddAddon<TBase>(string addonTypeName) where TBase : global::IDLocalCharacterAddon
	{
		Type type;
		if (!global::Character.AddonStringRegistry.Validate<TBase>(addonTypeName, out type))
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

	// Token: 0x06000711 RID: 1809 RVA: 0x000202D0 File Offset: 0x0001E4D0
	public void RemoveAddon(global::IDLocalCharacterAddon addon)
	{
		if (addon)
		{
			addon.RemoveAddon();
		}
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x000202E4 File Offset: 0x0001E4E4
	public void RemoveAddon<T>(ref T addon) where T : global::IDLocalCharacterAddon
	{
		this.RemoveAddon(addon);
		addon = (T)((object)null);
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x00020304 File Offset: 0x0001E504
	private bool InitAddon(global::IDLocalCharacterAddon addon)
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

	// Token: 0x17000168 RID: 360
	// (get) Token: 0x06000714 RID: 1812 RVA: 0x00020334 File Offset: 0x0001E534
	public static IEnumerable<global::Character> PlayerRootCharacters
	{
		get
		{
			foreach (global::PlayerClient pc in global::PlayerClient.All)
			{
				global::Controllable controllable = pc.rootControllable;
				if (controllable)
				{
					yield return controllable.idMain;
				}
			}
			yield break;
		}
	}

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x06000715 RID: 1813 RVA: 0x00020350 File Offset: 0x0001E550
	public static IEnumerable<global::Character> PlayerCurrentCharacters
	{
		get
		{
			foreach (global::PlayerClient pc in global::PlayerClient.All)
			{
				global::Controllable controllable = pc.controllable;
				if (controllable)
				{
					yield return controllable.idMain;
				}
			}
			yield break;
		}
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x0002036C File Offset: 0x0001E56C
	public static IEnumerable<global::Character> RootCharacters(IEnumerable<global::PlayerClient> playerClients)
	{
		foreach (global::PlayerClient pc in playerClients)
		{
			global::Controllable controllable = pc.rootControllable;
			if (controllable)
			{
				yield return controllable.idMain;
			}
		}
		yield break;
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x00020398 File Offset: 0x0001E598
	public static IEnumerable<global::Character> CurrentCharacters(IEnumerable<global::PlayerClient> playerClients)
	{
		foreach (global::PlayerClient pc in playerClients)
		{
			global::Controllable controllable = pc.controllable;
			if (controllable)
			{
				yield return controllable.idMain;
			}
		}
		yield break;
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x000203C4 File Offset: 0x0001E5C4
	public static IEnumerable<TCharacter> RootCharacters<TCharacter>(IEnumerable<global::PlayerClient> playerClients) where TCharacter : global::Character
	{
		foreach (global::PlayerClient pc in playerClients)
		{
			global::Controllable controllable = pc.rootControllable;
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

	// Token: 0x06000719 RID: 1817 RVA: 0x000203F0 File Offset: 0x0001E5F0
	public static IEnumerable<TCharacter> CurrentCharacters<TCharacter>(IEnumerable<global::PlayerClient> playerClients) where TCharacter : global::Character
	{
		foreach (global::PlayerClient pc in playerClients)
		{
			global::Controllable controllable = pc.controllable;
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

	// Token: 0x0400055F RID: 1375
	[PrefetchChildComponent(NameMask = "*Eyes")]
	[SerializeField]
	private Transform _eyesTransform;

	// Token: 0x04000560 RID: 1376
	private global::Angle2 _eyesAngles;

	// Token: 0x04000561 RID: 1377
	private Vector3 _eyesOffset;

	// Token: 0x04000562 RID: 1378
	private Vector3 _initialEyesOffset;

	// Token: 0x04000563 RID: 1379
	[PrefetchComponent]
	[SerializeField]
	private global::Controllable _controllable;

	// Token: 0x04000564 RID: 1380
	[PrefetchChildComponent]
	[SerializeField]
	private global::HitBoxSystem _hitBoxSystem;

	// Token: 0x04000565 RID: 1381
	[SerializeField]
	[PrefetchComponent]
	private global::TakeDamage _takeDamage;

	// Token: 0x04000566 RID: 1382
	[PrefetchComponent]
	[SerializeField]
	private global::RecoilSimulation _recoilSimulation;

	// Token: 0x04000567 RID: 1383
	[SerializeField]
	[PrefetchComponent]
	private global::VisNode _visNode;

	// Token: 0x04000568 RID: 1384
	[PrefetchComponent]
	[SerializeField]
	private global::Crouchable _crouchable;

	// Token: 0x04000569 RID: 1385
	[SerializeField]
	[PrefetchComponent]
	private global::IDLocalCharacterIdleControl _idleControl;

	// Token: 0x0400056A RID: 1386
	[NonSerialized]
	private global::IDLocalCharacterAddon _overlay;

	// Token: 0x0400056B RID: 1387
	[NonSerialized]
	private global::CCMotor _ccmotor;

	// Token: 0x0400056C RID: 1388
	[NonSerialized]
	private NavMeshAgent _agent;

	// Token: 0x0400056D RID: 1389
	[NonSerialized]
	private global::CharacterInterpolatorBase _interpolator;

	// Token: 0x0400056E RID: 1390
	[SerializeField]
	private string _traitMapName = "Default";

	// Token: 0x0400056F RID: 1391
	[NonSerialized]
	private bool _attemptedTraitMapLoad;

	// Token: 0x04000570 RID: 1392
	[NonSerialized]
	private bool _traitMapLoaded;

	// Token: 0x04000571 RID: 1393
	[NonSerialized]
	private global::CharacterTraitMap _traitMap;

	// Token: 0x04000572 RID: 1394
	[NonSerialized]
	private bool _signaledDeath;

	// Token: 0x04000573 RID: 1395
	[NonSerialized]
	public bool lockMovement;

	// Token: 0x04000574 RID: 1396
	[NonSerialized]
	public bool lockLook;

	// Token: 0x04000575 RID: 1397
	[NonSerialized]
	private bool _eyesSetup;

	// Token: 0x04000576 RID: 1398
	[NonSerialized]
	private bool _originSetup;

	// Token: 0x04000577 RID: 1399
	[NonSerialized]
	private bool didHitBoxSystemTest;

	// Token: 0x04000578 RID: 1400
	[NonSerialized]
	private bool didTakeDamageTest;

	// Token: 0x04000579 RID: 1401
	[NonSerialized]
	private bool didControllableTest;

	// Token: 0x0400057A RID: 1402
	[NonSerialized]
	private bool didRecoilSimulationTest;

	// Token: 0x0400057B RID: 1403
	[NonSerialized]
	private bool didVisNodeTest;

	// Token: 0x0400057C RID: 1404
	[NonSerialized]
	private bool didCrouchableTest;

	// Token: 0x0400057D RID: 1405
	[NonSerialized]
	private bool didIdleControlTest;

	// Token: 0x0400057E RID: 1406
	[SerializeField]
	private float _maxPitch = 89.9f;

	// Token: 0x0400057F RID: 1407
	[SerializeField]
	private float _minPitch = -89.9f;

	// Token: 0x04000580 RID: 1408
	[NonSerialized]
	private global::CharacterDeathSignal signals_death;

	// Token: 0x04000581 RID: 1409
	[NonSerialized]
	private global::CharacterStateSignal signals_state;

	// Token: 0x04000582 RID: 1410
	[NonSerialized]
	public global::CharacterStateFlags stateFlags;

	// Token: 0x02000111 RID: 273
	private static class AddonRegistry<T> where T : global::IDLocalCharacterAddon, new()
	{
		// Token: 0x04000583 RID: 1411
		public static readonly bool valid = global::Character.AddonRegistry.Validate(typeof(T));
	}

	// Token: 0x02000112 RID: 274
	private static class AddonRegistry
	{
		// Token: 0x0600071C RID: 1820 RVA: 0x00020440 File Offset: 0x0001E640
		public static bool Validate(Type type)
		{
			if (type == null)
			{
				return false;
			}
			bool flag;
			if (!global::Character.AddonRegistry.validatedCache.TryGetValue(type, out flag))
			{
				if (!typeof(global::IDLocalCharacterAddon).IsAssignableFrom(type))
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
				global::Character.AddonRegistry.validatedCache[type] = flag;
			}
			return flag;
		}

		// Token: 0x04000584 RID: 1412
		private static readonly Dictionary<Type, bool> validatedCache = new Dictionary<Type, bool>();
	}

	// Token: 0x02000113 RID: 275
	private static class AddonStringRegistry
	{
		// Token: 0x0600071E RID: 1822 RVA: 0x00020518 File Offset: 0x0001E718
		private static bool Validate(string typeName, out Type type)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				type = null;
				return false;
			}
			global::Character.AddonStringRegistry.TypePair typePair;
			if (!global::Character.AddonStringRegistry.validatedCache.TryGetValue(typeName, out typePair))
			{
				bool flag = global::TypeUtility.TryParse(typeName, out type) && global::Character.AddonRegistry.Validate(type);
				if (!flag)
				{
					foreach (string str in global::Character.AddonStringRegistry.assemblyStrings)
					{
						if (global::TypeUtility.TryParse(typeName + str, out type) && global::Character.AddonRegistry.Validate(type))
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
				global::Character.AddonStringRegistry.validatedCache[typeName] = new global::Character.AddonStringRegistry.TypePair(type, flag);
				return flag;
			}
			type = typePair.type;
			return typePair.valid;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x000205EC File Offset: 0x0001E7EC
		public static bool Validate(string typeName)
		{
			Type type;
			return global::Character.AddonStringRegistry.Validate(typeName, out type);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00020604 File Offset: 0x0001E804
		public static bool Validate<TBase>(string typeName)
		{
			Type c;
			return global::Character.AddonStringRegistry.Validate(typeName, out c) && typeof(TBase).IsAssignableFrom(c);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00020634 File Offset: 0x0001E834
		public static bool Validate<TBase>(string typeName, out Type type)
		{
			return global::Character.AddonStringRegistry.Validate(typeName, out type) && typeof(TBase).IsAssignableFrom(type);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00020664 File Offset: 0x0001E864
		public static bool Validate(string typeName, Type minimumType)
		{
			Type c;
			return global::Character.AddonStringRegistry.Validate(typeName, out c) && minimumType.IsAssignableFrom(c);
		}

		// Token: 0x04000585 RID: 1413
		private static readonly Dictionary<string, global::Character.AddonStringRegistry.TypePair> validatedCache = new Dictionary<string, global::Character.AddonStringRegistry.TypePair>();

		// Token: 0x04000586 RID: 1414
		private static readonly string[] assemblyStrings = new string[]
		{
			", Assembly-CSharp-firstpass",
			", Assembly-CSharp"
		};

		// Token: 0x02000114 RID: 276
		private struct TypePair
		{
			// Token: 0x06000723 RID: 1827 RVA: 0x00020688 File Offset: 0x0001E888
			public TypePair(Type type, bool valid)
			{
				this.type = type;
				this.valid = valid;
			}

			// Token: 0x04000587 RID: 1415
			public readonly Type type;

			// Token: 0x04000588 RID: 1416
			public readonly bool valid;
		}
	}
}
