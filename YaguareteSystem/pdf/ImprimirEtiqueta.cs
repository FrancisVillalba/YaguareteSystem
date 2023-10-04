using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.qrcode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using YaguareteSystem.Clases;

namespace YaguareteSystem.pdf
{
    public class ImprimirEtiqueta
    {
        EtiquetaBLL et = new EtiquetaBLL();
        NumerosEnLetras n = new NumerosEnLetras();
        public byte[] generaPdfEtiqueta(int materialID)
        {
            MemoryStream ms = new MemoryStream();
            Document document = new Document(); 
            document.SetPageSize(PageSize.A4.Rotate());
            document.SetMargins(5, 5, 5, 5);
            PdfWriter pw = PdfWriter.GetInstance(document, ms); 
            PdfContentByte cd = new PdfContentByte(pw);
            DataTable dt = et.RetornaDatosImprimirEtiqueta(materialID);

            document.Open();

            foreach (DataRow row2 in dt.Rows)
            { 
                BaseFont _h2_letrasChico = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
                iTextSharp.text.Font h2_letrasChico = new iTextSharp.text.Font(_h2_letrasChico, 40f, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));

                BaseFont _h2_letrasMediano = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
                iTextSharp.text.Font h2_letrasMediano = new iTextSharp.text.Font(_h2_letrasMediano, 50f , iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));

                BaseFont _h2_letrasGrande = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
                iTextSharp.text.Font h2_letrasGrande = new iTextSharp.text.Font(_h2_letrasGrande, 65f, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));
                
                var cuadro1 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100 };
                cuadro1.AddCell(new PdfPCell(new Phrase(row2["empresa"].ToString() + " - " + row2["matIdEmpresa"].ToString(), h2_letrasMediano)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                cuadro1.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                document.Add(cuadro1); 

                var cuadro2 = new PdfPTable(new float[] { 100f });
                BarcodeCodabar codabar = new BarcodeCodabar();
                codabar.Code = "A" + row2["matCodigo"].ToString() + "A";
                codabar.StartStopText = false;
                codabar.Font = null;
                iTextSharp.text.Image _img = codabar.CreateImageWithBarcode(cd, BaseColor.BLACK, BaseColor.BLACK);
                _img.ScaleAbsolute(500,150);
                cuadro2.AddCell(new PdfPCell(_img) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                document.Add(cuadro2);

                var cuadro6 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100 };
                cuadro6.AddCell(new PdfPCell(new Phrase(row2["matCodigo"].ToString(), h2_letrasGrande)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                document.Add(cuadro6);

                var cuadro4 = new PdfPTable(new float[] { 100f  }) { WidthPercentage = 100 };
                cuadro4.AddCell(new PdfPCell(new Phrase(row2["matNombre"].ToString(), h2_letrasChico)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                document.Add(cuadro4);

                var cuaro5 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100 };
                //cuaro5.AddCell(new PdfPCell(new Phrase(DateTime.Now.ToString(), h2_letrasMediano)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                cuaro5.AddCell(new PdfPCell(new Phrase(row2["matUbicacion"].ToString(), h2_letrasMediano)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                document.Add(cuaro5); 
            } 

            document.Close(); 
            byte[] bytesStream = ms.ToArray(); 

            return bytesStream;
             
        }
    }
}