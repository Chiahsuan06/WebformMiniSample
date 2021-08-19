<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm7.aspx.cs" Inherits="TryWebApplication.WebForm7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>My Title</title>
    <style>
        p:last-child
        {
            /*color:aqua;*/
        }
        span {
        }
        #span2 {
            color:aquamarine;
        }
        .cls1 {
            color:blueviolet;
        }
        .cls2 {
            color:cadetblue;
        }
        p > span {
           background-color:lightsteelblue;
        }
    </style>
</head>
<body>
    <div>
        <p class="cls1">P Text1</p>
        <p>
            <span class="cls1">First</span>
            <span id="span2" class="cls1">Second</span>
            <span class="cls2">Third</span>
        </p>
        <p class="cls2">P Text3</p>
        <span>123456</span>
    </div>

</body>
</html>
