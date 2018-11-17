using System;
using System.Collections.Generic;
using Facepunch;
using uLink;

// Token: 0x0200012E RID: 302
public class ControllablePrefab : CharacterPrefab
{
	// Token: 0x06000893 RID: 2195 RVA: 0x00025548 File Offset: 0x00023748
	public ControllablePrefab() : this(typeof(Character), false, ControllablePrefab.minimalRequiredIDLocals, false)
	{
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x00025564 File Offset: 0x00023764
	protected ControllablePrefab(Type characterType, params Type[] idlocalRequired) : this(characterType, true, idlocalRequired, idlocalRequired != null && idlocalRequired.Length > 0)
	{
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x00025580 File Offset: 0x00023780
	protected ControllablePrefab(Type characterType) : this(characterType, true, null, false)
	{
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x0002558C File Offset: 0x0002378C
	private ControllablePrefab(Type characterType, bool typeCheck, Type[] requiredIDLocalTypes, bool mergeTypes) : base(characterType, (!mergeTypes) ? ControllablePrefab.minimalRequiredIDLocals : CharacterPrefab.TypeArrayAppend(ControllablePrefab.minimalRequiredIDLocals, requiredIDLocalTypes))
	{
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x000255F8 File Offset: 0x000237F8
	protected override void StandardInitialization(bool didAppend, IDRemote appended, NetInstance instance, NetworkView view, ref NetworkMessageInfo info)
	{
		Character character = (Character)instance.idMain;
		Controllable controllable = character.controllable;
		controllable.PrepareInstantiate(view, ref info);
		base.StandardInitialization(false, appended, instance, view, ref info);
		if (didAppend)
		{
			NetMainPrefab.IssueLocallyAppended(appended, instance.idMain);
		}
		controllable.OnInstantiated();
	}

	// Token: 0x17000209 RID: 521
	// (get) Token: 0x06000899 RID: 2201 RVA: 0x00025648 File Offset: 0x00023848
	private bool playerRootComapatable
	{
		get
		{
			Controllable controllable = ((Character)base.serverPrefab).controllable;
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
			controllable = ((Character)base.proxyPrefab).controllable;
			return controllable && controllable.classFlagsRootControllable && controllable.classFlagsPlayerSupport;
		}
	}

	// Token: 0x1700020A RID: 522
	// (get) Token: 0x0600089A RID: 2202 RVA: 0x000256C8 File Offset: 0x000238C8
	private bool aiRootComapatable
	{
		get
		{
			Controllable controllable = ((Character)base.serverPrefab).controllable;
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
			controllable = ((Character)base.proxyPrefab).controllable;
			return controllable && controllable.classFlagsRootControllable && controllable.classFlagsAISupport;
		}
	}

	// Token: 0x1700020B RID: 523
	// (get) Token: 0x0600089B RID: 2203 RVA: 0x00025748 File Offset: 0x00023948
	private ControllerClass.Merge mergedClasses
	{
		get
		{
			ControllerClass.Merge result = default(ControllerClass.Merge);
			Controllable.MergeClasses(base.serverPrefab, ref result);
			Controllable.MergeClasses(base.proxyPrefab, ref result);
			Controllable.MergeClasses(base.localPrefab, ref result);
			return result;
		}
	}

	// Token: 0x1700020C RID: 524
	// (get) Token: 0x0600089C RID: 2204 RVA: 0x0002578C File Offset: 0x0002398C
	private byte vesselCompatibility
	{
		get
		{
			ControllerClass.Merge mergedClasses = this.mergedClasses;
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

	// Token: 0x0600089D RID: 2205 RVA: 0x00025834 File Offset: 0x00023A34
	public static void EnsurePrefabIsPlayerRootCompatible(string name)
	{
		NetMainPrefab.EnsurePrefabName(name);
		byte b;
		if (!ControllablePrefab.playerRootCompatibilityCache.TryGetValue(name, out b))
		{
			ControllablePrefab controllablePrefab = NetMainPrefab.Lookup<ControllablePrefab>(name);
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
			ControllablePrefab.playerRootCompatibilityCache[name] = b;
		}
		if (b == 0)
		{
			throw new NonControllableException(name);
		}
		if (b == 2)
		{
			throw new NonPlayerRootControllableException(name);
		}
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x000258B0 File Offset: 0x00023AB0
	private static byte GetVesselCompatibility(string name)
	{
		NetMainPrefab.EnsurePrefabName(name);
		byte b;
		if (ControllablePrefab.vesselCompatibilityCache.TryGetValue(name, out b))
		{
			return b;
		}
		ControllablePrefab controllablePrefab = NetMainPrefab.Lookup<ControllablePrefab>(name);
		if (!controllablePrefab)
		{
			b = 0;
		}
		else
		{
			b = controllablePrefab.vesselCompatibility;
		}
		ControllablePrefab.vesselCompatibilityCache[name] = b;
		return b;
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x00025904 File Offset: 0x00023B04
	public static void EnsurePrefabIsVessel(string name, out ControllablePrefab.VesselInfo vi)
	{
		byte vesselCompatibility = ControllablePrefab.GetVesselCompatibility(name);
		if ((vesselCompatibility & 1) != 1)
		{
			if ((vesselCompatibility & 64) == 64)
			{
				throw new NonVesselControllableException(name);
			}
			throw new NonControllableException(name);
		}
		else
		{
			if ((vesselCompatibility & 24) == 0)
			{
				throw new NonControllableException("The vessel has not been marked for either ai and/or player control. not bothering to spawn it.");
			}
			vi = new ControllablePrefab.VesselInfo(vesselCompatibility);
			return;
		}
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x00025958 File Offset: 0x00023B58
	public static void EnsurePrefabIsVessel(string name, Controllable forControllable, out ControllablePrefab.VesselInfo vi)
	{
		ControllablePrefab.EnsurePrefabIsVessel(name, out vi);
		if (forControllable && forControllable.controlled)
		{
			if (forControllable.aiControlled)
			{
				if (!vi.supportsAI)
				{
					throw new NonAIVesselControllableException(name);
				}
			}
			else if (forControllable.playerControlled && !vi.supportsPlayer)
			{
				throw new NonPlayerVesselControllableException(name);
			}
		}
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x000259C4 File Offset: 0x00023BC4
	public static void EnsurePrefabIsAIRootCompatible(string name, out bool staticGroup)
	{
		NetMainPrefab.EnsurePrefabName(name);
		sbyte b;
		if (!ControllablePrefab.aiRootCompatibilityCache.TryGetValue(name, out b))
		{
			ControllablePrefab controllablePrefab = NetMainPrefab.Lookup<ControllablePrefab>(name);
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
				b = ((!((Character)controllablePrefab.serverPrefab).controllable.classFlagsStaticGroup) ? 1 : -1);
			}
			ControllablePrefab.aiRootCompatibilityCache[name] = b;
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
			throw new NonAIRootControllableException(name);
		}
		throw new NonControllableException(name);
	}

	// Token: 0x040005F0 RID: 1520
	private const byte kVesselFlag_Vessel = 1;

	// Token: 0x040005F1 RID: 1521
	private const byte kVesselFlag_Vessel_Standalone = 3;

	// Token: 0x040005F2 RID: 1522
	private const byte kVesselFlag_Vessel_Dependant = 5;

	// Token: 0x040005F3 RID: 1523
	private const byte kVesselFlag_Vessel_Free = 7;

	// Token: 0x040005F4 RID: 1524
	private const byte kVesselFlag_PlayerCanControl = 8;

	// Token: 0x040005F5 RID: 1525
	private const byte kVesselFlag_AICanControl = 16;

	// Token: 0x040005F6 RID: 1526
	private const byte kVesselFlag_StaticGroup = 32;

	// Token: 0x040005F7 RID: 1527
	private const byte kVesselFlag_Missing = 64;

	// Token: 0x040005F8 RID: 1528
	private const byte kVesselKindMask = 7;

	// Token: 0x040005F9 RID: 1529
	private static readonly Type[] minimalRequiredIDLocals = new Type[]
	{
		typeof(Controllable)
	};

	// Token: 0x040005FA RID: 1530
	private static Dictionary<string, byte> playerRootCompatibilityCache = new Dictionary<string, byte>();

	// Token: 0x040005FB RID: 1531
	private static Dictionary<string, sbyte> aiRootCompatibilityCache = new Dictionary<string, sbyte>();

	// Token: 0x040005FC RID: 1532
	private static Dictionary<string, byte> vesselCompatibilityCache = new Dictionary<string, byte>();

	// Token: 0x0200012F RID: 303
	public struct VesselInfo
	{
		// Token: 0x060008A2 RID: 2210 RVA: 0x00025A80 File Offset: 0x00023C80
		internal VesselInfo(byte data)
		{
			this.data = data;
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00025A8C File Offset: 0x00023C8C
		public bool staticGroup
		{
			get
			{
				return (this.data & 32) == 32;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00025A9C File Offset: 0x00023C9C
		public bool supportsAI
		{
			get
			{
				return (this.data & 16) == 16;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00025AAC File Offset: 0x00023CAC
		public bool supportsPlayer
		{
			get
			{
				return (this.data & 8) == 8;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00025ABC File Offset: 0x00023CBC
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

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00025B0C File Offset: 0x00023D0C
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

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00025B5C File Offset: 0x00023D5C
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

		// Token: 0x040005FD RID: 1533
		private byte data;
	}
}
