using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x020006FB RID: 1787
public static class ReflectionExtensions
{
	// Token: 0x06004089 RID: 16521 RVA: 0x000F7CAC File Offset: 0x000F5EAC
	public static FieldInfo[] GetAllFields(this Type type)
	{
		if (type == null)
		{
			return new FieldInfo[0];
		}
		BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
		return (from f in type.GetFields(bindingAttr).Concat(type.BaseType.GetAllFields())
		where !f.IsDefined(typeof(HideInInspector), true)
		select f).ToArray<FieldInfo>();
	}

	// Token: 0x0600408A RID: 16522 RVA: 0x000F7D08 File Offset: 0x000F5F08
	public static object GetProperty(this object target, string property)
	{
		if (target == null)
		{
			throw new NullReferenceException("Target is null");
		}
		MemberInfo[] member = target.GetType().GetMember(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		if (member == null || member.Length == 0)
		{
			throw new IndexOutOfRangeException("Property not found: " + property);
		}
		MemberInfo memberInfo = member[0];
		if (memberInfo is FieldInfo)
		{
			return ((FieldInfo)memberInfo).GetValue(target);
		}
		if (memberInfo is PropertyInfo)
		{
			return ((PropertyInfo)memberInfo).GetValue(target, null);
		}
		throw new InvalidOperationException("Member type not supported: " + memberInfo.MemberType);
	}

	// Token: 0x0600408B RID: 16523 RVA: 0x000F7DA4 File Offset: 0x000F5FA4
	public static void SetProperty(this object target, string property, object value)
	{
		if (target == null)
		{
			throw new NullReferenceException("Target is null");
		}
		MemberInfo[] member = target.GetType().GetMember(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		if (member == null || member.Length == 0)
		{
			throw new IndexOutOfRangeException("Property not found: " + property);
		}
		MemberInfo memberInfo = member[0];
		if (memberInfo is FieldInfo)
		{
			((FieldInfo)memberInfo).SetValue(target, value);
			return;
		}
		if (memberInfo is PropertyInfo)
		{
			((PropertyInfo)memberInfo).SetValue(target, value, null);
			return;
		}
		throw new InvalidOperationException("Member type not supported: " + memberInfo.MemberType);
	}
}
