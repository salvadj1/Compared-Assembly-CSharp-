using System;
using System.Collections;
using UnityEngine;

namespace Facepunch.Load
{
	// Token: 0x02000270 RID: 624
	public static class Utility
	{
		// Token: 0x060016B1 RID: 5809 RVA: 0x000565C0 File Offset: 0x000547C0
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

		// Token: 0x02000271 RID: 625
		public sealed class ReferenceCountedCoroutine : IEnumerator
		{
			// Token: 0x060016B2 RID: 5810 RVA: 0x00056604 File Offset: 0x00054804
			private ReferenceCountedCoroutine(Utility.ReferenceCountedCoroutine.Runner runner, Utility.ReferenceCountedCoroutine.Callback callback, object yieldInstruction, object tag, bool skipOnce)
			{
				this.runner = runner;
				this.callback = callback;
				this.yieldInstruction = yieldInstruction;
				this.tag = tag;
				this.skipOnce = skipOnce;
			}

			// Token: 0x17000680 RID: 1664
			// (get) Token: 0x060016B3 RID: 5811 RVA: 0x00056634 File Offset: 0x00054834
			object IEnumerator.Current
			{
				get
				{
					return this.yieldInstruction;
				}
			}

			// Token: 0x060016B4 RID: 5812 RVA: 0x0005663C File Offset: 0x0005483C
			void IEnumerator.Reset()
			{
			}

			// Token: 0x060016B5 RID: 5813 RVA: 0x00056640 File Offset: 0x00054840
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

			// Token: 0x04000B94 RID: 2964
			private readonly Utility.ReferenceCountedCoroutine.Runner runner;

			// Token: 0x04000B95 RID: 2965
			private readonly Utility.ReferenceCountedCoroutine.Callback callback;

			// Token: 0x04000B96 RID: 2966
			private object tag;

			// Token: 0x04000B97 RID: 2967
			private object yieldInstruction;

			// Token: 0x04000B98 RID: 2968
			private bool skipOnce;

			// Token: 0x02000272 RID: 626
			public sealed class Runner
			{
				// Token: 0x060016B6 RID: 5814 RVA: 0x000566CC File Offset: 0x000548CC
				public Runner(string gameObjectName)
				{
					this.gameObjectName = gameObjectName;
				}

				// Token: 0x060016B7 RID: 5815 RVA: 0x000566DC File Offset: 0x000548DC
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

				// Token: 0x060016B8 RID: 5816 RVA: 0x00056740 File Offset: 0x00054940
				public Coroutine Install(Utility.ReferenceCountedCoroutine.Callback callback, object tag, object defaultYieldInstruction, bool skipFirst)
				{
					this.Retain();
					return this.script.StartCoroutine(new Utility.ReferenceCountedCoroutine(this, callback, defaultYieldInstruction, tag, skipFirst));
				}

				// Token: 0x060016B9 RID: 5817 RVA: 0x0005676C File Offset: 0x0005496C
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

				// Token: 0x04000B99 RID: 2969
				private readonly string gameObjectName;

				// Token: 0x04000B9A RID: 2970
				private GameObject go;

				// Token: 0x04000B9B RID: 2971
				private MonoBehaviour script;

				// Token: 0x04000B9C RID: 2972
				private int refCount;
			}

			// Token: 0x02000868 RID: 2152
			// (Invoke) Token: 0x06004B78 RID: 19320
			public delegate bool Callback(ref object yieldInstruction, ref object tag);
		}
	}
}
