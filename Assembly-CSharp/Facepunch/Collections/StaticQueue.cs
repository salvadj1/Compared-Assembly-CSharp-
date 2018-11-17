using System;

namespace Facepunch.Collections
{
	// Token: 0x02000172 RID: 370
	public abstract class StaticQueue<KEY, T> where T : class
	{
		// Token: 0x06000B5D RID: 2909 RVA: 0x0002C484 File Offset: 0x0002A684
		protected static bool validate(T instance, ref StaticQueue<KEY, T>.Entry state, bool must_be_contained = false)
		{
			return (!state.inside) ? (!must_be_contained) : (!object.ReferenceEquals(state.node, null) && object.ReferenceEquals(state.node.v, instance));
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002C4D4 File Offset: 0x0002A6D4
		protected static bool contains(ref StaticQueue<KEY, T>.Entry state)
		{
			return state.inside;
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0002C4DC File Offset: 0x0002A6DC
		protected static int num
		{
			get
			{
				return StaticQueue<KEY, T>.count;
			}
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002C4E4 File Offset: 0x0002A6E4
		protected static bool enqueue(T instance, ref StaticQueue<KEY, T>.Entry state)
		{
			if (!state.inside)
			{
				state.inside = true;
				state.node = StaticQueue<KEY, T>.reg.insert_end(StaticQueue<KEY, T>.reg.make_node(instance));
				return true;
			}
			return false;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0002C518 File Offset: 0x0002A718
		protected static bool dequeue(T instance, ref StaticQueue<KEY, T>.Entry state)
		{
			if (state.inside)
			{
				state.inside = false;
				return StaticQueue<KEY, T>.reg.dispose(ref state.node);
			}
			return false;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002C53C File Offset: 0x0002A73C
		protected static bool requeue(T instance, ref StaticQueue<KEY, T>.Entry state)
		{
			if (!state.inside)
			{
				return false;
			}
			if (object.ReferenceEquals(StaticQueue<KEY, T>.reg.last, state.node))
			{
				return true;
			}
			state.node = StaticQueue<KEY, T>.reg.insert_end(state.node);
			return true;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0002C580 File Offset: 0x0002A780
		protected static bool enrequeue(T instance, ref StaticQueue<KEY, T>.Entry state)
		{
			return (!state.inside) ? StaticQueue<KEY, T>.enqueue(instance, ref state) : StaticQueue<KEY, T>.requeue(instance, ref state);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002C5A0 File Offset: 0x0002A7A0
		protected static bool requeue(T instance, ref StaticQueue<KEY, T>.Entry state, bool enqueue_if_missing)
		{
			return (!enqueue_if_missing) ? StaticQueue<KEY, T>.enqueue(instance, ref state) : StaticQueue<KEY, T>.enrequeue(instance, ref state);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002C5BC File Offset: 0x0002A7BC
		protected static void drain()
		{
			if (StaticQueue<KEY, T>.reg_made)
			{
				StaticQueue<KEY, T>.reg.drain();
			}
		}

		// Token: 0x04000709 RID: 1801
		private static bool reg_made;

		// Token: 0x0400070A RID: 1802
		private static int count;

		// Token: 0x02000173 RID: 371
		internal struct way
		{
			// Token: 0x0400070B RID: 1803
			public StaticQueue<KEY, T>.node v;

			// Token: 0x0400070C RID: 1804
			public bool e;
		}

		// Token: 0x02000174 RID: 372
		internal struct fork
		{
			// Token: 0x0400070D RID: 1805
			public StaticQueue<KEY, T>.way p;

			// Token: 0x0400070E RID: 1806
			public StaticQueue<KEY, T>.way n;
		}

		// Token: 0x02000175 RID: 373
		internal class node
		{
			// Token: 0x0400070F RID: 1807
			public T v;

			// Token: 0x04000710 RID: 1808
			public bool e;

			// Token: 0x04000711 RID: 1809
			public StaticQueue<KEY, T>.fork w;
		}

		// Token: 0x02000176 RID: 374
		private static class reg
		{
			// Token: 0x06000B67 RID: 2919 RVA: 0x0002C5D8 File Offset: 0x0002A7D8
			static reg()
			{
				StaticQueue<KEY, T>.reg_made = true;
			}

			// Token: 0x06000B68 RID: 2920 RVA: 0x0002C5E0 File Offset: 0x0002A7E0
			internal static StaticQueue<KEY, T>.node make_node(T v)
			{
				StaticQueue<KEY, T>.node node;
				if (StaticQueue<KEY, T>.reg.dump_size > 0)
				{
					StaticQueue<KEY, T>.reg.dump_size--;
					node = StaticQueue<KEY, T>.reg.dump;
					StaticQueue<KEY, T>.reg.dump = node.w.p.v;
					node.w = default(StaticQueue<KEY, T>.fork);
				}
				else
				{
					node = new StaticQueue<KEY, T>.node();
				}
				node.v = v;
				node.e = false;
				return node;
			}

			// Token: 0x06000B69 RID: 2921 RVA: 0x0002C648 File Offset: 0x0002A848
			internal static void delete(StaticQueue<KEY, T>.node r)
			{
				r.v = (T)((object)null);
				r.w.n = default(StaticQueue<KEY, T>.way);
				r.e = false;
				int num = StaticQueue<KEY, T>.reg.dump_size;
				StaticQueue<KEY, T>.reg.dump_size = num + 1;
				r.w.p.e = (num > 0);
				r.w.p.v = StaticQueue<KEY, T>.reg.dump;
				StaticQueue<KEY, T>.reg.dump = r;
			}

			// Token: 0x06000B6A RID: 2922 RVA: 0x0002C6B8 File Offset: 0x0002A8B8
			internal static StaticQueue<KEY, T>.node insert_begin(StaticQueue<KEY, T>.node node)
			{
				if (node.e)
				{
					StaticQueue<KEY, T>.reg.deref(node);
				}
				int count = StaticQueue<KEY, T>.count;
				StaticQueue<KEY, T>.count = count + 1;
				if (count == 0)
				{
					StaticQueue<KEY, T>.reg.first = node;
					StaticQueue<KEY, T>.reg.last = node;
				}
				else
				{
					node.w.n.e = true;
					node.w.n.v = StaticQueue<KEY, T>.reg.first;
					StaticQueue<KEY, T>.reg.first.w.p.e = true;
					StaticQueue<KEY, T>.reg.first.w.p.v = node;
					StaticQueue<KEY, T>.reg.first = node;
				}
				node.e = true;
				return StaticQueue<KEY, T>.reg.first;
			}

			// Token: 0x06000B6B RID: 2923 RVA: 0x0002C75C File Offset: 0x0002A95C
			internal static StaticQueue<KEY, T>.node insert_end(StaticQueue<KEY, T>.node node)
			{
				if (node.e)
				{
					StaticQueue<KEY, T>.reg.deref(node);
				}
				int count = StaticQueue<KEY, T>.count;
				StaticQueue<KEY, T>.count = count + 1;
				if (count == 0)
				{
					StaticQueue<KEY, T>.reg.first = node;
					StaticQueue<KEY, T>.reg.last = node;
				}
				else
				{
					node.w.p.e = true;
					node.w.p.v = StaticQueue<KEY, T>.reg.last;
					StaticQueue<KEY, T>.reg.last.w.n.e = true;
					StaticQueue<KEY, T>.reg.last.w.n.v = node;
					StaticQueue<KEY, T>.reg.last = node;
				}
				node.e = true;
				return StaticQueue<KEY, T>.reg.last;
			}

			// Token: 0x06000B6C RID: 2924 RVA: 0x0002C800 File Offset: 0x0002AA00
			internal static bool deref(StaticQueue<KEY, T>.node node)
			{
				if (object.ReferenceEquals(node, null))
				{
					return false;
				}
				if (node.e)
				{
					if (--StaticQueue<KEY, T>.count == 0)
					{
						StaticQueue<KEY, T>.reg.first = (StaticQueue<KEY, T>.reg.last = null);
					}
					else
					{
						if (node.w.p.e)
						{
							node.w.p.v.w.n = node.w.n;
						}
						else if (node.w.n.e)
						{
							StaticQueue<KEY, T>.reg.first = node.w.n.v;
						}
						if (node.w.n.e)
						{
							node.w.n.v.w.p = node.w.p;
						}
						else if (node.w.p.e)
						{
							StaticQueue<KEY, T>.reg.last = node.w.p.v;
						}
						node.w = default(StaticQueue<KEY, T>.fork);
					}
					node.e = false;
					return true;
				}
				return false;
			}

			// Token: 0x06000B6D RID: 2925 RVA: 0x0002C934 File Offset: 0x0002AB34
			internal static bool dispose(ref StaticQueue<KEY, T>.node node)
			{
				if (StaticQueue<KEY, T>.reg.deref(node))
				{
					node.v = (T)((object)null);
					StaticQueue<KEY, T>.reg.delete(node);
					node = null;
					return true;
				}
				return false;
			}

			// Token: 0x06000B6E RID: 2926 RVA: 0x0002C968 File Offset: 0x0002AB68
			public static void drain()
			{
				StaticQueue<KEY, T>.reg.dump = null;
				StaticQueue<KEY, T>.reg.dump_size = 0;
			}

			// Token: 0x04000712 RID: 1810
			private static int dump_size;

			// Token: 0x04000713 RID: 1811
			private static StaticQueue<KEY, T>.node dump;

			// Token: 0x04000714 RID: 1812
			internal static StaticQueue<KEY, T>.node first;

			// Token: 0x04000715 RID: 1813
			internal static StaticQueue<KEY, T>.node last;
		}

		// Token: 0x02000177 RID: 375
		protected enum act
		{
			// Token: 0x04000717 RID: 1815
			none,
			// Token: 0x04000718 RID: 1816
			front,
			// Token: 0x04000719 RID: 1817
			back,
			// Token: 0x0400071A RID: 1818
			delist
		}

		// Token: 0x02000178 RID: 376
		protected struct iterator
		{
			// Token: 0x06000B6F RID: 2927 RVA: 0x0002C978 File Offset: 0x0002AB78
			public iterator(int maxIterations, int maxFailedIterations)
			{
				if (maxIterations == 0 || maxIterations > StaticQueue<KEY, T>.count)
				{
					this.attempts = StaticQueue<KEY, T>.count;
					this.fail_left = 0;
				}
				else if (maxIterations == StaticQueue<KEY, T>.count)
				{
					this.attempts = StaticQueue<KEY, T>.count;
					this.fail_left = 0;
				}
				else if (maxIterations + maxFailedIterations > StaticQueue<KEY, T>.count)
				{
					this.attempts = maxIterations;
					this.fail_left = StaticQueue<KEY, T>.count - maxIterations;
				}
				else
				{
					this.attempts = maxIterations;
					this.fail_left = maxFailedIterations;
				}
				this.position = 0;
				this.node = null;
				this.next = ((!StaticQueue<KEY, T>.reg_made) ? null : StaticQueue<KEY, T>.reg.first);
			}

			// Token: 0x06000B70 RID: 2928 RVA: 0x0002CA2C File Offset: 0x0002AC2C
			public iterator(int maxIter)
			{
				this = new StaticQueue<KEY, T>.iterator(maxIter, (maxIter >= StaticQueue<KEY, T>.count) ? 0 : (StaticQueue<KEY, T>.count - maxIter));
			}

			// Token: 0x06000B71 RID: 2929 RVA: 0x0002CA50 File Offset: 0x0002AC50
			public bool Start(out T v)
			{
				if (this.position++ < this.attempts)
				{
					this.node = this.next;
					this.next = this.node.w.n.v;
					v = this.node.v;
					return true;
				}
				this.node = (this.next = null);
				v = (T)((object)null);
				return false;
			}

			// Token: 0x06000B72 RID: 2930 RVA: 0x0002CAD0 File Offset: 0x0002ACD0
			public bool Validate(ref StaticQueue<KEY, T>.Entry key)
			{
				return key.inside;
			}

			// Token: 0x06000B73 RID: 2931 RVA: 0x0002CAD8 File Offset: 0x0002ACD8
			public bool MissingNext(out T v)
			{
				if (this.fail_left-- > 0)
				{
					this.position--;
				}
				StaticQueue<KEY, T>.reg.dispose(ref this.node);
				return this.Start(out v);
			}

			// Token: 0x06000B74 RID: 2932 RVA: 0x0002CB20 File Offset: 0x0002AD20
			public bool Next(ref StaticQueue<KEY, T>.Entry prev_key, StaticQueue<KEY, T>.act cmd, out T v)
			{
				bool flag = object.ReferenceEquals(prev_key.node, null);
				if (!flag && !object.ReferenceEquals(prev_key.node, this.node))
				{
					throw new ArgumentException("prev_key did not match that of what was expected", "prev_key");
				}
				if (flag)
				{
					prev_key.inside = false;
				}
				if (!prev_key.inside)
				{
					cmd = StaticQueue<KEY, T>.act.delist;
					if (this.fail_left-- > 0)
					{
						this.position--;
					}
					if (flag)
					{
						StaticQueue<KEY, T>.reg.dispose(ref this.node);
					}
					else
					{
						StaticQueue<KEY, T>.reg.dispose(ref prev_key.node);
					}
				}
				else
				{
					switch (cmd)
					{
					case StaticQueue<KEY, T>.act.front:
						if (StaticQueue<KEY, T>.reg.deref(this.node))
						{
							prev_key.node = StaticQueue<KEY, T>.reg.insert_begin(this.node);
						}
						break;
					case StaticQueue<KEY, T>.act.back:
						if (StaticQueue<KEY, T>.reg.deref(this.node))
						{
							prev_key.node = StaticQueue<KEY, T>.reg.insert_end(this.node);
						}
						break;
					case StaticQueue<KEY, T>.act.delist:
						StaticQueue<KEY, T>.reg.dispose(ref this.node);
						prev_key.node = null;
						break;
					}
				}
				return this.Start(out v);
			}

			// Token: 0x0400071B RID: 1819
			private int attempts;

			// Token: 0x0400071C RID: 1820
			private int fail_left;

			// Token: 0x0400071D RID: 1821
			private int position;

			// Token: 0x0400071E RID: 1822
			private StaticQueue<KEY, T>.node node;

			// Token: 0x0400071F RID: 1823
			private StaticQueue<KEY, T>.node next;
		}

		// Token: 0x02000179 RID: 377
		public struct Entry
		{
			// Token: 0x04000720 RID: 1824
			internal bool inside;

			// Token: 0x04000721 RID: 1825
			internal StaticQueue<KEY, T>.node node;
		}
	}
}
