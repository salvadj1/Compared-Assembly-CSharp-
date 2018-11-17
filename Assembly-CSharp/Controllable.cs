using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x0200013C RID: 316
public sealed class Controllable : global::IDLocalCharacter
{
	// Token: 0x14000004 RID: 4
	// (add) Token: 0x06000891 RID: 2193 RVA: 0x00025114 File Offset: 0x00023314
	// (remove) Token: 0x06000892 RID: 2194 RVA: 0x0002512C File Offset: 0x0002332C
	public static event global::Controllable.DestroyInContextQuery onDestroyInContextQuery;

	// Token: 0x06000893 RID: 2195 RVA: 0x00025144 File Offset: 0x00023344
	private void ON_CHAIN_RENEW()
	{
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x00025148 File Offset: 0x00023348
	private void ON_CHAIN_SUBSCRIBE()
	{
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x0002514C File Offset: 0x0002334C
	private void ON_CHAIN_ERASE(int cmd)
	{
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x00025150 File Offset: 0x00023350
	private void ON_CHAIN_ABOLISHED()
	{
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x00025154 File Offset: 0x00023354
	private static int CAP_THIS(int cmd, int RT, global::Controllable.ControlFlags F)
	{
		cmd &= -30721;
		if ((F & global::Controllable.ControlFlags.Strong) == (global::Controllable.ControlFlags)0)
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
		if ((F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player)) == global::Controllable.ControlFlags.Owned)
		{
			cmd |= 0;
		}
		else if ((F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player)) == (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player))
		{
			cmd |= 8192;
		}
		if ((F & global::Controllable.ControlFlags.Local) == global::Controllable.ControlFlags.Local)
		{
			cmd |= 16384;
		}
		else
		{
			cmd |= 0;
		}
		if ((F & global::Controllable.ControlFlags.Root) == global::Controllable.ControlFlags.Root)
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

	// Token: 0x06000898 RID: 2200 RVA: 0x00025220 File Offset: 0x00023420
	private static int CAP_ENTER(int cmd, int RT, global::Controllable.ControlFlags F)
	{
		cmd = global::Controllable.CAP_THIS(cmd, RT, F);
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

	// Token: 0x06000899 RID: 2201 RVA: 0x0002525C File Offset: 0x0002345C
	private static int CAP_PROMOTE(int cmd, int RT, global::Controllable.ControlFlags F)
	{
		cmd = global::Controllable.CAP_THIS(cmd, RT, F);
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

	// Token: 0x0600089A RID: 2202 RVA: 0x000252A8 File Offset: 0x000234A8
	private static int CAP_DEMOTE(int cmd, int RT, global::Controllable.ControlFlags F)
	{
		cmd = global::Controllable.CAP_THIS(cmd, RT, F);
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

	// Token: 0x0600089B RID: 2203 RVA: 0x000252E8 File Offset: 0x000234E8
	private static int CAP_EXIT(int cmd, int RT, global::Controllable.ControlFlags F)
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

	// Token: 0x0600089C RID: 2204 RVA: 0x00025320 File Offset: 0x00023520
	private static void DO_ENTER(int cmd, global::Controllable citr)
	{
		if ((citr.RT & 8) == 8)
		{
			return;
		}
		citr.RT |= 8;
		citr.ControlEnter(cmd);
		citr.RT = ((citr.RT & -12) | 65);
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x00025364 File Offset: 0x00023564
	private static void DO_PROMOTE(int cmd, global::Controllable citr)
	{
		if ((citr.RT & 16) == 16)
		{
			return;
		}
		citr.RT |= 16;
		citr.ControlEngauge(cmd);
		citr.RT = ((citr.RT & -20) | 131);
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x000253B0 File Offset: 0x000235B0
	private static void DO_DEMOTE(int cmd, global::Controllable citr)
	{
		if ((citr.RT & 16) == 16)
		{
			return;
		}
		citr.RT |= 16;
		citr.ControlCease(cmd);
		citr.RT = ((citr.RT & -20) | 257);
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x000253FC File Offset: 0x000235FC
	private static void DO_EXIT(int cmd, global::Controllable citr)
	{
		if ((citr.RT & 8) == 8)
		{
			return;
		}
		citr.RT |= 8;
		citr.ControlExit(cmd);
		citr.RT = ((citr.RT & -12) | 512);
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x00025438 File Offset: 0x00023638
	private void ClearBinder()
	{
		if (this._binder != null)
		{
			this._binder.Dispose();
		}
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x00025450 File Offset: 0x00023650
	private void CL_OverideControlOf(uLink.NetworkViewID rootViewID, uLink.NetworkViewID parentViewID, ref uLink.NetworkMessageInfo info)
	{
		this.ClearBinder();
		this._binder = new global::Controllable.CL_Binder(this, rootViewID, parentViewID, ref info);
		if (this._binder.CanLink())
		{
			this._binder.Link();
		}
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x00025490 File Offset: 0x00023690
	private void CL_RootControlCountSet(int count, ref uLink.NetworkMessageInfo info)
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

	// Token: 0x060008A3 RID: 2211 RVA: 0x00025530 File Offset: 0x00023730
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
			global::Controllable.CL_Binder.StaticLink(this);
		}
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x000255A8 File Offset: 0x000237A8
	private void CL_Clear()
	{
		this.ClearBinder();
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x000255B0 File Offset: 0x000237B0
	private void RN(int n, ref uLink.NetworkMessageInfo info)
	{
		this.CL_RootControlCountSet(n, ref info);
	}

	// Token: 0x170001F2 RID: 498
	// (get) Token: 0x060008A6 RID: 2214 RVA: 0x000255BC File Offset: 0x000237BC
	public new global::Controller controller
	{
		get
		{
			return this._controller;
		}
	}

	// Token: 0x170001F3 RID: 499
	// (get) Token: 0x060008A7 RID: 2215 RVA: 0x000255C4 File Offset: 0x000237C4
	public new global::Controller controlledController
	{
		get
		{
			return ((this.F & global::Controllable.ControlFlags.Owned) != global::Controllable.ControlFlags.Owned) ? null : this._controller;
		}
	}

	// Token: 0x170001F4 RID: 500
	// (get) Token: 0x060008A8 RID: 2216 RVA: 0x000255E0 File Offset: 0x000237E0
	public new global::Controller playerControlledController
	{
		get
		{
			return ((this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player)) != (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player)) ? null : this._controller;
		}
	}

	// Token: 0x170001F5 RID: 501
	// (get) Token: 0x060008A9 RID: 2217 RVA: 0x000255FC File Offset: 0x000237FC
	public new global::Controller aiControlledController
	{
		get
		{
			return ((this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player)) != global::Controllable.ControlFlags.Owned) ? null : this._controller;
		}
	}

	// Token: 0x170001F6 RID: 502
	// (get) Token: 0x060008AA RID: 2218 RVA: 0x00025618 File Offset: 0x00023818
	public new global::Controller localPlayerControlledController
	{
		get
		{
			return ((this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player)) != (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player)) ? null : this._controller;
		}
	}

	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x060008AB RID: 2219 RVA: 0x00025634 File Offset: 0x00023834
	public new global::Controller localAIControlledController
	{
		get
		{
			return ((this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player)) != (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local)) ? null : this._controller;
		}
	}

	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x060008AC RID: 2220 RVA: 0x00025650 File Offset: 0x00023850
	public new global::Controller remotePlayerControlledController
	{
		get
		{
			return ((this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player)) != (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player)) ? null : this._controller;
		}
	}

	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x060008AD RID: 2221 RVA: 0x0002566C File Offset: 0x0002386C
	public new global::Controller remoteAIControlledController
	{
		get
		{
			return ((this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player)) != global::Controllable.ControlFlags.Owned) ? null : this._controller;
		}
	}

	// Token: 0x170001FA RID: 506
	// (get) Token: 0x060008AE RID: 2222 RVA: 0x00025688 File Offset: 0x00023888
	public new global::Controllable controllable
	{
		get
		{
			return this;
		}
	}

	// Token: 0x170001FB RID: 507
	// (get) Token: 0x060008AF RID: 2223 RVA: 0x0002568C File Offset: 0x0002388C
	public new global::Controllable controlledControllable
	{
		get
		{
			return ((this.F & global::Controllable.ControlFlags.Owned) != global::Controllable.ControlFlags.Owned) ? null : this;
		}
	}

	// Token: 0x170001FC RID: 508
	// (get) Token: 0x060008B0 RID: 2224 RVA: 0x000256A4 File Offset: 0x000238A4
	public new global::Controllable playerControlledControllable
	{
		get
		{
			return ((this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player)) != (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player)) ? null : this;
		}
	}

	// Token: 0x170001FD RID: 509
	// (get) Token: 0x060008B1 RID: 2225 RVA: 0x000256BC File Offset: 0x000238BC
	public new global::Controllable aiControlledControllable
	{
		get
		{
			return ((this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player)) != global::Controllable.ControlFlags.Owned) ? null : this;
		}
	}

	// Token: 0x170001FE RID: 510
	// (get) Token: 0x060008B2 RID: 2226 RVA: 0x000256D4 File Offset: 0x000238D4
	public new global::Controllable localPlayerControlledControllable
	{
		get
		{
			return ((this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player)) != (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player)) ? null : this;
		}
	}

	// Token: 0x170001FF RID: 511
	// (get) Token: 0x060008B3 RID: 2227 RVA: 0x000256EC File Offset: 0x000238EC
	public new global::Controllable localAIControlledControllable
	{
		get
		{
			return ((this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player)) != (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local)) ? null : this;
		}
	}

	// Token: 0x17000200 RID: 512
	// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00025704 File Offset: 0x00023904
	public new global::Controllable remotePlayerControlledControllable
	{
		get
		{
			return ((this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player)) != (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player)) ? null : this;
		}
	}

	// Token: 0x17000201 RID: 513
	// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0002571C File Offset: 0x0002391C
	public new global::Controllable remoteAIControlledControllable
	{
		get
		{
			return ((this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player)) != global::Controllable.ControlFlags.Owned) ? null : this;
		}
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00025734 File Offset: 0x00023934
	public new global::PlayerClient playerClient
	{
		get
		{
			return this._playerClient;
		}
	}

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0002573C File Offset: 0x0002393C
	public uLink.NetworkPlayer netPlayer
	{
		get
		{
			return (!this._playerClient) ? uLink.NetworkPlayer.unassigned : this._playerClient.netPlayer;
		}
	}

	// Token: 0x17000204 RID: 516
	// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00025764 File Offset: 0x00023964
	public new bool controlled
	{
		get
		{
			return (this.F & global::Controllable.ControlFlags.Owned) == global::Controllable.ControlFlags.Owned;
		}
	}

	// Token: 0x17000205 RID: 517
	// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00025774 File Offset: 0x00023974
	public new bool playerControlled
	{
		get
		{
			return (this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player)) == (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player);
		}
	}

