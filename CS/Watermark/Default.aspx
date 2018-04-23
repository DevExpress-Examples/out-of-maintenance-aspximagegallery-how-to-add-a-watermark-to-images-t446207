<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Watermark.WebForm1" %>

<%@ Register Src="~/ASPxProcessedImageGallery.ascx" TagPrefix="uc1" TagName="ASPxProcessedImageGallery" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<uc1:ASPxProcessedImageGallery runat="server" ID="ASPxProcessedImageGallery" ProcessedImageFolder="~/Images/Processed" SourceImageFolder="~/Images/Original" WatermarkImageUrl="~/Images/watermark.png" >
				<ImageGallery ClientInstanceName="imageGallery"
					ThumbnailWidth="120" ThumbnailHeight="160" Layout="Flow">
					<SettingsFolder ImageCacheFolder="~/Thumb" />
					<SettingsFlowLayout ItemsPerPage="5" />
					<ItemTextTemplate>
						<%# System.IO.Path.GetFileNameWithoutExtension(Container.Item.ImageUrl) %>
					</ItemTextTemplate>
				</ImageGallery>
			</uc1:ASPxProcessedImageGallery>
		</div>
	</form>
</body>
</html>
