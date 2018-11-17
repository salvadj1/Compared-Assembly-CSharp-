using System;
using UnityEngine;

// Token: 0x0200070B RID: 1803
[AddComponentMenu("Daikon Forge/Data Binding/Property Binding")]
[Serializable]
public class dfPropertyBinding : MonoBehaviour, IDataBindingComponent
{
	// Token: 0x060041D6 RID: 16854 RVA: 0x000FDEAC File Offset: 0x000FC0AC
	public void OnEnable()
	{
		if (!this.isBound && this.DataSource.IsValid && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x060041D7 RID: 16855 RVA: 0x000FDEE0 File Offset: 0x000FC0E0
	public void Start()
	{
		if (!this.isBound && this.DataSource.IsValid && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x060041D8 RID: 16856 RVA: 0x000FDF14 File Offset: 0x000FC114
	public void OnDisable()
	{
		this.Unbind();
	}

	// Token: 0x060041D9 RID: 16857 RVA: 0x000FDF1C File Offset: 0x000FC11C
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

	// Token: 0x060041DA RID: 16858 RVA: 0x000FDFB4 File Offset: 0x000FC1B4
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

	// Token: 0x060041DB RID: 16859 RVA: 0x000FE06C File Offset: 0x000FC26C
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

	// Token: 0x060041DC RID: 16860 RVA: 0x000FE090 File Offset: 0x000FC290
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

	// Token: 0x040022AC RID: 8876
	public dfComponentMemberInfo DataSource;

	// Token: 0x040022AD RID: 8877
	public dfComponentMemberInfo DataTarget;

	// Token: 0x040022AE RID: 8878
	public bool TwoWay;

	// Token: 0x040022AF RID: 8879
	private dfObservableProperty sourceProperty;

	// Token: 0x040022B0 RID: 8880
	private dfObservableProperty targetProperty;

	// Token: 0x040022B1 RID: 8881
	private bool isBound;
}
