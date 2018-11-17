using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x0200011D RID: 285
public sealed class Controllable : IDLocalCharacter
{
	// Token: 0x14000004 RID: 4
	// (add) Token: 0x060007BF RID: 1983 RVA: 0x00022540 File Offset: 0x00020740
	// (remove) Token: 0x060007C0 RID: 1984 RVA: 0x00022558 File Offset: 0x00020758
	public static event Controllable.DestroyInContextQuery onDestroyInContextQuery;

	// Token: 0x060007C1 RID: 1985 RVA: 0x00022570 File Offset: 0x00020770
	private void ON_CHAIN_RENEW()
	{
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x00022574 File Offset: 0x00020774
	private void ON_CHAIN_SUBSCRIBE()
	{
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x00022578 File Offset: 0x00020778
	private void ON_CHAIN_ERASE(int cmd)
	{
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x0002257C File Offset: 0x0002077C
	private void ON_CHAIN_ABOLISHED()
	{
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x00022580 File Offset: 0x00020780
	private static int CAP_THIS(int cmd, int RT, Controllable.ControlFlags F)
	{
		cmd &= -30721;
		if ((F & Controllable.ControlFlags.Strong) == (Controllable.ControlFlags)0)
		{
			cmd |= 0;
		}
		else if ((cmd & 32) == 32)
		{
			cmd |= 4097;
		}
		else
		{
			cmd |= 4096;
		}
		if ((F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player)) == Controllable.ControlFlags.Owned)
		{
			cmd |= 0;
		}
		else if ((F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player)) == (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player))
		{
			cmd |= 8192;
		}
		if ((F & Controllable.ControlFlags.Local) == Controllable.ControlFlags.Local)
		{
			cmd |= 16384;
		}
		else
		{
			cmd |= 0;
		}
		if ((F & Controllable.ControlFlags.Root) == Controllable.ControlFlags.Root)
		{
			cmd |= 2048;
		}
		else
		{
			cmd |= 0;
		}
		if ((RT & 3072) != 0 || (cmd & 4128) == 4128)
		{
			cmd |= 1;
		}
		return cmd;
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x0002264C File Offset: 0x0002084C
	private static int CAP_ENTER(int cmd, int RT, Controllable.ControlFlags F)
	{
		cmd = Controllable.CAP_THIS(cmd, RT, F);
		if ((RT & 64) == 64)
		{
			cmd |= ((cmd & -1025) | 1024);
		}
		else
		{
			cmd |= ((cmd & -1025) | 0);
		}
		return cmd;
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x00022688 File Offset: 0x00020888
	private static int CAP_PROMOTE(int cmd, int RT, Controllable.ControlFlags F)
	{
		cmd = Controllable.CAP_THIS(cmd, RT, F);
		if ((RT & 128) == 128)
		{
			cmd |= ((cmd & -1025) | 1024);
		}
		else
		{
			cmd |= ((cmd & -1025) | 0);
		}
		return cmd;
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x000226D4 File Offset: 0x000208D4
	private static int CAP_DEMOTE(int cmd, int RT, Controllable.ControlFlags F)
	{
		cmd = Controllable.CAP_THIS(cmd, RT, F);
		if ((RT & 256) == 256)
		{
			cmd = ((cmd & -1025) | 1024);
		}
		else
		{
			cmd = ((cmd & -1025) | 0);
		}
		return cmd;
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x00022714 File Offset: 0x00020914
	private static int CAP_EXIT(int cmd, int RT, Controllable.ControlFlags F)
	{
		if ((RT & 512) == 512)
		{
			cmd |= ((cmd & -1025) | 1024);
		}
		else
		{
			cmd |= ((cmd & -1025) | 0);
		}
		return cmd;
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x0002274C File Offset: 0x0002094C
	private static void DO_ENTER(int cmd, Controllable citr)
	{
		if ((citr.RT & 8) == 8)
		{
			return;
		}
		citr.RT |= 8;
		citr.ControlEnter(cmd);
		citr.RT = ((citr.RT & -12) | 65);
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x00022790 File Offset: 0x00020990
	private static void DO_PROMOTE(int cmd, Controllable citr)
	{
		if ((citr.RT & 16) == 16)
		{
			return;
		}
		citr.RT |= 16;
		citr.ControlEngauge(cmd);
		citr.RT = ((citr.RT & -20) | 131);
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x000227DC File Offset: 0x000209DC
	private static void DO_DEMOTE(int cmd, Controllable citr)
	{
		if ((citr.RT & 16) == 16)
		{
			return;
		}
		citr.RT |= 16;
		citr.ControlCease(cmd);
		citr.RT = ((citr.RT & -20) | 257);
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x00022828 File Offset: 0x00020A28
	private static void DO_EXIT(int cmd, Controllable citr)
	{
		if ((citr.RT & 8) == 8)
		{
			return;
		}
		citr.RT |= 8;
		citr.ControlExit(cmd);
		citr.RT = ((citr.RT & -12) | 512);
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x00022864 File Offset: 0x00020A64
	private void ClearBinder()
	{
		if (this._binder != null)
		{
			this._binder.Dispose();
		}
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x0002287C File Offset: 0x00020A7C
	private void CL_OverideControlOf(NetworkViewID rootViewID, NetworkViewID parentViewID, ref NetworkMessageInfo info)
	{
		this.ClearBinder();
		this._binder = new Controllable.CL_Binder(this, rootViewID, parentViewID, ref info);
		if (this._binder.CanLink())
		{
			this._binder.Link();
		}
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x000228BC File Offset: 0x00020ABC
	private void CL_RootControlCountSet(int count, ref NetworkMessageInfo info)
	{
		if (this._rootCountTimeStamps == null)
		{
			this._rootCountTimeStamps = new List<ulong>();
		}
		int count2 = this._rootCountTimeStamps.Count;
		if (count2 < count)
		{
			while (count2++ < count - 1)
			{
				this._rootCountTimeStamps.Add(ulong.MaxValue);
			}
			this._rootCountTimeStamps.Add(info.timestampInMillis);
		}
		else
		{
			if (count2 > count)
			{
				this._rootCountTimeStamps.RemoveRange(count, count2 - count);
			}
			this._rootCountTimeStamps[count - 1] = info.timestampInMillis;
		}
		this._pendingControlCount = count;
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x0002295C File Offset: 0x00020B5C
	private void CL_Refresh(int top)
	{
		this._refreshedControlCount = top;
		if (this._pendingControlCount > this._refreshedControlCount)
		{
			if (this._rootCountTimeStamps != null)
			{
				this._rootCountTimeStamps.RemoveRange(top, this._rootCountTimeStamps.Count - top);
			}
			this._pendingControlCount = top;
		}
		if (this.ch.su == top)
		{
			this.ch.RefreshEngauge();
		}
		else
		{
			Controllable.CL_Binder.StaticLink(this);
		}
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x000229D4 File Offset: 0x00020BD4
	private void CL_Clear()
	{
		this.ClearBinder();
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x000229DC File Offset: 0x00020BDC
	private void RN(int n, ref NetworkMessageInfo info)
	{
		this.CL_RootControlCountSet(n, ref info);
	}

	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x060007D4 RID: 2004 RVA: 0x000229E8 File Offset: 0x00020BE8
	public new Controller controller
	{
		get
		{
			return this._controller;
		}
	}

	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x060007D5 RID: 2005 RVA: 0x000229F0 File Offset: 0x00020BF0
	public new Controller controlledController
	{
		get
		{
			return ((this.F & Controllable.ControlFlags.Owned) != Controllable.ControlFlags.Owned) ? null : this._controller;
		}
	}

	// Token: 0x170001C6 RID: 454
	// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00022A0C File Offset: 0x00020C0C
	public new Controller playerControlledController
	{
		get
		{
			return ((this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player)) != (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player)) ? null : this._controller;
		}
	}

	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00022A28 File Offset: 0x00020C28
	public new Controller aiControlledController
	{
		get
		{
			return ((this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player)) != Controllable.ControlFlags.Owned) ? null : this._controller;
		}
	}

	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00022A44 File Offset: 0x00020C44
	public new Controller localPlayerControlledController
	{
		get
		{
			return ((this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player)) != (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player)) ? null : this._controller;
		}
	}

	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00022A60 File Offset: 0x00020C60
	public new Controller localAIControlledController
	{
		get
		{
			return ((this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player)) != (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local)) ? null : this._controller;
		}
	}

	// Token: 0x170001CA RID: 458
	// (get) Token: 0x060007DA RID: 2010 RVA: 0x00022A7C File Offset: 0x00020C7C
	public new Controller remotePlayerControlledController
	{
		get
		{
			return ((this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player)) != (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player)) ? null : this._controller;
		}
	}

	// Token: 0x170001CB RID: 459
	// (get) Token: 0x060007DB RID: 2011 RVA: 0x00022A98 File Offset: 0x00020C98
	public new Controller remoteAIControlledController
	{
		get
		{
			return ((this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player)) != Controllable.ControlFlags.Owned) ? null : this._controller;
		}
	}

	// Token: 0x170001CC RID: 460
	// (get) Token: 0x060007DC RID: 2012 RVA: 0x00022AB4 File Offset: 0x00020CB4
	public new Controllable controllable
	{
		get
		{
			return this;
		}
	}

	// Token: 0x170001CD RID: 461
	// (get) Token: 0x060007DD RID: 2013 RVA: 0x00022AB8 File Offset: 0x00020CB8
	public new Controllable controlledControllable
	{
		get
		{
			return ((this.F & Controllable.ControlFlags.Owned) != Controllable.ControlFlags.Owned) ? null : this;
		}
	}

	// Token: 0x170001CE RID: 462
	// (get) Token: 0x060007DE RID: 2014 RVA: 0x00022AD0 File Offset: 0x00020CD0
	public new Controllable playerControlledControllable
	{
		get
		{
			return ((this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player)) != (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player)) ? null : this;
		}
	}

	// Token: 0x170001CF RID: 463
	// (get) Token: 0x060007DF RID: 2015 RVA: 0x00022AE8 File Offset: 0x00020CE8
	public new Controllable aiControlledControllable
	{
		get
		{
			return ((this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player)) != Controllable.ControlFlags.Owned) ? null : this;
		}
	}

	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00022B00 File Offset: 0x00020D00
	public new Controllable localPlayerControlledControllable
	{
		get
		{
			return ((this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player)) != (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player)) ? null : this;
		}
	}

	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00022B18 File Offset: 0x00020D18
	public new Controllable localAIControlledControllable
	{
		get
		{
			return ((this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player)) != (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local)) ? null : this;
		}
	}

	// Token: 0x170001D2 RID: 466
	// (get) Token: 0x060007E2 RID: 2018 RVA: 0x00022B30 File Offset: 0x00020D30
	public new Controllable remotePlayerControlledControllable
	{
		get
		{
			return ((this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player)) != (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player)) ? null : this;
		}
	}

	// Token: 0x170001D3 RID: 467
	// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00022B48 File Offset: 0x00020D48
	public new Controllable remoteAIControlledControllable
	{
		get
		{
			return ((this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player)) != Controllable.ControlFlags.Owned) ? null : this;
		}
	}

	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00022B60 File Offset: 0x00020D60
	public new PlayerClient playerClient
	{
		get
		{
			return this._playerClient;
		}
	}

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00022B68 File Offset: 0x00020D68
	public NetworkPlayer netPlayer
	{
		get
		{
			return (!this._playerClient) ? NetworkPlayer.unassigned : this._playerClient.netPlayer;
		}
	}

	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00022B90 File Offset: 0x00020D90
	public new bool controlled
	{
		get
		{
			return (this.F & Controllable.ControlFlags.Owned) == Controllable.ControlFlags.Owned;
		}
	}

	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00022BA0 File Offset: 0x00020DA0
	public new bool playerControlled
	{
		get
		{
			return (this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player)) == (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player);
		}
	}

	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00022BB0 File Offset: 0x00020DB0
	public new bool aiControlled
	{
		get
		{
			return (this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player)) == Controllable.ControlFlags.Owned;
		}
	}

	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00022BC0 File Offset: 0x00020DC0
	public new bool localPlayerControlled
	{
		get
		{
			return (this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player)) == (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player);
		}
	}

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x060007EA RID: 2026 RVA: 0x00022BD0 File Offset: 0x00020DD0
	public new bool remotePlayerControlled
	{
		get
		{
			return (this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player)) == (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player);
		}
	}

