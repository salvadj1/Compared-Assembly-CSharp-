using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class AuthorPeice : AuthorShared
{
	// Token: 0x1700001B RID: 27
	// (get) Token: 0x0600005B RID: 91 RVA: 0x00002FAC File Offset: 0x000011AC
	public AuthorCreation creation
	{
		get
		{
			return this._creation;
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x0600005C RID: 92 RVA: 0x00002FB4 File Offset: 0x000011B4
	// (set) Token: 0x0600005D RID: 93 RVA: 0x00002FBC File Offset: 0x000011BC
	public string peiceID
	{
		get
		{
			return this._peiceID;
		}
		set
		{
			this._peiceID = (value ?? string.Empty);
		}
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00002FD4 File Offset: 0x000011D4
	public void Registered(AuthorCreation creation)
	{
		this._creation = creation;
		this._peiceID = (this._peiceID ?? string.Empty);
		this.OnRegistered();
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00002FFC File Offset: 0x000011FC
	protected virtual void OnRegistered()
	{
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00003000 File Offset: 0x00001200
	public virtual bool PeiceInspectorGUI()
	{
		AuthorShared.BeginHorizontal(AuthorShared.Styles.gradientInlineFill, new GUILayoutOption[0]);
		GUILayout.Space(48f);
		if (GUILayout.Button(AuthorShared.ObjectContent<Transform>(base.transform, typeof(Transform)).image, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(false),
			GUILayout.ExpandHeight(false)
		}))
		{
			AuthorShared.PingObject(this);
		}
		GUILayout.Space(10f);
		GUILayout.Label(this.peiceID, AuthorShared.Styles.boldLabel, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		AuthorShared.EndHorizontal();
		return false;
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000061 RID: 97 RVA: 0x00003098 File Offset: 0x00001298
	public Object selectReference
	{
		get
		{
			return base.gameObject;
		}
	}

	// Token: 0x06000062 RID: 98 RVA: 0x000030A0 File Offset: 0x000012A0
	protected static bool ActionButton(AuthorShared.Content content, ref AuthorShared.PeiceAction act, bool isSelected, AuthorShared.PeiceAction onAction, AuthorShared.PeiceAction offAction, GUIStyle style, params GUILayoutOption[] options)
	{
		if (AuthorShared.Toggle(content, isSelected, style, options) != isSelected)
		{
			act = ((!isSelected) ? onAction : offAction);
			return true;
		}
		return false;
	}

	// Token: 0x06000063 RID: 99 RVA: 0x000030D4 File Offset: 0x000012D4
	protected static bool ActionButton(AuthorShared.Content content, ref AuthorShared.PeiceAction act, bool isSelected, AuthorShared.PeiceAction action, GUIStyle style, params GUILayoutOption[] options)
	{
		if (AuthorShared.Toggle(content, isSelected, style, options) != isSelected)
		{
			act = action;
			return true;
		}
		return false;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x000030F0 File Offset: 0x000012F0
	public virtual AuthorShared.PeiceAction PeiceListGUI()
	{
		bool isSelected = AuthorShared.SelectionContains(this.selectReference) || AuthorShared.SelectionContains(this);
		AuthorShared.PeiceAction result = AuthorShared.PeiceAction.None;
		AuthorShared.BeginHorizontal(new GUILayoutOption[0]);
		AuthorPeice.ActionButton(this.peiceID, ref result, isSelected, AuthorShared.PeiceAction.AddToSelection, AuthorShared.PeiceAction.RemoveFromSelection, AuthorShared.Styles.peiceButtonLeft, new GUILayoutOption[0]);
		AuthorPeice.ActionButton(AuthorShared.Icon.solo, ref result, isSelected, AuthorShared.PeiceAction.SelectSolo, AuthorShared.Styles.peiceButtonMid, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(false)
		});
		Color contentColor = GUI.contentColor;
		GUI.contentColor = Color.red;
		AuthorPeice.ActionButton(AuthorShared.Icon.delete, ref result, isSelected, AuthorShared.PeiceAction.Delete, AuthorShared.Styles.peiceButtonRight, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(false)
		});
		GUI.contentColor = contentColor;
		AuthorShared.EndHorizontal();
		return result;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x000031B4 File Offset: 0x000013B4
	public virtual void OnListClicked()
	{
		if (AuthorShared.SelectionContains(this.selectReference) || AuthorShared.SelectionContains(this))
		{
		}
	}

	// Token: 0x06000066 RID: 102 RVA: 0x000031E4 File Offset: 0x000013E4
	public virtual bool OnSceneView()
	{
		return false;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x000031E8 File Offset: 0x000013E8
	protected virtual void OnWillUnRegister()
	{
	}

	// Token: 0x06000068 RID: 104 RVA: 0x000031EC File Offset: 0x000013EC
	protected virtual void OnDidUnRegister()
	{
	}

	// Token: 0x06000069 RID: 105 RVA: 0x000031F0 File Offset: 0x000013F0
	public void Delete()
	{
		if (!this.destroyed)
		{
			try
			{
				this.OnPeiceDestroy();
			}
			finally
			{
				this.destroyed = true;
				Object.DestroyImmediate(this);
			}
		}
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00003240 File Offset: 0x00001440
	protected virtual void OnPeiceDestroy()
	{
		if (this._creation)
		{
			this.OnWillUnRegister();
			this._creation.UnregisterPeice(this);
			this.OnDidUnRegister();
		}
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00003278 File Offset: 0x00001478
	public virtual void SaveJsonProperties(JSONStream stream)
	{
	}

	// Token: 0x0600006C RID: 108 RVA: 0x0000327C File Offset: 0x0000147C
	protected string FromRootBonePath(Transform transform)
	{
		if (this.creation)
		{
			return this.creation.RootBonePath(this, transform);
		}
		return string.Empty;
	}

	// Token: 0x0400002F RID: 47
	[SerializeField]
	private AuthorCreation _creation;

	// Token: 0x04000030 RID: 48
	[SerializeField]
	private string _peiceID;

	// Token: 0x04000031 RID: 49
	private bool destroyed;
}
