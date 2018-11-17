using System;
using UnityEngine;

// Token: 0x02000171 RID: 369
public abstract class IDLocalCharacter : IDLocal
{
	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0002B59C File Offset: 0x0002979C
	public global::Character idMain
	{
		get
		{
			return (global::Character)this.idMain;
		}
	}

	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0002B5AC File Offset: 0x000297AC
	public global::Character character
	{
		get
		{
			return (global::Character)this.idMain;
		}
	}

	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0002B5BC File Offset: 0x000297BC
	public global::HitBoxSystem hitBoxSystem
	{
		get
		{
			return this.idMain.hitBoxSystem;
		}
	}

	// Token: 0x170002B9 RID: 697
	// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0002B5CC File Offset: 0x000297CC
	public global::RecoilSimulation recoilSimulation
	{
		get
		{
			return this.idMain.recoilSimulation;
		}
	}

	// Token: 0x170002BA RID: 698
	// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0002B5DC File Offset: 0x000297DC
	public global::PlayerClient playerClient
	{
		get
		{
			return this.idMain.playerClient;
		}
	}

	// Token: 0x170002BB RID: 699
	// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0002B5EC File Offset: 0x000297EC
	public bool controlled
	{
		get
		{
			return this.idMain.controlled;
		}
	}

	// Token: 0x170002BC RID: 700
	// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0002B5FC File Offset: 0x000297FC
	public bool playerControlled
	{
		get
		{
			return this.idMain.playerControlled;
		}
	}

	// Token: 0x170002BD RID: 701
	// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0002B60C File Offset: 0x0002980C
	public bool aiControlled
	{
		get
		{
			return this.idMain.aiControlled;
		}
	}

	// Token: 0x170002BE RID: 702
	// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0002B61C File Offset: 0x0002981C
	public bool localPlayerControlled
	{
		get
		{
			return this.idMain.localPlayerControlled;
		}
	}

	// Token: 0x170002BF RID: 703
	// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0002B62C File Offset: 0x0002982C
	public bool remotePlayerControlled
	{
		get
		{
			return this.idMain.remotePlayerControlled;
		}
	}

	// Token: 0x170002C0 RID: 704
	// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0002B63C File Offset: 0x0002983C
	public bool localAIControlled
	{
		get
		{
			return this.idMain.localAIControlled;
		}
	}

	// Token: 0x170002C1 RID: 705
	// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0002B64C File Offset: 0x0002984C
	public bool remoteAIControlled
	{
		get
		{
			return this.idMain.remoteAIControlled;
		}
	}

	// Token: 0x170002C2 RID: 706
	// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0002B65C File Offset: 0x0002985C
	public bool localControlled
	{
		get
		{
			return this.idMain.localControlled;
		}
	}

	// Token: 0x170002C3 RID: 707
	// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0002B66C File Offset: 0x0002986C
	public bool remoteControlled
	{
		get
		{
			return this.idMain.remoteControlled;
		}
	}

	// Token: 0x170002C4 RID: 708
	// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0002B67C File Offset: 0x0002987C
	public global::Controllable controllable
	{
		get
		{
			return this.idMain.controllable;
		}
	}

	// Token: 0x170002C5 RID: 709
	// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0002B68C File Offset: 0x0002988C
	public global::Controllable controlledControllable
	{
		get
		{
			return this.idMain.controlledControllable;
		}
	}

	// Token: 0x170002C6 RID: 710
	// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0002B69C File Offset: 0x0002989C
	public global::Controllable playerControlledControllable
	{
		get
		{
			return this.idMain.playerControlledControllable;
		}
	}

	// Token: 0x170002C7 RID: 711
	// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0002B6AC File Offset: 0x000298AC
	public global::Controllable aiControlledControllable
	{
		get
		{
			return this.idMain.aiControlledControllable;
		}
	}

	// Token: 0x170002C8 RID: 712
	// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0002B6BC File Offset: 0x000298BC
	public global::Controllable localPlayerControlledControllable
	{
		get
		{
			return this.idMain.localPlayerControlledControllable;
		}
	}

	// Token: 0x170002C9 RID: 713
	// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0002B6CC File Offset: 0x000298CC
	public global::Controllable localAIControlledControllable
	{
		get
		{
			return this.idMain.localAIControlledControllable;
		}
	}

