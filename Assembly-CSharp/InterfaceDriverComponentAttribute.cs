using System;
using UnityEngine;

// Token: 0x020001C2 RID: 450
[AttributeUsage(AttributeTargets.Class)]
public class InterfaceDriverComponentAttribute : Attribute
{
	// Token: 0x06000C75 RID: 3189 RVA: 0x000318E8 File Offset: 0x0002FAE8
	public InterfaceDriverComponentAttribute(Type interfaceType, string serializedFieldName, string runtimeFieldName)
	{
		this.Interface = interfaceType;
		this.SerializedFieldName = serializedFieldName;
		this.RuntimeFieldName = runtimeFieldName;
	}

	// Token: 0x17000321 RID: 801
	// (get) Token: 0x06000C76 RID: 3190 RVA: 0x00031928 File Offset: 0x0002FB28
	// (set) Token: 0x06000C77 RID: 3191 RVA: 0x00031930 File Offset: 0x0002FB30
	public bool AlwaysSaveDisabled { get; set; }

	// Token: 0x17000322 RID: 802
	// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0003193C File Offset: 0x0002FB3C
	// (set) Token: 0x06000C79 RID: 3193 RVA: 0x00031944 File Offset: 0x0002FB44
	public string AdditionalProperties { get; set; }

	// Token: 0x17000323 RID: 803
	// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00031950 File Offset: 0x0002FB50
	// (set) Token: 0x06000C7B RID: 3195 RVA: 0x00031958 File Offset: 0x0002FB58
	public Type UnityType
	{
		get
		{
			return this._minimumType;
		}
		set
		{
			this._minimumType = (value ?? typeof(MonoBehaviour));
		}
	}

	// Token: 0x17000324 RID: 804
	// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00031974 File Offset: 0x0002FB74
	// (set) Token: 0x06000C7D RID: 3197 RVA: 0x0003197C File Offset: 0x0002FB7C
	public InterfaceSearchRoute SearchRoute
	{
		get
		{
			return this.searchRoute;
		}
		set
		{
			if (value == (InterfaceSearchRoute)0)
			{
				value = InterfaceSearchRoute.GameObject;
			}
			this.searchRoute = value;
		}
	}

	// Token: 0x04000792 RID: 1938
	public readonly string SerializedFieldName;

	// Token: 0x04000793 RID: 1939
	public readonly string RuntimeFieldName;

	// Token: 0x04000794 RID: 1940
	public readonly Type Interface;

	// Token: 0x04000795 RID: 1941
	private Type _minimumType = typeof(MonoBehaviour);

	// Token: 0x04000796 RID: 1942
	private InterfaceSearchRoute searchRoute = InterfaceSearchRoute.GameObject;
}
