using System;
using System.Collections.Generic;
using Facepunch;
using uLink;

// Token: 0x02000152 RID: 338
public class ControllablePrefab : global::CharacterPrefab
{
	// Token: 0x06000989 RID: 2441 RVA: 0x000287BC File Offset: 0x000269BC
	public ControllablePrefab() : this(typeof(global::Character), false, global::ControllablePrefab.minimalRequiredIDLocals, false)
	{
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x000287D8 File Offset: 0x000269D8
	protected ControllablePrefab(Type characterType, params Type[] idlocalRequired) : this(characterType, true, idlocalRequired, idlocalRequired != null && idlocalRequired.Length > 0)
	{
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x000287F4 File Offset: 0x000269F4
	protected ControllablePrefab(Type characterType) : this(characterType, true, null, false)
	{
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x00028800 File Offset: 0x00026A00
	private ControllablePrefab(Type characterType, bool typeCheck, Type[] requiredIDLocalTypes, bool mergeTypes) : base(characterType, (!mergeTypes) ? global::ControllablePrefab.minimalRequiredIDLocals : global::CharacterPrefab.TypeArrayAppend(global::ControllablePrefab.minimalRequiredIDLocals, requiredIDLocalTypes))
	{
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x0002886C File Offset: 0x00026A6C
	protected override void StandardInitialization(bool didAppend, IDRemote appended, global::NetInstance instance, Facepunch.NetworkView view, ref uLink.NetworkMessageInfo info)
	{
		global::Character character = (global::Character)instance.idMain;
		global::Controllable controllable = character.controllable;
		controllable.PrepareInstantiate(view, ref info);
		base.StandardInitialization(false, appended, instance, view, ref info);
		if (didAppend)
		{
			global::NetMainPrefab.IssueLocallyAppended(appended, instance.idMain);
		}
		controllable.OnInstantiated();
	}

	// Token: 0x1700023F RID: 575
	// (get) Token: 0x0600098F RID: 2447 RVA: 0x000288BC File Offset: 0x00026ABC
	private bool playerRootComapatable
	{
		get
		{
			global::Controllable controllable = ((global::Character)base.serverPrefab).controllable;
			if (!controllable)
			{
				return false;
			}
			if (!controllable.classFlagsRootControllable)
			{
				return false;
			}
			if (!controllable.classFlagsPlayerSupport)
			{
				return false;
			}
			controllable = ((global::Character)base.proxyPrefab).controllable;
			return controllable && controllable.classFlagsRootControllable && controllable.classFlagsPlayerSupport;
		}
	}

	// Token: 0x17000240 RID: 576
	// (get) Token: 0x06000990 RID: 2448 RVA: 0x0002893C File Offset: 0x00026B3C
	private bool aiRootComapatable
	{
		get
		{
			global::Controllable controllable = ((global::Character)base.serverPrefab).controllable;
			if (!controllable)
			{
				return false;
			}
			if (!controllable.classFlagsRootControllable)
			{
				return false;
			}
			if (!controllable.classFlagsAISupport)
			{
				return false;
			}
			controllable = ((global::Character)base.proxyPrefab).controllable;
			return controllable && controllable.classFlagsRootControllable && controllable.classFlagsAISupport;
		}
	}

	// Token: 0x17000241 RID: 577
	// (get) Token: 0x06000991 RID: 2449 RVA: 0x000289BC File Offset: 0x00026BBC
	private global::ControllerClass.Merge mergedClasses
	{
		get
		{
			global::ControllerClass.Merge result = default(global::ControllerClass.Merge);
			global::Controllable.MergeClasses(base.serverPrefab, ref result);
			global::Controllable.MergeClasses(base.proxyPrefab, ref result);
			global::Controllable.MergeClasses(base.localPrefab, ref result);
			return result;
		}
	}

	// Token: 0x17000242 RID: 578
	// (get) Token: 0x06000992 RID: 2450 RVA: 0x00028A00 File Offset: 0x00026C00
	private byte vesselCompatibility
	{
		get
		{
			global::ControllerClass.Merge mergedClasses = this.mergedClasses;
			if (!mergedClasses.any)
			{
				return 0;
			}
			if (!mergedClasses.vessel)
			{
				return 64;
			}
			byte b;
			if (mergedClasses.vesselFree)
			{
				b = 7;
			}
			else if (mergedClasses.vesselDependant)
			{
				b = 5;
			}
			else
			{
				if (!mergedClasses.vesselStandalone)
				{
					throw new NotImplementedException();
				}
				b = 3;
			}
			if (mergedClasses[true])
			{
				b |= 8;
			}
			if (mergedClasses[false])
			{
				b |= 16;
			}
			if (mergedClasses.staticGroup)
			{
				b |= 32;
			}
			return b;
		}
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x00028AA8 File Offset: 0x00026CA8
	public static void EnsurePrefabIsPlayerRootCompatible(string name)
	{
		global::NetMainPrefab.EnsurePrefabName(name);
		byte b;
		if (!global::ControllablePrefab.playerRootCompatibilityCache.TryGetValue(name, out b))
		{
			global::ControllablePrefab controllablePrefab = global::NetMainPrefab.Lookup<global::ControllablePrefab>(name);
			if (!controllablePrefab)
			{
				b = 0;
			}
			else if (!controllablePrefab.playerRootComapatable)
			{
				b = 2;
			}
			else
			{
				b = 1;
			}
			global::ControllablePrefab.playerRootCompatibilityCache[name] = b;
		}
		if (b == 0)
		{
			throw new global::NonControllableException(name);
		}
		if (b == 2)
		{
			throw new global::NonPlayerRootControllableException(name);
		}
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x00028B24 File Offset: 0x00026D24
	private static byte GetVesselCompatibility(string name)
	{
		global::NetMainPrefab.EnsurePrefabName(name);
		byte b;
		if (global::ControllablePrefab.vesselCompatibilityCache.TryGetValue(name, out b))
		{
			return b;
		}
		global::ControllablePrefab controllablePrefab = global::NetMainPrefab.Lookup<global::ControllablePrefab>(name);
		if (!controllablePrefab)
		{
			b = 0;
		}
		else
		{
			b = controllablePrefab.vesselCompatibility;
		}
		global::ControllablePrefab.vesselCompatibilityCache[name] = b;
		return b;
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x00028B78 File Offset: 0x00026D78
	public static void EnsurePrefabIsVessel(string name, out global::ControllablePrefab.VesselInfo vi)
	{
		byte vesselCompatibility = global::ControllablePrefab.GetVesselCompatibility(name);
		if ((vesselCompatibility & 1) != 1)
		{
			if ((vesselCompatibility & 64) == 64)
			{
				throw new global::NonVesselControllableException(name);
			}
			throw new global::NonControllableException(name);
		}
		else
		{
			if ((vesselCompatibility & 24) == 0)
			{
				throw new global::NonControllableException("The vessel has not been marked for either ai and/or player control. not bothering to spawn it.");
			}
			vi = new global::ControllablePrefab.VesselInfo(vesselCompatibility);
			return;
		}
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x00028BCC File Offset: 0x00026DCC
	public static void EnsurePrefabIsVessel(string name, global::Controllable forControllable, out global::ControllablePrefab.VesselInfo vi)
	{
		global::ControllablePrefab.EnsurePrefabIsVessel(name, out vi);
		if (forControllable && forControllable.controlled)
		{
			if (forControllable.aiControlled)
			{
				if (!vi.supportsAI)
				{
					throw new global::NonAIVesselControllableException(name);
				}
			}
			else if (forControllable.playerControlled && !vi.supportsPlayer)
			{
				throw new global::NonPlayerVesselControllableException(name);
			}
		}
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x00028C38 File Offset: 0x00026E38
	public static void EnsurePrefabIsAIRootCompatible(string name, out bool staticGroup)
	{
		global::NetMainPrefab.EnsurePrefabName(name);
		sbyte b;
		if (!global::ControllablePrefab.aiRootCompatibilityCache.TryGetValue(name, out b))
		{
			global::ControllablePrefab controllablePrefab = global::NetMainPrefab.Lookup<global::ControllablePrefab>(name);
			if (!controllablePrefab)
			{
				b = 0;
			}
			else if (!controllablePrefab.aiRootComapatable)
			{
				b = 2;
			}
			else
			{
				b = ((!((global::Character)controllablePrefab.serverPrefab).controllable.classFlagsStaticGroup) ? 1 : -1);
			}
			global::ControllablePrefab.aiRootCompatibilityCache[name] = b;
		}
		sbyte b2 = b;
		switch (b2 + 1)
		{
		case 0:
			staticGroup = true;
			return;
		case 2:
			staticGroup = false;
			return;
		case 3:
			throw new global::NonAIRootControllableException(name);
		}
		throw new global::NonControllableException(name);
	}

	// Token: 0x040006D3 RID: 1747
	private const byte kVesselFlag_Vessel = 1;

	// Token: 0x040006D4 RID: 1748
	private const byte kVesselFlag_Vessel_Standalone = 3;

	// Token: 0x040006D5 RID: 1749
	private const byte kVesselFlag_Vessel_Dependant = 5;

	// Token: 0x040006D6 RID: 1750
	private const byte kVesselFlag_Vessel_Free = 7;

	// Token: 0x040006D7 RID: 1751
	private const byte kVesselFlag_PlayerCanControl = 8;

	// Token: 0x040006D8 RID: 1752
	private const byte kVesselFlag_AICanControl = 16;

	// Token: 0x040006D9 RID: 1753
	private const byte kVesselFlag_StaticGroup = 32;

	// Token: 0x040006DA RID: 1754
	private const byte kVesselFlag_Missing = 64;

	// Token: 0x040006DB RID: 1755
	private const byte kVesselKindMask = 7;

	// Token: 0x040006DC RID: 1756
	private static readonly Type[] minimalRequiredIDLocals = new Type[]
	{
		typeof(global::Controllable)
	};

	// Token: 0x040006DD RID: 1757
	private static Dictionary<string, byte> playerRootCompatibilityCache = new Dictionary<string, byte>();

	// Token: 0x040006DE RID: 1758
	private static Dictionary<string, sbyte> aiRootCompatibilityCache = new Dictionary<string, sbyte>();

	// Token: 0x040006DF RID: 1759
	private static Dictionary<string, byte> vesselCompatibilityCache = new Dictionary<string, byte>();

	// Token: 0x02000153 RID: 339
	public struct VesselInfo
	{
		// Token: 0x06000998 RID: 2456 RVA: 0x00028CF4 File Offset: 0x00026EF4
		internal VesselInfo(byte data)
		{
			this.data = data;
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00028D00 File Offset: 0x00026F00
		public bool staticGroup
		{
			get
			{
				return (this.data & 32) == 32;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00028D10 File Offset: 0x00026F10
		public bool supportsAI
		{
			get
			{
				return (this.data & 16) == 16;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x00028D20 File Offset: 0x00026F20
		public bool supportsPlayer
		{
			get
			{
				return (this.data & 8) == 8;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00028D30 File Offset: 0x00026F30
		public bool canBind
		{
			get
			{
				switch (this.data & 7)
				{
				case 0:
					return false;
				case 3:
					return false;
				case 5:
					return true;
				case 7:
					return true;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00028D80 File Offset: 0x00026F80
		public bool mustBind
		{
			get
			{
				switch (this.data & 7)
				{
				case 0:
					return false;
				case 3:
					return false;
				case 5:
					return true;
				case 7:
					return false;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x00028DD0 File Offset: 0x00026FD0
		public bool bindless
		{
			get
			{
				switch (this.data & 7)
				{
				case 0:
					return false;
				case 3:
					return true;
				case 5:
					return false;
				case 7:
					return true;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x040006E0 RID: 1760
		private byte data;
	}
}
