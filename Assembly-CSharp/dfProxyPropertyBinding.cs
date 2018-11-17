using System;
using UnityEngine;

// Token: 0x020007E6 RID: 2022
[AddComponentMenu("Daikon Forge/Data Binding/Proxy Property Binding")]
[Serializable]
public class dfProxyPropertyBinding : MonoBehaviour, global::IDataBindingComponent
{
	// Token: 0x06004618 RID: 17944 RVA: 0x001070E8 File Offset: 0x001052E8
	public void Awake()
	{
	}

	// Token: 0x06004619 RID: 17945 RVA: 0x001070EC File Offset: 0x001052EC
	public void OnEnable()
	{
		if (!this.isBound && this.IsDataSourceValid() && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x0600461A RID: 17946 RVA: 0x00107128 File Offset: 0x00105328
	public void Start()
	{
		if (!this.isBound && this.IsDataSourceValid() && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x0600461B RID: 17947 RVA: 0x00107164 File Offset: 0x00105364
	public void OnDisable()
	{
		this.Unbind();
	}

	// Token: 0x0600461C RID: 17948 RVA: 0x0010716C File Offset: 0x0010536C
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

	// Token: 0x0600461D RID: 17949 RVA: 0x00107204 File Offset: 0x00105404
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
		global::dfDataObjectProxy dfDataObjectProxy = this.DataSource.Component as global::dfDataObjectProxy;
		this.sourceProperty = dfDataObjectProxy.GetProperty(this.DataSource.MemberName);
		this.targetProperty = this.DataTarget.GetProperty();
		this.isBound = (this.sourceProperty != null && this.targetProperty != null);
		if (this.isBound)
		{
			this.targetProperty.Value = this.sourceProperty.Value;
		}
		this.attachEvent();
	}

	// Token: 0x0600461E RID: 17950 RVA: 0x001072F0 File Offset: 0x001054F0
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

	// Token: 0x0600461F RID: 17951 RVA: 0x0010731C File Offset: 0x0010551C
	private bool IsDataSourceValid()
	{
		return this.DataSource != null || this.DataSource.Component != null || !string.IsNullOrEmpty(this.DataSource.MemberName) || (this.DataSource.Component as global::dfDataObjectProxy).Data != null;
	}

	// Token: 0x06004620 RID: 17952 RVA: 0x00107380 File Offset: 0x00105580
	private void attachEvent()
	{
		if (this.eventsAttached)
		{
			return;
		}
		this.eventsAttached = true;
		global::dfDataObjectProxy dfDataObjectProxy = this.DataSource.Component as global::dfDataObjectProxy;
		if (dfDataObjectProxy != null)
		{
			dfDataObjectProxy.DataChanged += this.handle_DataChanged;
		}
	}

	// Token: 0x06004621 RID: 17953 RVA: 0x001073D0 File Offset: 0x001055D0
	private void detachEvent()
	{
		if (!this.eventsAttached)
		{
			return;
		}
		this.eventsAttached = false;
		global::dfDataObjectProxy dfDataObjectProxy = this.DataSource.Component as global::dfDataObjectProxy;
		if (dfDataObjectProxy != null)
		{
			dfDataObjectProxy.DataChanged -= this.handle_DataChanged;
		}
	}

	// Token: 0x06004622 RID: 17954 RVA: 0x00107420 File Offset: 0x00105620
	private void handle_DataChanged(object data)
	{
		this.Unbind();
		if (this.IsDataSourceValid())
		{
			this.Bind();
		}
	}

	// Token: 0x06004623 RID: 17955 RVA: 0x0010743C File Offset: 0x0010563C
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

	// Token: 0x040024C6 RID: 9414
	public global::dfComponentMemberInfo DataSource;

	// Token: 0x040024C7 RID: 9415
	public global::dfComponentMemberInfo DataTarget;

	// Token: 0x040024C8 RID: 9416
	public bool TwoWay;

	// Token: 0x040024C9 RID: 9417
	private global::dfObservableProperty sourceProperty;

	// Token: 0x040024CA RID: 9418
	private global::dfObservableProperty targetProperty;

	// Token: 0x040024CB RID: 9419
	private bool isBound;

	// Token: 0x040024CC RID: 9420
	private bool eventsAttached;
}
