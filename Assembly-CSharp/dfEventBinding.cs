using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x020007DD RID: 2013
[AddComponentMenu("Daikon Forge/Data Binding/Event Binding")]
[Serializable]
public class dfEventBinding : MonoBehaviour, global::IDataBindingComponent
{
	// Token: 0x060045CB RID: 17867 RVA: 0x00106024 File Offset: 0x00104224
	public void OnEnable()
	{
		if (this.DataSource != null && !this.isBound && this.DataSource.IsValid && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x060045CC RID: 17868 RVA: 0x00106070 File Offset: 0x00104270
	public void Start()
	{
		if (this.DataSource != null && !this.isBound && this.DataSource.IsValid && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x060045CD RID: 17869 RVA: 0x001060BC File Offset: 0x001042BC
	public void OnDisable()
	{
		this.Unbind();
	}

	// Token: 0x060045CE RID: 17870 RVA: 0x001060C4 File Offset: 0x001042C4
	public void Bind()
	{
		if (this.isBound || this.DataSource == null)
		{
			return;
		}
		if (!this.DataSource.IsValid || !this.DataTarget.IsValid)
		{
			Debug.LogError(string.Format("Invalid event binding configuration - Source:{0}, Target:{1}", this.DataSource, this.DataTarget));
			return;
		}
		this.sourceComponent = this.DataSource.Component;
		this.targetComponent = this.DataTarget.Component;
		MethodInfo method = this.DataTarget.GetMethod();
		if (method == null)
		{
			Debug.LogError("Event handler not found: " + this.targetComponent.GetType().Name + "." + this.DataTarget.MemberName);
			return;
		}
		this.eventField = this.getField(this.sourceComponent, this.DataSource.MemberName);
		if (this.eventField == null)
		{
			Debug.LogError("Event definition not found: " + this.sourceComponent.GetType().Name + "." + this.DataSource.MemberName);
			return;
		}
		try
		{
			MethodInfo method2 = this.eventField.FieldType.GetMethod("Invoke");
			ParameterInfo[] parameters = method2.GetParameters();
			ParameterInfo[] parameters2 = method.GetParameters();
			if (parameters.Length == parameters2.Length)
			{
				this.eventDelegate = Delegate.CreateDelegate(this.eventField.FieldType, this.targetComponent, method, true);
			}
			else
			{
				if (parameters.Length <= 0 || parameters2.Length != 0)
				{
					base.enabled = false;
					throw new InvalidCastException("Event signature mismatch: " + method);
				}
				this.eventDelegate = this.createEventProxyDelegate(this.targetComponent, this.eventField.FieldType, parameters, method);
			}
		}
		catch (Exception ex)
		{
			base.enabled = false;
			Debug.LogError("Event binding failed - Failed to create event handler: " + ex.ToString());
			return;
		}
		Delegate value = Delegate.Combine(this.eventDelegate, (Delegate)this.eventField.GetValue(this.sourceComponent));
		this.eventField.SetValue(this.sourceComponent, value);
		this.isBound = true;
	}

	// Token: 0x060045CF RID: 17871 RVA: 0x00106308 File Offset: 0x00104508
	public void Unbind()
	{
		if (!this.isBound)
		{
			return;
		}
		this.isBound = false;
		Delegate source = (Delegate)this.eventField.GetValue(this.sourceComponent);
		Delegate value = Delegate.Remove(source, this.eventDelegate);
		this.eventField.SetValue(this.sourceComponent, value);
		this.eventField = null;
		this.eventDelegate = null;
		this.handlerProxy = null;
		this.sourceComponent = null;
		this.targetComponent = null;
	}

	// Token: 0x060045D0 RID: 17872 RVA: 0x00106384 File Offset: 0x00104584
	public override string ToString()
	{
		string text = (this.DataSource == null || !(this.DataSource.Component != null)) ? "[null]" : this.DataSource.Component.GetType().Name;
		string text2 = (this.DataSource == null || string.IsNullOrEmpty(this.DataSource.MemberName)) ? "[null]" : this.DataSource.MemberName;
		string text3 = (this.DataTarget == null || !(this.DataTarget.Component != null)) ? "[null]" : this.DataTarget.Component.GetType().Name;
		string text4 = (this.DataTarget == null || string.IsNullOrEmpty(this.DataTarget.MemberName)) ? "[null]" : this.DataTarget.MemberName;
		return string.Format("Bind {0}.{1} -> {2}.{3}", new object[]
		{
			text,
			text2,
			text3,
			text4
		});
	}

	// Token: 0x060045D1 RID: 17873 RVA: 0x001064A0 File Offset: 0x001046A0
	[global::dfEventProxy]
	private void MouseEventProxy(global::dfControl control, global::dfMouseEventArgs mouseEvent)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060045D2 RID: 17874 RVA: 0x001064A8 File Offset: 0x001046A8
	[global::dfEventProxy]
	private void KeyEventProxy(global::dfControl control, global::dfKeyEventArgs keyEvent)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060045D3 RID: 17875 RVA: 0x001064B0 File Offset: 0x001046B0
	[global::dfEventProxy]
	private void DragEventProxy(global::dfControl control, global::dfDragEventArgs dragEvent)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060045D4 RID: 17876 RVA: 0x001064B8 File Offset: 0x001046B8
	[global::dfEventProxy]
	private void ChildControlEventProxy(global::dfControl container, global::dfControl child)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060045D5 RID: 17877 RVA: 0x001064C0 File Offset: 0x001046C0
	[global::dfEventProxy]
	private void FocusEventProxy(global::dfControl control, global::dfFocusEventArgs args)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060045D6 RID: 17878 RVA: 0x001064C8 File Offset: 0x001046C8
	[global::dfEventProxy]
	private void PropertyChangedProxy(global::dfControl control, int value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060045D7 RID: 17879 RVA: 0x001064D0 File Offset: 0x001046D0
	[global::dfEventProxy]
	private void PropertyChangedProxy(global::dfControl control, float value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060045D8 RID: 17880 RVA: 0x001064D8 File Offset: 0x001046D8
	[global::dfEventProxy]
	private void PropertyChangedProxy(global::dfControl control, bool value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060045D9 RID: 17881 RVA: 0x001064E0 File Offset: 0x001046E0
	[global::dfEventProxy]
	private void PropertyChangedProxy(global::dfControl control, string value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060045DA RID: 17882 RVA: 0x001064E8 File Offset: 0x001046E8
	[global::dfEventProxy]
	private void PropertyChangedProxy(global::dfControl control, Vector2 value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060045DB RID: 17883 RVA: 0x001064F0 File Offset: 0x001046F0
	[global::dfEventProxy]
	private void PropertyChangedProxy(global::dfControl control, Vector3 value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060045DC RID: 17884 RVA: 0x001064F8 File Offset: 0x001046F8
	[global::dfEventProxy]
	private void PropertyChangedProxy(global::dfControl control, Vector4 value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060045DD RID: 17885 RVA: 0x00106500 File Offset: 0x00104700
	[global::dfEventProxy]
	private void PropertyChangedProxy(global::dfControl control, Quaternion value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060045DE RID: 17886 RVA: 0x00106508 File Offset: 0x00104708
	[global::dfEventProxy]
	private void PropertyChangedProxy(global::dfControl control, global::dfButton.ButtonState value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060045DF RID: 17887 RVA: 0x00106510 File Offset: 0x00104710
	[global::dfEventProxy]
	private void PropertyChangedProxy(global::dfControl control, global::dfPivotPoint value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060045E0 RID: 17888 RVA: 0x00106518 File Offset: 0x00104718
	[global::dfEventProxy]
	private void PropertyChangedProxy(global::dfControl control, Texture2D value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060045E1 RID: 17889 RVA: 0x00106520 File Offset: 0x00104720
	[global::dfEventProxy]
	private void PropertyChangedProxy(global::dfControl control, Material value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060045E2 RID: 17890 RVA: 0x00106528 File Offset: 0x00104728
	private void callProxyEventHandler()
	{
		if (this.handlerProxy != null)
		{
			this.handlerProxy.Invoke(this.targetComponent, null);
		}
	}

	// Token: 0x060045E3 RID: 17891 RVA: 0x00106548 File Offset: 0x00104748
	private FieldInfo getField(Component sourceComponent, string fieldName)
	{
		return (from f in sourceComponent.GetType().GetAllFields()
		where f.Name == fieldName
		select f).FirstOrDefault<FieldInfo>();
	}

	// Token: 0x060045E4 RID: 17892 RVA: 0x00106584 File Offset: 0x00104784
	private Delegate createEventProxyDelegate(object target, Type delegateType, ParameterInfo[] eventParams, MethodInfo eventHandler)
	{
		MethodInfo methodInfo = (from m in typeof(global::dfEventBinding).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
		where m.IsDefined(typeof(global::dfEventProxyAttribute), true) && this.signatureIsCompatible(eventParams, m.GetParameters())
		select m).FirstOrDefault<MethodInfo>();
		if (methodInfo == null)
		{
			return null;
		}
		this.handlerProxy = eventHandler;
		return Delegate.CreateDelegate(delegateType, this, methodInfo, true);
	}

	// Token: 0x060045E5 RID: 17893 RVA: 0x001065E8 File Offset: 0x001047E8
	private bool signatureIsCompatible(ParameterInfo[] lhs, ParameterInfo[] rhs)
	{
		if (lhs == null || rhs == null)
		{
			return false;
		}
		if (lhs.Length != rhs.Length)
		{
			return false;
		}
		for (int i = 0; i < lhs.Length; i++)
		{
			if (!this.areTypesCompatible(lhs[i], rhs[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060045E6 RID: 17894 RVA: 0x00106638 File Offset: 0x00104838
	private bool areTypesCompatible(ParameterInfo lhs, ParameterInfo rhs)
	{
		return lhs.ParameterType.Equals(rhs.ParameterType) || lhs.ParameterType.IsAssignableFrom(rhs.ParameterType);
	}

	// Token: 0x040024A9 RID: 9385
	public global::dfComponentMemberInfo DataSource;

	// Token: 0x040024AA RID: 9386
	public global::dfComponentMemberInfo DataTarget;

	// Token: 0x040024AB RID: 9387
	private bool isBound;

	// Token: 0x040024AC RID: 9388
	private Component sourceComponent;

	// Token: 0x040024AD RID: 9389
	private Component targetComponent;

	// Token: 0x040024AE RID: 9390
	private FieldInfo eventField;

	// Token: 0x040024AF RID: 9391
	private Delegate eventDelegate;

	// Token: 0x040024B0 RID: 9392
	private MethodInfo handlerProxy;
}
