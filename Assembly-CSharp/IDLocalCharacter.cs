using System;
using UnityEngine;

// Token: 0x02000147 RID: 327
public abstract class IDLocalCharacter : IDLocal
{
	// Token: 0x17000274 RID: 628
	// (get) Token: 0x06000953 RID: 2387 RVA: 0x00027820 File Offset: 0x00025A20
	public Character idMain
	{
		get
		{
			return (Character)this.idMain;
		}
	}

	// Token: 0x17000275 RID: 629
	// (get) Token: 0x06000954 RID: 2388 RVA: 0x00027830 File Offset: 0x00025A30
	public Character character
	{
		get
		{
			return (Character)this.idMain;
		}
	}

	// Token: 0x17000276 RID: 630
	// (get) Token: 0x06000955 RID: 2389 RVA: 0x00027840 File Offset: 0x00025A40
	public HitBoxSystem hitBoxSystem
	{
		get
		{
			return this.idMain.hitBoxSystem;
		}
	}

	// Token: 0x17000277 RID: 631
	// (get) Token: 0x06000956 RID: 2390 RVA: 0x00027850 File Offset: 0x00025A50
	public RecoilSimulation recoilSimulation
	{
		get
		{
			return this.idMain.recoilSimulation;
		}
	}

	// Token: 0x17000278 RID: 632
	// (get) Token: 0x06000957 RID: 2391 RVA: 0x00027860 File Offset: 0x00025A60
	public PlayerClient playerClient
	{
		get
		{
			return this.idMain.playerClient;
		}
	}

	// Token: 0x17000279 RID: 633
	// (get) Token: 0x06000958 RID: 2392 RVA: 0x00027870 File Offset: 0x00025A70
	public bool controlled
	{
		get
		{
			return this.idMain.controlled;
		}
	}

	// Token: 0x1700027A RID: 634
	// (get) Token: 0x06000959 RID: 2393 RVA: 0x00027880 File Offset: 0x00025A80
	public bool playerControlled
	{
		get
		{
			return this.idMain.playerControlled;
		}
	}

	// Token: 0x1700027B RID: 635
	// (get) Token: 0x0600095A RID: 2394 RVA: 0x00027890 File Offset: 0x00025A90
	public bool aiControlled
	{
		get
		{
			return this.idMain.aiControlled;
		}
	}

	// Token: 0x1700027C RID: 636
	// (get) Token: 0x0600095B RID: 2395 RVA: 0x000278A0 File Offset: 0x00025AA0
	public bool localPlayerControlled
	{
		get
		{
			return this.idMain.localPlayerControlled;
		}
	}

	// Token: 0x1700027D RID: 637
	// (get) Token: 0x0600095C RID: 2396 RVA: 0x000278B0 File Offset: 0x00025AB0
	public bool remotePlayerControlled
	{
		get
		{
			return this.idMain.remotePlayerControlled;
		}
	}

	// Token: 0x1700027E RID: 638
	// (get) Token: 0x0600095D RID: 2397 RVA: 0x000278C0 File Offset: 0x00025AC0
	public bool localAIControlled
	{
		get
		{
			return this.idMain.localAIControlled;
		}
	}

	// Token: 0x1700027F RID: 639
	// (get) Token: 0x0600095E RID: 2398 RVA: 0x000278D0 File Offset: 0x00025AD0
	public bool remoteAIControlled
	{
		get
		{
			return this.idMain.remoteAIControlled;
		}
	}

	// Token: 0x17000280 RID: 640
	// (get) Token: 0x0600095F RID: 2399 RVA: 0x000278E0 File Offset: 0x00025AE0
	public bool localControlled
	{
		get
		{
			return this.idMain.localControlled;
		}
	}

	// Token: 0x17000281 RID: 641
	// (get) Token: 0x06000960 RID: 2400 RVA: 0x000278F0 File Offset: 0x00025AF0
	public bool remoteControlled
	{
		get
		{
			return this.idMain.remoteControlled;
		}
	}

	// Token: 0x17000282 RID: 642
	// (get) Token: 0x06000961 RID: 2401 RVA: 0x00027900 File Offset: 0x00025B00
	public Controllable controllable
	{
		get
		{
			return this.idMain.controllable;
		}
	}

	// Token: 0x17000283 RID: 643
	// (get) Token: 0x06000962 RID: 2402 RVA: 0x00027910 File Offset: 0x00025B10
	public Controllable controlledControllable
	{
		get
		{
			return this.idMain.controlledControllable;
		}
	}

	// Token: 0x17000284 RID: 644
	// (get) Token: 0x06000963 RID: 2403 RVA: 0x00027920 File Offset: 0x00025B20
	public Controllable playerControlledControllable
	{
		get
		{
			return this.idMain.playerControlledControllable;
		}
	}

	// Token: 0x17000285 RID: 645
	// (get) Token: 0x06000964 RID: 2404 RVA: 0x00027930 File Offset: 0x00025B30
	public Controllable aiControlledControllable
	{
		get
		{
			return this.idMain.aiControlledControllable;
		}
	}

