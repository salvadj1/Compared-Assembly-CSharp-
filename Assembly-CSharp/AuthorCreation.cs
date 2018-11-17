using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x0200000B RID: 11
public abstract class AuthorCreation : AuthorShared
{
	// Token: 0x0600001E RID: 30 RVA: 0x00002298 File Offset: 0x00000498
	protected AuthorCreation(Type outputType) : this()
	{
		this.outputType = outputType;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000022A8 File Offset: 0x000004A8
	private AuthorCreation()
	{
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000021 RID: 33 RVA: 0x00002308 File Offset: 0x00000508
	public int settingsHeight
	{
		get
		{
			return this.creationSeperatorHeight;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000022 RID: 34 RVA: 0x00002310 File Offset: 0x00000510
	public int palletWidth
	{
		get
		{
			return this.palletPanelWidth;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000023 RID: 35 RVA: 0x00002318 File Offset: 0x00000518
	public int rightPanelWidth
	{
		get
		{
			return this.sideBarWidth;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000024 RID: 36 RVA: 0x00002320 File Offset: 0x00000520
	public int palletContentHeight
	{
		get
		{
			return this.palletLabelHeight;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000025 RID: 37 RVA: 0x00002328 File Offset: 0x00000528
	protected Object output
	{
		get
		{
			return this._output;
		}
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002330 File Offset: 0x00000530
	protected virtual IEnumerable<AuthorPalletObject> EnumeratePalletObjects()
	{
		return AuthorCreation.NoPalletObjects;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002338 File Offset: 0x00000538
	protected IEnumerable<AuthorPeice> EnumeratePeices()
	{
		IEnumerable<AuthorPeice> result;
		if (this.allPeices == null || this.allPeices.Count == 0)
		{
			IEnumerable<AuthorPeice> noPeices = AuthorCreation.NoPeices;
			result = noPeices;
		}
		else
		{
			result = new List<AuthorPeice>(this.allPeices);
		}
		return result;
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002378 File Offset: 0x00000578
	protected IEnumerable<AuthorPeice> EnumerateSelectedPeices()
	{
		IEnumerable<AuthorPeice> result;
		if (this.selected == null || this.selected.Count == 0)
		{
			IEnumerable<AuthorPeice> noPeices = AuthorCreation.NoPeices;
			result = noPeices;
		}
		else
		{
			result = new List<AuthorPeice>(this.selected);
		}
		return result;
	}

	// Token: 0x06000029 RID: 41 RVA: 0x000023B8 File Offset: 0x000005B8
	internal IEnumerable<AuthorPeice> EnumeratePeices(bool selectedOnly)
	{
		IEnumerable<AuthorPeice> result;
		if (selectedOnly)
		{
			IEnumerable<AuthorPeice> enumerable = this.EnumerateSelectedPeices();
			result = enumerable;
		}
		else
		{
			result = this.EnumeratePeices();
		}
		return result;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x000023E0 File Offset: 0x000005E0
	protected virtual bool RegisterPeice(AuthorPeice peice)
	{
		if (this.allPeices == null)
		{
			this.allPeices = new List<AuthorPeice>();
			this.allPeices.Add(peice);
		}
		else
		{
			if (this.allPeices.Contains(peice))
			{
				return false;
			}
			this.allPeices.Add(peice);
		}
		peice.Registered(this);
		return true;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002440 File Offset: 0x00000640
	private bool RegisterPeice(AuthorPeice peice, string id)
	{
		peice.peiceID = id;
		return this.RegisterPeice(peice);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002450 File Offset: 0x00000650
	public bool SetSelection(Object[] objects)
	{
		List<AuthorPeice> list = null;
		foreach (Object @object in objects)
		{
			if (@object is AuthorPeice && @object)
			{
				if (list == null)
				{
					list = new List<AuthorPeice>();
					list.Add((AuthorPeice)@object);
				}
				else if (!list.Contains((AuthorPeice)@object))
				{
					list.Add((AuthorPeice)@object);
				}
			}
		}
		bool flag = false;
		try
		{
			if (list == null)
			{
				if (this.selected != null)
				{
					flag = (this.selected.Count > 0);
					this.selected.Clear();
				}
			}
			else
			{
				if (this.allPeices != null)
				{
					list.Sort((AuthorPeice x, AuthorPeice y) => this.allPeices.IndexOf(x).CompareTo(this.allPeices.IndexOf(y)));
				}
				if (this.selected == null || this.selected.Count != list.Count)
				{
					flag = true;
				}
				else
				{
					using (List<AuthorPeice>.Enumerator enumerator = this.selected.GetEnumerator())
					{
						using (List<AuthorPeice>.Enumerator enumerator2 = list.GetEnumerator())
						{
							while (enumerator.MoveNext() && enumerator2.MoveNext())
							{
								if (enumerator.Current != enumerator2.Current)
								{
									flag = true;
									break;
								}
							}
						}
					}
				}
			}
		}
		finally
		{
			if (flag)
			{
				if (this.selected != null)
				{
					this.selected.Clear();
					if (list != null)
					{
						this.selected.AddRange(list);
					}
				}
				else if (list != null)
				{
					this.selected = list;
				}
				this.OnSelectionChange();
			}
		}
		return flag;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x0000265C File Offset: 0x0000085C
	public bool GUICreationSettings()
	{
		return this.OnGUICreationSettings();
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002664 File Offset: 0x00000864
	protected virtual bool OnGUICreationSettings()
	{
		return false;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002668 File Offset: 0x00000868
	public IEnumerable<AuthorPeice> GUIPeiceInspector()
	{
		if (this.selected == null || this.selected.Count == 0)
		{
			return AuthorCreation.NoPeices;
		}
		return this.DoGUIPeiceInspector(this.selected);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002698 File Offset: 0x00000898
	public IEnumerable<AuthorShared.PeiceCommand> GUIPeiceList()
	{
		if (this.allPeices == null || this.allPeices.Count == 0)
		{
			return AuthorCreation.NoCommand;
		}
		return this.DoGUIPeiceList(this.allPeices);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000026C8 File Offset: 0x000008C8
	private IEnumerable<AuthorPeice> DoGUIPeiceInspector(List<AuthorPeice> peices)
	{
		foreach (AuthorPeice peice in peices)
		{
			AuthorShared.BeginVertical(AuthorShared.Styles.gradientOutline, new GUILayoutOption[0]);
			bool b = peice.PeiceInspectorGUI();
			AuthorShared.EndVertical();
			if (b)
			{
				yield return peice;
			}
		}
		yield break;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x000026F4 File Offset: 0x000008F4
	private IEnumerable<AuthorShared.PeiceCommand> DoGUIPeiceList(List<AuthorPeice> peices)
	{
		foreach (AuthorPeice peice in peices)
		{
			AuthorShared.BeginVertical(new GUILayoutOption[0]);
			AuthorShared.PeiceAction action = peice.PeiceListGUI();
			AuthorShared.EndVertical();
			if (action != AuthorShared.PeiceAction.None)
			{
				yield return new AuthorShared.PeiceCommand
				{
					peice = peice,
					action = action
				};
			}
		}
		yield break;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002720 File Offset: 0x00000920
	public virtual IEnumerable<AuthorPeice> DoSceneView()
	{
		if (this.selected != null)
		{
			Matrix4x4 mat = AuthorShared.Scene.matrix;
			Color color = AuthorShared.Scene.color;
			bool lighting = AuthorShared.Scene.lighting;
			foreach (AuthorPeice peice in this.selected)
			{
				if (peice)
				{
					bool change;
					try
					{
						change = peice.OnSceneView();
					}
					finally
					{
						AuthorShared.Scene.matrix = mat;
						AuthorShared.Scene.color = color;
						AuthorShared.Scene.lighting = lighting;
					}
					if (change)
					{
						yield return peice;
					}
				}
			}
		}
		yield break;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002744 File Offset: 0x00000944
	protected virtual void OnSelectionChange()
	{
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00002748 File Offset: 0x00000948
	public bool GUIPalletObjects(params GUILayoutOption[] options)
	{
		return this.GUIPalletObjects(GUI.skin.button, options);
	}

	// Token: 0x06000036 RID: 54 RVA: 0x0000275C File Offset: 0x0000095C
	public bool GUIPalletObjects(GUIStyle buttonStyle, params GUILayoutOption[] options)
	{
		bool enabled = GUI.enabled;
		bool result = false;
		foreach (AuthorPalletObject authorPalletObject in this.EnumeratePalletObjects())
		{
			if (authorPalletObject.guiContent == null)
			{
				authorPalletObject.guiContent = new GUIContent(authorPalletObject.ToString());
			}
			GUI.enabled = (enabled && authorPalletObject.Validate(this));
			AuthorPeice authorPeice;
			if (GUILayout.Button(authorPalletObject.guiContent, buttonStyle, options) && authorPalletObject.Create(this, out authorPeice))
			{
				if (!this.RegisterPeice(authorPeice))
				{
					Object.DestroyImmediate(authorPeice.gameObject);
				}
				else
				{
					result = true;
				}
			}
		}
		GUI.enabled = enabled;
		return result;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002838 File Offset: 0x00000A38
	public TPeice CreatePeice<TPeice>(string id, params Type[] additionalComponents) where TPeice : AuthorPeice
	{
		Type[] array = new Type[additionalComponents.Length + 1];
		Array.Copy(additionalComponents, 0, array, 1, additionalComponents.Length);
		array[0] = typeof(TPeice);
		GameObject gameObject = new GameObject(id, array);
		TPeice tpeice = gameObject.GetComponent<TPeice>();
		if (!tpeice || !this.RegisterPeice(tpeice, id))
		{
			Object.DestroyImmediate(gameObject);
			tpeice = (TPeice)((object)null);
		}
		return tpeice;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x000028A8 File Offset: 0x00000AA8
	public bool Contains(string peiceID)
	{
		if (this.allPeices != null)
		{
			foreach (AuthorPeice authorPeice in this.allPeices)
			{
				if (authorPeice && authorPeice.peiceID == peiceID)
				{
					return true;
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00002938 File Offset: 0x00000B38
	public bool Contains(AuthorPeice comp)
	{
		if (this.allPeices != null)
		{
			foreach (AuthorPeice authorPeice in this.allPeices)
			{
				if (authorPeice && authorPeice == comp)
				{
					return true;
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x000029C4 File Offset: 0x00000BC4
	protected virtual bool DefaultApply()
	{
		return false;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x000029C8 File Offset: 0x00000BC8
	protected virtual void OnWillUnregisterPeice(AuthorPeice peice)
	{
	}

	// Token: 0x0600003C RID: 60 RVA: 0x000029CC File Offset: 0x00000BCC
	protected virtual void OnUnregisteredPeice(AuthorPeice peice)
	{
	}

	// Token: 0x0600003D RID: 61 RVA: 0x000029D0 File Offset: 0x00000BD0
	internal void UnregisterPeice(AuthorPeice peice)
	{
		if (this.allPeices != null)
		{
			int num = this.allPeices.IndexOf(peice);
			if (num != -1)
			{
				this.OnWillUnregisterPeice(peice);
				this.allPeices.Remove(peice);
				if (this.selected != null)
				{
					this.selected.Remove(peice);
				}
				this.OnUnregisteredPeice(peice);
				if (!Application.isPlaying)
				{
					AuthorShared.SetDirty(this);
				}
			}
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00002A40 File Offset: 0x00000C40
	public virtual void ExecuteCommand(AuthorShared.PeiceCommand cmd)
	{
		Debug.Log(cmd.action, cmd.peice);
		switch (cmd.action)
		{
		case AuthorShared.PeiceAction.AddToSelection:
		{
			Object selectReference = cmd.peice.selectReference;
			Object[] allSelectedObjects = AuthorShared.GetAllSelectedObjects();
			Array.Resize<Object>(ref allSelectedObjects, allSelectedObjects.Length + 1);
			allSelectedObjects[allSelectedObjects.Length - 1] = selectReference;
			AuthorShared.SetAllSelectedObjects(allSelectedObjects);
			break;
		}
		case AuthorShared.PeiceAction.RemoveFromSelection:
		{
			Object selectReference2 = cmd.peice.selectReference;
			Object[] allSelectedObjects2 = AuthorShared.GetAllSelectedObjects();
			int num = 0;
			for (int i = 0; i < allSelectedObjects2.Length; i++)
			{
				if (allSelectedObjects2[i] != selectReference2 && allSelectedObjects2[i] != cmd.peice)
				{
					allSelectedObjects2[num++] = allSelectedObjects2[i];
				}
			}
			if (num < allSelectedObjects2.Length)
			{
				Array.Resize<Object>(ref allSelectedObjects2, num);
				AuthorShared.SetAllSelectedObjects(allSelectedObjects2);
			}
			break;
		}
		case AuthorShared.PeiceAction.SelectSolo:
			AuthorShared.SetAllSelectedObjects(new Object[]
			{
				cmd.peice.selectReference
			});
			break;
		case AuthorShared.PeiceAction.Delete:
		{
			bool? flag = AuthorShared.Ask(string.Concat(new object[]
			{
				"You want to delete ",
				cmd.peice.peiceID,
				"? (",
				cmd.peice,
				")"
			}));
			if (flag != null && flag.Value)
			{
				cmd.peice.Delete();
			}
			break;
		}
		case AuthorShared.PeiceAction.Dirty:
			AuthorShared.SetDirty(cmd.peice);
			break;
		case AuthorShared.PeiceAction.Ping:
			AuthorShared.PingObject(cmd.peice);
			break;
		}
	}

	// Token: 0x0600003F RID: 63
	protected abstract void SaveSettings(JSONStream stream);

	// Token: 0x06000040 RID: 64
	protected abstract void LoadSettings(JSONStream stream);

	// Token: 0x06000041 RID: 65 RVA: 0x00002BF8 File Offset: 0x00000DF8
	protected Stream GetStream(bool write, string filepath, out AuthorCreationProject proj)
	{
		proj = AuthorCreationProject.current;
		if (!proj)
		{
			throw new InvalidOperationException("Theres no project loaded");
		}
		if (proj.FindAuthorCreationInScene() != this)
		{
			throw new InvalidOperationException("The current project is not for this creation");
		}
		return proj.GetStream(write, filepath);
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00002C4C File Offset: 0x00000E4C
	protected bool SaveSettings()
	{
		AuthorCreationProject authorCreationProject;
		Stream stream = this.GetStream(true, "dat.asc", out authorCreationProject);
		if (stream != null)
		{
			try
			{
				using (JSONStream jsonstream = JSONStream.CreateWriter(stream))
				{
					jsonstream.WriteObjectStart();
					jsonstream.WriteObjectStart("project");
					jsonstream.WriteText("guid", AuthorShared.PathToGUID(AuthorShared.GetAssetPath(authorCreationProject)));
					jsonstream.WriteText("name", authorCreationProject.project);
					jsonstream.WriteText("author", authorCreationProject.authorName);
					jsonstream.WriteText("scene", authorCreationProject.scene);
					jsonstream.WriteText("folder", authorCreationProject.folder);
					jsonstream.WriteObjectEnd();
					jsonstream.WriteProperty("settings");
					this.SaveSettings(jsonstream);
					jsonstream.WriteObjectEnd();
				}
				return true;
			}
			finally
			{
				stream.Dispose();
			}
			return false;
		}
		return false;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00002D5C File Offset: 0x00000F5C
	protected bool LoadSettings()
	{
		AuthorCreationProject authorCreationProject;
		Stream stream = this.GetStream(true, "dat.asc", out authorCreationProject);
		if (stream != null)
		{
			try
			{
				using (JSONStream jsonstream = JSONStream.CreateWriter(stream))
				{
					while (jsonstream.Read())
					{
						JSONToken token = jsonstream.token;
						if (token == 1)
						{
							string text;
							while (jsonstream.ReadNextProperty(ref text))
							{
								string text2 = text;
								if (text2 != null)
								{
									if (AuthorCreation.<>f__switch$map0 == null)
									{
										AuthorCreation.<>f__switch$map0 = new Dictionary<string, int>(2)
										{
											{
												"project",
												0
											},
											{
												"settings",
												1
											}
										};
									}
									int num;
									if (AuthorCreation.<>f__switch$map0.TryGetValue(text2, out num))
									{
										if (num != 0)
										{
											if (num == 1)
											{
												this.LoadSettings(jsonstream);
											}
										}
										else
										{
											jsonstream.ReadSkip();
										}
									}
								}
							}
						}
					}
				}
				return true;
			}
			finally
			{
				stream.Dispose();
			}
			return false;
		}
		return false;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00002E9C File Offset: 0x0000109C
	public virtual string RootBonePath(AuthorPeice callingPeice, Transform bone)
	{
		return AuthorShared.CalculatePath(bone, bone.root);
	}

	// Token: 0x0400001B RID: 27
	[SerializeField]
	private Object _output;

	// Token: 0x0400001C RID: 28
	[NonSerialized]
	public readonly Type outputType;

	// Token: 0x0400001D RID: 29
	protected int creationSeperatorHeight = 300;

	// Token: 0x0400001E RID: 30
	protected int sideBarWidth = 200;

	// Token: 0x0400001F RID: 31
	protected int palletLabelHeight = 48;

	// Token: 0x04000020 RID: 32
	protected int palletPanelWidth = 96;

	// Token: 0x04000021 RID: 33
	[SerializeField]
	private List<AuthorPeice> allPeices;

	// Token: 0x04000022 RID: 34
	[NonSerialized]
	private List<AuthorPeice> selected;

	// Token: 0x04000023 RID: 35
	protected static readonly AuthorPalletObject[] NoPalletObjects = new AuthorPalletObject[0];

	// Token: 0x04000024 RID: 36
	protected static readonly AuthorPeice[] NoPeices = new AuthorPeice[0];

	// Token: 0x04000025 RID: 37
	private static readonly AuthorShared.PeiceCommand[] NoCommand = new AuthorShared.PeiceCommand[0];
}
