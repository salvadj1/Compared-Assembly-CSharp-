using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x020003B3 RID: 947
public static class NetErrorHelper
{
	// Token: 0x06002139 RID: 8505 RVA: 0x0007A56C File Offset: 0x0007876C
	static NetErrorHelper()
	{
		global::NetErrorHelper.CacheNiceStrings();
		uLink.NetworkConnectionError networkConnectionError = 0;
		foreach (object obj in Enum.GetValues(typeof(uLink.NetworkConnectionError)))
		{
			if ((int)obj < networkConnectionError)
			{
				networkConnectionError = (int)obj;
			}
		}
		if (networkConnectionError != -5)
		{
			Debug.LogWarning(string.Concat(new object[]
			{
				"Most Negative Base ",
				networkConnectionError,
				" (",
				networkConnectionError,
				")"
			}));
		}
	}

	// Token: 0x0600213A RID: 8506 RVA: 0x0007A654 File Offset: 0x00078854
	public static global::NetError ToNetError(this uLink.NetworkConnectionError error)
	{
		int num = error;
		if (num < -5 && num >> 7 == -1)
		{
			num &= 255;
		}
		return (global::NetError)num;
	}

	// Token: 0x0600213B RID: 8507 RVA: 0x0007A680 File Offset: 0x00078880
	internal static uLink.NetworkConnectionError _uLink(this global::NetError error)
	{
		return error;
	}

	// Token: 0x0600213C RID: 8508 RVA: 0x0007A684 File Offset: 0x00078884
	private static void CacheNiceStrings()
	{
		foreach (object obj in Enum.GetValues(typeof(global::NetError)))
		{
			global::NetError netError = (global::NetError)((int)obj);
			string text = global::NetErrorHelper.BuildNiceString(netError);
			if (text == null && netError != global::NetError.NoError)
			{
				Debug.LogWarning("NetError." + obj + " has no nice string");
				text = global::NetErrorHelper.FallbackNiceString(netError);
			}
			global::NetErrorHelper.niceStrings[netError] = text;
		}
	}

	// Token: 0x0600213D RID: 8509 RVA: 0x0007A738 File Offset: 0x00078938
	private static string FallbackNiceString(global::NetError error)
	{
		string str = error.ToString().Replace("Facepunch_", string.Empty);
		string str2 = "(";
		int num = (int)error;
		return (str + str2 + num.ToString("X") + ")").Replace("_", " ");
	}

	// Token: 0x0600213E RID: 8510 RVA: 0x0007A78C File Offset: 0x0007898C
	public static string NiceString(this global::NetError value)
	{
		string result;
		if (global::NetErrorHelper.niceStrings.TryGetValue(value, out result))
		{
			return result;
		}
		return global::NetErrorHelper.FallbackNiceString(value);
	}

