using System;
using UnityEngine;

// Token: 0x020001F2 RID: 498
[AttributeUsage(AttributeTargets.Class)]
public class InterfaceDriverComponentAttribute : Attribute
{
	// Token: 0x06000DB5 RID: 3509 RVA: 0x000357D4 File Offset: 0x000339D4
	public InterfaceDriverComponentAttribute(Type interfaceType, string serializedFieldName, string runtimeFieldName)
	{
		this.Interface = interfaceType;
		this.SerializedFieldName = serializedFieldName;
		this.RuntimeFieldName = runtimeFieldName;
	}

	// Token: 0x17000365 RID: 869
	// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x00035814 File Offset: 0x00033A14
	// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x0003581C File Offset: 0x00033A1C
	public bool AlwaysSaveDisabled { get; set; }

	// Token: 0x17000366 RID: 870
	// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x00035828 File Offset: 0x00033A28
	// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x00035830 File Offset: 0x00033A30
	public string AdditionalProperties { get; set; }

	// Token: 0x17000367 RID: 871
	// (get) Token: 0x06000DBA RID: 3514 RVA: 0x0003583C File Offset: 0x00033A3C
	// (set) Token: 0x06000DBB RID: 3515 RVA: 0x00035844 File Offset: 0x00033A44
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

	// Token: 0x17000368 RID: 872
	// (get) Token: 0x06000DBC RID: 3516 RVA: 0x00035860 File Offset: 0x00033A60
	// (set) Token: 0x06000DBD RID: 3517 RVA: 0x00035868 File Offset: 0x00033A68
	public global::InterfaceSearchRoute SearchRoute
	{
		get
		{
			return this.searchRoute;
		}
		set
		{
			if (value == (global::InterfaceSearchRoute)0)
			{
				value = global::InterfaceSearchRoute.GameObject;
			}
			this.searchRoute = value;
		}
	}

	// Token: 0x040008A6 RID: 2214
	public readonly string SerializedFieldName;

	// Token: 0x040008A7 RID: 2215
	public readonly string RuntimeFieldName;

	// Token: 0x040008A8 RID: 2216
	public readonly Type Interface;

	// Token: 0x040008A9 RID: 2217
	private Type _minimumType = typeof(MonoBehaviour);

	// Token: 0x040008AA RID: 2218
	private global::InterfaceSearchRoute searchRoute = global::InterfaceSearchRoute.GameObject;
}
