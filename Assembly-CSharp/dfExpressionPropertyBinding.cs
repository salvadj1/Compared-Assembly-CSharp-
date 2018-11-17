using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007E1 RID: 2017
[AddComponentMenu("Daikon Forge/Data Binding/Expression Binding")]
[Serializable]
public class dfExpressionPropertyBinding : MonoBehaviour, global::IDataBindingComponent
{
	// Token: 0x17000D79 RID: 3449
	// (get) Token: 0x060045ED RID: 17901 RVA: 0x001066EC File Offset: 0x001048EC
	// (set) Token: 0x060045EE RID: 17902 RVA: 0x001066F4 File Offset: 0x001048F4
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

	// Token: 0x060045EF RID: 17903 RVA: 0x00106714 File Offset: 0x00104914
	public void OnDisable()
	{
		this.Unbind();
	}

	// Token: 0x060045F0 RID: 17904 RVA: 0x0010671C File Offset: 0x0010491C
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

	// Token: 0x060045F1 RID: 17905 RVA: 0x0010677C File Offset: 0x0010497C
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

	// Token: 0x060045F2 RID: 17906 RVA: 0x001067A0 File Offset: 0x001049A0
	public void Bind()
	{
		if (this.isBound)
		{
			return;
		}
		if (this.DataSource is global::dfDataObjectProxy && ((global::dfDataObjectProxy)this.DataSource).Data == null)
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
		if (this.DataSource is global::dfDataObjectProxy)
		{
			global::dfDataObjectProxy dfDataObjectProxy = this.DataSource as global::dfDataObjectProxy;
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

	// Token: 0x060045F3 RID: 17907 RVA: 0x00106984 File Offset: 0x00104B84
	private void evaluate()
	{
		try
		{
			object obj = this.DataSource;
			if (obj is global::dfDataObjectProxy)
			{
				obj = ((global::dfDataObjectProxy)obj).Data;
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

	// Token: 0x060045F4 RID: 17908 RVA: 0x00106A00 File Offset: 0x00104C00
	public override string ToString()
	{
		string arg = (this.DataTarget == null || !(this.DataTarget.Component != null)) ? "[null]" : this.DataTarget.Component.GetType().Name;
		string arg2 = (this.DataTarget == null || string.IsNullOrEmpty(this.DataTarget.MemberName)) ? "[null]" : this.DataTarget.MemberName;
		return string.Format("Bind [expression] -> {0}.{1}", arg, arg2);
	}

	// Token: 0x040024B4 RID: 9396
	public Component DataSource;

	// Token: 0x040024B5 RID: 9397
	public global::dfComponentMemberInfo DataTarget;

	// Token: 0x040024B6 RID: 9398
	[SerializeField]
	protected string expression;

	// Token: 0x040024B7 RID: 9399
	private Delegate compiledExpression;

	// Token: 0x040024B8 RID: 9400
	private global::dfObservableProperty targetProperty;

	// Token: 0x040024B9 RID: 9401
	private bool isBound;
}
