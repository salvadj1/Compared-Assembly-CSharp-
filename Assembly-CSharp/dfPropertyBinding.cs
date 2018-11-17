using System;
using UnityEngine;

// Token: 0x020007E5 RID: 2021
[AddComponentMenu("Daikon Forge/Data Binding/Property Binding")]
[Serializable]
public class dfPropertyBinding : MonoBehaviour, global::IDataBindingComponent
{
	// Token: 0x06004610 RID: 17936 RVA: 0x00106DE0 File Offset: 0x00104FE0
	public void OnEnable()
	{
		if (!this.isBound && this.DataSource.IsValid && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x06004611 RID: 17937 RVA: 0x00106E14 File Offset: 0x00105014
	public void Start()
	{
		if (!this.isBound && this.DataSource.IsValid && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x06004612 RID: 17938 RVA: 0x00106E48 File Offset: 0x00105048
	public void OnDisable()
	{
		this.Unbind();
	}

	// Token: 0x06004613 RID: 17939 RVA: 0x00106E50 File Offset: 0x00105050
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

	// Token: 0x06004614 RID: 17940 RVA: 0x00106EE8 File Offset: 0x001050E8
	public void Bind()
	{
		if (this.isBound)
		{
			return;
		}
		if (!this.DataSource.IsValid || !this.DataTarget.IsValid)
		{
			Debug.LogError(string.Format("Invalid data binding configuration - Source:{0}, Target:{1}", this.DataSource, this.DataTarget));
			return;
		}
		this.sourceProperty = this.DataSource.GetProperty();
		this.targetProperty = this.DataTarget.GetProperty();
		this.isBound = (this.sourceProperty != null && this.targetProperty != null);
		if (this.isBound)
		{
			this.targetProperty.Value = this.sourceProperty.Value;
		}
	}

	// Token: 0x06004615 RID: 17941 RVA: 0x00106FA0 File Offset: 0x001051A0
	public void Unbind()
	{
		if (!this.isBound)
		{
			return;
		}
		this.sourceProperty = null;
		this.targetProperty = null;
		this.isBound = false;
	}

	// Token: 0x06004616 RID: 17942 RVA: 0x00106FC4 File Offset: 0x001051C4
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

	// Token: 0x040024C0 RID: 9408
	public global::dfComponentMemberInfo DataSource;

	// Token: 0x040024C1 RID: 9409
	public global::dfComponentMemberInfo DataTarget;

	// Token: 0x040024C2 RID: 9410
	public bool TwoWay;

	// Token: 0x040024C3 RID: 9411
	private global::dfObservableProperty sourceProperty;

	// Token: 0x040024C4 RID: 9412
	private global::dfObservableProperty targetProperty;

	// Token: 0x040024C5 RID: 9413
	private bool isBound;
}
