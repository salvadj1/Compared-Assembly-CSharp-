using System;
using System.Linq;
using System.Reflection;

// Token: 0x020007E2 RID: 2018
public class dfObservableProperty : global::IObservableValue
{
	// Token: 0x060045F5 RID: 17909 RVA: 0x00106A90 File Offset: 0x00104C90
	internal dfObservableProperty(object target, string memberName)
	{
		MemberInfo memberInfo = target.GetType().GetMember(memberName, BindingFlags.Instance | BindingFlags.Public).FirstOrDefault<MemberInfo>();
		if (memberInfo == null)
		{
			throw new ArgumentException("Invalid property or field name: " + memberName, "memberName");
		}
		this.initMember(target, memberInfo);
	}

	// Token: 0x060045F6 RID: 17910 RVA: 0x00106ADC File Offset: 0x00104CDC
	internal dfObservableProperty(object target, FieldInfo field)
	{
		this.initField(target, field);
	}

	// Token: 0x060045F7 RID: 17911 RVA: 0x00106AEC File Offset: 0x00104CEC
	internal dfObservableProperty(object target, PropertyInfo property)
	{
		this.initProperty(target, property);
	}

	// Token: 0x060045F8 RID: 17912 RVA: 0x00106AFC File Offset: 0x00104CFC
	internal dfObservableProperty(object target, MemberInfo member)
	{
		this.initMember(target, member);
	}

	// Token: 0x17000D7A RID: 3450
	// (get) Token: 0x060045F9 RID: 17913 RVA: 0x00106B0C File Offset: 0x00104D0C
	// (set) Token: 0x060045FA RID: 17914 RVA: 0x00106B14 File Offset: 0x00104D14
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

	// Token: 0x17000D7B RID: 3451
	// (get) Token: 0x060045FB RID: 17915 RVA: 0x00106B2C File Offset: 0x00104D2C
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

	// Token: 0x060045FC RID: 17916 RVA: 0x00106BA4 File Offset: 0x00104DA4
	public void ClearChangedFlag()
	{
		this.hasChanged = false;
		this.lastValue = this.getter();
	}

	// Token: 0x060045FD RID: 17917 RVA: 0x00106BBC File Offset: 0x00104DBC
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

	// Token: 0x060045FE RID: 17918 RVA: 0x00106BF4 File Offset: 0x00104DF4
	private void initField(object target, FieldInfo field)
	{
		this.target = target;
		this.fieldInfo = field;
		this.Value = this.getter();
	}

	// Token: 0x060045FF RID: 17919 RVA: 0x00106C10 File Offset: 0x00104E10
	private void initProperty(object target, PropertyInfo property)
	{
		this.target = target;
		this.propertyInfo = property;
		this.Value = this.getter();
	}

	// Token: 0x06004600 RID: 17920 RVA: 0x00106C2C File Offset: 0x00104E2C
	private object getter()
	{
		if (this.propertyInfo != null)
		{
			return this.getPropertyValue();
		}
		return this.getFieldValue();
	}

	// Token: 0x06004601 RID: 17921 RVA: 0x00106C48 File Offset: 0x00104E48
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

	// Token: 0x06004602 RID: 17922 RVA: 0x00106C68 File Offset: 0x00104E68
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

	// Token: 0x06004603 RID: 17923 RVA: 0x00106CC4 File Offset: 0x00104EC4
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

	// Token: 0x06004604 RID: 17924 RVA: 0x00106D4C File Offset: 0x00104F4C
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

	// Token: 0x06004605 RID: 17925 RVA: 0x00106DC0 File Offset: 0x00104FC0
	private void setFieldValueNOP(object value)
	{
	}

	// Token: 0x06004606 RID: 17926 RVA: 0x00106DC4 File Offset: 0x00104FC4
	private object getFieldValue()
	{
		return this.fieldInfo.GetValue(this.target);
	}

	// Token: 0x040024BA RID: 9402
	private object lastValue;

	// Token: 0x040024BB RID: 9403
	private bool hasChanged;

	// Token: 0x040024BC RID: 9404
	private object target;

	// Token: 0x040024BD RID: 9405
	private FieldInfo fieldInfo;

	// Token: 0x040024BE RID: 9406
	private PropertyInfo propertyInfo;

	// Token: 0x040024BF RID: 9407
	private MethodInfo propertyGetter;

	// Token: 0x020007E3 RID: 2019
	// (Invoke) Token: 0x06004608 RID: 17928
	private delegate object ValueGetter();

	// Token: 0x020007E4 RID: 2020
	// (Invoke) Token: 0x0600460C RID: 17932
	private delegate void ValueSetter(object value);
}
