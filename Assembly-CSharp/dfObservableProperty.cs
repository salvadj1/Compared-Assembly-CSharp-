using System;
using System.Linq;
using System.Reflection;

// Token: 0x0200070A RID: 1802
public class dfObservableProperty : IObservableValue
{
	// Token: 0x060041C3 RID: 16835 RVA: 0x000FDB5C File Offset: 0x000FBD5C
	internal dfObservableProperty(object target, string memberName)
	{
		MemberInfo memberInfo = target.GetType().GetMember(memberName, BindingFlags.Instance | BindingFlags.Public).FirstOrDefault<MemberInfo>();
		if (memberInfo == null)
		{
			throw new ArgumentException("Invalid property or field name: " + memberName, "memberName");
		}
		this.initMember(target, memberInfo);
	}

	// Token: 0x060041C4 RID: 16836 RVA: 0x000FDBA8 File Offset: 0x000FBDA8
	internal dfObservableProperty(object target, FieldInfo field)
	{
		this.initField(target, field);
	}

	// Token: 0x060041C5 RID: 16837 RVA: 0x000FDBB8 File Offset: 0x000FBDB8
	internal dfObservableProperty(object target, PropertyInfo property)
	{
		this.initProperty(target, property);
	}

	// Token: 0x060041C6 RID: 16838 RVA: 0x000FDBC8 File Offset: 0x000FBDC8
	internal dfObservableProperty(object target, MemberInfo member)
	{
		this.initMember(target, member);
	}

	// Token: 0x17000CF2 RID: 3314
	// (get) Token: 0x060041C7 RID: 16839 RVA: 0x000FDBD8 File Offset: 0x000FBDD8
	// (set) Token: 0x060041C8 RID: 16840 RVA: 0x000FDBE0 File Offset: 0x000FBDE0
	public object Value
	{
		get
		{
			return this.getter();
		}
		set
		{
			this.lastValue = value;
			this.setter(value);
			this.hasChanged = false;
		}
	}

	// Token: 0x17000CF3 RID: 3315
	// (get) Token: 0x060041C9 RID: 16841 RVA: 0x000FDBF8 File Offset: 0x000FBDF8
	public bool HasChanged
	{
		get
		{
			if (this.hasChanged)
			{
				return true;
			}
			object obj = this.getter();
			if (object.ReferenceEquals(obj, this.lastValue))
			{
				this.hasChanged = false;
			}
			else if (obj == null || this.lastValue == null)
			{
				this.hasChanged = true;
			}
			else
			{
				this.hasChanged = !obj.Equals(this.lastValue);
			}
			return this.hasChanged;
		}
	}

	// Token: 0x060041CA RID: 16842 RVA: 0x000FDC70 File Offset: 0x000FBE70
	public void ClearChangedFlag()
	{
		this.hasChanged = false;
		this.lastValue = this.getter();
	}

	// Token: 0x060041CB RID: 16843 RVA: 0x000FDC88 File Offset: 0x000FBE88
	private void initMember(object target, MemberInfo member)
	{
		if (member is FieldInfo)
		{
			this.initField(target, (FieldInfo)member);
		}
		else
		{
			this.initProperty(target, (PropertyInfo)member);
		}
	}

	// Token: 0x060041CC RID: 16844 RVA: 0x000FDCC0 File Offset: 0x000FBEC0
	private void initField(object target, FieldInfo field)
	{
		this.target = target;
		this.fieldInfo = field;
		this.Value = this.getter();
	}

	// Token: 0x060041CD RID: 16845 RVA: 0x000FDCDC File Offset: 0x000FBEDC
	private void initProperty(object target, PropertyInfo property)
	{
		this.target = target;
		this.propertyInfo = property;
		this.Value = this.getter();
	}

	// Token: 0x060041CE RID: 16846 RVA: 0x000FDCF8 File Offset: 0x000FBEF8
	private object getter()
	{
		if (this.propertyInfo != null)
		{
			return this.getPropertyValue();
		}
		return this.getFieldValue();
	}

	// Token: 0x060041CF RID: 16847 RVA: 0x000FDD14 File Offset: 0x000FBF14
	private void setter(object value)
	{
		if (this.propertyInfo != null)
		{
			this.setPropertyValue(value);
		}
		else
		{
			this.setFieldValue(value);
		}
	}

	// Token: 0x060041D0 RID: 16848 RVA: 0x000FDD34 File Offset: 0x000FBF34
	private object getPropertyValue()
	{
		if (this.propertyGetter == null)
		{
			this.propertyGetter = this.propertyInfo.GetGetMethod();
			if (this.propertyGetter == null)
			{
				throw new InvalidOperationException("Cannot read property: " + this.propertyInfo);
			}
		}
		return this.propertyGetter.Invoke(this.target, null);
	}

	// Token: 0x060041D1 RID: 16849 RVA: 0x000FDD90 File Offset: 0x000FBF90
	private void setPropertyValue(object value)
	{
		MethodInfo setMethod = this.propertyInfo.GetSetMethod();
		if (!this.propertyInfo.CanWrite || setMethod == null)
		{
			return;
		}
		Type propertyType = this.propertyInfo.PropertyType;
		if (value == null || propertyType.IsAssignableFrom(value.GetType()))
		{
			this.propertyInfo.SetValue(this.target, value, null);
		}
		else
		{
			object value2 = Convert.ChangeType(value, propertyType);
			this.propertyInfo.SetValue(this.target, value2, null);
		}
	}

	// Token: 0x060041D2 RID: 16850 RVA: 0x000FDE18 File Offset: 0x000FC018
	private void setFieldValue(object value)
	{
		if (this.fieldInfo.IsLiteral)
		{
			return;
		}
		Type fieldType = this.fieldInfo.FieldType;
		if (value == null || fieldType.IsAssignableFrom(value.GetType()))
		{
			this.fieldInfo.SetValue(this.target, value);
		}
		else
		{
			object value2 = Convert.ChangeType(value, fieldType);
			this.fieldInfo.SetValue(this.target, value2);
		}
	}

	// Token: 0x060041D3 RID: 16851 RVA: 0x000FDE8C File Offset: 0x000FC08C
	private void setFieldValueNOP(object value)
	{
	}

	// Token: 0x060041D4 RID: 16852 RVA: 0x000FDE90 File Offset: 0x000FC090
	private object getFieldValue()
	{
		return this.fieldInfo.GetValue(this.target);
	}

	// Token: 0x040022A6 RID: 8870
	private object lastValue;

	// Token: 0x040022A7 RID: 8871
	private bool hasChanged;

	// Token: 0x040022A8 RID: 8872
	private object target;

	// Token: 0x040022A9 RID: 8873
	private FieldInfo fieldInfo;

	// Token: 0x040022AA RID: 8874
	private PropertyInfo propertyInfo;

	// Token: 0x040022AB RID: 8875
	private MethodInfo propertyGetter;

	// Token: 0x020008E0 RID: 2272
	// (Invoke) Token: 0x06004D58 RID: 19800
	private delegate object ValueGetter();

	// Token: 0x020008E1 RID: 2273
	// (Invoke) Token: 0x06004D5C RID: 19804
	private delegate void ValueSetter(object value);
}
