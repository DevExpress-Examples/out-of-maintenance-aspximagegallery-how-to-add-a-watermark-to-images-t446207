using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Watermark
{
	public partial class ASPxProcessedImageGallery : System.Web.UI.UserControl
	{
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ASPxImageGallery ImageGallery {
			get {
				return ASPxImageGallery;
			}
		}
		[PersistenceMode(PersistenceMode.Attribute)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public string ProcessedImageFolder { get; set; }
		[PersistenceMode(PersistenceMode.Attribute)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public string SourceImageFolder { get; set; }
		[PersistenceMode(PersistenceMode.Attribute)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public string WatermarkImageUrl { get; set; }
		protected void Page_Init(object sender, EventArgs e) {
			foreach (string item in Directory.GetFiles(Server.MapPath(SourceImageFolder))) {
				if (!File.Exists(SourceImageFolder + "\\" + Path.GetFileName(item)))
					ProcessImage(item);
			}
			ImageGallery.SettingsFolder.ImageSourceFolder = ProcessedImageFolder;
		}

		private void ProcessImage(string name) {
			using (Image image = Image.FromFile(name))
			using (Image watermarkImage = Image.FromFile(Server.MapPath(WatermarkImageUrl)))
			using (Graphics imageGraphics = Graphics.FromImage(image))
			using (TextureBrush watermarkBrush = new TextureBrush(watermarkImage)) {
				int x = (image.Width - watermarkImage.Width);
				int y = (image.Height - watermarkImage.Height);
				watermarkBrush.TranslateTransform(x, y);
				imageGraphics.FillRectangle(watermarkBrush, new Rectangle(new Point(x, y), new Size(watermarkImage.Width + 1, watermarkImage.Height)));
				image.Save(Server.MapPath(ProcessedImageFolder + "\\" + Path.GetFileName(name)));
			}
		}
	}
}