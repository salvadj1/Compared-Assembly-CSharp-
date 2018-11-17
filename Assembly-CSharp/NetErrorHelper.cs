using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x0200030B RID: 779
public static class NetErrorHelper
{
	// Token: 0x06001DFB RID: 7675 RVA: 0x00075AEC File Offset: 0x00073CEC
	static NetErrorHelper()
	{
		NetErrorHelper.CacheNiceStrings();
		NetworkConnectionError networkConnectionError = 0;
		foreach (object obj in Enum.GetValues(typeof(NetworkConnectionError)))
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

	// Token: 0x06001DFC RID: 7676 RVA: 0x00075BD4 File Offset: 0x00073DD4
	public static NetError ToNetError(this NetworkConnectionError error)
	{
		int num = error;
		if (num < -5 && num >> 7 == -1)
		{
			num &= 255;
		}
		return (NetError)num;
	}

	// Token: 0x06001DFD RID: 7677 RVA: 0x00075C00 File Offset: 0x00073E00
	internal static NetworkConnectionError _uLink(this NetError error)
	{
		return error;
	}

	// Token: 0x06001DFE RID: 7678 RVA: 0x00075C04 File Offset: 0x00073E04
	private static void CacheNiceStrings()
	{
		foreach (object obj in Enum.GetValues(typeof(NetError)))
		{
			NetError netError = (NetError)((int)obj);
			string text = NetErrorHelper.BuildNiceString(netError);
			if (text == null && netError != NetError.NoError)
			{
				Debug.LogWarning("NetError." + obj + " has no nice string");
				text = NetErrorHelper.FallbackNiceString(netError);
			}
			NetErrorHelper.niceStrings[netError] = text;
		}
	}

	// Token: 0x06001DFF RID: 7679 RVA: 0x00075CB8 File Offset: 0x00073EB8
	private static string FallbackNiceString(NetError error)
	{
		string str = error.ToString().Replace("Facepunch_", string.Empty);
		string str2 = "(";
		int num = (int)error;
		return (str + str2 + num.ToString("X") + ")").Replace("_", " ");
	}

	// Token: 0x06001E00 RID: 7680 RVA: 0x00075D0C File Offset: 0x00073F0C
	public static string NiceString(this NetError value)
	{
		string result;
		if (NetErrorHelper.niceStrings.TryGetValue(value, out result))
		{
			return result;
		}
		return NetErrorHelper.FallbackNiceString(value);
	}

	// Token: 0x06001E01 RID: 7681 RVA: 0x00075D34 File Offset: 0x00073F34
	private static string BuildNiceString(NetError value)
	{
		switch (value)
		{
		case NetError.ProxyTargetNotConnected:
			return "Proxy target not connected";
		case NetError.ProxyTargetNotRegistered:
			return "Proxy target not registered";
		case NetError.ProxyServerNotEnabled:
			return "Proxy server not enabled";
		case NetError.ProxyServerOutOfPorts:
			return "Proxy server out of ports";
		default:
			switch (value)
			{
			case NetError.NATTargetNotConnected:
				return "NAT target not connected";
			case NetError.NATTargetConnectionLost:
				return "NAT target connection lost";
			case NetError.NATPunchthroughFailed:
				return "NAT punchthrough";
			case NetError.IncompatibleVersions:
				return "Version incompatible";
			default:
				switch (value)
				{
				case NetError.ConnectionFailed:
					return "Could not reach the server";
				default:
					switch (value + 5)
					{
					case NetError.NoError:
						return "Direct connect failed";
					case (NetError)1:
						return "Invalid server";
					case (NetError)2:
						return "Incorrect parameters";
					case (NetError)3:
						return "Could not create socket or thread";
					case (NetError)4:
						return "Already connected to different server";
					case (NetError)5:
						return null;
					default:
						if (value == NetError.IsAuthoritativeServer)
						{
							return "Authoritative server";
						}
						if (value != NetError.ApprovalDenied)
						{
							return null;
						}
						return "You've been denied from connecting";
					}
					break;
				case NetError.TooManyConnectedPlayers:
					return "Full";
				case NetError.RSAPublicKeyMismatch:
					return "RSA public key mismatch";
				case NetError.ConnectionBanned:
					return "Banned from connecting";
				case NetError.InvalidPassword:
					return "Invalid password";
				case NetError.DetectedDuplicatePlayerID:
					return "Duplicate players identified";
				}
				break;
			case NetError.ConnectionTimeout:
				return "Timed out";
			case NetError.LimitedPlayers:
				return "Server has limited players";
			}
			break;
		case NetError.Facepunch_Kick_ServerRestarting:
			return "Server restarting";
		case NetError.Facepunch_Approval_Closed:
			return "Not accepting new connections.";
		case NetError.Facepunch_Approval_TooManyConnectedPlayersNow:
			return "Authorization busy";
		case NetError.Facepunch_Approval_ConnectorAuthorizeException:
			return "Server exception with authorization";
		case NetError.Facepunch_Approval_ConnectorAuthorizeExecution:
			return "Aborted starting of authorization";
		case NetError.Facepunch_Approval_ConnectorDidNothing:
			return "Server failed to start authorization";
		case NetError.Facepunch_Approval_ConnectorCreateFailure:
			return "Server was unable to start authorization";
		case NetError.Facepunch_Approval_ServerDoesNotSupportConnector:
			return "Unsupported ticket";
		case NetError.Facepunch_Approval_MissingServerManagement:
			return "Server is not prepared";
		case NetError.Facepunch_Approval_ServerLoginException:
			return "Server exception";
		case NetError.Facepunch_Approval_DisposedWait:
			return "Aborted authorization";
		case NetError.Facepunch_Approval_DisposedLimbo:
			return "Failed to run authorization";
		case NetError.Facepunch_Kick_MultipleConnections:
			return "Started a different connection";
		case NetError.Facepunch_Kick_Violation:
			return "Kicked because of violation";
		case NetError.Facepunch_Kick_RCON:
			return "Kicked by admin";
		case NetError.Facepunch_Kick_Ban:
			return "Kicked and Banned by admin";
		case NetError.Facepunch_Kick_BadName:
			return "Rejected name";
		case NetError.Facepunch_Connector_InLimboState:
			return "Lost connection during authorization";
		case NetError.Facepunch_Connector_WaitedLimbo:
			return "Server lost you while processing ticket";
		case NetError.Facepunch_Connector_RoutineMoveException:
			return "Server exception occured while awaiting authorization";
		case NetError.Facepunch_Connector_RoutineYieldException:
			return "Server exception occured when checking authorization";
		case NetError.Facepunch_Connector_MissingFeatureImplementation:
			return "Authorization produced an unhandled message";
		case NetError.Facepunch_Connector_Cancelled:
			return "A ticket was cancelled - try again";
		case NetError.Facepunch_Connector_AuthFailure:
			return "Authorization failed";
		case NetError.Facepunch_Connector_AuthException:
			return "Server exception while starting authorization";
		case NetError.Facepunch_Connector_MultipleAttempts:
			return "Multiple authorization attempts";
		case NetError.Facepunch_Connector_VAC_Banned:
			return "VAC banned";
		case NetError.Facepunch_Connector_AuthTimeout:
			return "Timed out authorizing your ticket";
		case NetError.Facepunch_Connector_Old:
			return "Ticket already used";
		case NetError.Facepunch_Connector_NoConnect:
			return "Lost authorization";
		case NetError.Facepunch_Connector_Invalid:
			return "Ticket invalid";
		case NetError.Facepunch_Connector_Expired:
			return "Ticket expired";
		case NetError.Facepunch_Connector_ConnectedElsewhere:
			return "Changed connection";
		case NetError.Facepunch_API_Failure:
			return "API Failure";
		case NetError.Facepunch_Whitelist_Failure:
			return "Not in whitelist";
		}
	}

	// Token: 0x04000E6D RID: 3693
	private const int mostNegativeNoErrorValue = -5;

	// Token: 0x04000E6E RID: 3694
	private const int userDefined1Value = 128;

	// Token: 0x04000E6F RID: 3695
	private const int noErrorValue = 0;

	// Token: 0x04000E70 RID: 3696
	private const int fixErrorSignageMask = 255;

	// Token: 0x04000E71 RID: 3697
	private const int maxUserDefinedErrorCount = 119;

	// Token: 0x04000E72 RID: 3698
	private const string kConnectFailServerSide = "Server failed to approve the connection ";

	// Token: 0x04000E73 RID: 3699
	private static readonly Dictionary<NetError, string> niceStrings = new Dictionary<NetError, string>(Enum.GetValues(typeof(NetError)).Length);
}
