using System;

namespace Facepunch.Attributes
{
	// Token: 0x020003D0 RID: 976
	public abstract class ObjectLookupFieldFixedTypeAttribute : ObjectLookupFieldAttribute
	{
		// Token: 0x06002494 RID: 9364 RVA: 0x0008BD40 File Offset: 0x00089F40
		protected ObjectLookupFieldFixedTypeAttribute(PrefabLookupKinds kinds, Type minimalType, SearchMode defaultSearchMode, Type[] interfacesRequired) : base(kinds, minimalType, defaultSearchMode, interfacesRequired)
		{
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002495 RID: 9365 RVA: 0x0008BD50 File Offset: 0x00089F50
		public new Type MinimumType
		{
			get
			{
				return base.MinimumType;
			}
		}

		// Token: 0x040011A8 RID: 4520
		public readonly Type[] RequiredComponents;
	}
}
