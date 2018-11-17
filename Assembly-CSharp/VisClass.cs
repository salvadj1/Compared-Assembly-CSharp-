using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000396 RID: 918
public class VisClass : ScriptableObject
{
	// Token: 0x1700087F RID: 2175
	// (get) Token: 0x060022CD RID: 8909 RVA: 0x00085BE0 File Offset: 0x00083DE0
	public VisClass superClass
	{
		get
		{
			return this._super;
		}
	}

	// Token: 0x060022CE RID: 8910 RVA: 0x00085BE8 File Offset: 0x00083DE8
	public void EditorOnly_Rep(ref VisClass.Rep rep)
	{
		if (this.keys == null && this.values == null)
		{
			this.keys = new string[0];
			this.values = new VisQuery[0];
		}
		VisClass.Rep.Ref(ref rep, this);
	}

	// Token: 0x060022CF RID: 8911 RVA: 0x00085C20 File Offset: 0x00083E20
	public bool EditorOnly_Apply(ref VisClass.Rep rep)
	{
		return rep != null && rep.Apply();
	}

	// Token: 0x060022D0 RID: 8912 RVA: 0x00085C34 File Offset: 0x00083E34
	public void EditorOnly_Add(ref VisClass.Rep rep, string key, VisQuery value)
	{
		Array.Resize<string>(ref this.keys, this.keys.Length + 1);
		Array.Resize<VisQuery>(ref this.values, this.values.Length + 1);
		this.keys[this.keys.Length - 1] = key;
		this.values[this.values.Length - 1] = value;
		rep = null;
	}

	// Token: 0x060022D1 RID: 8913 RVA: 0x00085C94 File Offset: 0x00083E94
	public bool EditorOnly_SetSuper(ref VisClass.Rep rep, VisClass _super)
	{
		VisClass visClass = _super;
		int num = 50;
		while (visClass != null)
		{
			if (visClass == this)
			{
				Debug.LogError("Self Reference Detected", this);
				return false;
			}
			visClass = visClass._super;
			if (--num <= 0)
			{
				Debug.LogError("Circular Dependancy Detected", this);
				return false;
			}
		}
		rep = null;
		this._super = _super;
		return true;
	}

	// Token: 0x060022D2 RID: 8914 RVA: 0x00085CFC File Offset: 0x00083EFC
	private void BuildMembers(List<VisQuery> list, HashSet<VisQuery> hset)
	{
		if (this._super)
		{
			if (this._super.recurseLock)
			{
				Debug.LogError("Recursion in setup hit itself, some VisClass has super set to something which references itself", this._super);
				return;
			}
			this._super.recurseLock = true;
			this._super.BuildMembers(list, hset);
			this._super.recurseLock = false;
		}
		if (this.values != null)
		{
			for (int i = 0; i < this.values.Length; i++)
			{
				if (this.values[i] != null && hset.Remove(this.values[i]))
				{
					list.Add(this.values[i]);
				}
			}
		}
	}

	// Token: 0x060022D3 RID: 8915 RVA: 0x00085DB8 File Offset: 0x00083FB8
	private void Setup()
	{
		if (this.locked)
		{
			return;
		}
		if (this.recurseLock)
		{
			Debug.LogError("Recursion in setup hit itself, some VisClass has super set to something which references itself", this);
			return;
		}
		this.recurseLock = true;
		List<VisQuery> list = new List<VisQuery>();
		HashSet<VisQuery> hashSet = new HashSet<VisQuery>();
		Dictionary<string, VisQuery> dictionary = new Dictionary<string, VisQuery>();
		if (this._super)
		{
			this._super.Setup();
			if (this.keys != null)
			{
				for (int i = 0; i < this.keys.Length; i++)
				{
					string text = this.keys[i];
					if (!string.IsNullOrEmpty(text))
					{
						VisQuery visQuery = this.values[i];
						int num;
						if (this._super.members.TryGetValue(text, out num))
						{
							VisQuery visQuery2 = this._super.instance[num];
							if (visQuery2 == visQuery)
							{
								if (visQuery2 != null)
								{
									hashSet.Add(visQuery2);
									dictionary.Add(text, visQuery2);
								}
							}
							else if (visQuery != null)
							{
								dictionary.Add(text, visQuery);
								hashSet.Add(visQuery);
							}
						}
						else if (visQuery != null)
						{
							dictionary.Add(text, visQuery);
							hashSet.Add(visQuery);
						}
					}
				}
			}
			this.BuildMembers(list, hashSet);
		}
		else
		{
			for (int j = 0; j < this.keys.Length; j++)
			{
				string text2 = this.keys[j];
				if (!string.IsNullOrEmpty(text2))
				{
					VisQuery visQuery3 = this.values[j];
					if (!(visQuery3 == null))
					{
						dictionary.Add(text2, visQuery3);
						if (hashSet.Add(visQuery3))
						{
							list.Add(visQuery3);
						}
					}
				}
			}
		}
		this.members = new Dictionary<string, int>(dictionary.Count);
		foreach (KeyValuePair<string, VisQuery> keyValuePair in dictionary)
		{
			this.members.Add(keyValuePair.Key, list.IndexOf(keyValuePair.Value));
		}
		this.instance = list.ToArray();
		this.recurseLock = false;
		this.locked = true;
	}

