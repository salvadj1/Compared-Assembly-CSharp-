using System;
using UnityEngine;

// Token: 0x02000168 RID: 360
public class ControllerClass : ScriptableObject
{
	// Token: 0x17000299 RID: 665
	// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0002A964 File Offset: 0x00028B64
	internal string npcName
	{
		get
		{
			return (!string.IsNullOrEmpty(this._npcName)) ? this._npcName : base.name;
		}
	}

	// Token: 0x1700029A RID: 666
	// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0002A988 File Offset: 0x00028B88
	internal bool root
	{
		get
		{
			return (this.runtime & global::ControllerClass.Configuration.DynamicFreeVessel) == global::ControllerClass.Configuration.DynamicRoot;
		}
	}

	// Token: 0x1700029B RID: 667
	// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0002A998 File Offset: 0x00028B98
	internal bool vessel
	{
		get
		{
			return (this.runtime & global::ControllerClass.Configuration.DynamicFreeVessel) != global::ControllerClass.Configuration.DynamicRoot;
		}
	}

	// Token: 0x1700029C RID: 668
	// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0002A9A8 File Offset: 0x00028BA8
	internal bool staticGroup
	{
		get
		{
			return (this.runtime & global::ControllerClass.Configuration.StaticRoot) == global::ControllerClass.Configuration.StaticRoot;
		}
	}

	// Token: 0x1700029D RID: 669
	// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0002A9B8 File Offset: 0x00028BB8
	internal bool vesselStandalone
	{
		get
		{
			return (this.runtime & global::ControllerClass.Configuration.DynamicFreeVessel) == global::ControllerClass.Configuration.DynamicStandaloneVessel;
		}
	}

	// Token: 0x1700029E RID: 670
	// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0002A9C8 File Offset: 0x00028BC8
	internal bool vesselDependant
	{
		get
		{
			return (this.runtime & global::ControllerClass.Configuration.DynamicFreeVessel) == global::ControllerClass.Configuration.DynamicDependantVessel;
		}
	}

