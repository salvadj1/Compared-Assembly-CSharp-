using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000709 RID: 1801
[AddComponentMenu("Daikon Forge/Data Binding/Expression Binding")]
[Serializable]
public class dfExpressionPropertyBinding : MonoBehaviour, IDataBindingComponent
{
	// Token: 0x17000CF1 RID: 3313
	// (get) Token: 0x060041BB RID: 16827 RVA: 0x000FD7B8 File Offset: 0x000FB9B8
	// (set) Token: 0x060041BC RID: 16828 RVA: 0x000FD7C0 File Offset: 0x000FB9C0
	public string Expression
	{
		get
		{
			return this.expression;
		}
		set
		{
			if (!string.Equals(value, this.expression))
			{
				this.Unbind();
				this.expression = value;
			}
		}
	}

	// Token: 0x060041BD RID: 16829 RVA: 0x000FD7E0 File Offset: 0x000FB9E0
	public void OnDisable()
	{
		this.Unbind();
	}

	// Token: 0x060041BE RID: 16830 RVA: 0x000FD7E8 File Offset: 0x000FB9E8
	public void Update()
	{
		if (this.isBound)
		{
			this.evaluate();
		}
		else
		{
			bool flag = this.DataSource != null && !string.IsNullOrEmpty(this.expression) && this.DataTarget.IsValid;
			if (flag)
			{
				this.Bind();
			}
		}
	}

	// Token: 0x060041BF RID: 16831 RVA: 0x000FD848 File Offset: 0x000FBA48
	public void Unbind()
	{
		if (!this.isBound)
		{
			return;
		}
		this.compiledExpression = null;
		this.targetProperty = null;
		this.isBound = false;
	}

	// Token: 0x060041C0 RID: 16832 RVA: 0x000FD86C File Offset: 0x000FBA6C
	public void Bind()
	{
		if (this.isBound)
		{
			return;
		}
		if (this.DataSource is dfDataObjectProxy && ((dfDataObjectProxy)this.DataSource).Data == null)
		{
			return;
		}
		dfScriptEngineSettings dfScriptEngineSettings = new dfScriptEngineSettings
		{
			Constants = new Dictionary<string, object>
			{
				{
					"Application",
					typeof(Application)
				},
				{
					"Color",
					typeof(Color)
				},
				{
					"Color32",
					typeof(Color32)
				},
				{
					"Random",
					typeof(Random)
				},
				{
					"Time",
					typeof(Time)
				},
				{
					"ScriptableObject",
					typeof(ScriptableObject)
				},
				{
					"Vector2",
					typeof(Vector2)
				},
				{
					"Vector3",
					typeof(Vector3)
				},
				{
					"Vector4",
					typeof(Vector4)
				},
				{
					"Quaternion",
					typeof(Quaternion)
				},
				{
					"Matrix",
					typeof(Matrix4x4)
				},
				{
					"Mathf",
					typeof(Mathf)
				}
			}
		};
		if (this.DataSource is dfDataObjectProxy)
		{
			dfDataObjectProxy dfDataObjectProxy = this.DataSource as dfDataObjectProxy;
			dfScriptEngineSettings.AddVariable(new dfScriptVariable("source", null, dfDataObjectProxy.DataType));
		}
		else
		{
			dfScriptEngineSettings.AddVariable(new dfScriptVariable("source", this.DataSource));
		}
		this.compiledExpression = dfScriptEngine.CompileExpression(this.expression, dfScriptEngineSettings);
		this.targetProperty = this.DataTarget.GetProperty();
		this.isBound = (this.compiledExpression != null && this.targetProperty != null);
	}

	// Token: 0x060041C1 RID: 16833 RVA: 0x000FDA50 File Offset: 0x000FBC50
	private void evaluate()
	{
		try
		{
			object obj = this.DataSource;
			if (obj is dfDataObjectProxy)
			{
				obj = ((dfDataObjectProxy)obj).Data;
			}
			object value = this.compiledExpression.DynamicInvoke(new object[]
			{
				obj
			});
			this.targetProperty.Value = value;
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
	}

	// Token: 0x060041C2 RID: 16834 RVA: 0x000FDACC File Offset: 0x000FBCCC
	public override string ToString()
	{
		string arg = (this.DataTarget == null || !(this.DataTarget.Component != null)) ? "[null]" : this.DataTarget.Component.GetType().Name;
		string arg2 = (this.DataTarget == null || string.IsNullOrEmpty(this.DataTarget.MemberName)) ? "[null]" : this.DataTarget.MemberName;
		return string.Format("Bind [expression] -> {0}.{1}", arg, arg2);
	}

	// Token: 0x040022A0 RID: 8864
	public Component DataSource;

	// Token: 0x040022A1 RID: 8865
	public dfComponentMemberInfo DataTarget;

	// Token: 0x040022A2 RID: 8866
	[SerializeField]
	protected string expression;

	// Token: 0x040022A3 RID: 8867
	private Delegate compiledExpression;

	// Token: 0x040022A4 RID: 8868
	private dfObservableProperty targetProperty;

	// Token: 0x040022A5 RID: 8869
	private bool isBound;
}
