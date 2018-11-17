using System;
using UnityEngine;

namespace Facepunch.Attributes
{
	// Token: 0x020003CE RID: 974
	public abstract class ObjectLookupFieldAttribute : FieldAttribute
	{
		// Token: 0x06002484 RID: 9348 RVA: 0x0008B8F8 File Offset: 0x00089AF8
		protected ObjectLookupFieldAttribute(PrefabLookupKinds kinds, Type minimumType, SearchMode searchModeDefault, Type[] interfaceTypes)
		{
			this.Kinds = kinds;
			this.MinimumType = minimumType;
			if (searchModeDefault != SearchMode.Default)
			{
				this.searchModeDefault = searchModeDefault;
			}
			this.RequiredInterfaces = (interfaceTypes ?? ObjectLookupFieldAttribute.Empty.TypeArray);
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x0008B954 File Offset: 0x00089B54
		// (set) Token: 0x06002486 RID: 9350 RVA: 0x0008B95C File Offset: 0x00089B5C
		public bool AllowNull { get; set; }

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x0008B968 File Offset: 0x00089B68
		// (set) Token: 0x06002488 RID: 9352 RVA: 0x0008B988 File Offset: 0x00089B88
		public SearchMode SearchMode
		{
			get
			{
				return (this.searchMode != SearchMode.Default) ? this.searchMode : this.searchModeDefault;
			}
			protected set
			{
				this.searchMode = value;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002489 RID: 9353 RVA: 0x0008B994 File Offset: 0x00089B94
		// (set) Token: 0x0600248A RID: 9354 RVA: 0x0008B9AC File Offset: 0x00089BAC
		public Type MinimumType
		{
			get
			{
				return this.minType ?? this.attributeMinimumType;
			}
			set
			{
				if (value != null && !this.attributeMinimumType.IsAssignableFrom(value) && !this.CompliantMinimumType(value))
				{
					throw new ArgumentOutOfRangeException("value", value, "The type is not assignable given restrictions");
				}
				this.minType = value;
			}
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x0008B9EC File Offset: 0x00089BEC
		protected virtual CustomLookupResult CustomLookup(object value, Type type, ref Object find)
		{
			return CustomLookupResult.Fallback;
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x0008B9F0 File Offset: 0x00089BF0
		public CustomLookupResult Lookup(object value, out Object find)
		{
			return this.Lookup(value, this.MinimumType, out find);
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x0008BA00 File Offset: 0x00089C00
		public CustomLookupResult Lookup(object value, Type type, out Object find)
		{
			find = null;
			if (!this.MinimumType.IsAssignableFrom(type))
			{
				return CustomLookupResult.FailCast;
			}
			foreach (Type type2 in this.RequiredInterfaces)
			{
				if (!type2.IsAssignableFrom(type))
				{
					return CustomLookupResult.FailInterface;
				}
			}
			CustomLookupResult customLookupResult;
			try
			{
				customLookupResult = this.CustomLookup(value, type, ref find);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex, find);
				return CustomLookupResult.FailCustomException;
			}
			if (customLookupResult == CustomLookupResult.Fallback)
			{
				customLookupResult = CustomLookupResult.Accept;
			}
			if (customLookupResult == CustomLookupResult.Accept)
			{
				try
				{
					customLookupResult = this.Confirm(find);
				}
				catch (Exception ex2)
				{
					Debug.LogException(ex2, find);
					return CustomLookupResult.FailConfirmException;
				}
			}
			return customLookupResult;
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x0008BAE8 File Offset: 0x00089CE8
		public CustomLookupResult Lookup<TObj>(object value, out TObj find) where TObj : Object
		{
			return this.Lookup<TObj>(value, typeof(TObj), out find);
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x0008BAFC File Offset: 0x00089CFC
		public CustomLookupResult Lookup<TObj>(object value, Type type, out TObj find) where TObj : Object
		{
			if (!typeof(TObj).IsAssignableFrom(type))
			{
				throw new ArgumentOutOfRangeException("type", type, "type is not assignable to the generic " + typeof(TObj));
			}
			Object @object;
			CustomLookupResult customLookupResult;
			try
			{
				customLookupResult = this.Lookup(value, typeof(TObj), out @object);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				find = (TObj)((object)null);
				return CustomLookupResult.FailCustomException;
			}
			if (customLookupResult > CustomLookupResult.Fallback)
			{
				try
				{
					find = (TObj)((object)@object);
				}
				catch (Exception ex2)
				{
					Debug.LogException(ex2, @object);
					find = (TObj)((object)null);
					return CustomLookupResult.FailCast;
				}
			}
			else
			{
				try
				{
					find = (TObj)((object)@object);
				}
				catch
				{
					find = (TObj)((object)null);
				}
			}
			return customLookupResult;
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x0008BC2C File Offset: 0x00089E2C
		protected virtual CustomLookupResult CustomConfirm(Object obj, bool isNull, Type type)
		{
			return CustomLookupResult.Fallback;
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x0008BC30 File Offset: 0x00089E30
		public CustomLookupResult Confirm(Object obj)
		{
			bool flag;
			if (!this.AllowNull)
			{
				if (!obj)
				{
					return CustomLookupResult.FailNull;
				}
				flag = false;
			}
			else
			{
				flag = !obj;
			}
			CustomLookupResult customLookupResult;
			if (!flag)
			{
				Type type;
				try
				{
					type = obj.GetType();
				}
				catch (Exception ex)
				{
					Debug.LogException(ex, obj);
					return CustomLookupResult.FailNull;
				}
				try
				{
					customLookupResult = this.CustomConfirm(obj, false, type);
				}
				catch (Exception ex2)
				{
					Debug.LogException(ex2, obj);
					customLookupResult = CustomLookupResult.FailConfirmException;
				}
			}
			else
			{
				try
				{
					customLookupResult = this.CustomConfirm(null, true, null);
				}
				catch (Exception ex3)
				{
					Debug.LogException(ex3);
					customLookupResult = CustomLookupResult.FailConfirmException;
				}
			}
			if (customLookupResult == CustomLookupResult.Fallback)
			{
				return CustomLookupResult.AcceptConfirmed;
			}
			return customLookupResult;
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x0008BD2C File Offset: 0x00089F2C
		protected virtual bool CompliantMinimumType(Type type)
		{
			return true;
		}

		// Token: 0x040011A0 RID: 4512
		public readonly PrefabLookupKinds Kinds;

		// Token: 0x040011A1 RID: 4513
		private Type minType;

		// Token: 0x040011A2 RID: 4514
		private SearchMode searchMode;

		// Token: 0x040011A3 RID: 4515
		private readonly Type attributeMinimumType = typeof(Object);

		// Token: 0x040011A4 RID: 4516
		private readonly SearchMode searchModeDefault = SearchMode.MainAsset;

		// Token: 0x040011A5 RID: 4517
		public readonly Type[] RequiredInterfaces;

		// Token: 0x020003CF RID: 975
		private static class Empty
		{
			// Token: 0x040011A7 RID: 4519
			public static readonly Type[] TypeArray = new Type[0];
		}
	}
}