	// Token: 0x17000286 RID: 646
	// (get) Token: 0x06000965 RID: 2405 RVA: 0x00027940 File Offset: 0x00025B40
	public Controllable localPlayerControlledControllable
	{
		get
		{
			return this.idMain.localPlayerControlledControllable;
		}
	}

	// Token: 0x17000287 RID: 647
	// (get) Token: 0x06000966 RID: 2406 RVA: 0x00027950 File Offset: 0x00025B50
	public Controllable localAIControlledControllable
	{
		get
		{
			return this.idMain.localAIControlledControllable;
		}
	}

	// Token: 0x17000288 RID: 648
	// (get) Token: 0x06000967 RID: 2407 RVA: 0x00027960 File Offset: 0x00025B60
	public Controllable remotePlayerControlledControllable
	{
		get
		{
			return this.idMain.remotePlayerControlledControllable;
		}
	}

	// Token: 0x17000289 RID: 649
	// (get) Token: 0x06000968 RID: 2408 RVA: 0x00027970 File Offset: 0x00025B70
	public Controllable remoteAIControlledControllable
	{
		get
		{
			return this.idMain.remoteAIControlledControllable;
		}
	}

	// Token: 0x1700028A RID: 650
	// (get) Token: 0x06000969 RID: 2409 RVA: 0x00027980 File Offset: 0x00025B80
	public string npcName
	{
		get
		{
			return this.idMain.npcName;
		}
	}

	// Token: 0x1700028B RID: 651
	// (get) Token: 0x0600096A RID: 2410 RVA: 0x00027990 File Offset: 0x00025B90
	public Character previousCharacter
	{
		get
		{
			return this.idMain.previousCharacter;
		}
	}

	// Token: 0x1700028C RID: 652
	// (get) Token: 0x0600096B RID: 2411 RVA: 0x000279A0 File Offset: 0x00025BA0
	public Character rootCharacter
	{
		get
		{
			return this.idMain.rootCharacter;
		}
	}

	// Token: 0x1700028D RID: 653
	// (get) Token: 0x0600096C RID: 2412 RVA: 0x000279B0 File Offset: 0x00025BB0
	public Character nextCharacter
	{
		get
		{
			return this.idMain.nextCharacter;
		}
	}

	// Token: 0x1700028E RID: 654
	// (get) Token: 0x0600096D RID: 2413 RVA: 0x000279C0 File Offset: 0x00025BC0
	public Character masterCharacter
	{
		get
		{
			return this.idMain.masterCharacter;
		}
	}

	// Token: 0x1700028F RID: 655
	// (get) Token: 0x0600096E RID: 2414 RVA: 0x000279D0 File Offset: 0x00025BD0
	public Controllable masterControllable
	{
		get
		{
			return this.idMain.masterControllable;
		}
	}

	// Token: 0x17000290 RID: 656
	// (get) Token: 0x0600096F RID: 2415 RVA: 0x000279E0 File Offset: 0x00025BE0
	public Controllable rootControllable
	{
		get
		{
			return this.idMain.rootControllable;
		}
	}

	// Token: 0x17000291 RID: 657
	// (get) Token: 0x06000970 RID: 2416 RVA: 0x000279F0 File Offset: 0x00025BF0
	public Controllable nextControllable
	{
		get
		{
			return this.idMain.nextControllable;
		}
	}

	// Token: 0x17000292 RID: 658
	// (get) Token: 0x06000971 RID: 2417 RVA: 0x00027A00 File Offset: 0x00025C00
	public Controllable previousControllable
	{
		get
		{
			return this.idMain.previousControllable;
		}
	}

	// Token: 0x17000293 RID: 659
	// (get) Token: 0x06000972 RID: 2418 RVA: 0x00027A10 File Offset: 0x00025C10
	public int controlDepth
	{
		get
		{
			return this.idMain.controlDepth;
		}
	}

	// Token: 0x17000294 RID: 660
	// (get) Token: 0x06000973 RID: 2419 RVA: 0x00027A20 File Offset: 0x00025C20
	public int controlCount
	{
		get
		{
			return this.idMain.controlCount;
		}
	}

	// Token: 0x17000295 RID: 661
	// (get) Token: 0x06000974 RID: 2420 RVA: 0x00027A30 File Offset: 0x00025C30
	public string controllerClassName
	{
		get
		{
			return this.idMain.controllerClassName;
		}
	}

