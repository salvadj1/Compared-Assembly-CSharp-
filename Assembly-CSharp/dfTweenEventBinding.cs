using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

// Token: 0x02000741 RID: 1857
[AddComponentMenu("Daikon Forge/Tweens/Tween Event Binding")]
[Serializable]
public class dfTweenEventBinding : MonoBehaviour
{
	// Token: 0x060043E3 RID: 17379 RVA: 0x00107340 File Offset: 0x00105540
	private void OnEnable()
	{
		if (this.isValid())
		{
			this.Bind();
		}
	}

	// Token: 0x060043E4 RID: 17380 RVA: 0x00107354 File Offset: 0x00105554
	private void Start()
	{
		if (this.isValid())
		{
			this.Bind();
		}
	}

	// Token: 0x060043E5 RID: 17381 RVA: 0x00107368 File Offset: 0x00105568
	private void OnDisable()
	{
		this.Unbind();
	}

	// Token: 0x060043E6 RID: 17382 RVA: 0x00107370 File Offset: 0x00105570
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

	// Token: 0x060043E7 RID: 17383 RVA: 0x00107424 File Offset: 0x00105624
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

	// Token: 0x060043E8 RID: 17384 RVA: 0x001074C8 File Offset: 0x001056C8
	private bool isValid()
	{
		if (this.Tween == null || !(this.Tween is dfTweenComponentBase))
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

	// Token: 0x060043E9 RID: 17385 RVA: 0x001075BC File Offset: 0x001057BC
	private void unbindEvent(FieldInfo eventField, Delegate eventDelegate)
	{
		Delegate source = (Delegate)eventField.GetValue(this.EventSource);
		Delegate value = Delegate.Remove(source, eventDelegate);
		eventField.SetValue(this.EventSource, value);
	}

	// Token: 0x060043EA RID: 17386 RVA: 0x001075F0 File Offset: 0x001057F0
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

	// Token: 0x060043EB RID: 17387 RVA: 0x00107754 File Offset: 0x00105954
	private FieldInfo getField(Type type, string fieldName)
	{
		return (from f in type.GetAllFields()
		where f.Name == fieldName
		select f).FirstOrDefault<FieldInfo>();
	}

	// Token: 0x060043EC RID: 17388 RVA: 0x0010778C File Offset: 0x0010598C
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

	// Token: 0x040023B5 RID: 9141
	public Component Tween;

	// Token: 0x040023B6 RID: 9142
	public Component EventSource;

	// Token: 0x040023B7 RID: 9143
	public string StartEvent;

	// Token: 0x040023B8 RID: 9144
	public string StopEvent;

	// Token: 0x040023B9 RID: 9145
	public string ResetEvent;

	// Token: 0x040023BA RID: 9146
	private bool isBound;

	// Token: 0x040023BB RID: 9147
	private FieldInfo startEventField;

	// Token: 0x040023BC RID: 9148
	private FieldInfo stopEventField;

	// Token: 0x040023BD RID: 9149
	private FieldInfo resetEventField;

	// Token: 0x040023BE RID: 9150
	private Delegate startEventHandler;

	// Token: 0x040023BF RID: 9151
	private Delegate stopEventHandler;

	// Token: 0x040023C0 RID: 9152
	private Delegate resetEventHandler;
}
