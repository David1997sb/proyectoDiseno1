<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Externo.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="AppReservas.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
body {font-family: Arial, Helvetica, sans-serif;}
form {border: 3px solid #f1f1f1;}

input[type=text], input[type=password] {
    width: 100%;
    padding: 12px 20px;
    margin: 8px 0;
    display: inline-block;
    border: 1px solid #ccc;
    box-sizing: border-box;
}

.button {
    background-color: #243054;
    color: white;
    padding: 14px 20px;
    margin: 8px 0;
    border: none;
    cursor: pointer;
    width: 100%;
}

button:hover {
    opacity: 0.8;
}

.cancelbtn {
    width: 100%;
    padding: 10px 18px;
    color: white;
    background-color: #898989;
}

.imgcontainer {
    text-align: center;
    margin: 24px 0 12px 0;
}

img.avatar {
    width: 10%;
    border-radius: 10%;
}

/* Clear floats */
.clearfix::after {
    content: "";
    clear: both;
    display: table;
}

.container {
    padding: 16px;
}

span.psw {
    float: right;
    padding-top: 16px;
}

/* Change styles for span and cancel button on extra small screens */
@media screen and (max-width: 300px) {
    span.psw {
       display: block;
       float: none;
        text-align: left;
    }
    .cancelbtn {
       width: 100%;
    }
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="container">
  <div class="imgcontainer"></div>
    <h1>Registro</h1>
    <p>Complete el siguiente formulario para registrarse.</p>
    <hr>
    <label for="Nombre"><b>Nombre:</b></label>
    <asp:TextBox  runat="server" Placeholder="Ingrese su nombre" ID="Nombre" CssClass="form-control" />
    <asp:RequiredFieldValidator runat="server" ControlToValidate="Nombre" CssClass="text-danger" ErrorMessage="El campo de nombre es obligatorio." />
    <br /><label for="Identificacion"> "><b>Identificacion:</b></label>
    <asp:TextBox  runat="server" Placeholder="Ingrese su identificacion" ID="Identificacion" CssClass="form-control" />
    <asp:RequiredFieldValidator runat="server" ControlToValidate="Identificacion" CssClass="text-danger" ErrorMessage="El campo de nombre de usuario es obligatorio." />
    <br /><label for="Email"><b>Email</b></label>
    <asp:TextBox  runat="server" Placeholder="Ingrese su email" ID="Email" CssClass="form-control" />
    <asp:RequiredFieldValidator runat="server" ControlToValidate="Email" CssClass="text-danger" ErrorMessage="El campo de nombre de email es obligatorio." />
    <br /><label for="Password"><b>Password</b></label>
    <asp:TextBox  runat="server" Placeholder="Ingrese su password" ID="Password" CssClass="form-control" />
    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="El campo de nombre de password es obligatorio." />
    <br /><label for="ConfirmacionPassword"><b>Confirme password</b></label>
    <asp:TextBox  runat="server" Placeholder="Confirme su password" ID="ConfirmacionPassword" CssClass="form-control" />
    <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmacionPassword" CssClass="text-danger" ErrorMessage="La confirmación del password es obligatoria." />
    <div class="clearfix">

    <asp:Button type="button" CssClass="button" ID="btnRegistro"  runat="server" Text="Registrarme" OnClick="btnRegistro_Click" /> 
    <br><br>
    <asp:Button type="button" CssClass="cancelbtn" ID="btnCancelar"  runat="server" Text="Cancelar" />
    
    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                <p class="text-danger">
                    <asp:Literal runat="server" ID="FailureText" />
                </p>
      </asp:PlaceHolder>
          
      <label>
      <input type="checkbox" checked="checked" name="remember" style="margin-bottom:15px"> Recordarme
    </label>
    </div>
  </div>
</asp:Content>
