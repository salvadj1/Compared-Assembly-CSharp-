using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x02000704 RID: 1796
[Serializable]
public class dfComponentMemberInfo
{
	// Token: 0x17000CE7 RID: 3303
	// (get) Token: 0x06004175 RID: 16757 RVA: 0x000FCAF8 File Offset: 0x000FACF8
	public bool IsValid
	{
		get
		{
			return this.Component != null && !string.IsNullOrEmpty(this.MemberName) && this.Component.GetType().GetMember(this.MemberName).FirstOrDefault<MemberInfo>() != null;
		}
	}

	// Token: 0x06004176 RID: 16758 RVA: 0x000FCB58 File Offset: 0x000FAD58
	public Type GetMemberType()
	{
		Type type = this.Component.GetType();
		MemberInfo memberInfo = type.GetMember(this.MemberName).FirstOrDefault<MemberInfo>();
		if (memberInfo == null)
		{
			throw new MissingMemberException("Member not found: " + type.Name + "." + this.MemberName);
		}
		if (memberInfo is FieldInfo)
		{
			return ((FieldInfo)memberInfo).FieldType;
		}
		if (memberInfo is PropertyInfo)
		{
			return ((PropertyInfo)memberInfo).PropertyType;
		}
		if (memberInfo is MethodInfo)
		{
			return ((MethodInfo)memberInfo).ReturnType;
		}
		if (memberInfo is EventInfo)
		{
			return ((EventInfo)memberInfo).EventHandlerType;
		}
		throw new InvalidCastException("Invalid member type: " + memberInfo.MemberType);
	}

	// Token: 0x06004177 RID: 16759 RVA: 0x000FCC20 File Offset: 0x000FAE20
	public MethodInfo GetMethod()
	{
		return this.Component.GetType().GetMember(this.MemberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault<MemberInfo>() as MethodInfo;
	}

	// Token: 0x06004178 RID: 16760 RVA: 0x000FCC54 File Offset: 0x000FAE54
	public dfObservableProperty GetProperty()
	{
		Type type = this.Component.GetType();
		MemberInfo memberInfo = this.Component.GetType().GetMember(this.MemberName).FirstOrDefault<MemberInfo>();
		if (memberInfo == null)
		{
			throw new MissingMemberException("Member not found: " + type.Name + "." + this.MemberName);
		}
		if (!(memberInfo is FieldInfo) && !(memberInfo is PropertyInfo))
		{
			throw new InvalidCastException("Member " + this.MemberName + " is not an observable field or property");
		}
		return new dfObservableProperty(this.Component, memberInfo);
	}

	// Token: 0x06004179 RID: 16761 RVA: 0x000FCCF0 File Offset: 0x000FAEF0
	public override string ToString()
	{
		string arg = (!(this.Component != null)) ? "[Missing ComponentType]" : this.Component.GetType().Name;
		string arg2 = string.IsNullOrEmpty(this.MemberName) ? "[Missing MemberName]" : this.MemberName;
		return string.Format("{0}.{1}", arg, arg2);
	}

	// Token: 0x0400228C RID: 8844
	public Component Component;

	// Token: 0x0400228D RID: 8845
	public string MemberName;
}
