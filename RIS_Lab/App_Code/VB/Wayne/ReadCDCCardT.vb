Imports System
Imports System.Text
Imports System.Runtime.InteropServices

Public Class ReadCDCCardT

    Private Structure SCARD_IO_REQUEST
        Public dwProtocol As Integer
        Public cbPciLength As Integer
    End Structure

    '引用 PC/SC(Personal Computer/Smart Card) API WinScard.dll

    <DllImport("WinScard.dll")> _
    Private Shared Function SCardEstablishContext(ByVal dwScope As UInteger, ByVal nNotUsed1 As Integer, ByVal nNotUsed2 As Integer, ByRef phContext As Integer) As Integer
    End Function

    <DllImport("WinScard.dll")> _
    Private Shared Function SCardReleaseContext(ByVal phContext As Integer) As Integer
    End Function

    <DllImport("WinScard.dll")> _
    Private Shared Function SCardConnect(ByVal hContext As Integer, ByVal cReaderName As String, ByVal dwShareMode As UInteger, ByVal dwPrefProtocol As UInteger, ByRef phCard As Integer, ByRef ActiveProtocol As Integer) As Integer
    End Function

    <DllImport("WinScard.dll")> _
    Private Shared Function SCardDisconnect(ByVal hCard As Integer, ByVal Disposition As Integer) As Integer
    End Function

    <DllImport("WinScard.dll")> _
    Private Shared Function SCardListReaders(ByVal hContext As Integer, ByVal cGroups As String, ByRef cReaderLists As String, ByRef nReaderCount As Integer) As Integer
    End Function

    <DllImport("WinScard.dll")> _
    Private Shared Function SCardTransmit(ByVal hCard As Integer, ByRef pioSendPci As SCARD_IO_REQUEST, ByVal pbSendBuffer() As Byte, ByVal cbSendLength As Integer, ByRef pioRecvPci As SCARD_IO_REQUEST, ByRef pbRecvBuffer As Byte, ByRef pcbRecvLength As Integer) As Integer
    End Function

    '''
    ''' 取得自然人憑證的卡號
    '''
    '''
    '''
    Public Function GetCardNumber() As String
        Dim ContextHandle As Integer = 0, CardHandle As Integer = 0, ActiveProtocol As Integer = 0, ReaderCount As Integer = -1
        Dim ReaderList As String = String.Empty '讀卡機名稱列表
        Dim SendPci, RecvPci As SCARD_IO_REQUEST

        'SCardTransmit (handle 0xEA0A0000):
        'transmitted:
        '80 A4 00 00 02 3F 00
        'received:
        '90 00
        Dim SelEFAPDU_1() As Byte = {&H80, &HA4, &H0, &H0, &H2, &H3F, &H0} 'Select Elementary File 的 APDU
        'SCardTransmit (handle 0xEA0A0000):
        'transmitted:
        '80 A4 00 00 02 09 00
        'received:
        '90 00
        Dim SelEFAPDU_2() As Byte = {&H80, &HA4, &H0, &H0, &H2, &H9, &H0} 'Select Elementary File 的 APDU

        'SCardTransmit (handle 0xEA0A0000):
        'transmitted:
        '80 A4 00 00 02 09 03
        'received:
        '90 00
        Dim SelEFAPDU_3() As Byte = {&H80, &HA4, &H0, &H0, &H2, &H9, &H3} 'Select Elementary File 的 APDU

        'SCardTransmit (handle 0xEA0A0000):
        'transmitted:
        '80 B0 00 00 10
        'received:
        '54 50 30 30 30 30 30 30 30 31 36 31 31 31 31 31 90 00
        Dim ReadSNAPDU() As Byte = {&H80, &HB0, &H0, &H0, &H10} '由offset 0 讀取 0x10位 Binary 資料的 APDU

        Dim SelEFRecvBytes(1) As Byte '應回 90 00
        Dim SelEFRecvLength As Integer = 2
        Dim SNRecvBytes(17) As Byte '接收卡號的 Byte Array
        Dim SnRecvLength As Integer = 18

        '建立 Smart Card API
        If SCardEstablishContext(0, 0, 0, ContextHandle) = 0 Then

            '列出可用的 Smart Card 讀卡機
            If SCardListReaders(ContextHandle, Nothing, ReaderList, ReaderCount) = 0 Then

                '建立 Smart Card 連線
                If SCardConnect(ContextHandle, ReaderList, 1, 2, CardHandle, ActiveProtocol) = 0 Then

                    RecvPci.dwProtocol = ActiveProtocol
                    SendPci.dwProtocol = RecvPci.dwProtocol

                    RecvPci.cbPciLength = 8
                    SendPci.cbPciLength = RecvPci.cbPciLength

                    '下達 Select FE14 檔的 APDU
                    If SCardTransmit(CardHandle, SendPci, SelEFAPDU_1, SelEFAPDU_1.Length, RecvPci, SelEFRecvBytes(0), SelEFRecvLength) = 0 Then
                        If SCardTransmit(CardHandle, SendPci, SelEFAPDU_2, SelEFAPDU_2.Length, RecvPci, SelEFRecvBytes(0), SelEFRecvLength) = 0 Then
                            If SCardTransmit(CardHandle, SendPci, SelEFAPDU_3, SelEFAPDU_3.Length, RecvPci, SelEFRecvBytes(0), SelEFRecvLength) = 0 Then

                                '下達讀取卡號指令
                                If SCardTransmit(CardHandle, SendPci, ReadSNAPDU, ReadSNAPDU.Length, RecvPci, SNRecvBytes(0), SnRecvLength) = 0 Then
                                    Return Encoding.Default.GetString(SNRecvBytes, 0, 16)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
        Return ""
    End Function

End Class


