using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x02000707 RID: 1799
[AddComponentMenu("Daikon Forge/Data Binding/Event Binding")]
[Serializable]
public class dfEventBinding : MonoBehaviour, IDataBindingComponent
{
	// Token: 0x0600419D RID: 16797 RVA: 0x000FD154 File Offset: 0x000FB354
	public void OnEnable()
	{
		if (this.DataSource != null && !this.isBound && this.DataSource.IsValid && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x0600419E RID: 16798 RVA: 0x000FD1A0 File Offset: 0x000FB3A0
	public void Start()
	{
		if (this.DataSource != null && !this.isBound && this.DataSource.IsValid && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x0600419F RID: 16799 RVA: 0x000FD1EC File Offset: 0x000FB3EC
	public void OnDisable()
	{
		this.Unbind();
	}

	// Token: 0x060041A0 RID: 16800 RVA: 0x000FD1F4 File Offset: 0x000FB3F4
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

	// Token: 0x060041A1 RID: 16801 RVA: 0x000FD438 File Offset: 0x000FB638
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

	// Token: 0x060041A2 RID: 16802 RVA: 0x000FD4B4 File Offset: 0x000FB6B4
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

	// Token: 0x060041A3 RID: 16803 RVA: 0x000FD5D0 File Offset: 0x000FB7D0
	[dfEventProxy]
	private void MouseEventProxy(dfControl control, dfMouseEventArgs mouseEvent)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060041A4 RID: 16804 RVA: 0x000FD5D8 File Offset: 0x000FB7D8
	[dfEventProxy]
	private void KeyEventProxy(dfControl control, dfKeyEventArgs keyEvent)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060041A5 RID: 16805 RVA: 0x000FD5E0 File Offset: 0x000FB7E0
	[dfEventProxy]
	private void DragEventProxy(dfControl control, dfDragEventArgs dragEvent)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060041A6 RID: 16806 RVA: 0x000FD5E8 File Offset: 0x000FB7E8
	[dfEventProxy]
	private void ChildControlEventProxy(dfControl container, dfControl child)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060041A7 RID: 16807 RVA: 0x000FD5F0 File Offset: 0x000FB7F0
	[dfEventProxy]
	private void FocusEventProxy(dfControl control, dfFocusEventArgs args)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060041A8 RID: 16808 RVA: 0x000FD5F8 File Offset: 0x000FB7F8
	[dfEventProxy]
	private void PropertyChangedProxy(dfControl control, int value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060041A9 RID: 16809 RVA: 0x000FD600 File Offset: 0x000FB800
	[dfEventProxy]
	private void PropertyChangedProxy(dfControl control, float value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060041AA RID: 16810 RVA: 0x000FD608 File Offset: 0x000FB808
	[dfEventProxy]
	private void PropertyChangedProxy(dfControl control, bool value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060041AB RID: 16811 RVA: 0x000FD610 File Offset: 0x000FB810
	[dfEventProxy]
	private void PropertyChangedProxy(dfControl control, string value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060041AC RID: 16812 RVA: 0x000FD618 File Offset: 0x000FB818
	[dfEventProxy]
	private void PropertyChangedProxy(dfControl control, Vector2 value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060041AD RID: 16813 RVA: 0x000FD620 File Offset: 0x000FB820
	[dfEventProxy]
	private void PropertyChangedProxy(dfControl control, Vector3 value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060041AE RID: 16814 RVA: 0x000FD628 File Offset: 0x000FB828
	[dfEventProxy]
	private void PropertyChangedProxy(dfControl control, Vector4 value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060041AF RID: 16815 RVA: 0x000FD630 File Offset: 0x000FB830
	[dfEventProxy]
	private void PropertyChangedProxy(dfControl control, Quaternion value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060041B0 RID: 16816 RVA: 0x000FD638 File Offset: 0x000FB838
	[dfEventProxy]
	private void PropertyChangedProxy(dfControl control, dfButton.ButtonState value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060041B1 RID: 16817 RVA: 0x000FD640 File Offset: 0x000FB840
	[dfEventProxy]
	private void PropertyChangedProxy(dfControl control, dfPivotPoint value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060041B2 RID: 16818 RVA: 0x000FD648 File Offset: 0x000FB848
	[dfEventProxy]
	private void PropertyChangedProxy(dfControl control, Texture2D value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060041B3 RID: 16819 RVA: 0x000FD650 File Offset: 0x000FB850
	[dfEventProxy]
	private void PropertyChangedProxy(dfControl control, Material value)
	{
		this.callProxyEventHandler();
	}

	// Token: 0x060041B4 RID: 16820 RVA: 0x000FD658 File Offset: 0x000FB858
	private void callProxyEventHandler()
	{
		if (this.handlerProxy != null)
		{
			this.handlerProxy.Invoke(this.targetComponent, null);
		}
	}

	// Token: 0x060041B5 RID: 16821 RVA: 0x000FD678 File Offset: 0x000FB878
	private FieldInfo getField(Component sourceComponent, string fieldName)
	{
		return (from f in sourceComponent.GetType().GetAllFields()
		where f.Name == fieldName
		select f).FirstOrDefault<FieldInfo>();
	}

	// Token: 0x060041B6 RID: 16822 RVA: 0x000FD6B4 File Offset: 0x000FB8B4
	private Delegate createEventProxyDelegate(object target, Type delegateType, ParameterInfo[] eventParams, MethodInfo eventHandler)
	{
		MethodInfo methodInfo = (from m in typeof(dfEventBinding).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
		where m.IsDefined(typeof(dfEventProxyAttribute), true) && this.signatureIsCompatible(eventParams, m.GetParameters())
		select m).FirstOrDefault<MethodInfo>();
		if (methodInfo == null)
		{
			return null;
		}
		this.handlerProxy = eventHandler;
		return Delegate.CreateDelegate(delegateType, this, methodInfo, true);
	}

	// Token: 0x060041B7 RID: 16823 RVA: 0x000FD718 File Offset: 0x000FB918
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

	// Token: 0x060041B8 RID: 16824 RVA: 0x000FD768 File Offset: 0x000FB968
	private bool areTypesCompatible(ParameterInfo lhs, ParameterInfo rhs)
	{
		return lhs.ParameterType.Equals(rhs.ParameterType) || lhs.ParameterType.IsAssignableFrom(rhs.ParameterType);
	}

	// Token: 0x04002298 RID: 8856
	public dfComponentMemberInfo DataSource;

	// Token: 0x04002299 RID: 8857
	public dfComponentMemberInfo DataTarget;

	// Token: 0x0400229A RID: 8858
	private bool isBound;

	// Token: 0x0400229B RID: 8859
	private Component sourceComponent;

	// Token: 0x0400229C RID: 8860
	private Component targetComponent;

	// Token: 0x0400229D RID: 8861
	private FieldInfo eventField;

	// Token: 0x0400229E RID: 8862
	private Delegate eventDelegate;

	// Token: 0x0400229F RID: 8863
	private MethodInfo handlerProxy;
}
