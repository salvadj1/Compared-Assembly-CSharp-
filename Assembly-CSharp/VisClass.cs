using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000443 RID: 1091
public class VisClass : ScriptableObject
{
	// Token: 0x170008DD RID: 2269
	// (get) Token: 0x0600262F RID: 9775 RVA: 0x0008AFDC File Offset: 0x000891DC
	public global::VisClass superClass
	{
		get
		{
			return this._super;
		}
	}

	// Token: 0x06002630 RID: 9776 RVA: 0x0008AFE4 File Offset: 0x000891E4
	public void EditorOnly_Rep(ref global::VisClass.Rep rep)
	{
		if (this.keys == null && this.values == null)
		{
			this.keys = new string[0];
			this.values = new global::VisQuery[0];
		}
		global::VisClass.Rep.Ref(ref rep, this);
	}

	// Token: 0x06002631 RID: 9777 RVA: 0x0008B01C File Offset: 0x0008921C
	public bool EditorOnly_Apply(ref global::VisClass.Rep rep)
	{
		return rep != null && rep.Apply();
	}

	// Token: 0x06002632 RID: 9778 RVA: 0x0008B030 File Offset: 0x00089230
	public void EditorOnly_Add(ref global::VisClass.Rep rep, string key, global::VisQuery value)
	{
		Array.Resize<string>(ref this.keys, this.keys.Length + 1);
		Array.Resize<global::VisQuery>(ref this.values, this.values.Length + 1);
		this.keys[this.keys.Length - 1] = key;
		this.values[this.values.Length - 1] = value;
		rep = null;
	}