	// Token: 0x0600213F RID: 8511 RVA: 0x0007A7B4 File Offset: 0x000789B4
	private static string BuildNiceString(global::NetError value)
	{
		switch (value)
		{
		case global::NetError.ProxyTargetNotConnected:
			return "Proxy target not connected";
		case global::NetError.ProxyTargetNotRegistered:
			return "Proxy target not registered";
		case global::NetError.ProxyServerNotEnabled:
			return "Proxy server not enabled";
		case global::NetError.ProxyServerOutOfPorts:
			return "Proxy server out of ports";
		default:
			switch (value)
			{
			case global::NetError.NATTargetNotConnected:
				return "NAT target not connected";
			case global::NetError.NATTargetConnectionLost:
				return "NAT target connection lost";
			case global::NetError.NATPunchthroughFailed:
				return "NAT punchthrough";
			case global::NetError.IncompatibleVersions:
				return "Version incompatible";
			default:
				switch (value)
				{
				case global::NetError.ConnectionFailed:
					return "Could not reach the server";
				default:
					switch (value + 5)
					{
					case global::NetError.NoError:
						return "Direct connect failed";
					case (global::NetError)1:
						return "Invalid server";
					case (global::NetError)2:
						return "Incorrect parameters";
					case (global::NetError)3:
						return "Could not create socket or thread";
					case (global::NetError)4:
						return "Already connected to different server";
					case (global::NetError)5:
						return null;
					default:
						if (value == global::NetError.IsAuthoritativeServer)
						{
							return "Authoritative server";
						}
						if (value != global::NetError.ApprovalDenied)
						{
							return null;
						}
						return "You've been denied from connecting";
					}
					break;
				case global::NetError.TooManyConnectedPlayers:
					return "Full";
				case global::NetError.RSAPublicKeyMismatch:
					return "RSA public key mismatch";
				case global::NetError.ConnectionBanned:
					return "Banned from connecting";
				case global::NetError.InvalidPassword:
					return "Invalid password";
				case global::NetError.DetectedDuplicatePlayerID:
					return "Duplicate players identified";
				}
				break;
			case global::NetError.ConnectionTimeout:
				return "Timed out";
			case global::NetError.LimitedPlayers:
				return "Server has limited players";
			}
			break;
		case global::NetError.Facepunch_Kick_ServerRestarting:
			return "Server restarting";
		case global::NetError.Facepunch_Approval_Closed:
			return "Not accepting new connections.";
		case global::NetError.Facepunch_Approval_TooManyConnectedPlayersNow:
			return "Authorization busy";
		case global::NetError.Facepunch_Approval_ConnectorAuthorizeException:
			return "Server exception with authorization";
		case global::NetError.Facepunch_Approval_ConnectorAuthorizeExecution:
			return "Aborted starting of authorization";
		case global::NetError.Facepunch_Approval_ConnectorDidNothing:
			return "Server failed to start authorization";
		case global::NetError.Facepunch_Approval_ConnectorCreateFailure:
			return "Server was unable to start authorization";
		case global::NetError.Facepunch_Approval_ServerDoesNotSupportConnector:
			return "Unsupported ticket";
		case global::NetError.Facepunch_Approval_MissingServerManagement:
			return "Server is not prepared";
		case global::NetError.Facepunch_Approval_ServerLoginException:
			return "Server exception";
		case global::NetError.Facepunch_Approval_DisposedWait:
			return "Aborted authorization";
		case global::NetError.Facepunch_Approval_DisposedLimbo:
			return "Failed to run authorization";
		case global::NetError.Facepunch_Kick_MultipleConnections:
			return "Started a different connection";
		case global::NetError.Facepunch_Kick_Violation:
			return "Kicked because of violation";
		case global::NetError.Facepunch_Kick_RCON:
			return "Kicked by admin";
		case global::NetError.Facepunch_Kick_Ban:
			return "Kicked and Banned by admin";
		case global::NetError.Facepunch_Kick_BadName:
			return "Rejected name";
		case global::NetError.Facepunch_Connector_InLimboState:
			return "Lost connection during authorization";
		case global::NetError.Facepunch_Connector_WaitedLimbo:
			return "Server lost you while processing ticket";
		case global::NetError.Facepunch_Connector_RoutineMoveException:
			return "Server exception occured while awaiting authorization";
		case global::NetError.Facepunch_Connector_RoutineYieldException:
			return "Server exception occured when checking authorization";
		case global::NetError.Facepunch_Connector_MissingFeatureImplementation:
			return "Authorization produced an unhandled message";
		case global::NetError.Facepunch_Connector_Cancelled:
			return "A ticket was cancelled - try again";
		case global::NetError.Facepunch_Connector_AuthFailure:
			return "Authorization failed";
		case global::NetError.Facepunch_Connector_AuthException:
			return "Server exception while starting authorization";
		case global::NetError.Facepunch_Connector_MultipleAttempts:
			return "Multiple authorization attempts";
		case global::NetError.Facepunch_Connector_VAC_Banned:
			return "VAC banned";
		case global::NetError.Facepunch_Connector_AuthTimeout:
			return "Timed out authorizing your ticket";
		case global::NetError.Facepunch_Connector_Old:
			return "Ticket already used";
		case global::NetError.Facepunch_Connector_NoConnect:
			return "Lost authorization";
		case global::NetError.Facepunch_Connector_Invalid:
			return "Ticket invalid";
		case global::NetError.Facepunch_Connector_Expired:
			return "Ticket expired";
		case global::NetError.Facepunch_Connector_ConnectedElsewhere:
			return "Changed connection";
		case global::NetError.Facepunch_API_Failure:
			return "API Failure";
		case global::NetError.Facepunch_Whitelist_Failure:
			return "Not in whitelist";
		}
	}

	// Token: 0x04000FAD RID: 4013
	private const int mostNegativeNoErrorValue = -5;

	// Token: 0x04000FAE RID: 4014
	private const int userDefined1Value = 128;

	// Token: 0x04000FAF RID: 4015
	private const int noErrorValue = 0;

	// Token: 0x04000FB0 RID: 4016
	private const int fixErrorSignageMask = 255;

	// Token: 0x04000FB1 RID: 4017
	private const int maxUserDefinedErrorCount = 119;

	// Token: 0x04000FB2 RID: 4018
	private const string kConnectFailServerSide = "Server failed to approve the connection ";

	// Token: 0x04000FB3 RID: 4019
	private static readonly Dictionary<global::NetError, string> niceStrings = new Dictionary<global::NetError, string>(Enum.GetValues(typeof(global::NetError)).Length);
}
