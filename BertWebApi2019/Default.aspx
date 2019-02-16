<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BertWebApi2019.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bert Web API 2019</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="./api/FRCEvents">FRCEvents</a>
        </div>
        <div>
            <a href="./api/Teams">Teams</a>
        </div>
        <div>
            <a href="./api/EventTeams">Teams in each Event</a>
        </div>
        <div>
            <a href="./api/EventTeamMatches">Matches for each Team for each Event</a>
        </div>
    </form>
</body>
</html>
