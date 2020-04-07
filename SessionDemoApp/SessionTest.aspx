<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionTest.aspx.cs" Inherits="SessionDemoApp.SessionTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Application and Session Object demonstrator</title>
    <link href="MainStyles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="Table100">
            <colgroup>
                <col style="width:70px"/>
                <col />
                <col style="width:40px"/>
                <col />
                <col style="width:70px"/>
            </colgroup>
            <tbody>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td colspan="3">
                        <h1>HttpSessionState and HttpApplicationState Variable Demonstrator</h1>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        
                       
                        <table>
                            <tbody>
                                <tr>
                                    <td class="paragraphBold">
                                        Application storage status for session: </td>
                                    <td>
                                        &nbsp;
                                        <asp:Button ID="cmdRefresh" runat="server" onclick="cmdRefresh_Click" 
                                            Text="Refresh Content" CssClass="blueButton roundButton" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <asp:TextBox ID="txtSessionContent" runat="server" Height="300px" ReadOnly="true"
                            TextMode="MultiLine"  Width="700px" CssClass="textBoxLarge"></asp:TextBox>
                        <br />
                        <br />
                        <table>
                            <tbody>
                                <tr>
                                    <td class="paragraphBold">
                                        Type in some text to add to the HttpSessionState object:</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtAddSessionContent" runat="server" Width="500px" CssClass="textBoxLarge"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="cmdAddSessionContent" runat="server" onclick="cmdAddSessionContent_Click" 
                                            Text="Add Content" CssClass="blueButton roundButton" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>    
                    </td>
                    <td>

                    </td>
                    <td>
                        <br />
                        <table>
                            <tbody>
                                <tr>
                                    <td class="paragraphBold">
                                        Application storage state for HttpApplicationState object:
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <asp:TextBox ID="txtApplicationContent" runat="server" Height="300px" 
                            ReadOnly="True" TextMode="MultiLine" Width="700px" CssClass="textBoxLarge"></asp:TextBox>
                        <br />
                        <br />
                        <table>
                            <tbody>
                                <tr>
                                    <td class="paragraphBold">
                                        Type in some text to add to the HttpApplicationState object:</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtAddApplicationContent" runat="server" Width="500px" CssClass="textBoxLarge"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="cmdAddApplicationContent" runat="server" onclick="cmdAddApplicationContent_Click" 
                                            Text="Add Content" CssClass="roundButton blueButton" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>    
                    &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </tbody>
        </table>

    </div>
    </form>
</body>
</html>