	// Token: 0x1700029F RID: 671
	// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0002A9D8 File Offset: 0x00028BD8
	internal bool vesselFree
	{
		get
		{
			return (this.runtime & global::ControllerClass.Configuration.DynamicFreeVessel) == global::ControllerClass.Configuration.DynamicFreeVessel;
		}
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x0002A9E8 File Offset: 0x00028BE8
	internal string GetClassName(bool player, bool local)
	{
		return (this.classNames != null) ? this.classNames.GetClassName(player, local) : null;
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x0002AA08 File Offset: 0x00028C08
	internal bool GetClassName(bool player, bool local, out string className)
	{
		string className2;
		className = (className2 = this.GetClassName(player, local));
		return !object.ReferenceEquals(className2, null);
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x0002AA2C File Offset: 0x00028C2C
	internal bool DefinesClass(bool player, bool local)
	{
		return !object.ReferenceEquals(this.GetClassName(player, local), null);
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x0002AA40 File Offset: 0x00028C40
	internal bool DefinesClass(bool player)
	{
		return !object.ReferenceEquals(this.GetClassName(player, false) ?? this.GetClassName(player, true), null);
	}

	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0002AA70 File Offset: 0x00028C70
	internal string unassignedClassName
	{
		get
		{
			return this.classNames.unassignedClassName;
		}
	}

	// Token: 0x0400075D RID: 1885
	private const global::ControllerClass.Configuration kDriverMask = global::ControllerClass.Configuration.DynamicFreeVessel;

	// Token: 0x0400075E RID: 1886
	private const global::ControllerClass.Configuration kStaticMask = global::ControllerClass.Configuration.StaticRoot;

	// Token: 0x0400075F RID: 1887
	private const global::ControllerClass.Configuration kDriver_Root = global::ControllerClass.Configuration.DynamicRoot;

	// Token: 0x04000760 RID: 1888
	private const global::ControllerClass.Configuration kDriver_StandaloneVessel = global::ControllerClass.Configuration.DynamicStandaloneVessel;

	// Token: 0x04000761 RID: 1889
	private const global::ControllerClass.Configuration kDriver_DependantVessel = global::ControllerClass.Configuration.DynamicDependantVessel;

	// Token: 0x04000762 RID: 1890
	private const global::ControllerClass.Configuration kDriver_FreeVessel = global::ControllerClass.Configuration.DynamicFreeVessel;

	// Token: 0x04000763 RID: 1891
	private const global::ControllerClass.Configuration kStatic_Static = global::ControllerClass.Configuration.StaticRoot;

	// Token: 0x04000764 RID: 1892
	private const global::ControllerClass.Configuration kStatic_Dynamic = global::ControllerClass.Configuration.DynamicRoot;

	// Token: 0x04000765 RID: 1893
	[SerializeField]
	private string _npcName = string.Empty;

	// Token: 0x04000766 RID: 1894
	[SerializeField]
	private global::ControllerClassesConfigurations classNames;

	// Token: 0x04000767 RID: 1895
	[SerializeField]
	private global::ControllerClass.Configuration runtime;

	// Token: 0x02000169 RID: 361
	public enum Configuration
	{
		// Token: 0x04000769 RID: 1897
		DynamicRoot,
		// Token: 0x0400076A RID: 1898
		DynamicStandaloneVessel,
		// Token: 0x0400076B RID: 1899
		DynamicDependantVessel,
		// Token: 0x0400076C RID: 1900
		DynamicFreeVessel,
		// Token: 0x0400076D RID: 1901
		StaticRoot,
		// Token: 0x0400076E RID: 1902
		StaticStandaloneVessel,
		// Token: 0x0400076F RID: 1903
		StaticDependantVessel,
		// Token: 0x04000770 RID: 1904
		StaticFreeVessel
	}

	// Token: 0x0200016A RID: 362
	public struct Merge
	{
		// Token: 0x06000A57 RID: 2647 RVA: 0x0002AA80 File Offset: 0x00028C80
		public bool Add(global::ControllerClass @class)
		{
			if (!@class)
			{
				return false;
			}
			global::ControllerClass.Merge.Instance instance;
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
				this.classes = new global::ControllerClass.Merge.Instance[]
				{
					this.first,
					instance
				};
				this.first.hash = 0;
				this.first.value = null;
			}
			else
			{
				Array.Resize<global::ControllerClass.Merge.Instance>(ref this.classes, this.length);
				this.classes[num] = instance;
			}
			return true;
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0002AC18 File Offset: 0x00028E18
		public bool any
		{
			get
			{
				return this.length > 0;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0002AC24 File Offset: 0x00028E24
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

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0002AC94 File Offset: 0x00028E94
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

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0002AD04 File Offset: 0x00028F04
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

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0002AD74 File Offset: 0x00028F74
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

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0002ADE4 File Offset: 0x00028FE4
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

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0002AE54 File Offset: 0x00029054
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

		// Token: 0x170002A8 RID: 680
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

		// Token: 0x170002A9 RID: 681
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

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0002AFA8 File Offset: 0x000291A8
		public bool multiple
		{
			get
			{
				return this.length > 1;
			}
		}

		// Token: 0x04000771 RID: 1905
		private int length;

		// Token: 0x04000772 RID: 1906
		private int hash;

		// Token: 0x04000773 RID: 1907
		private global::ControllerClass.Merge.Instance first;

		// Token: 0x04000774 RID: 1908
		private global::ControllerClass.Merge.Instance[] classes;

		// Token: 0x0200016B RID: 363
		private struct Instance
		{
			// Token: 0x04000775 RID: 1909
			public int hash;

			// Token: 0x04000776 RID: 1910
			public global::ControllerClass value;
		}
	}
}
