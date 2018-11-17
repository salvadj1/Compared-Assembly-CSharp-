using System;
using System.Collections;
using UnityEngine;

namespace Facepunch.Load
{
	// Token: 0x020002A4 RID: 676
	public static class Utility
	{
		// Token: 0x0600180B RID: 6155 RVA: 0x0005AA08 File Offset: 0x00058C08
		public static string GetBuildInvariantTypeName(this Type type)
		{
			string text = type.Assembly.FullName;
			int num = text.IndexOf(',');
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			return type.FullName + ", " + text;
		}

		// Token: 0x020002A5 RID: 677
		public sealed class ReferenceCountedCoroutine : IEnumerator
		{
			// Token: 0x0600180C RID: 6156 RVA: 0x0005AA4C File Offset: 0x00058C4C
			private ReferenceCountedCoroutine(Utility.ReferenceCountedCoroutine.Runner runner, Utility.ReferenceCountedCoroutine.Callback callback, object yieldInstruction, object tag, bool skipOnce)
			{
				this.runner = runner;
				this.callback = callback;
				this.yieldInstruction = yieldInstruction;
				this.tag = tag;
				this.skipOnce = skipOnce;
			}

			// Token: 0x170006CA RID: 1738
			// (get) Token: 0x0600180D RID: 6157 RVA: 0x0005AA7C File Offset: 0x00058C7C
			object IEnumerator.Current
			{
				get
				{
					return this.yieldInstruction;
				}
			}

			// Token: 0x0600180E RID: 6158 RVA: 0x0005AA84 File Offset: 0x00058C84
			void IEnumerator.Reset()
			{
			}

			// Token: 0x0600180F RID: 6159 RVA: 0x0005AA88 File Offset: 0x00058C88
			bool IEnumerator.MoveNext()
			{
				if (this.skipOnce)
				{
					this.skipOnce = false;
					return true;
				}
				bool flag;
				try
				{
					flag = this.callback(ref this.yieldInstruction, ref this.tag);
				}
				catch (Exception ex)
				{
					flag = false;
					Debug.LogException(ex);
				}
				if (!flag)
				{
					this.runner.Release();
					this.tag = null;
					this.yieldInstruction = null;
					return false;
				}
				return true;
			}

			// Token: 0x04000CBA RID: 3258
			private readonly Utility.ReferenceCountedCoroutine.Runner runner;

			// Token: 0x04000CBB RID: 3259
			private readonly Utility.ReferenceCountedCoroutine.Callback callback;

			// Token: 0x04000CBC RID: 3260
			private object tag;

			// Token: 0x04000CBD RID: 3261
			private object yieldInstruction;

			// Token: 0x04000CBE RID: 3262
			private bool skipOnce;

			// Token: 0x020002A6 RID: 678
			public sealed class Runner
			{
				// Token: 0x06001810 RID: 6160 RVA: 0x0005AB14 File Offset: 0x00058D14
				public Runner(string gameObjectName)
				{
					this.gameObjectName = gameObjectName;
				}

				// Token: 0x06001811 RID: 6161 RVA: 0x0005AB24 File Offset: 0x00058D24
				public void Retain()
				{
					if (this.refCount++ == 0)
					{
						this.go = new GameObject(this.gameObjectName, new Type[]
						{
							typeof(MonoBehaviour)
						});
						Object.DontDestroyOnLoad(this.go);
						this.script = this.go.GetComponent<MonoBehaviour>();
					}
				}

				// Token: 0x06001812 RID: 6162 RVA: 0x0005AB88 File Offset: 0x00058D88
				public Coroutine Install(Utility.ReferenceCountedCoroutine.Callback callback, object tag, object defaultYieldInstruction, bool skipFirst)
				{
					this.Retain();
					return this.script.StartCoroutine(new Utility.ReferenceCountedCoroutine(this, callback, defaultYieldInstruction, tag, skipFirst));
				}

				// Token: 0x06001813 RID: 6163 RVA: 0x0005ABB4 File Offset: 0x00058DB4
				public void Release()
				{
					if (--this.refCount == 0)
					{
						Object.Destroy(this.go);
						Object.Destroy(this.script);
						this.go = null;
						this.script = null;
					}
				}

				// Token: 0x04000CBF RID: 3263
				private readonly string gameObjectName;

				// Token: 0x04000CC0 RID: 3264
				private GameObject go;

				// Token: 0x04000CC1 RID: 3265
				private MonoBehaviour script;

				// Token: 0x04000CC2 RID: 3266
				private int refCount;
			}

			// Token: 0x020002A7 RID: 679
			// (Invoke) Token: 0x06001815 RID: 6165
			public delegate bool Callback(ref object yieldInstruction, ref object tag);
		}
	}
}
