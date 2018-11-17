using System;

namespace Facepunch.Attributes
{
	// Token: 0x0200047D RID: 1149
	public abstract class ObjectLookupFieldFixedTypeAttribute : ObjectLookupFieldAttribute
	{
		// Token: 0x060027F6 RID: 10230 RVA: 0x0009113C File Offset: 0x0008F33C
		protected ObjectLookupFieldFixedTypeAttribute(PrefabLookupKinds kinds, Type minimalType, SearchMode defaultSearchMode, Type[] interfacesRequired) : base(kinds, minimalType, defaultSearchMode, interfacesRequired)
		{
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060027F7 RID: 10231 RVA: 0x0009114C File Offset: 0x0008F34C
		public new Type MinimumType
		{
			get
			{
				return base.MinimumType;
			}
		}

		// Token: 0x0400130E RID: 4878
		public readonly Type[] RequiredComponents;
	}
}
