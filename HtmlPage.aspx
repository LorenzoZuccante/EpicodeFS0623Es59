<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HtmlPage.aspx.cs" Inherits="ConcessionariaZuccante.HtmlPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Preventivo Auto Personalizzato</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
    <style>
        .container { margin-top: 20px; }
        .result { margin-top: 20px; }
    </style>
</head>

<body class="bg-info">
    <form id="form1" runat="server">
        <div class="container">
            <h2>Creazione Preventivo Auto</h2>
            <div class="form-group">
                <label for="ddlAuto">Seleziona Auto:</label>
                <asp:DropDownList ID="ddlAuto" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAuto_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Image ID="imgAuto" CssClass="img-fluid rounded" runat="server" />
                <asp:Label ID="lblPrezzoBase" runat="server" CssClass="form-text " />
            </div>
            <div class="form-group">
                <h4>Optional:</h4>
                <asp:CheckBoxList ID="cblOptional" CssClass="list-unstyled" runat="server"></asp:CheckBoxList>
            </div>
            <div class="form-group">
                <label for="ddlGaranzia">Anni di Garanzia:</label>
                <asp:DropDownList ID="ddlGaranzia" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <asp:Button ID="btnCalcola" CssClass="btn btn-primary" runat="server" Text="Calcola Preventivo" OnClick="btnCalcola_Click" />
            <div class="result">
                <asp:Label ID="lblRisultato" CssClass="alert alert-info bg-rounded" runat="server" />
            </div>
        </div>
    </form>
</body>

</html>