	// Token: 0x17000296 RID: 662
	// (get) Token: 0x06000975 RID: 2421 RVA: 0x00027A40 File Offset: 0x00025C40
	public bool controlOverridden
	{
		get
		{
			return this.idMain.controlOverridden;
		}
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x00027A50 File Offset: 0x00025C50
	public bool ControlOverriddenBy(Controllable controllable)
	{
		return this.idMain.ControlOverriddenBy(controllable);
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x00027A60 File Offset: 0x00025C60
	public bool ControlOverriddenBy(Controller controller)
	{
		return this.idMain.ControlOverriddenBy(controller);
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x00027A70 File Offset: 0x00025C70
	public bool ControlOverriddenBy(Character character)
	{
		return this.idMain.ControlOverriddenBy(character);
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x00027A80 File Offset: 0x00025C80
	public bool ControlOverriddenBy(IDMain main)
	{
		return this.idMain.ControlOverriddenBy(main);
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x00027A90 File Offset: 0x00025C90
	public bool ControlOverriddenBy(IDBase idBase)
	{
		return this.idMain.ControlOverriddenBy(idBase);
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x00027AA0 File Offset: 0x00025CA0
	public bool ControlOverriddenBy(IDLocalCharacter idLocal)
	{
		return this.idMain.ControlOverriddenBy(idLocal);
	}

	// Token: 0x17000297 RID: 663
	// (get) Token: 0x0600097C RID: 2428 RVA: 0x00027AB0 File Offset: 0x00025CB0
	public bool overridingControl
	{
		get
		{
			return this.idMain.overridingControl;
		}
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x00027AC0 File Offset: 0x00025CC0
	public bool OverridingControlOf(Controllable controllable)
	{
		return this.idMain.OverridingControlOf(controllable);
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x00027AD0 File Offset: 0x00025CD0
	public bool OverridingControlOf(Controller controller)
	{
		return this.idMain.OverridingControlOf(controller);
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x00027AE0 File Offset: 0x00025CE0
	public bool OverridingControlOf(Character character)
	{
		return this.idMain.OverridingControlOf(character);
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x00027AF0 File Offset: 0x00025CF0
	public bool OverridingControlOf(IDMain main)
	{
		return this.idMain.OverridingControlOf(main);
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x00027B00 File Offset: 0x00025D00
	public bool OverridingControlOf(IDBase idBase)
	{
		return this.idMain.OverridingControlOf(idBase);
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x00027B10 File Offset: 0x00025D10
	public bool OverridingControlOf(IDLocalCharacter idLocal)
	{
		return this.idMain.OverridingControlOf(idLocal);
	}

	// Token: 0x17000298 RID: 664
	// (get) Token: 0x06000983 RID: 2435 RVA: 0x00027B20 File Offset: 0x00025D20
	public bool assignedControl
	{
		get
		{
			return this.idMain.assignedControl;
		}
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x00027B30 File Offset: 0x00025D30
	public bool AssignedControlOf(Controllable controllable)
	{
		return this.idMain.AssignedControlOf(controllable);
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x00027B40 File Offset: 0x00025D40
	public bool AssignedControlOf(Controller controller)
	{
		return this.idMain.AssignedControlOf(controller);
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x00027B50 File Offset: 0x00025D50
	public bool AssignedControlOf(IDMain character)
	{
		return this.idMain.AssignedControlOf(character);
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x00027B60 File Offset: 0x00025D60
	public bool AssignedControlOf(IDBase idBase)
	{
		return this.idMain.AssignedControlOf(idBase);
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x00027B70 File Offset: 0x00025D70
	public RelativeControl RelativeControlTo(Controllable controllable)
	{
		return this.idMain.RelativeControlTo(controllable);
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x00027B80 File Offset: 0x00025D80
	public RelativeControl RelativeControlTo(Controller controller)
	{
		return this.idMain.RelativeControlTo(controller);
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x00027B90 File Offset: 0x00025D90
	public RelativeControl RelativeControlTo(Character character)
	{
		return this.idMain.RelativeControlTo(character);
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x00027BA0 File Offset: 0x00025DA0
	public RelativeControl RelativeControlTo(IDMain main)
	{
		return this.idMain.RelativeControlTo(main);
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x00027BB0 File Offset: 0x00025DB0
	public RelativeControl RelativeControlTo(IDLocalCharacter idLocal)
	{
		return this.idMain.RelativeControlTo(idLocal);
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x00027BC0 File Offset: 0x00025DC0
	public RelativeControl RelativeControlTo(IDBase idBase)
	{
		return this.idMain.RelativeControlTo(idBase);
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x00027BD0 File Offset: 0x00025DD0
	public RelativeControl RelativeControlFrom(Controllable controllable)
	{
		return this.idMain.RelativeControlFrom(controllable);
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x00027BE0 File Offset: 0x00025DE0
	public RelativeControl RelativeControlFrom(Controller controller)
	{
		return this.idMain.RelativeControlFrom(controller);
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x00027BF0 File Offset: 0x00025DF0
	public RelativeControl RelativeControlFrom(Character character)
	{
		return this.idMain.RelativeControlFrom(character);
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x00027C00 File Offset: 0x00025E00
	public RelativeControl RelativeControlFrom(IDMain main)
	{
		return this.idMain.RelativeControlFrom(main);
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x00027C10 File Offset: 0x00025E10
	public RelativeControl RelativeControlFrom(IDLocalCharacter idLocal)
	{
		return this.idMain.RelativeControlFrom(idLocal);
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x00027C20 File Offset: 0x00025E20
	public RelativeControl RelativeControlFrom(IDBase idBase)
	{
		return this.idMain.RelativeControlFrom(idBase);
	}

	// Token: 0x17000299 RID: 665
	// (get) Token: 0x06000994 RID: 2452 RVA: 0x00027C30 File Offset: 0x00025E30
	public Controller controller
	{
		get
		{
			return this.idMain.controller;
		}
	}

	// Token: 0x1700029A RID: 666
	// (get) Token: 0x06000995 RID: 2453 RVA: 0x00027C40 File Offset: 0x00025E40
	public Controller controlledController
	{
		get
		{
			return this.idMain.controlledController;
		}
	}

	// Token: 0x1700029B RID: 667
	// (get) Token: 0x06000996 RID: 2454 RVA: 0x00027C50 File Offset: 0x00025E50
	public Controller playerControlledController
	{
		get
		{
			return this.idMain.playerControlledController;
		}
	}

	// Token: 0x1700029C RID: 668
	// (get) Token: 0x06000997 RID: 2455 RVA: 0x00027C60 File Offset: 0x00025E60
	public Controller aiControlledController
	{
		get
		{
			return this.idMain.aiControlledController;
		}
	}

	// Token: 0x1700029D RID: 669
	// (get) Token: 0x06000998 RID: 2456 RVA: 0x00027C70 File Offset: 0x00025E70
	public Controller localPlayerControlledController
	{
		get
		{
			return this.idMain.localPlayerControlledController;
		}
	}

	// Token: 0x1700029E RID: 670
	// (get) Token: 0x06000999 RID: 2457 RVA: 0x00027C80 File Offset: 0x00025E80
	public Controller localAIControlledController
	{
		get
		{
			return this.idMain.localAIControlledController;
		}
	}

	// Token: 0x1700029F RID: 671
	// (get) Token: 0x0600099A RID: 2458 RVA: 0x00027C90 File Offset: 0x00025E90
	public Controller remotePlayerControlledController
	{
		get
		{
			return this.idMain.remotePlayerControlledController;
		}
	}

	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x0600099B RID: 2459 RVA: 0x00027CA0 File Offset: 0x00025EA0
	public Controller remoteAIControlledController
	{
		get
		{
			return this.idMain.remoteAIControlledController;
		}
	}

	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x0600099C RID: 2460 RVA: 0x00027CB0 File Offset: 0x00025EB0
	public Controller masterController
	{
		get
		{
			return this.idMain.masterController;
		}
	}

	// Token: 0x170002A2 RID: 674
	// (get) Token: 0x0600099D RID: 2461 RVA: 0x00027CC0 File Offset: 0x00025EC0
	public Controller rootController
	{
		get
		{
			return this.idMain.rootController;
		}
	}

	// Token: 0x170002A3 RID: 675
	// (get) Token: 0x0600099E RID: 2462 RVA: 0x00027CD0 File Offset: 0x00025ED0
	public Controller nextController
	{
		get
		{
			return this.idMain.nextController;
		}
	}

	// Token: 0x170002A4 RID: 676
	// (get) Token: 0x0600099F RID: 2463 RVA: 0x00027CE0 File Offset: 0x00025EE0
	public Controller previousController
	{
		get
		{
			return this.idMain.previousController;
		}
	}

	// Token: 0x170002A5 RID: 677
	// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00027CF0 File Offset: 0x00025EF0
	public float maxPitch
	{
		get
		{
			return this.idMain.maxPitch;
		}
	}

	// Token: 0x170002A6 RID: 678
	// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00027D00 File Offset: 0x00025F00
	public float minPitch
	{
		get
		{
			return this.idMain.minPitch;
		}
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x00027D10 File Offset: 0x00025F10
	public float ClampPitch(float v)
	{
		return this.idMain.ClampPitch(v);
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x00027D20 File Offset: 0x00025F20
	public void ClampPitch(ref float v)
	{
		this.idMain.ClampPitch(ref v);
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x00027D30 File Offset: 0x00025F30
	public Angle2 ClampPitch(Angle2 v)
	{
		return this.idMain.ClampPitch(v);
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x00027D40 File Offset: 0x00025F40
	public void ClampPitch(ref Angle2 v)
	{
		this.idMain.ClampPitch(ref v);
	}

	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00027D50 File Offset: 0x00025F50
	// (set) Token: 0x060009A7 RID: 2471 RVA: 0x00027D60 File Offset: 0x00025F60
	public CharacterStateFlags stateFlags
	{
		get
		{
			return this.idMain.stateFlags;
		}
		set
		{
			this.idMain.stateFlags = value;
		}
	}

	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00027D70 File Offset: 0x00025F70
	// (set) Token: 0x060009A9 RID: 2473 RVA: 0x00027D80 File Offset: 0x00025F80
	public bool lockMovement
	{
		get
		{
			return this.idMain.lockMovement;
		}
		set
		{
			this.idMain.lockMovement = value;
		}
	}

	// Token: 0x170002A9 RID: 681
	// (get) Token: 0x060009AA RID: 2474 RVA: 0x00027D90 File Offset: 0x00025F90
	// (set) Token: 0x060009AB RID: 2475 RVA: 0x00027DA0 File Offset: 0x00025FA0
	public bool lockLook
	{
		get
		{
			return this.idMain.lockLook;
		}
		set
		{
			this.idMain.lockLook = value;
		}
	}

	// Token: 0x170002AA RID: 682
	// (get) Token: 0x060009AC RID: 2476 RVA: 0x00027DB0 File Offset: 0x00025FB0
	// (set) Token: 0x060009AD RID: 2477 RVA: 0x00027DC0 File Offset: 0x00025FC0
	public float eyesPitch
	{
		get
		{
			return this.idMain.eyesPitch;
		}
		set
		{
			this.idMain.eyesPitch = value;
		}
	}

	// Token: 0x170002AB RID: 683
	// (get) Token: 0x060009AE RID: 2478 RVA: 0x00027DD0 File Offset: 0x00025FD0
	// (set) Token: 0x060009AF RID: 2479 RVA: 0x00027DE0 File Offset: 0x00025FE0
	public float eyesYaw
	{
		get
		{
			return this.idMain.eyesYaw;
		}
		set
		{
			this.idMain.eyesYaw = value;
		}
	}

	// Token: 0x170002AC RID: 684
	// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00027DF0 File Offset: 0x00025FF0
	// (set) Token: 0x060009B1 RID: 2481 RVA: 0x00027E00 File Offset: 0x00026000
	public Angle2 eyesAngles
	{
		get
		{
			return this.idMain.eyesAngles;
		}
		set
		{
			this.idMain.eyesAngles = value;
		}
	}

	// Token: 0x170002AD RID: 685
	// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00027E10 File Offset: 0x00026010
	public Vector3 eyesOrigin
	{
		get
		{
			return this.idMain.eyesOrigin;
		}
	}

	// Token: 0x170002AE RID: 686
	// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00027E20 File Offset: 0x00026020
	public Vector3 eyesOriginAtInitialOffset
	{
		get
		{
			return this.idMain.eyesOriginAtInitialOffset;
		}
	}

	// Token: 0x170002AF RID: 687
	// (get) Token: 0x060009B4 RID: 2484 RVA: 0x00027E30 File Offset: 0x00026030
	// (set) Token: 0x060009B5 RID: 2485 RVA: 0x00027E40 File Offset: 0x00026040
	public Vector3 eyesOffset
	{
		get
		{
			return this.idMain.eyesOffset;
		}
		set
		{
			this.idMain.eyesOffset = value;
		}
	}

	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x060009B6 RID: 2486 RVA: 0x00027E50 File Offset: 0x00026050
	public Vector3 initialEyesOffset
	{
		get
		{
			return this.idMain.initialEyesOffset;
		}
	}

	// Token: 0x170002B1 RID: 689
	// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00027E60 File Offset: 0x00026060
	public float initialEyesOffsetX
	{
		get
		{
			return this.idMain.initialEyesOffsetX;
		}
	}

	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x060009B8 RID: 2488 RVA: 0x00027E70 File Offset: 0x00026070
	public float initialEyesOffsetY
	{
		get
		{
			return this.idMain.initialEyesOffsetY;
		}
	}

	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x060009B9 RID: 2489 RVA: 0x00027E80 File Offset: 0x00026080
	public float initialEyesOffsetZ
	{
		get
		{
			return this.idMain.initialEyesOffsetZ;
		}
	}

	// Token: 0x170002B4 RID: 692
	// (get) Token: 0x060009BA RID: 2490 RVA: 0x00027E90 File Offset: 0x00026090
	public Ray eyesRay
	{
		get
		{
			return this.idMain.eyesRay;
		}
	}

	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x060009BB RID: 2491 RVA: 0x00027EA0 File Offset: 0x000260A0
	// (set) Token: 0x060009BC RID: 2492 RVA: 0x00027EB0 File Offset: 0x000260B0
	public Quaternion eyesRotation
	{
		get
		{
			return this.idMain.eyesRotation;
		}
		set
		{
			this.idMain.eyesRotation = value;
		}
	}

	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x060009BD RID: 2493 RVA: 0x00027EC0 File Offset: 0x000260C0
	public Transform eyesTransformReadOnly
	{
		get
		{
			return this.idMain.eyesTransformReadOnly;
		}
	}

	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x060009BE RID: 2494 RVA: 0x00027ED0 File Offset: 0x000260D0
	// (set) Token: 0x060009BF RID: 2495 RVA: 0x00027EE0 File Offset: 0x000260E0
	public Vector3 origin
	{
		get
		{
			return this.idMain.origin;
		}
		set
		{
			this.idMain.origin = value;
		}
	}

	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x060009C0 RID: 2496 RVA: 0x00027EF0 File Offset: 0x000260F0
	public Vector3 forward
	{
		get
		{
			return this.idMain.forward;
		}
	}

	// Token: 0x170002B9 RID: 697
	// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00027F00 File Offset: 0x00026100
	public Vector3 right
	{
		get
		{
			return this.idMain.right;
		}
	}

	// Token: 0x170002BA RID: 698
	// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00027F10 File Offset: 0x00026110
	public Vector3 up
	{
		get
		{
			return this.idMain.up;
		}
	}

	// Token: 0x170002BB RID: 699
	// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00027F20 File Offset: 0x00026120
	// (set) Token: 0x060009C4 RID: 2500 RVA: 0x00027F30 File Offset: 0x00026130
	public Quaternion rotation
	{
		get
		{
			return this.idMain.rotation;
		}
		set
		{
			this.idMain.rotation = value;
		}
	}

	// Token: 0x170002BC RID: 700
	// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00027F40 File Offset: 0x00026140
	public bool signaledDeath
	{
		get
		{
			return this.idMain.signaledDeath;
		}
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x00027F50 File Offset: 0x00026150
	public void ApplyAdditiveEyeAngles(Angle2 angles)
	{
		this.idMain.ApplyAdditiveEyeAngles(angles);
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x00027F60 File Offset: 0x00026160
	public T AddAddon<T>() where T : IDLocalCharacterAddon, new()
	{
		return this.idMain.AddAddon<T>();
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x00027F70 File Offset: 0x00026170
	public TBase AddAddon<TBase, T>() where TBase : IDLocalCharacterAddon where T : TBase, new()
	{
		return this.idMain.AddAddon<TBase, T>();
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x00027F80 File Offset: 0x00026180
	public IDLocalCharacterAddon AddAddon(Type addonType)
	{
		return this.idMain.AddAddon(addonType);
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x00027F90 File Offset: 0x00026190
	public IDLocalCharacterAddon AddAddon(Type addonType, Type minimumType)
	{
		return this.idMain.AddAddon(addonType, minimumType);
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x00027FA0 File Offset: 0x000261A0
	public TBase AddAddon<TBase>(Type addonType) where TBase : IDLocalCharacterAddon
	{
		return this.idMain.AddAddon<TBase>(addonType);
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x00027FB0 File Offset: 0x000261B0
	public IDLocalCharacterAddon AddAddon(string addonTypeName)
	{
		return this.idMain.AddAddon(addonTypeName);
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x00027FC0 File Offset: 0x000261C0
	public IDLocalCharacterAddon AddAddon(string addonTypeName, Type minimumType)
	{
		return this.idMain.AddAddon(addonTypeName, minimumType);
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x00027FD0 File Offset: 0x000261D0
	public TBase AddAddon<TBase>(string addonTypeName) where TBase : IDLocalCharacterAddon
	{
		return this.idMain.AddAddon<TBase>(addonTypeName);
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x00027FE0 File Offset: 0x000261E0
	public void RemoveAddon(IDLocalCharacterAddon addon)
	{
		this.idMain.RemoveAddon(addon);
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x00027FF0 File Offset: 0x000261F0
	public void RemoveAddon<T>(ref T addon) where T : IDLocalCharacterAddon
	{
		this.idMain.RemoveAddon<T>(ref addon);
	}

	// Token: 0x170002BD RID: 701
	// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00028000 File Offset: 0x00026200
	// (set) Token: 0x060009D2 RID: 2514 RVA: 0x0002802C File Offset: 0x0002622C
	protected RPOSLimitFlags rposLimitFlags
	{
		get
		{
			Controller controller = this.controller;
			return (!controller) ? ((RPOSLimitFlags)(-1)) : controller.rposLimitFlags;
		}
		set
		{
			Controller controller = this.controller;
			if (controller)
			{
				controller.rposLimitFlags = value;
			}
		}
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x00028054 File Offset: 0x00026254
	public CharacterTrait GetTrait(Type characterTraitType)
	{
		return this.idMain.GetTrait(characterTraitType);
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x00028064 File Offset: 0x00026264
	public TCharacterTrait GetTrait<TCharacterTrait>() where TCharacterTrait : CharacterTrait
	{
		return this.idMain.GetTrait<TCharacterTrait>();
	}

	// Token: 0x170002BE RID: 702
	// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00028074 File Offset: 0x00026274
	public bool? idle
	{
		get
		{
			return this.idMain.idle;
		}
	}

	// Token: 0x170002BF RID: 703
	// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00028084 File Offset: 0x00026284
	public IDLocalCharacterIdleControl idleControl
	{
		get
		{
			return this.idMain.idleControl;
		}
	}

	// Token: 0x170002C0 RID: 704
	// (get) Token: 0x060009D7 RID: 2519 RVA: 0x00028094 File Offset: 0x00026294
	public Crouchable crouchable
	{
		get
		{
			return this.idMain.crouchable;
		}
	}

	// Token: 0x170002C1 RID: 705
	// (get) Token: 0x060009D8 RID: 2520 RVA: 0x000280A4 File Offset: 0x000262A4
	public bool crouched
	{
		get
		{
			return this.idMain.crouched;
		}
	}

	// Token: 0x170002C2 RID: 706
	// (get) Token: 0x060009D9 RID: 2521 RVA: 0x000280B4 File Offset: 0x000262B4
	public TakeDamage takeDamage
	{
		get
		{
			return this.idMain.takeDamage;
		}
	}

	// Token: 0x170002C3 RID: 707
	// (get) Token: 0x060009DA RID: 2522 RVA: 0x000280C4 File Offset: 0x000262C4
	public float health
	{
		get
		{
			return this.idMain.health;
		}
	}

	// Token: 0x170002C4 RID: 708
	// (get) Token: 0x060009DB RID: 2523 RVA: 0x000280D4 File Offset: 0x000262D4
	public float healthFraction
	{
		get
		{
			return this.idMain.healthFraction;
		}
	}

	// Token: 0x170002C5 RID: 709
	// (get) Token: 0x060009DC RID: 2524 RVA: 0x000280E4 File Offset: 0x000262E4
	public bool alive
	{
		get
		{
			return this.idMain.alive;
		}
	}

	// Token: 0x170002C6 RID: 710
	// (get) Token: 0x060009DD RID: 2525 RVA: 0x000280F4 File Offset: 0x000262F4
	public bool dead
	{
		get
		{
			return this.idMain.dead;
		}
	}

	// Token: 0x170002C7 RID: 711
	// (get) Token: 0x060009DE RID: 2526 RVA: 0x00028104 File Offset: 0x00026304
	public float healthLoss
	{
		get
		{
			return this.idMain.healthLoss;
		}
	}

	// Token: 0x170002C8 RID: 712
	// (get) Token: 0x060009DF RID: 2527 RVA: 0x00028114 File Offset: 0x00026314
	public float healthLossFraction
	{
		get
		{
			return this.idMain.healthLossFraction;
		}
	}

	// Token: 0x170002C9 RID: 713
	// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00028124 File Offset: 0x00026324
	public float maxHealth
	{
		get
		{
			return this.idMain.maxHealth;
		}
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x00028134 File Offset: 0x00026334
	public void AdjustClientSideHealth(float newHealthValue)
	{
		this.idMain.AdjustClientSideHealth(newHealthValue);
	}

	// Token: 0x170002CA RID: 714
	// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00028144 File Offset: 0x00026344
	public VisNode visNode
	{
		get
		{
			return this.idMain.visNode;
		}
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x00028154 File Offset: 0x00026354
	public bool CanSee(VisNode other)
	{
		return this.idMain.CanSee(other);
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x00028164 File Offset: 0x00026364
	public bool CanSee(Character other)
	{
		return this.idMain.CanSee(other);
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x00028174 File Offset: 0x00026374
	public bool CanSee(IDMain other)
	{
		return this.idMain.CanSee(other);
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x00028184 File Offset: 0x00026384
	public bool CanSeeUnobstructed(VisNode other)
	{
		return this.idMain.CanSeeUnobstructed(other);
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x00028194 File Offset: 0x00026394
	public bool CanSeeUnobstructed(Character other)
	{
		return this.idMain.CanSeeUnobstructed(other);
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x000281A4 File Offset: 0x000263A4
	public bool CanSeeUnobstructed(IDMain other)
	{
		return this.idMain.CanSeeUnobstructed(other);
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x000281B4 File Offset: 0x000263B4
	public bool CanSee(VisNode other, bool unobstructed)
	{
		return this.idMain.CanSee(other, unobstructed);
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x000281C4 File Offset: 0x000263C4
	public bool CanSee(Character other, bool unobstructed)
	{
		return this.idMain.CanSee(other, unobstructed);
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x000281D4 File Offset: 0x000263D4
	public bool CanSee(IDMain other, bool unobstructed)
	{
		return this.idMain.CanSee(other, unobstructed);
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x000281E4 File Offset: 0x000263E4
	public bool AudibleMessage(Vector3 point, float hearRadius, string message, object arg)
	{
		return this.idMain.AudibleMessage(point, hearRadius, message, arg);
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x000281F8 File Offset: 0x000263F8
	public bool AudibleMessage(Vector3 point, float hearRadius, string message)
	{
		return this.idMain.AudibleMessage(point, hearRadius, message);
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x00028208 File Offset: 0x00026408
	public bool AudibleMessage(float hearRadius, string message, object arg)
	{
		return this.idMain.AudibleMessage(hearRadius, message, arg);
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x00028218 File Offset: 0x00026418
	public bool AudibleMessage(float hearRadius, string message)
	{
		return this.idMain.AudibleMessage(hearRadius, message);
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x00028228 File Offset: 0x00026428
	public bool GestureMessage(string message)
	{
		return this.idMain.GestureMessage(message);
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x00028238 File Offset: 0x00026438
	public bool GestureMessage(string message, object arg)
	{
		return this.idMain.GestureMessage(message, arg);
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x00028248 File Offset: 0x00026448
	public bool AttentionMessage(string message)
	{
		return this.idMain.AttentionMessage(message);
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x00028258 File Offset: 0x00026458
	public bool AttentionMessage(string message, object arg)
	{
		return this.idMain.AttentionMessage(message, arg);
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x00028268 File Offset: 0x00026468
	public bool ContactMessage(string message)
	{
		return this.idMain.ContactMessage(message);
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x00028278 File Offset: 0x00026478
	public bool ContactMessage(string message, object arg)
	{
		return this.idMain.ContactMessage(message, arg);
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x00028288 File Offset: 0x00026488
	public bool StealthMessage(string message)
	{
		return this.idMain.StealthMessage(message);
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x00028298 File Offset: 0x00026498
	public bool StealthMessage(string message, object arg)
	{
		return this.idMain.StealthMessage(message, arg);
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x000282A8 File Offset: 0x000264A8
	public bool PreyMessage(string message)
	{
		return this.idMain.PreyMessage(message);
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x000282B8 File Offset: 0x000264B8
	public bool PreyMessage(string message, object arg)
	{
		return this.idMain.PreyMessage(message, arg);
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x000282C8 File Offset: 0x000264C8
	public bool ObliviousMessage(string message)
	{
		return this.idMain.ObliviousMessage(message);
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x000282D8 File Offset: 0x000264D8
	public bool ObliviousMessage(string message, object arg)
	{
		return this.idMain.ObliviousMessage(message, arg);
	}

	// Token: 0x170002CB RID: 715
	// (get) Token: 0x060009FC RID: 2556 RVA: 0x000282E8 File Offset: 0x000264E8
	// (set) Token: 0x060009FD RID: 2557 RVA: 0x000282F8 File Offset: 0x000264F8
	public Vis.Mask viewMask
	{
		get
		{
			return this.idMain.viewMask;
		}
		set
		{
			this.idMain.viewMask = value;
		}
	}

	// Token: 0x170002CC RID: 716
	// (get) Token: 0x060009FE RID: 2558 RVA: 0x00028308 File Offset: 0x00026508
	// (set) Token: 0x060009FF RID: 2559 RVA: 0x00028318 File Offset: 0x00026518
	public Vis.Mask traitMask
	{
		get
		{
			return this.idMain.traitMask;
		}
		set
		{
			this.idMain.traitMask = value;
		}
	}

	// Token: 0x170002CD RID: 717
	// (get) Token: 0x06000A00 RID: 2560 RVA: 0x00028328 File Offset: 0x00026528
	// (set) Token: 0x06000A01 RID: 2561 RVA: 0x00028338 File Offset: 0x00026538
	public bool blind
	{
		get
		{
			return this.idMain.blind;
		}
		set
		{
			this.idMain.blind = value;
		}
	}

	// Token: 0x170002CE RID: 718
	// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00028348 File Offset: 0x00026548
	// (set) Token: 0x06000A03 RID: 2563 RVA: 0x00028358 File Offset: 0x00026558
	public bool deaf
	{
		get
		{
			return this.idMain.deaf;
		}
		set
		{
			this.idMain.deaf = value;
		}
	}

	// Token: 0x170002CF RID: 719
	// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00028368 File Offset: 0x00026568
	// (set) Token: 0x06000A05 RID: 2565 RVA: 0x00028378 File Offset: 0x00026578
	public bool mute
	{
		get
		{
			return this.idMain.mute;
		}
		set
		{
			this.idMain.mute = value;
		}
	}

	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x06000A06 RID: 2566 RVA: 0x00028388 File Offset: 0x00026588
	public NavMeshAgent agent
	{
		get
		{
			return this.idMain.agent;
		}
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x00028398 File Offset: 0x00026598
	public bool CreateNavMeshAgent()
	{
		return this.idMain.CreateNavMeshAgent();
	}

	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x06000A08 RID: 2568 RVA: 0x000283A8 File Offset: 0x000265A8
	public CharacterInterpolatorBase interpolator
	{
		get
		{
			return this.idMain.interpolator;
		}
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x000283B8 File Offset: 0x000265B8
	public bool CreateInterpolator()
	{
		return this.idMain.CreateInterpolator();
	}

	// Token: 0x170002D2 RID: 722
	// (get) Token: 0x06000A0A RID: 2570 RVA: 0x000283C8 File Offset: 0x000265C8
	public CCMotor ccmotor
	{
		get
		{
			return this.idMain.ccmotor;
		}
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x000283D8 File Offset: 0x000265D8
	public bool CreateCCMotor()
	{
		return this.idMain.CreateCCMotor();
	}

	// Token: 0x170002D3 RID: 723
	// (get) Token: 0x06000A0C RID: 2572 RVA: 0x000283E8 File Offset: 0x000265E8
	public IDLocalCharacterAddon overlay
	{
		get
		{
			return this.idMain.overlay;
		}
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x000283F8 File Offset: 0x000265F8
	public bool CreateOverlay()
	{
		return this.idMain.CreateOverlay();
	}
}
