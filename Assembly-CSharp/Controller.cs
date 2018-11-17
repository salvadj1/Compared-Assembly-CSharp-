using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using uLink;
using UnityEngine;

// Token: 0x02000130 RID: 304
public abstract class Controller : IDLocalCharacterAddon
{
	// Token: 0x060008A9 RID: 2217 RVA: 0x00025BAC File Offset: 0x00023DAC
	protected Controller(Controller.ControllerFlags controllerFlags) : this(controllerFlags, (IDLocalCharacterAddon.AddonFlags)0)
	{
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x00025BB8 File Offset: 0x00023DB8
	protected Controller(Controller.ControllerFlags controllerFlags, IDLocalCharacterAddon.AddonFlags addonFlags) : base(addonFlags)
	{
		this.controllerFlags = controllerFlags;
	}

	// Token: 0x17000213 RID: 531
	// (get) Token: 0x060008AB RID: 2219 RVA: 0x00025BC8 File Offset: 0x00023DC8
	public new bool controlled
	{
		get
		{
			return this._controllable.controlled;
		}
	}

	// Token: 0x17000214 RID: 532
	// (get) Token: 0x060008AC RID: 2220 RVA: 0x00025BD8 File Offset: 0x00023DD8
	public new bool playerControlled
	{
		get
		{
			return this._controllable.playerControlled;
		}
	}

	// Token: 0x17000215 RID: 533
	// (get) Token: 0x060008AD RID: 2221 RVA: 0x00025BE8 File Offset: 0x00023DE8
	public new bool aiControlled
	{
		get
		{
			return this._controllable.aiControlled;
		}
	}

	// Token: 0x17000216 RID: 534
	// (get) Token: 0x060008AE RID: 2222 RVA: 0x00025BF8 File Offset: 0x00023DF8
	public new bool localPlayerControlled
	{
		get
		{
			return this._controllable.localPlayerControlled;
		}
	}

	// Token: 0x17000217 RID: 535
	// (get) Token: 0x060008AF RID: 2223 RVA: 0x00025C08 File Offset: 0x00023E08
	public new bool remotePlayerControlled
	{
		get
		{
			return this._controllable.remotePlayerControlled;
		}
	}

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00025C18 File Offset: 0x00023E18
	public new bool localAIControlled
	{
		get
		{
			return this._controllable.localAIControlled;
		}
	}

	// Token: 0x17000219 RID: 537
	// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00025C28 File Offset: 0x00023E28
	public new bool remoteAIControlled
	{
		get
		{
			return this._controllable.remoteAIControlled;
		}
	}

	// Token: 0x1700021A RID: 538
	// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00025C38 File Offset: 0x00023E38
	public new bool localControlled
	{
		get
		{
			return this._controllable.localControlled;
		}
	}

	// Token: 0x1700021B RID: 539
	// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00025C48 File Offset: 0x00023E48
	public new bool remoteControlled
	{
		get
		{
			return this._controllable.remoteControlled;
		}
	}

	// Token: 0x1700021C RID: 540
	// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00025C58 File Offset: 0x00023E58
	public new PlayerClient playerClient
	{
		get
		{
			return this._controllable.playerClient;
		}
	}

	// Token: 0x1700021D RID: 541
	// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00025C68 File Offset: 0x00023E68
	public new Controller controller
	{
		get
		{
			return this;
		}
	}

	// Token: 0x1700021E RID: 542
	// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00025C6C File Offset: 0x00023E6C
	public new Controller controlledController
	{
		get
		{
			return (!this._controllable.controlled) ? null : this;
		}
	}

	// Token: 0x1700021F RID: 543
	// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00025C88 File Offset: 0x00023E88
	public new Controller localPlayerControlledController
	{
		get
		{
			return this._controllable.localPlayerControlledController;
		}
	}

	// Token: 0x17000220 RID: 544
	// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00025C98 File Offset: 0x00023E98
	public new Controller remotePlayerControlledController
	{
		get
		{
			return this._controllable.remotePlayerControlledController;
		}
	}

	// Token: 0x17000221 RID: 545
	// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00025CA8 File Offset: 0x00023EA8
	public new Controller localAIControlledController
	{
		get
		{
			return this._controllable.localAIControlledController;
		}
	}

	// Token: 0x17000222 RID: 546
	// (get) Token: 0x060008BA RID: 2234 RVA: 0x00025CB8 File Offset: 0x00023EB8
	public new Controller remoteAIControlledController
	{
		get
		{
			return this._controllable.remoteAIControlledController;
		}
	}

	// Token: 0x17000223 RID: 547
	// (get) Token: 0x060008BB RID: 2235 RVA: 0x00025CC8 File Offset: 0x00023EC8
	public new Controllable controllable
	{
		get
		{
			return this._controllable;
		}
	}

	// Token: 0x17000224 RID: 548
	// (get) Token: 0x060008BC RID: 2236 RVA: 0x00025CD0 File Offset: 0x00023ED0
	public new Controllable controlledControllable
	{
		get
		{
			return (!this._controllable.controlled) ? null : this._controllable;
		}
	}

	// Token: 0x17000225 RID: 549
	// (get) Token: 0x060008BD RID: 2237 RVA: 0x00025CF0 File Offset: 0x00023EF0
	public new Controllable localPlayerControlledControllable
	{
		get
		{
			return this._controllable.localPlayerControlledControllable;
		}
	}

	// Token: 0x17000226 RID: 550
	// (get) Token: 0x060008BE RID: 2238 RVA: 0x00025D00 File Offset: 0x00023F00
	public new Controllable remotePlayerControlledControllable
	{
		get
		{
			return this._controllable.remotePlayerControlledControllable;
		}
	}

	// Token: 0x17000227 RID: 551
	// (get) Token: 0x060008BF RID: 2239 RVA: 0x00025D10 File Offset: 0x00023F10
	public new Controllable localAIControlledControllable
	{
		get
		{
			return this._controllable.localAIControlledControllable;
		}
	}

	// Token: 0x17000228 RID: 552
	// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00025D20 File Offset: 0x00023F20
	public new Controllable remoteAIControlledControllable
	{
		get
		{
			return this._controllable.remoteAIControlledControllable;
		}
	}

	// Token: 0x17000229 RID: 553
	// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00025D30 File Offset: 0x00023F30
	public new bool controlOverridden
	{
		get
		{
			return this._controllable.controlOverridden;
		}
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x00025D40 File Offset: 0x00023F40
	public new bool ControlOverriddenBy(Controllable controllable)
	{
		return this._controllable.ControlOverriddenBy(controllable);
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x00025D50 File Offset: 0x00023F50
	public new bool ControlOverriddenBy(Controller controller)
	{
		return this._controllable.ControlOverriddenBy(controller);
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x00025D60 File Offset: 0x00023F60
	public new bool ControlOverriddenBy(Character character)
	{
		return this._controllable.ControlOverriddenBy(character);
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x00025D70 File Offset: 0x00023F70
	public new bool ControlOverriddenBy(IDMain main)
	{
		return this._controllable.ControlOverriddenBy(main);
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x00025D80 File Offset: 0x00023F80
	public new bool ControlOverriddenBy(IDBase idBase)
	{
		return this._controllable.ControlOverriddenBy(idBase);
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x00025D90 File Offset: 0x00023F90
	public new bool ControlOverriddenBy(IDLocalCharacter idLocal)
	{
		return this._controllable.ControlOverriddenBy(idLocal);
	}

	// Token: 0x1700022A RID: 554
	// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00025DA0 File Offset: 0x00023FA0
	public new bool overridingControl
	{
		get
		{
			return this._controllable.overridingControl;
		}
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x00025DB0 File Offset: 0x00023FB0
	public new bool OverridingControlOf(Controllable controllable)
	{
		return this._controllable.OverridingControlOf(controllable);
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x00025DC0 File Offset: 0x00023FC0
	public new bool OverridingControlOf(Controller controller)
	{
		return this._controllable.OverridingControlOf(controller);
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x00025DD0 File Offset: 0x00023FD0
	public new bool OverridingControlOf(Character character)
	{
		return this._controllable.OverridingControlOf(character);
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x00025DE0 File Offset: 0x00023FE0
	public new bool OverridingControlOf(IDMain main)
	{
		return this._controllable.OverridingControlOf(main);
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x00025DF0 File Offset: 0x00023FF0
	public new bool OverridingControlOf(IDBase idBase)
	{
		return this._controllable.OverridingControlOf(idBase);
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x00025E00 File Offset: 0x00024000
	public new bool OverridingControlOf(IDLocalCharacter idLocal)
	{
		return this._controllable.OverridingControlOf(idLocal);
	}

	// Token: 0x1700022B RID: 555
	// (get) Token: 0x060008CF RID: 2255 RVA: 0x00025E10 File Offset: 0x00024010
	public new bool assignedControl
	{
		get
		{
			return this._controllable.assignedControl;
		}
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x00025E20 File Offset: 0x00024020
	public new bool AssignedControlOf(Controllable controllable)
	{
		return this._controllable.AssignedControlOf(controllable);
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x00025E30 File Offset: 0x00024030
	public new bool AssignedControlOf(Controller controller)
	{
		return this._controllable.AssignedControlOf(controller);
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x00025E40 File Offset: 0x00024040
	public new bool AssignedControlOf(IDMain character)
	{
		return this._controllable.AssignedControlOf(character);
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x00025E50 File Offset: 0x00024050
	public new bool AssignedControlOf(IDBase idBase)
	{
		return this._controllable.AssignedControlOf(idBase);
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x00025E60 File Offset: 0x00024060
	public new RelativeControl RelativeControlTo(Controllable controllable)
	{
		return this._controllable.RelativeControlTo(controllable);
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x00025E70 File Offset: 0x00024070
	public new RelativeControl RelativeControlTo(Controller controller)
	{
		return this._controllable.RelativeControlTo(controller);
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x00025E80 File Offset: 0x00024080
	public new RelativeControl RelativeControlTo(Character character)
	{
		return this._controllable.RelativeControlTo(character);
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x00025E90 File Offset: 0x00024090
	public new RelativeControl RelativeControlTo(IDMain main)
	{
		return this._controllable.RelativeControlTo(main);
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x00025EA0 File Offset: 0x000240A0
	public new RelativeControl RelativeControlTo(IDLocalCharacter idLocal)
	{
		return this._controllable.RelativeControlTo(idLocal);
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x00025EB0 File Offset: 0x000240B0
	public new RelativeControl RelativeControlTo(IDBase idBase)
	{
		return this._controllable.RelativeControlTo(idBase);
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x00025EC0 File Offset: 0x000240C0
	public new RelativeControl RelativeControlFrom(Controllable controllable)
	{
		return this._controllable.RelativeControlFrom(controllable);
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x00025ED0 File Offset: 0x000240D0
	public new RelativeControl RelativeControlFrom(Controller controller)
	{
		return this._controllable.RelativeControlFrom(controller);
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x00025EE0 File Offset: 0x000240E0
	public new RelativeControl RelativeControlFrom(Character character)
	{
		return this._controllable.RelativeControlFrom(character);
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x00025EF0 File Offset: 0x000240F0
	public new RelativeControl RelativeControlFrom(IDMain main)
	{
		return this._controllable.RelativeControlFrom(main);
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00025F00 File Offset: 0x00024100
	public new RelativeControl RelativeControlFrom(IDLocalCharacter idLocal)
	{
		return this._controllable.RelativeControlFrom(idLocal);
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x00025F10 File Offset: 0x00024110
	public new RelativeControl RelativeControlFrom(IDBase idBase)
	{
		return this._controllable.RelativeControlFrom(idBase);
	}

	// Token: 0x1700022C RID: 556
	// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00025F20 File Offset: 0x00024120
	public new Controllable masterControllable
	{
		get
		{
			return this._controllable.masterControllable;
		}
	}

	// Token: 0x1700022D RID: 557
	// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00025F30 File Offset: 0x00024130
	public new Controllable rootControllable
	{
		get
		{
			return this._controllable.rootControllable;
		}
	}

	// Token: 0x1700022E RID: 558
	// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00025F40 File Offset: 0x00024140
	public new Controllable nextControllable
	{
		get
		{
			return this._controllable.nextControllable;
		}
	}

	// Token: 0x1700022F RID: 559
	// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00025F50 File Offset: 0x00024150
	public new Controllable previousControllable
	{
		get
		{
			return this._controllable.previousControllable;
		}
	}

	// Token: 0x17000230 RID: 560
	// (get) Token: 0x060008E4 RID: 2276 RVA: 0x00025F60 File Offset: 0x00024160
	public new Character previousCharacter
	{
		get
		{
			return this._controllable.previousCharacter;
		}
	}

	// Token: 0x17000231 RID: 561
	// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00025F70 File Offset: 0x00024170
	public new Character rootCharacter
	{
		get
		{
			return this._controllable.rootCharacter;
		}
	}

	// Token: 0x17000232 RID: 562
	// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00025F80 File Offset: 0x00024180
	public new Character nextCharacter
	{
		get
		{
			return this._controllable.nextCharacter;
		}
	}

	// Token: 0x17000233 RID: 563
	// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00025F90 File Offset: 0x00024190
	public new Character masterCharacter
	{
		get
		{
			return this._controllable.masterCharacter;
		}
	}

	// Token: 0x17000234 RID: 564
	// (get) Token: 0x060008E8 RID: 2280 RVA: 0x00025FA0 File Offset: 0x000241A0
	public new Controller masterController
	{
		get
		{
			return this._controllable.masterController;
		}
	}

	// Token: 0x17000235 RID: 565
	// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00025FB0 File Offset: 0x000241B0
	public new Controller rootController
	{
		get
		{
			return this._controllable.rootController;
		}
	}

	// Token: 0x17000236 RID: 566
	// (get) Token: 0x060008EA RID: 2282 RVA: 0x00025FC0 File Offset: 0x000241C0
	public new Controller nextController
	{
		get
		{
			return this._controllable.nextController;
		}
	}

	// Token: 0x17000237 RID: 567
	// (get) Token: 0x060008EB RID: 2283 RVA: 0x00025FD0 File Offset: 0x000241D0
	public new Controller previousController
	{
		get
		{
			return this._controllable.previousController;
		}
	}

	// Token: 0x17000238 RID: 568
	// (get) Token: 0x060008EC RID: 2284 RVA: 0x00025FE0 File Offset: 0x000241E0
	public new int controlDepth
	{
		get
		{
			return this._controllable.controlDepth;
		}
	}

	// Token: 0x17000239 RID: 569
	// (get) Token: 0x060008ED RID: 2285 RVA: 0x00025FF0 File Offset: 0x000241F0
	public new int controlCount
	{
		get
		{
			return this._controllable.controlCount;
		}
	}

	// Token: 0x1700023A RID: 570
	// (get) Token: 0x060008EE RID: 2286 RVA: 0x00026000 File Offset: 0x00024200
	public new string controllerClassName
	{
		get
		{
			return this._controllable.controllerClassName;
		}
	}

	// Token: 0x1700023B RID: 571
	// (get) Token: 0x060008EF RID: 2287 RVA: 0x00026010 File Offset: 0x00024210
	public new string npcName
	{
		get
		{
			return this._controllable.npcName;
		}
	}

	// Token: 0x1700023C RID: 572
	// (get) Token: 0x060008F0 RID: 2288 RVA: 0x00026020 File Offset: 0x00024220
	// (set) Token: 0x060008F1 RID: 2289 RVA: 0x00026028 File Offset: 0x00024228
	public new RPOSLimitFlags rposLimitFlags
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

	// Token: 0x1700023D RID: 573
	// (get) Token: 0x060008F2 RID: 2290 RVA: 0x00026034 File Offset: 0x00024234
	// (set) Token: 0x060008F3 RID: 2291 RVA: 0x0002603C File Offset: 0x0002423C
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

	// Token: 0x1700023E RID: 574
	// (get) Token: 0x060008F4 RID: 2292 RVA: 0x00026048 File Offset: 0x00024248
	// (set) Token: 0x060008F5 RID: 2293 RVA: 0x00026050 File Offset: 0x00024250
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

	// Token: 0x060008F6 RID: 2294 RVA: 0x0002605C File Offset: 0x0002425C
	protected virtual void OnControllerSetup(NetworkView networkView, ref NetworkMessageInfo info)
	{
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x00026060 File Offset: 0x00024260
	internal void ControllerSetup(Controllable controllable, NetworkView view, ref NetworkMessageInfo info)
	{
		if (this.wasSetup)
		{
			throw new InvalidOperationException("Already was setup");
		}
		this.wasSetup = true;
		Controller.ControllerFlags controllerFlags = this.controllerFlags & Controller.ControllerFlags.DontMessWithEnabled;
		bool flag;
		if (controllerFlags != Controller.ControllerFlags.AlwaysSavedAsDisabled)
		{
			if (controllerFlags != Controller.ControllerFlags.AlwaysSavedAsEnabled)
			{
				flag = (controllerFlags == Controller.ControllerFlags.DontMessWithEnabled);
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
				if ((this.controllerFlags & Controller.ControllerFlags.IncompatibleAsLocalPlayer) == Controller.ControllerFlags.IncompatibleAsLocalPlayer)
				{
					throw new NotSupportedException();
				}
			}
			else if ((this.controllerFlags & Controller.ControllerFlags.IncompatibleAsRemotePlayer) == Controller.ControllerFlags.IncompatibleAsRemotePlayer)
			{
				throw new NotSupportedException();
			}
		}
		else if (this.localAIControlled)
		{
			if ((this.controllerFlags & Controller.ControllerFlags.IncompatibleAsLocalAI) == Controller.ControllerFlags.IncompatibleAsLocalAI)
			{
				throw new NotSupportedException();
			}
		}
		else if ((this.controllerFlags & Controller.ControllerFlags.IncompatibleAsRemoteAI) == Controller.ControllerFlags.IncompatibleAsRemoteAI)
		{
			throw new NotSupportedException();
		}
		this.OnControllerSetup(view, ref info);
		if (!flag)
		{
			Controller.ControllerFlags controllerFlags2;
			Controller.ControllerFlags controllerFlags3;
			if (this.playerControlled)
			{
				if (this.localPlayerControlled)
				{
					controllerFlags2 = Controller.ControllerFlags.EnableWhenLocalPlayer;
					controllerFlags3 = Controller.ControllerFlags.DisableWhenLocalPlayer;
				}
				else
				{
					controllerFlags2 = Controller.ControllerFlags.EnableWhenRemotePlayer;
					controllerFlags3 = Controller.ControllerFlags.DisableWhenRemotePlayer;
				}
			}
			else if (this.localAIControlled)
			{
				controllerFlags2 = Controller.ControllerFlags.EnableWhenLocalAI;
				controllerFlags3 = Controller.ControllerFlags.DisableWhenLocalAI;
			}
			else
			{
				controllerFlags2 = Controller.ControllerFlags.EnableWhenRemoteAI;
				controllerFlags3 = Controller.ControllerFlags.DisableWhenRemoteAI;
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

	// Token: 0x060008F8 RID: 2296 RVA: 0x00026260 File Offset: 0x00024460
	protected virtual void OnLocalPlayerInputFrame()
	{
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x00026264 File Offset: 0x00024464
	internal void ProcessLocalPlayerInput()
	{
		this.OnLocalPlayerInputFrame();
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x0002626C File Offset: 0x0002446C
	protected virtual void OnLocalPlayerPreRender()
	{
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x00026270 File Offset: 0x00024470
	internal void ProcessLocalPlayerPreRender()
	{
		this.OnLocalPlayerPreRender();
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x00026278 File Offset: 0x00024478
	protected virtual void OnControlEnter()
	{
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x0002627C File Offset: 0x0002447C
	protected virtual void OnControlEngauge()
	{
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x00026280 File Offset: 0x00024480
	protected virtual void OnControlCease()
	{
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x00026284 File Offset: 0x00024484
	protected virtual void OnControlExit()
	{
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x00026288 File Offset: 0x00024488
	[Obsolete("Used only by Controllable")]
	internal void ControlEnter(int cmd)
	{
		Controller.Commandment commandment = this.commandment;
		this.commandment = new Controller.Commandment((cmd & 32767) | 32768);
		try
		{
			this.OnControlEnter();
		}
		finally
		{
			this.commandment = commandment;
		}
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x000262E4 File Offset: 0x000244E4
	[Obsolete("Used only by Controllable")]
	internal void ControlExit(int cmd)
	{
		Controller.Commandment commandment = this.commandment;
		this.commandment = new Controller.Commandment((cmd & 32767) | 131072);
		try
		{
			this.OnControlExit();
		}
		finally
		{
			this.commandment = commandment;
		}
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x00026340 File Offset: 0x00024540
	[Obsolete("Used only by Controllable")]
	internal void ControlEngauge(int cmd)
	{
		Controller.Commandment commandment = this.commandment;
		this.commandment = new Controller.Commandment((cmd & 32767) | 65536);
		try
		{
			this.OnControlEngauge();
		}
		finally
		{
			this.commandment = commandment;
		}
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x0002639C File Offset: 0x0002459C
	[Obsolete("Used only by Controllable")]
	internal void ControlCease(int cmd)
	{
		Controller.Commandment commandment = this.commandment;
		this.commandment = new Controller.Commandment((cmd & 32767) | 98304);
		try
		{
			this.OnControlCease();
		}
		finally
		{
			this.commandment = commandment;
		}
	}

	// Token: 0x1700023F RID: 575
	// (get) Token: 0x06000904 RID: 2308 RVA: 0x000263F8 File Offset: 0x000245F8
	public static IEnumerable<Controller> PlayerRootControllers
	{
		get
		{
			foreach (PlayerClient pc in PlayerClient.All)
			{
				Controllable controllable = pc.rootControllable;
				if (controllable)
				{
					Controller controller = controllable.controller;
					if (controller)
					{
						yield return controllable.controller;
					}
				}
			}
			yield break;
		}
	}

	// Token: 0x17000240 RID: 576
	// (get) Token: 0x06000905 RID: 2309 RVA: 0x00026414 File Offset: 0x00024614
	public static IEnumerable<Controller> PlayerCurrentControllers
	{
		get
		{
			foreach (PlayerClient pc in PlayerClient.All)
			{
				Controllable controllable = pc.controllable;
				if (controllable)
				{
					Controller controller = controllable.controller;
					if (controller)
					{
						yield return controllable.controller;
					}
				}
			}
			yield break;
		}
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x00026430 File Offset: 0x00024630
	public static IEnumerable<Controller> RootControllers(IEnumerable<PlayerClient> playerClients)
	{
		foreach (PlayerClient pc in playerClients)
		{
			Controllable controllable = pc.rootControllable;
			if (controllable)
			{
				Controller controller = controllable.controller;
				if (controller)
				{
					yield return controller;
				}
			}
		}
		yield break;
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x0002645C File Offset: 0x0002465C
	public static IEnumerable<Controller> CurrentControllers(IEnumerable<PlayerClient> playerClients)
	{
		foreach (PlayerClient pc in playerClients)
		{
			Controllable controllable = pc.controllable;
			if (controllable)
			{
				Controller controller = controllable.controller;
				if (controller)
				{
					yield return controller;
				}
			}
		}
		yield break;
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x00026488 File Offset: 0x00024688
	public static IEnumerable<TController> RootControllers<TController>(IEnumerable<PlayerClient> playerClients) where TController : Controller
	{
		foreach (PlayerClient pc in playerClients)
		{
			Controllable controllable = pc.rootControllable;
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

	// Token: 0x06000909 RID: 2313 RVA: 0x000264B4 File Offset: 0x000246B4
	public static IEnumerable<TController> CurrentControllers<TController>(IEnumerable<PlayerClient> playerClients) where TController : Controller
	{
		foreach (PlayerClient pc in playerClients)
		{
			Controllable controllable = pc.controllable;
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

	// Token: 0x040005FE RID: 1534
	[NonSerialized]
	private Controllable _controllable;

	// Token: 0x040005FF RID: 1535
	[NonSerialized]
	private readonly Controller.ControllerFlags controllerFlags;

	// Token: 0x04000600 RID: 1536
	[NonSerialized]
	private RPOSLimitFlags _rposLimitFlags;

	// Token: 0x04000601 RID: 1537
	[NonSerialized]
	private bool wasSetup;

	// Token: 0x04000602 RID: 1538
	[NonSerialized]
	private bool _forwardsPlayerClientInput;

	// Token: 0x04000603 RID: 1539
	[NonSerialized]
	private bool _doesNotSave;

	// Token: 0x04000604 RID: 1540
	[NonSerialized]
	protected Controller.Commandment commandment;

	// Token: 0x02000131 RID: 305
	protected enum ControllerFlags
	{
		// Token: 0x04000606 RID: 1542
		EnableWhenLocalPlayer = 1,
		// Token: 0x04000607 RID: 1543
		EnableWhenLocalAI,
		// Token: 0x04000608 RID: 1544
		EnableWhenRemotePlayer = 4,
		// Token: 0x04000609 RID: 1545
		EnableWhenRemoteAI = 8,
		// Token: 0x0400060A RID: 1546
		DisableWhenLocalPlayer = 16,
		// Token: 0x0400060B RID: 1547
		DisableWhenLocalAI = 32,
		// Token: 0x0400060C RID: 1548
		DisableWhenRemotePlayer = 64,
		// Token: 0x0400060D RID: 1549
		DisableWhenRemoteAI = 128,
		// Token: 0x0400060E RID: 1550
		ToggleEnableWhenLocalPlayer = 17,
		// Token: 0x0400060F RID: 1551
		ToggleEnableLocalAI = 34,
		// Token: 0x04000610 RID: 1552
		ToggleEnableRemotePlayer = 68,
		// Token: 0x04000611 RID: 1553
		ToggleEnableRemoteAI = 136,
		// Token: 0x04000612 RID: 1554
		AlwaysSavedAsDisabled = 256,
		// Token: 0x04000613 RID: 1555
		AlwaysSavedAsEnabled = 512,
		// Token: 0x04000614 RID: 1556
		DontMessWithEnabled = 768,
		// Token: 0x04000615 RID: 1557
		IncompatibleAsRemoteAI = 1024,
		// Token: 0x04000616 RID: 1558
		IncompatibleAsRemotePlayer = 2048,
		// Token: 0x04000617 RID: 1559
		IncompatibleAsLocalPlayer = 4096,
		// Token: 0x04000618 RID: 1560
		IncompatibleAsLocalAI = 8192
	}

	// Token: 0x02000132 RID: 306
	[StructLayout(LayoutKind.Sequential, Size = 4)]
	protected internal struct Commandment
	{
		// Token: 0x0600090A RID: 2314 RVA: 0x000264E0 File Offset: 0x000246E0
		internal Commandment(int f)
		{
			this.f = (f & 262143);
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x000264F0 File Offset: 0x000246F0
		public bool thisDestroying
		{
			get
			{
				return (this.f & 1) == 1;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00026500 File Offset: 0x00024700
		public bool baseDestroying
		{
			get
			{
				return (this.f & 2) == 2;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00026510 File Offset: 0x00024710
		public bool rootDestroying
		{
			get
			{
				return (this.f & 4) == 4;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x00026520 File Offset: 0x00024720
		public bool baseExit
		{
			get
			{
				return (this.f & 32) == 32;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00026530 File Offset: 0x00024730
		public bool thisExit
		{
			get
			{
				return (this.f & 16) == 16;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x00026540 File Offset: 0x00024740
		public bool rootExit
		{
			get
			{
				return (this.f & 64) == 64;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00026550 File Offset: 0x00024750
		public bool networkValid
		{
			get
			{
				return (this.f & 8) == 0;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x00026560 File Offset: 0x00024760
		public bool networkInvalid
		{
			get
			{
				return (this.f & 8) == 8;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00026570 File Offset: 0x00024770
		public bool overrideThis
		{
			get
			{
				return (this.f & 128) == 128;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00026588 File Offset: 0x00024788
		public bool overrideBase
		{
			get
			{
				return (this.f & 256) == 256;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x000265A0 File Offset: 0x000247A0
		public bool overrideRoot
		{
			get
			{
				return (this.f & 512) == 512;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x000265B8 File Offset: 0x000247B8
		public bool ownerServer
		{
			get
			{
				return (this.f & 8192) == 0;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x000265CC File Offset: 0x000247CC
		public bool ownerClient
		{
			get
			{
				return (this.f & 8192) == 8192;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x000265E4 File Offset: 0x000247E4
		public bool runningLocally
		{
			get
			{
				return (this.f & 16384) == 16384;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x000265FC File Offset: 0x000247FC
		public bool runningRemotely
		{
			get
			{
				return (this.f & 16384) == 0;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00026610 File Offset: 0x00024810
		public bool callFirst
		{
			get
			{
				return (this.f & 1024) == 0;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00026624 File Offset: 0x00024824
		public bool callAgain
		{
			get
			{
				return (this.f & 1024) == 1024;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0002663C File Offset: 0x0002483C
		public bool bindWeak
		{
			get
			{
				return (this.f & 4096) == 0;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00026650 File Offset: 0x00024850
		public bool bindStrong
		{
			get
			{
				return (this.f & 4096) == 4096;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00026668 File Offset: 0x00024868
		public bool kindRoot
		{
			get
			{
				return (this.f & 2048) == 2048;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x00026680 File Offset: 0x00024880
		public bool kindVessel
		{
			get
			{
				return (this.f & 2048) == 0;
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00026694 File Offset: 0x00024894
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

		// Token: 0x04000619 RID: 1561
		private const int B = 1;

		// Token: 0x0400061A RID: 1562
		internal const int THIS_TO_BASE = 1;

		// Token: 0x0400061B RID: 1563
		internal const int THIS_TO_ROOT = 2;

		// Token: 0x0400061C RID: 1564
		internal const int ALL = 32767;

		// Token: 0x0400061D RID: 1565
		internal const int ALL_THIS = 145;

		// Token: 0x0400061E RID: 1566
		internal const int ALL_BASE = 290;

		// Token: 0x0400061F RID: 1567
		internal const int ALL_ROOT = 580;

		// Token: 0x04000620 RID: 1568
		private readonly int f;

		// Token: 0x02000133 RID: 307
		internal static class DESTROY
		{
			// Token: 0x04000621 RID: 1569
			public const int THIS = 1;

			// Token: 0x04000622 RID: 1570
			public const int BASE = 2;

			// Token: 0x04000623 RID: 1571
			public const int ROOT = 4;

			// Token: 0x04000624 RID: 1572
			public const int NONE = 0;

			// Token: 0x04000625 RID: 1573
			public const int ALL = 7;
		}

		// Token: 0x02000134 RID: 308
		internal static class NETWORK
		{
			// Token: 0x04000626 RID: 1574
			public const int VALID = 0;

			// Token: 0x04000627 RID: 1575
			public const int INVALID = 8;

			// Token: 0x04000628 RID: 1576
			public const int ALL = 8;
		}

		// Token: 0x02000135 RID: 309
		internal static class EXIT
		{
			// Token: 0x04000629 RID: 1577
			public const int THIS = 16;

			// Token: 0x0400062A RID: 1578
			public const int BASE = 32;

			// Token: 0x0400062B RID: 1579
			public const int ROOT = 64;

			// Token: 0x0400062C RID: 1580
			public const int NONE = 0;

			// Token: 0x0400062D RID: 1581
			public const int ALL = 112;
		}

		// Token: 0x02000136 RID: 310
		internal static class OVERRIDE
		{
			// Token: 0x0400062E RID: 1582
			public const int THIS = 128;

			// Token: 0x0400062F RID: 1583
			public const int BASE = 256;

			// Token: 0x04000630 RID: 1584
			public const int ROOT = 512;

			// Token: 0x04000631 RID: 1585
			public const int NONE = 0;

			// Token: 0x04000632 RID: 1586
			public const int ALL = 896;
		}

		// Token: 0x02000137 RID: 311
		internal static class ONCE
		{
			// Token: 0x04000633 RID: 1587
			public const int TRUE = 1024;

			// Token: 0x04000634 RID: 1588
			public const int FALSE = 0;

			// Token: 0x04000635 RID: 1589
			public const int ALL = 1024;
		}

		// Token: 0x02000138 RID: 312
		internal static class KIND
		{
			// Token: 0x04000636 RID: 1590
			public const int ROOT = 2048;

			// Token: 0x04000637 RID: 1591
			public const int VESSEL = 0;

			// Token: 0x04000638 RID: 1592
			public const int ALL = 2048;
		}

		// Token: 0x02000139 RID: 313
		internal static class BINDING
		{
			// Token: 0x04000639 RID: 1593
			public const int STRONG = 4096;

			// Token: 0x0400063A RID: 1594
			public const int WEAK = 0;

			// Token: 0x0400063B RID: 1595
			public const int ALL = 4096;
		}

		// Token: 0x0200013A RID: 314
		internal static class OWNER
		{
			// Token: 0x0400063C RID: 1596
			public const int CLIENT = 8192;

			// Token: 0x0400063D RID: 1597
			public const int SERVER = 0;

			// Token: 0x0400063E RID: 1598
			public const int ALL = 8192;
		}

		// Token: 0x0200013B RID: 315
		internal static class PLACE
		{
			// Token: 0x0400063F RID: 1599
			public const int HERE = 16384;

			// Token: 0x04000640 RID: 1600
			public const int ELSEWHERE = 0;

			// Token: 0x04000641 RID: 1601
			public const int ALL = 16384;
		}

		// Token: 0x0200013C RID: 316
		internal static class EVENT
		{
			// Token: 0x04000642 RID: 1602
			public const int NONE = 0;

			// Token: 0x04000643 RID: 1603
			public const int ENTER = 32768;

			// Token: 0x04000644 RID: 1604
			public const int ENGAUGE = 65536;

			// Token: 0x04000645 RID: 1605
			public const int CEASE = 98304;

			// Token: 0x04000646 RID: 1606
			public const int EXIT = 131072;

			// Token: 0x04000647 RID: 1607
			public const int ALL = 229376;
		}
	}
}
