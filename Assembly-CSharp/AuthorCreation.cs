using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x0200000B RID: 11
public abstract class AuthorCreation : global::AuthorShared
{
	// Token: 0x0600001E RID: 30 RVA: 0x000021FC File Offset: 0x000003FC
	protected AuthorCreation(Type outputType) : this()
	{
		this.outputType = outputType;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x0000220C File Offset: 0x0000040C
	private AuthorCreation()
	{
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000021 RID: 33 RVA: 0x0000226C File Offset: 0x0000046C
	public int settingsHeight
	{
		get
		{
			return this.creationSeperatorHeight;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000022 RID: 34 RVA: 0x00002274 File Offset: 0x00000474
	public int palletWidth
	{
		get
		{
			return this.palletPanelWidth;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000023 RID: 35 RVA: 0x0000227C File Offset: 0x0000047C
	public int rightPanelWidth
	{
		get
		{
			return this.sideBarWidth;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000024 RID: 36 RVA: 0x00002284 File Offset: 0x00000484
	public int palletContentHeight
	{
		get
		{
			return this.palletLabelHeight;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000025 RID: 37 RVA: 0x0000228C File Offset: 0x0000048C
	protected Object output
	{
		get
		{
			return this._output;
		}
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002294 File Offset: 0x00000494
	protected virtual IEnumerable<global::AuthorPalletObject> EnumeratePalletObjects()
	{
		return global::AuthorCreation.NoPalletObjects;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x0000229C File Offset: 0x0000049C
	protected IEnumerable<global::AuthorPeice> EnumeratePeices()
	{
		IEnumerable<global::AuthorPeice> result;
		if (this.allPeices == null || this.allPeices.Count == 0)
		{
			IEnumerable<global::AuthorPeice> noPeices = global::AuthorCreation.NoPeices;
			result = noPeices;
		}
		else
		{
			result = new List<global::AuthorPeice>(this.allPeices);
		}
		return result;
	}

	// Token: 0x06000028 RID: 40 RVA: 0x000022DC File Offset: 0x000004DC
	protected IEnumerable<global::AuthorPeice> EnumerateSelectedPeices()
	{
		IEnumerable<global::AuthorPeice> result;
		if (this.selected == null || this.selected.Count == 0)
		{
			IEnumerable<global::AuthorPeice> noPeices = global::AuthorCreation.NoPeices;
			result = noPeices;
		}
		else
		{
			result = new List<global::AuthorPeice>(this.selected);
		}
		return result;
	}

	// Token: 0x06000029 RID: 41 RVA: 0x0000231C File Offset: 0x0000051C
	internal IEnumerable<global::AuthorPeice> EnumeratePeices(bool selectedOnly)
	{
		IEnumerable<global::AuthorPeice> result;
		if (selectedOnly)
		{
			IEnumerable<global::AuthorPeice> enumerable = this.EnumerateSelectedPeices();
			result = enumerable;
		}
		else
		{
			result = this.EnumeratePeices();
		}
		return result;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002344 File Offset: 0x00000544
	protected virtual bool RegisterPeice(global::AuthorPeice peice)
	{
		if (this.allPeices == null)
		{
			this.allPeices = new List<global::AuthorPeice>();
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

	// Token: 0x0600002B RID: 43 RVA: 0x000023A4 File Offset: 0x000005A4
	private bool RegisterPeice(global::AuthorPeice peice, string id)
	{
		peice.peiceID = id;
		return this.RegisterPeice(peice);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x000023B4 File Offset: 0x000005B4
	public bool SetSelection(Object[] objects)
	{
		List<global::AuthorPeice> list = null;
		foreach (Object @object in objects)
		{
			if (@object is global::AuthorPeice && @object)
			{
				if (list == null)
				{
					list = new List<global::AuthorPeice>();
					list.Add((global::AuthorPeice)@object);
				}
				else if (!list.Contains((global::AuthorPeice)@object))
				{
					list.Add((global::AuthorPeice)@object);
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
					list.Sort((global::AuthorPeice x, global::AuthorPeice y) => this.allPeices.IndexOf(x).CompareTo(this.allPeices.IndexOf(y)));
				}
				if (this.selected == null || this.selected.Count != list.Count)
				{
					flag = true;
				}
				else
				{
					using (List<global::AuthorPeice>.Enumerator enumerator = this.selected.GetEnumerator())
					{
						using (List<global::AuthorPeice>.Enumerator enumerator2 = list.GetEnumerator())
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

	// Token: 0x0600002D RID: 45 RVA: 0x000025C0 File Offset: 0x000007C0
	public bool GUICreationSettings()
	{
		return this.OnGUICreationSettings();
	}

	// Token: 0x0600002E RID: 46 RVA: 0x000025C8 File Offset: 0x000007C8
	protected virtual bool OnGUICreationSettings()
	{
		return false;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x000025CC File Offset: 0x000007CC
	public IEnumerable<global::AuthorPeice> GUIPeiceInspector()
	{
		if (this.selected == null || this.selected.Count == 0)
		{
			return global::AuthorCreation.NoPeices;
		}
		return this.DoGUIPeiceInspector(this.selected);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000025FC File Offset: 0x000007FC
	public IEnumerable<global::AuthorShared.PeiceCommand> GUIPeiceList()
	{
		if (this.allPeices == null || this.allPeices.Count == 0)
		{
			return global::AuthorCreation.NoCommand;
		}
		return this.DoGUIPeiceList(this.allPeices);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x0000262C File Offset: 0x0000082C
	private IEnumerable<global::AuthorPeice> DoGUIPeiceInspector(List<global::AuthorPeice> peices)
	{
		foreach (global::AuthorPeice peice in peices)
		{
			global::AuthorShared.BeginVertical(global::AuthorShared.Styles.gradientOutline, new GUILayoutOption[0]);
			bool b = peice.PeiceInspectorGUI();
			global::AuthorShared.EndVertical();
			if (b)
			{
				yield return peice;
			}
		}
		yield break;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002658 File Offset: 0x00000858
	private IEnumerable<global::AuthorShared.PeiceCommand> DoGUIPeiceList(List<global::AuthorPeice> peices)
	{
		foreach (global::AuthorPeice peice in peices)
		{
			global::AuthorShared.BeginVertical(new GUILayoutOption[0]);
			global::AuthorShared.PeiceAction action = peice.PeiceListGUI();
			global::AuthorShared.EndVertical();
			if (action != global::AuthorShared.PeiceAction.None)
			{
				yield return new global::AuthorShared.PeiceCommand
				{
					peice = peice,
					action = action
				};
			}
		}
		yield break;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002684 File Offset: 0x00000884
	public virtual IEnumerable<global::AuthorPeice> DoSceneView()
	{
		if (this.selected != null)
		{
			Matrix4x4 mat = global::AuthorShared.Scene.matrix;
			Color color = global::AuthorShared.Scene.color;
			bool lighting = global::AuthorShared.Scene.lighting;
			foreach (global::AuthorPeice peice in this.selected)
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
						global::AuthorShared.Scene.matrix = mat;
						global::AuthorShared.Scene.color = color;
						global::AuthorShared.Scene.lighting = lighting;
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

	// Token: 0x06000034 RID: 52 RVA: 0x000026A8 File Offset: 0x000008A8
	protected virtual void OnSelectionChange()
	{
	}

	// Token: 0x06000035 RID: 53 RVA: 0x000026AC File Offset: 0x000008AC
	public bool GUIPalletObjects(params GUILayoutOption[] options)
	{
		return this.GUIPalletObjects(GUI.skin.button, options);
	}

	// Token: 0x06000036 RID: 54 RVA: 0x000026C0 File Offset: 0x000008C0
	public bool GUIPalletObjects(GUIStyle buttonStyle, params GUILayoutOption[] options)
	{
		bool enabled = GUI.enabled;
		bool result = false;
		foreach (global::AuthorPalletObject authorPalletObject in this.EnumeratePalletObjects())
		{
			if (authorPalletObject.guiContent == null)
			{
				authorPalletObject.guiContent = new GUIContent(authorPalletObject.ToString());
			}
			GUI.enabled = (enabled && authorPalletObject.Validate(this));
			global::AuthorPeice authorPeice;
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

	// Token: 0x06000037 RID: 55 RVA: 0x0000279C File Offset: 0x0000099C
	public TPeice CreatePeice<TPeice>(string id, params Type[] additionalComponents) where TPeice : global::AuthorPeice
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

	// Token: 0x06000038 RID: 56 RVA: 0x0000280C File Offset: 0x00000A0C
	public bool Contains(string peiceID)
	{
		if (this.allPeices != null)
		{
			foreach (global::AuthorPeice authorPeice in this.allPeices)
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

	// Token: 0x06000039 RID: 57 RVA: 0x0000289C File Offset: 0x00000A9C
	public bool Contains(global::AuthorPeice comp)
	{
		if (this.allPeices != null)
		{
			foreach (global::AuthorPeice authorPeice in this.allPeices)
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

	// Token: 0x0600003A RID: 58 RVA: 0x00002928 File Offset: 0x00000B28
	protected virtual bool DefaultApply()
	{
		return false;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x0000292C File Offset: 0x00000B2C
	protected virtual void OnWillUnregisterPeice(global::AuthorPeice peice)
	{
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00002930 File Offset: 0x00000B30
	protected virtual void OnUnregisteredPeice(global::AuthorPeice peice)
	{
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00002934 File Offset: 0x00000B34
	internal void UnregisterPeice(global::AuthorPeice peice)
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
					global::AuthorShared.SetDirty(this);
				}
			}
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x000029A4 File Offset: 0x00000BA4
	public virtual void ExecuteCommand(global::AuthorShared.PeiceCommand cmd)
	{
		Debug.Log(cmd.action, cmd.peice);
		switch (cmd.action)
		{
		case global::AuthorShared.PeiceAction.AddToSelection:
		{
			Object selectReference = cmd.peice.selectReference;
			Object[] allSelectedObjects = global::AuthorShared.GetAllSelectedObjects();
			Array.Resize<Object>(ref allSelectedObjects, allSelectedObjects.Length + 1);
			allSelectedObjects[allSelectedObjects.Length - 1] = selectReference;
			global::AuthorShared.SetAllSelectedObjects(allSelectedObjects);
			break;
		}
		case global::AuthorShared.PeiceAction.RemoveFromSelection:
		{
			Object selectReference2 = cmd.peice.selectReference;
			Object[] allSelectedObjects2 = global::AuthorShared.GetAllSelectedObjects();
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
				global::AuthorShared.SetAllSelectedObjects(allSelectedObjects2);
			}
			break;
		}
		case global::AuthorShared.PeiceAction.SelectSolo:
			global::AuthorShared.SetAllSelectedObjects(new Object[]
			{
				cmd.peice.selectReference
			});
			break;
		case global::AuthorShared.PeiceAction.Delete:
		{
			bool? flag = global::AuthorShared.Ask(string.Concat(new object[]
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
		case global::AuthorShared.PeiceAction.Dirty:
			global::AuthorShared.SetDirty(cmd.peice);
			break;
		case global::AuthorShared.PeiceAction.Ping:
			global::AuthorShared.PingObject(cmd.peice);
			break;
		}
	}

	// Token: 0x0600003F RID: 63
	protected abstract void SaveSettings(JSONStream stream);

	// Token: 0x06000040 RID: 64
	protected abstract void LoadSettings(JSONStream stream);

	// Token: 0x06000041 RID: 65 RVA: 0x00002B5C File Offset: 0x00000D5C
	protected Stream GetStream(bool write, string filepath, out global::AuthorCreationProject proj)
	{
		proj = global::AuthorCreationProject.current;
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

	// Token: 0x06000042 RID: 66 RVA: 0x00002BB0 File Offset: 0x00000DB0
	protected bool SaveSettings()
	{
		global::AuthorCreationProject authorCreationProject;
		Stream stream = this.GetStream(true, "dat.asc", out authorCreationProject);
		if (stream != null)
		{
			try
			{
				using (JSONStream jsonstream = JSONStream.CreateWriter(stream))
				{
					jsonstream.WriteObjectStart();
					jsonstream.WriteObjectStart("project");
					jsonstream.WriteText("guid", global::AuthorShared.PathToGUID(global::AuthorShared.GetAssetPath(authorCreationProject)));
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

	// Token: 0x06000043 RID: 67 RVA: 0x00002CC0 File Offset: 0x00000EC0
	protected bool LoadSettings()
	{
		global::AuthorCreationProject authorCreationProject;
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
									if (global::AuthorCreation.<>f__switch$map0 == null)
									{
										global::AuthorCreation.<>f__switch$map0 = new Dictionary<string, int>(2)
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
									if (global::AuthorCreation.<>f__switch$map0.TryGetValue(text2, out num))
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

	// Token: 0x06000044 RID: 68 RVA: 0x00002E00 File Offset: 0x00001000
	public virtual string RootBonePath(global::AuthorPeice callingPeice, Transform bone)
	{
		return global::AuthorShared.CalculatePath(bone, bone.root);
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
	private List<global::AuthorPeice> allPeices;

	// Token: 0x04000022 RID: 34
	[NonSerialized]
	private List<global::AuthorPeice> selected;

	// Token: 0x04000023 RID: 35
	protected static readonly global::AuthorPalletObject[] NoPalletObjects = new global::AuthorPalletObject[0];

	// Token: 0x04000024 RID: 36
	protected static readonly global::AuthorPeice[] NoPeices = new global::AuthorPeice[0];

	// Token: 0x04000025 RID: 37
	private static readonly global::AuthorShared.PeiceCommand[] NoCommand = new global::AuthorShared.PeiceCommand[0];
}
