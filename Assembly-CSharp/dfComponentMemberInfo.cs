using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x020007D8 RID: 2008
[Serializable]
public class dfComponentMemberInfo
{
	// Token: 0x17000D6F RID: 3439
	// (get) Token: 0x0600459D RID: 17821 RVA: 0x001059AC File Offset: 0x00103BAC
	public bool IsValid
	{
		get
		{
			return this.Component != null && !string.IsNullOrEmpty(this.MemberName) && this.Component.GetType().GetMember(this.MemberName).FirstOrDefault<MemberInfo>() != null;
		}
	}

	// Token: 0x0600459E RID: 17822 RVA: 0x00105A0C File Offset: 0x00103C0C
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

	// Token: 0x0600459F RID: 17823 RVA: 0x00105AD4 File Offset: 0x00103CD4
	public MethodInfo GetMethod()
	{
		return this.Component.GetType().GetMember(this.MemberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault<MemberInfo>() as MethodInfo;
	}

	// Token: 0x060045A0 RID: 17824 RVA: 0x00105B08 File Offset: 0x00103D08
	public global::dfObservableProperty GetProperty()
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
		return new global::dfObservableProperty(this.Component, memberInfo);
	}

	// Token: 0x060045A1 RID: 17825 RVA: 0x00105BA4 File Offset: 0x00103DA4
	public override string ToString()
	{
		string arg = (!(this.Component != null)) ? "[Missing ComponentType]" : this.Component.GetType().Name;
		string arg2 = string.IsNullOrEmpty(this.MemberName) ? "[Missing MemberName]" : this.MemberName;
		return string.Format("{0}.{1}", arg, arg2);
	}

	// Token: 0x0400249C RID: 9372
	public Component Component;

	// Token: 0x0400249D RID: 9373
	public string MemberName;
}
