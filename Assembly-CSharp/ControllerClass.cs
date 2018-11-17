using System;
using UnityEngine;

// Token: 0x0200013E RID: 318
public class ControllerClass : ScriptableObject
{
	// Token: 0x17000257 RID: 599
	// (get) Token: 0x06000925 RID: 2341 RVA: 0x00026BE8 File Offset: 0x00024DE8
	internal string npcName
	{
		get
		{
			return (!string.IsNullOrEmpty(this._npcName)) ? this._npcName : base.name;
		}
	}

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06000926 RID: 2342 RVA: 0x00026C0C File Offset: 0x00024E0C
	internal bool root
	{
		get
		{
			return (this.runtime & ControllerClass.Configuration.DynamicFreeVessel) == ControllerClass.Configuration.DynamicRoot;
		}
	}

	// Token: 0x17000259 RID: 601
	// (get) Token: 0x06000927 RID: 2343 RVA: 0x00026C1C File Offset: 0x00024E1C
	internal bool vessel
	{
		get
		{
			return (this.runtime & ControllerClass.Configuration.DynamicFreeVessel) != ControllerClass.Configuration.DynamicRoot;
		}
	}

	// Token: 0x1700025A RID: 602
	// (get) Token: 0x06000928 RID: 2344 RVA: 0x00026C2C File Offset: 0x00024E2C
	internal bool staticGroup
	{
		get
		{
			return (this.runtime & ControllerClass.Configuration.StaticRoot) == ControllerClass.Configuration.StaticRoot;
		}
	}

	// Token: 0x1700025B RID: 603
	// (get) Token: 0x06000929 RID: 2345 RVA: 0x00026C3C File Offset: 0x00024E3C
	internal bool vesselStandalone
	{
		get
		{
			return (this.runtime & ControllerClass.Configuration.DynamicFreeVessel) == ControllerClass.Configuration.DynamicStandaloneVessel;
		}
	}

	// Token: 0x1700025C RID: 604
	// (get) Token: 0x0600092A RID: 2346 RVA: 0x00026C4C File Offset: 0x00024E4C
	internal bool vesselDependant
	{
		get
		{
			return (this.runtime & ControllerClass.Configuration.DynamicFreeVessel) == ControllerClass.Configuration.DynamicDependantVessel;
		}
	}