	// Token: 0x06002633 RID: 9779 RVA: 0x0008B090 File Offset: 0x00089290
	public bool EditorOnly_SetSuper(ref global::VisClass.Rep rep, global::VisClass _super)
	{
		global::VisClass visClass = _super;
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

	// Token: 0x06002634 RID: 9780 RVA: 0x0008B0F8 File Offset: 0x000892F8
	private void BuildMembers(List<global::VisQuery> list, HashSet<global::VisQuery> hset)
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

	// Token: 0x06002635 RID: 9781 RVA: 0x0008B1B4 File Offset: 0x000893B4
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
		List<global::VisQuery> list = new List<global::VisQuery>();
		HashSet<global::VisQuery> hashSet = new HashSet<global::VisQuery>();
		Dictionary<string, global::VisQuery> dictionary = new Dictionary<string, global::VisQuery>();
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
						global::VisQuery visQuery = this.values[i];
						int num;
						if (this._super.members.TryGetValue(text, out num))
						{
							global::VisQuery visQuery2 = this._super.instance[num];
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
					global::VisQuery visQuery3 = this.values[j];
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
		foreach (KeyValuePair<string, global::VisQuery> keyValuePair in dictionary)
		{
			this.members.Add(keyValuePair.Key, list.IndexOf(keyValuePair.Value));
		}
		this.instance = list.ToArray();
		this.recurseLock = false;
		this.locked = true;
	}

	// Token: 0x170008DE RID: 2270
	// (get) Token: 0x06002636 RID: 9782 RVA: 0x0008B420 File Offset: 0x00089620
	public global::VisClass.Handle handle
	{
		get
		{
			if (!this.locked)
			{
				this.Setup();
				if (!this.locked)
				{
					return new global::VisClass.Handle(null);
				}
			}
			return new global::VisClass.Handle(this);
		}
	}

	// Token: 0x040011E5 RID: 4581
	[SerializeField]
	private global::VisClass _super;

	// Token: 0x040011E6 RID: 4582
	[SerializeField]
	private string[] keys;

	// Token: 0x040011E7 RID: 4583
	[SerializeField]
	private global::VisQuery[] values;

	// Token: 0x040011E8 RID: 4584
	[NonSerialized]
	private global::VisQuery[] instance;

	// Token: 0x040011E9 RID: 4585
	[NonSerialized]
	private Dictionary<string, int> members;

	// Token: 0x040011EA RID: 4586
	[NonSerialized]
	private bool locked;

	// Token: 0x040011EB RID: 4587
	[NonSerialized]
	private bool recurseLock;

	// Token: 0x040011EC RID: 4588
	private static readonly global::VisQuery.Instance[] none = new global::VisQuery.Instance[0];

	// Token: 0x02000444 RID: 1092
	public class Rep
	{
		// Token: 0x06002639 RID: 9785 RVA: 0x0008B464 File Offset: 0x00089664
		private static bool MarkModified(global::VisClass.Rep.Setting setting)
		{
			if (global::VisClass.Rep.building)
			{
				return false;
			}
			setting.rep.modifiedSettings.Add(setting);
			return true;
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x0008B488 File Offset: 0x00089688
		internal static void Recur(ref global::VisClass.Rep rep, global::VisClass klass)
		{
			if (klass._super)
			{
				global::VisClass.Rep.Recur(ref rep, klass._super);
				foreach (global::VisClass.Rep.Setting setting in rep.dict.Values)
				{
					setting.isInherited = true;
				}
				for (int i = 0; i < klass.keys.Length; i++)
				{
					string text = klass.keys[i];
					if (!string.IsNullOrEmpty(text))
					{
						global::VisQuery visQuery = klass.values[i];
						global::VisClass.Rep.Setting setting2;
						if (!rep.dict.TryGetValue(text, out setting2))
						{
							if (visQuery == null)
							{
								goto IL_F7;
							}
							setting2 = new global::VisClass.Rep.Setting(text, klass, rep);
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
				rep = new global::VisClass.Rep();
				rep.klass = global::VisClass.Rep.nklass;
				rep.dict = new Dictionary<string, global::VisClass.Rep.Setting>();
				for (int j = 0; j < klass.keys.Length; j++)
				{
					string text2 = klass.keys[j];
					if (!string.IsNullOrEmpty(text2))
					{
						global::VisQuery visQuery2 = klass.values[j];
						if (!(visQuery2 == null))
						{
							global::VisClass.Rep.Setting setting3 = new global::VisClass.Rep.Setting(text2, klass, rep);
							setting3.query = visQuery2;
							rep.dict.Add(text2, setting3);
						}
					}
				}
			}
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x0008B65C File Offset: 0x0008985C
		internal static void Ref(ref global::VisClass.Rep rep, global::VisClass klass)
		{
			if (rep == null)
			{
				global::VisClass.Rep.nklass = klass;
				global::VisClass.Rep.building = true;
				global::VisClass.Rep.Recur(ref rep, klass);
				global::VisClass.Rep.building = false;
				global::VisClass.Rep.nklass = null;
			}
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x0008B690 File Offset: 0x00089890
		private void Remove(global::VisClass.Rep.Setting setting)
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
					Array.Resize<global::VisQuery>(ref this.klass.values, this.klass.values.Length - 1);
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

		// Token: 0x0600263D RID: 9789 RVA: 0x0008B7AC File Offset: 0x000899AC
		private void Change(global::VisClass.Rep.Setting setting)
		{
			if (setting.isInherited)
			{
				global::VisQuery valueSet = setting.valueSet;
				setting = (this.dict[setting.name] = setting.Override(this.klass));
				setting.isInherited = false;
				setting.valueSet = valueSet;
				Array.Resize<string>(ref this.klass.keys, this.klass.keys.Length + 1);
				Array.Resize<global::VisQuery>(ref this.klass.values, this.klass.values.Length + 1);
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

		// Token: 0x0600263E RID: 9790 RVA: 0x0008B8CC File Offset: 0x00089ACC
		internal bool Apply()
		{
			if (this.modifiedSettings.Count == 0)
			{
				return false;
			}
			foreach (global::VisClass.Rep.Setting setting in this.modifiedSettings)
			{
				global::VisClass.Rep.Action action = setting.action;
				if (action != global::VisClass.Rep.Action.Revert)
				{
					if (action == global::VisClass.Rep.Action.Value)
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
				setting.action = global::VisClass.Rep.Action.None;
			}
			return true;
		}

		// Token: 0x040011ED RID: 4589
		internal static global::VisClass nklass;

		// Token: 0x040011EE RID: 4590
		internal global::VisClass klass;

		// Token: 0x040011EF RID: 4591
		private static bool building;

		// Token: 0x040011F0 RID: 4592
		private HashSet<global::VisClass.Rep.Setting> modifiedSettings = new HashSet<global::VisClass.Rep.Setting>();

		// Token: 0x040011F1 RID: 4593
		public Dictionary<string, global::VisClass.Rep.Setting> dict;

		// Token: 0x02000445 RID: 1093
		internal enum Action
		{
			// Token: 0x040011F3 RID: 4595
			None,
			// Token: 0x040011F4 RID: 4596
			Revert,
			// Token: 0x040011F5 RID: 4597
			Value
		}

		// Token: 0x02000446 RID: 1094
		public class Setting
		{
			// Token: 0x0600263F RID: 9791 RVA: 0x0008B9A4 File Offset: 0x00089BA4
			internal Setting(string key, global::VisClass klass, global::VisClass.Rep rep)
			{
				this.key = key;
				this.rep = rep;
				this._inheritedClass = klass;
			}

			// Token: 0x170008DF RID: 2271
			// (get) Token: 0x06002640 RID: 9792 RVA: 0x0008B9C4 File Offset: 0x00089BC4
			internal string name
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x170008E0 RID: 2272
			// (get) Token: 0x06002641 RID: 9793 RVA: 0x0008B9CC File Offset: 0x00089BCC
			private global::VisClass inheritedClass
			{
				get
				{
					return this._inheritedClass;
				}
			}

			// Token: 0x06002642 RID: 9794 RVA: 0x0008B9D4 File Offset: 0x00089BD4
			internal global::VisClass.Rep.Setting Override(global::VisClass klass)
			{
				global::VisClass.Rep.Setting setting = (global::VisClass.Rep.Setting)base.MemberwiseClone();
				setting._inheritedClass = klass;
				setting._hasSuper = true;
				setting._inheritSetting = this;
				return setting;
			}

			// Token: 0x170008E1 RID: 2273
			// (get) Token: 0x06002643 RID: 9795 RVA: 0x0008BA04 File Offset: 0x00089C04
			// (set) Token: 0x06002644 RID: 9796 RVA: 0x0008BA0C File Offset: 0x00089C0C
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
						if (global::VisClass.Rep.MarkModified(this))
						{
							this.action = global::VisClass.Rep.Action.Revert;
						}
					}
				}
			}

			// Token: 0x170008E2 RID: 2274
			// (get) Token: 0x06002645 RID: 9797 RVA: 0x0008BA34 File Offset: 0x00089C34
			public bool isOverride
			{
				get
				{
					return this._hasSuper;
				}
			}

			// Token: 0x170008E3 RID: 2275
			// (get) Token: 0x06002646 RID: 9798 RVA: 0x0008BA3C File Offset: 0x00089C3C
			public global::VisQuery superQuery
			{
				get
				{
					return (!this._hasSuper) ? null : this._inheritSetting.query;
				}
			}

			// Token: 0x170008E4 RID: 2276
			// (get) Token: 0x06002647 RID: 9799 RVA: 0x0008BA5C File Offset: 0x00089C5C
			// (set) Token: 0x06002648 RID: 9800 RVA: 0x0008BA64 File Offset: 0x00089C64
			public global::VisQuery query
			{
				get
				{
					return this._value;
				}
				set
				{
					if (this._isInherited)
					{
						global::VisClass.Rep.MarkModified(this);
					}
					else if (this._value == value)
					{
						return;
					}
					if (global::VisClass.Rep.MarkModified(this))
					{
						this.action = global::VisClass.Rep.Action.Value;
						this._valueSet = value;
					}
					else
					{
						this._value = value;
					}
				}
			}

			// Token: 0x06002649 RID: 9801 RVA: 0x0008BAC0 File Offset: 0x00089CC0
			internal global::VisClass.Rep.Setting MoveBack()
			{
				return this._inheritSetting;
			}

			// Token: 0x170008E5 RID: 2277
			// (get) Token: 0x0600264A RID: 9802 RVA: 0x0008BAC8 File Offset: 0x00089CC8
			// (set) Token: 0x0600264B RID: 9803 RVA: 0x0008BAD0 File Offset: 0x00089CD0
			internal global::VisQuery valueSet
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

			// Token: 0x040011F6 RID: 4598
			internal global::VisClass.Rep rep;

			// Token: 0x040011F7 RID: 4599
			internal global::VisClass.Rep.Action action;

			// Token: 0x040011F8 RID: 4600
			private bool _unchanged;

			// Token: 0x040011F9 RID: 4601
			private bool _isInherited;

			// Token: 0x040011FA RID: 4602
			private bool _hasSuper;

			// Token: 0x040011FB RID: 4603
			private global::VisQuery _value;

			// Token: 0x040011FC RID: 4604
			private global::VisQuery _valueSet;

			// Token: 0x040011FD RID: 4605
			private global::VisClass _inheritedClass;

			// Token: 0x040011FE RID: 4606
			private global::VisClass.Rep.Setting _inheritSetting;

			// Token: 0x040011FF RID: 4607
			private string key;
		}
	}

	// Token: 0x02000447 RID: 1095
	public struct Handle
	{
		// Token: 0x0600264C RID: 9804 RVA: 0x0008BADC File Offset: 0x00089CDC
		internal Handle(global::VisClass klass)
		{
			this.klass = klass;
			this.bits = 0L;
			if (klass)
			{
				int num = 0;
				this.queries = new global::VisQuery.Instance[klass.instance.Length];
				for (int i = 0; i < this.queries.Length; i++)
				{
					this.queries[i] = new global::VisQuery.Instance(klass.instance[i], ref num);
				}
			}
			else
			{
				this.queries = global::VisClass.none;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x0008BB58 File Offset: 0x00089D58
		public bool valid
		{
			get
			{
				return this.queries != null;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x0600264E RID: 9806 RVA: 0x0008BB68 File Offset: 0x00089D68
		public int Length
		{
			get
			{
				return this.klass.instance.Length;
			}
		}

		// Token: 0x170008E8 RID: 2280
		public global::VisQuery.Instance this[int i]
		{
			get
			{
				return this.queries[i];
			}
		}

		// Token: 0x170008E9 RID: 2281
		public global::VisQuery.Instance this[string name]
		{
			get
			{
				return this.queries[this.klass.members[name]];
			}
		}

		// Token: 0x04001200 RID: 4608
		private readonly global::VisClass klass;

		// Token: 0x04001201 RID: 4609
		private readonly global::VisQuery.Instance[] queries;

		// Token: 0x04001202 RID: 4610
		private long bits;
	}
}
