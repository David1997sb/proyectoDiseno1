<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="frmHabitacion.aspx.cs" Inherits="AppReservas.frmHabitacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .auto-style1 {
        }
        .auto-style2 {
            width: 85px;
            height: 22px;
        }
        .auto-style3 {
            height: 22px;
        }
        .auto-style4 {
            width: 279px;
        }
        .auto-style5 {
            height: 22px;
            width: 279px;
        }
        .auto-style6 {
            height: 23px;
        }
        .auto-style7 {
            width: 279px;
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:GridView ID="grdHoteles" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>


    <table style="width:100%;">
        <tr>
            <td class="auto-style6">Codigo</td>
            <td class="auto-style7">
                <asp:TextBox ID="txtHabCod" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style6"></td>
        </tr>
        <tr>
            <td class="auto-style6">Cantidad de huespedes</td>
            <td class="auto-style7">
                <asp:TextBox ID="txtCantHuesp" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style6"></td>
        </tr>
        <tr>
            <td class="auto-style2">Tipo</td>
            <td class="auto-style5">
                <asp:TextBox ID="txtTipo" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style3"></td>
        </tr>
        <tr>
            <td class="auto-style1">Precio</td>
            <td class="auto-style4">
                <asp:TextBox ID="txtPrecio" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">Estado</td>
            <td class="auto-style4">
                <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:Button ID="btnIngresar" runat="server" OnClick="btnIngresar_Click" Text="Ingresar" />
            </td>
            <td class="auto-style4">
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1" colspan="3">
                <asp:Label ID="lblResultado" runat="server" 
                    ForeColor="#009933" Text="Label" 
                    Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
