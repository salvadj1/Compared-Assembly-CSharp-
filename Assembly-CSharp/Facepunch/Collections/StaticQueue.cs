using System;

namespace Facepunch.Collections
{
	// Token: 0x0200019E RID: 414
	public abstract class StaticQueue<KEY, T> where T : class
	{
		// Token: 0x06000C8D RID: 3213 RVA: 0x00030370 File Offset: 0x0002E570
		protected static bool validate(T instance, ref StaticQueue<KEY, T>.Entry state, bool must_be_contained = false)
		{
			return (!state.inside) ? (!must_be_contained) : (!object.ReferenceEquals(state.node, null) && object.ReferenceEquals(state.node.v, instance));
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x000303C0 File Offset: 0x0002E5C0
		protected static bool contains(ref StaticQueue<KEY, T>.Entry state)
		{
			return state.inside;
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x000303C8 File Offset: 0x0002E5C8
		protected static int num
		{
			get
			{
				return StaticQueue<KEY, T>.count;
			}
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x000303D0 File Offset: 0x0002E5D0
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

		// Token: 0x06000C91 RID: 3217 RVA: 0x00030404 File Offset: 0x0002E604
		protected static bool dequeue(T instance, ref StaticQueue<KEY, T>.Entry state)
		{
			if (state.inside)
			{
				state.inside = false;
				return StaticQueue<KEY, T>.reg.dispose(ref state.node);
			}
			return false;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00030428 File Offset: 0x0002E628
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

		// Token: 0x06000C93 RID: 3219 RVA: 0x0003046C File Offset: 0x0002E66C
		protected static bool enrequeue(T instance, ref StaticQueue<KEY, T>.Entry state)
		{
			return (!state.inside) ? StaticQueue<KEY, T>.enqueue(instance, ref state) : StaticQueue<KEY, T>.requeue(instance, ref state);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0003048C File Offset: 0x0002E68C
		protected static bool requeue(T instance, ref StaticQueue<KEY, T>.Entry state, bool enqueue_if_missing)
		{
			return (!enqueue_if_missing) ? StaticQueue<KEY, T>.enqueue(instance, ref state) : StaticQueue<KEY, T>.enrequeue(instance, ref state);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x000304A8 File Offset: 0x0002E6A8
		protected static void drain()
		{
			if (StaticQueue<KEY, T>.reg_made)
			{
				StaticQueue<KEY, T>.reg.drain();
			}
		}

		// Token: 0x0400081D RID: 2077
		private static bool reg_made;

		// Token: 0x0400081E RID: 2078
		private static int count;

		// Token: 0x0200019F RID: 415
		internal struct way
		{
			// Token: 0x0400081F RID: 2079
			public StaticQueue<KEY, T>.node v;

			// Token: 0x04000820 RID: 2080
			public bool e;
		}

		// Token: 0x020001A0 RID: 416
		internal struct fork
		{
			// Token: 0x04000821 RID: 2081
			public StaticQueue<KEY, T>.way p;

			// Token: 0x04000822 RID: 2082
			public StaticQueue<KEY, T>.way n;
		}

		// Token: 0x020001A1 RID: 417
		internal class node
		{
			// Token: 0x04000823 RID: 2083
			public T v;

			// Token: 0x04000824 RID: 2084
			public bool e;

			// Token: 0x04000825 RID: 2085
			public StaticQueue<KEY, T>.fork w;
		}

		// Token: 0x020001A2 RID: 418
		private static class reg
		{
			// Token: 0x06000C97 RID: 3223 RVA: 0x000304C4 File Offset: 0x0002E6C4
			static reg()
			{
				StaticQueue<KEY, T>.reg_made = true;
			}

			// Token: 0x06000C98 RID: 3224 RVA: 0x000304CC File Offset: 0x0002E6CC
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

			// Token: 0x06000C99 RID: 3225 RVA: 0x00030534 File Offset: 0x0002E734
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

			// Token: 0x06000C9A RID: 3226 RVA: 0x000305A4 File Offset: 0x0002E7A4
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

			// Token: 0x06000C9B RID: 3227 RVA: 0x00030648 File Offset: 0x0002E848
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

			// Token: 0x06000C9C RID: 3228 RVA: 0x000306EC File Offset: 0x0002E8EC
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

			// Token: 0x06000C9D RID: 3229 RVA: 0x00030820 File Offset: 0x0002EA20
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

			// Token: 0x06000C9E RID: 3230 RVA: 0x00030854 File Offset: 0x0002EA54
			public static void drain()
			{
				StaticQueue<KEY, T>.reg.dump = null;
				StaticQueue<KEY, T>.reg.dump_size = 0;
			}

			// Token: 0x04000826 RID: 2086
			private static int dump_size;

			// Token: 0x04000827 RID: 2087
			private static StaticQueue<KEY, T>.node dump;

			// Token: 0x04000828 RID: 2088
			internal static StaticQueue<KEY, T>.node first;

			// Token: 0x04000829 RID: 2089
			internal static StaticQueue<KEY, T>.node last;
		}

		// Token: 0x020001A3 RID: 419
		protected enum act
		{
			// Token: 0x0400082B RID: 2091
			none,
			// Token: 0x0400082C RID: 2092
			front,
			// Token: 0x0400082D RID: 2093
			back,
			// Token: 0x0400082E RID: 2094
			delist
		}

		// Token: 0x020001A4 RID: 420
		protected struct iterator
		{
			// Token: 0x06000C9F RID: 3231 RVA: 0x00030864 File Offset: 0x0002EA64
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

			// Token: 0x06000CA0 RID: 3232 RVA: 0x00030918 File Offset: 0x0002EB18
			public iterator(int maxIter)
			{
				this = new StaticQueue<KEY, T>.iterator(maxIter, (maxIter >= StaticQueue<KEY, T>.count) ? 0 : (StaticQueue<KEY, T>.count - maxIter));
			}

			// Token: 0x06000CA1 RID: 3233 RVA: 0x0003093C File Offset: 0x0002EB3C
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

			// Token: 0x06000CA2 RID: 3234 RVA: 0x000309BC File Offset: 0x0002EBBC
			public bool Validate(ref StaticQueue<KEY, T>.Entry key)
			{
				return key.inside;
			}

			// Token: 0x06000CA3 RID: 3235 RVA: 0x000309C4 File Offset: 0x0002EBC4
			public bool MissingNext(out T v)
			{
				if (this.fail_left-- > 0)
				{
					this.position--;
				}
				StaticQueue<KEY, T>.reg.dispose(ref this.node);
				return this.Start(out v);
			}

			// Token: 0x06000CA4 RID: 3236 RVA: 0x00030A0C File Offset: 0x0002EC0C
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

			// Token: 0x0400082F RID: 2095
			private int attempts;

			// Token: 0x04000830 RID: 2096
			private int fail_left;

			// Token: 0x04000831 RID: 2097
			private int position;

			// Token: 0x04000832 RID: 2098
			private StaticQueue<KEY, T>.node node;

			// Token: 0x04000833 RID: 2099
			private StaticQueue<KEY, T>.node next;
		}

		// Token: 0x020001A5 RID: 421
		public struct Entry
		{
			// Token: 0x04000834 RID: 2100
			internal bool inside;

			// Token: 0x04000835 RID: 2101
			internal StaticQueue<KEY, T>.node node;
		}
	}
}
