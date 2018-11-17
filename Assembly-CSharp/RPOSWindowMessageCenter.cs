using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200041F RID: 1055
public struct RPOSWindowMessageCenter
{
	// Token: 0x06002744 RID: 10052 RVA: 0x00098E88 File Offset: 0x00097088
	public void Fire(RPOSWindow window, RPOSWindowMessage message)
	{
		if (this.init && message >= RPOSWindowMessage.WillShow && message <= RPOSWindowMessage.DidHide)
		{
			this.responders[message - RPOSWindowMessage.WillShow].Invoke(window, message);
		}
	}

	// Token: 0x06002745 RID: 10053 RVA: 0x00098EC4 File Offset: 0x000970C4
	public bool Add(RPOSWindowMessage message, RPOSWindowMessageHandler handler)
	{
		if (message < RPOSWindowMessage.WillShow || message > RPOSWindowMessage.DidHide || handler == null)
		{
			return false;
		}
		if (!this.init)
		{
			this.responders = new RPOSWindowMessageCenter.RPOSWindowMessageResponder[4];
			this.init = true;
		}
		return this.responders[message - RPOSWindowMessage.WillShow].Add(handler);
	}

	// Token: 0x06002746 RID: 10054 RVA: 0x00098F1C File Offset: 0x0009711C
	public bool Remove(RPOSWindowMessage message, RPOSWindowMessageHandler handler)
	{
		return this.init && message >= RPOSWindowMessage.WillShow && message <= RPOSWindowMessage.DidHide && handler != null && this.responders[message - RPOSWindowMessage.WillShow].Remove(handler);
	}

	// Token: 0x06002747 RID: 10055 RVA: 0x00098F54 File Offset: 0x00097154
	public IEnumerable<RPOSWindowMessageHandler> EnumerateHandlers(RPOSWindowMessage message)
	{
		if (!this.init || message < RPOSWindowMessage.WillShow || message > RPOSWindowMessage.DidHide)
		{
			return RPOSWindowMessageCenter.none;
		}
		int num = message - RPOSWindowMessage.WillShow;
		if (!this.responders[num].init || this.responders[num].count == 0)
		{
			return RPOSWindowMessageCenter.none;
		}
		return this.responders[num].handlers;
	}

	// Token: 0x06002748 RID: 10056 RVA: 0x00098FC8 File Offset: 0x000971C8
	public int CountHandlers(RPOSWindowMessage message)
	{
		return (this.init && message >= RPOSWindowMessage.WillShow && message <= RPOSWindowMessage.DidHide) ? this.responders[message - RPOSWindowMessage.WillShow].count : 0;
	}

	// Token: 0x0400134A RID: 4938
	public const RPOSWindowMessage kBegin = RPOSWindowMessage.WillShow;

	// Token: 0x0400134B RID: 4939
	public const RPOSWindowMessage kLast = RPOSWindowMessage.DidHide;

	// Token: 0x0400134C RID: 4940
	public const RPOSWindowMessage kEnd = RPOSWindowMessage.WillClose;

	// Token: 0x0400134D RID: 4941
	public const int kMessageCount = 4;

	// Token: 0x0400134E RID: 4942
	private RPOSWindowMessageCenter.RPOSWindowMessageResponder[] responders;

	// Token: 0x0400134F RID: 4943
	private bool init;

	// Token: 0x04001350 RID: 4944
	private static readonly RPOSWindowMessageHandler[] none = new RPOSWindowMessageHandler[0];

	// Token: 0x02000420 RID: 1056
	private struct RPOSWindowMessageResponder
	{
		// Token: 0x06002749 RID: 10057 RVA: 0x00099008 File Offset: 0x00097208
		public bool Add(RPOSWindowMessageHandler handler)
		{
			if (handler == null)
			{
				return false;
			}
			if (!this.init)
			{
				this.handlerGate = new HashSet<RPOSWindowMessageHandler>();
				this.handlers = new List<RPOSWindowMessageHandler>();
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

		// Token: 0x0600274A RID: 10058 RVA: 0x00099088 File Offset: 0x00097288
		public bool Remove(RPOSWindowMessageHandler handler)
		{
			if (!this.init || handler == null || !this.handlerGate.Remove(handler))
			{
				return false;
			}
			this.handlers.Remove(handler);
			this.count--;
			return true;
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x000990D8 File Offset: 0x000972D8
		private bool TryInvoke(RPOSWindow window, RPOSWindowMessage message, int i)
		{
			RPOSWindowMessageHandler rposwindowMessageHandler = this.handlers[i];
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

		// Token: 0x0600274C RID: 10060 RVA: 0x0009916C File Offset: 0x0009736C
		public void Invoke(RPOSWindow window, RPOSWindowMessage message)
		{
			if (!this.init || this.count == 0)
			{
				return;
			}
			if ((message - RPOSWindowMessage.WillShow & 1) == 1)
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

		// Token: 0x04001351 RID: 4945
		public HashSet<RPOSWindowMessageHandler> handlerGate;

		// Token: 0x04001352 RID: 4946
		public List<RPOSWindowMessageHandler> handlers;

		// Token: 0x04001353 RID: 4947
		public int count;

		// Token: 0x04001354 RID: 4948
		public bool init;
	}
}