	// Token: 0x17000880 RID: 2176
	// (get) Token: 0x060022D4 RID: 8916 RVA: 0x00086024 File Offset: 0x00084224
	public VisClass.Handle handle
	{
		get
		{
			if (!this.locked)
			{
				this.Setup();
				if (!this.locked)
				{
					return new VisClass.Handle(null);
				}
			}
			return new VisClass.Handle(this);
		}
	}

	// Token: 0x0400107F RID: 4223
	[SerializeField]
	private VisClass _super;

	// Token: 0x04001080 RID: 4224
	[SerializeField]
	private string[] keys;

	// Token: 0x04001081 RID: 4225
	[SerializeField]
	private VisQuery[] values;

	// Token: 0x04001082 RID: 4226
	[NonSerialized]
	private VisQuery[] instance;

	// Token: 0x04001083 RID: 4227
	[NonSerialized]
	private Dictionary<string, int> members;

	// Token: 0x04001084 RID: 4228
	[NonSerialized]
	private bool locked;

	// Token: 0x04001085 RID: 4229
	[NonSerialized]
	private bool recurseLock;

	// Token: 0x04001086 RID: 4230
	private static readonly VisQuery.Instance[] none = new VisQuery.Instance[0];

	// Token: 0x02000397 RID: 919
	public class Rep
	{
		// Token: 0x060022D7 RID: 8919 RVA: 0x00086068 File Offset: 0x00084268
		private static bool MarkModified(VisClass.Rep.Setting setting)
		{
			if (VisClass.Rep.building)
			{
				return false;
			}
			setting.rep.modifiedSettings.Add(setting);
			return true;
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x0008608C File Offset: 0x0008428C
		internal static void Recur(ref VisClass.Rep rep, VisClass klass)
		{
			if (klass._super)
			{
				VisClass.Rep.Recur(ref rep, klass._super);
				foreach (VisClass.Rep.Setting setting in rep.dict.Values)
				{
					setting.isInherited = true;
				}
				for (int i = 0; i < klass.keys.Length; i++)
				{
					string text = klass.keys[i];
					if (!string.IsNullOrEmpty(text))
					{
						VisQuery visQuery = klass.values[i];
						VisClass.Rep.Setting setting2;
						if (!rep.dict.TryGetValue(text, out setting2))
						{
							if (visQuery == null)
							{
								goto IL_F7;
							}
							setting2 = new VisClass.Rep.Setting(text, klass, rep);
							rep.dict.Add(text, setting2);
						}
						else
						{
							setting2 = (rep.dict[text] = setting2.Override(klass));
						}
						setting2.isInherited = false;
						setting2.query = visQuery;
					}
					IL_F7:;
				}
			}
			else
			{
				rep = new VisClass.Rep();
				rep.klass = VisClass.Rep.nklass;
				rep.dict = new Dictionary<string, VisClass.Rep.Setting>();
				for (int j = 0; j < klass.keys.Length; j++)
				{
					string text2 = klass.keys[j];
					if (!string.IsNullOrEmpty(text2))
					{
						VisQuery visQuery2 = klass.values[j];
						if (!(visQuery2 == null))
						{
							VisClass.Rep.Setting setting3 = new VisClass.Rep.Setting(text2, klass, rep);
							setting3.query = visQuery2;
							rep.dict.Add(text2, setting3);
						}
					}
				}
			}
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x00086260 File Offset: 0x00084460
		internal static void Ref(ref VisClass.Rep rep, VisClass klass)
		{
			if (rep == null)
			{
				VisClass.Rep.nklass = klass;
				VisClass.Rep.building = true;
				VisClass.Rep.Recur(ref rep, klass);
				VisClass.Rep.building = false;
				VisClass.Rep.nklass = null;
			}
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x00086294 File Offset: 0x00084494
		private void Remove(VisClass.Rep.Setting setting)
		{
			for (int i = 0; i < this.klass.keys.Length; i++)
			{
				if (this.klass.keys[i] == setting.name)
				{
					int num = i;
					while (++num < this.klass.keys.Length)
					{
						this.klass.keys[num - 1] = this.klass.keys[num];
						this.klass.values[num - 1] = this.klass.values[num];
					}
					Array.Resize<string>(ref this.klass.keys, this.klass.keys.Length - 1);
					Array.Resize<VisQuery>(ref this.klass.values, this.klass.values.Length - 1);
					break;
				}
			}
			if (setting.isOverride)
			{
				this.dict[setting.name] = setting.MoveBack();
			}
			else
			{
				this.dict.Remove(setting.name);
			}
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x000863B0 File Offset: 0x000845B0
		private void Change(VisClass.Rep.Setting setting)
		{
			if (setting.isInherited)
			{
				VisQuery valueSet = setting.valueSet;
				setting = (this.dict[setting.name] = setting.Override(this.klass));
				setting.isInherited = false;
				setting.valueSet = valueSet;
				Array.Resize<string>(ref this.klass.keys, this.klass.keys.Length + 1);
				Array.Resize<VisQuery>(ref this.klass.values, this.klass.values.Length + 1);
				this.klass.keys[this.klass.keys.Length - 1] = setting.name;
				this.klass.values[this.klass.values.Length - 1] = valueSet;
			}
			else
			{
				for (int i = 0; i < this.klass.keys.Length; i++)
				{
					if (this.klass.keys[i] == setting.name)
					{
						this.klass.values[i] = setting.query;
						break;
					}
				}
			}
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x000864D0 File Offset: 0x000846D0
		internal bool Apply()
		{
			if (this.modifiedSettings.Count == 0)
			{
				return false;
			}
			foreach (VisClass.Rep.Setting setting in this.modifiedSettings)
			{
				VisClass.Rep.Action action = setting.action;
				if (action != VisClass.Rep.Action.Revert)
				{
					if (action == VisClass.Rep.Action.Value)
					{
						if (setting.valueSet == null && !setting.isOverride)
						{
							this.Remove(setting);
						}
						else
						{
							this.Change(setting);
						}
					}
				}
				else
				{
					this.Remove(setting);
				}
				setting.action = VisClass.Rep.Action.None;
			}
			return true;
		}

		// Token: 0x04001087 RID: 4231
		internal static VisClass nklass;

		// Token: 0x04001088 RID: 4232
		internal VisClass klass;

		// Token: 0x04001089 RID: 4233
		private static bool building;

		// Token: 0x0400108A RID: 4234
		private HashSet<VisClass.Rep.Setting> modifiedSettings = new HashSet<VisClass.Rep.Setting>();

		// Token: 0x0400108B RID: 4235
		public Dictionary<string, VisClass.Rep.Setting> dict;

		// Token: 0x02000398 RID: 920
		internal enum Action
		{
			// Token: 0x0400108D RID: 4237
			None,
			// Token: 0x0400108E RID: 4238
			Revert,
			// Token: 0x0400108F RID: 4239
			Value
		}

		// Token: 0x02000399 RID: 921
		public class Setting
		{
			// Token: 0x060022DD RID: 8925 RVA: 0x000865A8 File Offset: 0x000847A8
			internal Setting(string key, VisClass klass, VisClass.Rep rep)
			{
				this.key = key;
				this.rep = rep;
				this._inheritedClass = klass;
			}

			// Token: 0x17000881 RID: 2177
			// (get) Token: 0x060022DE RID: 8926 RVA: 0x000865C8 File Offset: 0x000847C8
			internal string name
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x17000882 RID: 2178
			// (get) Token: 0x060022DF RID: 8927 RVA: 0x000865D0 File Offset: 0x000847D0
			private VisClass inheritedClass
			{
				get
				{
					return this._inheritedClass;
				}
			}

			// Token: 0x060022E0 RID: 8928 RVA: 0x000865D8 File Offset: 0x000847D8
			internal VisClass.Rep.Setting Override(VisClass klass)
			{
				VisClass.Rep.Setting setting = (VisClass.Rep.Setting)base.MemberwiseClone();
				setting._inheritedClass = klass;
				setting._hasSuper = true;
				setting._inheritSetting = this;
				return setting;
			}

			// Token: 0x17000883 RID: 2179
			// (get) Token: 0x060022E1 RID: 8929 RVA: 0x00086608 File Offset: 0x00084808
			// (set) Token: 0x060022E2 RID: 8930 RVA: 0x00086610 File Offset: 0x00084810
			public bool isInherited
			{
				get
				{
					return this._isInherited;
				}
				set
				{
					if (this._isInherited != value)
					{
						this._isInherited = value;
						if (VisClass.Rep.MarkModified(this))
						{
							this.action = VisClass.Rep.Action.Revert;
						}
					}
				}
			}

			// Token: 0x17000884 RID: 2180
			// (get) Token: 0x060022E3 RID: 8931 RVA: 0x00086638 File Offset: 0x00084838
			public bool isOverride
			{
				get
				{
					return this._hasSuper;
				}
			}

			// Token: 0x17000885 RID: 2181
			// (get) Token: 0x060022E4 RID: 8932 RVA: 0x00086640 File Offset: 0x00084840
			public VisQuery superQuery
			{
				get
				{
					return (!this._hasSuper) ? null : this._inheritSetting.query;
				}
			}

			// Token: 0x17000886 RID: 2182
			// (get) Token: 0x060022E5 RID: 8933 RVA: 0x00086660 File Offset: 0x00084860
			// (set) Token: 0x060022E6 RID: 8934 RVA: 0x00086668 File Offset: 0x00084868
			public VisQuery query
			{
				get
				{
					return this._value;
				}
				set
				{
					if (this._isInherited)
					{
						VisClass.Rep.MarkModified(this);
					}
					else if (this._value == value)
					{
						return;
					}
					if (VisClass.Rep.MarkModified(this))
					{
						this.action = VisClass.Rep.Action.Value;
						this._valueSet = value;
					}
					else
					{
						this._value = value;
					}
				}
			}

			// Token: 0x060022E7 RID: 8935 RVA: 0x000866C4 File Offset: 0x000848C4
			internal VisClass.Rep.Setting MoveBack()
			{
				return this._inheritSetting;
			}

			// Token: 0x17000887 RID: 2183
			// (get) Token: 0x060022E8 RID: 8936 RVA: 0x000866CC File Offset: 0x000848CC
			// (set) Token: 0x060022E9 RID: 8937 RVA: 0x000866D4 File Offset: 0x000848D4
			internal VisQuery valueSet
			{
				get
				{
					return this._valueSet;
				}
				set
				{
					this._value = value;
				}
			}

			// Token: 0x04001090 RID: 4240
			internal VisClass.Rep rep;

			// Token: 0x04001091 RID: 4241
			internal VisClass.Rep.Action action;

			// Token: 0x04001092 RID: 4242
			private bool _unchanged;

			// Token: 0x04001093 RID: 4243
			private bool _isInherited;

			// Token: 0x04001094 RID: 4244
			private bool _hasSuper;

			// Token: 0x04001095 RID: 4245
			private VisQuery _value;

			// Token: 0x04001096 RID: 4246
			private VisQuery _valueSet;

			// Token: 0x04001097 RID: 4247
			private VisClass _inheritedClass;

			// Token: 0x04001098 RID: 4248
			private VisClass.Rep.Setting _inheritSetting;

			// Token: 0x04001099 RID: 4249
			private string key;
		}
	}

	// Token: 0x0200039A RID: 922
	public struct Handle
	{
		// Token: 0x060022EA RID: 8938 RVA: 0x000866E0 File Offset: 0x000848E0
		internal Handle(VisClass klass)
		{
			this.klass = klass;
			this.bits = 0L;
			if (klass)
			{
				int num = 0;
				this.queries = new VisQuery.Instance[klass.instance.Length];
				for (int i = 0; i < this.queries.Length; i++)
				{
					this.queries[i] = new VisQuery.Instance(klass.instance[i], ref num);
				}
			}
			else
			{
				this.queries = VisClass.none;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x0008675C File Offset: 0x0008495C
		public bool valid
		{
			get
			{
				return this.queries != null;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x0008676C File Offset: 0x0008496C
		public int Length
		{
			get
			{
				return this.klass.instance.Length;
			}
		}

		// Token: 0x1700088A RID: 2186
		public VisQuery.Instance this[int i]
		{
			get
			{
				return this.queries[i];
			}
		}

		// Token: 0x1700088B RID: 2187
		public VisQuery.Instance this[string name]
		{
			get
			{
				return this.queries[this.klass.members[name]];
			}
		}

		// Token: 0x0400109A RID: 4250
		private readonly VisClass klass;

		// Token: 0x0400109B RID: 4251
		private readonly VisQuery.Instance[] queries;

		// Token: 0x0400109C RID: 4252
		private long bits;
	}
}