	// Token: 0x1700025D RID: 605
	// (get) Token: 0x0600092B RID: 2347 RVA: 0x00026C5C File Offset: 0x00024E5C
	internal bool vesselFree
	{
		get
		{
			return (this.runtime & ControllerClass.Configuration.DynamicFreeVessel) == ControllerClass.Configuration.DynamicFreeVessel;
		}
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x00026C6C File Offset: 0x00024E6C
	internal string GetClassName(bool player, bool local)
	{
		return (this.classNames != null) ? this.classNames.GetClassName(player, local) : null;
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x00026C8C File Offset: 0x00024E8C
	internal bool GetClassName(bool player, bool local, out string className)
	{
		string className2;
		className = (className2 = this.GetClassName(player, local));
		return !object.ReferenceEquals(className2, null);
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x00026CB0 File Offset: 0x00024EB0
	internal bool DefinesClass(bool player, bool local)
	{
		return !object.ReferenceEquals(this.GetClassName(player, local), null);
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x00026CC4 File Offset: 0x00024EC4
	internal bool DefinesClass(bool player)
	{
		return !object.ReferenceEquals(this.GetClassName(player, false) ?? this.GetClassName(player, true), null);
	}

	// Token: 0x1700025E RID: 606
	// (get) Token: 0x06000930 RID: 2352 RVA: 0x00026CF4 File Offset: 0x00024EF4
	internal string unassignedClassName
	{
		get
		{
			return this.classNames.unassignedClassName;
		}
	}

	// Token: 0x0400064E RID: 1614
	private const ControllerClass.Configuration kDriverMask = ControllerClass.Configuration.DynamicFreeVessel;

	// Token: 0x0400064F RID: 1615
	private const ControllerClass.Configuration kStaticMask = ControllerClass.Configuration.StaticRoot;

	// Token: 0x04000650 RID: 1616
	private const ControllerClass.Configuration kDriver_Root = ControllerClass.Configuration.DynamicRoot;

	// Token: 0x04000651 RID: 1617
	private const ControllerClass.Configuration kDriver_StandaloneVessel = ControllerClass.Configuration.DynamicStandaloneVessel;

	// Token: 0x04000652 RID: 1618
	private const ControllerClass.Configuration kDriver_DependantVessel = ControllerClass.Configuration.DynamicDependantVessel;

	// Token: 0x04000653 RID: 1619
	private const ControllerClass.Configuration kDriver_FreeVessel = ControllerClass.Configuration.DynamicFreeVessel;

	// Token: 0x04000654 RID: 1620
	private const ControllerClass.Configuration kStatic_Static = ControllerClass.Configuration.StaticRoot;

	// Token: 0x04000655 RID: 1621
	private const ControllerClass.Configuration kStatic_Dynamic = ControllerClass.Configuration.DynamicRoot;

	// Token: 0x04000656 RID: 1622
	[SerializeField]
	private string _npcName = string.Empty;

	// Token: 0x04000657 RID: 1623
	[SerializeField]
	private ControllerClassesConfigurations classNames;

	// Token: 0x04000658 RID: 1624
	[SerializeField]
	private ControllerClass.Configuration runtime;

	// Token: 0x0200013F RID: 319
	public enum Configuration
	{
		// Token: 0x0400065A RID: 1626
		DynamicRoot,
		// Token: 0x0400065B RID: 1627
		DynamicStandaloneVessel,
		// Token: 0x0400065C RID: 1628
		DynamicDependantVessel,
		// Token: 0x0400065D RID: 1629
		DynamicFreeVessel,
		// Token: 0x0400065E RID: 1630
		StaticRoot,
		// Token: 0x0400065F RID: 1631
		StaticStandaloneVessel,
		// Token: 0x04000660 RID: 1632
		StaticDependantVessel,
		// Token: 0x04000661 RID: 1633
		StaticFreeVessel
	}

	// Token: 0x02000140 RID: 320
	public struct Merge
	{
		// Token: 0x06000931 RID: 2353 RVA: 0x00026D04 File Offset: 0x00024F04
		public bool Add(ControllerClass @class)
		{
			if (!@class)
			{
				return false;
			}
			ControllerClass.Merge.Instance instance;
			instance.hash = @class.GetHashCode();
			instance.value = @class;
			if (this.length == 1)
			{
				if (this.hash == instance.hash && object.ReferenceEquals(this.first.value, instance.value))
				{
					return false;
				}
			}
			else if (this.length > 1 && (this.hash & instance.hash) == instance.hash)
			{
				for (int i = 0; i < this.length; i++)
				{
					if (this.classes[i].hash == this.hash && object.ReferenceEquals(this.classes[i].value, instance.value))
					{
						return false;
					}
				}
			}
			this.hash |= instance.hash;
			int num = this.length++;
			if (num == 0)
			{
				this.first = instance;
			}
			else if (num == 1)
			{
				this.classes = new ControllerClass.Merge.Instance[]
				{
					this.first,
					instance
				};
				this.first.hash = 0;
				this.first.value = null;
			}
			else
			{
				Array.Resize<ControllerClass.Merge.Instance>(ref this.classes, this.length);
				this.classes[num] = instance;
			}
			return true;
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00026E9C File Offset: 0x0002509C
		public bool any
		{
			get
			{
				return this.length > 0;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00026EA8 File Offset: 0x000250A8
		public bool root
		{
			get
			{
				if (this.length <= 0)
				{
					return false;
				}
				if (this.length == 1)
				{
					return this.first.value.root;
				}
				for (int i = 0; i < this.length; i++)
				{
					if (!this.classes[i].value.root)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x00026F18 File Offset: 0x00025118
		public bool vessel
		{
			get
			{
				if (this.length <= 0)
				{
					return false;
				}
				if (this.length == 1)
				{
					return this.first.value.vessel;
				}
				for (int i = 0; i < this.length; i++)
				{
					if (!this.classes[i].value.vessel)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00026F88 File Offset: 0x00025188
		public bool staticGroup
		{
			get
			{
				if (this.length <= 0)
				{
					return false;
				}
				if (this.length == 1)
				{
					return this.first.value.staticGroup;
				}
				for (int i = 0; i < this.length; i++)
				{
					if (!this.classes[i].value.staticGroup)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x00026FF8 File Offset: 0x000251F8
		public bool vesselStandalone
		{
			get
			{
				if (this.length <= 0)
				{
					return false;
				}
				if (this.length == 1)
				{
					return this.first.value.vesselStandalone;
				}
				for (int i = 0; i < this.length; i++)
				{
					if (!this.classes[i].value.vesselStandalone)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00027068 File Offset: 0x00025268
		public bool vesselDependant
		{
			get
			{
				if (this.length <= 0)
				{
					return false;
				}
				if (this.length == 1)
				{
					return this.first.value.vesselDependant;
				}
				for (int i = 0; i < this.length; i++)
				{
					if (!this.classes[i].value.vesselDependant)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x000270D8 File Offset: 0x000252D8
		public bool vesselFree
		{
			get
			{
				if (this.length <= 0)
				{
					return false;
				}
				if (this.length == 1)
				{
					return this.first.value.vesselFree;
				}
				for (int i = 0; i < this.length; i++)
				{
					if (!this.classes[i].value.vesselFree)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000266 RID: 614
		public bool this[bool player, bool local]
		{
			get
			{
				if (this.length <= 0)
				{
					return false;
				}
				if (this.length == 1)
				{
					return this.first.value.DefinesClass(player, local);
				}
				for (int i = 0; i < this.length; i++)
				{
					if (!this.classes[i].value.DefinesClass(player, local))
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000267 RID: 615
		public bool this[bool player]
		{
			get
			{
				if (this.length <= 0)
				{
					return false;
				}
				if (this.length == 1)
				{
					return this.first.value.DefinesClass(player);
				}
				for (int i = 0; i < this.length; i++)
				{
					if (!this.classes[i].value.DefinesClass(player))
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0002722C File Offset: 0x0002542C
		public bool multiple
		{
			get
			{
				return this.length > 1;
			}
		}

		// Token: 0x04000662 RID: 1634
		private int length;

		// Token: 0x04000663 RID: 1635
		private int hash;

		// Token: 0x04000664 RID: 1636
		private ControllerClass.Merge.Instance first;

		// Token: 0x04000665 RID: 1637
		private ControllerClass.Merge.Instance[] classes;

		// Token: 0x02000141 RID: 321
		private struct Instance
		{
			// Token: 0x04000666 RID: 1638
			public int hash;

			// Token: 0x04000667 RID: 1639
			public ControllerClass value;
		}
	}
}
