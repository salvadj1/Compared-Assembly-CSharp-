using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

// Token: 0x02000820 RID: 2080
[AddComponentMenu("Daikon Forge/Tweens/Tween Event Binding")]
[Serializable]
public class dfTweenEventBinding : MonoBehaviour
{
	// Token: 0x06004835 RID: 18485 RVA: 0x00110A04 File Offset: 0x0010EC04
	private void OnEnable()
	{
		if (this.isValid())
		{
			this.Bind();
		}
	}

	// Token: 0x06004836 RID: 18486 RVA: 0x00110A18 File Offset: 0x0010EC18
	private void Start()
	{
		if (this.isValid())
		{
			this.Bind();
		}
	}

	// Token: 0x06004837 RID: 18487 RVA: 0x00110A2C File Offset: 0x0010EC2C
	private void OnDisable()
	{
		this.Unbind();
	}

	// Token: 0x06004838 RID: 18488 RVA: 0x00110A34 File Offset: 0x0010EC34
	public void Bind()
	{
		if (this.isBound && !this.isValid())
		{
			return;
		}
		this.isBound = true;
		if (!string.IsNullOrEmpty(this.StartEvent))
		{
			this.bindEvent(this.StartEvent, "Play", out this.startEventField, out this.startEventHandler);
		}
		if (!string.IsNullOrEmpty(this.StopEvent))
		{
			this.bindEvent(this.StopEvent, "Stop", out this.stopEventField, out this.stopEventHandler);
		}
		if (!string.IsNullOrEmpty(this.ResetEvent))
		{
			this.bindEvent(this.ResetEvent, "Reset", out this.resetEventField, out this.resetEventHandler);
		}
	}

	// Token: 0x06004839 RID: 18489 RVA: 0x00110AE8 File Offset: 0x0010ECE8
	public void Unbind()
	{
		if (!this.isBound)
		{
			return;
		}
		this.isBound = false;
		if (this.startEventField != null)
		{
			this.unbindEvent(this.startEventField, this.startEventHandler);
			this.startEventField = null;
			this.startEventHandler = null;
		}
		if (this.stopEventField != null)
		{
			this.unbindEvent(this.stopEventField, this.stopEventHandler);
			this.stopEventField = null;
			this.stopEventHandler = null;
		}
		if (this.resetEventField != null)
		{
			this.unbindEvent(this.resetEventField, this.resetEventHandler);
			this.resetEventField = null;
			this.resetEventHandler = null;
		}
	}

	// Token: 0x0600483A RID: 18490 RVA: 0x00110B8C File Offset: 0x0010ED8C
	private bool isValid()
	{
		if (this.Tween == null || !(this.Tween is global::dfTweenComponentBase))
		{
			return false;
		}
		if (this.EventSource == null)
		{
			return false;
		}
		bool flag = string.IsNullOrEmpty(this.StartEvent) && string.IsNullOrEmpty(this.StopEvent) && string.IsNullOrEmpty(this.ResetEvent);
		if (flag)
		{
			return false;
		}
		Type type = this.EventSource.GetType();
		return (string.IsNullOrEmpty(this.StartEvent) || this.getField(type, this.StartEvent) != null) && (string.IsNullOrEmpty(this.StopEvent) || this.getField(type, this.StopEvent) != null) && (string.IsNullOrEmpty(this.ResetEvent) || this.getField(type, this.ResetEvent) != null);
	}

	// Token: 0x0600483B RID: 18491 RVA: 0x00110C80 File Offset: 0x0010EE80
	private void unbindEvent(FieldInfo eventField, Delegate eventDelegate)
	{
		Delegate source = (Delegate)eventField.GetValue(this.EventSource);
		Delegate value = Delegate.Remove(source, eventDelegate);
		eventField.SetValue(this.EventSource, value);
	}

	// Token: 0x0600483C RID: 18492 RVA: 0x00110CB4 File Offset: 0x0010EEB4
	private void bindEvent(string eventName, string handlerName, out FieldInfo eventField, out Delegate eventHandler)
	{
		eventField = null;
		eventHandler = null;
		MethodInfo method = this.Tween.GetType().GetMethod(handlerName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		if (method == null)
		{
			throw new MissingMemberException("Method not found: " + handlerName);
		}
		eventField = this.getField(this.EventSource.GetType(), eventName);
		if (eventField == null)
		{
			throw new MissingMemberException("Event not found: " + eventName);
		}
		try
		{
			MethodInfo method2 = eventField.FieldType.GetMethod("Invoke");
			ParameterInfo[] parameters = method2.GetParameters();
			ParameterInfo[] parameters2 = method.GetParameters();
			if (parameters.Length == parameters2.Length)
			{
				eventHandler = Delegate.CreateDelegate(eventField.FieldType, this.Tween, method, true);
			}
			else
			{
				if (parameters.Length <= 0 || parameters2.Length != 0)
				{
					throw new InvalidCastException("Event signature mismatch: " + eventHandler);
				}
				eventHandler = this.createDynamicWrapper(this.Tween, eventField.FieldType, parameters, method);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("Event binding failed - Failed to create event handler: " + ex.ToString());
			return;
		}
		Delegate value = Delegate.Combine(eventHandler, (Delegate)eventField.GetValue(this.EventSource));
		eventField.SetValue(this.EventSource, value);
	}

	// Token: 0x0600483D RID: 18493 RVA: 0x00110E18 File Offset: 0x0010F018
	private FieldInfo getField(Type type, string fieldName)
	{
		return (from f in type.GetAllFields()
		where f.Name == fieldName
		select f).FirstOrDefault<FieldInfo>();
	}

	// Token: 0x0600483E RID: 18494 RVA: 0x00110E50 File Offset: 0x0010F050
	private Delegate createDynamicWrapper(object target, Type delegateType, ParameterInfo[] eventParams, MethodInfo eventHandler)
	{
		Type[] parameterTypes = new Type[]
		{
			target.GetType()
		}.Concat(from p in eventParams
		select p.ParameterType).ToArray<Type>();
		DynamicMethod dynamicMethod = new DynamicMethod("DynamicEventWrapper_" + eventHandler.Name, typeof(void), parameterTypes);
		ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
		ilgenerator.Emit(OpCodes.Ldarg_0);
		ilgenerator.EmitCall(OpCodes.Callvirt, eventHandler, Type.EmptyTypes);
		ilgenerator.Emit(OpCodes.Ret);
		return dynamicMethod.CreateDelegate(delegateType, target);
	}

	// Token: 0x040025E1 RID: 9697
	public Component Tween;

	// Token: 0x040025E2 RID: 9698
	public Component EventSource;

	// Token: 0x040025E3 RID: 9699
	public string StartEvent;

	// Token: 0x040025E4 RID: 9700
	public string StopEvent;

	// Token: 0x040025E5 RID: 9701
	public string ResetEvent;

	// Token: 0x040025E6 RID: 9702
	private bool isBound;

	// Token: 0x040025E7 RID: 9703
	private FieldInfo startEventField;

	// Token: 0x040025E8 RID: 9704
	private FieldInfo stopEventField;

	// Token: 0x040025E9 RID: 9705
	private FieldInfo resetEventField;

	// Token: 0x040025EA RID: 9706
	private Delegate startEventHandler;

	// Token: 0x040025EB RID: 9707
	private Delegate stopEventHandler;

	// Token: 0x040025EC RID: 9708
	private Delegate resetEventHandler;
}
