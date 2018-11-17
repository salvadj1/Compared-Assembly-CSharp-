using System;
using UnityEngine;

// Token: 0x0200070C RID: 1804
[AddComponentMenu("Daikon Forge/Data Binding/Proxy Property Binding")]
[Serializable]
public class dfProxyPropertyBinding : MonoBehaviour, IDataBindingComponent
{
	// Token: 0x060041DE RID: 16862 RVA: 0x000FE1B4 File Offset: 0x000FC3B4
	public void Awake()
	{
	}

	// Token: 0x060041DF RID: 16863 RVA: 0x000FE1B8 File Offset: 0x000FC3B8
	public void OnEnable()
	{
		if (!this.isBound && this.IsDataSourceValid() && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x060041E0 RID: 16864 RVA: 0x000FE1F4 File Offset: 0x000FC3F4
	public void Start()
	{
		if (!this.isBound && this.IsDataSourceValid() && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x060041E1 RID: 16865 RVA: 0x000FE230 File Offset: 0x000FC430
	public void OnDisable()
	{
		this.Unbind();
	}

	// Token: 0x060041E2 RID: 16866 RVA: 0x000FE238 File Offset: 0x000FC438
	public void Update()
	{
		if (this.sourceProperty == null || this.targetProperty == null)
		{
			return;
		}
		if (this.sourceProperty.HasChanged)
		{
			this.targetProperty.Value = this.sourceProperty.Value;
			this.sourceProperty.ClearChangedFlag();
		}
		else if (this.TwoWay && this.targetProperty.HasChanged)
		{
			this.sourceProperty.Value = this.targetProperty.Value;
			this.targetProperty.ClearChangedFlag();
		}
	}

	// Token: 0x060041E3 RID: 16867 RVA: 0x000FE2D0 File Offset: 0x000FC4D0
	public void Bind()
	{
		if (this.isBound)
		{
			return;
		}
		if (!this.IsDataSourceValid())
		{
			Debug.LogError(string.Format("Invalid data binding configuration - Source:{0}, Target:{1}", this.DataSource, this.DataTarget));
			return;
		}
		if (!this.DataTarget.IsValid)
		{
			Debug.LogError(string.Format("Invalid data binding configuration - Source:{0}, Target:{1}", this.DataSource, this.DataTarget));
			return;
		}
		dfDataObjectProxy dfDataObjectProxy = this.DataSource.Component as dfDataObjectProxy;
		this.sourceProperty = dfDataObjectProxy.GetProperty(this.DataSource.MemberName);
		this.targetProperty = this.DataTarget.GetProperty();
		this.isBound = (this.sourceProperty != null && this.targetProperty != null);
		if (this.isBound)
		{
			this.targetProperty.Value = this.sourceProperty.Value;
		}
		this.attachEvent();
	}

	// Token: 0x060041E4 RID: 16868 RVA: 0x000FE3BC File Offset: 0x000FC5BC
	public void Unbind()
	{
		if (!this.isBound)
		{
			return;
		}
		this.detachEvent();
		this.sourceProperty = null;
		this.targetProperty = null;
		this.isBound = false;
	}

	// Token: 0x060041E5 RID: 16869 RVA: 0x000FE3E8 File Offset: 0x000FC5E8
	private bool IsDataSourceValid()
	{
		return this.DataSource != null || this.DataSource.Component != null || !string.IsNullOrEmpty(this.DataSource.MemberName) || (this.DataSource.Component as dfDataObjectProxy).Data != null;
	}

	// Token: 0x060041E6 RID: 16870 RVA: 0x000FE44C File Offset: 0x000FC64C
	private void attachEvent()
	{
		if (this.eventsAttached)
		{
			return;
		}
		this.eventsAttached = true;
		dfDataObjectProxy dfDataObjectProxy = this.DataSource.Component as dfDataObjectProxy;
		if (dfDataObjectProxy != null)
		{
			dfDataObjectProxy.DataChanged += this.handle_DataChanged;
		}
	}

	// Token: 0x060041E7 RID: 16871 RVA: 0x000FE49C File Offset: 0x000FC69C
	private void detachEvent()
	{
		if (!this.eventsAttached)
		{
			return;
		}
		this.eventsAttached = false;
		dfDataObjectProxy dfDataObjectProxy = this.DataSource.Component as dfDataObjectProxy;
		if (dfDataObjectProxy != null)
		{
			dfDataObjectProxy.DataChanged -= this.handle_DataChanged;
		}
	}

	// Token: 0x060041E8 RID: 16872 RVA: 0x000FE4EC File Offset: 0x000FC6EC
	private void handle_DataChanged(object data)
	{
		this.Unbind();
		if (this.IsDataSourceValid())
		{
			this.Bind();
		}
	}

	// Token: 0x060041E9 RID: 16873 RVA: 0x000FE508 File Offset: 0x000FC708
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

	// Token: 0x040022B2 RID: 8882
	public dfComponentMemberInfo DataSource;

	// Token: 0x040022B3 RID: 8883
	public dfComponentMemberInfo DataTarget;

	// Token: 0x040022B4 RID: 8884
	public bool TwoWay;

	// Token: 0x040022B5 RID: 8885
	private dfObservableProperty sourceProperty;

	// Token: 0x040022B6 RID: 8886
	private dfObservableProperty targetProperty;

	// Token: 0x040022B7 RID: 8887
	private bool isBound;

	// Token: 0x040022B8 RID: 8888
	private bool eventsAttached;
}
