using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x020007DA RID: 2010
[AddComponentMenu("Daikon Forge/Data Binding/Proxy Data Object")]
[Serializable]
public class dfDataObjectProxy : MonoBehaviour, global::IDataBindingComponent
{
	// Token: 0x1400005B RID: 91
	// (add) Token: 0x060045B6 RID: 17846 RVA: 0x00105DE8 File Offset: 0x00103FE8
	// (remove) Token: 0x060045B7 RID: 17847 RVA: 0x00105E04 File Offset: 0x00104004
	public event global::dfDataObjectProxy.DataObjectChangedHandler DataChanged;

	// Token: 0x060045B8 RID: 17848 RVA: 0x00105E20 File Offset: 0x00104020
	public void Start()
	{
		if (this.DataType == null)
		{
			Debug.LogError("Unable to retrieve System.Type reference for type: " + this.TypeName);
		}
	}

	// Token: 0x17000D76 RID: 3446
	// (get) Token: 0x060045B9 RID: 17849 RVA: 0x00105E50 File Offset: 0x00104050
	// (set) Token: 0x060045BA RID: 17850 RVA: 0x00105E58 File Offset: 0x00104058
	public string TypeName
	{
		get
		{
			return this.typeName;
		}
		set
		{
			if (this.typeName != value)
			{
				this.typeName = value;
				this.Data = null;
			}
		}
	}

	// Token: 0x17000D77 RID: 3447
	// (get) Token: 0x060045BB RID: 17851 RVA: 0x00105E7C File Offset: 0x0010407C
	public Type DataType
	{
		get
		{
			return this.getTypeFromName(this.typeName);
		}
	}

	// Token: 0x17000D78 RID: 3448
	// (get) Token: 0x060045BC RID: 17852 RVA: 0x00105E8C File Offset: 0x0010408C
	// (set) Token: 0x060045BD RID: 17853 RVA: 0x00105E94 File Offset: 0x00104094
	public object Data
	{
		get
		{
			return this.data;
		}
		set
		{
			if (!object.ReferenceEquals(value, this.data))
			{
				this.data = value;
				if (value != null)
				{
					this.typeName = value.GetType().Name;
				}
				if (this.DataChanged != null)
				{
					this.DataChanged(value);
				}
			}
		}
	}

	// Token: 0x060045BE RID: 17854 RVA: 0x00105EE8 File Offset: 0x001040E8
	public Type GetPropertyType(string PropertyName)
	{
		Type dataType = this.DataType;
		if (dataType == null)
		{
			return null;
		}
		MemberInfo memberInfo = dataType.GetMember(PropertyName, BindingFlags.Instance | BindingFlags.Public).FirstOrDefault<MemberInfo>();
		if (memberInfo is FieldInfo)
		{
			return ((FieldInfo)memberInfo).FieldType;
		}
		if (memberInfo is PropertyInfo)
		{
			return ((PropertyInfo)memberInfo).PropertyType;
		}
		return null;
	}

	// Token: 0x060045BF RID: 17855 RVA: 0x00105F44 File Offset: 0x00104144
	public global::dfObservableProperty GetProperty(string PropertyName)
	{
		if (this.data == null)
		{
			return null;
		}
		return new global::dfObservableProperty(this.data, PropertyName);
	}

	// Token: 0x060045C0 RID: 17856 RVA: 0x00105F60 File Offset: 0x00104160
	private Type getTypeFromName(string typeName)
	{
		Type[] types = base.GetType().Assembly.GetTypes();
		return (from t in types
		where t.Name == typeName
		select t).FirstOrDefault<Type>();
	}

	// Token: 0x060045C1 RID: 17857 RVA: 0x00105FA4 File Offset: 0x001041A4
	private static Type getTypeFromQualifiedName(string typeName)
	{
		Type type = Type.GetType(typeName);
		if (type != null)
		{
			return type;
		}
		if (typeName.IndexOf('.') == -1)
		{
			return null;
		}
		string assemblyString = typeName.Substring(0, typeName.IndexOf('.'));
		Assembly assembly = Assembly.Load(assemblyString);
		if (assembly == null)
		{
			return null;
		}
		return assembly.GetType(typeName);
	}

	// Token: 0x060045C2 RID: 17858 RVA: 0x00105FF8 File Offset: 0x001041F8
	public void Bind()
	{
	}

	// Token: 0x060045C3 RID: 17859 RVA: 0x00105FFC File Offset: 0x001041FC
	public void Unbind()
	{
	}

	// Token: 0x040024A5 RID: 9381
	[SerializeField]
	protected string typeName;

	// Token: 0x040024A6 RID: 9382
	private object data;

	// Token: 0x020007DB RID: 2011
	// (Invoke) Token: 0x060045C5 RID: 17861
	[global::dfEventCategory("Data Changed")]
	public delegate void DataObjectChangedHandler(object data);
}
