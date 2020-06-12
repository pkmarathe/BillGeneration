using System;
using System.Text;
using System.Web;
using System.IO;
using System.Web.UI;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html.simpleparser;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using BarcodeLib;
using Color = System.Drawing.Color;
namespace BillGeneration.BusinessLayer
{
    public class CreatePDFManager
    {
        #region Create PDF
        public string CreatePDF(string filepath, string htmltext, string createdby, string ReceiptWidth, string ReceiptHight, int SubscriptionPlanId)
        {
            string Res = "";
            try
            {
                if (File.Exists(filepath))
                    File.Delete(filepath);
                if (createdby == "itextsharp")
                {
                    //System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    //createDiv.ID = "DynamicDiv";
                    //createDiv.InnerHtml = htmltext; // " क, ख, ग, घ "; 
                    //HttpResponse Response = HttpContext.Current.Response;
                    //StringBuilder SB = new StringBuilder();
                    //StringWriter SW = new StringWriter(SB);

                    //HtmlTextWriter htmlTW = new HtmlTextWriter(SW);
                    //createDiv.RenderControl(htmlTW);
                    //String GridViewHTML = SB.ToString();
                    //StringBuilder strHTMLContent = new StringBuilder();
                    //strHTMLContent.Append(GridViewHTML);

                    //StringReader reader = new StringReader(strHTMLContent.ToString());
                    //Document document = new Document(PageSize.A4);
                    //HTMLWorker parser = new HTMLWorker(document);
                    //PdfWriter.GetInstance(document, new FileStream(filepath, FileMode.Create));
                    //document.Open();
                    //try
                    //{
                    //    parser.Parse(reader);
                    //    document.Close();
                    //}
                    //catch (Exception ex)
                    //{
                    //    HttpContext.Current.Response.Write(ex.Message.ToString());
                    //}
                }
                else if (createdby == "pdfcrowd")
                {
                    try
                    {
                        //-----------------------------------V4 Version---------------------------------------------------------------------------------------------------
                        System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
                        pdfcrowd.HtmlToPdfClient client = new pdfcrowd.HtmlToPdfClient("quacito", "0c5d0c77d5d6bfcbcf231c1f28fcc0f0");
                        client.setTag("Bills99");
                        MemoryStream Stream = new MemoryStream();
                        if (ReceiptWidth != null && ReceiptWidth != "")
                        {
                            client.setPageWidth(ReceiptWidth);
                        }
                        if (ReceiptHight != null && ReceiptHight != "")
                        {
                            client.setPageHeight(ReceiptHight);
                        }
                        if (SubscriptionPlanId == 1)
                        {
                            client.setPageWatermarkUrl("https://app.bills99.com/assets/images/watermarkpdfimage1.pdf");
                        }
                        client.convertStringToFile(htmltext, filepath);
                        Response.Clear();
                        //-----------------------------------V2 Version---------------------------------------------------------------------------------------------------
                        //System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
                        //pdfcrowd.Client client = new pdfcrowd.Client("quacito", "0c5d0c77d5d6bfcbcf231c1f28fcc0f0", "quacito.pdfcrowd.com");

                        //MemoryStream Stream = new MemoryStream();
                        //// 12*18in - 780*1267mm
                        ////A4 - portrait 8.27*11.69in - 210*298mm landscape 298*210mm, 300pt for pixel
                        //if (ReceiptWidth != null && ReceiptWidth != "")
                        //{
                        //    client.setPageWidth(ReceiptWidth);
                        //}
                        //if (ReceiptHight != null && ReceiptHight != "")
                        //{
                        //    client.setPageHeight(ReceiptHight);
                        //}
                        //if (SubscriptionPlanId == 1)
                        //{
                        //    client.setWatermark("https://app.bills99.com/assets/images/watermarkpdfimage.png", 50, 200); // set image in backgroud in pdf
                        //    client.setWatermarkRotation(-50); // Rotates the watermark by angle degrees.
                        //    client.setWatermarkInBackground(true); // true is for backgroud else foreground
                        //}
                        //client.convertHtml(htmltext, Stream); 
                        //Response.Clear();
                        //FileStream objFile = new FileStream(filepath, FileMode.Create);
                        //Stream.WriteTo(objFile);
                        //objFile.Flush();
                        //objFile.Close();
                        //Stream.Close();
                    }
                    catch (pdfcrowd.Error why)
                    {
                        HttpContext.Current.Response.Write(why.ToString());
                    }
                }
                //else if (createdby == "texttoimage")
                //{ 
                //    string text = htmltext;
                //    Bitmap bitmap = new Bitmap(1, 1);
                //    Font font = new Font("Arial", 25, FontStyle.Regular, GraphicsUnit.Pixel);
                //    Graphics graphics = Graphics.FromImage(bitmap);
                //    int width = (int)graphics.MeasureString(text, font).Width;
                //    int height = (int)graphics.MeasureString(text, font).Height;
                //    bitmap = new Bitmap(bitmap, new Size(width, height));
                //    graphics = Graphics.FromImage(bitmap);
                //    graphics.Clear(Color.White);
                //    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                //    graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                //    graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(255, 0, 0)), 0, 0);
                //    graphics.Flush();
                //    graphics.Dispose();
                //    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".jpg";
                //    bitmap.Save(@"E:\" + fileName, ImageFormat.Jpeg);
                //}
                Res = "Ok";
            }
            catch (Exception ex)
            {
                Res = ex.Message.ToString();
            }
            return Res;
        }
        #endregion

        #region Generate BarCode
        public void GenrateBarCode(string BarCodeNo, string Path, int ImageWidth, int ImageHeight)
        {
            Barcode barcodeAPI = new Barcode();     //int imageWidth = 290;    //int imageHeight = 120; 
            Image barcodeImage = barcodeAPI.Encode(TYPE.UPCA, BarCodeNo, Color.Black, Color.Transparent, ImageWidth, ImageHeight);
            barcodeImage.Save(Path, ImageFormat.Png);
        }
        #endregion
    }
}
