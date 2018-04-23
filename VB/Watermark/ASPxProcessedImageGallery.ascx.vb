Imports DevExpress.Web
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Web
Imports System.Web.UI

Namespace Watermark
    Partial Public Class ASPxProcessedImageGallery
        Inherits System.Web.UI.UserControl

        <PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        Public ReadOnly Property ImageGallery() As ASPxImageGallery
            Get
                Return ASPxImageGallery
            End Get
        End Property
        <PersistenceMode(PersistenceMode.Attribute), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        Public Property ProcessedImageFolder() As String
        <PersistenceMode(PersistenceMode.Attribute), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        Public Property SourceImageFolder() As String
        <PersistenceMode(PersistenceMode.Attribute), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        Public Property WatermarkImageUrl() As String
        Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            For Each item As String In Directory.GetFiles(Server.MapPath(SourceImageFolder))
                If Not File.Exists(SourceImageFolder & "\" & Path.GetFileName(item)) Then
                    ProcessImage(item)
                End If
            Next item
            ImageGallery.SettingsFolder.ImageSourceFolder = ProcessedImageFolder
        End Sub

        Private Sub ProcessImage(ByVal name As String)
            Using image As Image = System.Drawing.Image.FromFile(name)
            Using watermarkImage As Image = System.Drawing.Image.FromFile(Server.MapPath(WatermarkImageUrl))
            Using imageGraphics As Graphics = Graphics.FromImage(image)
            Using watermarkBrush As New TextureBrush(watermarkImage)
                Dim x As Integer = (image.Width - watermarkImage.Width)
                Dim y As Integer = (image.Height - watermarkImage.Height)
                watermarkBrush.TranslateTransform(x, y)
                imageGraphics.FillRectangle(watermarkBrush, New Rectangle(New Point(x, y), New Size(watermarkImage.Width + 1, watermarkImage.Height)))
                image.Save(Server.MapPath(ProcessedImageFolder & "\" & Path.GetFileName(name)))
            End Using
            End Using
            End Using
            End Using
        End Sub
    End Class
End Namespace