	// Token: 0x170002CA RID: 714
	// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0002B6DC File Offset: 0x000298DC
	public global::Controllable remotePlayerControlledControllable
	{
		get
		{
			return this.idMain.remotePlayerControlledControllable;
		}
	}

	// Token: 0x170002CB RID: 715
	// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0002B6EC File Offset: 0x000298EC
	public global::Controllable remoteAIControlledControllable
	{
		get
		{
			return this.idMain.remoteAIControlledControllable;
		}
	}

	// Token: 0x170002CC RID: 716
	// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0002B6FC File Offset: 0x000298FC
	public string npcName
	{
		get
		{
			return this.idMain.npcName;
		}
	}

	// Token: 0x170002CD RID: 717
	// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0002B70C File Offset: 0x0002990C
	public global::Character previousCharacter
	{
		get
		{
			return this.idMain.previousCharacter;
		}
	}

	// Token: 0x170002CE RID: 718
	// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0002B71C File Offset: 0x0002991C
	public global::Character rootCharacter
	{
		get
		{
			return this.idMain.rootCharacter;
		}
	}

	// Token: 0x170002CF RID: 719
	// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0002B72C File Offset: 0x0002992C
	public global::Character nextCharacter
	{
		get
		{
			return this.idMain.nextCharacter;
		}
	}

	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0002B73C File Offset: 0x0002993C
	public global::Character masterCharacter
	{
		get
		{
			return this.idMain.masterCharacter;
		}
	}

	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0002B74C File Offset: 0x0002994C
	public global::Controllable masterControllable
	{
		get
		{
			return this.idMain.masterControllable;
		}
	}

	// Token: 0x170002D2 RID: 722
	// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0002B75C File Offset: 0x0002995C
	public global::Controllable rootControllable
	{
		get
		{
			return this.idMain.rootControllable;
		}
	}

	// Token: 0x170002D3 RID: 723
	// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0002B76C File Offset: 0x0002996C
	public global::Controllable nextControllable
	{
		get
		{
			return this.idMain.nextControllable;
		}
	}

	// Token: 0x170002D4 RID: 724
	// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0002B77C File Offset: 0x0002997C
	public global::Controllable previousControllable
	{
		get
		{
			return this.idMain.previousControllable;
		}
	}

	// Token: 0x170002D5 RID: 725
	// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0002B78C File Offset: 0x0002998C
	public int controlDepth
	{
		get
		{
			return this.idMain.controlDepth;
		}
	}

	// Token: 0x170002D6 RID: 726
	// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0002B79C File Offset: 0x0002999C
	public int controlCount
	{
		get
		{
			return this.idMain.controlCount;
		}
	}

	// Token: 0x170002D7 RID: 727
	// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0002B7AC File Offset: 0x000299AC
	public string controllerClassName
	{
		get
		{
			return this.idMain.controllerClassName;
		}
	}

