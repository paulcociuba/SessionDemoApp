<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionMaddness.aspx.cs" Inherits="SessionDemoApp.SessionMaddness" Async="true" %>

<!DOCTYPE html>
<script runat="server">

    protected void cmdInduceLatencyBlock_Click(object sender, EventArgs e)
    {

    }
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Session Testing Application</title>
    <link href="MainStyles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptManager" runat="server">
        </asp:ScriptManager>
        <div class="paragraphStandardLeft">
            <h1>        
                Managed Session Maddness...
            </h1>
            <br />
            <strong>Session Identifier is: </strong><asp:Label ID="lblSessionID" runat="server" ViewStateMode="Disabled"/>
            <br />
            <strong>Process Identifier (PID) is: </strong><asp:Label ID="lblProcessID" runat="server" ViewStateMode="Disabled" />
            <br />
            <br />
            <div class="DivInlineBlock" style="width: 30px">

            </div>
            <div class="DivInlineBlock fieldSetDefault" style="width: 470px">
            
                <strong>Session Contents:</strong>
                <br />

                <asp:UpdateProgress ID="udppSessionContents" runat="server" AssociatedUpdatePanelID="udpSessionContents">
                    <ProgressTemplate>
                        <div class="alert-warning"> Operation in progress ...</div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                <asp:UpdatePanel ID="udpSessionContents" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>

                        <asp:TextBox ID="txtSessionContents" runat="server" TextMode="MultiLine" CssClass="textBoxLarge" ReadOnly="true" Width="450px" Height="330px" />
                        <br />
                        <asp:TextBox ID="txtSessionAdd" runat="server" TextMode="SingleLine" CssClass="textBoxLarge" Width="300px" />

                        <asp:Button ID="cmdAddContent" runat="server" Text="Add Content" CssClass="roundButton greenButton" OnClick="cmdAddContent_Click"/>

                        <asp:Button ID="cmdRefreshContent" runat="server" Text="Refresh" CssClass="roundButton blueButton" OnClick="cmdRefreshContent_Click"/>

                        <asp:Label ID="lblLastRefreshTime" runat="server" ViewStateMode="Disabled" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            
            </div>
            
            <div class="DivInlineBlock" style="width: 20px">
                &nbsp;
            </div>

            <div class="DivInlineBlock DivVerticalTop" style="width: 470px">
                
                <div class="fieldSetDefault">
                    <strong>Long Running Operations:</strong>
                    <br />
                    <asp:UpdateProgress ID="udppLongOperation" runat="server" AssociatedUpdatePanelID="udpLongOperation">
                        <ProgressTemplate>
                            <div class="alert-warning"> Operation in progress ...</div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                    <asp:UpdatePanel ID="udpLongOperation" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>

                            Desired Latency: <asp:TextBox ID="txtLatencyTime" runat="server" TextMode="Number" Width="200px" CssClass="textBoxLarge" />
                            <br />
                            <asp:Button ID="cmdInduceLatency" runat="server" Text="Run Operation" CssClass="roundButton redButton" OnClick="cmdInduceLatency_Click" />
                            
                            <asp:Button ID="cmdInduceLatencyAsync" runat="server" Text="Run Async" CssClass="roundLinkButton" OnClick="cmdInduceLatencyAsync_Click" />
                            
                            <asp:Button ID="cmdBlockAsyncLatency" runat="server" Text="Run & Block" CssClass="roundLinkButton" OnClick="cmdBlockAsyncLatency_Click" />
                            <br />
                            <br />
                            <asp:Label ID="lblDelayOperation" runat="server" ViewStateMode="Disabled" />

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                
                <br />

                <div class="fieldSetDefault">
                    <strong>Redirect on expiration:</strong>
                    <br />

                    <asp:UpdatePanel ID="udpRedirectExpiration" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>

                            Redirection url:
                            <br />
                            <asp:TextBox ID="txtRedirectUrl" runat="server" TextMode="Url" CssClass="textBoxLarge" Width="440px" />
                            <br />
                            <asp:Button ID="cmdRedirect" runat="server" Text="Redirect" CssClass="roundButton blueButton" OnClick="cmdRedirect_Click" />
                            <br />
                            <asp:Label ID="lblRedirectEngaged" runat="server" ViewStateMode="Disabled" />

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                
            </div>
          
            <br />
            <br />
             
            <div class="DivInlineBlock" style="width: 30px">
                &nbsp;
            </div>
            <div class="DivInlineBlock DivVerticalTop fieldSetDefault" style="width: 470px">
                
                <strong>Display the current time and date:</strong>
                <br />

                <asp:UpdateProgress ID="udppDateTime" runat="server" AssociatedUpdatePanelID="udpDateTime">
                    <ProgressTemplate>
                        <div class="alert-warning"> Operation in progress ...</div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                <asp:Timer ID="tmrTimeRefresh" runat="server" Interval="2000" OnTick="tmrTimeRefresh_Tick" Enabled="false"></asp:Timer>

                <asp:UpdatePanel ID="udpDateTime" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="tmrTimeRefresh" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>

                        Execution start date / time: <asp:Label ID="lblStartDateTime" runat="server" ViewStateMode="Disabled" />
                        <br />
                        Execution completion date / time: <asp:Label ID="lblEndDateTime" runat="server" ViewStateMode="Disabled" />
                        <br />
                        <br />
                        <asp:Button ID="cmdGetDateTime" runat="server" Text="Get Time & Date" CssClass="roundButton blueButton" OnClick="cmdGetDateTime_Click" />
                        <asp:Button ID="cmdAutoRefreshDateTime" runat="server" Text="Auto-Refresh" CssClass="roundButton greenButton" OnClick="cmdAutoRefreshDateTime_Click" />

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="DivInlineBlock" style="width: 20px">
                &nbsp;
            </div>

            <div class="DivInlineBlock DivVerticalTop fieldSetDefault" style="width: 470px">
                
                <strong>Damage session content </strong>

                <asp:UpdatePanel ID="udpDamageSession" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>

                        <asp:Button ID="cmdDamageSession" runat="server" Text="Damage Session" CssClass="roundButton redButton" OnClick="cmdDamageSession_Click" />
                        <asp:Button ID="cmdDamageSessionAsync" runat="server" Text="Damage Async" CssClass="roundLinkButton" OnClick="cmdDamageSessionAsync_Click" />

                        <br />
                        <br />

                        <asp:Label ID="lblDamagedSession" runat="server" ViewStateMode="Disabled" />

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            </div>
        
    </form>
</body>
</html>