	// Token: 0x170001DB RID: 475
	// (get) Token: 0x060007EB RID: 2027 RVA: 0x00022BE0 File Offset: 0x00020DE0
	public new bool localAIControlled
	{
		get
		{
			return (this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player)) == (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local);
		}
	}

	// Token: 0x170001DC RID: 476
	// (get) Token: 0x060007EC RID: 2028 RVA: 0x00022BF0 File Offset: 0x00020DF0
	public new bool remoteAIControlled
	{
		get
		{
			return (this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player)) == Controllable.ControlFlags.Owned;
		}
	}

	// Token: 0x170001DD RID: 477
	// (get) Token: 0x060007ED RID: 2029 RVA: 0x00022C00 File Offset: 0x00020E00
	public new bool localControlled
	{
		get
		{
			return (this.F & Controllable.ControlFlags.Local) == Controllable.ControlFlags.Local;
		}
	}

	// Token: 0x170001DE RID: 478
	// (get) Token: 0x060007EE RID: 2030 RVA: 0x00022C10 File Offset: 0x00020E10
	public new bool remoteControlled
	{
		get
		{
			return (this.F & Controllable.ControlFlags.Local) == (Controllable.ControlFlags)0;
		}
	}

	// Token: 0x170001DF RID: 479
	// (get) Token: 0x060007EF RID: 2031 RVA: 0x00022C20 File Offset: 0x00020E20
	public bool core
	{
		get
		{
			return (this.F & Controllable.ControlFlags.Root) == Controllable.ControlFlags.Root;
		}
	}

	// Token: 0x170001E0 RID: 480
	// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00022C30 File Offset: 0x00020E30
	public bool vessel
	{
		get
		{
			return (this.F & Controllable.ControlFlags.Root) == (Controllable.ControlFlags)0;
		}
	}

	// Token: 0x170001E1 RID: 481
	// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00022C40 File Offset: 0x00020E40
	public new string npcName
	{
		get
		{
			return (!this.@class) ? null : this.@class.npcName;
		}
	}

	// Token: 0x170001E2 RID: 482
	// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00022C64 File Offset: 0x00020E64
	public new bool controlOverridden
	{
		get
		{
			return this.ch.vl && this.ch.ln > 0;
		}
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x00022C88 File Offset: 0x00020E88
	public new bool ControlOverriddenBy(Controllable controllable)
	{
		return this.ch.vl && this.ch.ln > 0 && controllable && controllable.ch.vl && this.ch.ln > controllable.ch.ln && this.ch.bt == controllable.ch.bt;
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x00022D0C File Offset: 0x00020F0C
	public new bool ControlOverriddenBy(Controller controller)
	{
		Controllable controllable;
		return this.ch.vl && this.ch.ln > 0 && controller && (controllable = controller.controllable) && controllable.ch.vl && this.ch.ln > controllable.ch.ln && this.ch.bt == controllable.ch.bt;
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x00022DA0 File Offset: 0x00020FA0
	public new bool ControlOverriddenBy(Character character)
	{
		Controllable controllable;
		return this.ch.vl && this.ch.ln > 0 && character && (controllable = character.controllable) && controllable.ch.vl && this.ch.ln > controllable.ch.ln && this.ch.bt == controllable.ch.bt;
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x00022E34 File Offset: 0x00021034
	public new bool ControlOverriddenBy(IDMain main)
	{
		return this.ch.vl && this.ch.ln != 0 && main is Character && this.ControlOverriddenBy((Character)main);
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x00022E70 File Offset: 0x00021070
	public new bool ControlOverriddenBy(IDBase idBase)
	{
		return this.ch.vl && this.ch.ln != 0 && idBase && this.ControlOverriddenBy(idBase.idMain);
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x00022EAC File Offset: 0x000210AC
	public new bool ControlOverriddenBy(IDLocalCharacter idLocal)
	{
		return this.ch.vl && this.ch.ln != 0 && idLocal && this.ControlOverriddenBy(idLocal.idMain);
	}

	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00022EE8 File Offset: 0x000210E8
	public new bool overridingControl
	{
		get
		{
			return this.ch.vl && this.ch.nm > 0;
		}
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x00022F0C File Offset: 0x0002110C
	public new bool OverridingControlOf(Controllable controllable)
	{
		return this.ch.vl && this.ch.nm > 0 && controllable && controllable.ch.vl && this.ch.nm > controllable.ch.nm && this.ch.bt == controllable.ch.bt;
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x00022F90 File Offset: 0x00021190
	public new bool OverridingControlOf(Controller controller)
	{
		Controllable controllable;
		return this.ch.vl && this.ch.nm > 0 && controller && (controllable = controller.controllable) && controllable.ch.vl && this.ch.nm > controllable.ch.nm && this.ch.bt == controllable.ch.bt;
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x00023024 File Offset: 0x00021224
	public new bool OverridingControlOf(Character character)
	{
		Controllable controllable;
		return this.ch.vl && this.ch.nm > 0 && character && (controllable = character.controllable) && controllable.ch.vl && this.ch.nm > controllable.ch.nm && this.ch.bt == controllable.ch.bt;
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x000230B8 File Offset: 0x000212B8
	public new bool OverridingControlOf(IDMain main)
	{
		return this.ch.vl && this.ch.nm != 0 && main is Character && this.OverridingControlOf((Character)main);
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x000230F4 File Offset: 0x000212F4
	public new bool OverridingControlOf(IDBase idBase)
	{
		return this.ch.vl && this.ch.nm != 0 && idBase && this.OverridingControlOf(idBase.idMain);
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x00023130 File Offset: 0x00021330
	public new bool OverridingControlOf(IDLocalCharacter idLocal)
	{
		return this.ch.vl && this.ch.nm != 0 && idLocal && this.OverridingControlOf(idLocal.idMain);
	}

	// Token: 0x170001E4 RID: 484
	// (get) Token: 0x06000800 RID: 2048 RVA: 0x0002316C File Offset: 0x0002136C
	public new bool assignedControl
	{
		get
		{
			return this.ch.vl;
		}
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x0002317C File Offset: 0x0002137C
	public new bool AssignedControlOf(Controllable controllable)
	{
		return this.ch.vl && this == controllable;
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x00023198 File Offset: 0x00021398
	public new bool AssignedControlOf(Controller controller)
	{
		return this.ch.vl && this._controller == controller && this._controller;
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x000231CC File Offset: 0x000213CC
	public new bool AssignedControlOf(IDMain character)
	{
		return this.ch.vl && this.idMain == character;
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x000231F0 File Offset: 0x000213F0
	public new bool AssignedControlOf(IDBase idBase)
	{
		return this.ch.vl && idBase && this.idMain == idBase.idMain;
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x00023224 File Offset: 0x00021424
	public new RelativeControl RelativeControlTo(Controllable controllable)
	{
		if (!this.ch.vl || !controllable || !controllable.ch.vl || controllable.ch.bt != this.ch.bt)
		{
			return RelativeControl.Incompatible;
		}
		if (this.ch.ln > controllable.ch.ln)
		{
			return RelativeControl.OverriddenBy;
		}
		if (this.ch.ln < controllable.ch.ln)
		{
			return RelativeControl.IsOverriding;
		}
		return RelativeControl.Assigned;
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x000232BC File Offset: 0x000214BC
	public new RelativeControl RelativeControlTo(Controller controller)
	{
		Controllable controllable;
		if (!this.ch.vl || !controller || !(controllable = controller.controllable) || controllable.ch.vl || controllable.ch.bt != this.ch.bt)
		{
			return RelativeControl.Incompatible;
		}
		if (this.ch.ln > controllable.ch.ln)
		{
			return RelativeControl.OverriddenBy;
		}
		if (this.ch.ln < controllable.ch.ln)
		{
			return RelativeControl.IsOverriding;
		}
		return RelativeControl.Assigned;
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x00023364 File Offset: 0x00021564
	public new RelativeControl RelativeControlTo(Character character)
	{
		if (!character)
		{
			return RelativeControl.Incompatible;
		}
		return this.RelativeControlTo(character.controllable);
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x00023380 File Offset: 0x00021580
	public new RelativeControl RelativeControlTo(IDMain idMain)
	{
		if (!(idMain is Character))
		{
			return RelativeControl.Incompatible;
		}
		return this.RelativeControlTo((Character)idMain);
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x0002339C File Offset: 0x0002159C
	public new RelativeControl RelativeControlTo(IDLocalCharacter idLocal)
	{
		if (!idLocal)
		{
			return RelativeControl.Incompatible;
		}
		return this.RelativeControlTo(idLocal.idMain.controllable);
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x000233C8 File Offset: 0x000215C8
	public new RelativeControl RelativeControlTo(IDBase idBase)
	{
		if (!idBase)
		{
			return RelativeControl.Incompatible;
		}
		return this.RelativeControlTo(idBase.idMain as Character);
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x000233E8 File Offset: 0x000215E8
	public new RelativeControl RelativeControlFrom(Controllable controllable)
	{
		if (!this.ch.vl || !controllable || !controllable.ch.vl || controllable.ch.bt != this.ch.bt)
		{
			return RelativeControl.Incompatible;
		}
		if (this.ch.ln > controllable.ch.ln)
		{
			return RelativeControl.IsOverriding;
		}
		if (this.ch.ln < controllable.ch.ln)
		{
			return RelativeControl.OverriddenBy;
		}
		return RelativeControl.Assigned;
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x00023480 File Offset: 0x00021680
	public new RelativeControl RelativeControlFrom(Controller controller)
	{
		Controllable controllable;
		if (!this.ch.vl || !controller || !(controllable = controller.controllable) || controllable.ch.vl || controllable.ch.bt != this.ch.bt)
		{
			return RelativeControl.Incompatible;
		}
		if (this.ch.ln > controllable.ch.ln)
		{
			return RelativeControl.IsOverriding;
		}
		if (this.ch.ln < controllable.ch.ln)
		{
			return RelativeControl.OverriddenBy;
		}
		return RelativeControl.Assigned;
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x00023528 File Offset: 0x00021728
	public new RelativeControl RelativeControlFrom(Character character)
	{
		if (!character)
		{
			return RelativeControl.Incompatible;
		}
		return this.RelativeControlFrom(character.controllable);
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x00023544 File Offset: 0x00021744
	public new RelativeControl RelativeControlFrom(IDMain idMain)
	{
		if (!(idMain is Character))
		{
			return RelativeControl.Incompatible;
		}
		return this.RelativeControlFrom((Character)idMain);
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00023560 File Offset: 0x00021760
	public new RelativeControl RelativeControlFrom(IDLocalCharacter idLocal)
	{
		if (!idLocal)
		{
			return RelativeControl.Incompatible;
		}
		return this.RelativeControlFrom(idLocal.idMain.controllable);
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x0002358C File Offset: 0x0002178C
	public new RelativeControl RelativeControlFrom(IDBase idBase)
	{
		if (!idBase)
		{
			return RelativeControl.Incompatible;
		}
		return this.RelativeControlFrom(idBase.idMain as Character);
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x000235AC File Offset: 0x000217AC
	internal void PrepareInstantiate(NetworkView view, ref NetworkMessageInfo info)
	{
		this.__controllerCreateMessageInfo = info;
		this.__networkViewForControllable = view;
		if (this.classFlagsRootControllable || this.classFlagsStandaloneVessel)
		{
			this.__controllerDriverViewID = NetworkViewID.unassigned;
			if (this.classFlagsStandaloneVessel)
			{
				return;
			}
		}
		else if (this.classFlagsDependantVessel || this.classFlagsFreeVessel)
		{
			PlayerClient playerClient;
			if (PlayerClient.Find(view.owner, out playerClient))
			{
				this.__controllerDriverViewID = playerClient.topControllable.networkViewID;
			}
			else
			{
				this.__controllerDriverViewID = NetworkViewID.unassigned;
			}
			if (this.classFlagsFreeVessel)
			{
				return;
			}
			if (this.__controllerDriverViewID == NetworkViewID.unassigned)
			{
				Debug.LogError("NOT RIGHT");
				return;
			}
		}
		this.FreshInitializeController();
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00023678 File Offset: 0x00021878
	internal void FreshInitializeController()
	{
		if (this.__controllerDriverViewID == NetworkViewID.unassigned)
		{
			if ((this.F & Controllable.ControlFlags.Initialized) == Controllable.ControlFlags.Initialized)
			{
				throw new InvalidOperationException("Was already intialized.");
			}
			Controllable.Chain.ROOT(this);
			this.F = Controllable.ControlFlags.Root;
			this.InitializeController_OnFoundOverriding(null);
		}
		else
		{
			NetworkView driverView = NetworkView.Find(this.__controllerDriverViewID);
			this.F |= (Controllable.ControlFlags)0;
			this.InitializeController_OnFoundOverriding(driverView);
		}
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x000236F0 File Offset: 0x000218F0
	public void ClientExit()
	{
		if (!this.ch.vl)
		{
			return;
		}
		if (this.ch.vl && this.ch.bt == this.ch.it)
		{
			Debug.LogWarning("You cannot exit the root controllable", this);
			return;
		}
		if (!this.localControlled)
		{
			throw new InvalidOperationException("Cannot exit other owned controllables");
		}
		base.networkView.RPC("Controllable:CLD", 0, new object[0]);
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00023778 File Offset: 0x00021978
	private bool EnsureControllee(NetworkPlayer player)
	{
		if (!this.controlled)
		{
			return false;
		}
		if (player.isClient)
		{
			if (!this.playerControlled || (this.playerClient && this.playerClient.netPlayer != player))
			{
				Debug.LogWarning("player was not the controllee of this player controlled controlable", this);
				return false;
			}
		}
		else if (this.playerControlled)
		{
			Debug.LogWarning("this player controlled controlable is not server owned", this);
			return false;
		}
		return true;
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x000237FC File Offset: 0x000219FC
	private void InitializeController_OnFoundOverriding(NetworkView driverView)
	{
		if ((this.F & Controllable.ControlFlags.Root) == (Controllable.ControlFlags)0)
		{
			Character character = driverView.idMain as Character;
			Controllable controllable = character.controllable;
			this.F = ((this.F & (Controllable.ControlFlags.Root | Controllable.ControlFlags.Strong)) | (controllable.F & (Controllable.ControlFlags.Local | Controllable.ControlFlags.Player)));
			this._playerClient = controllable.playerClient;
			controllable.ch.Add(this);
		}
		else
		{
			this.F |= ((!this.__networkViewForControllable.isMine) ? ((Controllable.ControlFlags)0) : Controllable.ControlFlags.Local);
			this.F |= ((!PlayerClient.Find(this.__networkViewForControllable.owner, out this._playerClient)) ? Controllable.ControlFlags.Owned : (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player));
		}
		this.F |= Controllable.ControlFlags.Owned;
		string controllerClassName = this.controllerClassName;
		if (string.IsNullOrEmpty(controllerClassName))
		{
			Controllable.ControlFlags f = this.F;
			this.F = (Controllable.ControlFlags)0;
			throw new ArgumentOutOfRangeException("@class", f, "The ControllerClass did not support given flags");
		}
		Controller controller = null;
		try
		{
			controller = base.AddAddon<Controller>(controllerClassName);
			if (!controller)
			{
				throw new ArgumentOutOfRangeException("className", controllerClassName, "classname as not a Controller!");
			}
			this._controller = controller;
			Controller controller2 = this._controller;
			try
			{
				try
				{
					this._controller.ControllerSetup(this, this.__networkViewForControllable, ref this.__controllerCreateMessageInfo);
				}
				catch
				{
					this._controller = controller2;
					throw;
				}
			}
			catch
			{
				throw;
			}
			this.F |= Controllable.ControlFlags.Initialized;
		}
		catch
		{
			if (controller)
			{
				Object.Destroy(controller);
			}
			this.ch.Delete();
			throw;
		}
	}

	// Token: 0x170001E5 RID: 485
	// (get) Token: 0x06000816 RID: 2070 RVA: 0x000239F0 File Offset: 0x00021BF0
	public bool forwardsPlayerClientInput
	{
		get
		{
			return this._controller && this._controller.forwardsPlayerClientInput;
		}
	}

	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x06000817 RID: 2071 RVA: 0x00023A10 File Offset: 0x00021C10
	public bool doesNotSave
	{
		get
		{
			return !this._controller || this._controller.doesNotSave;
		}
	}

	// Token: 0x170001E7 RID: 487
	// (get) Token: 0x06000818 RID: 2072 RVA: 0x00023A30 File Offset: 0x00021C30
	public new Controllable masterControllable
	{
		get
		{
			return (!this.ch.vl) ? null : this.ch.tp;
		}
	}

	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x06000819 RID: 2073 RVA: 0x00023A54 File Offset: 0x00021C54
	public new Controllable rootControllable
	{
		get
		{
			return (!this.ch.vl) ? null : this.ch.bt;
		}
	}

	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x0600081A RID: 2074 RVA: 0x00023A78 File Offset: 0x00021C78
	public new Controllable nextControllable
	{
		get
		{
			return (!this.ch.vl || !this.ch.up.vl) ? null : this.ch.up.it;
		}
	}

	// Token: 0x170001EA RID: 490
	// (get) Token: 0x0600081B RID: 2075 RVA: 0x00023AB8 File Offset: 0x00021CB8
	public new Controllable previousControllable
	{
		get
		{
			return (!this.ch.vl || !this.ch.dn.vl) ? null : this.ch.dn.it;
		}
	}

	// Token: 0x170001EB RID: 491
	// (get) Token: 0x0600081C RID: 2076 RVA: 0x00023AF8 File Offset: 0x00021CF8
	public new Controller masterController
	{
		get
		{
			return (!this.ch.vl) ? null : this.ch.tp._controller;
		}
	}

	// Token: 0x170001EC RID: 492
	// (get) Token: 0x0600081D RID: 2077 RVA: 0x00023B2C File Offset: 0x00021D2C
	public new Controller rootController
	{
		get
		{
			return (!this.ch.vl) ? null : this.ch.bt._controller;
		}
	}

	// Token: 0x170001ED RID: 493
	// (get) Token: 0x0600081E RID: 2078 RVA: 0x00023B60 File Offset: 0x00021D60
	public new Controller nextController
	{
		get
		{
			return (!this.ch.vl || !this.ch.up.vl) ? null : this.ch.up.it._controller;
		}
	}

	// Token: 0x170001EE RID: 494
	// (get) Token: 0x0600081F RID: 2079 RVA: 0x00023BB0 File Offset: 0x00021DB0
	public new Controller previousController
	{
		get
		{
			return (!this.ch.vl || !this.ch.dn.vl) ? null : this.ch.dn.it._controller;
		}
	}

	// Token: 0x170001EF RID: 495
	// (get) Token: 0x06000820 RID: 2080 RVA: 0x00023C00 File Offset: 0x00021E00
	public new Character masterCharacter
	{
		get
		{
			return (!this.ch.vl) ? null : this.ch.tp.idMain;
		}
	}

	// Token: 0x170001F0 RID: 496
	// (get) Token: 0x06000821 RID: 2081 RVA: 0x00023C34 File Offset: 0x00021E34
	public new Character rootCharacter
	{
		get
		{
			return (!this.ch.vl) ? null : this.ch.bt.idMain;
		}
	}

	// Token: 0x170001F1 RID: 497
	// (get) Token: 0x06000822 RID: 2082 RVA: 0x00023C68 File Offset: 0x00021E68
	public new Character nextCharacter
	{
		get
		{
			return (!this.ch.vl || !this.ch.up.vl) ? null : this.ch.up.it.idMain;
		}
	}

	// Token: 0x170001F2 RID: 498
	// (get) Token: 0x06000823 RID: 2083 RVA: 0x00023CB8 File Offset: 0x00021EB8
	public new Character previousCharacter
	{
		get
		{
			return (!this.ch.vl || !this.ch.dn.vl) ? null : this.ch.dn.it.idMain;
		}
	}

	// Token: 0x170001F3 RID: 499
	// (get) Token: 0x06000824 RID: 2084 RVA: 0x00023D08 File Offset: 0x00021F08
	public new int controlDepth
	{
		get
		{
			return this.ch.id;
		}
	}

	// Token: 0x170001F4 RID: 500
	// (get) Token: 0x06000825 RID: 2085 RVA: 0x00023D18 File Offset: 0x00021F18
	public new int controlCount
	{
		get
		{
			return this.ch.su;
		}
	}

	// Token: 0x170001F5 RID: 501
	// (get) Token: 0x06000826 RID: 2086 RVA: 0x00023D28 File Offset: 0x00021F28
	internal bool classAssigned
	{
		get
		{
			return this.@class;
		}
	}

	// Token: 0x170001F6 RID: 502
	// (get) Token: 0x06000827 RID: 2087 RVA: 0x00023D38 File Offset: 0x00021F38
	internal bool classFlagsRootControllable
	{
		get
		{
			return this.@class && this.@class.root;
		}
	}

	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x06000828 RID: 2088 RVA: 0x00023D58 File Offset: 0x00021F58
	internal bool classFlagsVessel
	{
		get
		{
			return this.@class && this.@class.vessel;
		}
	}

	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x06000829 RID: 2089 RVA: 0x00023D78 File Offset: 0x00021F78
	internal bool classFlagsStandaloneVessel
	{
		get
		{
			return this.@class && this.@class.vesselStandalone;
		}
	}

	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x0600082A RID: 2090 RVA: 0x00023D98 File Offset: 0x00021F98
	internal bool classFlagsDependantVessel
	{
		get
		{
			return this.@class && this.@class.vesselDependant;
		}
	}

	// Token: 0x170001FA RID: 506
	// (get) Token: 0x0600082B RID: 2091 RVA: 0x00023DB8 File Offset: 0x00021FB8
	internal bool classFlagsFreeVessel
	{
		get
		{
			return this.@class && this.@class.vesselFree;
		}
	}

	// Token: 0x170001FB RID: 507
	// (get) Token: 0x0600082C RID: 2092 RVA: 0x00023DD8 File Offset: 0x00021FD8
	internal bool classFlagsStaticGroup
	{
		get
		{
			return this.@class && this.@class.staticGroup;
		}
	}

	// Token: 0x170001FC RID: 508
	// (get) Token: 0x0600082D RID: 2093 RVA: 0x00023DF8 File Offset: 0x00021FF8
	internal bool classFlagsPlayerSupport
	{
		get
		{
			return this.@class && this.@class.DefinesClass(true);
		}
	}

	// Token: 0x170001FD RID: 509
	// (get) Token: 0x0600082E RID: 2094 RVA: 0x00023E1C File Offset: 0x0002201C
	internal bool classFlagsAISupport
	{
		get
		{
			return this.@class && this.@class.DefinesClass(false);
		}
	}

	// Token: 0x170001FE RID: 510
	// (get) Token: 0x0600082F RID: 2095 RVA: 0x00023E40 File Offset: 0x00022040
	public new string controllerClassName
	{
		get
		{
			return (!this.@class) ? null : this.@class.GetClassName((this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player)) == (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player), (this.F & Controllable.ControlFlags.Local) == Controllable.ControlFlags.Local);
		}
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x00023E7C File Offset: 0x0002207C
	internal void ProcessLocalPlayerPreRender()
	{
		this._controller.ProcessLocalPlayerPreRender();
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x00023E8C File Offset: 0x0002208C
	[Conditional("LOG_CONTROL_CHANGE")]
	private static void LogState(bool guard, string state, Controllable controllable)
	{
		Debug.Log(string.Format("{2}{0}::{1}", controllable.GetType().Name, state, (!guard) ? "-" : "+"), controllable);
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x00023ECC File Offset: 0x000220CC
	[Conditional("LOG_CONTROL_CHANGE")]
	private static void GuardState(string state, Controllable self)
	{
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x00023ED0 File Offset: 0x000220D0
	[Conditional("LOG_CONTROL_CHANGE")]
	private static void UnguardState(string state, Controllable self)
	{
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x00023ED4 File Offset: 0x000220D4
	private void ControlEnter(int cmd)
	{
		try
		{
			this._controller.ControlEnter(cmd);
		}
		finally
		{
			if ((this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player | Controllable.ControlFlags.Root)) == (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player | Controllable.ControlFlags.Root))
			{
				try
				{
					this._playerClient.OnRootControllableEntered(this);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex, this);
				}
				if ((this.F & Controllable.ControlFlags.Local) == Controllable.ControlFlags.Local)
				{
					Controllable.localPlayerControllableCount++;
					Controllable.LocalOnly.rootLocalPlayerControllables.Add(this);
				}
			}
		}
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x00023F7C File Offset: 0x0002217C
	private void ControlExit(int cmd)
	{
		try
		{
			this._controller.ControlExit(cmd);
		}
		finally
		{
			if ((this.F & (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player | Controllable.ControlFlags.Root)) == (Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player | Controllable.ControlFlags.Root))
			{
				if (this._playerClient)
				{
					try
					{
						this._playerClient.OnRootControllableExited(this);
					}
					catch (Exception ex)
					{
						Debug.LogError(ex, this);
					}
				}
				if ((this.F & Controllable.ControlFlags.Local) == Controllable.ControlFlags.Local)
				{
					Controllable.localPlayerControllableCount--;
					Controllable.LocalOnly.rootLocalPlayerControllables.Remove(this);
				}
			}
		}
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x00024034 File Offset: 0x00022234
	private void Net_Shutdown_Exit()
	{
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x00024038 File Offset: 0x00022238
	private void ControlEngauge(int cmd)
	{
		this._controller.ControlEngauge(cmd);
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x00024048 File Offset: 0x00022248
	private void ControlCease(int cmd)
	{
		this._controller.ControlCease(cmd);
	}

	// Token: 0x170001FF RID: 511
	// (get) Token: 0x06000839 RID: 2105 RVA: 0x00024058 File Offset: 0x00022258
	public new RPOSLimitFlags rposLimitFlags
	{
		get
		{
			return (!this._controller) ? ((RPOSLimitFlags)(-1)) : this._controller.rposLimitFlags;
		}
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x0002407C File Offset: 0x0002227C
	[Obsolete("Used only by PlayerClient")]
	internal void SetRootPlayer(PlayerClient rootPlayer)
	{
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x00024080 File Offset: 0x00022280
	private bool SetIdle(bool idle)
	{
		IDLocalCharacterIdleControl idleControl = base.idMain.idleControl;
		if (idleControl)
		{
			try
			{
				return idleControl.SetIdle(idle);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex, idleControl);
				return true;
			}
			return false;
		}
		return false;
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x000240EC File Offset: 0x000222EC
	[RPC]
	[Obsolete("RPC call only. Do not call through script", false)]
	private void CLD(NetworkMessageInfo info)
	{
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x000240F0 File Offset: 0x000222F0
	[Obsolete("RPC call only. Do not call through script", false)]
	[RPC]
	private void CLR(NetworkMessageInfo info)
	{
		this.ch.Delete();
		this.SharedPostCLR();
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x00024104 File Offset: 0x00022304
	private void SharedPostCLR()
	{
		if (this._controller)
		{
			Object.Destroy(this._controller);
		}
		this.F &= (Controllable.ControlFlags.Root | Controllable.ControlFlags.Strong);
		this.RT = 0;
		this._playerClient = null;
		this._controller = null;
		this.SetIdle(true);
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x00024158 File Offset: 0x00022358
	[Obsolete("RPC call only. Do not call through script", false)]
	[RPC]
	private void ID1()
	{
		this.SetIdle(true);
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x00024164 File Offset: 0x00022364
	[RPC]
	private void OC2(NetworkViewID rootViewID, NetworkViewID parentViewID, NetworkMessageInfo info)
	{
		this.OverrideControlOfHandleRPC(rootViewID, parentViewID, ref info);
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x00024170 File Offset: 0x00022370
	[RPC]
	private void OC1(NetworkViewID rootViewID, NetworkMessageInfo info)
	{
		this.OverrideControlOfHandleRPC(rootViewID, rootViewID, ref info);
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x0002417C File Offset: 0x0002237C
	private void OverrideControlOfHandleRPC(NetworkViewID rootViewID, NetworkViewID parentViewID, ref NetworkMessageInfo info)
	{
		this.CL_OverideControlOf(rootViewID, parentViewID, ref info);
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x00024188 File Offset: 0x00022388
	[RPC]
	private void RN0(NetworkMessageInfo info)
	{
		this.RN(0, ref info);
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x00024194 File Offset: 0x00022394
	[RPC]
	private void RN1(NetworkMessageInfo info)
	{
		this.RN(1, ref info);
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x000241A0 File Offset: 0x000223A0
	[RPC]
	private void RN2(NetworkMessageInfo info)
	{
		this.RN(2, ref info);
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x000241AC File Offset: 0x000223AC
	[RPC]
	private void RN3(NetworkMessageInfo info)
	{
		this.RN(3, ref info);
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x000241B8 File Offset: 0x000223B8
	[RPC]
	private void RN4(NetworkMessageInfo info)
	{
		this.RN(4, ref info);
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x000241C4 File Offset: 0x000223C4
	[RPC]
	private void RN5(NetworkMessageInfo info)
	{
		this.RN(5, ref info);
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x000241D0 File Offset: 0x000223D0
	[RPC]
	private void RN6(NetworkMessageInfo info)
	{
		this.RN(6, ref info);
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x000241DC File Offset: 0x000223DC
	[RPC]
	private void RN7(NetworkMessageInfo info)
	{
		this.RN(7, ref info);
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x000241E8 File Offset: 0x000223E8
	[RPC]
	private void RFH(byte top)
	{
		this.CL_Refresh((int)top);
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x000241F4 File Offset: 0x000223F4
	internal void OnInstantiated()
	{
		if ((this.F & Controllable.ControlFlags.Root) == Controllable.ControlFlags.Root)
		{
			this.ch.RefreshEngauge();
		}
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x00024210 File Offset: 0x00022410
	private void OCO_FOUND(NetworkViewID viewID, ref NetworkMessageInfo info)
	{
		this.SetIdle(false);
		this.__networkViewForControllable = base.networkView;
		this.__controllerDriverViewID = viewID;
		this.__controllerCreateMessageInfo = info;
		this.FreshInitializeController();
	}

	// Token: 0x17000200 RID: 512
	// (get) Token: 0x0600084E RID: 2126 RVA: 0x0002423C File Offset: 0x0002243C
	public static bool localPlayerControllableExists
	{
		get
		{
			return Controllable.localPlayerControllableCount > 0;
		}
	}

	// Token: 0x17000201 RID: 513
	// (get) Token: 0x0600084F RID: 2127 RVA: 0x00024248 File Offset: 0x00022448
	public static Controllable localPlayerControllable
	{
		get
		{
			int num = Controllable.localPlayerControllableCount;
			if (num == 0)
			{
				return null;
			}
			if (num != 1)
			{
				return Controllable.LocalOnly.rootLocalPlayerControllables[Controllable.localPlayerControllableCount - 1];
			}
			return Controllable.LocalOnly.rootLocalPlayerControllables[0];
		}
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x0002428C File Offset: 0x0002248C
	private void OnDestroy()
	{
		this.CL_Clear();
		if (this.isInContextQuery)
		{
			try
			{
				if (Controllable.onDestroyInContextQuery != null)
				{
					Controllable.onDestroyInContextQuery(this);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex, this);
			}
			finally
			{
				this.isInContextQuery = false;
			}
		}
		this.RT |= 2048;
		if ((this.RT & 1056) == 0)
		{
			this.DoDestroy();
		}
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x00024338 File Offset: 0x00022538
	private void DoDestroy()
	{
		this.CL_Clear();
		try
		{
			this.RT |= 32;
			if ((this.RT & 3) != 0)
			{
				this.ch.Delete();
			}
		}
		finally
		{
			this.RT &= -33;
		}
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x000243A4 File Offset: 0x000225A4
	internal bool MergeClasses(ref ControllerClass.Merge merge)
	{
		return this.@class && merge.Add(this.controllable.@class);
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x000243D8 File Offset: 0x000225D8
	internal static bool MergeClasses(IDMain character, ref ControllerClass.Merge merge)
	{
		Controllable component;
		return character && (component = character.GetComponent<Controllable>()) && component.MergeClasses(ref merge);
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x06000854 RID: 2132 RVA: 0x0002440C File Offset: 0x0002260C
	public static IEnumerable<Controllable> PlayerRootControllables
	{
		get
		{
			foreach (PlayerClient pc in PlayerClient.All)
			{
				Controllable controllable = pc.rootControllable;
				if (controllable)
				{
					yield return controllable;
				}
			}
			yield break;
		}
	}

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x06000855 RID: 2133 RVA: 0x00024428 File Offset: 0x00022628
	public static IEnumerable<Controllable> PlayerCurrentControllables
	{
		get
		{
			foreach (PlayerClient pc in PlayerClient.All)
			{
				Controllable controllable = pc.controllable;
				if (controllable)
				{
					yield return controllable;
				}
			}
			yield break;
		}
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x00024444 File Offset: 0x00022644
	public static IEnumerable<Controllable> RootControllers(IEnumerable<PlayerClient> playerClients)
	{
		foreach (PlayerClient pc in playerClients)
		{
			Controllable controllable = pc.rootControllable;
			if (controllable)
			{
				yield return controllable;
			}
		}
		yield break;
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x00024470 File Offset: 0x00022670
	public static IEnumerable<Controllable> CurrentControllers(IEnumerable<PlayerClient> playerClients)
	{
		foreach (PlayerClient pc in playerClients)
		{
			Controllable controllable = pc.controllable;
			if (controllable)
			{
				yield return controllable;
			}
		}
		yield break;
	}

	// Token: 0x04000570 RID: 1392
	private const int RT_ENTERED = 1;

	// Token: 0x04000571 RID: 1393
	private const int RT_PROMOTED = 3;

	// Token: 0x04000572 RID: 1394
	private const int RT_ENTER_LOCK = 8;

	// Token: 0x04000573 RID: 1395
	private const int RT_PROMO_LOCK = 16;

	// Token: 0x04000574 RID: 1396
	private const int RT_DESTROY_LOCK = 32;

	// Token: 0x04000575 RID: 1397
	private const int RT_ENTERED_ONCE = 64;

	// Token: 0x04000576 RID: 1398
	private const int RT_PROMOTED_ONCE = 128;

	// Token: 0x04000577 RID: 1399
	private const int RT_DEMOTED_ONCE = 256;

	// Token: 0x04000578 RID: 1400
	private const int RT_EXITED_ONCE = 512;

	// Token: 0x04000579 RID: 1401
	private const int RT_WILL_DESTROY = 1024;

	// Token: 0x0400057A RID: 1402
	private const int RT_IS_DESTROYED = 2048;

	// Token: 0x0400057B RID: 1403
	private const int RT_RPC_CONTROL_0 = 4096;

	// Token: 0x0400057C RID: 1404
	private const int RT_RPC_CONTROL_1 = 8192;

	// Token: 0x0400057D RID: 1405
	private const int RT_RPC_CONTROL_2 = 12288;

	// Token: 0x0400057E RID: 1406
	private const int RT_STATE = 3;

	// Token: 0x0400057F RID: 1407
	private const int RT_ONCE = 960;

	// Token: 0x04000580 RID: 1408
	private const int RT_DESTROY_STATE = 3072;

	// Token: 0x04000581 RID: 1409
	private const int RT_RPC_CONTROL = 12288;

	// Token: 0x04000582 RID: 1410
	private const Controllable.ControlFlags PERSISTANT_FLAGS = Controllable.ControlFlags.Root | Controllable.ControlFlags.Strong;

	// Token: 0x04000583 RID: 1411
	private const Controllable.ControlFlags MUTABLE_FLAGS = Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player | Controllable.ControlFlags.Initialized;

	// Token: 0x04000584 RID: 1412
	private const Controllable.ControlFlags TRANSFERED_FLAGS = Controllable.ControlFlags.Local | Controllable.ControlFlags.Player;

	// Token: 0x04000585 RID: 1413
	private const Controllable.ControlFlags CONTROLLER_NPC = (Controllable.ControlFlags)0;

	// Token: 0x04000586 RID: 1414
	private const Controllable.ControlFlags CONTROLLER_CLIENT = Controllable.ControlFlags.Player;

	// Token: 0x04000587 RID: 1415
	private const Controllable.ControlFlags NETWORK_MINE = Controllable.ControlFlags.Local;

	// Token: 0x04000588 RID: 1416
	private const Controllable.ControlFlags NETWORK_PROXY = (Controllable.ControlFlags)0;

	// Token: 0x04000589 RID: 1417
	private const Controllable.ControlFlags ACTIVE_OCCUPIED = Controllable.ControlFlags.Owned;

	// Token: 0x0400058A RID: 1418
	private const Controllable.ControlFlags ACTIVE_VACANT = (Controllable.ControlFlags)0;

	// Token: 0x0400058B RID: 1419
	private const Controllable.ControlFlags TREE_TRUNK = Controllable.ControlFlags.Root;

	// Token: 0x0400058C RID: 1420
	private const Controllable.ControlFlags TREE_BRANCH = (Controllable.ControlFlags)0;

	// Token: 0x0400058D RID: 1421
	private const Controllable.ControlFlags SETUP_INITIALIZED = Controllable.ControlFlags.Initialized;

	// Token: 0x0400058E RID: 1422
	private const Controllable.ControlFlags SETUP_UNINITIALIZED = (Controllable.ControlFlags)0;

	// Token: 0x0400058F RID: 1423
	private const Controllable.ControlFlags BINDING_STRONG = Controllable.ControlFlags.Strong;

	// Token: 0x04000590 RID: 1424
	private const Controllable.ControlFlags BINDING_WEAK = (Controllable.ControlFlags)0;

	// Token: 0x04000591 RID: 1425
	private const Controllable.ControlFlags CONTROLLER_MASK = Controllable.ControlFlags.Player;

	// Token: 0x04000592 RID: 1426
	private const Controllable.ControlFlags NETWORK_MASK = Controllable.ControlFlags.Local;

	// Token: 0x04000593 RID: 1427
	private const Controllable.ControlFlags ACTIVE_MASK = Controllable.ControlFlags.Owned;

	// Token: 0x04000594 RID: 1428
	private const Controllable.ControlFlags TREE_MASK = Controllable.ControlFlags.Root;

	// Token: 0x04000595 RID: 1429
	private const Controllable.ControlFlags SETUP_MASK = Controllable.ControlFlags.Initialized;

	// Token: 0x04000596 RID: 1430
	private const Controllable.ControlFlags BINDING_MASK = Controllable.ControlFlags.Strong;

	// Token: 0x04000597 RID: 1431
	private const Controllable.ControlFlags MASK = Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player | Controllable.ControlFlags.Root | Controllable.ControlFlags.Initialized | Controllable.ControlFlags.Strong;

	// Token: 0x04000598 RID: 1432
	private const Controllable.ControlFlags OWNER_MASK = Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player;

	// Token: 0x04000599 RID: 1433
	private const Controllable.ControlFlags OWNER_NPC = Controllable.ControlFlags.Owned;

	// Token: 0x0400059A RID: 1434
	private const Controllable.ControlFlags OWNER_CLIENT = Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player;

	// Token: 0x0400059B RID: 1435
	private const Controllable.ControlFlags OWNER_NET_MASK = Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player;

	// Token: 0x0400059C RID: 1436
	private const Controllable.ControlFlags OWNER_NET_NPC_MINE = Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local;

	// Token: 0x0400059D RID: 1437
	private const Controllable.ControlFlags OWNER_NET_NPC_PROXY = Controllable.ControlFlags.Owned;

	// Token: 0x0400059E RID: 1438
	private const Controllable.ControlFlags OWNER_NET_CLIENT_MINE = Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player;

	// Token: 0x0400059F RID: 1439
	private const Controllable.ControlFlags OWNER_NET_CLIENT_PROXY = Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player;

	// Token: 0x040005A0 RID: 1440
	private const Controllable.ControlFlags OWNER_NET_TREE_MASK = Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player | Controllable.ControlFlags.Root;

	// Token: 0x040005A1 RID: 1441
	private const Controllable.ControlFlags OWNER_NET_TREE_NPC_MINE_TRUNK = Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Root;

	// Token: 0x040005A2 RID: 1442
	private const Controllable.ControlFlags OWNER_NET_TREE_NPC_PROXY_TRUNK = Controllable.ControlFlags.Owned | Controllable.ControlFlags.Root;

	// Token: 0x040005A3 RID: 1443
	private const Controllable.ControlFlags OWNER_NET_TREE_CLIENT_MINE_TRUNK = Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player | Controllable.ControlFlags.Root;

	// Token: 0x040005A4 RID: 1444
	private const Controllable.ControlFlags OWNER_NET_TREE_CLIENT_PROXY_TRUNK = Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player | Controllable.ControlFlags.Root;

	// Token: 0x040005A5 RID: 1445
	private const Controllable.ControlFlags OWNER_NET_TREE_NPC_MINE_BRANCH = Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local;

	// Token: 0x040005A6 RID: 1446
	private const Controllable.ControlFlags OWNER_NET_TREE_NPC_PROXY_BRANCH = Controllable.ControlFlags.Owned;

	// Token: 0x040005A7 RID: 1447
	private const Controllable.ControlFlags OWNER_NET_TREE_CLIENT_MINE_BRANCH = Controllable.ControlFlags.Owned | Controllable.ControlFlags.Local | Controllable.ControlFlags.Player;

	// Token: 0x040005A8 RID: 1448
	private const Controllable.ControlFlags OWNER_NET_TREE_CLIENT_PROXY_BRANCH = Controllable.ControlFlags.Owned | Controllable.ControlFlags.Player;

	// Token: 0x040005A9 RID: 1449
	private const string kControllableRPCPrefix = "Controllable:";

	// Token: 0x040005AA RID: 1450
	private const string kClientDeleteRPCName = "Controllable:CLD";

	// Token: 0x040005AB RID: 1451
	private const string kClearFromChainRPCName = "Controllable:CLR";

	// Token: 0x040005AC RID: 1452
	private const string kIdleOnRPCName = "Controllable:ID1";

	// Token: 0x040005AD RID: 1453
	private const string kOverrideControlOfRPCName1 = "Controllable:OC1";

	// Token: 0x040005AE RID: 1454
	private const string kOverrideControlOfRPCName2 = "Controllable:OC2";

	// Token: 0x040005AF RID: 1455
	private const string kClientRefreshRPCName = "Controllable:RFH";

	// Token: 0x040005B0 RID: 1456
	private const RPCMode kClientDeleteRPCMode = 0;

	// Token: 0x040005B1 RID: 1457
	private const RPCMode kClearFromChainRPCMode = 2;

	// Token: 0x040005B2 RID: 1458
	private const RPCMode kClearFromChainRPCMode_POST = 1;

	// Token: 0x040005B3 RID: 1459
	private const RPCMode kOverrideControlOfRPCMode = 6;

	// Token: 0x040005B4 RID: 1460
	private const RPCMode kIdleOnRPCMode = 6;

	// Token: 0x040005B5 RID: 1461
	private const RPCMode kClientSideRootNumberRPCMode = 5;

	// Token: 0x040005B6 RID: 1462
	private const RPCMode kClientRefreshRPCMode = 5;

	// Token: 0x040005B7 RID: 1463
	private const string kRPCCall = "RPC call only. Do not call through script";

	// Token: 0x040005B8 RID: 1464
	private const bool kRPCCallError = false;

	// Token: 0x040005B9 RID: 1465
	[NonSerialized]
	private Controllable.CL_Binder _binder;

	// Token: 0x040005BA RID: 1466
	[NonSerialized]
	private List<ulong> _rootCountTimeStamps;

	// Token: 0x040005BB RID: 1467
	[NonSerialized]
	private int _pendingControlCount;

	// Token: 0x040005BC RID: 1468
	[NonSerialized]
	private int _refreshedControlCount;

	// Token: 0x040005BD RID: 1469
	[SerializeField]
	private ControllerClass @class;

	// Token: 0x040005BE RID: 1470
	[NonSerialized]
	private PlayerClient _playerClient;

	// Token: 0x040005BF RID: 1471
	[NonSerialized]
	private Controller _controller;

	// Token: 0x040005C0 RID: 1472
	[NonSerialized]
	private Controllable.ControlFlags F;

	// Token: 0x040005C1 RID: 1473
	[NonSerialized]
	private Controllable.Chain ch;

	// Token: 0x040005C2 RID: 1474
	[NonSerialized]
	private int RT;

	// Token: 0x040005C3 RID: 1475
	[NonSerialized]
	private NetworkViewID __controllerDriverViewID;

	// Token: 0x040005C4 RID: 1476
	[NonSerialized]
	private NetworkMessageInfo __controllerCreateMessageInfo;

	// Token: 0x040005C5 RID: 1477
	[NonSerialized]
	private NetworkView __networkViewForControllable;

	// Token: 0x040005C6 RID: 1478
	[NonSerialized]
	private bool lateFinding;

	// Token: 0x040005C7 RID: 1479
	[NonSerialized]
	public bool isInContextQuery;

	// Token: 0x040005C8 RID: 1480
	private static int localPlayerControllableCount;

	// Token: 0x0200011E RID: 286
	private struct Chain
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0002449C File Offset: 0x0002269C
		public int id
		{
			get
			{
				return (!this.vl) ? -1 : ((int)this.nm);
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x000244B8 File Offset: 0x000226B8
		public int su
		{
			get
			{
				return (!this.vl) ? -1 : ((int)(1 + this.nm + this.ln));
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000244E8 File Offset: 0x000226E8
		public static void ROOT(Controllable root)
		{
			root.ch.tp = root;
			root.ch.bt = root;
			root.ch.it = root;
			root.ch.vl = true;
			root.ch.dn.vl = (root.ch.up.vl = false);
			root.ch.dn.it = (root.ch.up.it = null);
			root.ch.nm = (root.ch.ln = 0);
			root.ch.iv = true;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00024598 File Offset: 0x00022798
		private bool Add(ref Controllable.Chain nw, Controllable ct)
		{
			if (!this.vl || nw.vl)
			{
				return false;
			}
			nw.it = ct;
			nw.it.ON_CHAIN_RENEW();
			this.tp.ch.up.vl = true;
			this.tp.ch.up.it = nw.it;
			nw.dn.vl = true;
			nw.dn.it = this.tp;
			nw.nm = this.tp.ch.nm;
			nw.nm += 1;
			nw.ln = 0;
			nw.up.vl = false;
			nw.up.it = null;
			nw.tp = nw.it;
			nw.bt = this.tp.ch.bt;
			nw.vl = true;
			Controllable.Link link = nw.dn;
			nw.iv = true;
			do
			{
				link.it.ch.tp = nw.tp;
				Controllable controllable = link.it;
				controllable.ch.ln = controllable.ch.ln + 1;
				link.it.ch.iv = true;
				link = link.it.ch.dn;
			}
			while (link.vl);
			nw.it.ON_CHAIN_SUBSCRIBE();
			return true;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00024708 File Offset: 0x00022908
		public bool Add(Controllable vessel)
		{
			return vessel && this.Add(ref vessel.ch, vessel);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00024728 File Offset: 0x00022928
		public bool RefreshEngauge()
		{
			if (!this.vl)
			{
				return false;
			}
			if (this.tp.ch.iv)
			{
				int num;
				if (this.bt.ch.up.vl)
				{
					Controllable controllable = this.bt;
					num = 128;
					for (;;)
					{
						controllable.ch.iv = false;
						switch (controllable.RT & 3)
						{
						case 0:
							Controllable.DO_ENTER(Controllable.CAP_ENTER(num, controllable.RT, controllable.F), controllable);
							break;
						case 3:
							Controllable.DO_DEMOTE(Controllable.CAP_DEMOTE(num, controllable.RT, controllable.F), controllable);
							break;
						}
						num |= 768;
						if (!controllable.ch.up.vl)
						{
							break;
						}
						controllable = controllable.ch.up.it;
					}
				}
				else
				{
					num = 0;
				}
				this.tp.ch.iv = false;
				switch (this.tp.RT & 3)
				{
				case 0:
					Controllable.DO_ENTER(Controllable.CAP_ENTER(num & -129, this.tp.RT, this.tp.F), this.tp);
					Controllable.DO_PROMOTE(Controllable.CAP_PROMOTE(num & -129, this.tp.RT, this.tp.F), this.tp);
					break;
				case 1:
					Controllable.DO_PROMOTE(Controllable.CAP_PROMOTE(num & -129, this.tp.RT, this.tp.F), this.tp);
					break;
				}
			}
			return true;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00024904 File Offset: 0x00022B04
		public bool RefreshEnter()
		{
			if (!this.vl)
			{
				return false;
			}
			if (this.tp.ch.iv)
			{
				int num;
				if (this.bt.ch.up.vl)
				{
					Controllable controllable = this.bt;
					num = 128;
					for (;;)
					{
						switch (controllable.RT & 3)
						{
						case 0:
							Controllable.DO_ENTER(Controllable.CAP_ENTER(num, controllable.RT, controllable.F), controllable);
							break;
						case 3:
							Controllable.DO_DEMOTE(Controllable.CAP_DEMOTE(num, controllable.RT, controllable.F), controllable);
							break;
						}
						num |= 768;
						if (!controllable.ch.up.vl)
						{
							break;
						}
						controllable = controllable.ch.up.it;
					}
				}
				else
				{
					num = 0;
				}
				switch (this.tp.RT & 3)
				{
				case 0:
					Controllable.DO_ENTER(Controllable.CAP_ENTER(num, this.tp.RT, this.tp.F), this.tp);
					break;
				}
			}
			return true;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00024A64 File Offset: 0x00022C64
		public override string ToString()
		{
			if (!this.vl)
			{
				return "invalid";
			}
			StringBuilder stringBuilder = new StringBuilder();
			Controllable controllable = this.bt;
			while (controllable)
			{
				if (controllable == this.it)
				{
					stringBuilder.Append("-->");
				}
				else
				{
					stringBuilder.Append("   ");
				}
				stringBuilder.AppendLine(controllable.name);
				controllable = ((!controllable.ch.up.vl) ? null : controllable.ch.up.it);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00024B0C File Offset: 0x00022D0C
		public void Delete()
		{
			if (!this.vl)
			{
				return;
			}
			int num = Controllable.CAP_THIS(16, this.it.RT, this.it.F);
			if (this.up.vl)
			{
				int num2 = (int)this.ln;
				int num3 = (num & 145) << 1;
				if (!this.dn.vl)
				{
					num3 |= (num & 145) << 2;
				}
				for (;;)
				{
					Controllable controllable = this.tp.ch.dn.it;
					Controllable controllable2 = this.tp;
					int cmd;
					switch (controllable2.RT & 3)
					{
					case 1:
						Controllable.DO_EXIT(cmd = Controllable.CAP_EXIT(num3, controllable2.RT, controllable2.F), controllable2);
						break;
					case 2:
						goto IL_10A;
					case 3:
						cmd = Controllable.CAP_EXIT(num3, controllable2.RT, controllable2.F);
						Controllable.DO_DEMOTE(Controllable.CAP_DEMOTE(cmd, controllable2.RT, controllable2.F), controllable2);
						Controllable.DO_EXIT(cmd, controllable2);
						break;
					default:
						goto IL_10A;
					}
					IL_124:
					controllable2.ON_CHAIN_ERASE(cmd);
					controllable2.ch = default(Controllable.Chain);
					controllable2.ON_CHAIN_ABOLISHED();
					this.tp = controllable;
					this.tp.ch.up = default(Controllable.Link);
					this.tp.ch.ln = this.tp.ch.ln - 1;
					this.tp.ch.tp = this.tp;
					Controllable.Link link = this.tp.ch.dn;
					byte b = this.tp.ch.ln;
					while (link.vl)
					{
						Controllable controllable3 = link.it;
						link = controllable3.ch.dn;
						controllable3.ch.tp = this.tp;
						b = (controllable3.ch.ln = b - 1);
					}
					if (--num2 <= 0)
					{
						break;
					}
					continue;
					IL_10A:
					cmd = Controllable.CAP_THIS(num3, controllable2.RT, controllable2.F);
					goto IL_124;
				}
			}
			switch (this.it.RT & 3)
			{
			case 1:
				Controllable.DO_EXIT(Controllable.CAP_EXIT(num, this.it.RT, this.it.F), this.it);
				break;
			case 3:
				Controllable.DO_DEMOTE(Controllable.CAP_DEMOTE(num, this.it.RT, this.it.F), this.it);
				Controllable.DO_EXIT(Controllable.CAP_EXIT(num, this.it.RT, this.it.F), this.it);
				break;
			}
			Controllable controllable4 = this.it;
			controllable4.ON_CHAIN_ERASE(num);
			Controllable.Link link2 = this.dn;
			controllable4.ch = (this = default(Controllable.Chain));
			if (link2.vl)
			{
				Controllable controllable5 = link2.it;
				controllable5.ch.up = default(Controllable.Link);
				int num4 = 0;
				do
				{
					Controllable controllable6 = link2.it;
					link2 = controllable6.ch.dn;
					controllable6.ch.iv = true;
					controllable6.ch.tp = controllable5;
					controllable6.ch.ln = (byte)num4++;
				}
				while (link2.vl);
			}
			controllable4.ON_CHAIN_ABOLISHED();
		}

		// Token: 0x040005CA RID: 1482
		public Controllable it;

		// Token: 0x040005CB RID: 1483
		public Controllable bt;

		// Token: 0x040005CC RID: 1484
		public Controllable tp;

		// Token: 0x040005CD RID: 1485
		public Controllable.Link dn;

		// Token: 0x040005CE RID: 1486
		public Controllable.Link up;

		// Token: 0x040005CF RID: 1487
		public byte nm;

		// Token: 0x040005D0 RID: 1488
		public byte ln;

		// Token: 0x040005D1 RID: 1489
		public bool vl;

		// Token: 0x040005D2 RID: 1490
		public bool iv;
	}

	// Token: 0x0200011F RID: 287
	private class CL_Binder : IDisposable
	{
		// Token: 0x06000861 RID: 2145 RVA: 0x00024EA0 File Offset: 0x000230A0
		public CL_Binder(Controllable owner, NetworkViewID rootID, NetworkViewID parentID, ref NetworkMessageInfo info)
		{
			this._root.id = rootID;
			this._parent.id = parentID;
			this._info = info;
			this.owner = owner;
			this.sameSearch = (this._root.id == this._parent.id);
			if (Controllable.CL_Binder.binderCount++ == 0)
			{
				Controllable.CL_Binder.last = this;
				Controllable.CL_Binder.first = this;
			}
			else
			{
				this.prev = Controllable.CL_Binder.last;
				this.prev.next = this;
				Controllable.CL_Binder.last = this;
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00024F3C File Offset: 0x0002313C
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (this.owner && this.owner._binder == this)
			{
				this.owner._binder = null;
			}
			if (--Controllable.CL_Binder.binderCount == 0)
			{
				Controllable.CL_Binder.first = (Controllable.CL_Binder.last = (this.next = (this.prev = null)));
			}
			else
			{
				if (Controllable.CL_Binder.first == this)
				{
					Controllable.CL_Binder.first = this.next;
					this.next.prev = null;
				}
				else if (Controllable.CL_Binder.last == this)
				{
					Controllable.CL_Binder.last = this.prev;
					this.prev.next = null;
				}
				else
				{
					this.next.prev = this.prev;
					this.prev.next = this.next;
				}
				this.next = (this.prev = null);
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00025040 File Offset: 0x00023240
		public bool Find()
		{
			return this._root.Find() && (this.sameSearch || this._parent.Find());
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0002507C File Offset: 0x0002327C
		protected int CountValidate(List<ulong> ts, int tsCount)
		{
			if (this.Find())
			{
				Controllable controllable = (!this.sameSearch) ? this._parent.controllable : this._root.controllable;
				if (this.sameSearch)
				{
					if (tsCount > 1 && ts[1] <= this._info.timestampInMillis)
					{
						return 2;
					}
					return -1;
				}
				else if (controllable._binder != null)
				{
					int num = controllable._binder.CountValidate(ts, tsCount);
					if (tsCount > num && ts[num] <= this._info.timestampInMillis)
					{
						return num + 1;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00025124 File Offset: 0x00023324
		public bool CanLink()
		{
			if (this._root.Find() && this._root.controllable._rootCountTimeStamps != null)
			{
				int num = this.CountValidate(this._root.controllable._rootCountTimeStamps, this._root.controllable._rootCountTimeStamps.Count);
				return num == this._root.controllable._pendingControlCount;
			}
			return false;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00025198 File Offset: 0x00023398
		private void PreLink()
		{
			Controllable controllable = (!this.sameSearch) ? this._parent.controllable : this._root.controllable;
			if ((controllable.F & Controllable.ControlFlags.Root) == (Controllable.ControlFlags)0)
			{
				controllable._binder.PreLink();
			}
			if ((this.owner.F & Controllable.ControlFlags.Initialized) == (Controllable.ControlFlags)0)
			{
				this.owner.OCO_FOUND(controllable.networkViewID, ref this._info);
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00025210 File Offset: 0x00023410
		public void Link()
		{
			this.PreLink();
			if (this._root.controllable._pendingControlCount == this._root.controllable._refreshedControlCount)
			{
				this._root.controllable.ch.RefreshEngauge();
			}
			else
			{
				this._root.controllable.ch.RefreshEnter();
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0002527C File Offset: 0x0002347C
		public static void StaticLink(Controllable root)
		{
			Controllable.CL_Binder cl_Binder = Controllable.CL_Binder.last;
			for (int i = Controllable.CL_Binder.binderCount - 1; i >= 0; i--)
			{
				Controllable.CL_Binder cl_Binder2 = cl_Binder;
				cl_Binder = cl_Binder.prev;
				if (cl_Binder2.Find() && cl_Binder2._root.controllable == root && cl_Binder2.CountValidate(root._rootCountTimeStamps, root._rootCountTimeStamps.Count) == root._refreshedControlCount)
				{
					cl_Binder2.Link();
					return;
				}
			}
		}

		// Token: 0x040005D3 RID: 1491
		private static Controllable.CL_Binder first;

		// Token: 0x040005D4 RID: 1492
		private static Controllable.CL_Binder last;

		// Token: 0x040005D5 RID: 1493
		private static int binderCount;

		// Token: 0x040005D6 RID: 1494
		private Controllable.CL_Binder.Search _root;

		// Token: 0x040005D7 RID: 1495
		private Controllable.CL_Binder.Search _parent;

		// Token: 0x040005D8 RID: 1496
		private readonly bool sameSearch;

		// Token: 0x040005D9 RID: 1497
		private NetworkMessageInfo _info;

		// Token: 0x040005DA RID: 1498
		private readonly Controllable owner;

		// Token: 0x040005DB RID: 1499
		private bool disposed;

		// Token: 0x040005DC RID: 1500
		private Controllable.CL_Binder next;

		// Token: 0x040005DD RID: 1501
		private Controllable.CL_Binder prev;

		// Token: 0x02000120 RID: 288
		private struct Search
		{
			// Token: 0x17000206 RID: 518
			// (get) Token: 0x06000869 RID: 2153 RVA: 0x000252FC File Offset: 0x000234FC
			// (set) Token: 0x0600086A RID: 2154 RVA: 0x00025304 File Offset: 0x00023504
			public NetworkViewID id
			{
				get
				{
					return this._id;
				}
				set
				{
					this._id = value;
					this._view = null;
					this._controllable = null;
				}
			}

			// Token: 0x17000207 RID: 519
			// (get) Token: 0x0600086B RID: 2155 RVA: 0x0002531C File Offset: 0x0002351C
			public NetworkView view
			{
				get
				{
					return this._view;
				}
			}

			// Token: 0x17000208 RID: 520
			// (get) Token: 0x0600086C RID: 2156 RVA: 0x00025324 File Offset: 0x00023524
			public Controllable controllable
			{
				get
				{
					return this._controllable;
				}
			}

			// Token: 0x0600086D RID: 2157 RVA: 0x0002532C File Offset: 0x0002352C
			public bool Find()
			{
				if (!this._controllable)
				{
					if (!this._view)
					{
						this._view = NetworkView.Find(this._id);
						if (!this._view)
						{
							return false;
						}
					}
					Character character = this._view.idMain as Character;
					return character && (this._controllable = character.controllable);
				}
				return true;
			}

			// Token: 0x040005DE RID: 1502
			private NetworkViewID _id;

			// Token: 0x040005DF RID: 1503
			private NetworkView _view;

			// Token: 0x040005E0 RID: 1504
			private Controllable _controllable;
		}
	}

	// Token: 0x02000121 RID: 289
	[Flags]
	private enum ControlFlags
	{
		// Token: 0x040005E2 RID: 1506
		Owned = 1,
		// Token: 0x040005E3 RID: 1507
		Local = 2,
		// Token: 0x040005E4 RID: 1508
		Player = 4,
		// Token: 0x040005E5 RID: 1509
		Root = 8,
		// Token: 0x040005E6 RID: 1510
		Initialized = 16,
		// Token: 0x040005E7 RID: 1511
		Strong = 32
	}

	// Token: 0x02000122 RID: 290
	private struct Link
	{
		// Token: 0x040005E8 RID: 1512
		public Controllable it;

		// Token: 0x040005E9 RID: 1513
		public bool vl;
	}

	// Token: 0x02000123 RID: 291
	private static class LocalOnly
	{
		// Token: 0x040005EA RID: 1514
		public static readonly List<Controllable> rootLocalPlayerControllables = new List<Controllable>();
	}

	// Token: 0x02000862 RID: 2146
	// (Invoke) Token: 0x06004B60 RID: 19296
	public delegate void DestroyInContextQuery(Controllable controllable);
}