	// Token: 0x170002D8 RID: 728
	// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0002B7BC File Offset: 0x000299BC
	public bool controlOverridden
	{
		get
		{
			return this.idMain.controlOverridden;
		}
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x0002B7CC File Offset: 0x000299CC
	public bool ControlOverriddenBy(global::Controllable controllable)
	{
		return this.idMain.ControlOverriddenBy(controllable);
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x0002B7DC File Offset: 0x000299DC
	public bool ControlOverriddenBy(global::Controller controller)
	{
		return this.idMain.ControlOverriddenBy(controller);
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x0002B7EC File Offset: 0x000299EC
	public bool ControlOverriddenBy(global::Character character)
	{
		return this.idMain.ControlOverriddenBy(character);
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x0002B7FC File Offset: 0x000299FC
	public bool ControlOverriddenBy(IDMain main)
	{
		return this.idMain.ControlOverriddenBy(main);
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x0002B80C File Offset: 0x00029A0C
	public bool ControlOverriddenBy(IDBase idBase)
	{
		return this.idMain.ControlOverriddenBy(idBase);
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x0002B81C File Offset: 0x00029A1C
	public bool ControlOverriddenBy(global::IDLocalCharacter idLocal)
	{
		return this.idMain.ControlOverriddenBy(idLocal);
	}

	// Token: 0x170002D9 RID: 729
	// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0002B82C File Offset: 0x00029A2C
	public bool overridingControl
	{
		get
		{
			return this.idMain.overridingControl;
		}
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x0002B83C File Offset: 0x00029A3C
	public bool OverridingControlOf(global::Controllable controllable)
	{
		return this.idMain.OverridingControlOf(controllable);
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x0002B84C File Offset: 0x00029A4C
	public bool OverridingControlOf(global::Controller controller)
	{
		return this.idMain.OverridingControlOf(controller);
	}

	// Token: 0x06000AA5 RID: 2725 RVA: 0x0002B85C File Offset: 0x00029A5C
	public bool OverridingControlOf(global::Character character)
	{
		return this.idMain.OverridingControlOf(character);
	}

	// Token: 0x06000AA6 RID: 2726 RVA: 0x0002B86C File Offset: 0x00029A6C
	public bool OverridingControlOf(IDMain main)
	{
		return this.idMain.OverridingControlOf(main);
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x0002B87C File Offset: 0x00029A7C
	public bool OverridingControlOf(IDBase idBase)
	{
		return this.idMain.OverridingControlOf(idBase);
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x0002B88C File Offset: 0x00029A8C
	public bool OverridingControlOf(global::IDLocalCharacter idLocal)
	{
		return this.idMain.OverridingControlOf(idLocal);
	}

	// Token: 0x170002DA RID: 730
	// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0002B89C File Offset: 0x00029A9C
	public bool assignedControl
	{
		get
		{
			return this.idMain.assignedControl;
		}
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x0002B8AC File Offset: 0x00029AAC
	public bool AssignedControlOf(global::Controllable controllable)
	{
		return this.idMain.AssignedControlOf(controllable);
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x0002B8BC File Offset: 0x00029ABC
	public bool AssignedControlOf(global::Controller controller)
	{
		return this.idMain.AssignedControlOf(controller);
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x0002B8CC File Offset: 0x00029ACC
	public bool AssignedControlOf(IDMain character)
	{
		return this.idMain.AssignedControlOf(character);
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x0002B8DC File Offset: 0x00029ADC
	public bool AssignedControlOf(IDBase idBase)
	{
		return this.idMain.AssignedControlOf(idBase);
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x0002B8EC File Offset: 0x00029AEC
	public global::RelativeControl RelativeControlTo(global::Controllable controllable)
	{
		return this.idMain.RelativeControlTo(controllable);
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x0002B8FC File Offset: 0x00029AFC
	public global::RelativeControl RelativeControlTo(global::Controller controller)
	{
		return this.idMain.RelativeControlTo(controller);
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x0002B90C File Offset: 0x00029B0C
	public global::RelativeControl RelativeControlTo(global::Character character)
	{
		return this.idMain.RelativeControlTo(character);
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x0002B91C File Offset: 0x00029B1C
	public global::RelativeControl RelativeControlTo(IDMain main)
	{
		return this.idMain.RelativeControlTo(main);
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x0002B92C File Offset: 0x00029B2C
	public global::RelativeControl RelativeControlTo(global::IDLocalCharacter idLocal)
	{
		return this.idMain.RelativeControlTo(idLocal);
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x0002B93C File Offset: 0x00029B3C
	public global::RelativeControl RelativeControlTo(IDBase idBase)
	{
		return this.idMain.RelativeControlTo(idBase);
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x0002B94C File Offset: 0x00029B4C
	public global::RelativeControl RelativeControlFrom(global::Controllable controllable)
	{
		return this.idMain.RelativeControlFrom(controllable);
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x0002B95C File Offset: 0x00029B5C
	public global::RelativeControl RelativeControlFrom(global::Controller controller)
	{
		return this.idMain.RelativeControlFrom(controller);
	}

	// Token: 0x06000AB6 RID: 2742 RVA: 0x0002B96C File Offset: 0x00029B6C
	public global::RelativeControl RelativeControlFrom(global::Character character)
	{
		return this.idMain.RelativeControlFrom(character);
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x0002B97C File Offset: 0x00029B7C
	public global::RelativeControl RelativeControlFrom(IDMain main)
	{
		return this.idMain.RelativeControlFrom(main);
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x0002B98C File Offset: 0x00029B8C
	public global::RelativeControl RelativeControlFrom(global::IDLocalCharacter idLocal)
	{
		return this.idMain.RelativeControlFrom(idLocal);
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x0002B99C File Offset: 0x00029B9C
	public global::RelativeControl RelativeControlFrom(IDBase idBase)
	{
		return this.idMain.RelativeControlFrom(idBase);
	}

	// Token: 0x170002DB RID: 731
	// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0002B9AC File Offset: 0x00029BAC
	public global::Controller controller
	{
		get
		{
			return this.idMain.controller;
		}
	}

	// Token: 0x170002DC RID: 732
	// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0002B9BC File Offset: 0x00029BBC
	public global::Controller controlledController
	{
		get
		{
			return this.idMain.controlledController;
		}
	}

	// Token: 0x170002DD RID: 733
	// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0002B9CC File Offset: 0x00029BCC
	public global::Controller playerControlledController
	{
		get
		{
			return this.idMain.playerControlledController;
		}
	}

	// Token: 0x170002DE RID: 734
	// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0002B9DC File Offset: 0x00029BDC
	public global::Controller aiControlledController
	{
		get
		{
			return this.idMain.aiControlledController;
		}
	}

	// Token: 0x170002DF RID: 735
	// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0002B9EC File Offset: 0x00029BEC
	public global::Controller localPlayerControlledController
	{
		get
		{
			return this.idMain.localPlayerControlledController;
		}
	}

	// Token: 0x170002E0 RID: 736
	// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0002B9FC File Offset: 0x00029BFC
	public global::Controller localAIControlledController
	{
		get
		{
			return this.idMain.localAIControlledController;
		}
	}

	// Token: 0x170002E1 RID: 737
	// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0002BA0C File Offset: 0x00029C0C
	public global::Controller remotePlayerControlledController
	{
		get
		{
			return this.idMain.remotePlayerControlledController;
		}
	}

	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0002BA1C File Offset: 0x00029C1C
	public global::Controller remoteAIControlledController
	{
		get
		{
			return this.idMain.remoteAIControlledController;
		}
	}

	// Token: 0x170002E3 RID: 739
	// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0002BA2C File Offset: 0x00029C2C
	public global::Controller masterController
	{
		get
		{
			return this.idMain.masterController;
		}
	}

	// Token: 0x170002E4 RID: 740
	// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0002BA3C File Offset: 0x00029C3C
	public global::Controller rootController
	{
		get
		{
			return this.idMain.rootController;
		}
	}

	// Token: 0x170002E5 RID: 741
	// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0002BA4C File Offset: 0x00029C4C
	public global::Controller nextController
	{
		get
		{
			return this.idMain.nextController;
		}
	}

	// Token: 0x170002E6 RID: 742
	// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0002BA5C File Offset: 0x00029C5C
	public global::Controller previousController
	{
		get
		{
			return this.idMain.previousController;
		}
	}

	// Token: 0x170002E7 RID: 743
	// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0002BA6C File Offset: 0x00029C6C
	public float maxPitch
	{
		get
		{
			return this.idMain.maxPitch;
		}
	}

	// Token: 0x170002E8 RID: 744
	// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0002BA7C File Offset: 0x00029C7C
	public float minPitch
	{
		get
		{
			return this.idMain.minPitch;
		}
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x0002BA8C File Offset: 0x00029C8C
	public float ClampPitch(float v)
	{
		return this.idMain.ClampPitch(v);
	}

	// Token: 0x06000AC9 RID: 2761 RVA: 0x0002BA9C File Offset: 0x00029C9C
	public void ClampPitch(ref float v)
	{
		this.idMain.ClampPitch(ref v);
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x0002BAAC File Offset: 0x00029CAC
	public global::Angle2 ClampPitch(global::Angle2 v)
	{
		return this.idMain.ClampPitch(v);
	}

	// Token: 0x06000ACB RID: 2763 RVA: 0x0002BABC File Offset: 0x00029CBC
	public void ClampPitch(ref global::Angle2 v)
	{
		this.idMain.ClampPitch(ref v);
	}

	// Token: 0x170002E9 RID: 745
	// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0002BACC File Offset: 0x00029CCC
	// (set) Token: 0x06000ACD RID: 2765 RVA: 0x0002BADC File Offset: 0x00029CDC
	public global::CharacterStateFlags stateFlags
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

	// Token: 0x170002EA RID: 746
	// (get) Token: 0x06000ACE RID: 2766 RVA: 0x0002BAEC File Offset: 0x00029CEC
	// (set) Token: 0x06000ACF RID: 2767 RVA: 0x0002BAFC File Offset: 0x00029CFC
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

	// Token: 0x170002EB RID: 747
	// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0002BB0C File Offset: 0x00029D0C
	// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x0002BB1C File Offset: 0x00029D1C
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

	// Token: 0x170002EC RID: 748
	// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0002BB2C File Offset: 0x00029D2C
	// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x0002BB3C File Offset: 0x00029D3C
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

	// Token: 0x170002ED RID: 749
	// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0002BB4C File Offset: 0x00029D4C
	// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x0002BB5C File Offset: 0x00029D5C
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

	// Token: 0x170002EE RID: 750
	// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0002BB6C File Offset: 0x00029D6C
	// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x0002BB7C File Offset: 0x00029D7C
	public global::Angle2 eyesAngles
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

	// Token: 0x170002EF RID: 751
	// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0002BB8C File Offset: 0x00029D8C
	public Vector3 eyesOrigin
	{
		get
		{
			return this.idMain.eyesOrigin;
		}
	}

	// Token: 0x170002F0 RID: 752
	// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0002BB9C File Offset: 0x00029D9C
	public Vector3 eyesOriginAtInitialOffset
	{
		get
		{
			return this.idMain.eyesOriginAtInitialOffset;
		}
	}

	// Token: 0x170002F1 RID: 753
	// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0002BBAC File Offset: 0x00029DAC
	// (set) Token: 0x06000ADB RID: 2779 RVA: 0x0002BBBC File Offset: 0x00029DBC
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

	// Token: 0x170002F2 RID: 754
	// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0002BBCC File Offset: 0x00029DCC
	public Vector3 initialEyesOffset
	{
		get
		{
			return this.idMain.initialEyesOffset;
		}
	}

	// Token: 0x170002F3 RID: 755
	// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0002BBDC File Offset: 0x00029DDC
	public float initialEyesOffsetX
	{
		get
		{
			return this.idMain.initialEyesOffsetX;
		}
	}

	// Token: 0x170002F4 RID: 756
	// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0002BBEC File Offset: 0x00029DEC
	public float initialEyesOffsetY
	{
		get
		{
			return this.idMain.initialEyesOffsetY;
		}
	}

	// Token: 0x170002F5 RID: 757
	// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0002BBFC File Offset: 0x00029DFC
	public float initialEyesOffsetZ
	{
		get
		{
			return this.idMain.initialEyesOffsetZ;
		}
	}

	// Token: 0x170002F6 RID: 758
	// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0002BC0C File Offset: 0x00029E0C
	public Ray eyesRay
	{
		get
		{
			return this.idMain.eyesRay;
		}
	}

	// Token: 0x170002F7 RID: 759
	// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0002BC1C File Offset: 0x00029E1C
	// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x0002BC2C File Offset: 0x00029E2C
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

	// Token: 0x170002F8 RID: 760
	// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0002BC3C File Offset: 0x00029E3C
	public Transform eyesTransformReadOnly
	{
		get
		{
			return this.idMain.eyesTransformReadOnly;
		}
	}

	// Token: 0x170002F9 RID: 761
	// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0002BC4C File Offset: 0x00029E4C
	// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x0002BC5C File Offset: 0x00029E5C
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

	// Token: 0x170002FA RID: 762
	// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0002BC6C File Offset: 0x00029E6C
	public Vector3 forward
	{
		get
		{
			return this.idMain.forward;
		}
	}

	// Token: 0x170002FB RID: 763
	// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x0002BC7C File Offset: 0x00029E7C
	public Vector3 right
	{
		get
		{
			return this.idMain.right;
		}
	}

	// Token: 0x170002FC RID: 764
	// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0002BC8C File Offset: 0x00029E8C
	public Vector3 up
	{
		get
		{
			return this.idMain.up;
		}
	}

	// Token: 0x170002FD RID: 765
	// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0002BC9C File Offset: 0x00029E9C
	// (set) Token: 0x06000AEA RID: 2794 RVA: 0x0002BCAC File Offset: 0x00029EAC
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

	// Token: 0x170002FE RID: 766
	// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0002BCBC File Offset: 0x00029EBC
	public bool signaledDeath
	{
		get
		{
			return this.idMain.signaledDeath;
		}
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x0002BCCC File Offset: 0x00029ECC
	public void ApplyAdditiveEyeAngles(global::Angle2 angles)
	{
		this.idMain.ApplyAdditiveEyeAngles(angles);
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x0002BCDC File Offset: 0x00029EDC
	public T AddAddon<T>() where T : global::IDLocalCharacterAddon, new()
	{
		return this.idMain.AddAddon<T>();
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x0002BCEC File Offset: 0x00029EEC
	public TBase AddAddon<TBase, T>() where TBase : global::IDLocalCharacterAddon where T : TBase, new()
	{
		return this.idMain.AddAddon<TBase, T>();
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x0002BCFC File Offset: 0x00029EFC
	public global::IDLocalCharacterAddon AddAddon(Type addonType)
	{
		return this.idMain.AddAddon(addonType);
	}

	// Token: 0x06000AF0 RID: 2800 RVA: 0x0002BD0C File Offset: 0x00029F0C
	public global::IDLocalCharacterAddon AddAddon(Type addonType, Type minimumType)
	{
		return this.idMain.AddAddon(addonType, minimumType);
	}

	// Token: 0x06000AF1 RID: 2801 RVA: 0x0002BD1C File Offset: 0x00029F1C
	public TBase AddAddon<TBase>(Type addonType) where TBase : global::IDLocalCharacterAddon
	{
		return this.idMain.AddAddon<TBase>(addonType);
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x0002BD2C File Offset: 0x00029F2C
	public global::IDLocalCharacterAddon AddAddon(string addonTypeName)
	{
		return this.idMain.AddAddon(addonTypeName);
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x0002BD3C File Offset: 0x00029F3C
	public global::IDLocalCharacterAddon AddAddon(string addonTypeName, Type minimumType)
	{
		return this.idMain.AddAddon(addonTypeName, minimumType);
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x0002BD4C File Offset: 0x00029F4C
	public TBase AddAddon<TBase>(string addonTypeName) where TBase : global::IDLocalCharacterAddon
	{
		return this.idMain.AddAddon<TBase>(addonTypeName);
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x0002BD5C File Offset: 0x00029F5C
	public void RemoveAddon(global::IDLocalCharacterAddon addon)
	{
		this.idMain.RemoveAddon(addon);
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x0002BD6C File Offset: 0x00029F6C
	public void RemoveAddon<T>(ref T addon) where T : global::IDLocalCharacterAddon
	{
		this.idMain.RemoveAddon<T>(ref addon);
	}

	// Token: 0x170002FF RID: 767
	// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0002BD7C File Offset: 0x00029F7C
	// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x0002BDA8 File Offset: 0x00029FA8
	protected global::RPOSLimitFlags rposLimitFlags
	{
		get
		{
			global::Controller controller = this.controller;
			return (!controller) ? ((global::RPOSLimitFlags)-1) : controller.rposLimitFlags;
		}
		set
		{
			global::Controller controller = this.controller;
			if (controller)
			{
				controller.rposLimitFlags = value;
			}
		}
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x0002BDD0 File Offset: 0x00029FD0
	public global::CharacterTrait GetTrait(Type characterTraitType)
	{
		return this.idMain.GetTrait(characterTraitType);
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x0002BDE0 File Offset: 0x00029FE0
	public TCharacterTrait GetTrait<TCharacterTrait>() where TCharacterTrait : global::CharacterTrait
	{
		return this.idMain.GetTrait<TCharacterTrait>();
	}

	// Token: 0x17000300 RID: 768
	// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0002BDF0 File Offset: 0x00029FF0
	public bool? idle
	{
		get
		{
			return this.idMain.idle;
		}
	}

	// Token: 0x17000301 RID: 769
	// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0002BE00 File Offset: 0x0002A000
	public global::IDLocalCharacterIdleControl idleControl
	{
		get
		{
			return this.idMain.idleControl;
		}
	}

	// Token: 0x17000302 RID: 770
	// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0002BE10 File Offset: 0x0002A010
	public global::Crouchable crouchable
	{
		get
		{
			return this.idMain.crouchable;
		}
	}

	// Token: 0x17000303 RID: 771
	// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0002BE20 File Offset: 0x0002A020
	public bool crouched
	{
		get
		{
			return this.idMain.crouched;
		}
	}

	// Token: 0x17000304 RID: 772
	// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0002BE30 File Offset: 0x0002A030
	public global::TakeDamage takeDamage
	{
		get
		{
			return this.idMain.takeDamage;
		}
	}

	// Token: 0x17000305 RID: 773
	// (get) Token: 0x06000B00 RID: 2816 RVA: 0x0002BE40 File Offset: 0x0002A040
	public float health
	{
		get
		{
			return this.idMain.health;
		}
	}

	// Token: 0x17000306 RID: 774
	// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0002BE50 File Offset: 0x0002A050
	public float healthFraction
	{
		get
		{
			return this.idMain.healthFraction;
		}
	}

	// Token: 0x17000307 RID: 775
	// (get) Token: 0x06000B02 RID: 2818 RVA: 0x0002BE60 File Offset: 0x0002A060
	public bool alive
	{
		get
		{
			return this.idMain.alive;
		}
	}

	// Token: 0x17000308 RID: 776
	// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0002BE70 File Offset: 0x0002A070
	public bool dead
	{
		get
		{
			return this.idMain.dead;
		}
	}

	// Token: 0x17000309 RID: 777
	// (get) Token: 0x06000B04 RID: 2820 RVA: 0x0002BE80 File Offset: 0x0002A080
	public float healthLoss
	{
		get
		{
			return this.idMain.healthLoss;
		}
	}

	// Token: 0x1700030A RID: 778
	// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0002BE90 File Offset: 0x0002A090
	public float healthLossFraction
	{
		get
		{
			return this.idMain.healthLossFraction;
		}
	}

	// Token: 0x1700030B RID: 779
	// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0002BEA0 File Offset: 0x0002A0A0
	public float maxHealth
	{
		get
		{
			return this.idMain.maxHealth;
		}
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x0002BEB0 File Offset: 0x0002A0B0
	public void AdjustClientSideHealth(float newHealthValue)
	{
		this.idMain.AdjustClientSideHealth(newHealthValue);
	}

	// Token: 0x1700030C RID: 780
	// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0002BEC0 File Offset: 0x0002A0C0
	public global::VisNode visNode
	{
		get
		{
			return this.idMain.visNode;
		}
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x0002BED0 File Offset: 0x0002A0D0
	public bool CanSee(global::VisNode other)
	{
		return this.idMain.CanSee(other);
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x0002BEE0 File Offset: 0x0002A0E0
	public bool CanSee(global::Character other)
	{
		return this.idMain.CanSee(other);
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x0002BEF0 File Offset: 0x0002A0F0
	public bool CanSee(IDMain other)
	{
		return this.idMain.CanSee(other);
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x0002BF00 File Offset: 0x0002A100
	public bool CanSeeUnobstructed(global::VisNode other)
	{
		return this.idMain.CanSeeUnobstructed(other);
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x0002BF10 File Offset: 0x0002A110
	public bool CanSeeUnobstructed(global::Character other)
	{
		return this.idMain.CanSeeUnobstructed(other);
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x0002BF20 File Offset: 0x0002A120
	public bool CanSeeUnobstructed(IDMain other)
	{
		return this.idMain.CanSeeUnobstructed(other);
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x0002BF30 File Offset: 0x0002A130
	public bool CanSee(global::VisNode other, bool unobstructed)
	{
		return this.idMain.CanSee(other, unobstructed);
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x0002BF40 File Offset: 0x0002A140
	public bool CanSee(global::Character other, bool unobstructed)
	{
		return this.idMain.CanSee(other, unobstructed);
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x0002BF50 File Offset: 0x0002A150
	public bool CanSee(IDMain other, bool unobstructed)
	{
		return this.idMain.CanSee(other, unobstructed);
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x0002BF60 File Offset: 0x0002A160
	public bool AudibleMessage(Vector3 point, float hearRadius, string message, object arg)
	{
		return this.idMain.AudibleMessage(point, hearRadius, message, arg);
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x0002BF74 File Offset: 0x0002A174
	public bool AudibleMessage(Vector3 point, float hearRadius, string message)
	{
		return this.idMain.AudibleMessage(point, hearRadius, message);
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x0002BF84 File Offset: 0x0002A184
	public bool AudibleMessage(float hearRadius, string message, object arg)
	{
		return this.idMain.AudibleMessage(hearRadius, message, arg);
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x0002BF94 File Offset: 0x0002A194
	public bool AudibleMessage(float hearRadius, string message)
	{
		return this.idMain.AudibleMessage(hearRadius, message);
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x0002BFA4 File Offset: 0x0002A1A4
	public bool GestureMessage(string message)
	{
		return this.idMain.GestureMessage(message);
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x0002BFB4 File Offset: 0x0002A1B4
	public bool GestureMessage(string message, object arg)
	{
		return this.idMain.GestureMessage(message, arg);
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x0002BFC4 File Offset: 0x0002A1C4
	public bool AttentionMessage(string message)
	{
		return this.idMain.AttentionMessage(message);
	}

	// Token: 0x06000B19 RID: 2841 RVA: 0x0002BFD4 File Offset: 0x0002A1D4
	public bool AttentionMessage(string message, object arg)
	{
		return this.idMain.AttentionMessage(message, arg);
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x0002BFE4 File Offset: 0x0002A1E4
	public bool ContactMessage(string message)
	{
		return this.idMain.ContactMessage(message);
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x0002BFF4 File Offset: 0x0002A1F4
	public bool ContactMessage(string message, object arg)
	{
		return this.idMain.ContactMessage(message, arg);
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x0002C004 File Offset: 0x0002A204
	public bool StealthMessage(string message)
	{
		return this.idMain.StealthMessage(message);
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x0002C014 File Offset: 0x0002A214
	public bool StealthMessage(string message, object arg)
	{
		return this.idMain.StealthMessage(message, arg);
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x0002C024 File Offset: 0x0002A224
	public bool PreyMessage(string message)
	{
		return this.idMain.PreyMessage(message);
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x0002C034 File Offset: 0x0002A234
	public bool PreyMessage(string message, object arg)
	{
		return this.idMain.PreyMessage(message, arg);
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x0002C044 File Offset: 0x0002A244
	public bool ObliviousMessage(string message)
	{
		return this.idMain.ObliviousMessage(message);
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x0002C054 File Offset: 0x0002A254
	public bool ObliviousMessage(string message, object arg)
	{
		return this.idMain.ObliviousMessage(message, arg);
	}

	// Token: 0x1700030D RID: 781
	// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0002C064 File Offset: 0x0002A264
	// (set) Token: 0x06000B23 RID: 2851 RVA: 0x0002C074 File Offset: 0x0002A274
	public global::Vis.Mask viewMask
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

	// Token: 0x1700030E RID: 782
	// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0002C084 File Offset: 0x0002A284
	// (set) Token: 0x06000B25 RID: 2853 RVA: 0x0002C094 File Offset: 0x0002A294
	public global::Vis.Mask traitMask
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

	// Token: 0x1700030F RID: 783
	// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0002C0A4 File Offset: 0x0002A2A4
	// (set) Token: 0x06000B27 RID: 2855 RVA: 0x0002C0B4 File Offset: 0x0002A2B4
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

	// Token: 0x17000310 RID: 784
	// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0002C0C4 File Offset: 0x0002A2C4
	// (set) Token: 0x06000B29 RID: 2857 RVA: 0x0002C0D4 File Offset: 0x0002A2D4
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

	// Token: 0x17000311 RID: 785
	// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0002C0E4 File Offset: 0x0002A2E4
	// (set) Token: 0x06000B2B RID: 2859 RVA: 0x0002C0F4 File Offset: 0x0002A2F4
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

	// Token: 0x17000312 RID: 786
	// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0002C104 File Offset: 0x0002A304
	public NavMeshAgent agent
	{
		get
		{
			return this.idMain.agent;
		}
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x0002C114 File Offset: 0x0002A314
	public bool CreateNavMeshAgent()
	{
		return this.idMain.CreateNavMeshAgent();
	}

	// Token: 0x17000313 RID: 787
	// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0002C124 File Offset: 0x0002A324
	public global::CharacterInterpolatorBase interpolator
	{
		get
		{
			return this.idMain.interpolator;
		}
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x0002C134 File Offset: 0x0002A334
	public bool CreateInterpolator()
	{
		return this.idMain.CreateInterpolator();
	}

	// Token: 0x17000314 RID: 788
	// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0002C144 File Offset: 0x0002A344
	public global::CCMotor ccmotor
	{
		get
		{
			return this.idMain.ccmotor;
		}
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x0002C154 File Offset: 0x0002A354
	public bool CreateCCMotor()
	{
		return this.idMain.CreateCCMotor();
	}

	// Token: 0x17000315 RID: 789
	// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0002C164 File Offset: 0x0002A364
	public global::IDLocalCharacterAddon overlay
	{
		get
		{
			return this.idMain.overlay;
		}
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x0002C174 File Offset: 0x0002A374
	public bool CreateOverlay()
	{
		return this.idMain.CreateOverlay();
	}
}