	// Token: 0x17000206 RID: 518
	// (get) Token: 0x060008BA RID: 2234 RVA: 0x00025784 File Offset: 0x00023984
	public new bool aiControlled
	{
		get
		{
			return (this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player)) == global::Controllable.ControlFlags.Owned;
		}
	}

	// Token: 0x17000207 RID: 519
	// (get) Token: 0x060008BB RID: 2235 RVA: 0x00025794 File Offset: 0x00023994
	public new bool localPlayerControlled
	{
		get
		{
			return (this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player)) == (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player);
		}
	}

	// Token: 0x17000208 RID: 520
	// (get) Token: 0x060008BC RID: 2236 RVA: 0x000257A4 File Offset: 0x000239A4
	public new bool remotePlayerControlled
	{
		get
		{
			return (this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player)) == (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player);
		}
	}

	// Token: 0x17000209 RID: 521
	// (get) Token: 0x060008BD RID: 2237 RVA: 0x000257B4 File Offset: 0x000239B4
	public new bool localAIControlled
	{
		get
		{
			return (this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player)) == (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local);
		}
	}

	// Token: 0x1700020A RID: 522
	// (get) Token: 0x060008BE RID: 2238 RVA: 0x000257C4 File Offset: 0x000239C4
	public new bool remoteAIControlled
	{
		get
		{
			return (this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player)) == global::Controllable.ControlFlags.Owned;
		}
	}

	// Token: 0x1700020B RID: 523
	// (get) Token: 0x060008BF RID: 2239 RVA: 0x000257D4 File Offset: 0x000239D4
	public new bool localControlled
	{
		get
		{
			return (this.F & global::Controllable.ControlFlags.Local) == global::Controllable.ControlFlags.Local;
		}
	}

	// Token: 0x1700020C RID: 524
	// (get) Token: 0x060008C0 RID: 2240 RVA: 0x000257E4 File Offset: 0x000239E4
	public new bool remoteControlled
	{
		get
		{
			return (this.F & global::Controllable.ControlFlags.Local) == (global::Controllable.ControlFlags)0;
		}
	}

	// Token: 0x1700020D RID: 525
	// (get) Token: 0x060008C1 RID: 2241 RVA: 0x000257F4 File Offset: 0x000239F4
	public bool core
	{
		get
		{
			return (this.F & global::Controllable.ControlFlags.Root) == global::Controllable.ControlFlags.Root;
		}
	}

	// Token: 0x1700020E RID: 526
	// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00025804 File Offset: 0x00023A04
	public bool vessel
	{
		get
		{
			return (this.F & global::Controllable.ControlFlags.Root) == (global::Controllable.ControlFlags)0;
		}
	}

	// Token: 0x1700020F RID: 527
	// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00025814 File Offset: 0x00023A14
	public new string npcName
	{
		get
		{
			return (!this.@class) ? null : this.@class.npcName;
		}
	}

	// Token: 0x17000210 RID: 528
	// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00025838 File Offset: 0x00023A38
	public new bool controlOverridden
	{
		get
		{
			return this.ch.vl && this.ch.ln > 0;
		}
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0002585C File Offset: 0x00023A5C
	public new bool ControlOverriddenBy(global::Controllable controllable)
	{
		return this.ch.vl && this.ch.ln > 0 && controllable && controllable.ch.vl && this.ch.ln > controllable.ch.ln && this.ch.bt == controllable.ch.bt;
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x000258E0 File Offset: 0x00023AE0
	public new bool ControlOverriddenBy(global::Controller controller)
	{
		global::Controllable controllable;
		return this.ch.vl && this.ch.ln > 0 && controller && (controllable = controller.controllable) && controllable.ch.vl && this.ch.ln > controllable.ch.ln && this.ch.bt == controllable.ch.bt;
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x00025974 File Offset: 0x00023B74
	public new bool ControlOverriddenBy(global::Character character)
	{
		global::Controllable controllable;
		return this.ch.vl && this.ch.ln > 0 && character && (controllable = character.controllable) && controllable.ch.vl && this.ch.ln > controllable.ch.ln && this.ch.bt == controllable.ch.bt;
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x00025A08 File Offset: 0x00023C08
	public new bool ControlOverriddenBy(IDMain main)
	{
		return this.ch.vl && this.ch.ln != 0 && main is global::Character && this.ControlOverriddenBy((global::Character)main);
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x00025A44 File Offset: 0x00023C44
	public new bool ControlOverriddenBy(IDBase idBase)
	{
		return this.ch.vl && this.ch.ln != 0 && idBase && this.ControlOverriddenBy(idBase.idMain);
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x00025A80 File Offset: 0x00023C80
	public new bool ControlOverriddenBy(global::IDLocalCharacter idLocal)
	{
		return this.ch.vl && this.ch.ln != 0 && idLocal && this.ControlOverriddenBy(idLocal.idMain);
	}

	// Token: 0x17000211 RID: 529
	// (get) Token: 0x060008CB RID: 2251 RVA: 0x00025ABC File Offset: 0x00023CBC
	public new bool overridingControl
	{
		get
		{
			return this.ch.vl && this.ch.nm > 0;
		}
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x00025AE0 File Offset: 0x00023CE0
	public new bool OverridingControlOf(global::Controllable controllable)
	{
		return this.ch.vl && this.ch.nm > 0 && controllable && controllable.ch.vl && this.ch.nm > controllable.ch.nm && this.ch.bt == controllable.ch.bt;
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x00025B64 File Offset: 0x00023D64
	public new bool OverridingControlOf(global::Controller controller)
	{
		global::Controllable controllable;
		return this.ch.vl && this.ch.nm > 0 && controller && (controllable = controller.controllable) && controllable.ch.vl && this.ch.nm > controllable.ch.nm && this.ch.bt == controllable.ch.bt;
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x00025BF8 File Offset: 0x00023DF8
	public new bool OverridingControlOf(global::Character character)
	{
		global::Controllable controllable;
		return this.ch.vl && this.ch.nm > 0 && character && (controllable = character.controllable) && controllable.ch.vl && this.ch.nm > controllable.ch.nm && this.ch.bt == controllable.ch.bt;
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x00025C8C File Offset: 0x00023E8C
	public new bool OverridingControlOf(IDMain main)
	{
		return this.ch.vl && this.ch.nm != 0 && main is global::Character && this.OverridingControlOf((global::Character)main);
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x00025CC8 File Offset: 0x00023EC8
	public new bool OverridingControlOf(IDBase idBase)
	{
		return this.ch.vl && this.ch.nm != 0 && idBase && this.OverridingControlOf(idBase.idMain);
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x00025D04 File Offset: 0x00023F04
	public new bool OverridingControlOf(global::IDLocalCharacter idLocal)
	{
		return this.ch.vl && this.ch.nm != 0 && idLocal && this.OverridingControlOf(idLocal.idMain);
	}

	// Token: 0x17000212 RID: 530
	// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00025D40 File Offset: 0x00023F40
	public new bool assignedControl
	{
		get
		{
			return this.ch.vl;
		}
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x00025D50 File Offset: 0x00023F50
	public new bool AssignedControlOf(global::Controllable controllable)
	{
		return this.ch.vl && this == controllable;
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x00025D6C File Offset: 0x00023F6C
	public new bool AssignedControlOf(global::Controller controller)
	{
		return this.ch.vl && this._controller == controller && this._controller;
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x00025DA0 File Offset: 0x00023FA0
	public new bool AssignedControlOf(IDMain character)
	{
		return this.ch.vl && this.idMain == character;
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x00025DC4 File Offset: 0x00023FC4
	public new bool AssignedControlOf(IDBase idBase)
	{
		return this.ch.vl && idBase && this.idMain == idBase.idMain;
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x00025DF8 File Offset: 0x00023FF8
	public new global::RelativeControl RelativeControlTo(global::Controllable controllable)
	{
		if (!this.ch.vl || !controllable || !controllable.ch.vl || controllable.ch.bt != this.ch.bt)
		{
			return global::RelativeControl.Incompatible;
		}
		if (this.ch.ln > controllable.ch.ln)
		{
			return global::RelativeControl.OverriddenBy;
		}
		if (this.ch.ln < controllable.ch.ln)
		{
			return global::RelativeControl.IsOverriding;
		}
		return global::RelativeControl.Assigned;
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x00025E90 File Offset: 0x00024090
	public new global::RelativeControl RelativeControlTo(global::Controller controller)
	{
		global::Controllable controllable;
		if (!this.ch.vl || !controller || !(controllable = controller.controllable) || controllable.ch.vl || controllable.ch.bt != this.ch.bt)
		{
			return global::RelativeControl.Incompatible;
		}
		if (this.ch.ln > controllable.ch.ln)
		{
			return global::RelativeControl.OverriddenBy;
		}
		if (this.ch.ln < controllable.ch.ln)
		{
			return global::RelativeControl.IsOverriding;
		}
		return global::RelativeControl.Assigned;
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x00025F38 File Offset: 0x00024138
	public new global::RelativeControl RelativeControlTo(global::Character character)
	{
		if (!character)
		{
			return global::RelativeControl.Incompatible;
		}
		return this.RelativeControlTo(character.controllable);
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x00025F54 File Offset: 0x00024154
	public new global::RelativeControl RelativeControlTo(IDMain idMain)
	{
		if (!(idMain is global::Character))
		{
			return global::RelativeControl.Incompatible;
		}
		return this.RelativeControlTo((global::Character)idMain);
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x00025F70 File Offset: 0x00024170
	public new global::RelativeControl RelativeControlTo(global::IDLocalCharacter idLocal)
	{
		if (!idLocal)
		{
			return global::RelativeControl.Incompatible;
		}
		return this.RelativeControlTo(idLocal.idMain.controllable);
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x00025F9C File Offset: 0x0002419C
	public new global::RelativeControl RelativeControlTo(IDBase idBase)
	{
		if (!idBase)
		{
			return global::RelativeControl.Incompatible;
		}
		return this.RelativeControlTo(idBase.idMain as global::Character);
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x00025FBC File Offset: 0x000241BC
	public new global::RelativeControl RelativeControlFrom(global::Controllable controllable)
	{
		if (!this.ch.vl || !controllable || !controllable.ch.vl || controllable.ch.bt != this.ch.bt)
		{
			return global::RelativeControl.Incompatible;
		}
		if (this.ch.ln > controllable.ch.ln)
		{
			return global::RelativeControl.IsOverriding;
		}
		if (this.ch.ln < controllable.ch.ln)
		{
			return global::RelativeControl.OverriddenBy;
		}
		return global::RelativeControl.Assigned;
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00026054 File Offset: 0x00024254
	public new global::RelativeControl RelativeControlFrom(global::Controller controller)
	{
		global::Controllable controllable;
		if (!this.ch.vl || !controller || !(controllable = controller.controllable) || controllable.ch.vl || controllable.ch.bt != this.ch.bt)
		{
			return global::RelativeControl.Incompatible;
		}
		if (this.ch.ln > controllable.ch.ln)
		{
			return global::RelativeControl.IsOverriding;
		}
		if (this.ch.ln < controllable.ch.ln)
		{
			return global::RelativeControl.OverriddenBy;
		}
		return global::RelativeControl.Assigned;
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x000260FC File Offset: 0x000242FC
	public new global::RelativeControl RelativeControlFrom(global::Character character)
	{
		if (!character)
		{
			return global::RelativeControl.Incompatible;
		}
		return this.RelativeControlFrom(character.controllable);
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x00026118 File Offset: 0x00024318
	public new global::RelativeControl RelativeControlFrom(IDMain idMain)
	{
		if (!(idMain is global::Character))
		{
			return global::RelativeControl.Incompatible;
		}
		return this.RelativeControlFrom((global::Character)idMain);
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x00026134 File Offset: 0x00024334
	public new global::RelativeControl RelativeControlFrom(global::IDLocalCharacter idLocal)
	{
		if (!idLocal)
		{
			return global::RelativeControl.Incompatible;
		}
		return this.RelativeControlFrom(idLocal.idMain.controllable);
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x00026160 File Offset: 0x00024360
	public new global::RelativeControl RelativeControlFrom(IDBase idBase)
	{
		if (!idBase)
		{
			return global::RelativeControl.Incompatible;
		}
		return this.RelativeControlFrom(idBase.idMain as global::Character);
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x00026180 File Offset: 0x00024380
	internal void PrepareInstantiate(Facepunch.NetworkView view, ref uLink.NetworkMessageInfo info)
	{
		this.__controllerCreateMessageInfo = info;
		this.__networkViewForControllable = view;
		if (this.classFlagsRootControllable || this.classFlagsStandaloneVessel)
		{
			this.__controllerDriverViewID = uLink.NetworkViewID.unassigned;
			if (this.classFlagsStandaloneVessel)
			{
				return;
			}
		}
		else if (this.classFlagsDependantVessel || this.classFlagsFreeVessel)
		{
			global::PlayerClient playerClient;
			if (global::PlayerClient.Find(view.owner, out playerClient))
			{
				this.__controllerDriverViewID = playerClient.topControllable.networkViewID;
			}
			else
			{
				this.__controllerDriverViewID = uLink.NetworkViewID.unassigned;
			}
			if (this.classFlagsFreeVessel)
			{
				return;
			}
			if (this.__controllerDriverViewID == uLink.NetworkViewID.unassigned)
			{
				Debug.LogError("NOT RIGHT");
				return;
			}
		}
		this.FreshInitializeController();
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x0002624C File Offset: 0x0002444C
	internal void FreshInitializeController()
	{
		if (this.__controllerDriverViewID == uLink.NetworkViewID.unassigned)
		{
			if ((this.F & global::Controllable.ControlFlags.Initialized) == global::Controllable.ControlFlags.Initialized)
			{
				throw new InvalidOperationException("Was already intialized.");
			}
			global::Controllable.Chain.ROOT(this);
			this.F = global::Controllable.ControlFlags.Root;
			this.InitializeController_OnFoundOverriding(null);
		}
		else
		{
			Facepunch.NetworkView driverView = Facepunch.NetworkView.Find(this.__controllerDriverViewID);
			this.F |= (global::Controllable.ControlFlags)0;
			this.InitializeController_OnFoundOverriding(driverView);
		}
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x000262C4 File Offset: 0x000244C4
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

	// Token: 0x060008E6 RID: 2278 RVA: 0x0002634C File Offset: 0x0002454C
	private bool EnsureControllee(uLink.NetworkPlayer player)
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

	// Token: 0x060008E7 RID: 2279 RVA: 0x000263D0 File Offset: 0x000245D0
	private void InitializeController_OnFoundOverriding(Facepunch.NetworkView driverView)
	{
		if ((this.F & global::Controllable.ControlFlags.Root) == (global::Controllable.ControlFlags)0)
		{
			global::Character character = driverView.idMain as global::Character;
			global::Controllable controllable = character.controllable;
			this.F = ((this.F & (global::Controllable.ControlFlags.Root | global::Controllable.ControlFlags.Strong)) | (controllable.F & (global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player)));
			this._playerClient = controllable.playerClient;
			controllable.ch.Add(this);
		}
		else
		{
			this.F |= ((!this.__networkViewForControllable.isMine) ? ((global::Controllable.ControlFlags)0) : global::Controllable.ControlFlags.Local);
			this.F |= ((!global::PlayerClient.Find(this.__networkViewForControllable.owner, out this._playerClient)) ? global::Controllable.ControlFlags.Owned : (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player));
		}
		this.F |= global::Controllable.ControlFlags.Owned;
		string controllerClassName = this.controllerClassName;
		if (string.IsNullOrEmpty(controllerClassName))
		{
			global::Controllable.ControlFlags f = this.F;
			this.F = (global::Controllable.ControlFlags)0;
			throw new ArgumentOutOfRangeException("@class", f, "The ControllerClass did not support given flags");
		}
		global::Controller controller = null;
		try
		{
			controller = base.AddAddon<global::Controller>(controllerClassName);
			if (!controller)
			{
				throw new ArgumentOutOfRangeException("className", controllerClassName, "classname as not a Controller!");
			}
			this._controller = controller;
			global::Controller controller2 = this._controller;
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
			this.F |= global::Controllable.ControlFlags.Initialized;
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

	// Token: 0x17000213 RID: 531
	// (get) Token: 0x060008E8 RID: 2280 RVA: 0x000265C4 File Offset: 0x000247C4
	public bool forwardsPlayerClientInput
	{
		get
		{
			return this._controller && this._controller.forwardsPlayerClientInput;
		}
	}

	// Token: 0x17000214 RID: 532
	// (get) Token: 0x060008E9 RID: 2281 RVA: 0x000265E4 File Offset: 0x000247E4
	public bool doesNotSave
	{
		get
		{
			return !this._controller || this._controller.doesNotSave;
		}
	}

	// Token: 0x17000215 RID: 533
	// (get) Token: 0x060008EA RID: 2282 RVA: 0x00026604 File Offset: 0x00024804
	public new global::Controllable masterControllable
	{
		get
		{
			return (!this.ch.vl) ? null : this.ch.tp;
		}
	}

	// Token: 0x17000216 RID: 534
	// (get) Token: 0x060008EB RID: 2283 RVA: 0x00026628 File Offset: 0x00024828
	public new global::Controllable rootControllable
	{
		get
		{
			return (!this.ch.vl) ? null : this.ch.bt;
		}
	}

	// Token: 0x17000217 RID: 535
	// (get) Token: 0x060008EC RID: 2284 RVA: 0x0002664C File Offset: 0x0002484C
	public new global::Controllable nextControllable
	{
		get
		{
			return (!this.ch.vl || !this.ch.up.vl) ? null : this.ch.up.it;
		}
	}

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x060008ED RID: 2285 RVA: 0x0002668C File Offset: 0x0002488C
	public new global::Controllable previousControllable
	{
		get
		{
			return (!this.ch.vl || !this.ch.dn.vl) ? null : this.ch.dn.it;
		}
	}

	// Token: 0x17000219 RID: 537
	// (get) Token: 0x060008EE RID: 2286 RVA: 0x000266CC File Offset: 0x000248CC
	public new global::Controller masterController
	{
		get
		{
			return (!this.ch.vl) ? null : this.ch.tp._controller;
		}
	}

	// Token: 0x1700021A RID: 538
	// (get) Token: 0x060008EF RID: 2287 RVA: 0x00026700 File Offset: 0x00024900
	public new global::Controller rootController
	{
		get
		{
			return (!this.ch.vl) ? null : this.ch.bt._controller;
		}
	}

	// Token: 0x1700021B RID: 539
	// (get) Token: 0x060008F0 RID: 2288 RVA: 0x00026734 File Offset: 0x00024934
	public new global::Controller nextController
	{
		get
		{
			return (!this.ch.vl || !this.ch.up.vl) ? null : this.ch.up.it._controller;
		}
	}

	// Token: 0x1700021C RID: 540
	// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00026784 File Offset: 0x00024984
	public new global::Controller previousController
	{
		get
		{
			return (!this.ch.vl || !this.ch.dn.vl) ? null : this.ch.dn.it._controller;
		}
	}

	// Token: 0x1700021D RID: 541
	// (get) Token: 0x060008F2 RID: 2290 RVA: 0x000267D4 File Offset: 0x000249D4
	public new global::Character masterCharacter
	{
		get
		{
			return (!this.ch.vl) ? null : this.ch.tp.idMain;
		}
	}

	// Token: 0x1700021E RID: 542
	// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00026808 File Offset: 0x00024A08
	public new global::Character rootCharacter
	{
		get
		{
			return (!this.ch.vl) ? null : this.ch.bt.idMain;
		}
	}

	// Token: 0x1700021F RID: 543
	// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0002683C File Offset: 0x00024A3C
	public new global::Character nextCharacter
	{
		get
		{
			return (!this.ch.vl || !this.ch.up.vl) ? null : this.ch.up.it.idMain;
		}
	}

	// Token: 0x17000220 RID: 544
	// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0002688C File Offset: 0x00024A8C
	public new global::Character previousCharacter
	{
		get
		{
			return (!this.ch.vl || !this.ch.dn.vl) ? null : this.ch.dn.it.idMain;
		}
	}

	// Token: 0x17000221 RID: 545
	// (get) Token: 0x060008F6 RID: 2294 RVA: 0x000268DC File Offset: 0x00024ADC
	public new int controlDepth
	{
		get
		{
			return this.ch.id;
		}
	}

	// Token: 0x17000222 RID: 546
	// (get) Token: 0x060008F7 RID: 2295 RVA: 0x000268EC File Offset: 0x00024AEC
	public new int controlCount
	{
		get
		{
			return this.ch.su;
		}
	}

	// Token: 0x17000223 RID: 547
	// (get) Token: 0x060008F8 RID: 2296 RVA: 0x000268FC File Offset: 0x00024AFC
	internal bool classAssigned
	{
		get
		{
			return this.@class;
		}
	}

	// Token: 0x17000224 RID: 548
	// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0002690C File Offset: 0x00024B0C
	internal bool classFlagsRootControllable
	{
		get
		{
			return this.@class && this.@class.root;
		}
	}

	// Token: 0x17000225 RID: 549
	// (get) Token: 0x060008FA RID: 2298 RVA: 0x0002692C File Offset: 0x00024B2C
	internal bool classFlagsVessel
	{
		get
		{
			return this.@class && this.@class.vessel;
		}
	}

	// Token: 0x17000226 RID: 550
	// (get) Token: 0x060008FB RID: 2299 RVA: 0x0002694C File Offset: 0x00024B4C
	internal bool classFlagsStandaloneVessel
	{
		get
		{
			return this.@class && this.@class.vesselStandalone;
		}
	}

	// Token: 0x17000227 RID: 551
	// (get) Token: 0x060008FC RID: 2300 RVA: 0x0002696C File Offset: 0x00024B6C
	internal bool classFlagsDependantVessel
	{
		get
		{
			return this.@class && this.@class.vesselDependant;
		}
	}

	// Token: 0x17000228 RID: 552
	// (get) Token: 0x060008FD RID: 2301 RVA: 0x0002698C File Offset: 0x00024B8C
	internal bool classFlagsFreeVessel
	{
		get
		{
			return this.@class && this.@class.vesselFree;
		}
	}

	// Token: 0x17000229 RID: 553
	// (get) Token: 0x060008FE RID: 2302 RVA: 0x000269AC File Offset: 0x00024BAC
	internal bool classFlagsStaticGroup
	{
		get
		{
			return this.@class && this.@class.staticGroup;
		}
	}

	// Token: 0x1700022A RID: 554
	// (get) Token: 0x060008FF RID: 2303 RVA: 0x000269CC File Offset: 0x00024BCC
	internal bool classFlagsPlayerSupport
	{
		get
		{
			return this.@class && this.@class.DefinesClass(true);
		}
	}

	// Token: 0x1700022B RID: 555
	// (get) Token: 0x06000900 RID: 2304 RVA: 0x000269F0 File Offset: 0x00024BF0
	internal bool classFlagsAISupport
	{
		get
		{
			return this.@class && this.@class.DefinesClass(false);
		}
	}

	// Token: 0x1700022C RID: 556
	// (get) Token: 0x06000901 RID: 2305 RVA: 0x00026A14 File Offset: 0x00024C14
	public new string controllerClassName
	{
		get
		{
			return (!this.@class) ? null : this.@class.GetClassName((this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player)) == (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player), (this.F & global::Controllable.ControlFlags.Local) == global::Controllable.ControlFlags.Local);
		}
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x00026A50 File Offset: 0x00024C50
	internal void ProcessLocalPlayerPreRender()
	{
		this._controller.ProcessLocalPlayerPreRender();
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x00026A60 File Offset: 0x00024C60
	[Conditional("LOG_CONTROL_CHANGE")]
	private static void LogState(bool guard, string state, global::Controllable controllable)
	{
		Debug.Log(string.Format("{2}{0}::{1}", controllable.GetType().Name, state, (!guard) ? "-" : "+"), controllable);
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x00026AA0 File Offset: 0x00024CA0
	[Conditional("LOG_CONTROL_CHANGE")]
	private static void GuardState(string state, global::Controllable self)
	{
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x00026AA4 File Offset: 0x00024CA4
	[Conditional("LOG_CONTROL_CHANGE")]
	private static void UnguardState(string state, global::Controllable self)
	{
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x00026AA8 File Offset: 0x00024CA8
	private void ControlEnter(int cmd)
	{
		try
		{
			this._controller.ControlEnter(cmd);
		}
		finally
		{
			if ((this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player | global::Controllable.ControlFlags.Root)) == (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player | global::Controllable.ControlFlags.Root))
			{
				try
				{
					this._playerClient.OnRootControllableEntered(this);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex, this);
				}
				if ((this.F & global::Controllable.ControlFlags.Local) == global::Controllable.ControlFlags.Local)
				{
					global::Controllable.localPlayerControllableCount++;
					global::Controllable.LocalOnly.rootLocalPlayerControllables.Add(this);
				}
			}
		}
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00026B50 File Offset: 0x00024D50
	private void ControlExit(int cmd)
	{
		try
		{
			this._controller.ControlExit(cmd);
		}
		finally
		{
			if ((this.F & (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player | global::Controllable.ControlFlags.Root)) == (global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player | global::Controllable.ControlFlags.Root))
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
				if ((this.F & global::Controllable.ControlFlags.Local) == global::Controllable.ControlFlags.Local)
				{
					global::Controllable.localPlayerControllableCount--;
					global::Controllable.LocalOnly.rootLocalPlayerControllables.Remove(this);
				}
			}
		}
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x00026C08 File Offset: 0x00024E08
	private void Net_Shutdown_Exit()
	{
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x00026C0C File Offset: 0x00024E0C
	private void ControlEngauge(int cmd)
	{
		this._controller.ControlEngauge(cmd);
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x00026C1C File Offset: 0x00024E1C
	private void ControlCease(int cmd)
	{
		this._controller.ControlCease(cmd);
	}

	// Token: 0x1700022D RID: 557
	// (get) Token: 0x0600090B RID: 2315 RVA: 0x00026C2C File Offset: 0x00024E2C
	public new global::RPOSLimitFlags rposLimitFlags
	{
		get
		{
			return (!this._controller) ? ((global::RPOSLimitFlags)-1) : this._controller.rposLimitFlags;
		}
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x00026C50 File Offset: 0x00024E50
	[Obsolete("Used only by PlayerClient")]
	internal void SetRootPlayer(global::PlayerClient rootPlayer)
	{
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x00026C54 File Offset: 0x00024E54
	private bool SetIdle(bool idle)
	{
		global::IDLocalCharacterIdleControl idleControl = base.idMain.idleControl;
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

	// Token: 0x0600090E RID: 2318 RVA: 0x00026CC0 File Offset: 0x00024EC0
	[Obsolete("RPC call only. Do not call through script", false)]
	[RPC]
	private void CLD(uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x00026CC4 File Offset: 0x00024EC4
	[Obsolete("RPC call only. Do not call through script", false)]
	[RPC]
	private void CLR(uLink.NetworkMessageInfo info)
	{
		this.ch.Delete();
		this.SharedPostCLR();
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x00026CD8 File Offset: 0x00024ED8
	private void SharedPostCLR()
	{
		if (this._controller)
		{
			Object.Destroy(this._controller);
		}
		this.F &= (global::Controllable.ControlFlags.Root | global::Controllable.ControlFlags.Strong);
		this.RT = 0;
		this._playerClient = null;
		this._controller = null;
		this.SetIdle(true);
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x00026D2C File Offset: 0x00024F2C
	[Obsolete("RPC call only. Do not call through script", false)]
	[RPC]
	private void ID1()
	{
		this.SetIdle(true);
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x00026D38 File Offset: 0x00024F38
	[RPC]
	private void OC2(uLink.NetworkViewID rootViewID, uLink.NetworkViewID parentViewID, uLink.NetworkMessageInfo info)
	{
		this.OverrideControlOfHandleRPC(rootViewID, parentViewID, ref info);
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x00026D44 File Offset: 0x00024F44
	[RPC]
	private void OC1(uLink.NetworkViewID rootViewID, uLink.NetworkMessageInfo info)
	{
		this.OverrideControlOfHandleRPC(rootViewID, rootViewID, ref info);
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x00026D50 File Offset: 0x00024F50
	private void OverrideControlOfHandleRPC(uLink.NetworkViewID rootViewID, uLink.NetworkViewID parentViewID, ref uLink.NetworkMessageInfo info)
	{
		this.CL_OverideControlOf(rootViewID, parentViewID, ref info);
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x00026D5C File Offset: 0x00024F5C
	[RPC]
	private void RN0(uLink.NetworkMessageInfo info)
	{
		this.RN(0, ref info);
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x00026D68 File Offset: 0x00024F68
	[RPC]
	private void RN1(uLink.NetworkMessageInfo info)
	{
		this.RN(1, ref info);
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x00026D74 File Offset: 0x00024F74
	[RPC]
	private void RN2(uLink.NetworkMessageInfo info)
	{
		this.RN(2, ref info);
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x00026D80 File Offset: 0x00024F80
	[RPC]
	private void RN3(uLink.NetworkMessageInfo info)
	{
		this.RN(3, ref info);
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x00026D8C File Offset: 0x00024F8C
	[RPC]
	private void RN4(uLink.NetworkMessageInfo info)
	{
		this.RN(4, ref info);
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x00026D98 File Offset: 0x00024F98
	[RPC]
	private void RN5(uLink.NetworkMessageInfo info)
	{
		this.RN(5, ref info);
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x00026DA4 File Offset: 0x00024FA4
	[RPC]
	private void RN6(uLink.NetworkMessageInfo info)
	{
		this.RN(6, ref info);
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x00026DB0 File Offset: 0x00024FB0
	[RPC]
	private void RN7(uLink.NetworkMessageInfo info)
	{
		this.RN(7, ref info);
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x00026DBC File Offset: 0x00024FBC
	[RPC]
	private void RFH(byte top)
	{
		this.CL_Refresh((int)top);
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x00026DC8 File Offset: 0x00024FC8
	internal void OnInstantiated()
	{
		if ((this.F & global::Controllable.ControlFlags.Root) == global::Controllable.ControlFlags.Root)
		{
			this.ch.RefreshEngauge();
		}
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x00026DE4 File Offset: 0x00024FE4
	private void OCO_FOUND(uLink.NetworkViewID viewID, ref uLink.NetworkMessageInfo info)
	{
		this.SetIdle(false);
		this.__networkViewForControllable = base.networkView;
		this.__controllerDriverViewID = viewID;
		this.__controllerCreateMessageInfo = info;
		this.FreshInitializeController();
	}

	// Token: 0x1700022E RID: 558
	// (get) Token: 0x06000920 RID: 2336 RVA: 0x00026E10 File Offset: 0x00025010
	public static bool localPlayerControllableExists
	{
		get
		{
			return global::Controllable.localPlayerControllableCount > 0;
		}
	}

	// Token: 0x1700022F RID: 559
	// (get) Token: 0x06000921 RID: 2337 RVA: 0x00026E1C File Offset: 0x0002501C
	public static global::Controllable localPlayerControllable
	{
		get
		{
			int num = global::Controllable.localPlayerControllableCount;
			if (num == 0)
			{
				return null;
			}
			if (num != 1)
			{
				return global::Controllable.LocalOnly.rootLocalPlayerControllables[global::Controllable.localPlayerControllableCount - 1];
			}
			return global::Controllable.LocalOnly.rootLocalPlayerControllables[0];
		}
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x00026E60 File Offset: 0x00025060
	private void OnDestroy()
	{
		this.CL_Clear();
		if (this.isInContextQuery)
		{
			try
			{
				if (global::Controllable.onDestroyInContextQuery != null)
				{
					global::Controllable.onDestroyInContextQuery(this);
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

	// Token: 0x06000923 RID: 2339 RVA: 0x00026F0C File Offset: 0x0002510C
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

	// Token: 0x06000924 RID: 2340 RVA: 0x00026F78 File Offset: 0x00025178
	internal bool MergeClasses(ref global::ControllerClass.Merge merge)
	{
		return this.@class && merge.Add(this.controllable.@class);
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x00026FAC File Offset: 0x000251AC
	internal static bool MergeClasses(IDMain character, ref global::ControllerClass.Merge merge)
	{
		global::Controllable component;
		return character && (component = character.GetComponent<global::Controllable>()) && component.MergeClasses(ref merge);
	}

	// Token: 0x17000230 RID: 560
	// (get) Token: 0x06000926 RID: 2342 RVA: 0x00026FE0 File Offset: 0x000251E0
	public static IEnumerable<global::Controllable> PlayerRootControllables
	{
		get
		{
			foreach (global::PlayerClient pc in global::PlayerClient.All)
			{
				global::Controllable controllable = pc.rootControllable;
				if (controllable)
				{
					yield return controllable;
				}
			}
			yield break;
		}
	}

	// Token: 0x17000231 RID: 561
	// (get) Token: 0x06000927 RID: 2343 RVA: 0x00026FFC File Offset: 0x000251FC
	public static IEnumerable<global::Controllable> PlayerCurrentControllables
	{
		get
		{
			foreach (global::PlayerClient pc in global::PlayerClient.All)
			{
				global::Controllable controllable = pc.controllable;
				if (controllable)
				{
					yield return controllable;
				}
			}
			yield break;
		}
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x00027018 File Offset: 0x00025218
	public static IEnumerable<global::Controllable> RootControllers(IEnumerable<global::PlayerClient> playerClients)
	{
		foreach (global::PlayerClient pc in playerClients)
		{
			global::Controllable controllable = pc.rootControllable;
			if (controllable)
			{
				yield return controllable;
			}
		}
		yield break;
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x00027044 File Offset: 0x00025244
	public static IEnumerable<global::Controllable> CurrentControllers(IEnumerable<global::PlayerClient> playerClients)
	{
		foreach (global::PlayerClient pc in playerClients)
		{
			global::Controllable controllable = pc.controllable;
			if (controllable)
			{
				yield return controllable;
			}
		}
		yield break;
	}

	// Token: 0x0400063B RID: 1595
	private const int RT_ENTERED = 1;

	// Token: 0x0400063C RID: 1596
	private const int RT_PROMOTED = 3;

	// Token: 0x0400063D RID: 1597
	private const int RT_ENTER_LOCK = 8;

	// Token: 0x0400063E RID: 1598
	private const int RT_PROMO_LOCK = 16;

	// Token: 0x0400063F RID: 1599
	private const int RT_DESTROY_LOCK = 32;

	// Token: 0x04000640 RID: 1600
	private const int RT_ENTERED_ONCE = 64;

	// Token: 0x04000641 RID: 1601
	private const int RT_PROMOTED_ONCE = 128;

	// Token: 0x04000642 RID: 1602
	private const int RT_DEMOTED_ONCE = 256;

	// Token: 0x04000643 RID: 1603
	private const int RT_EXITED_ONCE = 512;

	// Token: 0x04000644 RID: 1604
	private const int RT_WILL_DESTROY = 1024;

	// Token: 0x04000645 RID: 1605
	private const int RT_IS_DESTROYED = 2048;

	// Token: 0x04000646 RID: 1606
	private const int RT_RPC_CONTROL_0 = 4096;

	// Token: 0x04000647 RID: 1607
	private const int RT_RPC_CONTROL_1 = 8192;

	// Token: 0x04000648 RID: 1608
	private const int RT_RPC_CONTROL_2 = 12288;

	// Token: 0x04000649 RID: 1609
	private const int RT_STATE = 3;

	// Token: 0x0400064A RID: 1610
	private const int RT_ONCE = 960;

	// Token: 0x0400064B RID: 1611
	private const int RT_DESTROY_STATE = 3072;

	// Token: 0x0400064C RID: 1612
	private const int RT_RPC_CONTROL = 12288;

	// Token: 0x0400064D RID: 1613
	private const global::Controllable.ControlFlags PERSISTANT_FLAGS = global::Controllable.ControlFlags.Root | global::Controllable.ControlFlags.Strong;

	// Token: 0x0400064E RID: 1614
	private const global::Controllable.ControlFlags MUTABLE_FLAGS = global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player | global::Controllable.ControlFlags.Initialized;

	// Token: 0x0400064F RID: 1615
	private const global::Controllable.ControlFlags TRANSFERED_FLAGS = global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player;

	// Token: 0x04000650 RID: 1616
	private const global::Controllable.ControlFlags CONTROLLER_NPC = (global::Controllable.ControlFlags)0;

	// Token: 0x04000651 RID: 1617
	private const global::Controllable.ControlFlags CONTROLLER_CLIENT = global::Controllable.ControlFlags.Player;

	// Token: 0x04000652 RID: 1618
	private const global::Controllable.ControlFlags NETWORK_MINE = global::Controllable.ControlFlags.Local;

	// Token: 0x04000653 RID: 1619
	private const global::Controllable.ControlFlags NETWORK_PROXY = (global::Controllable.ControlFlags)0;

	// Token: 0x04000654 RID: 1620
	private const global::Controllable.ControlFlags ACTIVE_OCCUPIED = global::Controllable.ControlFlags.Owned;

	// Token: 0x04000655 RID: 1621
	private const global::Controllable.ControlFlags ACTIVE_VACANT = (global::Controllable.ControlFlags)0;

	// Token: 0x04000656 RID: 1622
	private const global::Controllable.ControlFlags TREE_TRUNK = global::Controllable.ControlFlags.Root;

	// Token: 0x04000657 RID: 1623
	private const global::Controllable.ControlFlags TREE_BRANCH = (global::Controllable.ControlFlags)0;

	// Token: 0x04000658 RID: 1624
	private const global::Controllable.ControlFlags SETUP_INITIALIZED = global::Controllable.ControlFlags.Initialized;

	// Token: 0x04000659 RID: 1625
	private const global::Controllable.ControlFlags SETUP_UNINITIALIZED = (global::Controllable.ControlFlags)0;

	// Token: 0x0400065A RID: 1626
	private const global::Controllable.ControlFlags BINDING_STRONG = global::Controllable.ControlFlags.Strong;

	// Token: 0x0400065B RID: 1627
	private const global::Controllable.ControlFlags BINDING_WEAK = (global::Controllable.ControlFlags)0;

	// Token: 0x0400065C RID: 1628
	private const global::Controllable.ControlFlags CONTROLLER_MASK = global::Controllable.ControlFlags.Player;

	// Token: 0x0400065D RID: 1629
	private const global::Controllable.ControlFlags NETWORK_MASK = global::Controllable.ControlFlags.Local;

	// Token: 0x0400065E RID: 1630
	private const global::Controllable.ControlFlags ACTIVE_MASK = global::Controllable.ControlFlags.Owned;

	// Token: 0x0400065F RID: 1631
	private const global::Controllable.ControlFlags TREE_MASK = global::Controllable.ControlFlags.Root;

	// Token: 0x04000660 RID: 1632
	private const global::Controllable.ControlFlags SETUP_MASK = global::Controllable.ControlFlags.Initialized;

	// Token: 0x04000661 RID: 1633
	private const global::Controllable.ControlFlags BINDING_MASK = global::Controllable.ControlFlags.Strong;

	// Token: 0x04000662 RID: 1634
	private const global::Controllable.ControlFlags MASK = global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player | global::Controllable.ControlFlags.Root | global::Controllable.ControlFlags.Initialized | global::Controllable.ControlFlags.Strong;

	// Token: 0x04000663 RID: 1635
	private const global::Controllable.ControlFlags OWNER_MASK = global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player;

	// Token: 0x04000664 RID: 1636
	private const global::Controllable.ControlFlags OWNER_NPC = global::Controllable.ControlFlags.Owned;

	// Token: 0x04000665 RID: 1637
	private const global::Controllable.ControlFlags OWNER_CLIENT = global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player;

	// Token: 0x04000666 RID: 1638
	private const global::Controllable.ControlFlags OWNER_NET_MASK = global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player;

	// Token: 0x04000667 RID: 1639
	private const global::Controllable.ControlFlags OWNER_NET_NPC_MINE = global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local;

	// Token: 0x04000668 RID: 1640
	private const global::Controllable.ControlFlags OWNER_NET_NPC_PROXY = global::Controllable.ControlFlags.Owned;

	// Token: 0x04000669 RID: 1641
	private const global::Controllable.ControlFlags OWNER_NET_CLIENT_MINE = global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player;

	// Token: 0x0400066A RID: 1642
	private const global::Controllable.ControlFlags OWNER_NET_CLIENT_PROXY = global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player;

	// Token: 0x0400066B RID: 1643
	private const global::Controllable.ControlFlags OWNER_NET_TREE_MASK = global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player | global::Controllable.ControlFlags.Root;

	// Token: 0x0400066C RID: 1644
	private const global::Controllable.ControlFlags OWNER_NET_TREE_NPC_MINE_TRUNK = global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Root;

	// Token: 0x0400066D RID: 1645
	private const global::Controllable.ControlFlags OWNER_NET_TREE_NPC_PROXY_TRUNK = global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Root;

	// Token: 0x0400066E RID: 1646
	private const global::Controllable.ControlFlags OWNER_NET_TREE_CLIENT_MINE_TRUNK = global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player | global::Controllable.ControlFlags.Root;

	// Token: 0x0400066F RID: 1647
	private const global::Controllable.ControlFlags OWNER_NET_TREE_CLIENT_PROXY_TRUNK = global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player | global::Controllable.ControlFlags.Root;

	// Token: 0x04000670 RID: 1648
	private const global::Controllable.ControlFlags OWNER_NET_TREE_NPC_MINE_BRANCH = global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local;

	// Token: 0x04000671 RID: 1649
	private const global::Controllable.ControlFlags OWNER_NET_TREE_NPC_PROXY_BRANCH = global::Controllable.ControlFlags.Owned;

	// Token: 0x04000672 RID: 1650
	private const global::Controllable.ControlFlags OWNER_NET_TREE_CLIENT_MINE_BRANCH = global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Local | global::Controllable.ControlFlags.Player;

	// Token: 0x04000673 RID: 1651
	private const global::Controllable.ControlFlags OWNER_NET_TREE_CLIENT_PROXY_BRANCH = global::Controllable.ControlFlags.Owned | global::Controllable.ControlFlags.Player;

	// Token: 0x04000674 RID: 1652
	private const string kControllableRPCPrefix = "Controllable:";

	// Token: 0x04000675 RID: 1653
	private const string kClientDeleteRPCName = "Controllable:CLD";

	// Token: 0x04000676 RID: 1654
	private const string kClearFromChainRPCName = "Controllable:CLR";

	// Token: 0x04000677 RID: 1655
	private const string kIdleOnRPCName = "Controllable:ID1";

	// Token: 0x04000678 RID: 1656
	private const string kOverrideControlOfRPCName1 = "Controllable:OC1";

	// Token: 0x04000679 RID: 1657
	private const string kOverrideControlOfRPCName2 = "Controllable:OC2";

	// Token: 0x0400067A RID: 1658
	private const string kClientRefreshRPCName = "Controllable:RFH";

	// Token: 0x0400067B RID: 1659
	private const uLink.RPCMode kClientDeleteRPCMode = 0;

	// Token: 0x0400067C RID: 1660
	private const uLink.RPCMode kClearFromChainRPCMode = 2;

	// Token: 0x0400067D RID: 1661
	private const uLink.RPCMode kClearFromChainRPCMode_POST = 1;

	// Token: 0x0400067E RID: 1662
	private const uLink.RPCMode kOverrideControlOfRPCMode = 6;

	// Token: 0x0400067F RID: 1663
	private const uLink.RPCMode kIdleOnRPCMode = 6;

	// Token: 0x04000680 RID: 1664
	private const uLink.RPCMode kClientSideRootNumberRPCMode = 5;

	// Token: 0x04000681 RID: 1665
	private const uLink.RPCMode kClientRefreshRPCMode = 5;

	// Token: 0x04000682 RID: 1666
	private const string kRPCCall = "RPC call only. Do not call through script";

	// Token: 0x04000683 RID: 1667
	private const bool kRPCCallError = false;

	// Token: 0x04000684 RID: 1668
	[NonSerialized]
	private global::Controllable.CL_Binder _binder;

	// Token: 0x04000685 RID: 1669
	[NonSerialized]
	private List<ulong> _rootCountTimeStamps;

	// Token: 0x04000686 RID: 1670
	[NonSerialized]
	private int _pendingControlCount;

	// Token: 0x04000687 RID: 1671
	[NonSerialized]
	private int _refreshedControlCount;

	// Token: 0x04000688 RID: 1672
	[SerializeField]
	private global::ControllerClass @class;

	// Token: 0x04000689 RID: 1673
	[NonSerialized]
	private global::PlayerClient _playerClient;

	// Token: 0x0400068A RID: 1674
	[NonSerialized]
	private global::Controller _controller;

	// Token: 0x0400068B RID: 1675
	[NonSerialized]
	private global::Controllable.ControlFlags F;

	// Token: 0x0400068C RID: 1676
	[NonSerialized]
	private global::Controllable.Chain ch;

	// Token: 0x0400068D RID: 1677
	[NonSerialized]
	private int RT;

	// Token: 0x0400068E RID: 1678
	[NonSerialized]
	private uLink.NetworkViewID __controllerDriverViewID;

	// Token: 0x0400068F RID: 1679
	[NonSerialized]
	private uLink.NetworkMessageInfo __controllerCreateMessageInfo;

	// Token: 0x04000690 RID: 1680
	[NonSerialized]
	private uLink.NetworkView __networkViewForControllable;

	// Token: 0x04000691 RID: 1681
	[NonSerialized]
	private bool lateFinding;

	// Token: 0x04000692 RID: 1682
	[NonSerialized]
	public bool isInContextQuery;

	// Token: 0x04000693 RID: 1683
	private static int localPlayerControllableCount;

	// Token: 0x0200013D RID: 317
	private struct Chain
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00027070 File Offset: 0x00025270
		public int id
		{
			get
			{
				return (!this.vl) ? -1 : ((int)this.nm);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0002708C File Offset: 0x0002528C
		public int su
		{
			get
			{
				return (!this.vl) ? -1 : ((int)(1 + this.nm + this.ln));
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x000270BC File Offset: 0x000252BC
		public static void ROOT(global::Controllable root)
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

		// Token: 0x0600092D RID: 2349 RVA: 0x0002716C File Offset: 0x0002536C
		private bool Add(ref global::Controllable.Chain nw, global::Controllable ct)
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
			global::Controllable.Link link = nw.dn;
			nw.iv = true;
			do
			{
				link.it.ch.tp = nw.tp;
				global::Controllable controllable = link.it;
				controllable.ch.ln = controllable.ch.ln + 1;
				link.it.ch.iv = true;
				link = link.it.ch.dn;
			}
			while (link.vl);
			nw.it.ON_CHAIN_SUBSCRIBE();
			return true;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000272DC File Offset: 0x000254DC
		public bool Add(global::Controllable vessel)
		{
			return vessel && this.Add(ref vessel.ch, vessel);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x000272FC File Offset: 0x000254FC
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
					global::Controllable controllable = this.bt;
					num = 128;
					for (;;)
					{
						controllable.ch.iv = false;
						switch (controllable.RT & 3)
						{
						case 0:
							global::Controllable.DO_ENTER(global::Controllable.CAP_ENTER(num, controllable.RT, controllable.F), controllable);
							break;
						case 3:
							global::Controllable.DO_DEMOTE(global::Controllable.CAP_DEMOTE(num, controllable.RT, controllable.F), controllable);
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
					global::Controllable.DO_ENTER(global::Controllable.CAP_ENTER(num & -129, this.tp.RT, this.tp.F), this.tp);
					global::Controllable.DO_PROMOTE(global::Controllable.CAP_PROMOTE(num & -129, this.tp.RT, this.tp.F), this.tp);
					break;
				case 1:
					global::Controllable.DO_PROMOTE(global::Controllable.CAP_PROMOTE(num & -129, this.tp.RT, this.tp.F), this.tp);
					break;
				}
			}
			return true;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x000274D8 File Offset: 0x000256D8
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
					global::Controllable controllable = this.bt;
					num = 128;
					for (;;)
					{
						switch (controllable.RT & 3)
						{
						case 0:
							global::Controllable.DO_ENTER(global::Controllable.CAP_ENTER(num, controllable.RT, controllable.F), controllable);
							break;
						case 3:
							global::Controllable.DO_DEMOTE(global::Controllable.CAP_DEMOTE(num, controllable.RT, controllable.F), controllable);
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
					global::Controllable.DO_ENTER(global::Controllable.CAP_ENTER(num, this.tp.RT, this.tp.F), this.tp);
					break;
				}
			}
			return true;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00027638 File Offset: 0x00025838
		public override string ToString()
		{
			if (!this.vl)
			{
				return "invalid";
			}
			StringBuilder stringBuilder = new StringBuilder();
			global::Controllable controllable = this.bt;
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

		// Token: 0x06000932 RID: 2354 RVA: 0x000276E0 File Offset: 0x000258E0
		public void Delete()
		{
			if (!this.vl)
			{
				return;
			}
			int num = global::Controllable.CAP_THIS(16, this.it.RT, this.it.F);
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
					global::Controllable controllable = this.tp.ch.dn.it;
					global::Controllable controllable2 = this.tp;
					int cmd;
					switch (controllable2.RT & 3)
					{
					case 1:
						global::Controllable.DO_EXIT(cmd = global::Controllable.CAP_EXIT(num3, controllable2.RT, controllable2.F), controllable2);
						break;
					case 2:
						goto IL_10A;
					case 3:
						cmd = global::Controllable.CAP_EXIT(num3, controllable2.RT, controllable2.F);
						global::Controllable.DO_DEMOTE(global::Controllable.CAP_DEMOTE(cmd, controllable2.RT, controllable2.F), controllable2);
						global::Controllable.DO_EXIT(cmd, controllable2);
						break;
					default:
						goto IL_10A;
					}
					IL_124:
					controllable2.ON_CHAIN_ERASE(cmd);
					controllable2.ch = default(global::Controllable.Chain);
					controllable2.ON_CHAIN_ABOLISHED();
					this.tp = controllable;
					this.tp.ch.up = default(global::Controllable.Link);
					this.tp.ch.ln = this.tp.ch.ln - 1;
					this.tp.ch.tp = this.tp;
					global::Controllable.Link link = this.tp.ch.dn;
					byte b = this.tp.ch.ln;
					while (link.vl)
					{
						global::Controllable controllable3 = link.it;
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
					cmd = global::Controllable.CAP_THIS(num3, controllable2.RT, controllable2.F);
					goto IL_124;
				}
			}
			switch (this.it.RT & 3)
			{
			case 1:
				global::Controllable.DO_EXIT(global::Controllable.CAP_EXIT(num, this.it.RT, this.it.F), this.it);
				break;
			case 3:
				global::Controllable.DO_DEMOTE(global::Controllable.CAP_DEMOTE(num, this.it.RT, this.it.F), this.it);
				global::Controllable.DO_EXIT(global::Controllable.CAP_EXIT(num, this.it.RT, this.it.F), this.it);
				break;
			}
			global::Controllable controllable4 = this.it;
			controllable4.ON_CHAIN_ERASE(num);
			global::Controllable.Link link2 = this.dn;
			controllable4.ch = (this = default(global::Controllable.Chain));
			if (link2.vl)
			{
				global::Controllable controllable5 = link2.it;
				controllable5.ch.up = default(global::Controllable.Link);
				int num4 = 0;
				do
				{
					global::Controllable controllable6 = link2.it;
					link2 = controllable6.ch.dn;
					controllable6.ch.iv = true;
					controllable6.ch.tp = controllable5;
					controllable6.ch.ln = (byte)num4++;
				}
				while (link2.vl);
			}
			controllable4.ON_CHAIN_ABOLISHED();
		}

		// Token: 0x04000695 RID: 1685
		public global::Controllable it;

		// Token: 0x04000696 RID: 1686
		public global::Controllable bt;

		// Token: 0x04000697 RID: 1687
		public global::Controllable tp;

		// Token: 0x04000698 RID: 1688
		public global::Controllable.Link dn;

		// Token: 0x04000699 RID: 1689
		public global::Controllable.Link up;

		// Token: 0x0400069A RID: 1690
		public byte nm;

		// Token: 0x0400069B RID: 1691
		public byte ln;

		// Token: 0x0400069C RID: 1692
		public bool vl;

		// Token: 0x0400069D RID: 1693
		public bool iv;
	}

	// Token: 0x0200013E RID: 318
	private class CL_Binder : IDisposable
	{
		// Token: 0x06000933 RID: 2355 RVA: 0x00027A74 File Offset: 0x00025C74
		public CL_Binder(global::Controllable owner, uLink.NetworkViewID rootID, uLink.NetworkViewID parentID, ref uLink.NetworkMessageInfo info)
		{
			this._root.id = rootID;
			this._parent.id = parentID;
			this._info = info;
			this.owner = owner;
			this.sameSearch = (this._root.id == this._parent.id);
			if (global::Controllable.CL_Binder.binderCount++ == 0)
			{
				global::Controllable.CL_Binder.last = this;
				global::Controllable.CL_Binder.first = this;
			}
			else
			{
				this.prev = global::Controllable.CL_Binder.last;
				this.prev.next = this;
				global::Controllable.CL_Binder.last = this;
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00027B10 File Offset: 0x00025D10
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
			if (--global::Controllable.CL_Binder.binderCount == 0)
			{
				global::Controllable.CL_Binder.first = (global::Controllable.CL_Binder.last = (this.next = (this.prev = null)));
			}
			else
			{
				if (global::Controllable.CL_Binder.first == this)
				{
					global::Controllable.CL_Binder.first = this.next;
					this.next.prev = null;
				}
				else if (global::Controllable.CL_Binder.last == this)
				{
					global::Controllable.CL_Binder.last = this.prev;
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

		// Token: 0x06000935 RID: 2357 RVA: 0x00027C14 File Offset: 0x00025E14
		public bool Find()
		{
			return this._root.Find() && (this.sameSearch || this._parent.Find());
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00027C50 File Offset: 0x00025E50
		protected int CountValidate(List<ulong> ts, int tsCount)
		{
			if (this.Find())
			{
				global::Controllable controllable = (!this.sameSearch) ? this._parent.controllable : this._root.controllable;
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

		// Token: 0x06000937 RID: 2359 RVA: 0x00027CF8 File Offset: 0x00025EF8
		public bool CanLink()
		{
			if (this._root.Find() && this._root.controllable._rootCountTimeStamps != null)
			{
				int num = this.CountValidate(this._root.controllable._rootCountTimeStamps, this._root.controllable._rootCountTimeStamps.Count);
				return num == this._root.controllable._pendingControlCount;
			}
			return false;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00027D6C File Offset: 0x00025F6C
		private void PreLink()
		{
			global::Controllable controllable = (!this.sameSearch) ? this._parent.controllable : this._root.controllable;
			if ((controllable.F & global::Controllable.ControlFlags.Root) == (global::Controllable.ControlFlags)0)
			{
				controllable._binder.PreLink();
			}
			if ((this.owner.F & global::Controllable.ControlFlags.Initialized) == (global::Controllable.ControlFlags)0)
			{
				this.owner.OCO_FOUND(controllable.networkViewID, ref this._info);
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00027DE4 File Offset: 0x00025FE4
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

		// Token: 0x0600093A RID: 2362 RVA: 0x00027E50 File Offset: 0x00026050
		public static void StaticLink(global::Controllable root)
		{
			global::Controllable.CL_Binder cl_Binder = global::Controllable.CL_Binder.last;
			for (int i = global::Controllable.CL_Binder.binderCount - 1; i >= 0; i--)
			{
				global::Controllable.CL_Binder cl_Binder2 = cl_Binder;
				cl_Binder = cl_Binder.prev;
				if (cl_Binder2.Find() && cl_Binder2._root.controllable == root && cl_Binder2.CountValidate(root._rootCountTimeStamps, root._rootCountTimeStamps.Count) == root._refreshedControlCount)
				{
					cl_Binder2.Link();
					return;
				}
			}
		}

		// Token: 0x0400069E RID: 1694
		private static global::Controllable.CL_Binder first;

		// Token: 0x0400069F RID: 1695
		private static global::Controllable.CL_Binder last;

		// Token: 0x040006A0 RID: 1696
		private static int binderCount;

		// Token: 0x040006A1 RID: 1697
		private global::Controllable.CL_Binder.Search _root;

		// Token: 0x040006A2 RID: 1698
		private global::Controllable.CL_Binder.Search _parent;

		// Token: 0x040006A3 RID: 1699
		private readonly bool sameSearch;

		// Token: 0x040006A4 RID: 1700
		private uLink.NetworkMessageInfo _info;

		// Token: 0x040006A5 RID: 1701
		private readonly global::Controllable owner;

		// Token: 0x040006A6 RID: 1702
		private bool disposed;

		// Token: 0x040006A7 RID: 1703
		private global::Controllable.CL_Binder next;

		// Token: 0x040006A8 RID: 1704
		private global::Controllable.CL_Binder prev;

		// Token: 0x0200013F RID: 319
		private struct Search
		{
			// Token: 0x17000234 RID: 564
			// (get) Token: 0x0600093B RID: 2363 RVA: 0x00027ED0 File Offset: 0x000260D0
			// (set) Token: 0x0600093C RID: 2364 RVA: 0x00027ED8 File Offset: 0x000260D8
			public uLink.NetworkViewID id
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

			// Token: 0x17000235 RID: 565
			// (get) Token: 0x0600093D RID: 2365 RVA: 0x00027EF0 File Offset: 0x000260F0
			public Facepunch.NetworkView view
			{
				get
				{
					return this._view;
				}
			}

			// Token: 0x17000236 RID: 566
			// (get) Token: 0x0600093E RID: 2366 RVA: 0x00027EF8 File Offset: 0x000260F8
			public global::Controllable controllable
			{
				get
				{
					return this._controllable;
				}
			}

			// Token: 0x0600093F RID: 2367 RVA: 0x00027F00 File Offset: 0x00026100
			public bool Find()
			{
				if (!this._controllable)
				{
					if (!this._view)
					{
						this._view = Facepunch.NetworkView.Find(this._id);
						if (!this._view)
						{
							return false;
						}
					}
					global::Character character = this._view.idMain as global::Character;
					return character && (this._controllable = character.controllable);
				}
				return true;
			}

			// Token: 0x040006A9 RID: 1705
			private uLink.NetworkViewID _id;

			// Token: 0x040006AA RID: 1706
			private Facepunch.NetworkView _view;

			// Token: 0x040006AB RID: 1707
			private global::Controllable _controllable;
		}
	}

	// Token: 0x02000140 RID: 320
	[Flags]
	private enum ControlFlags
	{
		// Token: 0x040006AD RID: 1709
		Owned = 1,
		// Token: 0x040006AE RID: 1710
		Local = 2,
		// Token: 0x040006AF RID: 1711
		Player = 4,
		// Token: 0x040006B0 RID: 1712
		Root = 8,
		// Token: 0x040006B1 RID: 1713
		Initialized = 16,
		// Token: 0x040006B2 RID: 1714
		Strong = 32
	}

	// Token: 0x02000141 RID: 321
	private struct Link
	{
		// Token: 0x040006B3 RID: 1715
		public global::Controllable it;

		// Token: 0x040006B4 RID: 1716
		public bool vl;
	}

	// Token: 0x02000142 RID: 322
	private static class LocalOnly
	{
		// Token: 0x040006B5 RID: 1717
		public static readonly List<global::Controllable> rootLocalPlayerControllables = new List<global::Controllable>();
	}

	// Token: 0x02000143 RID: 323
	// (Invoke) Token: 0x06000942 RID: 2370
	public delegate void DestroyInContextQuery(global::Controllable controllable);
}
