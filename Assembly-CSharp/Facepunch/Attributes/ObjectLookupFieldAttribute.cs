using System;
using UnityEngine;

namespace Facepunch.Attributes
{
	// Token: 0x0200047B RID: 1147
	public abstract class ObjectLookupFieldAttribute : FieldAttribute
	{
		// Token: 0x060027E6 RID: 10214 RVA: 0x00090CF4 File Offset: 0x0008EEF4
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

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060027E7 RID: 10215 RVA: 0x00090D50 File Offset: 0x0008EF50
		// (set) Token: 0x060027E8 RID: 10216 RVA: 0x00090D58 File Offset: 0x0008EF58
		public bool AllowNull { get; set; }

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060027E9 RID: 10217 RVA: 0x00090D64 File Offset: 0x0008EF64
		// (set) Token: 0x060027EA RID: 10218 RVA: 0x00090D84 File Offset: 0x0008EF84
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

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060027EB RID: 10219 RVA: 0x00090D90 File Offset: 0x0008EF90
		// (set) Token: 0x060027EC RID: 10220 RVA: 0x00090DA8 File Offset: 0x0008EFA8
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

		// Token: 0x060027ED RID: 10221 RVA: 0x00090DE8 File Offset: 0x0008EFE8
		protected virtual CustomLookupResult CustomLookup(object value, Type type, ref Object find)
		{
			return CustomLookupResult.Fallback;
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x00090DEC File Offset: 0x0008EFEC
		public CustomLookupResult Lookup(object value, out Object find)
		{
			return this.Lookup(value, this.MinimumType, out find);
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x00090DFC File Offset: 0x0008EFFC
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

		// Token: 0x060027F0 RID: 10224 RVA: 0x00090EE4 File Offset: 0x0008F0E4
		public CustomLookupResult Lookup<TObj>(object value, out TObj find) where TObj : Object
		{
			return this.Lookup<TObj>(value, typeof(TObj), out find);
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x00090EF8 File Offset: 0x0008F0F8
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

		// Token: 0x060027F2 RID: 10226 RVA: 0x00091028 File Offset: 0x0008F228
		protected virtual CustomLookupResult CustomConfirm(Object obj, bool isNull, Type type)
		{
			return CustomLookupResult.Fallback;
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x0009102C File Offset: 0x0008F22C
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

		// Token: 0x060027F4 RID: 10228 RVA: 0x00091128 File Offset: 0x0008F328
		protected virtual bool CompliantMinimumType(Type type)
		{
			return true;
		}

		// Token: 0x04001306 RID: 4870
		public readonly PrefabLookupKinds Kinds;

		// Token: 0x04001307 RID: 4871
		private Type minType;

		// Token: 0x04001308 RID: 4872
		private SearchMode searchMode;

		// Token: 0x04001309 RID: 4873
		private readonly Type attributeMinimumType = typeof(Object);

		// Token: 0x0400130A RID: 4874
		private readonly SearchMode searchModeDefault = SearchMode.MainAsset;

		// Token: 0x0400130B RID: 4875
		public readonly Type[] RequiredInterfaces;

		// Token: 0x0200047C RID: 1148
		private static class Empty
		{
			// Token: 0x0400130D RID: 4877
			public static readonly Type[] TypeArray = new Type[0];
		}
	}
}
