using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using uLink;
using UnityEngine;

// Token: 0x02000154 RID: 340
public abstract class Controller : global::IDLocalCharacterAddon
{
	// Token: 0x0600099F RID: 2463 RVA: 0x00028E20 File Offset: 0x00027020
	protected Controller(global::Controller.ControllerFlags controllerFlags) : this(controllerFlags, (global::IDLocalCharacterAddon.AddonFlags)0)
	{
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x00028E2C File Offset: 0x0002702C
	protected Controller(global::Controller.ControllerFlags controllerFlags, global::IDLocalCharacterAddon.AddonFlags addonFlags) : base(addonFlags)
	{
		this.controllerFlags = controllerFlags;
	}

	// Token: 0x17000249 RID: 585
	// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00028E3C File Offset: 0x0002703C
	public new bool controlled
	{
		get
		{
			return this._controllable.controlled;
		}
	}

	// Token: 0x1700024A RID: 586
	// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00028E4C File Offset: 0x0002704C
	public new bool playerControlled
	{
		get
		{
			return this._controllable.playerControlled;
		}
	}

	// Token: 0x1700024B RID: 587
	// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00028E5C File Offset: 0x0002705C
	public new bool aiControlled
	{
		get
		{
			return this._controllable.aiControlled;
		}
	}

	// Token: 0x1700024C RID: 588
	// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00028E6C File Offset: 0x0002706C
	public new bool localPlayerControlled
	{
		get
		{
			return this._controllable.localPlayerControlled;
		}
	}

	// Token: 0x1700024D RID: 589
	// (get) Token: 0x060009A5 RID: 2469 RVA: 0x00028E7C File Offset: 0x0002707C
	public new bool remotePlayerControlled
	{
		get
		{
			return this._controllable.remotePlayerControlled;
		}
	}

	// Token: 0x1700024E RID: 590
	// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00028E8C File Offset: 0x0002708C
	public new bool localAIControlled
	{
		get
		{
			return this._controllable.localAIControlled;
		}
	}

	// Token: 0x1700024F RID: 591
	// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00028E9C File Offset: 0x0002709C
	public new bool remoteAIControlled
	{
		get
		{
			return this._controllable.remoteAIControlled;
		}
	}

	// Token: 0x17000250 RID: 592
	// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00028EAC File Offset: 0x000270AC
	public new bool localControlled
	{
		get
		{
			return this._controllable.localControlled;
		}
	}

	// Token: 0x17000251 RID: 593
	// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00028EBC File Offset: 0x000270BC
	public new bool remoteControlled
	{
		get
		{
			return this._controllable.remoteControlled;
		}
	}

	// Token: 0x17000252 RID: 594
	// (get) Token: 0x060009AA RID: 2474 RVA: 0x00028ECC File Offset: 0x000270CC
	public new global::PlayerClient playerClient
	{
		get
		{
			return this._controllable.playerClient;
		}
	}

	// Token: 0x17000253 RID: 595
	// (get) Token: 0x060009AB RID: 2475 RVA: 0x00028EDC File Offset: 0x000270DC
	public new global::Controller controller
	{
		get
		{
			return this;
		}
	}

	// Token: 0x17000254 RID: 596
	// (get) Token: 0x060009AC RID: 2476 RVA: 0x00028EE0 File Offset: 0x000270E0
	public new global::Controller controlledController
	{
		get
		{
			return (!this._controllable.controlled) ? null : this;
		}
	}

	// Token: 0x17000255 RID: 597
	// (get) Token: 0x060009AD RID: 2477 RVA: 0x00028EFC File Offset: 0x000270FC
	public new global::Controller localPlayerControlledController
	{
		get
		{
			return this._controllable.localPlayerControlledController;
		}
	}

	// Token: 0x17000256 RID: 598
	// (get) Token: 0x060009AE RID: 2478 RVA: 0x00028F0C File Offset: 0x0002710C
	public new global::Controller remotePlayerControlledController
	{
		get
		{
			return this._controllable.remotePlayerControlledController;
		}
	}

	// Token: 0x17000257 RID: 599
	// (get) Token: 0x060009AF RID: 2479 RVA: 0x00028F1C File Offset: 0x0002711C
	public new global::Controller localAIControlledController
	{
		get
		{
			return this._controllable.localAIControlledController;
		}
	}

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00028F2C File Offset: 0x0002712C
	public new global::Controller remoteAIControlledController
	{
		get
		{
			return this._controllable.remoteAIControlledController;
		}
	}

	// Token: 0x17000259 RID: 601
	// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00028F3C File Offset: 0x0002713C
	public new global::Controllable controllable
	{
		get
		{
			return this._controllable;
		}
	}

	// Token: 0x1700025A RID: 602
	// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00028F44 File Offset: 0x00027144
	public new global::Controllable controlledControllable
	{
		get
		{
			return (!this._controllable.controlled) ? null : this._controllable;
		}
	}

	// Token: 0x1700025B RID: 603
	// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00028F64 File Offset: 0x00027164
	public new global::Controllable localPlayerControlledControllable
	{
		get
		{
			return this._controllable.localPlayerControlledControllable;
		}
	}

	// Token: 0x1700025C RID: 604
	// (get) Token: 0x060009B4 RID: 2484 RVA: 0x00028F74 File Offset: 0x00027174
	public new global::Controllable remotePlayerControlledControllable
	{
		get
		{
			return this._controllable.remotePlayerControlledControllable;
		}
	}

	// Token: 0x1700025D RID: 605
	// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00028F84 File Offset: 0x00027184
	public new global::Controllable localAIControlledControllable
	{
		get
		{
			return this._controllable.localAIControlledControllable;
		}
	}

	// Token: 0x1700025E RID: 606
	// (get) Token: 0x060009B6 RID: 2486 RVA: 0x00028F94 File Offset: 0x00027194
	public new global::Controllable remoteAIControlledControllable
	{
		get
		{
			return this._controllable.remoteAIControlledControllable;
		}
	}

	// Token: 0x1700025F RID: 607
	// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00028FA4 File Offset: 0x000271A4
	public new bool controlOverridden
	{
		get
		{
			return this._controllable.controlOverridden;
		}
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x00028FB4 File Offset: 0x000271B4
	public new bool ControlOverriddenBy(global::Controllable controllable)
	{
		return this._controllable.ControlOverriddenBy(controllable);
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x00028FC4 File Offset: 0x000271C4
	public new bool ControlOverriddenBy(global::Controller controller)
	{
		return this._controllable.ControlOverriddenBy(controller);
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x00028FD4 File Offset: 0x000271D4
	public new bool ControlOverriddenBy(global::Character character)
	{
		return this._controllable.ControlOverriddenBy(character);
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x00028FE4 File Offset: 0x000271E4
	public new bool ControlOverriddenBy(IDMain main)
	{
		return this._controllable.ControlOverriddenBy(main);
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x00028FF4 File Offset: 0x000271F4
	public new bool ControlOverriddenBy(IDBase idBase)
	{
		return this._controllable.ControlOverriddenBy(idBase);
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x00029004 File Offset: 0x00027204
	public new bool ControlOverriddenBy(global::IDLocalCharacter idLocal)
	{
		return this._controllable.ControlOverriddenBy(idLocal);
	}

	// Token: 0x17000260 RID: 608
	// (get) Token: 0x060009BE RID: 2494 RVA: 0x00029014 File Offset: 0x00027214
	public new bool overridingControl
	{
		get
		{
			return this._controllable.overridingControl;
		}
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x00029024 File Offset: 0x00027224
	public new bool OverridingControlOf(global::Controllable controllable)
	{
		return this._controllable.OverridingControlOf(controllable);
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x00029034 File Offset: 0x00027234
	public new bool OverridingControlOf(global::Controller controller)
	{
		return this._controllable.OverridingControlOf(controller);
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x00029044 File Offset: 0x00027244
	public new bool OverridingControlOf(global::Character character)
	{
		return this._controllable.OverridingControlOf(character);
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x00029054 File Offset: 0x00027254
	public new bool OverridingControlOf(IDMain main)
	{
		return this._controllable.OverridingControlOf(main);
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x00029064 File Offset: 0x00027264
	public new bool OverridingControlOf(IDBase idBase)
	{
		return this._controllable.OverridingControlOf(idBase);
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x00029074 File Offset: 0x00027274
	public new bool OverridingControlOf(global::IDLocalCharacter idLocal)
	{
		return this._controllable.OverridingControlOf(idLocal);
	}

	// Token: 0x17000261 RID: 609
	// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00029084 File Offset: 0x00027284
	public new bool assignedControl
	{
		get
		{
			return this._controllable.assignedControl;
		}
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x00029094 File Offset: 0x00027294
	public new bool AssignedControlOf(global::Controllable controllable)
	{
		return this._controllable.AssignedControlOf(controllable);
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x000290A4 File Offset: 0x000272A4
	public new bool AssignedControlOf(global::Controller controller)
	{
		return this._controllable.AssignedControlOf(controller);
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x000290B4 File Offset: 0x000272B4
	public new bool AssignedControlOf(IDMain character)
	{
		return this._controllable.AssignedControlOf(character);
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x000290C4 File Offset: 0x000272C4
	public new bool AssignedControlOf(IDBase idBase)
	{
		return this._controllable.AssignedControlOf(idBase);
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x000290D4 File Offset: 0x000272D4
	public new global::RelativeControl RelativeControlTo(global::Controllable controllable)
	{
		return this._controllable.RelativeControlTo(controllable);
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x000290E4 File Offset: 0x000272E4
	public new global::RelativeControl RelativeControlTo(global::Controller controller)
	{
		return this._controllable.RelativeControlTo(controller);
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x000290F4 File Offset: 0x000272F4
	public new global::RelativeControl RelativeControlTo(global::Character character)
	{
		return this._controllable.RelativeControlTo(character);
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x00029104 File Offset: 0x00027304
	public new global::RelativeControl RelativeControlTo(IDMain main)
	{
		return this._controllable.RelativeControlTo(main);
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x00029114 File Offset: 0x00027314
	public new global::RelativeControl RelativeControlTo(global::IDLocalCharacter idLocal)
	{
		return this._controllable.RelativeControlTo(idLocal);
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x00029124 File Offset: 0x00027324
	public new global::RelativeControl RelativeControlTo(IDBase idBase)
	{
		return this._controllable.RelativeControlTo(idBase);
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x00029134 File Offset: 0x00027334
	public new global::RelativeControl RelativeControlFrom(global::Controllable controllable)
	{
		return this._controllable.RelativeControlFrom(controllable);
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x00029144 File Offset: 0x00027344
	public new global::RelativeControl RelativeControlFrom(global::Controller controller)
	{
		return this._controllable.RelativeControlFrom(controller);
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x00029154 File Offset: 0x00027354
	public new global::RelativeControl RelativeControlFrom(global::Character character)
	{
		return this._controllable.RelativeControlFrom(character);
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x00029164 File Offset: 0x00027364
	public new global::RelativeControl RelativeControlFrom(IDMain main)
	{
		return this._controllable.RelativeControlFrom(main);
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x00029174 File Offset: 0x00027374
	public new global::RelativeControl RelativeControlFrom(global::IDLocalCharacter idLocal)
	{
		return this._controllable.RelativeControlFrom(idLocal);
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x00029184 File Offset: 0x00027384
	public new global::RelativeControl RelativeControlFrom(IDBase idBase)
	{
		return this._controllable.RelativeControlFrom(idBase);
	}

	// Token: 0x17000262 RID: 610
	// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00029194 File Offset: 0x00027394
	public new global::Controllable masterControllable
	{
		get
		{
			return this._controllable.masterControllable;
		}
	}

	// Token: 0x17000263 RID: 611
	// (get) Token: 0x060009D7 RID: 2519 RVA: 0x000291A4 File Offset: 0x000273A4
	public new global::Controllable rootControllable
	{
		get
		{
			return this._controllable.rootControllable;
		}
	}

	// Token: 0x17000264 RID: 612
	// (get) Token: 0x060009D8 RID: 2520 RVA: 0x000291B4 File Offset: 0x000273B4
	public new global::Controllable nextControllable
	{
		get
		{
			return this._controllable.nextControllable;
		}
	}

	// Token: 0x17000265 RID: 613
	// (get) Token: 0x060009D9 RID: 2521 RVA: 0x000291C4 File Offset: 0x000273C4
	public new global::Controllable previousControllable
	{
		get
		{
			return this._controllable.previousControllable;
		}
	}

	// Token: 0x17000266 RID: 614
	// (get) Token: 0x060009DA RID: 2522 RVA: 0x000291D4 File Offset: 0x000273D4
	public new global::Character previousCharacter
	{
		get
		{
			return this._controllable.previousCharacter;
		}
	}

	// Token: 0x17000267 RID: 615
	// (get) Token: 0x060009DB RID: 2523 RVA: 0x000291E4 File Offset: 0x000273E4
	public new global::Character rootCharacter
	{
		get
		{
			return this._controllable.rootCharacter;
		}
	}

	// Token: 0x17000268 RID: 616
	// (get) Token: 0x060009DC RID: 2524 RVA: 0x000291F4 File Offset: 0x000273F4
	public new global::Character nextCharacter
	{
		get
		{
			return this._controllable.nextCharacter;
		}
	}

	// Token: 0x17000269 RID: 617
	// (get) Token: 0x060009DD RID: 2525 RVA: 0x00029204 File Offset: 0x00027404
	public new global::Character masterCharacter
	{
		get
		{
			return this._controllable.masterCharacter;
		}
	}

	// Token: 0x1700026A RID: 618
	// (get) Token: 0x060009DE RID: 2526 RVA: 0x00029214 File Offset: 0x00027414
	public new global::Controller masterController
	{
		get
		{
			return this._controllable.masterController;
		}
	}

	// Token: 0x1700026B RID: 619
	// (get) Token: 0x060009DF RID: 2527 RVA: 0x00029224 File Offset: 0x00027424
	public new global::Controller rootController
	{
		get
		{
			return this._controllable.rootController;
		}
	}

	// Token: 0x1700026C RID: 620
	// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00029234 File Offset: 0x00027434
	public new global::Controller nextController
	{
		get
		{
			return this._controllable.nextController;
		}
	}

	// Token: 0x1700026D RID: 621
	// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00029244 File Offset: 0x00027444
	public new global::Controller previousController
	{
		get
		{
			return this._controllable.previousController;
		}
	}

	// Token: 0x1700026E RID: 622
	// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00029254 File Offset: 0x00027454
	public new int controlDepth
	{
		get
		{
			return this._controllable.controlDepth;
		}
	}

	// Token: 0x1700026F RID: 623
	// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00029264 File Offset: 0x00027464
	public new int controlCount
	{
		get
		{
			return this._controllable.controlCount;
		}
	}

	// Token: 0x17000270 RID: 624
	// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00029274 File Offset: 0x00027474
	public new string controllerClassName
	{
		get
		{
			return this._controllable.controllerClassName;
		}
	}

	// Token: 0x17000271 RID: 625
	// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00029284 File Offset: 0x00027484
	public new string npcName
	{
		get
		{
			return this._controllable.npcName;
		}
	}

	// Token: 0x17000272 RID: 626
	// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00029294 File Offset: 0x00027494
	// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0002929C File Offset: 0x0002749C
	public new global::RPOSLimitFlags rposLimitFlags
	{
		get
		{
			return this._rposLimitFlags;
		}
		protected internal set
		{
			this._rposLimitFlags = value;
		}
	}

	// Token: 0x17000273 RID: 627
	// (get) Token: 0x060009E8 RID: 2536 RVA: 0x000292A8 File Offset: 0x000274A8
	// (set) Token: 0x060009E9 RID: 2537 RVA: 0x000292B0 File Offset: 0x000274B0
	public bool forwardsPlayerClientInput
	{
		get
		{
			return this._forwardsPlayerClientInput;
		}
		protected set
		{
			this._forwardsPlayerClientInput = value;
		}
	}

	// Token: 0x17000274 RID: 628
	// (get) Token: 0x060009EA RID: 2538 RVA: 0x000292BC File Offset: 0x000274BC
	// (set) Token: 0x060009EB RID: 2539 RVA: 0x000292C4 File Offset: 0x000274C4
	public bool doesNotSave
	{
		get
		{
			return this._doesNotSave;
		}
		protected set
		{
			this._doesNotSave = value;
		}
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x000292D0 File Offset: 0x000274D0
	protected virtual void OnControllerSetup(uLink.NetworkView networkView, ref uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x000292D4 File Offset: 0x000274D4
	internal void ControllerSetup(global::Controllable controllable, uLink.NetworkView view, ref uLink.NetworkMessageInfo info)
	{
		if (this.wasSetup)
		{
			throw new InvalidOperationException("Already was setup");
		}
		this.wasSetup = true;
		global::Controller.ControllerFlags controllerFlags = this.controllerFlags & global::Controller.ControllerFlags.DontMessWithEnabled;
		bool flag;
		if (controllerFlags != global::Controller.ControllerFlags.AlwaysSavedAsDisabled)
		{
			if (controllerFlags != global::Controller.ControllerFlags.AlwaysSavedAsEnabled)
			{
				flag = (controllerFlags == global::Controller.ControllerFlags.DontMessWithEnabled);
			}
			else
			{
				flag = false;
				if (!base.enabled)
				{
					base.enabled = true;
					Debug.LogError("this was not saved as disabled", this);
				}
			}
		}
		else
		{
			flag = false;
			if (base.enabled)
			{
				base.enabled = false;
				Debug.LogError("this was not saved as enabled", this);
			}
		}
		this._controllable = controllable;
		if (this.playerControlled)
		{
			if (this.localPlayerControlled)
			{
				if ((this.controllerFlags & global::Controller.ControllerFlags.IncompatibleAsLocalPlayer) == global::Controller.ControllerFlags.IncompatibleAsLocalPlayer)
				{
					throw new NotSupportedException();
				}
			}
			else if ((this.controllerFlags & global::Controller.ControllerFlags.IncompatibleAsRemotePlayer) == global::Controller.ControllerFlags.IncompatibleAsRemotePlayer)
			{
				throw new NotSupportedException();
			}
		}
		else if (this.localAIControlled)
		{
			if ((this.controllerFlags & global::Controller.ControllerFlags.IncompatibleAsLocalAI) == global::Controller.ControllerFlags.IncompatibleAsLocalAI)
			{
				throw new NotSupportedException();
			}
		}
		else if ((this.controllerFlags & global::Controller.ControllerFlags.IncompatibleAsRemoteAI) == global::Controller.ControllerFlags.IncompatibleAsRemoteAI)
		{
			throw new NotSupportedException();
		}
		this.OnControllerSetup(view, ref info);
		if (!flag)
		{
			global::Controller.ControllerFlags controllerFlags2;
			global::Controller.ControllerFlags controllerFlags3;
			if (this.playerControlled)
			{
				if (this.localPlayerControlled)
				{
					controllerFlags2 = global::Controller.ControllerFlags.EnableWhenLocalPlayer;
					controllerFlags3 = global::Controller.ControllerFlags.DisableWhenLocalPlayer;
				}
				else
				{
					controllerFlags2 = global::Controller.ControllerFlags.EnableWhenRemotePlayer;
					controllerFlags3 = global::Controller.ControllerFlags.DisableWhenRemotePlayer;
				}
			}
			else if (this.localAIControlled)
			{
				controllerFlags2 = global::Controller.ControllerFlags.EnableWhenLocalAI;
				controllerFlags3 = global::Controller.ControllerFlags.DisableWhenLocalAI;
			}
			else
			{
				controllerFlags2 = global::Controller.ControllerFlags.EnableWhenRemoteAI;
				controllerFlags3 = global::Controller.ControllerFlags.DisableWhenRemoteAI;
			}
			if ((this.controllerFlags & controllerFlags2) == controllerFlags2)
			{
				if ((this.controllerFlags & controllerFlags3) == controllerFlags3)
				{
					base.enabled = !base.enabled;
				}
				else
				{
					base.enabled = true;
				}
			}
			else if ((this.controllerFlags & controllerFlags3) == controllerFlags3)
			{
				base.enabled = false;
			}
		}
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x000294D4 File Offset: 0x000276D4
	protected virtual void OnLocalPlayerInputFrame()
	{
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x000294D8 File Offset: 0x000276D8
	internal void ProcessLocalPlayerInput()
	{
		this.OnLocalPlayerInputFrame();
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x000294E0 File Offset: 0x000276E0
	protected virtual void OnLocalPlayerPreRender()
	{
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x000294E4 File Offset: 0x000276E4
	internal void ProcessLocalPlayerPreRender()
	{
		this.OnLocalPlayerPreRender();
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x000294EC File Offset: 0x000276EC
	protected virtual void OnControlEnter()
	{
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x000294F0 File Offset: 0x000276F0
	protected virtual void OnControlEngauge()
	{
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x000294F4 File Offset: 0x000276F4
	protected virtual void OnControlCease()
	{
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x000294F8 File Offset: 0x000276F8
	protected virtual void OnControlExit()
	{
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x000294FC File Offset: 0x000276FC
	[Obsolete("Used only by Controllable")]
	internal void ControlEnter(int cmd)
	{
		global::Controller.Commandment commandment = this.commandment;
		this.commandment = new global::Controller.Commandment((cmd & 32767) | 32768);
		try
		{
			this.OnControlEnter();
		}
		finally
		{
			this.commandment = commandment;
		}
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x00029558 File Offset: 0x00027758
	[Obsolete("Used only by Controllable")]
	internal void ControlExit(int cmd)
	{
		global::Controller.Commandment commandment = this.commandment;
		this.commandment = new global::Controller.Commandment((cmd & 32767) | 131072);
		try
		{
			this.OnControlExit();
		}
		finally
		{
			this.commandment = commandment;
		}
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x000295B4 File Offset: 0x000277B4
	[Obsolete("Used only by Controllable")]
	internal void ControlEngauge(int cmd)
	{
		global::Controller.Commandment commandment = this.commandment;
		this.commandment = new global::Controller.Commandment((cmd & 32767) | 65536);
		try
		{
			this.OnControlEngauge();
		}
		finally
		{
			this.commandment = commandment;
		}
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x00029610 File Offset: 0x00027810
	[Obsolete("Used only by Controllable")]
	internal void ControlCease(int cmd)
	{
		global::Controller.Commandment commandment = this.commandment;
		this.commandment = new global::Controller.Commandment((cmd & 32767) | 98304);
		try
		{
			this.OnControlCease();
		}
		finally
		{
			this.commandment = commandment;
		}
	}

	// Token: 0x17000275 RID: 629
	// (get) Token: 0x060009FA RID: 2554 RVA: 0x0002966C File Offset: 0x0002786C
	public static IEnumerable<global::Controller> PlayerRootControllers
	{
		get
		{
			foreach (global::PlayerClient pc in global::PlayerClient.All)
			{
				global::Controllable controllable = pc.rootControllable;
				if (controllable)
				{
					global::Controller controller = controllable.controller;
					if (controller)
					{
						yield return controllable.controller;
					}
				}
			}
			yield break;
		}
	}

	// Token: 0x17000276 RID: 630
	// (get) Token: 0x060009FB RID: 2555 RVA: 0x00029688 File Offset: 0x00027888
	public static IEnumerable<global::Controller> PlayerCurrentControllers
	{
		get
		{
			foreach (global::PlayerClient pc in global::PlayerClient.All)
			{
				global::Controllable controllable = pc.controllable;
				if (controllable)
				{
					global::Controller controller = controllable.controller;
					if (controller)
					{
						yield return controllable.controller;
					}
				}
			}
			yield break;
		}
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x000296A4 File Offset: 0x000278A4
	public static IEnumerable<global::Controller> RootControllers(IEnumerable<global::PlayerClient> playerClients)
	{
		foreach (global::PlayerClient pc in playerClients)
		{
			global::Controllable controllable = pc.rootControllable;
			if (controllable)
			{
				global::Controller controller = controllable.controller;
				if (controller)
				{
					yield return controller;
				}
			}
		}
		yield break;
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x000296D0 File Offset: 0x000278D0
	public static IEnumerable<global::Controller> CurrentControllers(IEnumerable<global::PlayerClient> playerClients)
	{
		foreach (global::PlayerClient pc in playerClients)
		{
			global::Controllable controllable = pc.controllable;
			if (controllable)
			{
				global::Controller controller = controllable.controller;
				if (controller)
				{
					yield return controller;
				}
			}
		}
		yield break;
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x000296FC File Offset: 0x000278FC
	public static IEnumerable<TController> RootControllers<TController>(IEnumerable<global::PlayerClient> playerClients) where TController : global::Controller
	{
		foreach (global::PlayerClient pc in playerClients)
		{
			global::Controllable controllable = pc.rootControllable;
			if (controllable)
			{
				TController controller = controllable.controller as TController;
				if (controller)
				{
					yield return controller;
				}
			}
		}
		yield break;
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x00029728 File Offset: 0x00027928
	public static IEnumerable<TController> CurrentControllers<TController>(IEnumerable<global::PlayerClient> playerClients) where TController : global::Controller
	{
		foreach (global::PlayerClient pc in playerClients)
		{
			global::Controllable controllable = pc.controllable;
			if (controllable)
			{
				TController controller = controllable.controller as TController;
				if (controller)
				{
					yield return controller;
				}
			}
		}
		yield break;
	}

	// Token: 0x040006E1 RID: 1761
	[NonSerialized]
	private global::Controllable _controllable;

	// Token: 0x040006E2 RID: 1762
	[NonSerialized]
	private readonly global::Controller.ControllerFlags controllerFlags;

	// Token: 0x040006E3 RID: 1763
	[NonSerialized]
	private global::RPOSLimitFlags _rposLimitFlags;

	// Token: 0x040006E4 RID: 1764
	[NonSerialized]
	private bool wasSetup;

	// Token: 0x040006E5 RID: 1765
	[NonSerialized]
	private bool _forwardsPlayerClientInput;

	// Token: 0x040006E6 RID: 1766
	[NonSerialized]
	private bool _doesNotSave;

	// Token: 0x040006E7 RID: 1767
	[NonSerialized]
	protected global::Controller.Commandment commandment;

	// Token: 0x02000155 RID: 341
	protected enum ControllerFlags
	{
		// Token: 0x040006E9 RID: 1769
		EnableWhenLocalPlayer = 1,
		// Token: 0x040006EA RID: 1770
		EnableWhenLocalAI,
		// Token: 0x040006EB RID: 1771
		EnableWhenRemotePlayer = 4,
		// Token: 0x040006EC RID: 1772
		EnableWhenRemoteAI = 8,
		// Token: 0x040006ED RID: 1773
		DisableWhenLocalPlayer = 16,
		// Token: 0x040006EE RID: 1774
		DisableWhenLocalAI = 32,
		// Token: 0x040006EF RID: 1775
		DisableWhenRemotePlayer = 64,
		// Token: 0x040006F0 RID: 1776
		DisableWhenRemoteAI = 128,
		// Token: 0x040006F1 RID: 1777
		ToggleEnableWhenLocalPlayer = 17,
		// Token: 0x040006F2 RID: 1778
		ToggleEnableLocalAI = 34,
		// Token: 0x040006F3 RID: 1779
		ToggleEnableRemotePlayer = 68,
		// Token: 0x040006F4 RID: 1780
		ToggleEnableRemoteAI = 136,
		// Token: 0x040006F5 RID: 1781
		AlwaysSavedAsDisabled = 256,
		// Token: 0x040006F6 RID: 1782
		AlwaysSavedAsEnabled = 512,
		// Token: 0x040006F7 RID: 1783
		DontMessWithEnabled = 768,
		// Token: 0x040006F8 RID: 1784
		IncompatibleAsRemoteAI = 1024,
		// Token: 0x040006F9 RID: 1785
		IncompatibleAsRemotePlayer = 2048,
		// Token: 0x040006FA RID: 1786
		IncompatibleAsLocalPlayer = 4096,
		// Token: 0x040006FB RID: 1787
		IncompatibleAsLocalAI = 8192
	}

	// Token: 0x02000156 RID: 342
	[StructLayout(LayoutKind.Sequential, Size = 4)]
	protected internal struct Commandment
	{
		// Token: 0x06000A00 RID: 2560 RVA: 0x00029754 File Offset: 0x00027954
		internal Commandment(int f)
		{
			this.f = (f & 262143);
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00029764 File Offset: 0x00027964
		public bool thisDestroying
		{
			get
			{
				return (this.f & 1) == 1;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00029774 File Offset: 0x00027974
		public bool baseDestroying
		{
			get
			{
				return (this.f & 2) == 2;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00029784 File Offset: 0x00027984
		public bool rootDestroying
		{
			get
			{
				return (this.f & 4) == 4;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00029794 File Offset: 0x00027994
		public bool baseExit
		{
			get
			{
				return (this.f & 32) == 32;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x000297A4 File Offset: 0x000279A4
		public bool thisExit
		{
			get
			{
				return (this.f & 16) == 16;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x000297B4 File Offset: 0x000279B4
		public bool rootExit
		{
			get
			{
				return (this.f & 64) == 64;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x000297C4 File Offset: 0x000279C4
		public bool networkValid
		{
			get
			{
				return (this.f & 8) == 0;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x000297D4 File Offset: 0x000279D4
		public bool networkInvalid
		{
			get
			{
				return (this.f & 8) == 8;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x000297E4 File Offset: 0x000279E4
		public bool overrideThis
		{
			get
			{
				return (this.f & 128) == 128;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x000297FC File Offset: 0x000279FC
		public bool overrideBase
		{
			get
			{
				return (this.f & 256) == 256;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x00029814 File Offset: 0x00027A14
		public bool overrideRoot
		{
			get
			{
				return (this.f & 512) == 512;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0002982C File Offset: 0x00027A2C
		public bool ownerServer
		{
			get
			{
				return (this.f & 8192) == 0;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00029840 File Offset: 0x00027A40
		public bool ownerClient
		{
			get
			{
				return (this.f & 8192) == 8192;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00029858 File Offset: 0x00027A58
		public bool runningLocally
		{
			get
			{
				return (this.f & 16384) == 16384;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x00029870 File Offset: 0x00027A70
		public bool runningRemotely
		{
			get
			{
				return (this.f & 16384) == 0;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x00029884 File Offset: 0x00027A84
		public bool callFirst
		{
			get
			{
				return (this.f & 1024) == 0;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00029898 File Offset: 0x00027A98
		public bool callAgain
		{
			get
			{
				return (this.f & 1024) == 1024;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x000298B0 File Offset: 0x00027AB0
		public bool bindWeak
		{
			get
			{
				return (this.f & 4096) == 0;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x000298C4 File Offset: 0x00027AC4
		public bool bindStrong
		{
			get
			{
				return (this.f & 4096) == 4096;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x000298DC File Offset: 0x00027ADC
		public bool kindRoot
		{
			get
			{
				return (this.f & 2048) == 2048;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x000298F4 File Offset: 0x00027AF4
		public bool kindVessel
		{
			get
			{
				return (this.f & 2048) == 0;
			}
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00029908 File Offset: 0x00027B08
		public override string ToString()
		{
			if ((this.f & 229376) != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = this.f & 112;
				switch (num)
				{
				case 16:
					stringBuilder.Append("exit[THIS]");
					break;
				default:
					if (num != 32)
					{
						if (num != 36)
						{
							if (num != 64)
							{
								if (num == 112)
								{
									stringBuilder.Append("exit[ALL]");
								}
							}
							else
							{
								stringBuilder.Append("exit[ROOT]");
							}
						}
						else
						{
							stringBuilder.Append("exit[BASE,ROOT]");
						}
					}
					else
					{
						stringBuilder.Append("exit[BASE]");
					}
					break;
				case 18:
					stringBuilder.Append("exit[THIS,BASE]");
					break;
				case 20:
					stringBuilder.Append("exit[THIS,ROOT]");
					break;
				}
				num = (this.f & 896);
				switch (num)
				{
				case 128:
					stringBuilder.Append("override[THIS]");
					break;
				default:
					if (num != 256)
					{
						if (num != 260)
						{
							if (num != 512)
							{
								if (num == 896)
								{
									stringBuilder.Append("override[ALL]");
								}
							}
							else
							{
								stringBuilder.Append("override[ROOT]");
							}
						}
						else
						{
							stringBuilder.Append("override[BASE,ROOT]");
						}
					}
					else
					{
						stringBuilder.Append("override[BASE]");
					}
					break;
				case 130:
					stringBuilder.Append("override[THIS,BASE]");
					break;
				case 132:
					stringBuilder.Append("override[THIS,ROOT]");
					break;
				}
				num = (this.f & 2048);
				if (num != 0)
				{
					if (num == 2048)
					{
						stringBuilder.Append("kind[ROOT]");
					}
				}
				else
				{
					stringBuilder.Append("kind[VESL]");
				}
				num = (this.f & 4096);
				if (num != 0)
				{
					if (num == 4096)
					{
						stringBuilder.Append("bind[STRONG]");
					}
				}
				else
				{
					stringBuilder.Append("bind[WEAK]");
				}
				num = (this.f & 8192);
				if (num != 0)
				{
					if (num == 8192)
					{
						stringBuilder.Append("client[");
					}
				}
				else
				{
					stringBuilder.Append("server[");
				}
				num = (this.f & 16384);
				if (num != 0)
				{
					if (num == 16384)
					{
						stringBuilder.Append("LOCAL]");
					}
				}
				else
				{
					stringBuilder.Append("RMOTE]");
				}
				num = (this.f & 8);
				if (num != 0)
				{
					if (num == 8)
					{
						stringBuilder.Append("net[NOO]");
					}
				}
				else
				{
					stringBuilder.Append("net[YES]");
				}
				switch (this.f & 7)
				{
				case 1:
					stringBuilder.Append("destroy[THIS]");
					break;
				case 2:
					stringBuilder.Append("destroy[BASE]");
					break;
				case 3:
					stringBuilder.Append("destroy[THIS,BASE]");
					break;
				case 4:
					stringBuilder.Append("destroy[ROOT]");
					break;
				case 5:
					stringBuilder.Append("destroy[THIS,ROOT]");
					break;
				case 6:
					stringBuilder.Append("destroy[BASE,ROOT]");
					break;
				case 7:
					stringBuilder.Append("destroy[ALL]");
					break;
				}
				num = (this.f & 229376);
				if (num != 32768)
				{
					if (num != 65536)
					{
						if (num != 98304)
						{
							if (num == 131072)
							{
								stringBuilder.Append("->EXIT");
							}
						}
						else
						{
							stringBuilder.Append("->DEMO");
						}
					}
					else
					{
						stringBuilder.Append("->PRMO");
					}
				}
				else
				{
					stringBuilder.Append("->ENTR");
				}
				if ((this.f & 1024) == 0)
				{
					stringBuilder.Append("(first)");
				}
				return stringBuilder.ToString();
			}
			return "INVALID";
		}

		// Token: 0x040006FC RID: 1788
		private const int B = 1;

		// Token: 0x040006FD RID: 1789
		internal const int THIS_TO_BASE = 1;

		// Token: 0x040006FE RID: 1790
		internal const int THIS_TO_ROOT = 2;

		// Token: 0x040006FF RID: 1791
		internal const int ALL = 32767;

		// Token: 0x04000700 RID: 1792
		internal const int ALL_THIS = 145;

		// Token: 0x04000701 RID: 1793
		internal const int ALL_BASE = 290;

		// Token: 0x04000702 RID: 1794
		internal const int ALL_ROOT = 580;

		// Token: 0x04000703 RID: 1795
		private readonly int f;

		// Token: 0x02000157 RID: 343
		internal static class DESTROY
		{
			// Token: 0x04000704 RID: 1796
			public const int THIS = 1;

			// Token: 0x04000705 RID: 1797
			public const int BASE = 2;

			// Token: 0x04000706 RID: 1798
			public const int ROOT = 4;

			// Token: 0x04000707 RID: 1799
			public const int NONE = 0;

			// Token: 0x04000708 RID: 1800
			public const int ALL = 7;
		}

		// Token: 0x02000158 RID: 344
		internal static class NETWORK
		{
			// Token: 0x04000709 RID: 1801
			public const int VALID = 0;

			// Token: 0x0400070A RID: 1802
			public const int INVALID = 8;

			// Token: 0x0400070B RID: 1803
			public const int ALL = 8;
		}

		// Token: 0x02000159 RID: 345
		internal static class EXIT
		{
			// Token: 0x0400070C RID: 1804
			public const int THIS = 16;

			// Token: 0x0400070D RID: 1805
			public const int BASE = 32;

			// Token: 0x0400070E RID: 1806
			public const int ROOT = 64;

			// Token: 0x0400070F RID: 1807
			public const int NONE = 0;

			// Token: 0x04000710 RID: 1808
			public const int ALL = 112;
		}

		// Token: 0x0200015A RID: 346
		internal static class OVERRIDE
		{
			// Token: 0x04000711 RID: 1809
			public const int THIS = 128;

			// Token: 0x04000712 RID: 1810
			public const int BASE = 256;

			// Token: 0x04000713 RID: 1811
			public const int ROOT = 512;

			// Token: 0x04000714 RID: 1812
			public const int NONE = 0;

			// Token: 0x04000715 RID: 1813
			public const int ALL = 896;
		}

		// Token: 0x0200015B RID: 347
		internal static class ONCE
		{
			// Token: 0x04000716 RID: 1814
			public const int TRUE = 1024;

			// Token: 0x04000717 RID: 1815
			public const int FALSE = 0;

			// Token: 0x04000718 RID: 1816
			public const int ALL = 1024;
		}

		// Token: 0x0200015C RID: 348
		internal static class KIND
		{
			// Token: 0x04000719 RID: 1817
			public const int ROOT = 2048;

			// Token: 0x0400071A RID: 1818
			public const int VESSEL = 0;

			// Token: 0x0400071B RID: 1819
			public const int ALL = 2048;
		}

		// Token: 0x0200015D RID: 349
		internal static class BINDING
		{
			// Token: 0x0400071C RID: 1820
			public const int STRONG = 4096;

			// Token: 0x0400071D RID: 1821
			public const int WEAK = 0;

			// Token: 0x0400071E RID: 1822
			public const int ALL = 4096;
		}

		// Token: 0x0200015E RID: 350
		internal static class OWNER
		{
			// Token: 0x0400071F RID: 1823
			public const int CLIENT = 8192;

			// Token: 0x04000720 RID: 1824
			public const int SERVER = 0;

			// Token: 0x04000721 RID: 1825
			public const int ALL = 8192;
		}

		// Token: 0x0200015F RID: 351
		internal static class PLACE
		{
			// Token: 0x04000722 RID: 1826
			public const int HERE = 16384;

			// Token: 0x04000723 RID: 1827
			public const int ELSEWHERE = 0;

			// Token: 0x04000724 RID: 1828
			public const int ALL = 16384;
		}

		// Token: 0x02000160 RID: 352
		internal static class EVENT
		{
			// Token: 0x04000725 RID: 1829
			public const int NONE = 0;

			// Token: 0x04000726 RID: 1830
			public const int ENTER = 32768;

			// Token: 0x04000727 RID: 1831
			public const int ENGAUGE = 65536;

			// Token: 0x04000728 RID: 1832
			public const int CEASE = 98304;

			// Token: 0x04000729 RID: 1833
			public const int EXIT = 131072;

			// Token: 0x0400072A RID: 1834
			public const int ALL = 229376;
		}
	}
}
