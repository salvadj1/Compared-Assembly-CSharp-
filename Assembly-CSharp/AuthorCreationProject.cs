using System;
using System.IO;
using UnityEngine;

// Token: 0x0200000D RID: 13
public sealed class AuthorCreationProject : ScriptableObject
{
	// Token: 0x1700000E RID: 14
	// (get) Token: 0x0600004A RID: 74 RVA: 0x00002F40 File Offset: 0x00001140
	public string scene
	{
		get
		{
			return this._scene;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600004B RID: 75 RVA: 0x00002F48 File Offset: 0x00001148
	public string folder
	{
		get
		{
			return this._folder;
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x0600004C RID: 76 RVA: 0x00002F50 File Offset: 0x00001150
	public string script
	{
		get
		{
			return this._script;
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x0600004D RID: 77 RVA: 0x00002F58 File Offset: 0x00001158
	public string project
	{
		get
		{
			return this._project;
		}
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x0600004E RID: 78 RVA: 0x00002F60 File Offset: 0x00001160
	public string authorName
	{
		get
		{
			return this._authorName;
		}
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00002F68 File Offset: 0x00001168
	public Stream GetStream(bool write, string filepath)
	{
		return null;
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x06000050 RID: 80 RVA: 0x00002F6C File Offset: 0x0000116C
	public static AuthorCreationProject current
	{
		get
		{
			return null;
		}
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000051 RID: 81 RVA: 0x00002F70 File Offset: 0x00001170
	public string scenePath
	{
		get
		{
			return string.Empty;
		}
	}

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000052 RID: 82 RVA: 0x00002F78 File Offset: 0x00001178
	public string folderPath
	{
		get
		{
			return string.Empty;
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000053 RID: 83 RVA: 0x00002F80 File Offset: 0x00001180
	public string scriptPath
	{
		get
		{
			return string.Empty;
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000054 RID: 84 RVA: 0x00002F88 File Offset: 0x00001188
	public string singletonName
	{
		get
		{
			return string.Empty;
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000055 RID: 85 RVA: 0x00002F90 File Offset: 0x00001190
	public Object monoScript
	{
		get
		{
			return null;
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000056 RID: 86 RVA: 0x00002F94 File Offset: 0x00001194
	public Type authorCreationType
	{
		get
		{
			return null;
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000057 RID: 87 RVA: 0x00002F98 File Offset: 0x00001198
	public bool isCurrent
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00002F9C File Offset: 0x0000119C
	public GameObject FindAuthorCreationGameObjectInScene()
	{
		return null;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00002FA0 File Offset: 0x000011A0
	public AuthorCreation FindAuthorCreationInScene()
	{
		return null;
	}

	// Token: 0x0400002A RID: 42
	[SerializeField]
	[HideInInspector]
	private string _scene;

	// Token: 0x0400002B RID: 43
	[HideInInspector]
	[SerializeField]
	private string _folder;

	// Token: 0x0400002C RID: 44
	[SerializeField]
	[HideInInspector]
	private string _script;

	// Token: 0x0400002D RID: 45
	[HideInInspector]
	[SerializeField]
	private string _project;

	// Token: 0x0400002E RID: 46
	[SerializeField]
	[HideInInspector]
	private string _authorName;
}
