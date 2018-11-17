using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x02000706 RID: 1798
[AddComponentMenu("Daikon Forge/Data Binding/Proxy Data Object")]
[Serializable]
public class dfDataObjectProxy : MonoBehaviour, IDataBindingComponent
{
	// Token: 0x1400005B RID: 91
	// (add) Token: 0x0600418E RID: 16782 RVA: 0x000FCF34 File Offset: 0x000FB134
	// (remove) Token: 0x0600418F RID: 16783 RVA: 0x000FCF50 File Offset: 0x000FB150
	public event dfDataObjectProxy.DataObjectChangedHandler DataChanged;

	// Token: 0x06004190 RID: 16784 RVA: 0x000FCF6C File Offset: 0x000FB16C
	public void Start()
	{
		if (this.DataType == null)
		{
			Debug.LogError("Unable to retrieve System.Type reference for type: " + this.TypeName);
		}
	}

	// Token: 0x17000CEE RID: 3310
	// (get) Token: 0x06004191 RID: 16785 RVA: 0x000FCF9C File Offset: 0x000FB19C
	// (set) Token: 0x06004192 RID: 16786 RVA: 0x000FCFA4 File Offset: 0x000FB1A4
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

	// Token: 0x17000CEF RID: 3311
	// (get) Token: 0x06004193 RID: 16787 RVA: 0x000FCFC8 File Offset: 0x000FB1C8
	public Type DataType
	{
		get
		{
			return this.getTypeFromName(this.typeName);
		}
	}

	// Token: 0x17000CF0 RID: 3312
	// (get) Token: 0x06004194 RID: 16788 RVA: 0x000FCFD8 File Offset: 0x000FB1D8
	// (set) Token: 0x06004195 RID: 16789 RVA: 0x000FCFE0 File Offset: 0x000FB1E0
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

	// Token: 0x06004196 RID: 16790 RVA: 0x000FD034 File Offset: 0x000FB234
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

	// Token: 0x06004197 RID: 16791 RVA: 0x000FD090 File Offset: 0x000FB290
	public dfObservableProperty GetProperty(string PropertyName)
	{
		if (this.data == null)
		{
			return null;
		}
		return new dfObservableProperty(this.data, PropertyName);
	}

	// Token: 0x06004198 RID: 16792 RVA: 0x000FD0AC File Offset: 0x000FB2AC
	private Type getTypeFromName(string typeName)
	{
		Type[] types = base.GetType().Assembly.GetTypes();
		return (from t in types
		where t.Name == typeName
		select t).FirstOrDefault<Type>();
	}

	// Token: 0x06004199 RID: 16793 RVA: 0x000FD0F0 File Offset: 0x000FB2F0
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

	// Token: 0x0600419A RID: 16794 RVA: 0x000FD144 File Offset: 0x000FB344
	public void Bind()
	{
	}

	// Token: 0x0600419B RID: 16795 RVA: 0x000FD148 File Offset: 0x000FB348
	public void Unbind()
	{
	}

	// Token: 0x04002295 RID: 8853
	[SerializeField]
	protected string typeName;

	// Token: 0x04002296 RID: 8854
	private object data;

	// Token: 0x020008DF RID: 2271
	// (Invoke) Token: 0x06004D54 RID: 19796
	[dfEventCategory("Data Changed")]
	public delegate void DataObjectChangedHandler(object data);
}
