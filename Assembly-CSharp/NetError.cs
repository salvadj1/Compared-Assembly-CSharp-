using System;

// Token: 0x020003B2 RID: 946
public enum NetError
{
	// Token: 0x04000F72 RID: 3954
	AlreadyConnectedToAnotherServer = -1,
	// Token: 0x04000F73 RID: 3955
	ApprovalDenied = 81,
	// Token: 0x04000F74 RID: 3956
	ConnectionBanned = 21,
	// Token: 0x04000F75 RID: 3957
	ConnectionFailed = 14,
	// Token: 0x04000F76 RID: 3958
	ConnectionTimeout = 70,
	// Token: 0x04000F77 RID: 3959
	CreateSocketOrThreadFailure = -2,
	// Token: 0x04000F78 RID: 3960
	DetectedDuplicatePlayerID = 23,
	// Token: 0x04000F79 RID: 3961
	EmptyConnectTarget = -4,
	// Token: 0x04000F7A RID: 3962
	IncompatibleVersions = 64,
	// Token: 0x04000F7B RID: 3963
	IncorrectParameters = -3,
	// Token: 0x04000F7C RID: 3964
	InternalDirectConnectFailed = -5,
	// Token: 0x04000F7D RID: 3965
	InvalidPassword = 22,
	// Token: 0x04000F7E RID: 3966
	IsAuthoritativeServer = 80,
	// Token: 0x04000F7F RID: 3967
	LimitedPlayers = 71,
	// Token: 0x04000F80 RID: 3968
	NATPunchthroughFailed = 63,
	// Token: 0x04000F81 RID: 3969
	NATTargetConnectionLost = 62,
	// Token: 0x04000F82 RID: 3970
	NATTargetNotConnected = 61,
	// Token: 0x04000F83 RID: 3971
	NoError = 0,
	// Token: 0x04000F84 RID: 3972
	ProxyServerNotEnabled = 92,
	// Token: 0x04000F85 RID: 3973
	ProxyServerOutOfPorts,
	// Token: 0x04000F86 RID: 3974
	ProxyTargetNotConnected = 90,
	// Token: 0x04000F87 RID: 3975
	ProxyTargetNotRegistered,
	// Token: 0x04000F88 RID: 3976
	RSAPublicKeyMismatch = 20,
	// Token: 0x04000F89 RID: 3977
	TooManyConnectedPlayers = 17,
	// Token: 0x04000F8A RID: 3978
	Facepunch_Kick_ServerRestarting = 128,
	// Token: 0x04000F8B RID: 3979
	Facepunch_Approval_Closed,
	// Token: 0x04000F8C RID: 3980
	Facepunch_Approval_TooManyConnectedPlayersNow,
	// Token: 0x04000F8D RID: 3981
	Facepunch_Approval_ConnectorAuthorizeException,
	// Token: 0x04000F8E RID: 3982
	Facepunch_Approval_ConnectorAuthorizeExecution,
	// Token: 0x04000F8F RID: 3983
	Facepunch_Approval_ConnectorDidNothing,
	// Token: 0x04000F90 RID: 3984
	Facepunch_Approval_ConnectorCreateFailure,
	// Token: 0x04000F91 RID: 3985
	Facepunch_Approval_ServerDoesNotSupportConnector,
	// Token: 0x04000F92 RID: 3986
	Facepunch_Approval_MissingServerManagement,
	// Token: 0x04000F93 RID: 3987
	Facepunch_Approval_ServerLoginException,
	// Token: 0x04000F94 RID: 3988
	Facepunch_Approval_DisposedWait,
	// Token: 0x04000F95 RID: 3989
	Facepunch_Approval_DisposedLimbo,
	// Token: 0x04000F96 RID: 3990
	Facepunch_Kick_MultipleConnections,
	// Token: 0x04000F97 RID: 3991
	Facepunch_Kick_Violation,
	// Token: 0x04000F98 RID: 3992
	Facepunch_Kick_RCON,
	// Token: 0x04000F99 RID: 3993
	Facepunch_Kick_Ban,
	// Token: 0x04000F9A RID: 3994
	Facepunch_Kick_BadName,
	// Token: 0x04000F9B RID: 3995
	Facepunch_Connector_InLimboState,
	// Token: 0x04000F9C RID: 3996
	Facepunch_Connector_WaitedLimbo,
	// Token: 0x04000F9D RID: 3997
	Facepunch_Connector_RoutineMoveException,
	// Token: 0x04000F9E RID: 3998
	Facepunch_Connector_RoutineYieldException,
	// Token: 0x04000F9F RID: 3999
	Facepunch_Connector_MissingFeatureImplementation,
	// Token: 0x04000FA0 RID: 4000
	Facepunch_Connector_Cancelled,
	// Token: 0x04000FA1 RID: 4001
	Facepunch_Connector_AuthFailure,
	// Token: 0x04000FA2 RID: 4002
	Facepunch_Connector_AuthException,
	// Token: 0x04000FA3 RID: 4003
	Facepunch_Connector_MultipleAttempts,
	// Token: 0x04000FA4 RID: 4004
	Facepunch_Connector_VAC_Banned,
	// Token: 0x04000FA5 RID: 4005
	Facepunch_Connector_AuthTimeout,
	// Token: 0x04000FA6 RID: 4006
	Facepunch_Connector_Old,
	// Token: 0x04000FA7 RID: 4007
	Facepunch_Connector_NoConnect,
	// Token: 0x04000FA8 RID: 4008
	Facepunch_Connector_Invalid,
	// Token: 0x04000FA9 RID: 4009
	Facepunch_Connector_Expired,
	// Token: 0x04000FAA RID: 4010
	Facepunch_Connector_ConnectedElsewhere,
	// Token: 0x04000FAB RID: 4011
	Facepunch_API_Failure,
	// Token: 0x04000FAC RID: 4012
	Facepunch_Whitelist_Failure
}
