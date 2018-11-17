using System;
using Google.ProtocolBuffers;

namespace RustProto.Helpers
{
	// Token: 0x020001F9 RID: 505
	public sealed class Recycler<TMessage, TBuilder> : IDisposable where TMessage : GeneratedMessage<TMessage, TBuilder> where TBuilder : GeneratedBuilder<TMessage, TBuilder>, new()
	{
		// Token: 0x06000DB5 RID: 3509 RVA: 0x00035AA0 File Offset: 0x00033CA0
		private Recycler()
		{
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00035AA8 File Offset: 0x00033CA8
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

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00035B4C File Offset: 0x00033D4C
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

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00035BD0 File Offset: 0x00033DD0
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

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00035C24 File Offset: 0x00033E24
		public TBuilder OpenBuilder(TMessage copyFrom)
		{
			TBuilder result = this.OpenBuilder();
			result.MergeFrom(copyFrom);
			return result;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00035C48 File Offset: 0x00033E48
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

		// Token: 0x04000878 RID: 2168
		private TBuilder Builder;

		// Token: 0x04000879 RID: 2169
		private Recycler<TMessage, TBuilder> Next;

		// Token: 0x0400087A RID: 2170
		private bool Disposed;

		// Token: 0x0400087B RID: 2171
		private bool Cleared;

		// Token: 0x0400087C RID: 2172
		private bool Created;

		// Token: 0x0400087D RID: 2173
		private int OpenCount;

		// Token: 0x0400087E RID: 2174
		private static Recycler<TMessage, TBuilder>.Holding Recovery;

		// Token: 0x020001FA RID: 506
		private struct Holding
		{
			// Token: 0x0400087F RID: 2175
			public Recycler<TMessage, TBuilder> Pile;

			// Token: 0x04000880 RID: 2176
			public int Count;
		}
	}
}
