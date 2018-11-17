using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x020007CD RID: 1997
public static class ReflectionExtensions
{
	// Token: 0x060044A5 RID: 17573 RVA: 0x001008B0 File Offset: 0x000FEAB0
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

	// Token: 0x060044A6 RID: 17574 RVA: 0x0010090C File Offset: 0x000FEB0C
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

	// Token: 0x060044A7 RID: 17575 RVA: 0x001009A8 File Offset: 0x000FEBA8
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
