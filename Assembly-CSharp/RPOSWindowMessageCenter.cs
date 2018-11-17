using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004D4 RID: 1236
public struct RPOSWindowMessageCenter
{
	// Token: 0x06002ACE RID: 10958 RVA: 0x0009ED4C File Offset: 0x0009CF4C
	public void Fire(global::RPOSWindow window, global::RPOSWindowMessage message)
	{
		if (this.init && message >= global::RPOSWindowMessage.WillShow && message <= global::RPOSWindowMessage.DidHide)
		{
			this.responders[message - global::RPOSWindowMessage.WillShow].Invoke(window, message);
		}
	}

	// Token: 0x06002ACF RID: 10959 RVA: 0x0009ED88 File Offset: 0x0009CF88
	public bool Add(global::RPOSWindowMessage message, global::RPOSWindowMessageHandler handler)
	{
		if (message < global::RPOSWindowMessage.WillShow || message > global::RPOSWindowMessage.DidHide || handler == null)
		{
			return false;
		}
		if (!this.init)
		{
			this.responders = new global::RPOSWindowMessageCenter.RPOSWindowMessageResponder[4];
			this.init = true;
		}
		return this.responders[message - global::RPOSWindowMessage.WillShow].Add(handler);
	}

	// Token: 0x06002AD0 RID: 10960 RVA: 0x0009EDE0 File Offset: 0x0009CFE0
	public bool Remove(global::RPOSWindowMessage message, global::RPOSWindowMessageHandler handler)
	{
		return this.init && message >= global::RPOSWindowMessage.WillShow && message <= global::RPOSWindowMessage.DidHide && handler != null && this.responders[message - global::RPOSWindowMessage.WillShow].Remove(handler);
	}

	// Token: 0x06002AD1 RID: 10961 RVA: 0x0009EE18 File Offset: 0x0009D018
	public IEnumerable<global::RPOSWindowMessageHandler> EnumerateHandlers(global::RPOSWindowMessage message)
	{
		if (!this.init || message < global::RPOSWindowMessage.WillShow || message > global::RPOSWindowMessage.DidHide)
		{
			return global::RPOSWindowMessageCenter.none;
		}
		int num = message - global::RPOSWindowMessage.WillShow;
		if (!this.responders[num].init || this.responders[num].count == 0)
		{
			return global::RPOSWindowMessageCenter.none;
		}
		return this.responders[num].handlers;
	}

	// Token: 0x06002AD2 RID: 10962 RVA: 0x0009EE8C File Offset: 0x0009D08C
	public int CountHandlers(global::RPOSWindowMessage message)
	{
		return (this.init && message >= global::RPOSWindowMessage.WillShow && message <= global::RPOSWindowMessage.DidHide) ? this.responders[message - global::RPOSWindowMessage.WillShow].count : 0;
	}

	// Token: 0x040014CA RID: 5322
	public const global::RPOSWindowMessage kBegin = global::RPOSWindowMessage.WillShow;

	// Token: 0x040014CB RID: 5323
	public const global::RPOSWindowMessage kLast = global::RPOSWindowMessage.DidHide;

	// Token: 0x040014CC RID: 5324
	public const global::RPOSWindowMessage kEnd = global::RPOSWindowMessage.WillClose;

	// Token: 0x040014CD RID: 5325
	public const int kMessageCount = 4;

	// Token: 0x040014CE RID: 5326
	private global::RPOSWindowMessageCenter.RPOSWindowMessageResponder[] responders;

	// Token: 0x040014CF RID: 5327
	private bool init;

	// Token: 0x040014D0 RID: 5328
	private static readonly global::RPOSWindowMessageHandler[] none = new global::RPOSWindowMessageHandler[0];

	// Token: 0x020004D5 RID: 1237
	private struct RPOSWindowMessageResponder
	{
		// Token: 0x06002AD3 RID: 10963 RVA: 0x0009EECC File Offset: 0x0009D0CC
		public bool Add(global::RPOSWindowMessageHandler handler)
		{
			if (handler == null)
			{
				return false;
			}
			if (!this.init)
			{
				this.handlerGate = new HashSet<global::RPOSWindowMessageHandler>();
				this.handlers = new List<global::RPOSWindowMessageHandler>();
				this.init = true;
				this.handlerGate.Add(handler);
			}
			else if (!this.handlerGate.Add(handler))
			{
				return false;
			}
			this.handlers.Add(handler);
			this.count++;
			return true;
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x0009EF4C File Offset: 0x0009D14C
		public bool Remove(global::RPOSWindowMessageHandler handler)
		{
			if (!this.init || handler == null || !this.handlerGate.Remove(handler))
			{
				return false;
			}
			this.handlers.Remove(handler);
			this.count--;
			return true;
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x0009EF9C File Offset: 0x0009D19C
		private bool TryInvoke(global::RPOSWindow window, global::RPOSWindowMessage message, int i)
		{
			global::RPOSWindowMessageHandler rposwindowMessageHandler = this.handlers[i];
			bool result;
			try
			{
				result = rposwindowMessageHandler(window, message);
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"handler ",
					rposwindowMessageHandler,
					" threw exception with message ",
					message,
					" on window ",
					window,
					" and will no longer execute. The exception is below\r\n",
					ex
				}), window);
				result = false;
			}
			return result;
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x0009F030 File Offset: 0x0009D230
		public void Invoke(global::RPOSWindow window, global::RPOSWindowMessage message)
		{
			if (!this.init || this.count == 0)
			{
				return;
			}
			if ((message - global::RPOSWindowMessage.WillShow & 1) == 1)
			{
				for (int i = this.count - 1; i >= 0; i--)
				{
					if (!this.TryInvoke(window, message, i))
					{
						this.handlerGate.Remove(this.handlers[i]);
						this.handlers.RemoveAt(i);
						this.count--;
					}
				}
			}
			else
			{
				for (int j = 0; j < this.count; j++)
				{
					if (!this.TryInvoke(window, message, j))
					{
						this.handlerGate.Remove(this.handlers[j]);
						this.handlers.RemoveAt(j--);
						this.count--;
					}
				}
			}
		}

		// Token: 0x040014D1 RID: 5329
		public HashSet<global::RPOSWindowMessageHandler> handlerGate;

		// Token: 0x040014D2 RID: 5330
		public List<global::RPOSWindowMessageHandler> handlers;

		// Token: 0x040014D3 RID: 5331
		public int count;

		// Token: 0x040014D4 RID: 5332
		public bool init;
	}
}
