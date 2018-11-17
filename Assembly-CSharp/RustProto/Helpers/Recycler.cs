using System;
using Google.ProtocolBuffers;

namespace RustProto.Helpers
{
	// Token: 0x0200022C RID: 556
	public sealed class Recycler<TMessage, TBuilder> : IDisposable where TMessage : GeneratedMessage<TMessage, TBuilder> where TBuilder : GeneratedBuilder<TMessage, TBuilder>, new()
	{
		// Token: 0x06000F09 RID: 3849 RVA: 0x00039E48 File Offset: 0x00038048
		private Recycler()
		{
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00039E50 File Offset: 0x00038050
		void IDisposable.Dispose()
		{
			if (!this.Disposed)
			{
				this.Disposed = true;
				int count;
				Recycler<TMessage, TBuilder>.Recovery.Count = (count = Recycler<TMessage, TBuilder>.Recovery.Count) + 1;
				if (count == 0)
				{
					this.Next = null;
					Recycler<TMessage, TBuilder>.Recovery.Pile = this;
				}
				else
				{
					this.Next = Recycler<TMessage, TBuilder>.Recovery.Pile;
					Recycler<TMessage, TBuilder>.Recovery.Pile = this;
				}
				this.OpenCount = 0;
				if (this.Created && !this.Cleared)
				{
					this.Builder.Clear();
					this.Cleared = true;
				}
			}
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00039EF4 File Offset: 0x000380F4
		public static Recycler<TMessage, TBuilder> Manufacture()
		{
			if (Recycler<TMessage, TBuilder>.Recovery.Count == 0)
			{
				return new Recycler<TMessage, TBuilder>();
			}
			Recycler<TMessage, TBuilder> pile = Recycler<TMessage, TBuilder>.Recovery.Pile;
			if (Recycler<TMessage, TBuilder>.Recovery.Count == 1)
			{
				Recycler<TMessage, TBuilder>.Recovery = default(Recycler<TMessage, TBuilder>.Holding);
			}
			else
			{
				Recycler<TMessage, TBuilder>.Recovery.Count = Recycler<TMessage, TBuilder>.Recovery.Count - 1;
				Recycler<TMessage, TBuilder>.Recovery.Pile = pile.Next;
				pile.Next = null;
			}
			pile.Disposed = false;
			return pile;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00039F78 File Offset: 0x00038178
		public TBuilder OpenBuilder()
		{
			if (this.OpenCount++ == 0)
			{
				if (!this.Created)
				{
					this.Builder = Activator.CreateInstance<TBuilder>();
					this.Created = true;
				}
				else
				{
					this.Cleared = false;
				}
			}
			return this.Builder;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00039FCC File Offset: 0x000381CC
		public TBuilder OpenBuilder(TMessage copyFrom)
		{
			TBuilder result = this.OpenBuilder();
			result.MergeFrom(copyFrom);
			return result;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00039FF0 File Offset: 0x000381F0
		public void CloseBuilder(ref TBuilder builder)
		{
			if (this.Disposed)
			{
				throw new ObjectDisposedException("Recycler");
			}
			if (this.OpenCount == 0)
			{
				throw new InvalidOperationException("Close was called more than Open for this Recycler");
			}
			if (!object.ReferenceEquals(builder, this.Builder))
			{
				throw new ArgumentOutOfRangeException("builder", "Was not opened by this recycler");
			}
			builder = (TBuilder)((object)null);
			if (--this.OpenCount == 0 && !this.Cleared)
			{
				this.Builder.Clear();
				this.Cleared = true;
			}
		}

		// Token: 0x0400099B RID: 2459
		private TBuilder Builder;

		// Token: 0x0400099C RID: 2460
		private Recycler<TMessage, TBuilder> Next;

		// Token: 0x0400099D RID: 2461
		private bool Disposed;

		// Token: 0x0400099E RID: 2462
		private bool Cleared;

		// Token: 0x0400099F RID: 2463
		private bool Created;

		// Token: 0x040009A0 RID: 2464
		private int OpenCount;

		// Token: 0x040009A1 RID: 2465
		private static Recycler<TMessage, TBuilder>.Holding Recovery;

		// Token: 0x0200022D RID: 557
		private struct Holding
		{
			// Token: 0x040009A2 RID: 2466
			public Recycler<TMessage, TBuilder> Pile;

			// Token: 0x040009A3 RID: 2467
			public int Count;
		}
	}
}
